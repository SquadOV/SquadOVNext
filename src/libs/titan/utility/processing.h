//
//  Copyright (C) 2022 Michael Bao
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
#pragma once

#include <any>
#include <atomic>
#include <boost/functional/hash.hpp>
#include <memory>
#include <optional>
#include <stdint.h>
#include <typeindex>
#include <typeinfo>
#include <unordered_map>
#include <vector>

#include "titan/dll.h"
#include "titan/utility/exception.h"

namespace titan::utility {

// Unique globally.
using NodeId = uint64_t;
// Unique only for the node and is specified by the constructor of the node.
// Note that ParamId must be unique to the node regardless of whether it's an input/output/persistent.
using ParamId = uint64_t;

CREATE_SIMPLE_EXCEPTION_CLASS(ComputeNodeOutputException, "Improperly implemented node - did not store requested node output in the cache properly.");
CREATE_SIMPLE_EXCEPTION_CLASS(NodeIncorrectTypeException, "Type mismatch.");
CREATE_SIMPLE_EXCEPTION_CLASS(NodeOutOfScopeException, "Computation was performed on a node that went out of scope.");
CREATE_SIMPLE_EXCEPTION_CLASS(ParameterNotFoundException, "Specified parameter not found.");

// A computed value is either used as an input for the node or is its output.
// Keeping track of its direction allows us to gate the public API of the node
// to *only* expose being able to retrieve its output values. Note that the
// intermediary value is to be used if there's "other" values that the node needs to
// compute/create in support of computing its output value.
enum class NodeValueDirection {
    Input,
    Output,
    Intermediary
};

class TITANEXPORT ProcessingCache {
public:
    template<typename T>
    std::optional<const T&> getValue(NodeId node, ParamId param) {
        const auto it = _values.find(std::make_pair(node, param));
        if (it == _values.end()) {
            return std::nullopt;
        }
        
        try {
            return std::any_cast<const T&>(it->second);
        } catch (...) {
            throw NodeIncorrectTypeException{};
        }
    }

    template<typename T>
    void setValue(NodeId node, ParamId param, const T& v) {
        _values[std::make_pair(node, param)] = std::make_any(v);
    }

    void clear() { _values.clear(); }

private:
    std::unordered_map<std::pair<NodeId, ParamId>, std::any, boost::hash<std::pair<NodeId, ParamId>>> _values;
};

enum class ProcessingCacheType {
    Ephemeral,
    Persistent
};

// The processing cache container has two sub-caches: the "ephemeral" and the "persistent" caches.
// Every time the graph gets evaluated, it'll be given an empty ephemeral cache while the persistent cache
// will contain the data from the previous run. This is necessary to save us from having to re-allocate
// certain memory. For example, imagine if our goal was to output a resized texture. Without a persistent cache,
// we would have to re-allocate a new texture every time we do the computation even though we're going to
// be resizing to the same size/format texture. Thus, we'll want to store that texture in the persistent cache.
// Note that node outputs *must* be stored in the ephemeral cache.
class TITANEXPORT ProcessingCacheContainer {
public:
    template<typename T>
    std::optional<const T&> getValue(ProcessingCacheType type, NodeId node, ParamId param) {
        return (type == ProcessingCacheType::Ephemeral) ?
            _ephemeral.getValue(node, param) :
            _persistent.getValue(node, param);
    }

    template<typename T>
    void setValue(ProcessingCacheType type, NodeId node, ParamId param, const T& v) {
        if (type == ProcessingCacheType::Ephemeral) {
            _ephemeral.setValue(node, param, v);
        } else {
            _persistent.setValue(node, param, v);
        }
    }

    void clearEphemeral() { _ephemeral.clear(); }

private:
    ProcessingCache _ephemeral;
    ProcessingCache _persistent;
};

// This should be able to be used as a generic way of connecting inputs and outputs in a processing graph.
// Note that we'll be assuming that this is a *pull* based graph, i.e. we'll have a reference to the final
// output node and will request that node for its latest value which will then propogate through the graph
// to compute the final output value. This representation should be generic - though its main focus is for
// doing image/audio processing.
//
// Effectively, any node is computing f(x) where x is a vector of parameters.
// A node can have zero or more input parameters as well as zero or more output parameters (though
// realistically it doesn't make much sense for a node to have 0 output parameters). It is up to the specific
// implementation of the node to determine how to best compute its outputs from its inputs - note that the
// computation will be passed a ProcessingCache to aid in not having to re-compute values when needed.
//
// To ensure a fairly generic structure, the node will make use of the "std::any" type to flow values
// from one node to the next. Each node must first register its inputs and outputs along with their types - this
// way we can check for any mismatched types at both construction and at runtime.
using ProcessingNodePtr = std::shared_ptr<class ProcessingNode>;

class TITANEXPORT ProcessingNode {
public:
    virtual ~ProcessingNode() {}

    // A globally unique ID that we can use to identify this node
    // in the processing cache.
    NodeId id() const { return _id; }

    // Connect the input of this node to the output of another node.
    void connectInputTo(ParamId inputId, const ProcessingNodePtr& outputNode, ParamId outputId);

    // Retrieve the output value (doing computation and storing in the cache if necessary).
    template<typename T>
    const T& getOutputValue(ParamId outputId, ProcessingCacheContainer& cache) const {
        const auto value = cache.getValue(ProcessingCacheType::Ephemeral, id(), outputId);
        if (value) {
            return *value;
        }
        
        compute(outputId, cache);

        const auto newValue = cache.getValue(ProcessingCacheType::Ephemeral, id(), outputId);
        if (!newValue) [[unlikely]] {
            throw ComputeNodeOutputException{};
        }

        return *newValue;
    }

    const std::type_index& getParameterType(NodeValueDirection dir, ParamId id) const;

protected:
    template<typename T>
    ParamId registerInputParameter(ParamId paramId) {
        return registerParameter<T>(NodeValueDirection::Input, paramId);
    }

    template<typename T>
    ParamId registerOutputParameter(ParamId paramId) {
        return registerParameter<T>(NodeValueDirection::Output, paramId);
    }

    // Effectively the same as getOutputValue but for querying the inputs.
    // All we need to do is figure out which node/param id we're need to query
    // and then call getOutputValue on it.
    template<typename T>
    const T& getInputValue(ParamId inputId, ProcessingCacheContainer& cache) const {
        const auto it = _inputMapping.find(inputId);
        if (it == _inputMapping.end()) [[unlikely]] {
            // TODO: Default values? Is that something that's desirable to support?
            throw ParameterNotFoundException{};
        }

        const auto& [outputNode, outputId] = it->second;
        if (ProcessingNodePtr ptr = outputNode.lock()) {
            return ptr->getOutputValue<T>(outputId, cache);
        } else [[unlikely]] {
            throw NodeOutOfScopeException{};
        }
    }

private:
    // ID needs to be globally unique.
    static inline std::atomic<NodeId> gNodeIdCounter = 0;
    const NodeId _id = gNodeIdCounter++;

    template<typename T>
    void registerParameter(NodeValueDirection dir, ParamId paramId) {
        _parameterTypeMap[std::make_pair(dir, paramId)] = std::type_index(typeid(std::remove_cv_t<T>));
    }

    // What we'll use to do type-checking when two nodes get connected before values start flowing.
    std::unordered_map<std::pair<NodeValueDirection, ParamId>, std::type_index, boost::hash<std::pair<NodeValueDirection, ParamId>>> _parameterTypeMap;

    // Each node only needs to know which nodes its inputs are connected to.
    // Note that we use a weak_ptr here instead of a shared_ptr to theoretically be able to support
    // loopback functionality at a certain point. I think that'll require some other adjustments too
    // but this is an easy enough of a change to support that. Note that this design choice does
    // necessitate the creation of a parent "graph" object that'll own all the ProcesingNodes.
    std::unordered_map<ParamId, std::pair<std::weak_ptr<ProcessingNode>, ParamId >> _inputMapping;

    // What derived nodes should override to compute values and put values into the cache.
    virtual void compute(ParamId outputId, ProcessingCacheContainer& cache) const = 0;
};

// A fairly dumb container mainly meant to take ownership of the ProcessingNode objects.
class TITANEXPORT ProcessingGraph {
public:
    void add(const ProcessingNodePtr& n) { _nodes.push_back(n); }
private:
    std::vector<ProcessingNodePtr> _nodes;
};

}
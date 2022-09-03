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
#include "av/image/processing/compositor_layer_node.h"
#include "av/image/compositor/compositor_layer.h"
#include "av/image/compositor/compositor_op.h"

namespace tu = titan::utility;
namespace av {

CompositorLayerNode::CompositorLayerNode(size_t numOps):
    _numOps(numOps)
{
    registerOutputParameter<CompositorLayerPtr>(kOutput);
    registerInputParameter<NativeImagePtr>(kInputImage);
    for (size_t i = 0; i < _numOps; ++i) {
        registerInputParameter<CompositorOpPtr>(kOpStart + i);
    }
}

void CompositorLayerNode::compute(titan::utility::ParamId outputId, titan::utility::ProcessingCacheContainer& cache) {
    auto layer = cache.getValueOrComputeIf<CompositorLayerPtr>(
        tu::ProcessingCacheType::Persistent,
        id(),
        kCacheLayer,
        [this](const CompositorLayerPtr& a) { return !a || _numOps != a->size(); },
        [this](){
            return std::make_shared<CompositorLayer>(_numOps);
        }
    );

    // This needs to be *outside* the construction of the layer since we have to handle
    // the potential scenario where the op pulls in time-varying parameters.
    for (size_t i = 0; i < _numOps; ++i) {
        const auto& op = getInputValue<CompositorOpPtr>(kOpStart + i, cache);
        layer.get()->setOp(i, op);
    }

    // Images on the other hand *may* be re-created so we need to always pull in the latest image pointer.
    const auto& image = getInputValue<NativeImagePtr>(kInputImage, cache);
    layer.get()->setBase(image);

    cache.setValue(tu::ProcessingCacheType::Ephemeral, id(), kOutput, layer.get());
}

}
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
#include "titan/utility/processing.h"

namespace titan::utility {

const std::type_index& ProcessingNode::getParameterType(NodeValueDirection dir, ParamId id) const {
    const auto it = _parameterTypeMap.find(std::make_pair(dir, id));
    if (it == _parameterTypeMap.end()) [[unlikely]] {
        throw ParameterNotFoundException{};
    }
    return it->second;
}

void ProcessingNode::connectInputTo(ParamId inputId, const ProcessingNodePtr& outputNode, ParamId outputId) {
    // First do a type check to ensure that the expected output type of the output node for this param
    // is equivalent to the expected input type of this node for the specified input param. 
    const auto& inputType = getParameterType(NodeValueDirection::Input, inputId);
    const auto& outputType = outputNode->getParameterType(NodeValueDirection::Output, outputId);
    if (inputType != outputType) [[unlikely]] {
        throw NodeIncorrectTypeException{};
    }

    _inputMapping[inputId] = std::make_pair(outputNode, outputId);
}

}
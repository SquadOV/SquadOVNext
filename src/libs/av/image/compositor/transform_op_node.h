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

#include "av/dll.h"
#include "av/image/processing/compositor_op_node.h"
#include "av/image/compositor/compositor_op.h"
#include "av/image/compositor/transform_op.h"
#include <type_traits>
#include <memory>

namespace av {

template<typename T> requires std::is_base_of_v<TransformOp, T> && std::is_base_of_v<CompositorOp, T>
class TransformOpNode: public CompositorOpNode {
public:
    enum TransformParams {
        kTranslation = 1,
        kRotation = 2,
        kScale = 3,
        kUvMode = 4,
        kOrigin = 5
    };

    explicit TransformOpNode(const std::shared_ptr<T>& op):
        CompositorOpNode(op)
    {
        registerInputParameter<Eigen::Vector2f>(kTranslation);
        registerInputParameter<float>(kRotation);
        registerInputParameter<Eigen::Vector2f>(kScale);
        registerInputParameter<TransformOp::UvMode>(kUvMode);
        registerInputParameter<TransformOp::Origin>(kOrigin);
    }
private:

    void updateOpNodeParams(titan::utility::ProcessingCacheContainer& cache) override {
        auto& top = typedOp<T>();

        const auto& translation = getInputValue<Eigen::Vector2f>(kTranslation, cache);
        top.setTranslation(translation);

        const auto rotation = getInputValue<float>(kRotation, cache);
        top.setRotation(rotation);

        const auto& scale = getInputValue<Eigen::Vector2f>(kScale, cache);
        top.setScale(scale);

        const auto& uvmode = getInputValue<TransformOp::UvMode>(kUvMode, cache);
        top.setUvMode(uvmode);

        const auto& origin = getInputValue<TransformOp::Origin>(kOrigin, cache);
        top.setOrigin(origin);
    }
};

}
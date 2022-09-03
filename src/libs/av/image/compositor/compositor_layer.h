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
#include "av/image/compositor/compositor_op.h"
#include "av/image/os_image.h"
#include <filesystem>
#include <Eigen/Geometry>
#include <memory>
#include <vector>

namespace av {

// A compositor layer is composed of an input image and a number of compositor operations.
// These operations will only be applied to the pixels of the input image in the final, composited image.
class AVEXPORT CompositorLayer {
public:
    explicit CompositorLayer(size_t numOps);

    void setOp(size_t idx, const CompositorOpPtr& op);
    size_t size() const { return _ops.size(); }

    void setBase(const NativeImagePtr& v) { _baseImage = v; }
    void renderTo(const NativeImagePtr& canvas, const Eigen::AlignedBox2i& bounds) const;

private:
    std::vector<CompositorOpPtr> _ops;
    NativeImagePtr _baseImage;
};

using CompositorLayerPtr = std::shared_ptr<CompositorLayer>;

}
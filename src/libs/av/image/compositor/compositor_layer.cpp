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
#include "av/image/compositor/compositor_layer.h"

namespace av {

CompositorLayer::CompositorLayer(size_t numOps) {
    _ops.resize(numOps);
}

void CompositorLayer::setOp(size_t idx, const CompositorOpPtr& op) {
    _ops[idx] = op;
}

void CompositorLayer::renderTo(const NativeImagePtr& canvas, const Eigen::AlignedBox2i& bounds) const {
    if (!_baseImage) {
        return;
    }

    for (const auto& op: _ops) {
        if (!op) {
            continue;
        }

        op->updateCanvas(bounds);
        op->render(*canvas, *_baseImage);
    }
}

}
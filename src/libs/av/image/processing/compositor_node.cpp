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
#ifdef _WIN32
#include <Windows.h>
#include <d3d11.h>
#endif

#include "av/image/processing/compositor_node.h"
#include "av/image/os_image.h"
#include "av/image/compositor/compositor_layer.h"

namespace tu = titan::utility;
namespace av {

CompositorNode::CompositorNode(size_t numLayers, size_t width, size_t height):
    _numLayers(numLayers),
    _width(width),
    _height(height)
{
    registerOutputParameter<NativeImagePtr>(kOutput);
    for (size_t i = 0; i < _numLayers; ++i) {
        registerInputParameter<CompositorLayerPtr>(kLayerStart + i);
    }
}

}
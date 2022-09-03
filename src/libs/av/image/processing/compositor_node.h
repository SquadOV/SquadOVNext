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
#include <titan/utility/processing.h>

namespace av {

// The compositor node will combine multiple layers (i.e. images) together into a single final image.
// Theoretically we could do this piecewise, i.e. have a resize node, HDR tonemapping node, etc. that
// are crafted similar to the ingest node. But then we'd be allocating a lot of memory for textures
// since we're assuming that an output of a node is immutable. Imagine a scenario where a node that
// produces images is output to two other nodes. If one of those other nodes modifies the output texture,
// then the other input node would receive the incorrect texture.
//
// To get around this, we introduce the concept of the "compositor." Each compositor layer is made up of
// 1) an input texture and 2) a series of "operations" (resize, filters, etc.) that we want to perform on that image.
// The final compositor node will then go through each input texture and rasterize it onto the final texture.
// Each operation is coded as a compute operation that operates on the bounds of the layer on the output texture.
// This way the operations are performed on as few pixels as possible.
class AVEXPORT CompositorNode: public titan::utility::ProcessingNode {
public:
    CompositorNode(size_t numLayers, size_t width, size_t height);

    enum Params {
        kOutput = 0,
        kCache = 1,
        kLayerStart = 2
    };

    size_t size() const { return _numLayers; }
    size_t width() const { return _width; }
    size_t height() const { return _height; }

private:
    size_t _numLayers = 0;
    size_t _width = 0;
    size_t _height = 0;
};

}
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

// The compositor layer will output into a compositor and will have as input
// some input image along with a number of compositor operations.
class AVEXPORT CompositorLayerNode: public titan::utility::ProcessingNode {
public:
    enum Params {
        kOutput = 0,
        kInputImage = 1,
        kCacheLayer = 2,
        kOpStart = 3
    };
    explicit CompositorLayerNode(size_t numOps);
private:
    size_t _numOps = 0;

    void compute(titan::utility::ParamId outputId, titan::utility::ProcessingCacheContainer& cache) override;
};

}
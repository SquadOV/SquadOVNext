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
#ifdef _WIN32

#include "av/dll.h"
#include "av/image/processing/compositor_node.h"
#include <titan/system/win32/directx/device.h>

namespace av::directx {

// This ingest node generally needs to be created and placed right after the image capture source.
// This is because the image capture source could be on a different device/context and we want to
// bring it into some shared device/context that'll be used for the rest of the pipeline.
class AVEXPORT CompositorDx11Node : public av::CompositorNode {
public:
    enum Params {
        kInput = 0,
        kOutput = 1,
        kCache = 2,
        kStagingGpu = 3,
        kStagingCpu = 4
    };

    CompositorDx11Node(const titan::system::win32::D3d11SharedDevicePtr& device, size_t numLayers, size_t width, size_t height);
private:
    titan::system::win32::D3d11SharedDevicePtr _device;

    void compute(titan::utility::ParamId outputId, titan::utility::ProcessingCacheContainer& cache) override;
};

}

#endif
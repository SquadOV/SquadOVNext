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
#include "av/image/image.h"
#include "av/image/cpu_image.h"

#ifdef _WIN32
#include <Windows.h>
#include <d3d11.h>
#include <wil/com.h>

#include <titan/system/win32/directx/device.h>
#endif

namespace av {

// A simple wrapper around whatever the "native" image format is for this operating system.
// Generally, we'll assume that the image can either live in hardware or on the CPU.
class AVEXPORT NativeImage: public IImage {
public:
#ifdef _WIN32
    // The input device should be the same device the texture was created on.
    NativeImage(const wil::com_ptr<ID3D11Texture2D>& texture, const titan::system::win32::D3d11SharedDevicePtr& device);

    const titan::system::win32::D3d11SharedDevicePtr& device() const { return _device; }
#endif

    size_t width() const override;
    size_t height() const override;
    ImageFormat format() const override;

#ifdef _WIN32
    DXGI_FORMAT nativeFormat() const { return _desc.Format; };
#endif

    // Create an image in a place that's accessible by the CPU that is compatible with this image.
    NativeImage createCompatibleStagingImage() const;

    // Same device location copying is easy and can generally be done with a simple graphics API call.
    void copyToSameDeviceLocation(NativeImage& to) const;

    // Copying to the CPU will generally rely on us copying to a temporary staging buffer that we can use to
    // ensure that we can actually read from the GPU to the CPU.
    void copyToCpu(CpuImage& to) const;

    // Copying from an image on the CPU. Pretty straightforward operation.
    void copyFromCpu(const CpuImage& from);
private:

#ifdef _WIN32
    wil::com_ptr<ID3D11Texture2D> _native;
    D3D11_TEXTURE2D_DESC _desc;
    titan::system::win32::D3d11SharedDevicePtr _device;
#endif

};

}
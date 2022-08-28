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

#ifdef _WIN32
#include <Windows.h>
#include <d3d11.h>
#include <wil/com.h>

#include <titan/system/win32/directx/device.h>
#endif

namespace av {

// A simple wrapper around whatever the "native" image format is for this operating system.
class AVEXPORT NativeImage: public IImage {
public:
#ifdef _WIN32
    // The input device should be the same device the texture was created on.
    NativeImage(const wil::com_ptr<ID3D11Texture2D>& texture, const titan::system::win32::D3d11SharedDevicePtr& device);
#endif

    size_t width() const override;
    size_t height() const override;
    size_t bytesPerPixel() const override;
    size_t channels() const override;

    void fillRawBuffer(std::vector<uint8_t>& buffer) const override;
private:

#ifdef _WIN32
    wil::com_ptr<ID3D11Texture2D> _native;
    D3D11_TEXTURE2D_DESC _desc;
    titan::system::win32::D3d11SharedDevicePtr _device;
#endif

};

}
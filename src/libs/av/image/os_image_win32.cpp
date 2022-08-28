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
#include <titan/system/win32/exceptions.h>

#include "av/image/os_image.h"
#include "av/image/image_exceptions.h"

namespace av {

NativeImage::NativeImage(ID3D11Texture2D* texture, const titan::system::win32::D3d11SharedDevicePtr& device):
    _native(texture),
    _device(device)
{
    if (!_native) {
        return;
    }
    _native->GetDesc(&_desc);
}

size_t NativeImage::width() const {
    return _desc.Width;
}

size_t NativeImage::height() const {
    return _desc.Height;
}

size_t NativeImage::bytesPerPixel() const {
    switch (_desc.Format) {
        case DXGI_FORMAT_B8G8R8A8_UNORM:
            return 4;
        default:
            throw av::UnsupportedImageFormat{};
            break;
    }
    return 0;
}

size_t NativeImage::channels() const {
    switch (_desc.Format) {
        case DXGI_FORMAT_B8G8R8A8_UNORM:
            return 4;
        default:
            throw av::UnsupportedImageFormat{};
            break;
    }
    return 0;
}

void NativeImage::fillRawBuffer(std::vector<uint8_t>& buffer) const {
    if (!_native) {
        return;
    }

    buffer.resize(width() * height() * bytesPerPixel());

    const auto immediate = _device->immediate();
    const auto guard = immediate->get();

    D3D11_MAPPED_SUBRESOURCE mappedData;
    HRESULT hr = guard.context()->Map(_native.get(), 0, D3D11_MAP_READ, 0, &mappedData);
    CHECK_WIN32_HRESULT_THROW(hr);

    D3D11_TEXTURE2D_DESC desc;
    _native->GetDesc(&desc);

    const uint8_t* src = reinterpret_cast<const uint8_t*>(mappedData.pData);
    uint8_t* dst = &buffer[0];
    for (size_t r = 0; r < height(); ++r) {
        std::memcpy(dst, src, bytesPerRow());
        src += mappedData.RowPitch;
        dst += bytesPerRow();
    }
    guard.context()->Unmap(_native.get(), 0);

}

}
#endif // _WIN32
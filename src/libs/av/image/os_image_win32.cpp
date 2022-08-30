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
#include <titan/system/win32/directx/shared_texture.h>

#include "av/image/os_image.h"

namespace av {

NativeImage::NativeImage(const wil::com_ptr<ID3D11Texture2D>& texture, const titan::system::win32::D3d11SharedDevicePtr& device):
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

ImageFormat NativeImage::format() const {
    switch (_desc.Format) {
        using enum ImageFormat;
        case DXGI_FORMAT_B8G8R8A8_UNORM:
            return B8R8G8A8_UNORM;
        case DXGI_FORMAT_R16G16B16A16_FLOAT:
            return R16G16B16A16_FLOAT;
        default:
            throw av::UnsupportedImageFormat{};
            break;
    }
}

NativeImage NativeImage::createCompatibleStagingImage() const {
    D3D11_TEXTURE2D_DESC newDesc = { 0 };
    newDesc.Width = width();
    newDesc.Height = height();
    newDesc.MipLevels = 1;
    newDesc.ArraySize = 1;
    newDesc.Format = nativeFormat();
    newDesc.SampleDesc.Count = 1;
    newDesc.Usage = D3D11_USAGE_STAGING;
    newDesc.BindFlags = 0;
    newDesc.CPUAccessFlags = D3D11_CPU_ACCESS_READ;
    newDesc.MiscFlags = 0;

    wil::com_ptr<ID3D11Texture2D> newTexture;
    HRESULT hr = _device->device()->CreateTexture2D(&newDesc, nullptr, &newTexture);
    CHECK_WIN32_HRESULT_THROW(hr, "Failed to create compatible staging texture.");
    return NativeImage{newTexture, _device};
}

void NativeImage::copyToSameDeviceLocation(NativeImage& to) const {
    assert(areGenericImagesCompatible(*this, to));
    auto immediate = _device->immediate()->get();

    if (to.device().get() != device().get()) {
        titan::system::win32::D3d11SharedTextureHandle sharedHandle{*_device, to._native.get(), false};
        immediate.context()->CopyResource(sharedHandle.texture(), _native.get());
        immediate.context()->Flush();
    } else {
        immediate.context()->CopyResource(to._native.get(), _native.get());
    }
}

void NativeImage::copyToCpu(CpuImage& to) const {
    assert(areGenericImagesCompatible(*this, to));

    auto immediate = _device->immediate()->get();
    D3D11_MAPPED_SUBRESOURCE mappedData;
    HRESULT hr = immediate.context()->Map(_native.get(), 0, D3D11_MAP_READ, 0, &mappedData);
    CHECK_WIN32_HRESULT_THROW(hr, "Failed to map texture.");

    const uint8_t* src = reinterpret_cast<const uint8_t*>(mappedData.pData);
    OIIO::ImageBuf& dst = to.raw();
    dst.set_pixels(OIIO::ROI::All(), dst.pixeltype(), src, OIIO::AutoStride, mappedData.RowPitch);
    immediate.context()->Unmap(_native.get(), 0);
}

void NativeImage::copyFromCpu(const CpuImage& from) {
    assert(areGenericImagesCompatible(*this, from));

    D3D11_BOX box;
    box.left = 0;
    box.right = width();
    box.top = 0;
    box.bottom = height();
    box.front = 0;
    box.back = 1;

    auto immediate = _device->immediate()->get();
    immediate.context()->UpdateSubresource(_native.get(), 0, &box, from.raw().localpixels(), from.raw().scanline_stride(), 0);
}

}
#endif // _WIN32
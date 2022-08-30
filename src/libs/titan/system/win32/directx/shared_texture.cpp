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

#include "titan/system/win32/directx/shared_texture.h"
#include "titan/system/win32/exceptions.h"

namespace titan::system::win32 {

D3d11SharedTextureHandle::D3d11SharedTextureHandle(D3d11SharedDevice& destContext, ID3D11Texture2D* srcTexture, bool canWrite) {
    D3D11_TEXTURE2D_DESC desc;
    srcTexture->GetDesc(&desc);

    _isNtHandle = (desc.MiscFlags & D3D11_RESOURCE_MISC_SHARED_NTHANDLE);

    HRESULT hr = S_OK;
    if (_isNtHandle) {
        hr = srcTexture->QueryInterface(__uuidof(IDXGIResource1), (void**)&_frameResource1);
        CHECK_WIN32_HRESULT_THROW(hr, "Failed to get frame DXGI resource (NT).");

        DWORD access = DXGI_SHARED_RESOURCE_READ;
        if (canWrite) {
            access |= DXGI_SHARED_RESOURCE_WRITE;
        }

        hr = _frameResource1->CreateSharedHandle(nullptr, access, nullptr, &_frameHandle);
        CHECK_WIN32_HRESULT_THROW(hr, "Failed to get frame shared handle (NT).");
        CHECK_NULLPTR_THROW(destContext.device());

        hr = destContext.device1()->OpenSharedResource1(_frameHandle, __uuidof(ID3D11Texture2D), (void**)&_sTexture);
        CHECK_WIN32_HRESULT_THROW(hr, "Failed to open shared resource (NT).");
    } else {
        hr = srcTexture->QueryInterface(__uuidof(IDXGIResource), (void**)&_frameResource);
        CHECK_WIN32_HRESULT_THROW(hr, "Failed to get frame DXGI resource.");

        hr = _frameResource->GetSharedHandle(&_frameHandle);
        CHECK_WIN32_HRESULT_THROW(hr, "Failed to get frame shared handle.");

        hr = destContext.device()->OpenSharedResource(_frameHandle, __uuidof(ID3D11Texture2D), (void**)&_sTexture);
        CHECK_WIN32_HRESULT_THROW(hr, "Failed to open shared resource.");
    }
}

D3d11SharedTextureHandle::~D3d11SharedTextureHandle() {
    if (_isNtHandle) {
        CloseHandle(_frameHandle);
        _frameHandle = nullptr;
    }   
}

}

#endif
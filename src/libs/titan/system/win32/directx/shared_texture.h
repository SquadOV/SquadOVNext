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

#include <dxgi1_2.h>
#include <memory>
#include <wil/com.h>

#include "titan/dll.h"
#include "titan/system/win32/directx/device.h"

namespace titan::system::win32 {

class TITANEXPORT D3d11SharedTextureHandle {
public:
    D3d11SharedTextureHandle(D3d11SharedDevice& destContext, ID3D11Texture2D* srcTexture, bool canWrite);
    ~D3d11SharedTextureHandle();

    ID3D11Texture2D* texture() const { return _sTexture.get(); };

private:
    bool _isNtHandle = false;
    HANDLE _frameHandle = nullptr;
    wil::com_ptr<IDXGIResource1> _frameResource1;
    wil::com_ptr<IDXGIResource> _frameResource;
    wil::com_ptr<ID3D11Texture2D> _sTexture;
};

}

#endif
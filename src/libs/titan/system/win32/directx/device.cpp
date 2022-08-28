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

#include "titan/system/win32/directx/device.h"
#include "titan/system/win32/exceptions.h"

namespace titan::system::win32 {

D3d11SharedDevice::D3d11SharedDevice(const wil::com_ptr<ID3D11Device>& device, const wil::com_ptr<ID3D11DeviceContext>& context):
    _device(device)
{
    HRESULT hr = _device->QueryInterface(__uuidof(ID3D11Device1), (void**)&_device1);
    CHECK_WIN32_HRESULT_THROW(hr);
}

}

#endif
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

#include <Windows.h>
#include <d3d11.h>
#include <d3d11_1.h>
#include <wil/com.h>
#include <memory>

#include "titan/dll.h"
#include "titan/system/win32/directx/context.h"

namespace titan::system::win32 {

// A wrapper around a ID3D11Device. It contains a shared D3d11SharedContext
// that we can use to synchronize tasks that require the same context (e.g. two tasks
// that require the same texture and we don't want to go through the trouble of doing
// shared textures).
class TITANEXPORT D3d11SharedDevice {
public:
    D3d11SharedDevice(const wil::com_ptr<ID3D11Device>& device, const wil::com_ptr<ID3D11DeviceContext>& context);

    // Returns the immediate context that was created along with the device.
    // The D3d11SharedContext contains the mechanism to make it thread-safe.
    const D3d11SharedContextPtr& immediate() const { return _immediate; }
private:
    wil::com_ptr<ID3D11Device> _device;
    wil::com_ptr<ID3D11Device1> _device1;

    D3d11SharedContextPtr _immediate;
};

using D3d11SharedDevicePtr = std::shared_ptr<D3d11SharedDevice>;

}

#endif
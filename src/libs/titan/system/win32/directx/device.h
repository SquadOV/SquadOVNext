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
#include "titan/utility/exception.h"

namespace titan::system::win32 {

enum class D3d11DeviceLocation {
    CPU,
    GPU
};

using D3d11SharedDevicePtr = std::shared_ptr<class D3d11SharedDevice>;

// A wrapper around a ID3D11Device. It contains a shared D3d11SharedContext
// that we can use to synchronize tasks that require the same context (e.g. two tasks
// that require the same texture and we don't want to go through the trouble of doing
// shared textures).
class TITANEXPORT D3d11SharedDevice {
public:
    D3d11SharedDevice(D3d11DeviceLocation location, const wil::com_ptr<ID3D11Device>& device, const wil::com_ptr<ID3D11DeviceContext>& context);

    // Returns the immediate context that was created along with the device.
    // The D3d11SharedContext contains the mechanism to make it thread-safe.
    const D3d11SharedContextPtr& immediate() const { return _immediate; }

    ID3D11Device* device() const { return _device.get(); }
    ID3D11Device1* device1() const { return _device1.get(); }

    // adapter will be non-NULL only if we loaded the device from a given monitor.
    IDXGIAdapter1* adapter() const { return _adapter.get(); }

    // output will be non-NULL only if we loaded the device from a given monitor.
    IDXGIOutput* output() const { return _output.get(); }

    // 
    D3d11DeviceLocation location() const { return _location; }

    friend TITANEXPORT D3d11SharedDevicePtr loadD3d11DeviceOnMonitor(HMONITOR monitor);

protected:
    void setAdapter(const wil::com_ptr<IDXGIAdapter1>& adapter) { _adapter = adapter; }
    void setOutput(const wil::com_ptr<IDXGIOutput>& output) { _output = output; }

private:
    wil::com_ptr<ID3D11Device> _device;
    wil::com_ptr<ID3D11Device1> _device1;
    wil::com_ptr<IDXGIAdapter1> _adapter;
    wil::com_ptr<IDXGIOutput> _output;

    D3d11SharedContextPtr _immediate;
    D3d11DeviceLocation _location;
};

using D3d11SharedDevicePtr = std::shared_ptr<D3d11SharedDevice>;

TITANEXPORT D3d11SharedDevicePtr loadD3d11DeviceOnMonitor(HMONITOR monitor);
TITANEXPORT D3d11SharedDevicePtr loadD3d11DeviceOnLocation(D3d11DeviceLocation loc);

}

#endif
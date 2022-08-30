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
#include "titan/utility/log.h"
#include "titan/utility/strings.h"
#include <vector>

namespace titan::system::win32 {
namespace {

constexpr UINT kDefaultDeviceFlags = 
#ifndef NDEBUG
    D3D11_CREATE_DEVICE_DEBUG | D3D11_CREATE_DEVICE_BGRA_SUPPORT
#else
    D3D11_CREATE_DEVICE_BGRA_SUPPORT
#endif
;

const std::vector<D3D_FEATURE_LEVEL> kDefaultDeviceFeatures{
    D3D_FEATURE_LEVEL_11_1,
    D3D_FEATURE_LEVEL_11_0,
    D3D_FEATURE_LEVEL_10_1,
    D3D_FEATURE_LEVEL_10_0,
    D3D_FEATURE_LEVEL_9_3,
    D3D_FEATURE_LEVEL_9_2,
    D3D_FEATURE_LEVEL_9_1
};

}

D3d11SharedDevice::D3d11SharedDevice(D3d11DeviceLocation location, const wil::com_ptr<ID3D11Device>& device, const wil::com_ptr<ID3D11DeviceContext>& context):
    _device(device),
    _location(location)
{
    _immediate = std::make_shared<D3d11SharedContext>(context);
    HRESULT hr = _device->QueryInterface(__uuidof(ID3D11Device1), (void**)&_device1);
    CHECK_WIN32_HRESULT_THROW(hr, "Failed to get ID3D11Device1 interface.");
}

D3d11SharedDevicePtr loadD3d11DeviceOnMonitor(HMONITOR monitor) {
    wil::com_ptr<IDXGIFactory1> factory;

    HRESULT hr = CreateDXGIFactory1(__uuidof(IDXGIFactory1), (void**)(&factory));
    CHECK_WIN32_HRESULT_THROW(hr, "Failed to create DXGI factory.");

    UINT lastAdapterIndex = 0;
    wil::com_ptr<IDXGIAdapter1> adapter;
    wil::com_ptr<IDXGIOutput> output;
    while (factory->EnumAdapters1(lastAdapterIndex++, &adapter) != DXGI_ERROR_NOT_FOUND) {
        DXGI_ADAPTER_DESC1 desc;
        adapter->GetDesc1(&desc);

        std::wstring strDesc(desc.Description);
        TITAN_DEBUG(
            "Found DXGI Adapter: {}\nDedicated Video Memory: {}\ntDedicated System Memory: {}\ntShared System Memory: {}",
            titan::utility::wcsToUtf8(strDesc),
            desc.DedicatedVideoMemory,
            desc.DedicatedSystemMemory,
            desc.SharedSystemMemory
        );
        DXGI_OUTPUT_DESC outputDesc;
        UINT outputIndex = 0;
        while (adapter->EnumOutputs(outputIndex++, &output) != DXGI_ERROR_NOT_FOUND) {
            hr = output->GetDesc(&outputDesc);
            if (hr != S_OK) {
                continue;
            }

            if (outputDesc.Monitor == monitor) {
                break;
            }
        }

        // Found an adapter that has the desired monitor output.
        if (output) {
            break;
        }
    }
    CHECK_NULLPTR_THROW(adapter);

    wil::com_ptr<ID3D11Device> device;
    wil::com_ptr<ID3D11DeviceContext> context;
    hr = D3D11CreateDevice(
        adapter.get(),
        D3D_DRIVER_TYPE_UNKNOWN,
        nullptr,
        kDefaultDeviceFlags,
        kDefaultDeviceFeatures.data(),
        kDefaultDeviceFeatures.size(),
        D3D11_SDK_VERSION,
        &device,
        nullptr,
        &context
    );
    CHECK_WIN32_HRESULT_THROW(hr, "Failed to create D3D11 device from adapater.");
    auto shared = std::make_shared<D3d11SharedDevice>(D3d11DeviceLocation::GPU, device, context);
    shared->setAdapter(adapter);
    shared->setOutput(output);
    return shared;
}

D3d11SharedDevicePtr loadD3d11DeviceOnLocation(D3d11DeviceLocation loc) {
    wil::com_ptr<ID3D11Device> device;
    wil::com_ptr<ID3D11DeviceContext> context;
    HRESULT hr = D3D11CreateDevice(
        nullptr,
        (loc == D3d11DeviceLocation::GPU) ? D3D_DRIVER_TYPE_HARDWARE : D3D_DRIVER_TYPE_WARP,
        nullptr,
        kDefaultDeviceFlags,
        kDefaultDeviceFeatures.data(),
        kDefaultDeviceFeatures.size(),
        D3D11_SDK_VERSION,
        &device,
        nullptr,
        &context
    );
    CHECK_WIN32_HRESULT_THROW(hr, "Failed to create D3D11 device from device location.");
    return std::make_shared<D3d11SharedDevice>(loc, device, context);
}

}

#endif
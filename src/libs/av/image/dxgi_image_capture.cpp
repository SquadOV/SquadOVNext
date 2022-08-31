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

#include "av/image/dxgi_image_capture.h"
#include <titan/utility/exception.h>
#include <titan/system/win32/exceptions.h>

namespace av {

DxgiImageCapture::DxgiImageCapture(const titan::system::Process& process):
    _process(process)
{
    if (!_process.hasActiveWindow()) {
        throw DxgiNoWindowFound{};
    }

    // We then go from the window to the monitor to get the appropriate adapter/device combo.
    _processMonitor = MonitorFromWindow(_process.activeWindow(), MONITOR_DEFAULTTONULL);
    if (!_processMonitor) {
        throw DxgiNoMonitorFound{};
    }

    _logger = titan::utility::Logger::get()->createScopedLogger("dxgi");
    refreshDevice();
}

void DxgiImageCapture::refreshDevice() {
    // Load the D3D11 device that we'll use for desktop duplication.
    _device = titan::system::win32::loadD3d11DeviceOnMonitor(_processMonitor);
    CHECK_NULLPTR_THROW(_device->adapter());
    CHECK_NULLPTR_THROW(_device->output());

    DXGI_OUTPUT_DESC outputDesc;
    _device->output()->GetDesc(&outputDesc);

    // Sure we could probably support the rotation somehow. But it seems like wasted work...
    // I don't think anyone is playing video games on a rotated screen.
    if (outputDesc.Rotation != DXGI_MODE_ROTATION_IDENTITY) {
        throw DxgiUnsupportedOperation{};
    }

    HRESULT hr =  _device->output()->QueryInterface(__uuidof(IDXGIOutput1), (void**)&_dxgiOutput1);
    CHECK_WIN32_HRESULT_THROW(hr, "Failed to get IDXGIOutput1");

    hr = _device->output()->QueryInterface(__uuidof(IDXGIOutput5), (void**)&_dxgiOutput5);
    CHECK_WIN32_HRESULT_IF(hr) {
        _dxgiOutput5.reset();
    }

    _width = static_cast<size_t>(outputDesc.DesktopCoordinates.right - outputDesc.DesktopCoordinates.left);
    _height = static_cast<size_t>(outputDesc.DesktopCoordinates.bottom - outputDesc.DesktopCoordinates.top);
    
    reacquireDuplicationInterface();
}

void DxgiImageCapture::reacquireDuplicationInterface() {
    _dupl.reset();

    std::vector<DXGI_FORMAT> formats = {
        // This is the default on non-HDR screens
        DXGI_FORMAT_B8G8R8A8_UNORM,
        // This seems to be what gets used on HDR screens (at least on Windows 10?)
        DXGI_FORMAT_R16G16B16A16_FLOAT
    };

    HRESULT hr = _dxgiOutput5 ?
        _dxgiOutput5->DuplicateOutput1(_device->device(), 0, formats.size(), formats.data(), &_dupl) :
        _dxgiOutput1->DuplicateOutput(_device->device(), &_dupl);
    CHECK_WIN32_HRESULT_THROW(hr, "Failed to create desktop duplication object.");
}

NativeImagePtr DxgiImageCapture::getCurrent() {
    // Note that the check here is slightly different from the check in the constructor.
    // If the monitor was null in the constructor, we'll assume that something went wrong
    // we should abort. If a monitor is null here, we'll just carry on using the previous
    // monitor that we detected in the constructor.
    HMONITOR monitor = MonitorFromWindow(_process.activeWindow(), MONITOR_DEFAULTTONULL);
    if (monitor && monitor != _processMonitor) {
        const HMONITOR oldMonitor = _processMonitor;

        try {
            _processMonitor = monitor;
            refreshDevice();
        } catch (const titan::utility::Exception& ex) {
            _processMonitor = oldMonitor;

            // Need to reset all of this since we're not quite sure where in the process we messed up.
            _dupl.reset();
            _dxgiOutput1.reset();
            _dxgiOutput5.reset();
            _device.reset();
            return nullptr;
        }
    }
    CHECK_NULLPTR_IF_RETURN(_dupl, nullptr);

    HRESULT hr = !!_dupl ? _dupl->ReleaseFrame() : DXGI_ERROR_ACCESS_LOST;
    if (hr == DXGI_ERROR_ACCESS_LOST) {
        // Don't need to error out here since we only start to use _dupl after the
        // call to ReleaseFrame.
        try {
            reacquireDuplicationInterface();
        } catch (const titan::system::win32::Win32HResultException& ex) {
            return nullptr;
        }
    } else if (hr == DXGI_ERROR_INVALID_CALL) {
        // Empty case here - we don't particularly care if we already released the frame.
        // It's all the same to us.
    } else {
        CHECK_WIN32_HRESULT_RETURN(hr, nullptr);
    }

    wil::com_ptr<IDXGIResource> desktopResource = nullptr;
    DXGI_OUTDUPL_FRAME_INFO frameInfo;
    hr = !!_dupl ? _dupl->AcquireNextFrame(100, &frameInfo, &desktopResource) : DXGI_ERROR_WAIT_TIMEOUT;
    if (hr != DXGI_ERROR_WAIT_TIMEOUT && hr != S_OK) {
        if (hr == DXGI_ERROR_ACCESS_LOST) {
            try {
                reacquireDuplicationInterface();
            } catch (const titan::system::win32::Win32HResultException& ex) {
                TITAN_LOGGER_WARN(_logger, "Failed to reacquire duplication interface after access lost: {}", ex.what());
            }
        }

        return nullptr;
    }

    if (frameInfo.AccumulatedFrames == 0) {
        return nullptr;
    }

    wil::com_ptr<ID3D11Texture2D> tex;
    hr = desktopResource->QueryInterface(__uuidof(ID3D11Texture2D), (void**)&tex);
    CHECK_WIN32_HRESULT_RETURN(hr, nullptr);
    return std::make_shared<NativeImage>(tex, _device);
}

}
#endif // _WIN32
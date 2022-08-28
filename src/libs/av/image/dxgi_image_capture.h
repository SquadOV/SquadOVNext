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
#include <dxgi1_2.h>
#include <dxgi1_5.h>

#include "av/image/image_capture.h"

#include <titan/system/process.h>
#include <titan/system/win32/directx/device.h>
#include <titan/utility/exception.h>

namespace av {

// Uses the DXGI desktop duplication API (https://docs.microsoft.com/en-us/windows/win32/direct3ddxgi/desktop-dup-api)
// to capture screenshots of the desktop which components in the pipeline can use. Note that this class will return the
// (effectively) raw texture which might have rotations, not be cropped properly, etc. It is up to the caller to handle
// those situations and incorporate the texture into their pipeline.
class DxgiImageCapture: public ImageCapture {
public:
    explicit DxgiImageCapture(const titan::system::Process& process);

    std::optional<NativeImage> getCurrent() override;

private:
    titan::system::Process _process;

    // There is a possibility that the HMONITOR changes (e.g. we're recording a
    // window and the user moves it onto another monitor).
    HMONITOR _processMonitor;

    // Function that gets called when we know we need to refresh the D3D11 device.
    // This happens when the monitor (i.e. the adapater) we're recording from changes.
    void refreshDevice();

    // Sometimes, things will error out and we need to re-acquire the IDXGIOutputDuplication
    // interface so that we can continue. This must also happen whenever the device changes.
    void reacquireDuplicationInterface();

    // Note that the DxgiImageCapture object should handle its own D3D11 device.
    // We do not want to share the device/context with anyone else since it might cause
    // deadlocks. I'm not 100% sure why but empiracally I've seen the deadlock happen
    // when sharing the device/context between the DXGI desktop duplication API and FFmpeg.
    titan::system::win32::D3d11SharedDevicePtr _device;

    // Cache the width and height of the screen that we're trying to capture.
    size_t _width = 0;
    size_t _height = 0;

    // Variables we need for desktop duplication.
    // _dxgiOutput1 vs _dxgiOutput5 will determine HDR support (5 has it, 1 does not).
    wil::com_ptr<IDXGIOutput1> _dxgiOutput1;
    wil::com_ptr<IDXGIOutput5> _dxgiOutput5;
    wil::com_ptr<IDXGIOutputDuplication> _dupl;
};

CREATE_SIMPLE_EXCEPTION_CLASS(DxgiNoWindowFound, "DxgiImageCapture failed to find the process window.");
CREATE_SIMPLE_EXCEPTION_CLASS(DxgiNoMonitorFound, "DxgiImageCapture failed to find the window monitor.");

}

#endif // _WIN32
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

#include <Windows.h>
#include <vector>

#include "titan/system/window_di.h"
#include "titan/system/win32/exceptions.h"
#include "titan/utility/log.h"
#include "titan/math/bounding_box.h"

namespace titan::system {
namespace {

struct EnumWindowData {
    DWORD pid;
    HWND out = nullptr;
    bool found = false;
    const std::vector<WindowFilterFn>& filters;
};

BOOL enumWindowCallback(HWND hwnd, LPARAM param) {
    EnumWindowData* data = (EnumWindowData*)param;
    DWORD refPid;
    GetWindowThreadProcessId(hwnd, &refPid);

    if (refPid == data->pid) {
        for (const auto& fn: data->filters) {
            if (!fn(hwnd)) {
                return TRUE;
            }
        }

        data->found = true;
        data->out = hwnd;
        return FALSE;
    }

    return TRUE;
}

}

std::optional<NativeWindowHandle> NativeWindowDI::findWindowForProcess(NativeProcessId pid, const std::vector<WindowFilterFn>& filters) const {
    EnumWindowData window {
        pid,
        nullptr,
        false,
        filters
    };
    
    if (!EnumWindows(enumWindowCallback, (LPARAM)&window) && !window.found) {
        TITAN_WARN("Failed to enumerate windows: {}", titan::system::win32::getLastWin32ErrorAsString());
        return std::nullopt;
    }

    if (!window.found) {
        return std::nullopt;
    }

    return window.out;
}

bool NativeWindowDI::isWindowActive(NativeWindowHandle win) {
    return (IsWindowEnabled(win) && IsWindowVisible(win));
}

Eigen::AlignedBox2i NativeWindowDI::getWindowScreenBoundingBox(NativeWindowHandle win) {
    RECT rect;
    if (!GetWindowRect(win, &rect)) {
        return {};
    }
    return titan::math::winRectToAlignedBox(rect);
}

Eigen::AlignedBox2i NativeWindowDI::getWindowClientBoundingBox(NativeWindowHandle win) {
    RECT rect;
    if (!GetClientRect(win, &rect)) {
        return {};
    }
    
    return titan::math::winRectToAlignedBox(rect);
}

}
#endif // _WIN32
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

#include "titan/dll.h"
#include "titan/system/types.h"
#include "titan/system/window_di_forward.h"

#include <Eigen/Geometry>
#include <functional>
#include <optional>
#include <vector>

namespace titan::system {

using WindowFilterFn = std::function<bool(NativeWindowHandle)>;

class TITANEXPORT NativeWindowDI: public std::enable_shared_from_this<NativeWindowDI> {
public:
    virtual ~NativeWindowDI() {}

    // Find the window handles associated with the given process that pass the given filters.
    std::optional<NativeWindowHandle> findWindowForProcess(NativeProcessId pid, const std::vector<WindowFilterFn>& filters) const;

    // Determine is the window is active and not some hidden system window.
    bool isWindowActive(NativeWindowHandle win);

    // Get the bounding box of the window in screen coordinates.
    Eigen::AlignedBox2i getWindowScreenBoundingBox(NativeWindowHandle win);

    // Get the bounding box of the client area of the window (window minus the border, etc.) in client coordinates 
    Eigen::AlignedBox2i getWindowClientBoundingBox(NativeWindowHandle win);
private:
};

}
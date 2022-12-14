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

#include <filesystem>
#include <memory>
#include <stdint.h>
#include <string>
#include <vector>

#include "titan/dll.h"
#include "titan/system/types.h"
#include "titan/system/process_di.h"
#include "titan/system/window_di.h"

namespace titan::system {

// Whether to load the process information immediately or later (manually).
enum class ProcessLoadType
{
    Immediate,
    Defer
};

// A wrapper around the OS's native process ID/handle.
//
// In addition to keep track of the process ID/handle, this object
// will also cache certain other properties about the process
// (e.g. the executable name). Furthermore, this class also provides
// useful utility functions for checking the state of the process
// (e.g. if it's full-screen or not).
class TITANEXPORT Process {
public:
    Process(
        NativeProcessId id,
        ProcessLoadType loadType,
        const NativeProcessDIPtr& di = getDefaultNativeProcessDI()
    );

    void loadInfo();

    NativeProcessId id() const { return _id; }
    const std::string& name() const { return _name; }
    const std::filesystem::path& path() const { return _fullPath; }
    int64_t startTime() const { return _startTime; }

    void initializeActiveWindow(size_t minSize = 0, const NativeWindowDIPtr& windowDi = getDefaultNativeWindowDI());
    bool hasActiveWindow() const { return _activeWindow; }
    NativeWindowHandle activeWindow() const { return _activeWindow; }

private:
    NativeProcessId _id;
    NativeProcessDIPtr _di;

    //
    // Cached properties of the process.
    //

    // A friendly name that could potentially be displayed to the user
    // for them to reference.
    std::string _name;

    // The full system path to the process executable.
    std::filesystem::path _fullPath;

    // Time at which the process started running. This isn't guaranteed to be UNIX time.
    // This is only relevant when comparing the start times of two different processes.
    int64_t _startTime = 0;

    //
    // Cached data about what we consider to be the "active" window.
    //
    NativeWindowHandle _activeWindow = nullptr;
};

// Returns all the running processes at the time of this function call.
// The process that *most recently* started (the newest process) will be first.
TITANEXPORT std::vector<Process> loadRunningProcesses(ProcessLoadType loadType = ProcessLoadType::Immediate, const NativeProcessDIPtr& di = getDefaultNativeProcessDI());

}
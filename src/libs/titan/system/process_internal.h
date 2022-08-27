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

#include "titan/system/process.h"
#include <stdint.h>
#include <vector>

namespace titan::system::internal {

// An OS-specific function that will return all running process IDs.
std::vector<NativeProcessId> enumProcesses();

// An OS-specific function to get the full process path from a process handle.
NativeString getProcessPath(NativeProcessHandle handle);

// An OS-specific function to get a friendly name for the process given its executable path.
std::string getProcessFriendlyName(const NativeString& fullPath);

// An OS-specific function to get some integer representation of when the process started.
int64_t getProcessStartTime(NativeProcessHandle handle);

// A safe RAII wrappper for the OS's NativeProcessHandle.
// Using this is safer since it's guaranteed to close the handle when going out of scope.
class NativeProcessHandleWrapper {
public:
    explicit NativeProcessHandleWrapper(NativeProcessHandle handle):
        _handle(handle)
    {}
    ~NativeProcessHandleWrapper();

    NativeProcessHandle handle() const { return _handle; }
private:
    NativeProcessHandle _handle;
};

}
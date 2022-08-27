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
#include <memory>
#include <stdint.h>
#include <vector>

namespace titan::system {

class TITANEXPORT NativeProcessDI {
public:
    virtual ~NativeProcessDI() {}
    
    // An OS-specific function that will return all running process IDs.
    virtual std::vector<NativeProcessId> enumProcesses();

    // An OS-specific function to get the full process path from a process handle.
    virtual NativeString getProcessPath(NativeProcessHandle handle);

    // An OS-specific function to get a friendly name for the process given its executable path.
    virtual std::string getProcessFriendlyName(const NativeString& fullPath);

    // An OS-specific function to get some integer representation of when the process started.
    virtual int64_t getProcessStartTime(NativeProcessHandle handle);
private:
};

using NativeProcessDIPtr = std::shared_ptr<NativeProcessDI>;
TITANEXPORT NativeProcessDIPtr getDefaultNativeProcessDI();

}
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
#include "titan/system/process.h"
#include "titan/system/process_internal.h"

#ifdef _WIN32
#include <processthreadsapi.h>
#endif

namespace titan::system {

Process::Process(NativeProcessId id):
    _id(id)
{
    // Open up a process handle to use for the duration of the constructor.
    internal::NativeProcessHandleWrapper handle(
#ifdef _WIN32
        OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, FALSE, _id)
#endif
    );

    _fullPath = internal::getProcessPath(handle.handle());
    _name = internal::getProcessFriendlyName(_fullPath.native());
    _startTime = internal::getProcessStartTime(handle.handle());
}

std::vector<Process> loadRunningProcesses() {
    std::vector<NativeProcessId> handles = internal::enumProcesses();

    std::vector<Process> processes;
    processes.reserve(handles.size());

    for (const auto& hnd : handles) {
        processes.emplace_back(Process{hnd});
    }

    return processes;
}

}
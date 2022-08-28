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
#include <processthreadsapi.h>
#endif

#include <map>

#include "titan/system/process.h"
#include "titan/system/process_handle.h"

namespace titan::system {

Process::Process(NativeProcessId id, const NativeProcessDIPtr& di):
    _id(id),
    _di(di)
{
    // Open up a process handle to use for the duration of the constructor.
    auto handle = di->openProcessHandleLimited(_id);
    _fullPath = _di->getProcessPath(handle.handle());
    _name = _di->getProcessFriendlyName(_fullPath.native());
    _startTime = _di->getProcessStartTime(handle.handle());
}

void Process::initializeActiveWindow() {

}

std::vector<Process> loadRunningProcesses(const NativeProcessDIPtr& di) {
    std::vector<NativeProcessId> handles = di->enumProcesses();

    std::vector<Process> processes;
    processes.reserve(handles.size());
    for (const auto& hnd : handles) {
        processes.emplace_back(Process{hnd, di});
    }

    std::sort(processes.begin(), processes.end(), [](const Process& a, const Process& b){
        return a.startTime() > b.startTime();
    });
    
    return processes;
}

}
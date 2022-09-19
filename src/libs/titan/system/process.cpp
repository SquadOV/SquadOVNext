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
#include <ranges>

#include "titan/system/process.h"
#include "titan/system/process_handle.h"

namespace titan::system {

Process::Process(NativeProcessId id, ProcessLoadType loadType, const NativeProcessDIPtr& di):
    _id(id),
    _di(di)
{
    if (loadType == ProcessLoadType::Immediate) {
        loadInfo();
    }
}

void Process::loadInfo() {
    // Open up a process handle to use for the duration of the constructor.
    auto handle = _di->openProcessHandleLimited(_id);
    _fullPath = _di->getProcessPath(handle.handle());
    _name = _di->getProcessFriendlyName(_fullPath.native());
    _startTime = _di->getProcessStartTime(handle.handle());
}

void Process::initializeActiveWindow(size_t minSize, const NativeWindowDIPtr& windowDi) {
    const std::optional<NativeWindowHandle> window = windowDi->findWindowForProcess(_id, {
        [&windowDi](NativeWindowHandle win){
            return windowDi->isWindowActive(win);
        },
        [minSize, &windowDi](NativeWindowHandle win){
            if (!minSize) {
                return true;
            }

            const auto region = windowDi->getWindowClientBoundingBox(win);
            if (region.isEmpty() && minSize) {
                return false;
            }

            for (const auto dim : region.sizes()) {
                if (dim < minSize) {
                    return false;
                }
            }
            
            return true;
        }
    });

    if (!window) {
        return;
    }
    
    _activeWindow = window.value();
}

std::vector<Process> loadRunningProcesses(ProcessLoadType loadType, const NativeProcessDIPtr& di) {
    std::vector<NativeProcessId> handles = di->enumProcesses();

    std::vector<Process> processes;
    processes.reserve(handles.size());
    for (const auto& hnd : handles) {
        processes.emplace_back(Process{hnd, loadType, di});
    }

    std::sort(processes.begin(), processes.end(), [](const Process& a, const Process& b){
        return a.startTime() > b.startTime();
    });
    
    return processes;
}

}
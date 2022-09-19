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

#include <functional>
#include <memory>
#include <mutex>
#include <string>
#include <thread>
#include <unordered_set>

#include <titan/system/process.h>
#include <titan/system/process_di.h>
#include <titan/system/window_di.h>

namespace engine {

enum class ProcessChangeStatus
{
    Start,
    Stop
};
using ProcessChangeFn = std::function<void(const titan::system::Process&, ProcessChangeStatus)>;

class ProcessWatcher {
public:
    ProcessWatcher(
        const ProcessChangeFn& callback,
        const titan::system::NativeProcessDIPtr& pdi = titan::system::getDefaultNativeProcessDI(),
        const titan::system::NativeWindowDIPtr& wdi = titan::system::getDefaultNativeWindowDI()
    );
    ~ProcessWatcher();

    void add(const std::string& process);
    void remove(const std::string& process);

private:
    ProcessChangeFn _callback;
    titan::system::NativeProcessDIPtr _pdi;
    titan::system::NativeWindowDIPtr _wdi;

    std::mutex _mutex;
    std::unordered_set<std::string> _processes;

    void tick();
    bool _running = true;
    std::thread _runner;
};

using ProcessWatcherPtr = std::shared_ptr<ProcessWatcher>;

}
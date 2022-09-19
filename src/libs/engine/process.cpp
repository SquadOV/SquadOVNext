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
#include "engine/process.h"

#include <chrono>
#include <ranges>
#include <unordered_map>

#include <titan/utility/log.h>
#include <titan/utility/strings.h>

namespace engine {

ProcessWatcher::ProcessWatcher(const ProcessChangeFn& callback, const titan::system::NativeProcessDIPtr& pdi, const titan::system::NativeWindowDIPtr& wdi):
    _callback(callback),
    _pdi(pdi),
    _wdi(wdi)
{
    _runner = std::thread(std::bind(&ProcessWatcher::tick, this));
}

ProcessWatcher::~ProcessWatcher() {
    _running = false;
    if (_runner.joinable()) {
        _runner.join();
    }
}

void ProcessWatcher::add(const std::string& process) {
    std::scoped_lock guard(_mutex);
    TITAN_INFO("ADDING process to watch {}", process);
    _processes.insert(process);
}

void ProcessWatcher::remove(const std::string& process) {
    std::scoped_lock guard(_mutex);
    TITAN_INFO("REMOVE process to watch {}", process);
    _processes.erase(process);
}

void ProcessWatcher::tick() {
    std::unordered_map<NativeProcessId, titan::system::Process> lastSet;
    while (_running) {
        {
            std::scoped_lock guard(_mutex);
            // Get a list of all the processes that are currently running. Note that we only care
            // about watching processes with an active window.
            auto processes = titan::system::loadRunningProcesses(titan::system::ProcessLoadType::Defer, _pdi);
            auto view = processes |
                std::views::filter([this](titan::system::Process& p) {
                    p.loadInfo();
                    return _processes.find(titan::utility::wcsToUtf8(p.path().filename().native())) != _processes.end();
                }) |
                std::views::filter([this](titan::system::Process& p) {
                    p.initializeActiveWindow(32, _wdi);
                    return p.hasActiveWindow();
                });

            std::unordered_map<NativeProcessId, titan::system::Process> currentSet;

            // Note that there's a couple events that we want to detect:
            //  1) A process started running (if a process is in currentSet but not in lastSet).
            //  2) A process stopped running (if a process is in lastSet but not in currentSet).
            // We will throw an event whenever this happens EVEN IF multiple processes with the same name (e.g. Discord.exe) are running
            // at the same time. It is up to the handler to determine what to do with that information.
            for (const auto& p: view) {
                currentSet.insert(std::make_pair(p.id(), p));

                if (lastSet.find(p.id()) != lastSet.end()) {
                    continue;
                }

                _callback(p, ProcessChangeStatus::Start);
            }

            for (const auto& kvp: lastSet) {
                if (currentSet.find(kvp.first) != currentSet.end()) {
                    continue;
                }

                _callback(kvp.second, ProcessChangeStatus::Stop);
            }

            lastSet = currentSet;
        }

        // 500ms of sleep time should be a good compromise of CPU usage and being able to detect when things start in a timely fashion.
        std::this_thread::sleep_for(std::chrono::milliseconds(500));
    }
}

}
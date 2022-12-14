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

#include <memory>
#include <string>
#include "engine/dll.h"
#include "engine/process.h"

#include <titan/system/process.h>

namespace engine {

struct ENGINEEXPORT EngineOptions {
    std::string vodPath;
    std::string clipPath;
    std::string screenshotPath;
    std::string matchPath;
    std::string logPath;
};

class ENGINEEXPORT Engine {
public:
    explicit Engine(const EngineOptions& options);

    // Process watcher interface.
    void addProcessToWatch(const std::string& process);
    void removeProcessToWatch(const std::string& process);
private:
    EngineOptions _options;

    void onProcessChange(const titan::system::Process& p, ProcessChangeStatus change);
    ProcessWatcherPtr _processWatcher;
};

using EnginePtr = std::shared_ptr<Engine>;

}
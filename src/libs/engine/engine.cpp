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
#include "engine/engine.h"
#include <titan/utility/ntp_client.h>
#include <titan/utility/log.h>


#include <filesystem>

namespace fs = std::filesystem;
namespace engine {

Engine::Engine(const EngineOptions& options):
    _options(options)
{
    // Initialize logging.
    const auto logPath = fs::path(_options.logPath);
    if (!fs::exists(logPath)) {
        fs::create_directories(logPath);
    }
    titan::utility::Logger::get()->addFileSystemSink(logPath / fs::path("squadov.log"));

    TITAN_INFO("Initializing NTP client...");
    // Initialize NTP. TODO: Initialize the initial offset better based off some server's time.
    titan::utility::NTPClient::singleton()->initialize(0);
}

}
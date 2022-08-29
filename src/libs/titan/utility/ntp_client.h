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


#include <stdint.h>
#include <shared_mutex>
#include <thread>

#include "titan/dll.h"
#include "titan/utility/time_forward.h"

namespace titan::utility {

// A *simple* (not yet doing any of the fancy filtering algorithms)
// NTP client that can talk to a list of pre-programmed NTP servers
// to adjust the user's system time.
class TITANEXPORT NTPClient {
public:
    static NTPClient* singleton();
    ~NTPClient();

    // Must be called to start the running the NTP client. 
    // initialOffset is an initial estimation of how far the
    // user's time is off from true time.
    void initialize(int64_t initialOffset);
    void enable(bool enabled, bool doTick = false);

    TimePoint now() const;
    TimePoint adjustTime(const TimePoint& tm) const;
    TimePoint revertTime(const TimePoint& tm) const;
private:
    void tick();
    TimePoint clientNow() const;
    int64_t offsetToServer(const std::string& server) const;

    mutable std::shared_mutex _offsetMutex;
    int64_t _offsetMs = 0;

    bool _enabled = true;
    bool _running = false;
    std::thread _tickThread;
};

}
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
#include "titan/utility/time.h"
#include "titan/utility/ntp_client.h"

namespace titan::utility {

TimePoint now() {
    return NTPClient::singleton()->now();
}

int64_t timeToUnixMs(const TimePoint& tm) {
    return std::chrono::duration_cast<std::chrono::milliseconds>(tm.time_since_epoch()).count();
}

TimePoint unixMsToTime(int64_t tm) {
    return TimePoint(std::chrono::milliseconds(tm));
}

}
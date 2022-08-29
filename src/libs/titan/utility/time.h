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

#include <chrono>
#include <type_traits>

#include "titan/dll.h"
#include "titan/utility/time_forward.h"

namespace titan::utility {

TITANEXPORT TimePoint now();
TITANEXPORT int64_t timeToUnixMs(const TimePoint& tm);
TITANEXPORT TimePoint unixMsToTime(int64_t tm);

template<typename TSrc, typename TDst>
TDst convertClockTime(const TSrc& tm) {
    if constexpr (std::is_same_v<TSrc, TDst>) {
        return tm;
    } else {
        return TDst::clock::now() + std::chrono::duration_cast<typename TDst::duration>(tm - TSrc::clock::now());
    }
}

}
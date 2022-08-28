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
#include "titan/utility/exception.h"

#include <boost/stacktrace.hpp>
#include <sstream>

namespace titan::utility {

Exception::Exception(const std::string& what, const std::source_location& loc) noexcept {
    std::ostringstream str;
    str << "[" << loc.file_name() << "(" << loc.line() << ":" << loc.column() << ")" << loc.function_name() << "]"
        << what << std::endl
        << boost::stacktrace::stacktrace();
    _what = str.str();
}

}
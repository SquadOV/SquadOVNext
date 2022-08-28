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
#include "titan/system/win32/exceptions.h"
#include <comdef.h>
#include <sstream>

namespace titan::system::win32 {

std::string hresultToString(HRESULT hr) {
    _com_error err(hr);

    std::ostringstream str;
    str << std::hex << hr << " [" << err.ErrorMessage() << "]";
    return str.str();
}

}

#endif
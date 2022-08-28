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

#include <string>

#include "titan/dll.h"

namespace titan::utility {

// Convert a CHAR string to WCHAR string.
TITANEXPORT std::wstring utf8ToWcs(const std::string& str);

// Convert a WCHAR string to CHAR string.
TITANEXPORT std::string wcsToUtf8(const std::wstring& str);
    
}
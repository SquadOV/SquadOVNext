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
#ifdef _WIN32

#include <Windows.h>
#include <fmt/core.h>
#include <string>
#include "titan/dll.h"
#include "titan/utility/exception.h"

namespace titan::system::win32 {

std::string hresultToString(HRESULT hr);

class TITANEXPORT Win32HResultException: public titan::utility::Exception {
public:
    Win32HResultException(HRESULT hr, const std::string& context, const std::source_location& loc = std::source_location::current()):
        titan::utility::Exception(fmt::format("{} :: {}", context, hresultToString(hr)) , loc)
    {}
};

TITANEXPORT std::string getLastWin32ErrorAsString();

}

#define CHECK_WIN32_HRESULT_THROW(HR, CONTEXT) if (HR != S_OK) { throw titan::system::win32::Win32HResultException(HR, CONTEXT); }
#define CHECK_WIN32_HRESULT_IF(HR) if (HR != S_OK)
#define CHECK_WIN32_HRESULT_RETURN(HR, RET) if (HR != S_OK) { return RET; }

#endif // _WIN32
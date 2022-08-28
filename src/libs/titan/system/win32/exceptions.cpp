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
#include <codecvt>
#include <comdef.h>
#include <sstream>

namespace titan::system::win32 {

std::string hresultToString(HRESULT hr) {
    _com_error err(hr);

    std::ostringstream str;
    str << std::hex << hr << " [" << err.ErrorMessage() << "]";
    return str.str();
}

std::string getLastWin32ErrorAsString() {
    const auto err = GetLastError();
    if (err == ERROR_SUCCESS) {
        return "";
    }

    LPWSTR pBuffer = NULL;
    FormatMessageW(
        FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
        nullptr,
        err,
        MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
        (LPWSTR)&pBuffer, 
        0,
        nullptr
    );

    if (!pBuffer) {
        return "";
    }

    std::wstring errStr(pBuffer);
    LocalFree(pBuffer);
    return std::wstring_convert<std::codecvt_utf8<wchar_t>>{}.to_bytes(errStr);
}

}

#endif
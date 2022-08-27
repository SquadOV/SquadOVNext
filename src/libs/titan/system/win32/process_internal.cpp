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
#pragma once

#include <Windows.h>
#include <psapi.h>
#include <processthreadsapi.h>

#include "titan/system/process_internal.h"
#include "titan/system/win32/conversions.h"

namespace titan::system::internal {

std::vector<NativeProcessId> enumProcesses() {
    DWORD aProcesses[1024], cbNeeded, cProcesses;
    if ( !EnumProcesses(aProcesses, sizeof(aProcesses), &cbNeeded) ) {
        return {};
    }

    cProcesses = cbNeeded / sizeof(DWORD);

    std::vector<NativeProcessId> ret(cProcesses);
    for (auto i = 0; i < cProcesses; ++i) {
        ret[i] = aProcesses[i];
    }
    return ret;
}

NativeString getProcessPath(NativeProcessHandle handle) {
    if (!handle) {
        return L"";
    }

    WCHAR szProcessName[MAX_PATH] = L"<unknown>";
    DWORD processNameSize = sizeof(szProcessName)/sizeof(WCHAR);

    if (QueryFullProcessImageNameW(handle, 0, szProcessName, &processNameSize) == 0) {
        return L"";
    }
    return std::wstring(szProcessName);
}

std::string getProcessFriendlyName(const NativeString& fullPath) {
    DWORD unk1 = 0;
    const auto fileInfoSize = GetFileVersionInfoSizeExW(0, fullPath.c_str(), &unk1);

    std::vector<char> buffer(fileInfoSize);
    if (!GetFileVersionInfoExW(0, fullPath.c_str(), 0, fileInfoSize, (void*)buffer.data())) {
        return "";
    }

    char* infBuffer = nullptr;
    unsigned int infSize = 0;
    if (!VerQueryValueA((void*)buffer.data(), "\\StringFileInfo\\000004B0\\ProductName", (void**)&infBuffer, &infSize)) {
        return "";
    }

    return std::string(infBuffer, infSize);
}

int64_t getProcessStartTime(NativeProcessHandle handle) {
    FILETIME creationTime;
    if (!GetProcessTimes(handle, &creationTime, nullptr, nullptr, nullptr)) {
        return 0;
    }

    return fileTimeToI64(creationTime);
}

NativeProcessHandleWrapper::~NativeProcessHandleWrapper() {
    CloseHandle(_handle);
}

}
#endif // _WIN32
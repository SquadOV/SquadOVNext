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
#endif

#include "titan/system/process.h"

namespace titan::system::internal {

// A safe RAII wrappper for the OS's NativeProcessHandle.
// Using this is safer since it's guaranteed to close the handle when going out of scope.
class NativeProcessHandleWrapper {
public:
    template<typename... Args>
    static NativeProcessHandleWrapper create(Args&&... args) {
        return NativeProcessHandleWrapper(
#ifdef _WIN32
            OpenProcess(std::forward<Args>(args)...)
#endif
        );
    }
    ~NativeProcessHandleWrapper();

    NativeProcessHandle handle() const { return _handle; }

protected:
    explicit NativeProcessHandleWrapper(NativeProcessHandle handle):
        _handle(handle)
    {}

private:
    NativeProcessHandle _handle;
};

}
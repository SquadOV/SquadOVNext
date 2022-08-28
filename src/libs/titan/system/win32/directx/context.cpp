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

#include "titan/system/win32/directx/context.h"

namespace titan::system::win32 {

D3d11ContextGuard::D3d11ContextGuard(std::unique_lock<std::recursive_mutex>&& guard, const wil::com_ptr<ID3D11DeviceContext>& context):
    _guard(std::move(guard)),
    _context(context) {
}

D3d11ContextGuard::D3d11ContextGuard(D3d11ContextGuard&& o):
    _guard(std::move(o._guard)),
    _context(o._context) {
    o._context = nullptr;
}

D3d11ContextGuard::~D3d11ContextGuard() {
}

D3d11SharedContext::D3d11SharedContext(const wil::com_ptr<ID3D11DeviceContext>& context):
    _context(context)
{
}

D3d11ContextGuard D3d11SharedContext::get() {
    std::unique_lock lock(_mutex);
    return D3d11ContextGuard{std::move(lock), _context};
}

}

#endif
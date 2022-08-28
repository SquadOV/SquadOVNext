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
#include <d3d11.h>
#include <wil/com.h>
#include <memory>
#include <mutex>
#include <stdint.h>

#include "titan/dll.h"

namespace titan::system::win32 {

// An object that we pass back to the user of the D3d11SharedContext to give an
// RAII-based single-threaded control over access to the underlying ID3D11DeviceContext.
class TITANEXPORT D3d11ContextGuard {
public:
    friend class D3d11SharedContext;

    D3d11ContextGuard(D3d11ContextGuard&& o);
    D3d11ContextGuard(const D3d11ContextGuard& o) = delete;
    ~D3d11ContextGuard();
    ID3D11DeviceContext* context() const { return _context.get(); }

protected:
    D3d11ContextGuard(std::unique_lock<std::recursive_mutex>&& guard, const wil::com_ptr<ID3D11DeviceContext>& context);

private:
    std::unique_lock<std::recursive_mutex> _guard;
    wil::com_ptr<ID3D11DeviceContext> _context;
};

// A wrapper around a ID3D11DeviceContext and ensuring that
// we control access to the same underlying ID3D11DeviceContext in a thread-safe
// manner.
class TITANEXPORT D3d11SharedContext {
public:
    explicit D3d11SharedContext(const wil::com_ptr<ID3D11DeviceContext>& context);

    D3d11ContextGuard get();
private:
    wil::com_ptr<ID3D11DeviceContext> _context;
    std::recursive_mutex _mutex;
};

using D3d11SharedContextPtr = std::shared_ptr<D3d11SharedContext>;

}

#endif
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

#include "av/dll.h"

#ifdef _WIN32
#include <Windows.h>
#include <d3d11.h>
#include <wil/com.h>
#endif

namespace av {

// A simple wrapper around whatever the "native" image format is for this operating system.
// For example, on Windows, that would be ID3D11Texture2D. This is needed because otherwise
// a pointer to the underlying native image (e.g. a ID3D11Texture2D*) would be exposed to SWIG/C#.
// This is problematic since SWIG creates a wrapper that uses the copy constructor to return something like
//      new ID3D11Texture2D((const ID3D11Texture2D&)stuff)
// which isn't valid. Using this NativeImage wrapper gets us around that in most cases and we only have
// to worry about that weird definition when implementing/defining the accessor in this class.
class AVEXPORT NativeImage {
public:

private:

#ifdef _WIN32
    wil::com_ptr<ID3D11Texture2D> _native;
#endif

};

}
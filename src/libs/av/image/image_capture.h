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

#include <memory>
#include "av/dll.h"
#include "av/image/os_image.h"

namespace av {

class DLLEXPORT ImageCapture {
public:
    virtual ~ImageCapture() {}

    virtual NativeImage getCurrent() const = 0;
};

using ImageCapturePtr = std::shared_ptr<ImageCapture>;

// A generic function to create the appropriate ImageCapture class for the given circumstances.
// Namely, we generally only care about what native process we're trying to record (i.e. a game).
// This function will return the first available image capture object that successfully initializes.
DLLEXPORT ImageCapturePtr createImageCapture();

}
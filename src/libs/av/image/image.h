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
#include <vector>
#include <stdint.h>
#include <titan/utility/exception.h>

namespace av {

// An interface that we expect everything that represents an "Image" should satisfy.
class AVEXPORT IImage {
public:
    virtual ~IImage() {}

    virtual size_t width() const = 0;
    virtual size_t height() const = 0;
    virtual size_t bytesPerPixel() const = 0;
    virtual size_t channels() const = 0;
    size_t bytesPerElement() const { return bytesPerPixel() / channels(); }
    size_t bytesPerRow() const { return bytesPerPixel() * width(); }

    virtual void fillRawBuffer(std::vector<uint8_t>& buffer) const = 0;
};

CREATE_SIMPLE_EXCEPTION_CLASS(UnsupportedImageFormat, "Unsupported Image Format");

}
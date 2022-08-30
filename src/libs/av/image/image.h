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

enum class ImageFormat {
    B8R8G8A8_UNORM,
    R16G16B16A16_FLOAT
};

AVEXPORT size_t formatToBytesPerPixel(ImageFormat format);
AVEXPORT size_t formatToChannels(ImageFormat format);
AVEXPORT size_t areFormatChannelsFlipped(ImageFormat format);

// An interface that we expect everything that represents an "Image" should satisfy.
class AVEXPORT IImage {
public:
    virtual ~IImage() {}

    virtual size_t width() const = 0;
    virtual size_t height() const = 0;
    virtual ImageFormat format() const = 0;

    size_t bytesPerPixel() const { return formatToBytesPerPixel(format()); }
    size_t channels() const { return formatToBytesPerPixel(format()); }
    size_t bytesPerElement() const { return bytesPerPixel() / channels(); }
    virtual size_t bytesPerRow() const { return bytesPerPixel() * width(); }
    bool areChannelsFlipped() const { return areFormatChannelsFlipped(format()); }
};

CREATE_SIMPLE_EXCEPTION_CLASS(UnsupportedImageFormat, "Unsupported Image Format");

inline
bool areGenericImagesCompatible(const IImage& a, const IImage& b) {
    return a.width() == b.width() &&
        a.height() == b.height() &&
        a.channels() == b.channels() &&
        a.format() == b.format();
}

}
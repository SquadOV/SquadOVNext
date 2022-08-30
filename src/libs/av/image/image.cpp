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
#include "av/image/image.h"

namespace av {

size_t formatToBytesPerPixel(ImageFormat format) {
    switch (format) { 
        using enum ImageFormat;
        case B8R8G8A8_UNORM:
            return 4;
        case R16G16B16A16_FLOAT:
            return 8;
        default:
            throw UnsupportedImageFormat{};
    }
    return 0;
}

size_t formatToChannels(ImageFormat format) {
    switch (format) { 
        using enum ImageFormat;
        case B8R8G8A8_UNORM:
        case R16G16B16A16_FLOAT:
            return 4;
        default:
            throw UnsupportedImageFormat{};
    }
    return 0;
}

size_t areFormatChannelsFlipped(ImageFormat format) {
    switch (format) { 
        using enum ImageFormat;
        case B8R8G8A8_UNORM:
            return true;
        case R16G16B16A16_FLOAT:
            return false;
        default:
            throw UnsupportedImageFormat{};
    }
    return 0;
}

}
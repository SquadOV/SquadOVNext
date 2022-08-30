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
#include "av/image/cpu_image.h"

namespace av {

CpuImage::CpuImage(size_t width, size_t height, ImageFormat format):
    _width(width),
    _height(height),
    _format(format)
{
    OIIO::ImageSpec spec(
        _width,
        _height,
        channels(),
        (_format == ImageFormat::B8R8G8A8_UNORM) ? OIIO::TypeDesc::UINT8 :
            (_format == ImageFormat::R16G16B16A16_FLOAT) ? OIIO::TypeDesc::FLOAT : OIIO::TypeDesc::UNKNOWN
    );
    _buffer = std::make_shared<OIIO::ImageBuf>(spec);
}

}
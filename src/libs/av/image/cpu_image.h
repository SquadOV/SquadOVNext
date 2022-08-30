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
#include "av/image/image.h"

#include <OpenImageIO/imagebuf.h>
#include <memory>

namespace av {

// A simple wrapper around our representation of a CPU image buffer.
class AVEXPORT CpuImage: public IImage {
public:
    CpuImage(size_t width, size_t height, ImageFormat format);

    size_t width() const override { return _width;}
    size_t height() const override { return _height; }
    ImageFormat format() const override { return _format; }

    const OIIO::ImageBuf& raw() const { return *_buffer; }
    OIIO::ImageBuf& raw() { return *_buffer; }

    size_t bytesPerRow() const override { return _buffer->scanline_stride(); }

private:
    size_t _width;
    size_t _height;
    ImageFormat _format;
    std::shared_ptr<OIIO::ImageBuf> _buffer;
};

}
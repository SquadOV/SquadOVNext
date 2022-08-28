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
#include "av/image/image_io.h"

#include <OpenImageIO/imageio.h>
namespace av {

void writeImageToFile(const NativeImage& image, const std::filesystem::path& fname) {
    std::unique_ptr<OIIO::ImageOutput> out = OIIO::ImageOutput::create(fname.native());
    if (!out) {
        return;
    }

    // TODO: Handle other types? Probably OK since we'll generally only be dealing with UINT8 images.
    OIIO::ImageSpec spec(image.width(), image.height(), image.channels(), OIIO::TypeDesc::UINT8);
    out->open(fname.native(), spec);

    std::vector<uint8_t> rawData;
    image.fillRawBuffer(rawData);

    out->write_image(OIIO::TypeDesc::UINT8, rawData.data());
    out->close();
}

}
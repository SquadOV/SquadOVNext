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
#include "av/image/image_capture.h"
#include "av/image/dxgi_image_capture.h"

namespace av {

ImageCapturePtr createImageCapture(const titan::system::Process& process) {
#ifdef _WIN32
    try {
        auto dxgi = std::make_shared<DxgiImageCapture>(process);
        return dxgi;
    } catch (std::exception& ex) {
    }
#endif

    return nullptr;
}

}
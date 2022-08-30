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

#include "av/image/processing/image_capture_source.h"

namespace av {

ImageCaptureSource::ImageCaptureSource(const ImageCapturePtr& capture):
    _capture(capture)
{
    registerOutputParameter<std::optional<NativeImage>>(kOutput);
}

void ImageCaptureSource::compute(titan::utility::ParamId outputId, titan::utility::ProcessingCacheContainer& cache) const {
    const auto value = _capture->getCurrent();
    cache.setValue(titan::utility::ProcessingCacheType::Ephemeral, id(), outputId, value);
}

}
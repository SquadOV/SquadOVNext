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
#include "av/image/image_capture.h"
#include "titan/utility/processing.h"

namespace av {

// A processing node that wraps around the behavior of our ImageCapture objects.
class AVEXPORT ImageCaptureSource: public titan::utility::ProcessingNode {
public:
    enum Params {
        kOutput = 0
    };

    explicit ImageCaptureSource(const ImageCapturePtr& capture);

private:
    ImageCapturePtr _capture;

    void compute(titan::utility::ParamId outputId, titan::utility::ProcessingCacheContainer& cache) override;
};

}
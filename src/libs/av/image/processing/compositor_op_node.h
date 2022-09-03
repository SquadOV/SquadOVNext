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
#include "av/image/compositor/compositor_op.h"
#include <titan/utility/processing.h>

namespace av {

// The compositor op node will output a compositor op into a compositor layer.
class AVEXPORT CompositorOpNode: public titan::utility::ProcessingNode {
public:
    enum BaseParams {
        kOutput = 0
    };
    explicit CompositorOpNode(const CompositorOpPtr& op);

protected:
    CompositorOp& op() { return *_op; }

    template<typename T>
    T& typedOp() {
        return dynamic_cast<T&>(op());
    }

private:
    CompositorOpPtr _op;

    void compute(titan::utility::ParamId outputId, titan::utility::ProcessingCacheContainer& cache) override;
    virtual void updateOpNodeParams(titan::utility::ProcessingCacheContainer& cache) = 0;
};

}
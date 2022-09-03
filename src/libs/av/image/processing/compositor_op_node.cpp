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
#include "av/image/processing/compositor_op_node.h"

namespace tu = titan::utility;
namespace av {

CompositorOpNode::CompositorOpNode(const CompositorOpPtr& op):
    _op(op)
{
    registerOutputParameter<CompositorOpPtr>(kOutput);
    _op->finalize();
}

void CompositorOpNode::compute(titan::utility::ParamId outputId, titan::utility::ProcessingCacheContainer& cache) {
    // Derived nodes should figure out how to pull in parameters themselves.
    updateOpNodeParams(cache);

    cache.setValue(tu::ProcessingCacheType::Ephemeral, id(), kOutput, _op);
}

}
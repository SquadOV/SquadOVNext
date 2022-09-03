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

#include "titan/math/matrix.h"

namespace titan::math {

#ifdef _WIN32
DirectX::XMFLOAT4X4 matrixToDirectX(const Eigen::Matrix3f& mat) {
    // I think this needs to be transposed to handle the left-handed nature of DirectX?
    const auto t = mat.transpose();
    return DirectX::XMFLOAT4X4{
        t(0, 0),
        t(0, 1),
        t(0, 2),
        0.f,
        t(1, 0),
        t(1, 1),
        t(1, 2),
        0.f,
        t(2, 0),
        t(2, 1),
        t(2, 2),
        0.f,
        0.f,
        0.f,
        0.f,
        0.f
    };
}

DirectX::XMUINT2 vectorToDirectXUnsigned(const Eigen::Vector2i& mat) {
    return DirectX::XMUINT2{
        static_cast<unsigned int>(mat(0)),
        static_cast<unsigned int>(mat(1))
    };
}
#endif

}
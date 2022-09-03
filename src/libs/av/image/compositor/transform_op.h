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
#include <Eigen/Core>
#include <Eigen/Geometry>

namespace av {

// A compositor operation that translates/scales/rotates the image as necessary.
// This needs to be the first op always.
class AVEXPORT TransformOp {
public:
    enum class UvMode {
        Default = 1,
        Fill = 2
    };

    enum class Origin {
        TopLeft = 1,
        Center = 2
    };

    void setTranslation(const Eigen::Vector2f& v) { _translation = v; }
    void setRotation(float v) { _rotation = v; }
    void setScale(const Eigen::Vector2f& v) { _scale = v; }
    void setUvMode(UvMode m) { _uvmode = m; }
    void setOrigin(Origin o) { _origin = o; }

    const Eigen::Vector2f& translation() const { return _translation; }
    float rotation() const { return _rotation; }
    const Eigen::Vector2f& scale() const { return _scale; }
    UvMode uvmode() const { return _uvmode; }
    Origin origin() const { return _origin; }

    Eigen::Matrix3f transform() const;
    Eigen::Matrix3f inverseTransform() const { return transform().inverse(); };
    
private:
    Eigen::Vector2f _translation = Eigen::Vector2f::Zero();
    float _rotation = 0.f;
    Eigen::Vector2f _scale = Eigen::Vector2f::Ones();
    UvMode _uvmode = UvMode::Default;
    Origin _origin = Origin::TopLeft;
};

inline
Eigen::Matrix3f TransformOp::transform() const {
    using TTransform = Eigen::Transform<float, 2, Eigen::Affine, Eigen::RowMajor>;
    TTransform transform = TTransform::Identity();
    transform.prescale(_scale);
    transform.prerotate(_rotation);
    transform.pretranslate(_translation);
    return transform.matrix();
}

}
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
#ifdef _WIN32

#include <Windows.h>
#include <d3d11.h>
#include <directxmath.h>

#include "av/dll.h"
#include "av/image/compositor/transform_op.h"
#include "av/image/compositor/directx/base_op_dx11.h"
#include <titan/system/win32/directx/device.h>
#include <wil/com.h>

namespace av::directx {

class AVEXPORT TransformOpDx11: public av::TransformOp, public BaseOpDx11 {
public:
    explicit TransformOpDx11(const titan::system::win32::D3d11SharedDevicePtr& device);
    void finalize() override;

    struct TransformParams {
        // Pass a 4x4 matrix with the relevant bits in the 3x3 in the top left.
        // Trying to pass a 3x3 matrix and packing it in memory to match HLSL is...painful.
        DirectX::XMFLOAT4X4 invTransform;
        unsigned int uvMode;
        unsigned int transformOrigin;
        uint8_t _p1[8];
    };

protected:
    size_t threadGroupSizeX() const override { return 4; }
    size_t threadGroupSizeY() const override { return 4; }

    void updateShaderParams(ID3D11DeviceContext* context, std::vector<wil::com_ptr<ID3D11ShaderResourceView>>& srvs, std::vector<wil::com_ptr<ID3D11UnorderedAccessView>>& uavs) override;
};

}

#endif
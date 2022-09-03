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
#ifdef _WIN32

#include "av/image/compositor/directx/transform_op_dx11.h"
#include <titan/utility/exception.h>
#include <titan/math/matrix.h>

namespace fs = std::filesystem;
namespace av::directx {

TransformOpDx11::TransformOpDx11(const titan::system::win32::D3d11SharedDevicePtr& device):
    BaseOpDx11(device)
{
}

void TransformOpDx11::finalize() {
    compileDerivedShader(fs::path("shaders/transform.hlsl"), threadGroupSizeX(), threadGroupSizeY());
    BaseOpDx11::finalize();
    createCachedConstantBuffer<TransformParams>(1);
}

void TransformOpDx11::updateShaderParams(ID3D11DeviceContext* context, std::vector<wil::com_ptr<ID3D11ShaderResourceView>>& srvs, std::vector<wil::com_ptr<ID3D11UnorderedAccessView>>& uavs) {
    // Need to compute the inverse transform matrix every frame since it could have changed and it's faster to do this in the CPU than in the GPU.
    TransformParams transform;
    transform.invTransform = titan::math::matrixToDirectX(inverseTransform());
    transform.uvMode = static_cast<unsigned int>(uvmode());
    transform.transformOrigin = static_cast<unsigned int>(origin());

    context->UpdateSubresource(_cbuffers[1].get(), 0, nullptr, &transform, 0, 0);
}

}

#endif
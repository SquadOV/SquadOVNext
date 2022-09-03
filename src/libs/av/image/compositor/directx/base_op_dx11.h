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
#include <filesystem>
#include <wil/com.h>
#include <vector>
#include <directxmath.h>

#include <titan/system/win32/directx/device.h>
#include <titan/system/win32/exceptions.h>

#include "av/dll.h"
#include "av/image/compositor/compositor_op.h"

namespace av::directx {

class AVEXPORT BaseOpDx11: public av::CompositorOp {
public:
    explicit BaseOpDx11(const titan::system::win32::D3d11SharedDevicePtr& device);
    virtual ~BaseOpDx11() {}

    void render(av::NativeImage& output, const av::NativeImage& input) override;
    void finalize() override;

    #pragma pack(push, 4)
    struct CanvasOutput {
        DirectX::XMUINT2 startPos;
        DirectX::XMUINT2 size;
    };
    #pragma pack(pop)

    void updateCanvas(const Eigen::AlignedBox2i& box) override;

protected:
    virtual size_t threadGroupSizeX() const = 0;
    virtual size_t threadGroupSizeY() const = 0;

    void compileDerivedShader(const std::filesystem::path& path, size_t tx, size_t ty);

    template<typename T>
    void createCachedConstantBuffer(size_t index) {
        if (_cbuffers.size() <= index) {
            _cbuffers.resize(index+1);
        }

        D3D11_BUFFER_DESC constantDesc = { 0 };
        constantDesc.Usage = D3D11_USAGE_DEFAULT;
        constantDesc.ByteWidth = sizeof(T);
        constantDesc.BindFlags = D3D11_BIND_CONSTANT_BUFFER;

        HRESULT hr = _device->device()->CreateBuffer(&constantDesc, nullptr, &_cbuffers[index]);
        CHECK_WIN32_HRESULT_THROW(hr, "Failed to create constant buffer.");
    }

    void createCachedSamplerState(size_t index, const D3D11_SAMPLER_DESC& desc);

    virtual void updateShaderParams(ID3D11DeviceContext* context, std::vector<wil::com_ptr<ID3D11ShaderResourceView>>& srvs, std::vector<wil::com_ptr<ID3D11UnorderedAccessView>>& uavs) = 0;

    // Cached parameters that we're going to pass to the shader later.
    std::vector<wil::com_ptr<ID3D11SamplerState>> _samplers;
    std::vector<wil::com_ptr<ID3D11Buffer>> _cbuffers;
private:
    titan::system::win32::D3d11SharedDevicePtr _device;

    // The actual shader that we'll use to render.
    wil::com_ptr<ID3D11ComputeShader> _shader;

    // Base State
    CanvasOutput _canvas;
};

}

#endif
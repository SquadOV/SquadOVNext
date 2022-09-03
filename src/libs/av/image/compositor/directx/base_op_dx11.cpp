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

#include "av/image/compositor/directx/base_op_dx11.h"

#include <d3dcompiler.h>
#include <boost/algorithm/string.hpp>
#include <titan/utility/resource_manager.h>
#include <titan/utility/file_io.h>
#include <titan/utility/stl.h>
#include <titan/utility/strings.h>
#include <titan/system/win32/exceptions.h>
#include <titan/math/matrix.h>

namespace tu = titan::utility;
namespace fs = std::filesystem;
namespace av::directx {

BaseOpDx11::BaseOpDx11(const titan::system::win32::D3d11SharedDevicePtr& device):
    _device(device)
{}

void BaseOpDx11::render(av::NativeImage& output, const av::NativeImage& input) {
    auto immediate = _device->immediate()->get();
    immediate.context()->CSSetShader(_shader.get(), nullptr, 0);

    // SRVs and UAVs are safe to just re-create - I think.
    std::vector<wil::com_ptr<ID3D11UnorderedAccessView>> uavs(1);
    std::vector<wil::com_ptr<ID3D11ShaderResourceView>> srvs(1);
    
    // We need to create a UAV for the output image and an SRV for the input image.
    {
        D3D11_UNORDERED_ACCESS_VIEW_DESC desc;
        desc.Format = output.nativeFormat();
        desc.ViewDimension = D3D11_UAV_DIMENSION_TEXTURE2D;
        desc.Texture2D.MipSlice = 0;

        HRESULT hr = _device->device()->CreateUnorderedAccessView(output.nativeHandle(), &desc, &uavs[0]);
        CHECK_WIN32_HRESULT_THROW(hr, "Failed to create base UAV.");
    }

    {
        D3D11_SHADER_RESOURCE_VIEW_DESC desc;
        desc.Format = input.nativeFormat();
        desc.ViewDimension = D3D11_SRV_DIMENSION_TEXTURE2D;
        desc.Texture2D.MostDetailedMip = 0;
        desc.Texture2D.MipLevels = 1;

        HRESULT hr = _device->device()->CreateShaderResourceView(input.nativeHandle(), &desc, &srvs[0]);
        CHECK_WIN32_HRESULT_THROW(hr, "Failed to create base SRV.");
    }

    // Update the base's constant buffer (CanvasOutput).
    immediate.context()->UpdateSubresource(_cbuffers[0].get(), 0, nullptr, &_canvas, 0, 0);

    // Let derived classes do work in creating SRV's, UAV's, and updating constant buffers.
    updateShaderParams(immediate.context(), srvs, uavs);
    
    // Bind the UAVs.
    std::vector<ID3D11UnorderedAccessView*> puavs = tu::vectorSmartPtrToRaw(uavs);
    immediate.context()->CSSetUnorderedAccessViews(0, puavs.size(), puavs.data(), nullptr);

    // Bind the SRVs.
    std::vector<ID3D11ShaderResourceView*> psrvs = tu::vectorSmartPtrToRaw(srvs);
    immediate.context()->CSSetShaderResources(0, psrvs.size(), psrvs.data());

    // Bind all the registered sampler states.
    std::vector<ID3D11SamplerState*> psamplers = tu::vectorSmartPtrToRaw(_samplers);
    immediate.context()->CSSetSamplers(0, psamplers.size(), psamplers.data());

    // Bind all the registered constant buffers.
    std::vector<ID3D11Buffer*> pcbuffers = tu::vectorSmartPtrToRaw(_cbuffers);
    immediate.context()->CSSetConstantBuffers(0, pcbuffers.size(), pcbuffers.data());

    // Dispatch gogo!
    immediate.context()->Dispatch(
        std::ceil(static_cast<float>(_canvas.size.x) / threadGroupSizeX()),
        std::ceil(static_cast<float>(_canvas.size.y) / threadGroupSizeY()),
        1
    );

    // Unbind everything
    immediate.context()->CSSetShader(nullptr, nullptr, 0);

    std::fill(puavs.begin(), puavs.end(), nullptr);
    std::fill(psrvs.begin(), psrvs.end(), nullptr);
    std::fill(psamplers.begin(), psamplers.end(), nullptr);
    std::fill(pcbuffers.begin(), pcbuffers.end(), nullptr);

    immediate.context()->CSSetUnorderedAccessViews(0, puavs.size(), puavs.data(), nullptr);
    immediate.context()->CSSetShaderResources(0, psrvs.size(), psrvs.data());
    immediate.context()->CSSetSamplers(0, psamplers.size(), psamplers.data());
    immediate.context()->CSSetConstantBuffers(0, pcbuffers.size(), pcbuffers.data());
}

void BaseOpDx11::finalize() {
    createCachedConstantBuffer<CanvasOutput>(0);

    D3D11_SAMPLER_DESC samplerDesc;
    samplerDesc.Filter = D3D11_FILTER_MIN_MAG_LINEAR_MIP_POINT;
    samplerDesc.AddressU = D3D11_TEXTURE_ADDRESS_BORDER;
    samplerDesc.AddressV = D3D11_TEXTURE_ADDRESS_BORDER;
    samplerDesc.AddressW = D3D11_TEXTURE_ADDRESS_BORDER;
    samplerDesc.MipLODBias = 0.0f;
    samplerDesc.MaxAnisotropy = 1;
    samplerDesc.ComparisonFunc = D3D11_COMPARISON_ALWAYS;
    samplerDesc.BorderColor[0] = 0.f;
	samplerDesc.BorderColor[1] = 0.f;
	samplerDesc.BorderColor[2] = 0.f;
	samplerDesc.BorderColor[3] = 0.f;
    samplerDesc.MinLOD = 0;
    samplerDesc.MaxLOD = D3D11_FLOAT32_MAX;
    createCachedSamplerState(0, samplerDesc);
}

void BaseOpDx11::compileDerivedShader(const std::filesystem::path& path, size_t tx, size_t ty) {
    const auto absPathBase = tu::ResourceManager::get().findResourceFromRelativePath(fs::path("shaders/base.hlsl"));
    const auto absPathShader = tu::ResourceManager::get().findResourceFromRelativePath(path);

    auto baseStr = tu::readFileAsString(absPathBase);
    boost::algorithm::replace_all(baseStr, "OP_INCLUDE", tu::readFileAsString(absPathShader));

    UINT flags = D3DCOMPILE_ENABLE_STRICTNESS | D3DCOMPILE_WARNINGS_ARE_ERRORS
#ifndef NDEBUG
        | D3DCOMPILE_DEBUG
        | D3DCOMPILE_SKIP_OPTIMIZATION
#endif
    ;

    std::string stx = std::to_string(threadGroupSizeX());
    std::string sty = std::to_string(threadGroupSizeY());
    std::vector<D3D_SHADER_MACRO> macros{
        {"THREADS_X", stx.c_str()},
        {"THREADS_Y", sty.c_str()},
        {nullptr, nullptr}
    };

    wil::com_ptr<ID3DBlob> blob;
    wil::com_ptr<ID3DBlob> errBlob;
    HRESULT hr = D3DCompile(
        baseStr.c_str(),
        baseStr.size(),
        nullptr,
        macros.data(),
        D3D_COMPILE_STANDARD_FILE_INCLUDE,
        "csMain",
        "cs_5_0",
        flags,
        0,
        &blob,
        &errBlob
    );
    CHECK_WIN32_HRESULT_THROW(hr, fmt::format("Failed to compile DX11 compute shader for the compositor: {}", (char*)errBlob->GetBufferPointer()));

    hr = _device->device()->CreateComputeShader(blob->GetBufferPointer(), blob->GetBufferSize(), nullptr, &_shader);
    CHECK_WIN32_HRESULT_THROW(hr, "Failed to compile DX11 compute shader for the compositor.");
    CHECK_NULLPTR_THROW(_shader);
}

void BaseOpDx11::createCachedSamplerState(size_t index, const D3D11_SAMPLER_DESC& desc) {
    if (_samplers.size() <= index) {
        _samplers.resize(index+1);
    }

    HRESULT hr = _device->device()->CreateSamplerState(&desc, &_samplers[index]);
    CHECK_WIN32_HRESULT_THROW(hr, "Failed to create sampler.");
}

void BaseOpDx11::updateCanvas(const Eigen::AlignedBox2i& box) {
    _canvas.startPos = titan::math::vectorToDirectXUnsigned(box.min());
    _canvas.size = titan::math::vectorToDirectXUnsigned(box.sizes());
}

}

#endif
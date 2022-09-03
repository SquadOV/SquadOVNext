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

#include <Windows.h>
#include <d3d11.h>

#include "av/image/processing/directx/compositor_dx11_node.h"
#include "av/image/compositor/compositor_layer.h"
#include "av/image/os_image.h"
#include <titan/system/win32/exceptions.h>

namespace tu = titan::utility;
namespace av::directx {

CompositorDx11Node::CompositorDx11Node(const titan::system::win32::D3d11SharedDevicePtr& device, size_t numLayers, size_t width, size_t height):
    av::CompositorNode(numLayers, width, height),
    _device(device)
{
}

void CompositorDx11Node::compute(titan::utility::ParamId outputId, titan::utility::ProcessingCacheContainer& cache) {
    auto canvasOutput = cache.getValueOrComputeIf<av::NativeImagePtr>(
        tu::ProcessingCacheType::Persistent,
        id(),
        kCache,
        [this](const NativeImagePtr& a) { return !a || a->width() != width() || a->height() != height(); },
        [this](){
            D3D11_TEXTURE2D_DESC newDesc = { 0 };
            newDesc.Width = width();
            newDesc.Height = height();
            newDesc.MipLevels = 1;
            newDesc.ArraySize = 1;
            newDesc.Format = DXGI_FORMAT_B8G8R8A8_UNORM;
            newDesc.SampleDesc.Count = 1;
            newDesc.Usage = D3D11_USAGE_DEFAULT;
            newDesc.BindFlags = D3D11_BIND_SHADER_RESOURCE | D3D11_BIND_UNORDERED_ACCESS;
            newDesc.CPUAccessFlags = 0;
            newDesc.MiscFlags = 0;

            wil::com_ptr<ID3D11Texture2D> newTexture;
            HRESULT hr = _device->device()->CreateTexture2D(&newDesc, nullptr, &newTexture);
            CHECK_WIN32_HRESULT_THROW(hr, "Failed to create ingested texture.");
            return std::make_shared<NativeImage>(newTexture, _device);
        }
    );

    for (size_t i = 0; i < size(); ++i) {
        const auto& op = getInputValue<av::CompositorLayerPtr>(kLayerStart + i, cache);

        // TODO: Eventually be able to do fancier things regarding how we set the bounds of the canvas we want to write to
        // for each particular layer.
        op->renderTo(
            canvasOutput,
            Eigen::AlignedBox2i{
                Eigen::Vector2i{0 ,0},
                Eigen::Vector2i{width(), height()}
            }
        );
    }

    cache.setValue(tu::ProcessingCacheType::Ephemeral, id(), kOutput, canvasOutput.get());
}

}

#endif
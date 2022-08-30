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
#include "av/image/processing/image_dx11_ingest.h"
#include "av/image/os_image.h"
#include "av/image/cpu_image.h"
#include "titan/system/win32/exceptions.h"

namespace tu = titan::utility;
namespace av {

ImageDx11IngestNode::ImageDx11IngestNode(const titan::system::win32::D3d11SharedDevicePtr& device):
    _device(device)
{
    registerInputParameter<std::optional<NativeImage>>(kInput);
    registerOutputParameter<std::optional<NativeImage>>(kOutput);
}

void ImageDx11IngestNode::compute(tu::ParamId outputId, tu::ProcessingCacheContainer& cache) const {
    const auto& image = getInputValue<std::optional<NativeImage>>(kInput, cache);
    if (!image) {
        cache.setValue(tu::ProcessingCacheType::Ephemeral, id(), kOutput, std::nullopt);
        return;
    }

    // Create a DX11 texture and store it into the persistent cache. This will get re-used as continue
    // to evaluate this node so we don't have to keep on recreating it.
    auto ingestedImage = cache.getValueOrComputeIf<NativeImage>(
        tu::ProcessingCacheType::Persistent,
        id(),
        kCache,
        [&image](const NativeImage& a) { return !areGenericImagesCompatible(image.value(), a); },
        [&image, this](){
            D3D11_TEXTURE2D_DESC newDesc = { 0 };
            newDesc.Width = image->width();
            newDesc.Height = image->height();
            newDesc.MipLevels = 1;
            newDesc.ArraySize = 1;
            newDesc.Format = image->nativeFormat();
            newDesc.SampleDesc.Count = 1;
            newDesc.Usage = D3D11_USAGE_DEFAULT;
            newDesc.BindFlags = D3D11_BIND_SHADER_RESOURCE;
            newDesc.CPUAccessFlags = 0;

            // We need the D3D11_RESOURCE_MISC_SHARED flag since we're going to do the 
            // copy operation on the INPUT image context.
            newDesc.MiscFlags = D3D11_RESOURCE_MISC_SHARED;

            wil::com_ptr<ID3D11Texture2D> newTexture;
            HRESULT hr = _device->device()->CreateTexture2D(&newDesc, nullptr, &newTexture);
            CHECK_WIN32_HRESULT_THROW(hr, "Failed to create ingested texture.");
            return NativeImage{newTexture, _device};
        }
    );

    // At this point we need to determine if we're doing a GPU -> GPU transfer, a GPU -> CPU transfer, a 
    // CPU -> GPU transfer, or a CPU -> CPU transfer since the mechanics of most of them will be different.
    // Note that going from the GPU to the CPU will require the use of an intermediate buffer.
    const auto srcLoc = image->device()->location();
    const auto dstLoc = ingestedImage.get().device()->location();
    if (srcLoc == dstLoc) {
        // A naive simple copy works fine.
        image->copyToSameDeviceLocation(ingestedImage);
    } else if (srcLoc == titan::system::win32::D3d11DeviceLocation::GPU) {
        // There are two textures that need to be made here.
        //  1) The CPU texture that we will use as an intermediary for copying from the GPU back to the final CPU image.
        //     Note that the "CPU" destination image is still a "hardware" image but we can't copy directly from hardware to "hardware"
        //     since the "hardware" is a WARP device for DirectX 11.
        auto cpuStaging = cache.getValueOrComputeIf<CpuImage>(
            tu::ProcessingCacheType::Persistent,
            id(),
            kStagingCpu,
            [&image](const CpuImage& a) { return !areGenericImagesCompatible(image.value(), a); },
            [&image](){ return CpuImage{image->width(), image->height(), image->format()}; }
        );

        //  2) The GPU texture that we will use to stage the texture for copying from the input (otherwise it can't be read from the CPU).
        auto gpuStaging = cache.getValueOrComputeIf<NativeImage>(
            tu::ProcessingCacheType::Persistent,
            id(),
            kStagingGpu,
            [&image](const NativeImage& a) { return !areGenericImagesCompatible(image.value(), a); },
            [&image, this](){
                return image->createCompatibleStagingImage();
            }
        );
        
        image->copyToSameDeviceLocation(gpuStaging);
        gpuStaging.get().copyToCpu(cpuStaging);
        ingestedImage.get().copyFromCpu(cpuStaging);
    } else {
        throw tu::UnsupportedException{};
    }

    cache.setValue(tu::ProcessingCacheType::Ephemeral, id(), kOutput, std::optional{NativeImage{ingestedImage.get()}});
}

}

#endif
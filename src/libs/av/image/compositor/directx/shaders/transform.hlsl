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

static const uint UV_MODE_DEFAULT = 0x01u;
static const uint UV_MODE_FILL = 0x02u;

static const uint ORIGIN_TOP_LEFT = 0x01u;
static const uint ORIGIN_CENTER = 0x02u;


static const float kUv1 = 0.0625f;
static const float kUv3 = 0.1875f;
static const float kUv5 = 0.3125f;
static const float kUv7 = 0.4375f;

cbuffer TransformParams: register(b1)
{
    // The transform that we're passing in is a transform on the original image (NOT ON THE CANVAS).
    float4x4 kInvTransform: packoffset(c0);
    // The uvMode will tell us whether or not we want to stretch the input to the output (or some other fancy operation).
    uint kUvMode: packoffset(c4.x);
    // The transformOrigin tells us what we want to do the rotation relative to (i.e. top left, center).
    uint kTransformOrigin: packoffset(c4.y);
    // 
    uint2 _padding: packoffset(c4.z);
};

float2 outputPosToSampleUv(uint2 outputPos, uint2 inputDims, uint2 base, uint2 origin) {
    uint2 canvasPixel = outputPos - kStartPos;
    float2 inputUv = float2(canvasPixel) / base;
    float2 inputPixel = inputUv * inputDims;

    // At this point, we have *some* pixel in the input image. But what we want instead is what pixel would be at this
    // position AFTER we applied the transformation. Hence, we need to pass in the inverse transformation instead.
    float3x3 xform = {kInvTransform[0].xyz, kInvTransform[1].xyz, kInvTransform[2].xyz};
    float2 samplePixel = mul(float3(inputPixel - origin, 1.f), xform).xy + float2(origin);
    return samplePixel / float2(inputDims);
}

void runOp(uint2 outputPos) {
    uint2 inputDims;
    gInput.GetDimensions(inputDims.x, inputDims.y);

    uint2 base = 0;
    if (kUvMode == UV_MODE_DEFAULT) {
        base = inputDims;
    } else if (kUvMode == UV_MODE_FILL) {
        base = kSize;
    }

    uint2 origin = 0;
    if (kTransformOrigin == ORIGIN_TOP_LEFT) {
        origin = uint2(0, 0);
    } else if (kTransformOrigin == ORIGIN_CENTER) {
        origin = inputDims / 2;
    }

    float2 sampleUv = outputPosToSampleUv(outputPos, inputDims, base, origin);

    // Note that in the case where uvMode = UV_MODE_FILL, we're going to assume that the canvas and the input image are the same size for the math.
    // So the sampling is based on https://docs.microsoft.com/en-us/windows/win32/api/d3d11/ne-d3d11-d3d11_standard_multisample_quality_levels
    // Let's attempt to use the same standard 8 sample pattern that DirectX does.
    float4 sum = 0.f;

    // We want to emulate ddx and ddy in pixel shader code. Effectively, we want the change in
    // the input image's UV for a unit (pixel) step in the output image. This can be computed as a simple
    // forward difference (just finding a gradient in either direction). Given some output pixel (i.e. outputPos),
    // if we move 1 unit in the X or Y direction, what's the corresponding step we'd take in the input image's UV space?
    float2 sampleUvDx = outputPosToSampleUv(outputPos + uint2(1, 0), inputDims, base, origin) - sampleUv;
    float2 sampleUvDy = outputPosToSampleUv(outputPos + uint2(0, 1), inputDims, base, origin) - sampleUv;

    sum += gInput.SampleLevel(gSampler, sampleUv + kUv1 * sampleUvDx + kUv1 * sampleUvDy, 0);
    sum += gInput.SampleLevel(gSampler, sampleUv - kUv1 * sampleUvDx + kUv3 * sampleUvDy, 0);
    sum += gInput.SampleLevel(gSampler, sampleUv + kUv5 * sampleUvDx + kUv1 * sampleUvDy, 0);
    sum += gInput.SampleLevel(gSampler, sampleUv - kUv3 * sampleUvDx - kUv5 * sampleUvDy, 0);
    sum += gInput.SampleLevel(gSampler, sampleUv - kUv5 * sampleUvDx + kUv5 * sampleUvDy, 0);
    sum += gInput.SampleLevel(gSampler, sampleUv - kUv7 * sampleUvDx - kUv1 * sampleUvDy, 0);
    sum += gInput.SampleLevel(gSampler, sampleUv + kUv3 * sampleUvDx + kUv7 * sampleUvDy, 0);
    sum += gInput.SampleLevel(gSampler, sampleUv + kUv7 * sampleUvDx - kUv7 * sampleUvDy, 0);

    gOutput[outputPos] = sum / 8.f;
}
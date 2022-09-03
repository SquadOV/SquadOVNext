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

// Single Output
RWTexture2D<float4> gOutput: register(u0);

// Inputs
Texture2D gInput: register(t0);
SamplerState gSampler: register(s0);

// Parameters
cbuffer CanvasOutput: register(b0)
{
    uint2 kStartPos: packoffset(c0);
    uint2 kSize: packoffset(c0.z);
};

OP_INCLUDE

[numthreads(THREADS_X, THREADS_Y, 1)]
void csMain(uint3 dtId : SV_DispatchThreadID) {
    uint2 outputMax;
    gOutput.GetDimensions(outputMax.x, outputMax.y);

    uint2 canvasMax = kStartPos + kSize;
    
    [unroll(THREADS_Y)] for (int dy = 0; dy < THREADS_Y; ++dy) {
        const uint y = kStartPos.y + dtId.y + dy;
        if (y >= canvasMax.y || y >= outputMax.y) {
            break;
        }

        [unroll(THREADS_X)] for (int dx = 0; dx < THREADS_X; ++dx) {
            const uint x = kStartPos.x + dtId.x + dx;
            if (x >= canvasMax.x || x >= outputMax.x) {
                break;
            }

            runOp(uint2(x, y));
        }
    }
}
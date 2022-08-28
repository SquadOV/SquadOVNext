# Prerequisites

* [Visual Studio 2022](https://visualstudio.microsoft.com/)
* [.NET 6.0 x64 SDK](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks)
* [CMake 3.24](https://cmake.org)
* [VCPKG](https://vcpkg.io)

# VCPkg Packages

We will assume vcpkg is at commit `e85b5bb95a14ff7e014601e88a8dc2fea6798e33`.

* `wil:x64-windows`
* `boost:x64-windows` - must be v1.80+
* `openimageio:x64-windows`

# Source Code

* `git clone --recurse-submodules https://github.com/SquadOV/SquadOVNext.git`

# C++ Components

* `cd $SRC`
* `mkdir build && cd build`
* `cmake -S ../ -G "Visual Studio 17 2022" -A x64 -DCMAKE_BUILD_TYPE=Debug -DCMAKE_TOOLCHAIN_FILE=$VCPKG\scripts\buildsystems\vcpkg.cmake`
* `cmake --build build --config Debug`
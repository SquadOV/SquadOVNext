# Prerequisites

* [Visual Studio 2022](https://visualstudio.microsoft.com/)
* [.NET 6.0 x64 SDK](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks)
* [CMake 3.24](https://cmake.org)
* [SWIG 4.0.2](https://www.swig.org/)
* [VCPKG](https://vcpkg.io)

# VCPkg Packages

* `wil:x64-windows`

# Source Code

* git clone --recurse-submodules git@github.com:SquadOV/SquadOVNext.git

# C++ Components

* `cd $SRC`
* `mkdir build && cd build`
* `cmake -S ../ -G "Visual Studio 17 2022" -A x64 -DCMAKE_BUILD_TYPE=Debug -DCMAKE_TOOLCHAIN_FILE=$VCPKG\scripts\buildsystems\vcpkg.cmake`
* `cmake --build build --config Debug`
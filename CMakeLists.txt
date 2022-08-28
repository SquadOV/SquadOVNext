cmake_minimum_required(VERSION 3.24)
project(
    SquadOVNext
    VERSION 0.0
    LANGUAGES C CXX
)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED True)
set(CMAKE_MODULE_PATH
    ${CMAKE_MODULE_PATH} 
    ${CMAKE_CURRENT_SOURCE_DIR}/cmake
)

if(WIN32)
    add_definitions(-DNOMINMAX)
endif()

include(ExecutableHelper)

#
# Project Options
#
option(ENABLE_TESTING "Whether to compile tests" ON)

if(WIN32)
    SET(SWIG_DIR ${CMAKE_SOURCE_DIR}/deps/swig/Lib)
    SET(SWIG_EXECUTABLE ${CMAKE_SOURCE_DIR}/deps/swig/swig.exe)
endif()
find_package(SWIG REQUIRED)
include(UseSWIG)
include(SWIGHelper)
set (UseSWIG_TARGET_NAME_PREFERENCE STANDARD)

set(Boost_NO_WARN_NEW_VERSIONS ON)
set(BOOST_COMPONENTS
    filesystem
    system
    program_options
)

if(WIN32)
    set(BOOST_COMPONENTS ${BOOST_COMPONENTS} stacktrace_windbg)
endif()

find_package(Boost 1.80.0 REQUIRED
    COMPONENTS "${BOOST_COMPONENTS}"
)

if(WIN32)
    find_package(WIL CONFIG REQUIRED)
endif()

find_package(OpenImageIO CONFIG REQUIRED)

add_subdirectory(deps)
include(DoctestHelper)
include(${CMAKE_SOURCE_DIR}/deps/doctest/scripts/cmake/doctest.cmake)

if(ENABLE_TESTING)
    enable_testing()
endif()

add_subdirectory(src)

set(CPACK_PROJECT_NAME ${PROJECT_NAME})
set(CPACK_PROJECT_VERSION ${PROJECT_VERSION})
include(CPack)
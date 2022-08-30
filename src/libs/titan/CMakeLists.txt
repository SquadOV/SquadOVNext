set(SRCS
    ${CMAKE_CURRENT_SOURCE_DIR}/interface.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/system/process.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/system/process_di.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/system/window_di.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/system/process_handle.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/system/win32/process_di.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/system/win32/window_di.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/system/win32/exceptions.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/system/win32/directx/context.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/system/win32/directx/device.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/system/win32/directx/shared_texture.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/utility/strings.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/utility/exception.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/utility/log.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/utility/ntp_client.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/utility/time.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/utility/processing.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/math/bounding_box.cpp
)

set(HEADERS
    ${CMAKE_CURRENT_SOURCE_DIR}/dll.h
    ${CMAKE_CURRENT_SOURCE_DIR}/interface.h
    ${CMAKE_CURRENT_SOURCE_DIR}/system/process.h
    ${CMAKE_CURRENT_SOURCE_DIR}/system/process_handle.h
    ${CMAKE_CURRENT_SOURCE_DIR}/system/process_di.h
    ${CMAKE_CURRENT_SOURCE_DIR}/system/process_di_forward.h
    ${CMAKE_CURRENT_SOURCE_DIR}/system/window_di.h
    ${CMAKE_CURRENT_SOURCE_DIR}/system/window_di_forward.h
    ${CMAKE_CURRENT_SOURCE_DIR}/system/win32/conversions.h
    ${CMAKE_CURRENT_SOURCE_DIR}/system/win32/exceptions.h
    ${CMAKE_CURRENT_SOURCE_DIR}/system/win32/directx/context.h
    ${CMAKE_CURRENT_SOURCE_DIR}/system/win32/directx/device.h
    ${CMAKE_CURRENT_SOURCE_DIR}/system/win32/directx/shared_texture.h
    ${CMAKE_CURRENT_SOURCE_DIR}/utility/strings.h
    ${CMAKE_CURRENT_SOURCE_DIR}/utility/exception.h
    ${CMAKE_CURRENT_SOURCE_DIR}/utility/log.h
    ${CMAKE_CURRENT_SOURCE_DIR}/utility/ntp_client.h
    ${CMAKE_CURRENT_SOURCE_DIR}/utility/time.h
    ${CMAKE_CURRENT_SOURCE_DIR}/utility/time_forward.h
    ${CMAKE_CURRENT_SOURCE_DIR}/utility/processing.h
    ${CMAKE_CURRENT_SOURCE_DIR}/math/bounding_box.h
)

add_library(libtitan
    SHARED
    ${SRCS}
    ${HEADERS}
)

target_compile_definitions(libtitan
    PRIVATE
    LIBTITAN_EXPORTS
)

target_include_directories(libtitan
    PUBLIC
    ${CMAKE_CURRENT_SOURCE_DIR}/../
)

target_link_libraries(libtitan PUBLIC
    fmt::fmt
    spdlog::spdlog
    Eigen3::Eigen
)

if(WIN32)
    target_link_libraries(libtitan PUBLIC    
        Kernel32
        Version
        Boost::stacktrace_windbg
        ole32
        dbgeng
        comsuppw
        d3d11
        dxgi
    )
endif()

set(TEST_SRCS
    ${CMAKE_CURRENT_SOURCE_DIR}/tests.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/system/process_tests.cpp
)

doctest_wrapper(
    libtitantests
    libtitan
    "${TEST_SRCS}"
)
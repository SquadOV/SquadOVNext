set(SRCS
    ${CMAKE_CURRENT_SOURCE_DIR}/interface.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/image/dxgi_image_capture.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/image/image_capture.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/image/image_io.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/image/image.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/image/os_image_win32.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/image/cpu_image.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/image/processing/image_capture_source.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/image/processing/image_dx11_ingest.cpp
)

set(HEADERS
    ${CMAKE_CURRENT_SOURCE_DIR}/interface.h
    ${CMAKE_CURRENT_SOURCE_DIR}/dll.h
    ${CMAKE_CURRENT_SOURCE_DIR}/image/image_capture.h
    ${CMAKE_CURRENT_SOURCE_DIR}/image/image_io.h
    ${CMAKE_CURRENT_SOURCE_DIR}/image/image.h
    ${CMAKE_CURRENT_SOURCE_DIR}/image/os_image.h
    ${CMAKE_CURRENT_SOURCE_DIR}/image/cpu_image.h
    ${CMAKE_CURRENT_SOURCE_DIR}/image/processing/image_capture_source.h
    ${CMAKE_CURRENT_SOURCE_DIR}/image/processing/image_sink.h
    ${CMAKE_CURRENT_SOURCE_DIR}/image/processing/image_dx11_ingest.h
)

add_library(libav
    SHARED
    ${SRCS}
    ${HEADERS}
)

target_compile_definitions(libav
    PRIVATE
    LIBAV_EXPORTS
)

target_include_directories(libav
    PUBLIC
    ${CMAKE_CURRENT_SOURCE_DIR}/../
)

target_link_libraries(libav
    PUBLIC
    libtitan
    WIL::WIL
    OpenImageIO::OpenImageIO
)
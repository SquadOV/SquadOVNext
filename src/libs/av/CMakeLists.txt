set(SRCS
    ${CMAKE_CURRENT_SOURCE_DIR}/interface.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/image/dxgi_image_capture.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/image/image_capture.cpp
)

set(HEADERS
    ${CMAKE_CURRENT_SOURCE_DIR}/interface.h
    ${CMAKE_CURRENT_SOURCE_DIR}/dll.h
    ${CMAKE_CURRENT_SOURCE_DIR}/image/image_capture.h
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
    libtitan
    WIL::WIL
)

set(SWIG_SOURCES
    ${CMAKE_CURRENT_SOURCE_DIR}/interface.i
    ${CMAKE_CURRENT_SOURCE_DIR}/image/image.i
)

standard_swig_wrapper(
    libavnet
    libav
    av
    LIBAV_EXPORTS
    "${SWIG_SOURCES}"
)
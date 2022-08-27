SET(SRCS
    ${CMAKE_CURRENT_SOURCE_DIR}/interface.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/image/dxgi_image_capture.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/image/image_capture.cpp
)

SET(HEADERS
    ${CMAKE_CURRENT_SOURCE_DIR}/interface.h
    ${CMAKE_CURRENT_SOURCE_DIR}/dll.h
    ${CMAKE_CURRENT_SOURCE_DIR}/image/image_capture.h
)

add_library(libav
    STATIC
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
    WIL::WIL
)

SET(SWIG_SOURCES
    ${CMAKE_CURRENT_SOURCE_DIR}/interface.i
    ${CMAKE_CURRENT_SOURCE_DIR}/image/image.i
)


if(WIN32)
    set(SWIG_FLAGS _WIN32)
endif()

set_source_files_properties(${SWIG_SOURCES} PROPERTIES
    CPLUSPLUS ON
    USE_TARGET_INCLUDE_DIRECTORIES TRUE
    USE_SWIG_DEPENDENCIES TRUE
)

swig_add_library(
    libavnet
    TYPE SHARED
    LANGUAGE csharp
    SOURCES ${SWIG_SOURCES}
    OUTPUT_DIR ${CMAKE_CURRENT_SOURCE_DIR}/generated
)
set_property(TARGET libavnet PROPERTY SWIG_USE_TARGET_INCLUDE_DIRECTORIES TRUE)
set_property(TARGET libavnet PROPERTY SWIG_COMPILE_DEFINITIONS ${SWIG_FLAGS} CSHARP)
set_property(TARGET libavnet PROPERTY SWIG_COMPILE_OPTIONS -namespace av)
set_property(TARGET libavnet PROPERTY SWIG_GENERATED_COMPILE_DEFINITIONS LIBAV_EXPORTS CSHARP)
target_link_libraries(libavnet libav)
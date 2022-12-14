set(SRCS
    ${CMAKE_CURRENT_SOURCE_DIR}/engine.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/process.cpp
)

set(HEADERS
    ${CMAKE_CURRENT_SOURCE_DIR}/engine.h
    ${CMAKE_CURRENT_SOURCE_DIR}/process.h
    ${CMAKE_CURRENT_SOURCE_DIR}/dll.h
)

add_library(libengine
    SHARED
    ${SRCS}
    ${HEADERS}
)

target_compile_definitions(libengine
    PRIVATE
    LIBENGINE_EXPORTS
)

target_include_directories(libengine
    PUBLIC
    ${CMAKE_CURRENT_SOURCE_DIR}/../
)

target_link_libraries(libengine PUBLIC
    libav
    libtitan
)

set(SWIG_SOURCES
    ${CMAKE_CURRENT_SOURCE_DIR}/engine.i
)

standard_swig_wrapper(
    libenginenet
    libengine
    LibEngine
    LIBENGINE_EXPORTS
    "${SWIG_SOURCES}"
)
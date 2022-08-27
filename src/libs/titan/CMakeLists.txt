set(SRCS
    ${CMAKE_CURRENT_SOURCE_DIR}/interface.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/system/process.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/system/win32/process_internal.cpp
)

set(HEADERS
    ${CMAKE_CURRENT_SOURCE_DIR}/dll.h
    ${CMAKE_CURRENT_SOURCE_DIR}/interface.h
    ${CMAKE_CURRENT_SOURCE_DIR}/system/process.h
    ${CMAKE_CURRENT_SOURCE_DIR}/system/process_internal.h
    ${CMAKE_CURRENT_SOURCE_DIR}/system/win32/conversions.h
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

if(WIN32)
    target_link_libraries(libtitan PUBLIC    
        Kernel32
        Version
    )
endif()

set(SWIG_SOURCES
    ${CMAKE_CURRENT_SOURCE_DIR}/interface.i
    ${CMAKE_CURRENT_SOURCE_DIR}/system/system.i
)

standard_swig_wrapper(
    libtitannet
    libtitan
    titan
    LIBTITAN_EXPORTS
    "${SWIG_SOURCES}"
)
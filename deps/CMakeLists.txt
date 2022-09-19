add_subdirectory(doctest)

set(BUILD_SHARED_LIBS ON)
add_subdirectory(fmt)

set(SPDLOG_BUILD_SHARED ON)
set(SPDLOG_FMT_EXTERNAL ON)
if(WIN32)
    set(SPDLOG_WCHAR_SUPPORT ON)
    set(SPDLOG_WCHAR_FILENAMES ON)
endif()
add_subdirectory(spdlog)

set(EIGEN_BUILD_DOC OFF)
set(BUILD_TESTING OFF)
add_subdirectory(eigen)
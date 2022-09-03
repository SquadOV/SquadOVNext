add_subdirectory(doctest)

set(BUILD_SHARED_LIBS ON)
add_subdirectory(fmt)

set(SPDLOG_BUILD_SHARED ON)
set(SPDLOG_FMT_EXTERNAL ON)
add_subdirectory(spdlog)

set(EIGEN_BUILD_DOC OFF)
set(BUILD_TESTING OFF)
add_subdirectory(eigen)
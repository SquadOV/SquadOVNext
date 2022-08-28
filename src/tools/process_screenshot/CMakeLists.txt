add_executable(process_screenshot
    ${CMAKE_CURRENT_SOURCE_DIR}/main.cpp
)

target_link_libraries(process_screenshot PRIVATE
    libav
    Boost::program_options
)

finalize_executable(process_screenshot)
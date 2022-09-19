add_custom_target(desktop ALL)
add_custom_command(TARGET desktop POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy $<TARGET_RUNTIME_DLLS:libenginenet> "${CMAKE_CURRENT_SOURCE_DIR}/bin/${CMAKE_BUILD_TYPE}/net6.0-windows10.0.17763.0"
    COMMAND_EXPAND_LISTS
)

add_custom_command(TARGET desktop POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy $<TARGET_FILE:libenginenet> "${CMAKE_CURRENT_SOURCE_DIR}/bin/${CMAKE_BUILD_TYPE}/net6.0-windows10.0.17763.0"
    COMMAND_EXPAND_LISTS
)
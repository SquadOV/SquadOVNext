MACRO(finalize_executable TARGET)
    if(WIN32)
        add_custom_command(TARGET ${TARGET} POST_BUILD
            COMMAND ${CMAKE_COMMAND} -E copy $<TARGET_RUNTIME_DLLS:${TARGET}> $<TARGET_FILE_DIR:${TARGET}>
            COMMAND_EXPAND_LISTS
        )
        
        file(GLOB HLSL_SHADERS "${CMAKE_SOURCE_DIR}/src/libs/av/image/compositor/directx/shaders/*.hlsl")
        add_custom_command(
            TARGET ${TARGET}
            POST_BUILD
            COMMAND ${CMAKE_COMMAND} -E make_directory $<TARGET_FILE_DIR:${TARGET}>/shaders
            COMMAND ${CMAKE_COMMAND} -E copy_if_different ${HLSL_SHADERS} $<TARGET_FILE_DIR:${TARGET}>/shaders
            COMMENT "Copying Shaders...\n"
        )
        endif()
ENDMACRO()
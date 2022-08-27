MACRO(doctest_wrapper TARGET PARENT_TARGET TEST_SRC SRCS HEADERS)
    if(ENABLE_TESTING)
        add_executable(${TARGET} ${TEST_SRC} ${SRCS} ${HEADERS})
        target_compile_definitions(${TARGET} PRIVATE
            $<TARGET_PROPERTY:${PARENT_TARGET},COMPILE_DEFINITIONS>
            TESTS
        )

        target_include_directories(${TARGET} PRIVATE
            $<TARGET_PROPERTY:${PARENT_TARGET},INCLUDE_DIRECTORIES>
        )

        target_link_libraries(${TARGET} PRIVATE
            doctest::doctest
            $<TARGET_PROPERTY:${PARENT_TARGET},LINK_LIBRARIES>
        )

        doctest_discover_tests(${TARGET})
    endif()
ENDMACRO()
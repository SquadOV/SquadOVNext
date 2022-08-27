MACRO(doctest_wrapper TARGET PARENT_TARGET TEST_SRC)
    if(ENABLE_TESTING)
        add_executable(${TARGET} ${TEST_SRC})
        target_compile_definitions(${TARGET} PRIVATE TESTS)
        target_link_libraries(${TARGET} PRIVATE
            doctest::doctest
            ${PARENT_TARGET}
        )

        doctest_discover_tests(${TARGET})
    endif()
ENDMACRO()
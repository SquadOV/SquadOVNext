//
//  Copyright (C) 2022 Michael Bao
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
#pragma once

#include "titan/dll.h"
#include <exception>
#include <source_location>
#include <string>

#define CREATE_SIMPLE_EXCEPTION_CLASS(NAME, WHAT) \
    class NAME: public titan::utility::Exception { \
    public: \
        explicit NAME(const std::source_location& loc = std::source_location::current()):\
            titan::utility::Exception(WHAT, loc)\
        {}\
    }

namespace titan::utility {

// A generic exception class that contains additional information
// that's useful for debugging (e.g. the backtrace).
class TITANEXPORT Exception: public std::exception {
public:
    Exception(
        const std::string& what,
        const std::source_location& loc = std::source_location::current()
    ) noexcept;

    const char* what() const noexcept override { return _what.c_str(); };

private:
    std::string _what;
};

CREATE_SIMPLE_EXCEPTION_CLASS(MemoryAllocationException, "Failed to allocate memory or unexpected nullptr.");
CREATE_SIMPLE_EXCEPTION_CLASS(UnsupportedException, "Unsupported operation.");

}

#define CHECK_NULLPTR_THROW_EXCEPTION(X, E) if (!X) { throw E; }
#define CHECK_NULLPTR_THROW(X) CHECK_NULLPTR_THROW_EXCEPTION(X, titan::utility::MemoryAllocationException{})
#define CHECK_NULLPTR_IF(X) if (!X)
#define CHECK_NULLPTR_IF_RETURN(X, RET) if (!X) { return RET; }
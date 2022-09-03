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

#ifdef _WIN32
#include <Windows.h>
#endif

#include <memory>
#include <vector>

#ifdef _WIN32

namespace std {
template<typename T>
struct pointer_traits<wil::com_ptr<T>> {
    using pointer = wil::com_ptr<T>;
    using element_type = T;
    using diffence_type = std::ptrdiff_t;

    static element_type* to_address(pointer p) noexcept {
        return p.get();
    }
};

}

#endif


namespace titan::utility {


template<typename T>
std::vector<typename std::pointer_traits<T>::element_type*> vectorSmartPtrToRaw(const std::vector<T>& input) {
    std::vector<typename std::pointer_traits<T>::element_type*> output;
    output.reserve(input.size());

    for (const auto& v : input) {
        output.push_back(std::pointer_traits<T>::to_address(v));
    }

    return output;
}

}
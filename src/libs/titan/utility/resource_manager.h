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
#include "titan/utility/exception.h"
#include <filesystem>

namespace titan::utility {

// A resource manager that will help us search through a bunch of different filesystem paths
// to find/load the given "resource" whatever said resource may be.
class TITANEXPORT ResourceManager {
public:
    static ResourceManager& get();
    std::filesystem::path findResourceFromRelativePath(const std::filesystem::path& path) const;
};

CREATE_SIMPLE_EXCEPTION_CLASS(ResourceNotFoundException, "Resource not found.");

}
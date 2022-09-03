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
#include "titan/utility/resource_manager.h"
#include <memory>

namespace fs = std::filesystem;
namespace titan::utility {

ResourceManager& ResourceManager::get() {
    static auto global = std::make_unique<ResourceManager>();
    return *global;
}

std::filesystem::path ResourceManager::findResourceFromRelativePath(const fs::path& path) const {
    auto retPath = fs::absolute(path);
    if (!fs::exists(retPath)) {
        throw ResourceNotFoundException{};
    }
    return retPath;
}

}
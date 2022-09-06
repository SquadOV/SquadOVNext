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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadOV.Services.Config
{
    internal interface IConfigService
    {
        // Check whether or not the configuration was just created (i.e. it didn't used to exist).
        bool IsNewlyCreated { get; }

        // Some path to a folder on the user's local machine that stores basic files for the app - this path can not be changed by the user.
        string UserFolder { get; }

        // Path to the file in the UserFolder that holds the configuration.
        string ConfigFile { get; }

        // The actual config data that we loaded.
        Models.Settings.ConfigModel Config { get; }
    }
}

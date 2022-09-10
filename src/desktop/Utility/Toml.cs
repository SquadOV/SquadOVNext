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
using Avalonia.Media;
using System;
using Tomlyn;

namespace SquadOV.Constants
{
    internal class Toml
    {
        public static TomlModelOptions TomlOptions
        {
            get
            {
                var options = new TomlModelOptions()
                {
                    IgnoreMissingProperties = true,
                    ConvertToToml = (input) =>
                    {
                        var it = input.GetType();
                        if (it.IsEnum)
                        {
                            return input.ToString();
                        }
                        return null;
                    },
                    ConvertToModel = (input, type) =>
                    {
                        if (type.IsEnum)
                        {
                            return Enum.Parse(type, (string)input);
                        }

                        return null;
                    },
                };
                return options;
            }
        }
    }
}

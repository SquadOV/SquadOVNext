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
using System.ComponentModel;
using System.Reflection;
using System.IO;
using ReactiveUI;

namespace SquadOV.Models.Settings
{
    internal abstract class BaseConfigModel: ReactiveObject
    {
        // This function allows us to add new config parameters and allow users with existing configs
        // to migrate over by generating the default values properly.
        public void FillInMissing(BaseConfigModel from)
        {
            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                var realType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                if (realType.IsSubclassOf(typeof(BaseConfigModel)))
                {
                    var propFrom = (BaseConfigModel?)prop.GetValue(from);
                    if (propFrom != null)
                    {
                        ((BaseConfigModel?)prop.GetValue(this))?.FillInMissing(propFrom);
                    }
                }
                else
                {
                    var selfValue = prop.GetValue(this);
                    var refValue = prop.GetValue(from);

                    if (refValue != null && selfValue == null)
                    {
                        prop.SetValue(this, refValue);
                    }
                }
            }
        }
    }

    internal class CoreConfigModel: BaseConfigModel
    {
        private string? _databasePath;
        public string? DatabasePath
        {
            get => _databasePath;
            set => this.RaiseAndSetIfChanged(ref _databasePath, value);
        }

        private string? _culture;
        public string? Culture
        {
            get => _culture;
            set => this.RaiseAndSetIfChanged(ref _culture, value);
        }

        public static CoreConfigModel CreateDefault(string location)
        {
            return new CoreConfigModel()
            {
                DatabasePath = Path.Combine(location, "Database"),
                Culture = "en",
            };
        }
    }

    internal class ConfigModel: BaseConfigModel
    {
        public static ConfigModel CreateDefault(string location)
        {
            return new ConfigModel()
            {
                Core = CoreConfigModel.CreateDefault(location),
            };
        }

        private CoreConfigModel? _core;
        public CoreConfigModel? Core
        {
            get => _core;
            private set => this.RaiseAndSetIfChanged(ref _core, value);
        }
    }
}

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
using System.Reflection;
using ReactiveUI;

namespace SquadOV.Models.Settings
{
    public abstract class BaseConfigModel: ReactiveObject
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
                        var propTo = ((BaseConfigModel?)prop.GetValue(this));
                        if (propTo == null)
                        {
                            prop.SetValue(this, propFrom);
                        }
                        else
                        {
                            propTo?.FillInMissing(propFrom);
                        }
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

    public class ConfigModel: BaseConfigModel
    {
        public static ConfigModel CreateDefault(string location)
        {
            return new ConfigModel()
            {
                Core = Config.CoreConfigModel.CreateDefault(location),
                Hotkeys = Config.HotkeyConfigModel.CreateDefault(),
                Games = Config.GameConfigModel.CreateDefault(),
            };
        }

        private Config.CoreConfigModel? _core;
        public Config.CoreConfigModel? Core
        {
            get => _core;
            private set
            {
                this.RaiseAndSetIfChanged(ref _core, value);
                if (_core != null)
                {
                    _core.PropertyChanged += (s, e) => this.RaisePropertyChanged($"Core.{e.PropertyName}");
                }
            }
        }

        private Config.HotkeyConfigModel? _hotkeys;
        public Config.HotkeyConfigModel? Hotkeys
        {
            get => _hotkeys;
            private set
            {
                this.RaiseAndSetIfChanged(ref _hotkeys, value);
                if (_hotkeys != null)
                {
                    _hotkeys.PropertyChanged += (s, e) => this.RaisePropertyChanged($"Hotkeys.{e.PropertyName}");
                }
            }
        }

        private Config.GameConfigModel? _games;
        public Config.GameConfigModel? Games
        {
            get => _games;
            private set
            {
                this.RaiseAndSetIfChanged(ref _games, value);
                if (_games != null)
                {
                    _games.PropertyChanged += (s, e) => this.RaisePropertyChanged($"Games.{e.PropertyName}");
                }
            }
        }
    }
}

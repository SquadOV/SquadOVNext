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
using ReactiveUI;
using SquadOV.Constants;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace SquadOV.Models.Settings.Config
{
    public class HotkeyConfigModel : BaseConfigModel
    {
        private Hotkey? _screenshot;
        public Hotkey? Screenshot
        {
            get => _screenshot;
            set => this.RaiseAndSetIfChanged(ref _screenshot, value);
        }

        private List<Hotkey?>? _allHotkeys = null;
        [IgnoreDataMember]
        public List<Hotkey?> AllHotkeys => _allHotkeys ??= new List<Hotkey?>()
        {
            Screenshot,
        };

        public static HotkeyConfigModel CreateDefault()
        {
            return new HotkeyConfigModel()
            {
                Screenshot = new Hotkey()
                {
                    Action = LogicalAction.Screenshot,
                    Keys = new ObservableCollection<int>()
                    {
                        (int)Keys.Codes.Snapshot,
                    },
                }
            };
        }
    }
}

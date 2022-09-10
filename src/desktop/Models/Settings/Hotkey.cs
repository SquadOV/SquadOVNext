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
using Avalonia.Collections;
using DynamicData;
using DynamicData.Binding;
using Newtonsoft.Json.Linq;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using SquadOV.Constants;
using SquadOV.Models.Identity;

namespace SquadOV.Models.Settings
{

    // A wrapper around a raw hotkey to make it reactive.
    public class Hotkey: ReactiveObject, ICloneable
    {
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        private LogicalAction _action = LogicalAction.Unknown;
        public LogicalAction Action
        {
            get => _action;
            set => this.RaiseAndSetIfChanged(ref _action, value);
        }

        private ObservableCollection<int> _keys = new ObservableCollection<int>();
        public ObservableCollection<int> Keys
        {
            get => _keys;
            set => this.RaiseAndSetIfChanged(ref _keys, value);
        }

        public bool IsEnabled { get => Keys.Count > 0; }

        public Hotkey()
        {
        }

        public Dictionary<int, bool> CheckPress()
        {
            Dictionary<int, bool> state = new Dictionary<int, bool>();
            foreach (var key in _keys)
            {
                short keyState = GetAsyncKeyState(key);
                bool isPressed = (keyState & 0x8000) > 0;
                state.Add(key, isPressed);
            }

            return state;
        }

        public object Clone()
        {
            return new Hotkey()
            {
                Action = Action,
                Keys = new ObservableCollection<int>(Keys),
            };
        }
    }
}

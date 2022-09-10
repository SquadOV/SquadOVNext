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
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Avalonia.Collections;
using Avalonia.Input;
using DynamicData.Binding;
using DynamicData;
using System.Collections.ObjectModel;

namespace SquadOV.ViewModels.Settings
{
    public class HotKeyEditorViewModel: ReactiveObject
    {
        private Services.System.IHotkeyService _hotkeys = Locator.Current.GetService<Services.System.IHotkeyService>()!;
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;
        public Models.Settings.Hotkey Hotkey { get; }
        public Models.Settings.Hotkey ScratchHotkey { get; }

        private readonly ObservableAsPropertyHelper<string> _hotkeyStr;
        public string HotkeyStr { get => _hotkeyStr.Value; }

        private bool _isEditing = false;
        public bool IsEditing
        {
            get => _isEditing;
            set => this.RaiseAndSetIfChanged(ref _isEditing, value);
        }

        public HotKeyEditorViewModel(Models.Settings.Hotkey hotkey)
        {
            Hotkey = hotkey;
            ScratchHotkey = (Models.Settings.Hotkey)hotkey.Clone();
            _hotkeyStr = Observable.CombineLatest(
                this.WhenAnyValue(x => x.Loc.Culture),
                ScratchHotkey.Keys.ToObservableChangeSet().ToCollection(),
                (CultureInfo culture, IReadOnlyCollection<int> keys) =>
                {
                    return string.Join(
                        '+',
                        keys.Select(k =>
                        {
                            return Loc.Get(Constants.Keys.CodeToLocKey(Constants.Keys.VirtualToCode(k)), culture);
                        })
                    );
                }
            )
                .Select(x => x)
                .ToProperty(this, x => x.HotkeyStr);
        }

        public void StartEdit()
        {
            IsEditing = true;
            ScratchHotkey.Keys.Clear();
            _hotkeys.EnableHotkeys(false);
        }

        public void CancelEdit()
        {
            IsEditing = false;
            ScratchHotkey.Keys.Clear();
            ScratchHotkey.Keys.AddRange(Hotkey.Keys);
            _hotkeys.EnableHotkeys(true);
        }

        public void SaveEdit()
        {
            IsEditing = false;
            Hotkey.Keys.Clear();
            Hotkey.Keys.AddRange(ScratchHotkey.Keys);
            _hotkeys.EnableHotkeys(true);
        }

        public void OnKey(object? sender, KeyEventArgs e)
        {
            if (!IsEditing)
            {
                return;
            }
            var k = Constants.Keys.VirtualKeyFromKey(e.Key);
            ScratchHotkey.Keys.Add(k);
        }
    }
}

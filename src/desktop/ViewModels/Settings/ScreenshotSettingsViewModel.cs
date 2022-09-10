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
using SquadOV.Models.Settings;
using System.Collections.Generic;

namespace SquadOV.ViewModels.Settings
{
    public class ScreenshotSettingsViewModel : ReactiveObject, IRoutableViewModel
    {
        private Services.Config.IConfigService _config;
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "/screenshot";
        public HotKeyEditorViewModel Hotkey { get; }
        public ScreenshotSettingsViewModel(IScreen parent)
        {
            _config = Locator.Current.GetService<Services.Config.IConfigService>()!;
            HostScreen = parent;
            Hotkey = new HotKeyEditorViewModel(_config.Config.Hotkeys!.Screenshot!);
        }
    }
}

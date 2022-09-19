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
using System;

namespace SquadOV.Locators
{
    public class SettingsViewLocator: IViewLocator
    {
        public IViewFor? ResolveView<T>(T viewModel, string? contract = null) => viewModel switch
        {
            ViewModels.Settings.StorageSettingsViewModel context => new Views.Settings.StorageSettingsView { DataContext = context },
            ViewModels.Settings.SystemSettingsViewModel context => new Views.Settings.SystemSettingsView { DataContext = context },
            ViewModels.Settings.LanguageSettingsViewModel context => new Views.Settings.LanguageSettingsView { DataContext = context },
            ViewModels.Settings.ProfileSettingsViewModel context => new Views.Settings.ProfileSettingsView { DataContext = context },
            ViewModels.Settings.DeviceSettingsViewModel context => new Views.Settings.DeviceSettingsView { DataContext = context },
            ViewModels.Settings.ScreenshotSettingsViewModel context => new Views.Settings.ScreenshotSettingsView { DataContext = context },
            ViewModels.Settings.GameSupportSettingsViewModel context => new Views.Settings.GameSupportSettingsView { DataContext = context },
            _ => null,
        };
    }
}

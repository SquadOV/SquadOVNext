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
    public class AppViewLocator: IViewLocator
    {
        public IViewFor? ResolveView<T>(T viewModel, string? contract = null) => viewModel switch
        {
            ViewModels.HomeViewModel context => new Views.Main.HomeView { DataContext = context },
            ViewModels.SettingsViewModel context => new Views.Main.SettingsView { DataContext = context },
            ViewModels.Library.ClipLibraryViewModel context => new Views.Library.ClipLibrary { DataContext = context },
            ViewModels.Library.ScreenshotLibraryViewModel context => new Views.Library.ScreenshotLibrary { DataContext = context },
            ViewModels.Library.VodLibraryViewModel context => new Views.Library.VodLibrary { DataContext = context },
            _ => null,
        };
    }
}

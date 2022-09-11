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
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using ReactiveUI;
using Splat;
using SquadOV.Models.Settings;

namespace SquadOV.ViewModels
{
    public class MainWindowViewModel: ReactiveObject, IScreen
    {
        private Services.Config.IConfigService _config;
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;
        public bool AllowExit { get; set; } = false;
        // The Router associated with this Screen.
        // Required by the IScreen interface.
        public RoutingState Router { get; } = new RoutingState();

        public void GoHome() => Router.Navigate.Execute(new HomeViewModel(this));
        public void GoSettings() => Router.Navigate.Execute(new SettingsViewModel(this));
        public void GoVods() => Router.Navigate.Execute(new Library.VodLibraryViewModel(this));
        public void GoClips() => Router.Navigate.Execute(new Library.ClipLibraryViewModel(this));
        public void GoScreenshots() => Router.Navigate.Execute(new Library.ScreenshotLibraryViewModel(this));
        public void GoStats() => Router.Navigate.Execute(new Library.StatLibraryViewModel(this));
        public ConfigModel Config { get => _config.Config; }

        public MainWindowViewModel()
        {
            _config = Locator.Current.GetService<Services.Config.IConfigService>()!;
        }
    }
}

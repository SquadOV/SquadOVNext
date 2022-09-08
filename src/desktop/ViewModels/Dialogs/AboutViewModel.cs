//
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
using System.Reactive;
using System.Reflection;
using System.IO;
using Avalonia;
using Avalonia.Platform;
using Splat;

namespace SquadOV.ViewModels.Dialogs
{
    public class AboutViewModel : ReactiveObject
    {
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;
        public ReactiveCommand<Unit, Unit> CloseCommand { get; }
        public string SquadOvVersion
        {
            get => string.Format("{0} v{1}", new object[] { Assembly.GetExecutingAssembly().GetName().Name!, Assembly.GetExecutingAssembly().GetName().Version!.ToString() });
        }

        public AboutViewModel()
        {
            CloseCommand = ReactiveCommand.Create(() =>{});
        }

    }
}

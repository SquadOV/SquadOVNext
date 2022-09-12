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
using Avalonia.Platform;
using Avalonia;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using SquadOV.Converters;
using System.Threading;

namespace SquadOV.ViewModels.Utility
{
    public class ControlPanelViewModel: ReactiveObject
    {
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;

        private readonly ObservableAsPropertyHelper<string> _statusIcon;
        public string StatusIcon { get => _statusIcon.Value; }

        private readonly ObservableAsPropertyHelper<string> _statusText;
        public string StatusText { get => _statusText.Value; }

        // TODO
        private int _status = 0;
        public int Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }

        public ControlPanelViewModel()
        {
            // TODO:
            _statusText = this.WhenAnyValue(x => x.Loc.Culture)
                .Select(x => Loc.Get("StatusReady", x))
                .ToProperty(this, x => x.StatusText);

            _statusIcon = this.WhenAnyValue(x => x.Status)
                .Select(x =>
                {
                    var uri = new Uri("avares://SquadOV/Assets/Buttons/ready-circle.png");
                    var assets = AvaloniaLocator.Current.GetService<IAssetLoader>()!;
                    var bm = new Bitmap(assets.Open(uri));
                    return (string)Base64PictureConverter.Instance.ConvertBack(bm, typeof(string), null, Thread.CurrentThread.CurrentUICulture);
                })
                .ToProperty(this, x => x.StatusIcon);
        }
    }
}

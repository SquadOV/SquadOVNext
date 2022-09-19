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
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Splat;
using System;
using System.Drawing;
using System.IO;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using SquadOV.Converters;
using System.Threading;
using SquadOV.Extensions;
using Avalonia.Platform;
using Avalonia;
using System.Diagnostics;
using Windows.Gaming.Input;

namespace SquadOV.ViewModels.Settings
{
    public class GameSupportSettingsViewModel : ReactiveObject, IRoutableViewModel
    {
        private Services.Config.IConfigService _config = Locator.Current.GetService<Services.Config.IConfigService>()!;
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "/games/support";

        private readonly ReadOnlyObservableCollection<Models.Settings.Config.GameSupportConfig> _games;
        public ReadOnlyObservableCollection<Models.Settings.Config.GameSupportConfig> Games { get => _games; }

        public Interaction<Unit, string?> GameFinderInteraction { get; }
        public ReactiveCommand<Unit, Unit> GameFinderCommand { get; }

        public GameSupportSettingsViewModel(IScreen parent)
        {
            HostScreen = parent;
            _config.Config.Games!.Support
                .ToObservableChangeSet()
                .AsObservableList()
                .Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _games)
                .Subscribe();

            GameFinderInteraction = new Interaction<Unit, string?>();
            GameFinderCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var newExe = await GameFinderInteraction.Handle(new Unit());
                if (newExe != null)
                {
                    AddNewGameFromExecutable(newExe);
                }
            });
        }

        private void AddNewGameFromExecutable(string exe)
        {
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>()!;
            var icon = Icon.ExtractAssociatedIcon(exe);
            var bitmap = icon?.ToBitmap().ToAvaloniaBitmap() ?? new Avalonia.Media.Imaging.Bitmap(assets.Open(new Uri("avares://SquadOV/Assets/Buttons/program.png")));
            var fi = FileVersionInfo.GetVersionInfo(exe);

            var gameId = $"CUSTOM:{Path.GetFileName(exe)}";
            if (_config.Config.Games!.SupportMap.ContainsKey(gameId))
            {
                return;
            }

            _config.Config.Games!.Support.Add(new Models.Settings.Config.GameSupportConfig()
            {
                Id = gameId,
                Name = fi.ProductName ?? "Unknown",
                Executable = exe,
                Icon = (string)Base64PictureConverter.Instance.ConvertBack(bitmap, typeof(string), null, Thread.CurrentThread.CurrentUICulture),
                Enabled = true,
                Plugin = null,
            });
        }

        public void ToggleGame(string id)
        {
            if (!_config.Config.Games!.SupportMap.ContainsKey(id))
            {
                return;
            }

            _config.Config.Games!.SupportMap[id].Enabled = !_config.Config.Games!.SupportMap[id].Enabled;
        }
    }
}

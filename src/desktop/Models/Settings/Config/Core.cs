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
using System.IO;
using ReactiveUI;

namespace SquadOV.Models.Settings.Config
{
    
    public class CoreConfigModel : BaseConfigModel
    {
        private string? _vodPath;
        public string? VodPath
        {
            get => _vodPath;
            set => this.RaiseAndSetIfChanged(ref _vodPath, value);
        }

        private string? _clipPath;
        public string? ClipPath
        {
            get => _clipPath;
            set => this.RaiseAndSetIfChanged(ref _clipPath, value);
        }

        private string? _screenshotPath;
        public string? ScreenshotPath
        {
            get => _screenshotPath;
            set => this.RaiseAndSetIfChanged(ref _screenshotPath, value);
        }

        private string? _matchPath;
        public string? MatchPath
        {
            get => _matchPath;
            set => this.RaiseAndSetIfChanged(ref _matchPath, value);
        }

        private string? _logPath;
        public string? LogPath
        {
            get => _logPath;
            set => this.RaiseAndSetIfChanged(ref _logPath, value);
        }

        private string? _culture;
        public string? Culture
        {
            get => _culture;
            set => this.RaiseAndSetIfChanged(ref _culture, value);
        }

        private bool? _minimizeOnClose;
        public bool? MinimizeOnClose
        {
            get => _minimizeOnClose;
            set => this.RaiseAndSetIfChanged(ref _minimizeOnClose, value);
        }

        private bool? _minimizeToSystemTray;
        public bool? MinimizeToSystemTray
        {
            get => _minimizeToSystemTray;
            set => this.RaiseAndSetIfChanged(ref _minimizeToSystemTray, value);
        }

        public static CoreConfigModel CreateDefault(string location)
        {
            var dbPath = Path.Combine(location, "Storage");
            return new CoreConfigModel()
            {
                VodPath = Path.Combine(dbPath, "VOD"),
                ClipPath = Path.Combine(dbPath, "Clip"),
                ScreenshotPath = Path.Combine(dbPath, "Screenshot"),
                LogPath = Path.Combine(dbPath, "Log"),
                MatchPath = Path.Combine(dbPath, "Match"),
                Culture = "en",
                MinimizeOnClose = true,
                MinimizeToSystemTray = true,
            };
        }
    }
}

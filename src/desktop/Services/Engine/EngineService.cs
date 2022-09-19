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
using Splat;
using System;
using DynamicData;
using DynamicData.Binding;
using System.Reactive.Linq;
using DynamicData.Kernel;

namespace SquadOV.Services.Engine
{
    internal class EngineService: IEngineService
    {
        private LibEngine.EngineOptions _options;
        private LibEngine.Engine _engine;

        public EngineService()
        {
            var config = Locator.Current.GetService<Config.IConfigService>()!;
            _options = new LibEngine.EngineOptions()
            {
                vodPath = config.Config.Core!.VodPath!,
                clipPath = config.Config.Core!.ClipPath!,
                screenshotPath = config.Config.Core!.ScreenshotPath!,
                matchPath = config.Config.Core!.MatchPath!,
                logPath = config.Config.Core!.LogPath!,
            };
            _engine = new LibEngine.Engine(_options);

            // Connect certain reactive properties to the engine.
            // Game support - we need to add/remove game support from the engine as those config options changes.
            // Furthermore, if individual properties change, then the engine needs to be updated about that info.
            config.Config.Games!.Support
                .ToObservableChangeSet()
                .AsObservableList()
                .Connect()
                .AutoRefresh(x => x.Enabled)
                .Subscribe(OnGameSupportChange);
        }

        private void OnGameSupportChange(IChangeSet<Models.Settings.Config.GameSupportConfig> x)
        {
            var changes = x.AsList();
            foreach (var c in changes)
            {
                switch (c.Reason)
                {
                    case ListChangeReason.Add:
                        _engine.addProcessToWatch(c.Item.Current.Executable);
                        break;
                    case ListChangeReason.AddRange:
                        foreach (var cc in c.Range)
                        {
                            _engine.addProcessToWatch(cc.Executable);
                        }
                        break;
                    case ListChangeReason.Refresh:
                        if (c.Item.Previous.HasValue)
                        {
                            if (c.Item.Current.Enabled != c.Item.Previous.Value.Enabled)
                            {
                                if (c.Item.Current.Enabled)
                                {
                                    _engine.addProcessToWatch(c.Item.Current.Executable);
                                }
                                else
                                {
                                    _engine.removeProcessToWatch(c.Item.Current.Executable);
                                }
                            }
                        }
                        break;
                    case ListChangeReason.Remove:
                        _engine.removeProcessToWatch(c.Item.Current.Executable);
                        break;
                    default: throw new ApplicationException("Unsupported change operation for syncing game support to the engine.");
                }
            }
        }

        public void TakeScreenshot()
        {
        }
    }
}

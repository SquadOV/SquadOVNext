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
using System;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace SquadOV.ViewModels.Settings
{
    public class StorageSettingsViewModel : ReactiveObject, IRoutableViewModel
    {
        private Services.Config.IConfigService _config;
        public ConfigModel Config { get => _config.Config; }
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "/storage";

        public Interaction<string, Unit> ErrorMessageInteraction { get; }
        public Interaction<Unit, string?> FolderBrowseInteraction { get; }
        public ReactiveCommand<string, Unit> FolderBrowseCommand { get; }


        public StorageSettingsViewModel(IScreen parent)
        {
            _config = Locator.Current.GetService<Services.Config.IConfigService>()!;
            HostScreen = parent;

            ErrorMessageInteraction = new Interaction<string, Unit>();
            FolderBrowseInteraction = new Interaction<Unit, string?>();
            FolderBrowseCommand = ReactiveCommand.CreateFromTask<string, Unit>(async (propName) =>
            {
                while (true)
                {
                    var folder = await FolderBrowseInteraction.Handle(new Unit());
                    if (folder != null)
                    {
                        var dInfo = new DirectoryInfo(folder);
                        // Do some verification on this folder - this folder should be empty and should not be a root folder (i.e. the C:\ drive).
                        if (!Directory.EnumerateFileSystemEntries(folder).Any() && dInfo.Parent != null)
                        {
                            var prop = Config.Core!.GetType().GetProperty(propName);
                            if (prop == null)
                            {
                                throw new ArgumentException(string.Format("{0} is an invalid prop.", propName));
                            }
                            prop.SetValue(Config.Core, folder);
                        }
                        else
                        {
                            // Need to display an error message to the user letting them know they made an invalid choice.
                            await ErrorMessageInteraction.Handle(Loc.ErrorStorageFolder);
                            continue;
                        }
                    }
                    break;
                }
                return new Unit();
            });
        }
    }
}

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
using System.Collections.Generic;
using Splat;
using System.Text.RegularExpressions;

namespace SquadOV.ViewModels.Settings
{
    public class LanguageSettingsViewModel : ReactiveObject, IRoutableViewModel
    {
        private Services.Config.IConfigService _config;
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "/language";
        public List<Models.Settings.CultureChoiceModel> LanguageChoices { get; } = Models.Settings.CultureChoiceModel.LoadAll();
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;
        public LanguageSettingsViewModel(IScreen parent)
        {
            _config = Locator.Current.GetService<Services.Config.IConfigService>()!;
            HostScreen = parent;
        }

        public void ChangeCulture(string culture)
        {
            _config.Config.Core!.Culture = culture;
        }
    }
}

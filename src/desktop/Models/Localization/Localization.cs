
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
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Resources;
using System.Reactive.Linq;
using ReactiveUI;
using Splat;
using System.Threading;

namespace SquadOV.Models.Localization
{
    public class Localization: ReactiveObject
    {
        private ResourceManager _manager;
        
        private CultureInfo _culture;
        private CultureInfo Culture
        {
            get => _culture;
            set => this.RaiseAndSetIfChanged(ref _culture, value);
        }

        public Localization()
        {
            _manager = new ResourceManager("SquadOV.Resources.Resources", typeof(Localization).Assembly);
            _culture = Thread.CurrentThread.CurrentUICulture;

            var systemService = Locator.Current.GetService<Services.System.ISystemService>()!;
            systemService.CultureChange += (v) => Culture = v;
            _obsAbout = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("About", x))
                    .ToProperty(this, x => x.About);
            _obsDialogCancel = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("DialogCancel", x))
                    .ToProperty(this, x => x.DialogCancel);
            _obsDialogOk = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("DialogOk", x))
                    .ToProperty(this, x => x.DialogOk);
            _obsLanguageEnglish = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("LanguageEnglish", x))
                    .ToProperty(this, x => x.LanguageEnglish);
            _obsLanguageFrench = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("LanguageFrench", x))
                    .ToProperty(this, x => x.LanguageFrench);
            _obsLanguageSelect = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("LanguageSelect", x))
                    .ToProperty(this, x => x.LanguageSelect);
            _obsLanguageSpanish = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("LanguageSpanish", x))
                    .ToProperty(this, x => x.LanguageSpanish);
            _obsNavHome = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("NavHome", x))
                    .ToProperty(this, x => x.NavHome);
            _obsNavSettings = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("NavSettings", x))
                    .ToProperty(this, x => x.NavSettings);
            _obsNoUpdateAvailable = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("NoUpdateAvailable", x))
                    .ToProperty(this, x => x.NoUpdateAvailable);
            _obsQuitConfirm = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("QuitConfirm", x))
                    .ToProperty(this, x => x.QuitConfirm);
            _obsQuitText = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("QuitText", x))
                    .ToProperty(this, x => x.QuitText);
            _obsSettingsAbout = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsAbout", x))
                    .ToProperty(this, x => x.SettingsAbout);
            _obsSettingsApp = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsApp", x))
                    .ToProperty(this, x => x.SettingsApp);
            _obsSettingsCheckForUpdates = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsCheckForUpdates", x))
                    .ToProperty(this, x => x.SettingsCheckForUpdates);
            _obsSettingsLanguage = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsLanguage", x))
                    .ToProperty(this, x => x.SettingsLanguage);
            _obsSettingsOther = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsOther", x))
                    .ToProperty(this, x => x.SettingsOther);
            _obsSettingsStorage = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsStorage", x))
                    .ToProperty(this, x => x.SettingsStorage);
            _obsSettingsSystem = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsSystem", x))
                    .ToProperty(this, x => x.SettingsSystem);
            _obsSplashLoading = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SplashLoading", x))
                    .ToProperty(this, x => x.SplashLoading);
            _obsSystemMinimizeOnClose = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SystemMinimizeOnClose", x))
                    .ToProperty(this, x => x.SystemMinimizeOnClose);
            _obsSystemMinimizeToSysTray = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SystemMinimizeToSysTray", x))
                    .ToProperty(this, x => x.SystemMinimizeToSysTray);
            _obsUpdateCheck = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("UpdateCheck", x))
                    .ToProperty(this, x => x.UpdateCheck);
        }

        private readonly ObservableAsPropertyHelper<string> _obsAbout;
        public string About { get => _obsAbout.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsDialogCancel;
        public string DialogCancel { get => _obsDialogCancel.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsDialogOk;
        public string DialogOk { get => _obsDialogOk.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsLanguageEnglish;
        public string LanguageEnglish { get => _obsLanguageEnglish.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsLanguageFrench;
        public string LanguageFrench { get => _obsLanguageFrench.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsLanguageSelect;
        public string LanguageSelect { get => _obsLanguageSelect.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsLanguageSpanish;
        public string LanguageSpanish { get => _obsLanguageSpanish.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsNavHome;
        public string NavHome { get => _obsNavHome.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsNavSettings;
        public string NavSettings { get => _obsNavSettings.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsNoUpdateAvailable;
        public string NoUpdateAvailable { get => _obsNoUpdateAvailable.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsQuitConfirm;
        public string QuitConfirm { get => _obsQuitConfirm.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsQuitText;
        public string QuitText { get => _obsQuitText.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsAbout;
        public string SettingsAbout { get => _obsSettingsAbout.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsApp;
        public string SettingsApp { get => _obsSettingsApp.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsCheckForUpdates;
        public string SettingsCheckForUpdates { get => _obsSettingsCheckForUpdates.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsLanguage;
        public string SettingsLanguage { get => _obsSettingsLanguage.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsOther;
        public string SettingsOther { get => _obsSettingsOther.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsStorage;
        public string SettingsStorage { get => _obsSettingsStorage.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsSystem;
        public string SettingsSystem { get => _obsSettingsSystem.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSplashLoading;
        public string SplashLoading { get => _obsSplashLoading.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSystemMinimizeOnClose;
        public string SystemMinimizeOnClose { get => _obsSystemMinimizeOnClose.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSystemMinimizeToSysTray;
        public string SystemMinimizeToSysTray { get => _obsSystemMinimizeToSysTray.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsUpdateCheck;
        public string UpdateCheck { get => _obsUpdateCheck.Value; }


        public string Get(string key, CultureInfo? info = null)
        {
            return _manager.GetString(key, info) ?? "<INVALID>";
        }
    }
}
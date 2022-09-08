
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
                    .ToProperty(this, nameof(About), deferSubscription: true);
            _obsButtonBrowse = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("ButtonBrowse", x))
                    .ToProperty(this, nameof(ButtonBrowse), deferSubscription: true);
            _obsChooseIdentityRequirements = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("ChooseIdentityRequirements", x))
                    .ToProperty(this, nameof(ChooseIdentityRequirements), deferSubscription: true);
            _obsChooseProfilePicture = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("ChooseProfilePicture", x))
                    .ToProperty(this, nameof(ChooseProfilePicture), deferSubscription: true);
            _obsChooseUsername = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("ChooseUsername", x))
                    .ToProperty(this, nameof(ChooseUsername), deferSubscription: true);
            _obsCreateUserInstruction = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("CreateUserInstruction", x))
                    .ToProperty(this, nameof(CreateUserInstruction), deferSubscription: true);
            _obsDialogCancel = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("DialogCancel", x))
                    .ToProperty(this, nameof(DialogCancel), deferSubscription: true);
            _obsDialogOk = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("DialogOk", x))
                    .ToProperty(this, nameof(DialogOk), deferSubscription: true);
            _obsDialogSave = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("DialogSave", x))
                    .ToProperty(this, nameof(DialogSave), deferSubscription: true);
            _obsDragAndDropOrClick = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("DragAndDropOrClick", x))
                    .ToProperty(this, nameof(DragAndDropOrClick), deferSubscription: true);
            _obsErrorStorageFolder = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("ErrorStorageFolder", x))
                    .ToProperty(this, nameof(ErrorStorageFolder), deferSubscription: true);
            _obsLanguageEnglish = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("LanguageEnglish", x))
                    .ToProperty(this, nameof(LanguageEnglish), deferSubscription: true);
            _obsLanguageFrench = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("LanguageFrench", x))
                    .ToProperty(this, nameof(LanguageFrench), deferSubscription: true);
            _obsLanguageSelect = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("LanguageSelect", x))
                    .ToProperty(this, nameof(LanguageSelect), deferSubscription: true);
            _obsLanguageSpanish = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("LanguageSpanish", x))
                    .ToProperty(this, nameof(LanguageSpanish), deferSubscription: true);
            _obsLoadingIdentity = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("LoadingIdentity", x))
                    .ToProperty(this, nameof(LoadingIdentity), deferSubscription: true);
            _obsNavHome = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("NavHome", x))
                    .ToProperty(this, nameof(NavHome), deferSubscription: true);
            _obsNavSettings = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("NavSettings", x))
                    .ToProperty(this, nameof(NavSettings), deferSubscription: true);
            _obsNoUpdateAvailable = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("NoUpdateAvailable", x))
                    .ToProperty(this, nameof(NoUpdateAvailable), deferSubscription: true);
            _obsProfilePictureChooseBuiltIn = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("ProfilePictureChooseBuiltIn", x))
                    .ToProperty(this, nameof(ProfilePictureChooseBuiltIn), deferSubscription: true);
            _obsProfilePictureUploadOwn = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("ProfilePictureUploadOwn", x))
                    .ToProperty(this, nameof(ProfilePictureUploadOwn), deferSubscription: true);
            _obsQuitConfirm = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("QuitConfirm", x))
                    .ToProperty(this, nameof(QuitConfirm), deferSubscription: true);
            _obsQuitText = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("QuitText", x))
                    .ToProperty(this, nameof(QuitText), deferSubscription: true);
            _obsSettingsAbout = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsAbout", x))
                    .ToProperty(this, nameof(SettingsAbout), deferSubscription: true);
            _obsSettingsApp = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsApp", x))
                    .ToProperty(this, nameof(SettingsApp), deferSubscription: true);
            _obsSettingsCheckForUpdates = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsCheckForUpdates", x))
                    .ToProperty(this, nameof(SettingsCheckForUpdates), deferSubscription: true);
            _obsSettingsLanguage = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsLanguage", x))
                    .ToProperty(this, nameof(SettingsLanguage), deferSubscription: true);
            _obsSettingsOther = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsOther", x))
                    .ToProperty(this, nameof(SettingsOther), deferSubscription: true);
            _obsSettingsStorage = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsStorage", x))
                    .ToProperty(this, nameof(SettingsStorage), deferSubscription: true);
            _obsSettingsSystem = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsSystem", x))
                    .ToProperty(this, nameof(SettingsSystem), deferSubscription: true);
            _obsSplashLoading = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SplashLoading", x))
                    .ToProperty(this, nameof(SplashLoading), deferSubscription: true);
            _obsStorageClipLocation = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageClipLocation", x))
                    .ToProperty(this, nameof(StorageClipLocation), deferSubscription: true);
            _obsStorageClipLocationTooltip = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageClipLocationTooltip", x))
                    .ToProperty(this, nameof(StorageClipLocationTooltip), deferSubscription: true);
            _obsStorageLogLocation = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageLogLocation", x))
                    .ToProperty(this, nameof(StorageLogLocation), deferSubscription: true);
            _obsStorageLogLocationTooltip = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageLogLocationTooltip", x))
                    .ToProperty(this, nameof(StorageLogLocationTooltip), deferSubscription: true);
            _obsStorageMatchLocation = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageMatchLocation", x))
                    .ToProperty(this, nameof(StorageMatchLocation), deferSubscription: true);
            _obsStorageMatchLocationTooltip = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageMatchLocationTooltip", x))
                    .ToProperty(this, nameof(StorageMatchLocationTooltip), deferSubscription: true);
            _obsStorageScreenshotLocation = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageScreenshotLocation", x))
                    .ToProperty(this, nameof(StorageScreenshotLocation), deferSubscription: true);
            _obsStorageScreenshotLocationTooltip = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageScreenshotLocationTooltip", x))
                    .ToProperty(this, nameof(StorageScreenshotLocationTooltip), deferSubscription: true);
            _obsStorageVodLocation = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageVodLocation", x))
                    .ToProperty(this, nameof(StorageVodLocation), deferSubscription: true);
            _obsStorageVodLocationTooltip = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageVodLocationTooltip", x))
                    .ToProperty(this, nameof(StorageVodLocationTooltip), deferSubscription: true);
            _obsSystemMinimizeOnClose = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SystemMinimizeOnClose", x))
                    .ToProperty(this, nameof(SystemMinimizeOnClose), deferSubscription: true);
            _obsSystemMinimizeToSysTray = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SystemMinimizeToSysTray", x))
                    .ToProperty(this, nameof(SystemMinimizeToSysTray), deferSubscription: true);
            _obsUpdateCheck = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("UpdateCheck", x))
                    .ToProperty(this, nameof(UpdateCheck), deferSubscription: true);
        }

        private readonly ObservableAsPropertyHelper<string> _obsAbout;
        public string About { get => _obsAbout.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsButtonBrowse;
        public string ButtonBrowse { get => _obsButtonBrowse.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsChooseIdentityRequirements;
        public string ChooseIdentityRequirements { get => _obsChooseIdentityRequirements.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsChooseProfilePicture;
        public string ChooseProfilePicture { get => _obsChooseProfilePicture.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsChooseUsername;
        public string ChooseUsername { get => _obsChooseUsername.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsCreateUserInstruction;
        public string CreateUserInstruction { get => _obsCreateUserInstruction.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsDialogCancel;
        public string DialogCancel { get => _obsDialogCancel.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsDialogOk;
        public string DialogOk { get => _obsDialogOk.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsDialogSave;
        public string DialogSave { get => _obsDialogSave.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsDragAndDropOrClick;
        public string DragAndDropOrClick { get => _obsDragAndDropOrClick.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsErrorStorageFolder;
        public string ErrorStorageFolder { get => _obsErrorStorageFolder.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsLanguageEnglish;
        public string LanguageEnglish { get => _obsLanguageEnglish.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsLanguageFrench;
        public string LanguageFrench { get => _obsLanguageFrench.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsLanguageSelect;
        public string LanguageSelect { get => _obsLanguageSelect.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsLanguageSpanish;
        public string LanguageSpanish { get => _obsLanguageSpanish.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsLoadingIdentity;
        public string LoadingIdentity { get => _obsLoadingIdentity.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsNavHome;
        public string NavHome { get => _obsNavHome.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsNavSettings;
        public string NavSettings { get => _obsNavSettings.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsNoUpdateAvailable;
        public string NoUpdateAvailable { get => _obsNoUpdateAvailable.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsProfilePictureChooseBuiltIn;
        public string ProfilePictureChooseBuiltIn { get => _obsProfilePictureChooseBuiltIn.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsProfilePictureUploadOwn;
        public string ProfilePictureUploadOwn { get => _obsProfilePictureUploadOwn.Value; }

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

        private readonly ObservableAsPropertyHelper<string> _obsStorageClipLocation;
        public string StorageClipLocation { get => _obsStorageClipLocation.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStorageClipLocationTooltip;
        public string StorageClipLocationTooltip { get => _obsStorageClipLocationTooltip.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStorageLogLocation;
        public string StorageLogLocation { get => _obsStorageLogLocation.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStorageLogLocationTooltip;
        public string StorageLogLocationTooltip { get => _obsStorageLogLocationTooltip.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStorageMatchLocation;
        public string StorageMatchLocation { get => _obsStorageMatchLocation.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStorageMatchLocationTooltip;
        public string StorageMatchLocationTooltip { get => _obsStorageMatchLocationTooltip.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStorageScreenshotLocation;
        public string StorageScreenshotLocation { get => _obsStorageScreenshotLocation.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStorageScreenshotLocationTooltip;
        public string StorageScreenshotLocationTooltip { get => _obsStorageScreenshotLocationTooltip.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStorageVodLocation;
        public string StorageVodLocation { get => _obsStorageVodLocation.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStorageVodLocationTooltip;
        public string StorageVodLocationTooltip { get => _obsStorageVodLocationTooltip.Value; }

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
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
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Splat;
using SquadOV.ViewModels.Settings;
using System.Reactive.Disposables;

namespace SquadOV.ViewModels
{
    public class SettingsViewModel : ReactiveObject, IRoutableViewModel, IScreen
    {
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;
        public IScreen HostScreen { get; }

        public RoutingState Router { get; } = new RoutingState();
        public string UrlPathSegment { get; } = "/settings";

        public SettingsViewModel(IScreen screen)
        {
            HostScreen = screen;
            GoToProfileSettings();

            ShowAboutInteraction = new Interaction<Dialogs.AboutViewModel, Unit>();
            ShowAboutCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var store = new Dialogs.AboutViewModel();
                await ShowAboutInteraction.Handle(store);
            });

            CheckUpdatesInteraction = new Interaction<Dialogs.CheckUpdatesViewModel, Unit>();
            CheckUpdatesCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var store = new Dialogs.CheckUpdatesViewModel();
                await CheckUpdatesInteraction.Handle(store);
            });
        }

        public void GoToStorageSettings() => Router.Navigate.Execute(new StorageSettingsViewModel(this));
        public void GoToSystemSettings() => Router.Navigate.Execute(new SystemSettingsViewModel(this));
        public void GoToLanguageSettings() => Router.Navigate.Execute(new LanguageSettingsViewModel(this));
        public void GoToProfileSettings() => Router.Navigate.Execute(new ProfileSettingsViewModel(this));
        public void GoToDeviceSettings() => Router.Navigate.Execute(new DeviceSettingsViewModel(this));
        public void GoToVideoRecordSettings() => Router.Navigate.Execute(new VideoSettingsViewModel(this));
        public void GoToAudioRecordSettings() => Router.Navigate.Execute(new AudioSettingsViewModel(this));
        public void GoToClipSettings() => Router.Navigate.Execute(new ClipSettingsViewModel(this));
        public void GoToScreenshotSettings() => Router.Navigate.Execute(new ScreenshotSettingsViewModel(this));
        public void GoToOverlaySettings() => Router.Navigate.Execute(new OverlaySettingsViewModel(this));
        public void GoToGameSupportSettings() => Router.Navigate.Execute(new GameSupportSettingsViewModel(this));

        public Interaction<Dialogs.AboutViewModel, Unit> ShowAboutInteraction { get; }
        public ReactiveCommand<Unit, Unit> ShowAboutCommand { get; }

        public Interaction<Dialogs.CheckUpdatesViewModel, Unit> CheckUpdatesInteraction { get; }
        public ReactiveCommand<Unit, Unit> CheckUpdatesCommand { get; }
    }
}

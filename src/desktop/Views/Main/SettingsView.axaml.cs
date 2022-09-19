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
using System.Threading.Tasks;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using Avalonia.Media;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace SquadOV.Views.Main
{
    public partial class SettingsView : ReactiveUserControl<ViewModels.SettingsViewModel>
    {
        public SettingsView()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.WhenAnyObservable(x => x.ViewModel!.Router.CurrentViewModel)
                    .Select(x =>
                    {
                        return (x?.UrlPathSegment == "/storage") ?
                            new SolidColorBrush(Constants.Colors.SelectedLinkBackground, 1.0) :
                            new SolidColorBrush();
                    })
                    .BindTo(this, x => x.StorageSettingsButton.Background)
                    .DisposeWith(disposables);

                this.WhenAnyObservable(x => x.ViewModel!.Router.CurrentViewModel)
                    .Select(x =>
                    {
                        return (x?.UrlPathSegment == "/system") ?
                            new SolidColorBrush(Constants.Colors.SelectedLinkBackground, 1.0) :
                            new SolidColorBrush();
                    })
                    .BindTo(this, x => x.SystemSettingsButton.Background)
                    .DisposeWith(disposables);

                this.WhenAnyObservable(x => x.ViewModel!.Router.CurrentViewModel)
                    .Select(x =>
                    {
                        return (x?.UrlPathSegment == "/language") ?
                            new SolidColorBrush(Constants.Colors.SelectedLinkBackground, 1.0) :
                            new SolidColorBrush();
                    })
                    .BindTo(this, x => x.LanguageSettingsButton.Background)
                    .DisposeWith(disposables);

                this.WhenAnyObservable(x => x.ViewModel!.Router.CurrentViewModel)
                    .Select(x =>
                    {
                        return (x?.UrlPathSegment == "/profile") ?
                            new SolidColorBrush(Constants.Colors.SelectedLinkBackground, 1.0) :
                            new SolidColorBrush();
                    })
                    .BindTo(this, x => x.ProfileSettingsButton.Background)
                    .DisposeWith(disposables);

                this.WhenAnyObservable(x => x.ViewModel!.Router.CurrentViewModel)
                    .Select(x =>
                    {
                        return (x?.UrlPathSegment == "/devices") ?
                            new SolidColorBrush(Constants.Colors.SelectedLinkBackground, 1.0) :
                            new SolidColorBrush();
                    })
                    .BindTo(this, x => x.DeviceSettingsButton.Background)
                    .DisposeWith(disposables);

                this.WhenAnyObservable(x => x.ViewModel!.Router.CurrentViewModel)
                    .Select(x =>
                    {
                        return (x?.UrlPathSegment == "/video") ?
                            new SolidColorBrush(Constants.Colors.SelectedLinkBackground, 1.0) :
                            new SolidColorBrush();
                    })
                    .BindTo(this, x => x.VideoRecordSettingsButton.Background)
                    .DisposeWith(disposables);

                this.WhenAnyObservable(x => x.ViewModel!.Router.CurrentViewModel)
                    .Select(x =>
                    {
                        return (x?.UrlPathSegment == "/audio") ?
                            new SolidColorBrush(Constants.Colors.SelectedLinkBackground, 1.0) :
                            new SolidColorBrush();
                    })
                    .BindTo(this, x => x.AudioRecordSettingsButton.Background)
                    .DisposeWith(disposables);

                this.WhenAnyObservable(x => x.ViewModel!.Router.CurrentViewModel)
                    .Select(x =>
                    {
                        return (x?.UrlPathSegment == "/clip") ?
                            new SolidColorBrush(Constants.Colors.SelectedLinkBackground, 1.0) :
                            new SolidColorBrush();
                    })
                    .BindTo(this, x => x.ClipSettingsButton.Background)
                    .DisposeWith(disposables);

                this.WhenAnyObservable(x => x.ViewModel!.Router.CurrentViewModel)
                    .Select(x =>
                    {
                        return (x?.UrlPathSegment == "/screenshot") ?
                            new SolidColorBrush(Constants.Colors.SelectedLinkBackground, 1.0) :
                            new SolidColorBrush();
                    })
                    .BindTo(this, x => x.ScreenshotSettingsButton.Background)
                    .DisposeWith(disposables);

                this.WhenAnyObservable(x => x.ViewModel!.Router.CurrentViewModel)
                    .Select(x =>
                    {
                        return (x?.UrlPathSegment == "/overlay") ?
                            new SolidColorBrush(Constants.Colors.SelectedLinkBackground, 1.0) :
                            new SolidColorBrush();
                    })
                    .BindTo(this, x => x.OverlaySettingsButton.Background)
                    .DisposeWith(disposables);

                this.WhenAnyObservable(x => x.ViewModel!.Router.CurrentViewModel)
                    .Select(x =>
                    {
                        return (x?.UrlPathSegment == "/games/support") ?
                            new SolidColorBrush(Constants.Colors.SelectedLinkBackground, 1.0) :
                            new SolidColorBrush();
                    })
                    .BindTo(this, x => x.GamesSupportSettingsButton.Background)
                    .DisposeWith(disposables);

                ViewModel!.ShowAboutInteraction.RegisterHandler(ShowAboutDialog).DisposeWith(disposables);
                ViewModel!.CheckUpdatesInteraction.RegisterHandler(ShowCheckUpdatesDialog).DisposeWith(disposables);
            });
        }

        private async Task ShowAboutDialog(InteractionContext<ViewModels.Dialogs.AboutViewModel, Unit> interaction)
        {
            var dialog = new Dialogs.AboutDialog()
            {
                DataContext = interaction.Input,
            };
            await dialog.ShowDialog(((IClassicDesktopStyleApplicationLifetime)Application.Current!.ApplicationLifetime!).MainWindow);
            interaction.SetOutput(new Unit());
        }

        private async Task ShowCheckUpdatesDialog(InteractionContext<ViewModels.Dialogs.CheckUpdatesViewModel, Unit> interaction)
        {
            var dialog = new Dialogs.CheckForUpdatesDialog()
            {
                DataContext = interaction.Input,
            };
            await dialog.ShowDialog(((IClassicDesktopStyleApplicationLifetime)Application.Current!.ApplicationLifetime!).MainWindow);
            interaction.SetOutput(new Unit());
        }
    }
}

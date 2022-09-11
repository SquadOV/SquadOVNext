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
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Media;
using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace SquadOV.Views
{
    public partial class MainWindow : ReactiveWindow<ViewModels.MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.WhenAnyObservable(x => x.ViewModel!.Router.CurrentViewModel)
                    .Select(x =>
                    {
                        return (x?.UrlPathSegment == "/settings") ?
                            new SolidColorBrush(Constants.Colors.SelectedLinkBackground, 1.0) :
                            new SolidColorBrush();
                    })
                    .BindTo(this, x => x.SettingsButton.Background)
                    .DisposeWith(disposables);

                this.WhenAnyObservable(x => x.ViewModel!.Router.CurrentViewModel)
                    .Select(x =>
                    {
                        return (x?.UrlPathSegment == "/library/vods") ?
                            new SolidColorBrush(Constants.Colors.SelectedLinkBackground, 1.0) :
                            new SolidColorBrush();
                    })
                    .BindTo(this, x => x.VodButton.Background)
                    .DisposeWith(disposables);

                this.WhenAnyObservable(x => x.ViewModel!.Router.CurrentViewModel)
                    .Select(x =>
                    {
                        return (x?.UrlPathSegment == "/library/clips") ?
                            new SolidColorBrush(Constants.Colors.SelectedLinkBackground, 1.0) :
                            new SolidColorBrush();
                    })
                    .BindTo(this, x => x.ClipButton.Background)
                    .DisposeWith(disposables);

                this.WhenAnyObservable(x => x.ViewModel!.Router.CurrentViewModel)
                    .Select(x =>
                    {
                        return (x?.UrlPathSegment == "/library/screenshots") ?
                            new SolidColorBrush(Constants.Colors.SelectedLinkBackground, 1.0) :
                            new SolidColorBrush();
                    })
                    .BindTo(this, x => x.ScreenshotButton.Background)
                    .DisposeWith(disposables);

                this.WhenAnyObservable(x => x.ViewModel!.Router.CurrentViewModel)
                    .Select(x =>
                    {
                        return (x?.UrlPathSegment == "/library/stats") ?
                            new SolidColorBrush(Constants.Colors.SelectedLinkBackground, 1.0) :
                            new SolidColorBrush();
                    })
                    .BindTo(this, x => x.StatButton.Background)
                    .DisposeWith(disposables);

                this.WhenAnyValue(x => x.WindowState)
                    .Subscribe(st =>
                    {
                        if (st == Avalonia.Controls.WindowState.Minimized)
                        {
                            ShowInTaskbar = !ViewModel?.Config.Core?.MinimizeToSystemTray ?? true;
                        }
                        else
                        {
                            ShowInTaskbar = true;
                        }
                    });
            });

            Closing += (s, e) =>
            {
                // Whether or not the user has agreed to closing.
                if (ViewModel!.AllowExit)
                {
                    return;
                }

                // Minimize on system close will force the window to minimize instead of close.
                if (ViewModel?.Config.Core?.MinimizeOnClose ?? false)
                {
                    e.Cancel = true;
                    WindowState = Avalonia.Controls.WindowState.Minimized;
                    return;
                }

                // At this point we know the user wants to close-close the main window.
                // Force the window to show again if it's minimized - this will make this dialog more noticeable.
                if (WindowState == Avalonia.Controls.WindowState.Minimized)
                {
                    WindowState = Avalonia.Controls.WindowState.Normal;
                }

                e.Cancel = true;
                ShowExitDialog();
            };
        }

        private async void ShowExitDialog()
        {
            var dialog = new Dialogs.ConfirmQuitDialog();
            dialog.DataContext = new ViewModels.Dialogs.ConfirmQuitViewModel();

            var result = await dialog.ShowDialog<bool>(this);
            if (result)
            {
                ViewModel!.AllowExit = true;
                Close();
            }
        }
    }
}
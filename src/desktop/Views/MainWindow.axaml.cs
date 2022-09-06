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
            });

            Closing += (s, e) =>
            {
                if (ViewModel!.AllowExit)
                {
                    return;
                }

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
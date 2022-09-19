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
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using ReactiveUI;
using SquadOV.Views.Dialogs;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using System.Reactive.Disposables;

namespace SquadOV.Views.Settings
{
    public partial class GameSupportSettingsView : ReactiveUserControl<ViewModels.Settings.GameSupportSettingsViewModel>
    {
        public GameSupportSettingsView()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                ViewModel!.GameFinderInteraction.RegisterHandler(ShowGameFinder).DisposeWith(disposables);
            });
        }

        private async Task ShowGameFinder(InteractionContext<Unit, string?> interaction)
        {
            var dialog = new OpenFileDialog()
            {
                AllowMultiple = false,
                Filters = new List<FileDialogFilter>()
                {
                    new FileDialogFilter()
                    {
                        Extensions = new List<string>()
                        {
                            "exe",
                        },
                        Name = "Executables",
                    }
                },
            };

            var files = await dialog.ShowAsync(((IClassicDesktopStyleApplicationLifetime)Application.Current!.ApplicationLifetime!).MainWindow);
            if (files == null || files.Length == 0)
            {
                interaction.SetOutput(null);
            }
            else
            {
                interaction.SetOutput(files[0]);
            }
        }
    }
}

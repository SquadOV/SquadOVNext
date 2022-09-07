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
using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using Avalonia.Controls.ApplicationLifetimes;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace SquadOV.Views.Settings
{
    public partial class StorageSettingsView : ReactiveUserControl<ViewModels.Settings.StorageSettingsViewModel>
    {
        public StorageSettingsView()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                ViewModel!.FolderBrowseInteraction.RegisterHandler(ShowFolderBrowser).DisposeWith(disposables);
                ViewModel!.ErrorMessageInteraction.RegisterHandler(ShowMessageBox).DisposeWith(disposables);
            });
        }

        private async Task ShowFolderBrowser(InteractionContext<Unit, string?> interaction)
        {
            var dialog = new OpenFolderDialog();
            var folder = await dialog.ShowAsync(((IClassicDesktopStyleApplicationLifetime)Application.Current!.ApplicationLifetime!).MainWindow);
            interaction.SetOutput(folder);
        }

        private async Task ShowMessageBox(InteractionContext<string, Unit> interaction)
        {
            var dialog = new Dialogs.MessageBox()
            {
                DataContext = new ViewModels.Dialogs.MessageBoxViewModel(interaction.Input),
            };
            await dialog.ShowDialog(((IClassicDesktopStyleApplicationLifetime)Application.Current!.ApplicationLifetime!).MainWindow);
            interaction.SetOutput(new Unit());
        }
    }
}

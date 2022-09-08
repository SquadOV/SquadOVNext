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
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Mixins;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System;
using System.Reactive;
using System.Threading.Tasks;

namespace SquadOV.Views.Dialogs
{
    public partial class CreateEditUserIdentity : ReactiveWindow<ViewModels.Dialogs.CreateUserIdentityViewModel>
    {
        public CreateEditUserIdentity()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                ViewModel!.CloseCommand.Subscribe(v =>
                {
                    Close(ViewModel!.User);
                }).DisposeWith(disposables);

                ViewModel!.ChooseProfilePictureInteraction.RegisterHandler(ShowProfilePictureChooser).DisposeWith(disposables);
            });
        }

        private async Task ShowProfilePictureChooser(InteractionContext<string, string?> interaction)
        {
            var dialog = new ProfilePictureChooser()
            {
                DataContext = new ViewModels.Dialogs.ProfilePictureChooserViewModel(interaction.Input),
            };
            var picture = await dialog.ShowDialog<string>(((IClassicDesktopStyleApplicationLifetime)Application.Current!.ApplicationLifetime!).MainWindow);
            interaction.SetOutput(picture);
        }
    }
}

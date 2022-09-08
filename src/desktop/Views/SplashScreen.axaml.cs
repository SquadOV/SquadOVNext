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
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using SquadOV.ViewModels;
using System.Reactive;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace SquadOV.Views
{
    public partial class SplashScreen : ReactiveWindow<SplashScreenViewModel>
    {
        public SplashScreen()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                ViewModel!.CreateUserIdentityInteraction.RegisterHandler(ShowUserIdentityCreationDialog).DisposeWith(disposables);
            });
        }

        private async Task ShowUserIdentityCreationDialog(InteractionContext<Unit, Models.Identity.UserIdentity> interaction)
        {
            var vm = new ViewModels.Dialogs.CreateUserIdentityViewModel();
            var dialog = new Dialogs.CreateEditUserIdentity()
            {
                DataContext = vm,
            };
            await dialog.ShowDialog(((IClassicDesktopStyleApplicationLifetime)Application.Current!.ApplicationLifetime!).MainWindow);
            interaction.SetOutput(vm.User);
        }
    }
}

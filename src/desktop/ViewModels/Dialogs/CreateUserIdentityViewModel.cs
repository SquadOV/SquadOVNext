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
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;
using System.IO;
using Avalonia;
using Avalonia.Platform;
using Splat;

namespace SquadOV.ViewModels.Dialogs
{
    public class CreateUserIdentityViewModel : ReactiveObject
    {
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;

        public Interaction<string, string?> ChooseProfilePictureInteraction { get; }
        public ReactiveCommand<Unit, Unit> ChooseProfilePictureCommand { get; }

        public ReactiveCommand<Unit, Unit> CloseCommand { get; }

        private Models.Identity.UserIdentity _user;
        public Models.Identity.UserIdentity User
        {
            get => _user;
            set => this.RaiseAndSetIfChanged(ref _user, value);
        }

        public CreateUserIdentityViewModel(Models.Identity.UserIdentity? user = null)
        {
            _user = (Models.Identity.UserIdentity)(user ?? new Models.Identity.UserIdentity()).Clone();
            if (!_user.IsValid)
            {
                _user.GenerateDefaultRsaKey();
            }
            CloseCommand = ReactiveCommand.Create(() => { });
            ChooseProfilePictureInteraction = new Interaction<string, string?>();
            ChooseProfilePictureCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var newPic = await ChooseProfilePictureInteraction.Handle(User.Picture);
                if (newPic != null)
                {
                    User.Picture = newPic;
                }
            });
        }
    }
}

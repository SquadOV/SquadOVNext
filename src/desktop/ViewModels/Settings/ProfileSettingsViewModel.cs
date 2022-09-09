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
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SquadOV.ViewModels.Settings
{
    public class ProfileSettingsViewModel: ReactiveObject, IRoutableViewModel
    {
        private readonly Services.Identity.IIdentityService _identity = Locator.Current.GetService<Services.Identity.IIdentityService>()!;
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;
        public IScreen HostScreen { get; }

        public string UrlPathSegment { get; } = "/profile";
        public Models.Identity.UserIdentity CurrentUser { get => _identity.User; }
        public Models.Identity.UserIdentity User { get; }
        public Interaction<string, string?> ChooseProfilePictureInteraction { get; }
        public ReactiveCommand<Unit, Unit> ChooseProfilePictureCommand { get; }

        private readonly ObservableAsPropertyHelper<bool> _hasChanges;
        public bool HasChanges { get => _hasChanges.Value; }

        public ProfileSettingsViewModel(IScreen screen)
        {
            User = (Models.Identity.UserIdentity)_identity.User.Clone();
            HostScreen = screen;
            ChooseProfilePictureInteraction = new Interaction<string, string?>();
            ChooseProfilePictureCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var newPic = await ChooseProfilePictureInteraction.Handle(User.Picture);
                if (newPic != null)
                {
                    User.Picture = newPic;
                }
            });

            _hasChanges = this.WhenAnyValue(
                x => x.User.Username,
                x => x.User.Tag,
                x => x.User.Picture,
                x => x.CurrentUser.Username,
                x => x.CurrentUser.Tag,
                x => x.CurrentUser.Picture
            )
                .Select(x => User != CurrentUser && User.IsValid)
                .ToProperty(this, x => x.HasChanges);
        }

        public void Save()
        {
            CurrentUser.Username = User.Username;
            CurrentUser.Tag = User.Tag;
            CurrentUser.Picture = User.Picture;
        }
    }
}

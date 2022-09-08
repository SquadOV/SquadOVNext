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
using System.Linq;
using System.Reactive.Linq;
using Splat;

namespace SquadOV.ViewModels
{
    public class HomeViewModel: ReactiveObject, IRoutableViewModel
    {
        private Services.Identity.IIdentityService _identity;
        public IScreen HostScreen { get; }
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;

        public string UrlPathSegment { get; } = "/";
        public Models.Identity.UserIdentity User
        {
            get => _identity.User;
        }

        private readonly ObservableAsPropertyHelper<string> _welcomeMessage;
        public string WelcomeMessage { get => _welcomeMessage.Value; }

        public HomeViewModel(IScreen screen)
        {
            _identity = Locator.Current.GetService<Services.Identity.IIdentityService>();
            HostScreen = screen;

            _welcomeMessage = this.WhenAnyValue(x => x.Loc.WelcomeMessage, x => x.User.Username)
                .Select(((string fmt, string username) inp) => string.Format(inp.fmt ?? "{0}", inp.username))
                .ToProperty(this, x => x.WelcomeMessage);
        }
    }
}

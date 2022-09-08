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
using System.Text;
using System.Threading.Tasks;

namespace SquadOV.ViewModels.Dialogs
{
    public class ProfilePictureChooserViewModel: ReactiveObject
    {
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;
        public List<string> PictureChoices { get; } = new List<string>(Models.Assets.ProfilePictures.BuiltInProfilePictures);

        private string _picture;
        public string Picture
        {
            get => _picture;
            private set => this.RaiseAndSetIfChanged(ref _picture, value);
        }

        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }

        public ProfilePictureChooserViewModel(string currentPicture)
        {
            _picture = currentPicture;
            CancelCommand = ReactiveCommand.Create(() => { });
            SaveCommand = ReactiveCommand.Create(() => { });
        }

        public void ChangePictureFromHexEncoded(string data)
        {
            Picture = data;
        }

        public void ChangePictureFromFilename(string filename)
        {
        }
    }
}

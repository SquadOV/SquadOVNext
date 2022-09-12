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
using Avalonia.Media.Imaging;
using ReactiveUI;
using Splat;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Platform;
using Avalonia;
using SquadOV.Converters;
using System.Threading;

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

        public Interaction<Unit, string?> SelectProfilePictureFilesystemInteraction;
        public ReactiveCommand<Unit, Unit> SelectProfilePictureFilesystemCommand { get; }

        public ProfilePictureChooserViewModel(string currentPicture)
        {
            _picture = currentPicture;
            CancelCommand = ReactiveCommand.Create(() => { });
            SaveCommand = ReactiveCommand.Create(() => { });
            SelectProfilePictureFilesystemInteraction = new Interaction<Unit, string?>();
            SelectProfilePictureFilesystemCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var newPic = await SelectProfilePictureFilesystemInteraction.Handle(new Unit());
                if (newPic != null)
                {
                    ChangePictureFromFilename(newPic);
                }
            });
        }

        public void ChangePictureFromHexEncoded(string data)
        {
            Picture = data;
        }

        public void ChangePictureFromFilename(string filename)
        {
            if (!File.Exists(filename))
            {
                return;
            }

            var bm = new Bitmap(filename);
            var desiredSize = bm.PixelSize;

            if (desiredSize.Width > 128)
            {
                desiredSize = new PixelSize(
                    128,
                    (int)(128 / desiredSize.AspectRatio)
                );
            }

            if (desiredSize.Height > 128)
            {
                desiredSize = new PixelSize(
                    (int)(128 * desiredSize.AspectRatio),
                    128
                );
            }

            if (desiredSize != bm.PixelSize)
            {
                bm = bm.CreateScaledBitmap(desiredSize);
            }
            
            Picture = (string)Base64PictureConverter.Instance.ConvertBack(bm, typeof(string), null, Thread.CurrentThread.CurrentUICulture);
        }
    }
}

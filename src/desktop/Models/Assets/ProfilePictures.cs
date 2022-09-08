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
using Avalonia.Platform;
using Avalonia;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SquadOV.Models.Assets
{
    internal class ProfilePictures
    {
        static public List<string> BuiltInProfilePictures
        {
            get
            {
                var assets = AvaloniaLocator.Current.GetService<IAssetLoader>()!;
                return assets.GetAssets(new Uri("avares://SquadOV/Assets/Profiles"), null)
                    .Select(x =>
                    {
                        var bm = new Bitmap(assets.Open(x));
                        var byteStream = new MemoryStream();
                        bm.Save(byteStream);
                        return Convert.ToBase64String(byteStream.ToArray());
                    })
                    .ToList();
            }
        }
    }
}

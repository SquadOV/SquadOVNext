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
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Platform;
using Avalonia;

namespace SquadOV.Converters
{
    public class Base64PictureConverter: IValueConverter
    {
        public static readonly Base64PictureConverter Instance = new();
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value != null && value is string hexData && !string.IsNullOrEmpty(hexData) && parameter is string height && targetType.IsAssignableFrom(typeof(Bitmap)))
            {
                var byteData = System.Convert.FromBase64String(hexData);
                var byteStream = new MemoryStream(byteData);
                return Bitmap.DecodeToHeight(byteStream, System.Convert.ToInt32(height));
            }

            // A bit of a relic of it being used for the profile picture...eh?
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>()!;
            var bm = new Bitmap(assets.Open(new Uri("avares://SquadOV/Assets/Profiles/Cranks-1.png")));
            return bm;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
            {
                throw new NotSupportedException();
            }

            if (value.GetType().IsAssignableFrom(typeof(Bitmap)) && value is Bitmap bitmap)
            {
                var byteStream = new MemoryStream();
                bitmap.Save(byteStream);
                return System.Convert.ToBase64String(byteStream.ToArray());
            }

            throw new NotSupportedException();
        }
    }
}

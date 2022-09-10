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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Splat;
using System.ComponentModel;
using Tomlyn;

namespace SquadOV.Services.Identity
{
    internal class IdentityService: IIdentityService
    {
        private Config.IConfigService _config = Locator.Current.GetService<Config.IConfigService>()!;

        public string IdentityFolder
        {
            get => Path.Combine(_config.UserFolder, "Identity");
        }

        private string UserIdentityPath
        {
            get => Path.Combine(IdentityFolder, "user.toml");
        }

        private string DeviceIdentityPath
        {
            get => Path.Combine(IdentityFolder, "device.toml");
        }

        Models.Identity.UserIdentity _user;
        public Models.Identity.UserIdentity User
        {
            get => _user;
            set
            {
                _user = value;
                OnUserIdentityChange(null, new PropertyChangedEventArgs(null));
            }
        }

        public Models.Identity.DeviceIdentity Device { get; }

        public IdentityService()
        {
            if (!Directory.Exists(IdentityFolder))
            {
                // We don't want users mucking around with the contents of this folder.
                DirectoryInfo info = Directory.CreateDirectory(IdentityFolder);
                info.Attributes |= FileAttributes.Hidden;
            }

            // There's two components to the identity.
            //  1) The "user" identity.
            //  2) The "device" identity.
            // The "user" identity will be some easy-to-remember/share token that must be globally unique. Attached to the user's identity is a cryptographic public/private key.
            // The "device" identity will be randomly generated UUID the first time the user starts the app.
            // Each "user" should be allowed to have multiple "devices" attached to that identity. If the user wants to add a device to an already existing identity, they must share the *PRIVATE KEY* to that new device.
            //
            // Load the stored user's identity - if any.
            if (File.Exists(UserIdentityPath))
            {
                _user = Toml.ToModel<Models.Identity.UserIdentity>(File.ReadAllText(UserIdentityPath), UserIdentityPath, Constants.Toml.TomlOptions);
            }
            else
            {
                _user = new Models.Identity.UserIdentity();
            }
            User.PropertyChanged += OnUserIdentityChange;

            // Load the device's identity - automatically generate if it doesn't already exist.
            if (File.Exists(DeviceIdentityPath))
            {
                Device = Toml.ToModel<Models.Identity.DeviceIdentity>(File.ReadAllText(DeviceIdentityPath), DeviceIdentityPath, Constants.Toml.TomlOptions);
            }
            else
            {
                Device = new Models.Identity.DeviceIdentity();
            }

            if (!Device.IsValid)
            {
                Device = Models.Identity.DeviceIdentity.Generate();
                OnDeviceIdentityChange(null, new PropertyChangedEventArgs(null));
            }

            Device.PropertyChanged += OnDeviceIdentityChange;
        }

        private void OnDeviceIdentityChange(object? sender, PropertyChangedEventArgs args)
        {
            var toml = Toml.FromModel(Device, Constants.Toml.TomlOptions);
            File.WriteAllText(DeviceIdentityPath, toml);
        }

        private void OnUserIdentityChange(object? sender, PropertyChangedEventArgs args)
        {
            var toml = Toml.FromModel(User, Constants.Toml.TomlOptions);
            File.WriteAllText(UserIdentityPath, toml);
        }
    }
}

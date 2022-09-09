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
using System.Reactive.Linq;
using SquadOV.Models.Settings;
using System.Collections.Generic;
using System.Globalization;
using SquadOV.Services.System;

namespace SquadOV.ViewModels.Settings
{
    public class DeviceSettingsViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly Services.Identity.IIdentityService _identity = Locator.Current.GetService<Services.Identity.IIdentityService>()!;
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "/devices";

        public Models.Identity.DeviceIdentity CurrentDevice { get => _identity.Device; }
        public Models.Identity.DeviceIdentity Device { get; }
        private readonly ObservableAsPropertyHelper<bool> _hasChanges;
        public bool HasChanges { get => _hasChanges.Value; }

        // TODO: This needs to pull from the underlying engine service instead once we go networked.
        public List<DeviceStatusViewModel> DeviceStatus { get; }

        public DeviceSettingsViewModel(IScreen parent)
        {
            HostScreen = parent;
            Device = (Models.Identity.DeviceIdentity)_identity.Device.Clone();
            _hasChanges = this.WhenAnyValue(
                x => x.Device.FriendlyName,
                x => x.CurrentDevice.FriendlyName
            )
                .Select(x => Device != CurrentDevice && Device.IsValid)
                .ToProperty(this, x => x.HasChanges);

            DeviceStatus = new List<DeviceStatusViewModel>()
            {
                new DeviceStatusViewModel(CurrentDevice)
            };
        }

        public void Save()
        {
            CurrentDevice.FriendlyName = Device.FriendlyName;
        }
    }
}

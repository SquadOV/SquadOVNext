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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadOV.ViewModels.Settings
{
    public class DeviceStatusViewModel: ReactiveObject
    {
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;
        public Models.Identity.DeviceIdentity Device { get; }

        private readonly ObservableAsPropertyHelper<string> _deviceTypeAsset;
        public string DeviceTypeAsset { get => _deviceTypeAsset.Value; }

        public DeviceStatusViewModel(Models.Identity.DeviceIdentity device)
        {
            Device = device;
            _deviceTypeAsset = this.WhenAnyValue(x => x.Device.Type)
                .Select(x => string.Format("avares://SquadOV/Assets/Devices/{0}.png", x))
                .ToProperty(this, x => x.DeviceTypeAsset);
        }
    }
}

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
using HarfBuzzSharp;
using ReactiveUI;
using SkiaSharp;
using Splat;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Tomlyn;

namespace SquadOV.Models.Identity
{
    // Device identity should never change once the app loads.
    public class DeviceIdentity: ReactiveObject, ICloneable, IEquatable<DeviceIdentity>
    {
        private Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;
        public enum DeviceType
        {
            DesktopWindows
        }

        private Guid _id;
        [IgnoreDataMember]
        public Guid Id { get => _id; private set => this.RaiseAndSetIfChanged(ref _id, value); }
        public string StrId
        {
            get => Id.ToString();
            set
            {
                Id = Guid.Parse(value);
            }
        }

        private string _friendlyName = "";
        public string FriendlyName
        {
            get => _friendlyName;
            set => this.RaiseAndSetIfChanged(ref _friendlyName, value);
        }

        private DeviceType _type = DeviceType.DesktopWindows;
        public DeviceType Type
        {
            get => _type;
            set => this.RaiseAndSetIfChanged(ref _type, value);
        }

        [IgnoreDataMember]
        public bool IsValid { get => !Id.Equals(Guid.Empty); }

        private readonly ObservableAsPropertyHelper<string> _deviceType;

        [IgnoreDataMember]
        public string DeviceTypeStr { get => _deviceType.Value; }
        public DeviceIdentity()
        {
            _deviceType = this.WhenAnyValue(x => x.Loc.Culture)
                .Select(x => Loc.Get(Type.ToString(), x))
                .ToProperty(this, x => x.DeviceTypeStr);
        }
        public static DeviceIdentity Generate()
        {
            return new DeviceIdentity()
            {
                Id = Guid.NewGuid(),
                FriendlyName = "Device",
                Type = DeviceType.DesktopWindows
            };
        }

        public static TomlModelOptions TomlOptions
        {
            get
            {
                var options = new TomlModelOptions()
                {
                    IgnoreMissingProperties = true,
                    ConvertToToml = (input) =>
                    {
                        if (input.GetType() == typeof(DeviceType))
                        {
                            return input.ToString();
                        }
                        return null;
                    },
                    ConvertToModel = (input, type) =>
                    {
                        if (type != typeof(DeviceType))
                        {
                            return null;
                        }

                        return Enum.Parse(typeof(DeviceType), (string)input);
                    },
                };
                return options;
            }
        }

        public object Clone()
        {
            return new DeviceIdentity()
            {
                StrId = StrId,
                FriendlyName = FriendlyName,
                Type = Type,
            };
        }

        public override bool Equals(object? obj) => this.Equals(obj as DeviceIdentity);
        public bool Equals(DeviceIdentity? other)
        {
            if (other == null)
            {
                return false;
            }

            return StrId == other.StrId
                && FriendlyName == other.FriendlyName
                && Type == other.Type;
        }

        public static bool operator ==(DeviceIdentity? a, DeviceIdentity? b)
        {
            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(DeviceIdentity? a, DeviceIdentity? b) => !(a == b);
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

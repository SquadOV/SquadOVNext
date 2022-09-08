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
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SquadOV.Models.Identity
{
    // Device identity should never change once the app loads.
    public class DeviceIdentity
    {
        [IgnoreDataMember]
        public Guid Id { get; private set; }
        public string StrId
        {
            get => Id.ToString();
            set
            {
                Id = Guid.Parse(value);
            }
        }

        public bool IsValid { get => !Id.Equals(Guid.Empty); }

        public static DeviceIdentity Generate()
        {
            return new DeviceIdentity()
            {
                Id = Guid.NewGuid(),
            };
        }
    }
}

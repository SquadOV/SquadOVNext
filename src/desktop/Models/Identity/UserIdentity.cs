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
using System.Reactive.Linq;
using ReactiveUI;
using System.Security.Cryptography;

namespace SquadOV.Models.Identity
{
    public class UserIdentity: ReactiveObject, ICloneable, IEquatable<UserIdentity>
    {
        private string _username = "";
        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        private string _tag = "";
        public string Tag
        {
            get => _tag;
            set => this.RaiseAndSetIfChanged(ref _tag, value);
        }

        // Hex encoded image.
        private string _picture = "";
        public string Picture
        {
            get => _picture;
            set => this.RaiseAndSetIfChanged(ref _picture, value);
        }

        private string _publicKey = "";
        public string PublicKey
        {
            get => _publicKey;
            set => this.RaiseAndSetIfChanged(ref _publicKey, value);
        }

        private string _privateKey = "";
        public string PrivateKey
        {
            get => _privateKey;
            set => this.RaiseAndSetIfChanged(ref _privateKey, value);
        }

        private readonly ObservableAsPropertyHelper<bool> _isValid;
        public bool IsValid { get => _isValid.Value; }
        public UserIdentity()
        {
            _isValid = this
                .WhenAnyValue(x => x.Username, x => x.Tag, x => x.PublicKey, x => x.PrivateKey)
                .Select(((string username, string tag, string publicKey, string privateKey) inp) => !string.IsNullOrEmpty(inp.username) &&
                    !string.IsNullOrEmpty(inp.tag) &&
                    inp.tag.Length >= 4 &&
                    !string.IsNullOrEmpty(inp.publicKey) &&
                    !string.IsNullOrEmpty(inp.privateKey))
                .ToProperty(this, x => x.IsValid);
        }

        public void GenerateDefaultRsaKey()
        {
            var key = RSA.Create(4096);
            PrivateKey = Convert.ToBase64String(key.ExportRSAPrivateKey());
            PublicKey = Convert.ToBase64String(key.ExportRSAPublicKey());
        }

        public object Clone()
        {
            return new UserIdentity()
            {
                Username = Username,
                Tag = Tag,
                Picture = Picture,
                PublicKey = PublicKey,
                PrivateKey = PrivateKey,
            };
        }

        public override bool Equals(object? obj) => this.Equals(obj as UserIdentity);
        public bool Equals(UserIdentity? other)
        {
            if (other == null)
            {
                return false;
            }

            return Username == other.Username
                && Tag == other.Tag
                && Picture == other.Picture
                && PublicKey == other.PublicKey
                && PrivateKey == other.PrivateKey;
        }

        public static bool operator==(UserIdentity? a, UserIdentity? b)
        {
            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(UserIdentity? a, UserIdentity? b) => !(a == b);
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

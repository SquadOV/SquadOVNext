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
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Splat;
using ReactiveUI;

namespace SquadOV.Services.System
{
    internal class SystemService : ISystemService
    {
        private Config.IConfigService _config;
        public event CultureChangeDelegate? CultureChange;

        public SystemService()
        {
            _config = Locator.Current.GetService<Config.IConfigService>();
            this.WhenAnyValue(x => x._config.Config.Core!.Culture).Subscribe(_ => OnCultureChange());

            // Everything needs to get called once to load up the config.
            OnCultureChange();
        }

        private void OnCultureChange()
        {
            // Culture setting - handle user's choice of localization.
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(_config.Config.Core!.Culture!);
            CultureChange?.Invoke(Thread.CurrentThread.CurrentUICulture);
        }
    }
}

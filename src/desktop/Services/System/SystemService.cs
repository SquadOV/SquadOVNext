using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Splat;

namespace SquadOV.Services.System
{
    internal class SystemService : ISystemService
    {
        Config.IConfigService _config;

        public SystemService()
        {
            _config = Locator.Current.GetService<Config.IConfigService>();
            _config.Config.PropertyChanged += OnConfigChange;
            OnConfigChange(null, new PropertyChangedEventArgs(null));
        }

        private void OnConfigChange(object? sender, PropertyChangedEventArgs args)
        {
            // Culture setting - handle user's choice of localization.
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(_config.Config.Core!.Culture!);
        }
    }
}

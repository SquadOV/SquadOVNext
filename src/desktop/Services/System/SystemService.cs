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

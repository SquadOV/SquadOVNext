using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadOV.ViewModels
{
    public class UnderConstructionViewModel
    {
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;
    }
}

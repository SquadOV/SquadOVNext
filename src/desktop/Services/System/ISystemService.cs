using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadOV.Services.System
{
    public delegate void CultureChangeDelegate(CultureInfo culture);

    internal interface ISystemService
    {
        event CultureChangeDelegate? CultureChange;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadOV.Services.Config
{
    internal interface IConfigService
    {
        // Check whether or not the configuration was just created (i.e. it didn't used to exist).
        bool IsNewlyCreated { get; }

        // Some path to a folder on the user's local machine that stores basic files for the app - this path can not be changed by the user.
        string UserFolder { get; }

        // Path to the file in the UserFolder that holds the configuration.
        string ConfigFile { get; }

        // The actual config data that we loaded.
        Models.ConfigModel Config { get; }
    }
}

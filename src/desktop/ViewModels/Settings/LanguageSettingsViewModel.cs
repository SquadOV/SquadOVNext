using ReactiveUI;
using System.Collections.Generic;
using Splat;
using System.Text.RegularExpressions;

namespace SquadOV.ViewModels.Settings
{
    public class LanguageSettingsViewModel : ReactiveObject, IRoutableViewModel
    {
        private Services.Config.IConfigService _config;
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "/language";
        public List<Models.Settings.CultureChoiceModel> LanguageChoices { get; } = Models.Settings.CultureChoiceModel.LoadAll();
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;
        public LanguageSettingsViewModel(IScreen parent)
        {
            _config = Locator.Current.GetService<Services.Config.IConfigService>()!;
            HostScreen = parent;
        }

        public void ChangeCulture(string culture)
        {
            _config.Config.Core!.Culture = culture;
        }
    }
}

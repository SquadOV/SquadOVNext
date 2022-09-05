using ReactiveUI;

namespace SquadOV.ViewModels
{
    public class SettingsViewModel : ReactiveObject, IRoutableViewModel
    {
        public IScreen HostScreen { get; }

        public string UrlPathSegment { get; } = "/settings";

        public SettingsViewModel(IScreen screen) => HostScreen = screen;
    }
}

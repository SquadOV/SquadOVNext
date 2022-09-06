using ReactiveUI;

namespace SquadOV.ViewModels.Settings
{
    public class StorageSettingsViewModel : ReactiveObject, IRoutableViewModel
    {
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "/storage";
        public StorageSettingsViewModel(IScreen parent)
        {
            HostScreen = parent;
        }
    }
}

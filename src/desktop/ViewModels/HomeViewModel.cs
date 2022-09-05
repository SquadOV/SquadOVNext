using ReactiveUI;

namespace SquadOV.ViewModels
{
    public class HomeViewModel: ReactiveObject, IRoutableViewModel
    {
        public IScreen HostScreen { get; }

        public string UrlPathSegment { get; } = "/";

        public HomeViewModel(IScreen screen) => HostScreen = screen;
    }
}

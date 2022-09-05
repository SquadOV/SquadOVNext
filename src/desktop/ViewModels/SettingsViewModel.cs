using ReactiveUI;
using Splat;
using SquadOV.ViewModels.Settings;
using System.Reactive.Disposables;

namespace SquadOV.ViewModels
{
    public class SettingsViewModel : ReactiveObject, IRoutableViewModel, IScreen
    {
        public IScreen HostScreen { get; }

        public RoutingState Router { get; } = new RoutingState();
        public string UrlPathSegment { get; } = "/settings";

        public SettingsViewModel(IScreen screen)
        {
            HostScreen = screen;
            GoToStorageSettings();
        }

        public void GoToStorageSettings() => Router.Navigate.Execute(new StorageSettingsViewModel(this));
        public void GoToSystemSettings() => Router.Navigate.Execute(new SystemSettingsViewModel(this));

        public void CheckForUpdates()
        {

        }

        public void ShowAbout()
        {

        }
    }
}

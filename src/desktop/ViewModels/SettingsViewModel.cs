using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Splat;
using SquadOV.ViewModels.Settings;
using System.Reactive.Disposables;

namespace SquadOV.ViewModels
{
    public class SettingsViewModel : ReactiveObject, IRoutableViewModel, IScreen
    {
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;
        public IScreen HostScreen { get; }

        public RoutingState Router { get; } = new RoutingState();
        public string UrlPathSegment { get; } = "/settings";

        public SettingsViewModel(IScreen screen)
        {
            HostScreen = screen;
            GoToStorageSettings();

            ShowAboutInteraction = new Interaction<Dialogs.AboutViewModel, Unit>();
            ShowAboutCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var store = new Dialogs.AboutViewModel();
                await ShowAboutInteraction.Handle(store);
            });

            CheckUpdatesInteraction = new Interaction<Dialogs.CheckUpdatesViewModel, Unit>();
            CheckUpdatesCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var store = new Dialogs.CheckUpdatesViewModel();
                await CheckUpdatesInteraction.Handle(store);
            });
        }

        public void GoToStorageSettings() => Router.Navigate.Execute(new StorageSettingsViewModel(this));
        public void GoToSystemSettings() => Router.Navigate.Execute(new SystemSettingsViewModel(this));
        public void GoToLanguageSettings() => Router.Navigate.Execute(new LanguageSettingsViewModel(this));

        public void CheckForUpdates()
        {

        }

        public Interaction<Dialogs.AboutViewModel, Unit> ShowAboutInteraction { get; }
        public ReactiveCommand<Unit, Unit> ShowAboutCommand { get; }

        public Interaction<Dialogs.CheckUpdatesViewModel, Unit> CheckUpdatesInteraction { get; }
        public ReactiveCommand<Unit, Unit> CheckUpdatesCommand { get; }
    }
}

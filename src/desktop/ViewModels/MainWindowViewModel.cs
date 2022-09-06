using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using ReactiveUI;
using Splat;

namespace SquadOV.ViewModels
{
    public class MainWindowViewModel: ReactiveObject, IScreen
    {
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;
        public bool AllowExit { get; set; } = false;
        // The Router associated with this Screen.
        // Required by the IScreen interface.
        public RoutingState Router { get; } = new RoutingState();

        public void GoHome() => Router.Navigate.Execute(new HomeViewModel(this));
        public void GoSettings() => Router.Navigate.Execute(new SettingsViewModel(this));

        public MainWindowViewModel()
        {
            GoHome();
        }
    }
}

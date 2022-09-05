using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace SquadOV.ViewModels
{
    public class MainWindowViewModel: ReactiveObject, IScreen
    {
        // The Router associated with this Screen.
        // Required by the IScreen interface.
        public RoutingState Router { get; } = new RoutingState();

        public void GoHome() => Router.Navigate.Execute(new HomeViewModel(this));
        public void GoSettings() => Router.Navigate.Execute(new SettingsViewModel(this));

        public ReactiveCommand<Unit, Unit> GoBack => Router.NavigateBack;

        public MainWindowViewModel()
        {
            Router.Navigate.Execute(new HomeViewModel(this));
        }
    }
}

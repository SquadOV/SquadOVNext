using ReactiveUI;
using System;

namespace SquadOV
{
    public class AppViewLocator: IViewLocator
    {
        public IViewFor? ResolveView<T>(T viewModel, string? contract = null) => viewModel switch
        {
            ViewModels.HomeViewModel context => new Views.HomeView { DataContext = context },
            ViewModels.SettingsViewModel context => new Views.SettingsView { DataContext = context },
            _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
        };
    }
}

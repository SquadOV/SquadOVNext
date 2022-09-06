using ReactiveUI;
using System;

namespace SquadOV.Locators
{
    public class AppViewLocator: IViewLocator
    {
        public IViewFor? ResolveView<T>(T viewModel, string? contract = null) => viewModel switch
        {
            ViewModels.HomeViewModel context => new Views.Main.HomeView { DataContext = context },
            ViewModels.SettingsViewModel context => new Views.Main.SettingsView { DataContext = context },
            _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
        };
    }
}

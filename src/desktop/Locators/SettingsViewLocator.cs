using ReactiveUI;
using System;

namespace SquadOV.Locators
{
    public class SettingsViewLocator: IViewLocator
    {
        public IViewFor? ResolveView<T>(T viewModel, string? contract = null) => viewModel switch
        {
            ViewModels.Settings.StorageSettingsViewModel context => new Views.Settings.StorageSettingsView { DataContext = context },
            ViewModels.Settings.SystemSettingsViewModel context => new Views.Settings.SystemSettingsView { DataContext = context },
            _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
        };
    }
}

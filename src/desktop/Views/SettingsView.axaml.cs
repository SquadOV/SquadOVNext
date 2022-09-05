using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace SquadOV.Views
{
    public partial class SettingsView : ReactiveUserControl<ViewModels.SettingsViewModel>
    {
        public SettingsView()
        {
            InitializeComponent();
        }
    }
}

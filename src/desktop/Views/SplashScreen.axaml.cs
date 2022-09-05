using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using SquadOV.ViewModels;

namespace SquadOV.Views
{
    public partial class SplashScreen : ReactiveWindow<SplashScreenViewModel>
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

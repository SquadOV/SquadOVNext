using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace SquadOV.Views.Main
{
    public partial class HomeView : ReactiveUserControl<ViewModels.HomeViewModel>
    {
        public HomeView()
        {
            InitializeComponent();
        }
    }
}

using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace SquadOV.Views
{
    public partial class HomeView : ReactiveUserControl<ViewModels.HomeViewModel>
    {
        public HomeView()
        {
            InitializeComponent();
        }
    }
}

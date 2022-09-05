using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using SquadOV.ViewModels;

namespace SquadOV.Views
{
    public partial class SetupWindow : ReactiveWindow<SetupWindowViewModel>
    {
        public SetupWindow()
        {
            InitializeComponent();
        }
    }
}

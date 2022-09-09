using Avalonia.Controls;

namespace SquadOV.Views
{
    public partial class UnderConstruction : UserControl
    {
        public UnderConstruction()
        {
            DataContext = new ViewModels.UnderConstructionViewModel();
            InitializeComponent();
        }
    }
}

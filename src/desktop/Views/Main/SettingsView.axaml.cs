using System.Threading.Tasks;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using Avalonia.Media;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace SquadOV.Views.Main
{
    public partial class SettingsView : ReactiveUserControl<ViewModels.SettingsViewModel>
    {
        public SettingsView()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.WhenAnyObservable(x => x.ViewModel!.Router.CurrentViewModel)
                    .Select(x =>
                    {
                        return (x?.UrlPathSegment == "/storage") ?
                            new SolidColorBrush(Constants.Colors.SelectedLinkBackground, 1.0) :
                            new SolidColorBrush();
                    })
                    .BindTo(this, x => x.StorageSettingsButton.Background)
                    .DisposeWith(disposables);

                this.WhenAnyObservable(x => x.ViewModel!.Router.CurrentViewModel)
                    .Select(x =>
                    {
                        return (x?.UrlPathSegment == "/system") ?
                            new SolidColorBrush(Constants.Colors.SelectedLinkBackground, 1.0) :
                            new SolidColorBrush();
                    })
                    .BindTo(this, x => x.SystemSettingsButton.Background)
                    .DisposeWith(disposables);

                ViewModel!.ShowAboutInteraction.RegisterHandler(ShowAboutDialog).DisposeWith(disposables);
            });
        }

        private async Task ShowAboutDialog(InteractionContext<ViewModels.Dialogs.AboutViewModel, Unit> interaction)
        {
            var dialog = new Dialogs.AboutDialog()
            {
                DataContext = interaction.Input,
            };
            await dialog.ShowDialog(((IClassicDesktopStyleApplicationLifetime)Application.Current!.ApplicationLifetime!).MainWindow);
            interaction.SetOutput(new Unit());
        }
    }
}

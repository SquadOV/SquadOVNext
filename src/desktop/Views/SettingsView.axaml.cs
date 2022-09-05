using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using Avalonia.Media;

namespace SquadOV.Views
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
            });
        }
    }
}

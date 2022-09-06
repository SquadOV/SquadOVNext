using Avalonia.ReactiveUI;
using ReactiveUI;
using System;
using System.Reactive.Disposables;

namespace SquadOV.Views.Dialogs
{
    public partial class AboutDialog : ReactiveWindow<ViewModels.Dialogs.AboutViewModel>
    {
        public AboutDialog()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                ViewModel!.CloseCommand.Subscribe(v =>
                {
                    Close();
                }).DisposeWith(disposables);
            });
        }
    }
}

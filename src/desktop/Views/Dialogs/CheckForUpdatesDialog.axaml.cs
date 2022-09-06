using Avalonia.ReactiveUI;
using ReactiveUI;
using System;
using System.Reactive.Disposables;

namespace SquadOV.Views.Dialogs
{
    public partial class CheckForUpdatesDialog : ReactiveWindow<ViewModels.Dialogs.CheckUpdatesViewModel>
    {
        public CheckForUpdatesDialog()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                ViewModel!.CancelCommand.Subscribe(v =>
                {
                    Close();
                }).DisposeWith(disposables);
            });
        }
    }
}

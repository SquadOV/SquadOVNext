using Avalonia.ReactiveUI;
using ReactiveUI;
using System;
using System.Reactive.Disposables;

namespace SquadOV.Views.Dialogs
{
    public partial class ConfirmQuitDialog : ReactiveWindow<ViewModels.Dialogs.ConfirmQuitViewModel>
    {
        public ConfirmQuitDialog()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                ViewModel!.ConfirmCommand.Subscribe(v =>
                {
                    Close(v);
                }).DisposeWith(disposables);
            });
        }
    }
}

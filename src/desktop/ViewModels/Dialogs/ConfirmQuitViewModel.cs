using ReactiveUI;
using System.Reactive;

namespace SquadOV.ViewModels.Dialogs
{
    public class ConfirmQuitViewModel: ReactiveObject
    {
        public ReactiveCommand<bool, bool> ConfirmCommand { get; }
        public ConfirmQuitViewModel()
        {
            ConfirmCommand = ReactiveCommand.Create<bool, bool>(v =>
            {
                return v;
            });
        }
    }
}

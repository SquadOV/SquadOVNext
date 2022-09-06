using ReactiveUI;
using Splat;
using System.Reactive;

namespace SquadOV.ViewModels.Dialogs
{
    public class ConfirmQuitViewModel: ReactiveObject
    {
        public Models.Localization.Localization Loc { get; } = Locator.Current.GetService<Models.Localization.Localization>()!;
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

using ReactiveUI;
using System;
using System.Reactive;
using System.Reflection;
using System.IO;
using Avalonia;
using Avalonia.Platform;

namespace SquadOV.ViewModels.Dialogs
{
    public class AboutViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> CloseCommand { get; }
        public string SquadOvVersion
        {
            get => string.Format("{0} v{1}", new object[] { Assembly.GetExecutingAssembly().GetName().Name!, Assembly.GetExecutingAssembly().GetName().Version!.ToString() });
        }

        public AboutViewModel()
        {
            CloseCommand = ReactiveCommand.Create(() =>{});
        }

    }
}

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace SquadOV.ViewModels
{
    public class AppViewModel
    {
        public AppViewModel()
        {
            ExitCommand = ReactiveCommand.Create(() =>
            {
                if (Application.Current!.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
                {
                    lifetime.Shutdown();
                }
            });

            FocusCommand = ReactiveCommand.Create(() =>
            {
                if (Application.Current!.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
                {
                    lifetime.MainWindow.WindowState = Avalonia.Controls.WindowState.Normal;
                    lifetime.MainWindow.Show();
                    lifetime.MainWindow.Focus();
                }
            });
        }

        public ReactiveCommand<Unit, Unit> ExitCommand { get; }
        public ReactiveCommand<Unit, Unit> FocusCommand { get; }
    }
}

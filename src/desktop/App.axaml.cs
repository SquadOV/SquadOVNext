//
//  Copyright (C) 2022 Michael Bao
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using System.Reactive.Disposables;

using Splat;
using SquadOV.ViewModels;

namespace SquadOV
{
    public partial class App : Application
    {
        public App()
        {
            DataContext = new AppViewModel();
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindow = new Views.MainWindow()
                {
                    DataContext = new MainWindowViewModel(),
                };

                var splashScreen = new Views.SplashScreen()
                {
                    ViewModel = new SplashScreenViewModel(),
                };

                // After the splash screen has loaded stuff that we consider necessary to run the app - check if there's any setup items that need to be done for the user.
                // Only after all the above is done do we want to actually want to show the main window and proceed with normal operating behavior.
                splashScreen.ViewModel.LoadingFinished += delegate ()
                {
                    desktop.MainWindow = mainWindow;
                    desktop.MainWindow.Show();
                    splashScreen.Close();
                };

                desktop.MainWindow = splashScreen;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
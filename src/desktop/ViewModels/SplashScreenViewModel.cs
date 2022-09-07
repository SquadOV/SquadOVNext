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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using SquadOV.Models.Localization;
using Splat;

namespace SquadOV.ViewModels
{
    public delegate void LoadingFinishedHandler();
    public class SplashScreenViewModel : ReactiveObject, IActivatableViewModel
    {
        public Localization Loc { get; } = Locator.Current.GetService<Localization>()!;
        public ViewModelActivator Activator { get; }
        public event LoadingFinishedHandler? LoadingFinished;

        private string _loadingMessage;
        public string LoadingMessage
        {
            get => _loadingMessage;
            set => this.RaiseAndSetIfChanged(ref _loadingMessage, value);
        }

        public SplashScreenViewModel()
        {
            Activator = new ViewModelActivator();
            _loadingMessage = Loc.SplashLoading;
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                StartLoading();
            });
        }
        
        async void StartLoading()
        {
            await Task.Delay(2000);
            OnLoadingFinished();
        }

        protected void OnLoadingFinished()
        {
            if (LoadingFinished != null)
            {
                LoadingFinished.Invoke();
            }
        }
    }
}

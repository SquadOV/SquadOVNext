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
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using SquadOV.Models.Localization;
using Splat;
using Avalonia.Threading;
using System.Reactive;

namespace SquadOV.ViewModels
{
    public delegate void LoadingFinishedHandler();
    public class SplashScreenViewModel : ReactiveObject, IActivatableViewModel
    {
        public ViewModelActivator Activator { get; }
        public event LoadingFinishedHandler? LoadingFinished;

        public Interaction<Unit, Models.Identity.UserIdentity> CreateUserIdentityInteraction { get; }

        private string _loadingMessage = "...";
        public string LoadingMessage
        {
            get => _loadingMessage;
            set => this.RaiseAndSetIfChanged(ref _loadingMessage, value);
        }

        public SplashScreenViewModel()
        {
            Activator = new ViewModelActivator();
            CreateUserIdentityInteraction = new Interaction<Unit, Models.Identity.UserIdentity>();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                Task.Run(() => StartLoading());
            });
        }
        
        async void StartLoading()
        {
            // First load up everything for localization since that's needed for us to start informing users what we're doing
            // The config service needs to be loaded first since everything will rely on the config somewhat.
            Locator.CurrentMutable.RegisterConstant(new Services.Config.ConfigService(), typeof(Services.Config.IConfigService));
            // Dealing with foundational stuff related to how the app runs (e.g. "system settings").
            Locator.CurrentMutable.RegisterConstant(new Services.System.SystemService(), typeof(Services.System.ISystemService));
            // Reactive localization so we can change this at runtime without usings restarting.
            Locator.CurrentMutable.RegisterConstant(new Localization());

            // Now that localization is finished loading we can start telling users what's happening!
            var loc = Locator.Current.GetService<Localization>()!;

            // Now that everything should be loaded and setup - get the engine up running.
            // The engine service is primarily reponsible for most of the behind the scenes work the app will do - recording, etc.
            LoadingMessage = loc.SplashLoading;
            Locator.CurrentMutable.RegisterConstant(new Services.Engine.EngineService(), typeof(Services.Engine.IEngineService));

            // Load user's identity and verify it.
            LoadingMessage = loc.LoadingIdentity;
            Locator.CurrentMutable.RegisterConstant(new Services.Identity.IdentityService(), typeof(Services.Identity.IIdentityService));

            var iden = Locator.Current.GetService<Services.Identity.IIdentityService>()!;
            if(!iden.User.IsValid)
            {
                iden.User = await Dispatcher.UIThread.InvokeAsync(async () => await CreateUserIdentityInteraction.Handle(new Unit()));
                // TODO: Make sure we can actually use this identity on the P2P network.
            }

            OnLoadingFinished();
        }

        protected void OnLoadingFinished()
        {
            Dispatcher.UIThread.Post(() =>
            {
                if (LoadingFinished != null)
                {
                    LoadingFinished.Invoke();
                }
            });
        }
    }
}

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
    public delegate void LoadingFinishedHandler(bool needsSetup);
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
            OnLoadingFinished(false);
        }

        protected void OnLoadingFinished(bool needsSetup)
        {
            if (LoadingFinished != null)
            {
                LoadingFinished.Invoke(needsSetup);
            }
        }
    }
}

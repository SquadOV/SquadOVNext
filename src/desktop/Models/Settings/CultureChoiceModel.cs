using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.IO;
using Newtonsoft.Json;
using ReactiveUI;
using Splat;
using System.Threading;

namespace SquadOV.Models.Settings
{
    [JsonObject(MemberSerialization.OptOut)]
    public class CultureChoiceModel: ReactiveObject
    {
        private Localization.Localization Loc { get; } = Locator.Current.GetService<Localization.Localization>()!;
        private Services.System.ISystemService _system;

        private string _native = "";
        public string Native
        {
            get => _native;
            set => this.RaiseAndSetIfChanged(ref _native, value);
        }

        private string _localized = "";
        [JsonIgnore]
        public string Localized
        {
            get => _localized;
            set => this.RaiseAndSetIfChanged(ref _localized, value);
        }

        private string _localizedRef = "";
        public string LocalizedReference {
            get => _localizedRef;
            set
            {
                this.RaiseAndSetIfChanged(ref _localizedRef, value);
                OnCultureChange(null);
            }
        }

        private string _icon = "";
        public string Icon
        {
            get => _icon;
            set
            {
                this.RaiseAndSetIfChanged(ref _icon, value);
                LoadBitmap();
            }
        }

        private string _culture = "";
        public string Culture
        {
            get => _culture;
            set
            {
                this.RaiseAndSetIfChanged(ref _culture, value);
                OnCultureChange(null);
            }
        }

        private bool _isActive = false;
        public bool IsActive
        {
            get => _isActive;
            set => this.RaiseAndSetIfChanged(ref _isActive, value);
        }

        IBitmap? _bitmap = null;
        public IBitmap? IconBitmap
        {
            get => _bitmap;
            set => this.RaiseAndSetIfChanged(ref _bitmap, value);
        }

        CultureChoiceModel()
        {
            _system = Locator.Current.GetService<Services.System.ISystemService>();
            _system.CultureChange += OnCultureChange;
            OnCultureChange(null);
        }

        private void OnCultureChange(CultureInfo? info)
        {
            var currentInfo = info ?? Thread.CurrentThread.CurrentUICulture;
            Localized = Loc.Get(LocalizedReference, currentInfo);
            IsActive = currentInfo.TwoLetterISOLanguageName == Culture;
        }

        private void LoadBitmap()
        {
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>()!;
            IconBitmap = new Bitmap(assets.Open(new Uri(Icon)));
        }

        public static List<CultureChoiceModel> LoadAll()
        {
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>()!;
            var fs = new StreamReader(assets.Open(new Uri("avares://SquadOV/Assets/Data/languages.json")));
            return JsonConvert.DeserializeObject<List<CultureChoiceModel>>(fs.ReadToEnd())!;
        }
    }
}

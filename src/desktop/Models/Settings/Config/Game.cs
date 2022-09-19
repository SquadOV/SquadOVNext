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
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using DynamicData.Binding;


namespace SquadOV.Models.Settings.Config
{
    public class GameSupportConfig: ReactiveObject
    {
        private string _id = "";
        public string Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }

        private string _name = "";
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private string _executable = "";
        public string Executable
        {
            get => _executable;
            set => this.RaiseAndSetIfChanged(ref _executable, value);
        }

        private string _icon = "";
        public string Icon
        {
            get => _icon;
            set => this.RaiseAndSetIfChanged(ref _icon, value);
        }

        private bool _enabled = false;
        public bool Enabled
        {
            get => _enabled;
            set => this.RaiseAndSetIfChanged(ref _enabled, value);
        }

        private string? _plugin = null;
        public string? Plugin
        {
            get => _plugin;
            set => this.RaiseAndSetIfChanged(ref _plugin, value);
        }
    }

    public class GameConfigModel : BaseConfigModel
    {
        private ObservableAsPropertyHelper<Dictionary<string, GameSupportConfig>>? _supportMap = null;

        private ObservableCollection<GameSupportConfig> _support = new ObservableCollection<GameSupportConfig>();
        public ObservableCollection<GameSupportConfig> Support
        {
            get => _support;
            set
            {
                this.RaiseAndSetIfChanged(ref _support, value);
                ConnectToSupport();
            }
        }

        [IgnoreDataMember]
        public Dictionary<string, GameSupportConfig> SupportMap
        {
            get => _supportMap?.Value ?? new Dictionary<string, GameSupportConfig>();
        }

        private void ConnectToSupport()
        {
            var dynamic = Support.ToObservableChangeSet()
                .AsObservableList()
                .Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Publish();

            _supportMap = dynamic.ToCollection()
                .Select((IReadOnlyCollection<GameSupportConfig> arr) =>
                {
                    var ret = new Dictionary<string, GameSupportConfig>();
                    foreach (var x in arr)
                    {
                        ret.Add(x.Id, x);
                    }
                    return ret;
                })
                .ToProperty(this, x => x.SupportMap);

            dynamic.Subscribe(x =>
            {
                this.RaisePropertyChanged("Support");
            });
            dynamic.Connect();
        }

        public GameConfigModel()
        {
            ConnectToSupport();
        }

        public static GameConfigModel CreateDefault()
        {
            return new GameConfigModel()
            {
                Support = new ObservableCollection<GameSupportConfig>(),
            };
        }
    }
}

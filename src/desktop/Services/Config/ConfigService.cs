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
using System.ComponentModel;
using System.IO;
using Tomlyn;

namespace SquadOV.Services.Config
{
    // Provides an easy way to get/modify any USER-FACING setting.
    internal class ConfigService: IConfigService
    {
        private Models.Settings.ConfigModel _model;

        public ConfigService()
        {
            // We should use LocalAppData since our view of the user is that they are a single machine and not some networked entity.
            if (!Directory.Exists(UserFolder))
            {
                Directory.CreateDirectory(UserFolder);
            }

            var defaultModel = Models.Settings.ConfigModel.CreateDefault(UserFolder);
            _isNewlyCreated = !File.Exists(ConfigFile);
            if (IsNewlyCreated)
            {
                _model = defaultModel;
            }
            else
            {
                _model = Toml.ToModel<Models.Settings.ConfigModel>(File.ReadAllText(ConfigFile));
            }

            _model.FillInMissing(defaultModel);
            _model.PropertyChanged += OnModelChange;
            _model.Core!.PropertyChanged += OnModelChange;
            OnModelChange(null, new PropertyChangedEventArgs(""));
        }

        private bool _isNewlyCreated;
        public bool IsNewlyCreated
        {
            get => _isNewlyCreated;
        }

        public string UserFolder
        {
            get => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SquadOVNext");
        }

        public string ConfigFile
        {
            get => Path.Combine(UserFolder, "config.toml");
        }

        public Models.Settings.ConfigModel Config
        {
            get => _model;
        }

        private void OnModelChange(object? sender, PropertyChangedEventArgs args)
        {
            // Save the model to disk.
            var toml = Toml.FromModel(this.Config);
            File.WriteAllText(ConfigFile, toml);
        }
    }
}

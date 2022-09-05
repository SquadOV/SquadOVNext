﻿using ReactiveUI;

namespace SquadOV.ViewModels.Settings
{
    public class SystemSettingsViewModel : ReactiveObject, IRoutableViewModel
    {
        public IScreen HostScreen { get; }
        public string UrlPathSegment { get; } = "/system";
        public SystemSettingsViewModel(SettingsViewModel parent)
        {
            HostScreen = parent;
        }
    }
}

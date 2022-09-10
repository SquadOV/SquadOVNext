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
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SquadOV.Services.System
{
    public class HotkeyService: IHotkeyService
    {
        private Config.IConfigService _config;

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        private Timer _timer;
        private bool _enabled = true;

        private Dictionary<Constants.LogicalAction, HotkeyDelegate> _actions = new Dictionary<Constants.LogicalAction, HotkeyDelegate>();
        private Dictionary<Constants.LogicalAction, Dictionary<int, bool>> _lastHotkeyState = new Dictionary<Constants.LogicalAction, Dictionary<int, bool>>();

        public HotkeyService()
        {
            _config = Locator.Current.GetService<Config.IConfigService>();

            var engine = Locator.Current.GetService<Engine.IEngineService>();
            // Register all the hotkey delegates - generally this should just be calling various functions on various services.
            _actions.Add(Constants.LogicalAction.Screenshot, engine.TakeScreenshot);

            // Running the hotkey checker at 60fps seems more than reasonable.
            _timer = new Timer(16.0);
            _timer.Elapsed += TickHotkeys;
            _timer.AutoReset = false;
            _timer.Start();
        }

        public void EnableHotkeys(bool v)
        {
            _enabled = v;
        }

        private void TickHotkeys(object? sender, ElapsedEventArgs args)
        {
            if (_enabled)
            {
                foreach (var hk in _config.Config.Hotkeys!.AllHotkeys)
                {
                    if (hk is null)
                    {
                        continue;
                    }

                    if (hk.IsEnabled)
                    {
                        Dictionary<int, bool>? lastState = null;
                        _lastHotkeyState.TryGetValue(hk.Action, out lastState);
                        var currentState = hk.CheckPress();

                        // For a "press" to be valid, it must satisfy two conditions:
                        //  1) All the buttons must be pressed
                        //  2) There must be a change in state between the last time we checked and the most recent press.
                        bool shouldFire = currentState.All(v => v.Value) && (lastState is null || lastState.Any(v => !v.Value));
                        if (shouldFire)
                        {
                            HotkeyDelegate? dels;
                            if (_actions.TryGetValue(hk.Action, out dels))
                            {
                                dels!();
                            }
                        }

                        _lastHotkeyState[hk.Action] = currentState;
                    }
                    else
                    {
                        _lastHotkeyState.Remove(hk.Action);
                    }
                }
            }

            _timer.Start();
        }
    }
}

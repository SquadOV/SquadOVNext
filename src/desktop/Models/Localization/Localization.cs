
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
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Resources;
using System.Reactive.Linq;
using ReactiveUI;
using Splat;
using System.Threading;

namespace SquadOV.Models.Localization
{
    public class Localization: ReactiveObject
    {
        private ResourceManager _manager;
        
        private CultureInfo _culture;
        public CultureInfo Culture
        {
            get => _culture;
            set => this.RaiseAndSetIfChanged(ref _culture, value);
        }

        public Localization()
        {
            _manager = new ResourceManager("SquadOV.Resources.Resources", typeof(Localization).Assembly);
            _culture = Thread.CurrentThread.CurrentUICulture;

            var systemService = Locator.Current.GetService<Services.System.ISystemService>()!;
            systemService.CultureChange += (v) => Culture = v;
            _obsAbout = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("About", x))
                    .ToProperty(this, nameof(About), deferSubscription: true);
            _obsButtonBrowse = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("ButtonBrowse", x))
                    .ToProperty(this, nameof(ButtonBrowse), deferSubscription: true);
            _obsChooseIdentityRequirements = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("ChooseIdentityRequirements", x))
                    .ToProperty(this, nameof(ChooseIdentityRequirements), deferSubscription: true);
            _obsChooseProfilePicture = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("ChooseProfilePicture", x))
                    .ToProperty(this, nameof(ChooseProfilePicture), deferSubscription: true);
            _obsChooseUsername = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("ChooseUsername", x))
                    .ToProperty(this, nameof(ChooseUsername), deferSubscription: true);
            _obsClips = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("Clips", x))
                    .ToProperty(this, nameof(Clips), deferSubscription: true);
            _obsControlPanel = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("ControlPanel", x))
                    .ToProperty(this, nameof(ControlPanel), deferSubscription: true);
            _obsCreateUserInstruction = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("CreateUserInstruction", x))
                    .ToProperty(this, nameof(CreateUserInstruction), deferSubscription: true);
            _obsDesktopWindows = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("DesktopWindows", x))
                    .ToProperty(this, nameof(DesktopWindows), deferSubscription: true);
            _obsDeviceFriendly = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("DeviceFriendly", x))
                    .ToProperty(this, nameof(DeviceFriendly), deferSubscription: true);
            _obsDeviceId = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("DeviceId", x))
                    .ToProperty(this, nameof(DeviceId), deferSubscription: true);
            _obsDeviceType = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("DeviceType", x))
                    .ToProperty(this, nameof(DeviceType), deferSubscription: true);
            _obsDialogCancel = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("DialogCancel", x))
                    .ToProperty(this, nameof(DialogCancel), deferSubscription: true);
            _obsDialogEdit = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("DialogEdit", x))
                    .ToProperty(this, nameof(DialogEdit), deferSubscription: true);
            _obsDialogOk = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("DialogOk", x))
                    .ToProperty(this, nameof(DialogOk), deferSubscription: true);
            _obsDialogSave = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("DialogSave", x))
                    .ToProperty(this, nameof(DialogSave), deferSubscription: true);
            _obsDragAndDropOrClick = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("DragAndDropOrClick", x))
                    .ToProperty(this, nameof(DragAndDropOrClick), deferSubscription: true);
            _obsErrorStorageFolder = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("ErrorStorageFolder", x))
                    .ToProperty(this, nameof(ErrorStorageFolder), deferSubscription: true);
            _obsKey0 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("Key0", x))
                    .ToProperty(this, nameof(Key0), deferSubscription: true);
            _obsKey1 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("Key1", x))
                    .ToProperty(this, nameof(Key1), deferSubscription: true);
            _obsKey2 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("Key2", x))
                    .ToProperty(this, nameof(Key2), deferSubscription: true);
            _obsKey3 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("Key3", x))
                    .ToProperty(this, nameof(Key3), deferSubscription: true);
            _obsKey4 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("Key4", x))
                    .ToProperty(this, nameof(Key4), deferSubscription: true);
            _obsKey5 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("Key5", x))
                    .ToProperty(this, nameof(Key5), deferSubscription: true);
            _obsKey6 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("Key6", x))
                    .ToProperty(this, nameof(Key6), deferSubscription: true);
            _obsKey7 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("Key7", x))
                    .ToProperty(this, nameof(Key7), deferSubscription: true);
            _obsKey8 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("Key8", x))
                    .ToProperty(this, nameof(Key8), deferSubscription: true);
            _obsKey9 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("Key9", x))
                    .ToProperty(this, nameof(Key9), deferSubscription: true);
            _obsKeyA = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyA", x))
                    .ToProperty(this, nameof(KeyA), deferSubscription: true);
            _obsKeyAlt = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyAlt", x))
                    .ToProperty(this, nameof(KeyAlt), deferSubscription: true);
            _obsKeyB = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyB", x))
                    .ToProperty(this, nameof(KeyB), deferSubscription: true);
            _obsKeyBack = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyBack", x))
                    .ToProperty(this, nameof(KeyBack), deferSubscription: true);
            _obsKeyC = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyC", x))
                    .ToProperty(this, nameof(KeyC), deferSubscription: true);
            _obsKeyCapsLock = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyCapsLock", x))
                    .ToProperty(this, nameof(KeyCapsLock), deferSubscription: true);
            _obsKeyControl = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyControl", x))
                    .ToProperty(this, nameof(KeyControl), deferSubscription: true);
            _obsKeyD = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyD", x))
                    .ToProperty(this, nameof(KeyD), deferSubscription: true);
            _obsKeyDelete = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyDelete", x))
                    .ToProperty(this, nameof(KeyDelete), deferSubscription: true);
            _obsKeyDown = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyDown", x))
                    .ToProperty(this, nameof(KeyDown), deferSubscription: true);
            _obsKeyE = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyE", x))
                    .ToProperty(this, nameof(KeyE), deferSubscription: true);
            _obsKeyEnd = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyEnd", x))
                    .ToProperty(this, nameof(KeyEnd), deferSubscription: true);
            _obsKeyEnter = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyEnter", x))
                    .ToProperty(this, nameof(KeyEnter), deferSubscription: true);
            _obsKeyEscape = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyEscape", x))
                    .ToProperty(this, nameof(KeyEscape), deferSubscription: true);
            _obsKeyF = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF", x))
                    .ToProperty(this, nameof(KeyF), deferSubscription: true);
            _obsKeyF1 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF1", x))
                    .ToProperty(this, nameof(KeyF1), deferSubscription: true);
            _obsKeyF10 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF10", x))
                    .ToProperty(this, nameof(KeyF10), deferSubscription: true);
            _obsKeyF11 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF11", x))
                    .ToProperty(this, nameof(KeyF11), deferSubscription: true);
            _obsKeyF12 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF12", x))
                    .ToProperty(this, nameof(KeyF12), deferSubscription: true);
            _obsKeyF13 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF13", x))
                    .ToProperty(this, nameof(KeyF13), deferSubscription: true);
            _obsKeyF14 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF14", x))
                    .ToProperty(this, nameof(KeyF14), deferSubscription: true);
            _obsKeyF15 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF15", x))
                    .ToProperty(this, nameof(KeyF15), deferSubscription: true);
            _obsKeyF16 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF16", x))
                    .ToProperty(this, nameof(KeyF16), deferSubscription: true);
            _obsKeyF17 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF17", x))
                    .ToProperty(this, nameof(KeyF17), deferSubscription: true);
            _obsKeyF18 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF18", x))
                    .ToProperty(this, nameof(KeyF18), deferSubscription: true);
            _obsKeyF19 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF19", x))
                    .ToProperty(this, nameof(KeyF19), deferSubscription: true);
            _obsKeyF2 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF2", x))
                    .ToProperty(this, nameof(KeyF2), deferSubscription: true);
            _obsKeyF20 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF20", x))
                    .ToProperty(this, nameof(KeyF20), deferSubscription: true);
            _obsKeyF21 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF21", x))
                    .ToProperty(this, nameof(KeyF21), deferSubscription: true);
            _obsKeyF22 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF22", x))
                    .ToProperty(this, nameof(KeyF22), deferSubscription: true);
            _obsKeyF23 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF23", x))
                    .ToProperty(this, nameof(KeyF23), deferSubscription: true);
            _obsKeyF24 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF24", x))
                    .ToProperty(this, nameof(KeyF24), deferSubscription: true);
            _obsKeyF3 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF3", x))
                    .ToProperty(this, nameof(KeyF3), deferSubscription: true);
            _obsKeyF4 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF4", x))
                    .ToProperty(this, nameof(KeyF4), deferSubscription: true);
            _obsKeyF5 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF5", x))
                    .ToProperty(this, nameof(KeyF5), deferSubscription: true);
            _obsKeyF6 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF6", x))
                    .ToProperty(this, nameof(KeyF6), deferSubscription: true);
            _obsKeyF7 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF7", x))
                    .ToProperty(this, nameof(KeyF7), deferSubscription: true);
            _obsKeyF8 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF8", x))
                    .ToProperty(this, nameof(KeyF8), deferSubscription: true);
            _obsKeyF9 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyF9", x))
                    .ToProperty(this, nameof(KeyF9), deferSubscription: true);
            _obsKeyG = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyG", x))
                    .ToProperty(this, nameof(KeyG), deferSubscription: true);
            _obsKeyH = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyH", x))
                    .ToProperty(this, nameof(KeyH), deferSubscription: true);
            _obsKeyHelp = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyHelp", x))
                    .ToProperty(this, nameof(KeyHelp), deferSubscription: true);
            _obsKeyHome = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyHome", x))
                    .ToProperty(this, nameof(KeyHome), deferSubscription: true);
            _obsKeyI = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyI", x))
                    .ToProperty(this, nameof(KeyI), deferSubscription: true);
            _obsKeyInsert = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyInsert", x))
                    .ToProperty(this, nameof(KeyInsert), deferSubscription: true);
            _obsKeyJ = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyJ", x))
                    .ToProperty(this, nameof(KeyJ), deferSubscription: true);
            _obsKeyK = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyK", x))
                    .ToProperty(this, nameof(KeyK), deferSubscription: true);
            _obsKeyL = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyL", x))
                    .ToProperty(this, nameof(KeyL), deferSubscription: true);
            _obsKeyLeft = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyLeft", x))
                    .ToProperty(this, nameof(KeyLeft), deferSubscription: true);
            _obsKeyLeftAlt = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyLeftAlt", x))
                    .ToProperty(this, nameof(KeyLeftAlt), deferSubscription: true);
            _obsKeyLeftControl = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyLeftControl", x))
                    .ToProperty(this, nameof(KeyLeftControl), deferSubscription: true);
            _obsKeyLeftMouseButton = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyLeftMouseButton", x))
                    .ToProperty(this, nameof(KeyLeftMouseButton), deferSubscription: true);
            _obsKeyLeftShift = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyLeftShift", x))
                    .ToProperty(this, nameof(KeyLeftShift), deferSubscription: true);
            _obsKeyM = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyM", x))
                    .ToProperty(this, nameof(KeyM), deferSubscription: true);
            _obsKeyMiddleMouseButton = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyMiddleMouseButton", x))
                    .ToProperty(this, nameof(KeyMiddleMouseButton), deferSubscription: true);
            _obsKeyMouseButton1 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyMouseButton1", x))
                    .ToProperty(this, nameof(KeyMouseButton1), deferSubscription: true);
            _obsKeyMouseButton2 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyMouseButton2", x))
                    .ToProperty(this, nameof(KeyMouseButton2), deferSubscription: true);
            _obsKeyN = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyN", x))
                    .ToProperty(this, nameof(KeyN), deferSubscription: true);
            _obsKeyNumLock = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyNumLock", x))
                    .ToProperty(this, nameof(KeyNumLock), deferSubscription: true);
            _obsKeyNumPad0 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyNumPad0", x))
                    .ToProperty(this, nameof(KeyNumPad0), deferSubscription: true);
            _obsKeyNumPad1 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyNumPad1", x))
                    .ToProperty(this, nameof(KeyNumPad1), deferSubscription: true);
            _obsKeyNumPad2 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyNumPad2", x))
                    .ToProperty(this, nameof(KeyNumPad2), deferSubscription: true);
            _obsKeyNumPad3 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyNumPad3", x))
                    .ToProperty(this, nameof(KeyNumPad3), deferSubscription: true);
            _obsKeyNumPad4 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyNumPad4", x))
                    .ToProperty(this, nameof(KeyNumPad4), deferSubscription: true);
            _obsKeyNumPad5 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyNumPad5", x))
                    .ToProperty(this, nameof(KeyNumPad5), deferSubscription: true);
            _obsKeyNumPad6 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyNumPad6", x))
                    .ToProperty(this, nameof(KeyNumPad6), deferSubscription: true);
            _obsKeyNumPad7 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyNumPad7", x))
                    .ToProperty(this, nameof(KeyNumPad7), deferSubscription: true);
            _obsKeyNumPad8 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyNumPad8", x))
                    .ToProperty(this, nameof(KeyNumPad8), deferSubscription: true);
            _obsKeyNumPad9 = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyNumPad9", x))
                    .ToProperty(this, nameof(KeyNumPad9), deferSubscription: true);
            _obsKeyO = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyO", x))
                    .ToProperty(this, nameof(KeyO), deferSubscription: true);
            _obsKeyP = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyP", x))
                    .ToProperty(this, nameof(KeyP), deferSubscription: true);
            _obsKeyPageDown = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyPageDown", x))
                    .ToProperty(this, nameof(KeyPageDown), deferSubscription: true);
            _obsKeyPageup = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyPageup", x))
                    .ToProperty(this, nameof(KeyPageup), deferSubscription: true);
            _obsKeyPause = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyPause", x))
                    .ToProperty(this, nameof(KeyPause), deferSubscription: true);
            _obsKeyPrintScreen = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyPrintScreen", x))
                    .ToProperty(this, nameof(KeyPrintScreen), deferSubscription: true);
            _obsKeyQ = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyQ", x))
                    .ToProperty(this, nameof(KeyQ), deferSubscription: true);
            _obsKeyR = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyR", x))
                    .ToProperty(this, nameof(KeyR), deferSubscription: true);
            _obsKeyRight = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyRight", x))
                    .ToProperty(this, nameof(KeyRight), deferSubscription: true);
            _obsKeyRightAlt = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyRightAlt", x))
                    .ToProperty(this, nameof(KeyRightAlt), deferSubscription: true);
            _obsKeyRightControl = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyRightControl", x))
                    .ToProperty(this, nameof(KeyRightControl), deferSubscription: true);
            _obsKeyRightMouseButton = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyRightMouseButton", x))
                    .ToProperty(this, nameof(KeyRightMouseButton), deferSubscription: true);
            _obsKeyRightShift = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyRightShift", x))
                    .ToProperty(this, nameof(KeyRightShift), deferSubscription: true);
            _obsKeyS = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyS", x))
                    .ToProperty(this, nameof(KeyS), deferSubscription: true);
            _obsKeyScrollLock = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyScrollLock", x))
                    .ToProperty(this, nameof(KeyScrollLock), deferSubscription: true);
            _obsKeyShift = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyShift", x))
                    .ToProperty(this, nameof(KeyShift), deferSubscription: true);
            _obsKeySpace = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeySpace", x))
                    .ToProperty(this, nameof(KeySpace), deferSubscription: true);
            _obsKeyT = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyT", x))
                    .ToProperty(this, nameof(KeyT), deferSubscription: true);
            _obsKeyTab = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyTab", x))
                    .ToProperty(this, nameof(KeyTab), deferSubscription: true);
            _obsKeyU = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyU", x))
                    .ToProperty(this, nameof(KeyU), deferSubscription: true);
            _obsKeyUp = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyUp", x))
                    .ToProperty(this, nameof(KeyUp), deferSubscription: true);
            _obsKeyV = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyV", x))
                    .ToProperty(this, nameof(KeyV), deferSubscription: true);
            _obsKeyW = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyW", x))
                    .ToProperty(this, nameof(KeyW), deferSubscription: true);
            _obsKeyX = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyX", x))
                    .ToProperty(this, nameof(KeyX), deferSubscription: true);
            _obsKeyY = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyY", x))
                    .ToProperty(this, nameof(KeyY), deferSubscription: true);
            _obsKeyZ = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("KeyZ", x))
                    .ToProperty(this, nameof(KeyZ), deferSubscription: true);
            _obsLanguageEnglish = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("LanguageEnglish", x))
                    .ToProperty(this, nameof(LanguageEnglish), deferSubscription: true);
            _obsLanguageFrench = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("LanguageFrench", x))
                    .ToProperty(this, nameof(LanguageFrench), deferSubscription: true);
            _obsLanguageSelect = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("LanguageSelect", x))
                    .ToProperty(this, nameof(LanguageSelect), deferSubscription: true);
            _obsLanguageSpanish = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("LanguageSpanish", x))
                    .ToProperty(this, nameof(LanguageSpanish), deferSubscription: true);
            _obsLoadingIdentity = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("LoadingIdentity", x))
                    .ToProperty(this, nameof(LoadingIdentity), deferSubscription: true);
            _obsNavHome = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("NavHome", x))
                    .ToProperty(this, nameof(NavHome), deferSubscription: true);
            _obsNavSettings = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("NavSettings", x))
                    .ToProperty(this, nameof(NavSettings), deferSubscription: true);
            _obsNoClips = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("NoClips", x))
                    .ToProperty(this, nameof(NoClips), deferSubscription: true);
            _obsNoScreenshots = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("NoScreenshots", x))
                    .ToProperty(this, nameof(NoScreenshots), deferSubscription: true);
            _obsNoStats = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("NoStats", x))
                    .ToProperty(this, nameof(NoStats), deferSubscription: true);
            _obsNoUpdateAvailable = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("NoUpdateAvailable", x))
                    .ToProperty(this, nameof(NoUpdateAvailable), deferSubscription: true);
            _obsNoVods = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("NoVods", x))
                    .ToProperty(this, nameof(NoVods), deferSubscription: true);
            _obsProfilePicture = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("ProfilePicture", x))
                    .ToProperty(this, nameof(ProfilePicture), deferSubscription: true);
            _obsProfilePictureChooseBuiltIn = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("ProfilePictureChooseBuiltIn", x))
                    .ToProperty(this, nameof(ProfilePictureChooseBuiltIn), deferSubscription: true);
            _obsProfilePictureUploadOwn = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("ProfilePictureUploadOwn", x))
                    .ToProperty(this, nameof(ProfilePictureUploadOwn), deferSubscription: true);
            _obsQuitConfirm = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("QuitConfirm", x))
                    .ToProperty(this, nameof(QuitConfirm), deferSubscription: true);
            _obsQuitText = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("QuitText", x))
                    .ToProperty(this, nameof(QuitText), deferSubscription: true);
            _obsScreenshotHotkey = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("ScreenshotHotkey", x))
                    .ToProperty(this, nameof(ScreenshotHotkey), deferSubscription: true);
            _obsScreenshots = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("Screenshots", x))
                    .ToProperty(this, nameof(Screenshots), deferSubscription: true);
            _obsSettingsAbout = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsAbout", x))
                    .ToProperty(this, nameof(SettingsAbout), deferSubscription: true);
            _obsSettingsApp = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsApp", x))
                    .ToProperty(this, nameof(SettingsApp), deferSubscription: true);
            _obsSettingsAudioRecord = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsAudioRecord", x))
                    .ToProperty(this, nameof(SettingsAudioRecord), deferSubscription: true);
            _obsSettingsCheckForUpdates = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsCheckForUpdates", x))
                    .ToProperty(this, nameof(SettingsCheckForUpdates), deferSubscription: true);
            _obsSettingsClip = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsClip", x))
                    .ToProperty(this, nameof(SettingsClip), deferSubscription: true);
            _obsSettingsCustomGames = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsCustomGames", x))
                    .ToProperty(this, nameof(SettingsCustomGames), deferSubscription: true);
            _obsSettingsDevices = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsDevices", x))
                    .ToProperty(this, nameof(SettingsDevices), deferSubscription: true);
            _obsSettingsGames = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsGames", x))
                    .ToProperty(this, nameof(SettingsGames), deferSubscription: true);
            _obsSettingsGameSupport = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsGameSupport", x))
                    .ToProperty(this, nameof(SettingsGameSupport), deferSubscription: true);
            _obsSettingsLanguage = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsLanguage", x))
                    .ToProperty(this, nameof(SettingsLanguage), deferSubscription: true);
            _obsSettingsOther = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsOther", x))
                    .ToProperty(this, nameof(SettingsOther), deferSubscription: true);
            _obsSettingsOverlay = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsOverlay", x))
                    .ToProperty(this, nameof(SettingsOverlay), deferSubscription: true);
            _obsSettingsProfile = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsProfile", x))
                    .ToProperty(this, nameof(SettingsProfile), deferSubscription: true);
            _obsSettingsRecord = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsRecord", x))
                    .ToProperty(this, nameof(SettingsRecord), deferSubscription: true);
            _obsSettingsScreenshot = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsScreenshot", x))
                    .ToProperty(this, nameof(SettingsScreenshot), deferSubscription: true);
            _obsSettingsStorage = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsStorage", x))
                    .ToProperty(this, nameof(SettingsStorage), deferSubscription: true);
            _obsSettingsSystem = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsSystem", x))
                    .ToProperty(this, nameof(SettingsSystem), deferSubscription: true);
            _obsSettingsUser = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsUser", x))
                    .ToProperty(this, nameof(SettingsUser), deferSubscription: true);
            _obsSettingsVideoRecord = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SettingsVideoRecord", x))
                    .ToProperty(this, nameof(SettingsVideoRecord), deferSubscription: true);
            _obsSplashLoading = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SplashLoading", x))
                    .ToProperty(this, nameof(SplashLoading), deferSubscription: true);
            _obsStats = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("Stats", x))
                    .ToProperty(this, nameof(Stats), deferSubscription: true);
            _obsStatusInGame = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StatusInGame", x))
                    .ToProperty(this, nameof(StatusInGame), deferSubscription: true);
            _obsStatusPaused = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StatusPaused", x))
                    .ToProperty(this, nameof(StatusPaused), deferSubscription: true);
            _obsStatusReady = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StatusReady", x))
                    .ToProperty(this, nameof(StatusReady), deferSubscription: true);
            _obsStatusRecording = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StatusRecording", x))
                    .ToProperty(this, nameof(StatusRecording), deferSubscription: true);
            _obsStatusStopped = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StatusStopped", x))
                    .ToProperty(this, nameof(StatusStopped), deferSubscription: true);
            _obsStorageClipLocation = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageClipLocation", x))
                    .ToProperty(this, nameof(StorageClipLocation), deferSubscription: true);
            _obsStorageClipLocationTooltip = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageClipLocationTooltip", x))
                    .ToProperty(this, nameof(StorageClipLocationTooltip), deferSubscription: true);
            _obsStorageLogLocation = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageLogLocation", x))
                    .ToProperty(this, nameof(StorageLogLocation), deferSubscription: true);
            _obsStorageLogLocationTooltip = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageLogLocationTooltip", x))
                    .ToProperty(this, nameof(StorageLogLocationTooltip), deferSubscription: true);
            _obsStorageMatchLocation = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageMatchLocation", x))
                    .ToProperty(this, nameof(StorageMatchLocation), deferSubscription: true);
            _obsStorageMatchLocationTooltip = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageMatchLocationTooltip", x))
                    .ToProperty(this, nameof(StorageMatchLocationTooltip), deferSubscription: true);
            _obsStorageScreenshotLocation = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageScreenshotLocation", x))
                    .ToProperty(this, nameof(StorageScreenshotLocation), deferSubscription: true);
            _obsStorageScreenshotLocationTooltip = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageScreenshotLocationTooltip", x))
                    .ToProperty(this, nameof(StorageScreenshotLocationTooltip), deferSubscription: true);
            _obsStorageVodLocation = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageVodLocation", x))
                    .ToProperty(this, nameof(StorageVodLocation), deferSubscription: true);
            _obsStorageVodLocationTooltip = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("StorageVodLocationTooltip", x))
                    .ToProperty(this, nameof(StorageVodLocationTooltip), deferSubscription: true);
            _obsSystemMinimizeOnClose = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SystemMinimizeOnClose", x))
                    .ToProperty(this, nameof(SystemMinimizeOnClose), deferSubscription: true);
            _obsSystemMinimizeToSysTray = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("SystemMinimizeToSysTray", x))
                    .ToProperty(this, nameof(SystemMinimizeToSysTray), deferSubscription: true);
            _obsUnderConstruction = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("UnderConstruction", x))
                    .ToProperty(this, nameof(UnderConstruction), deferSubscription: true);
            _obsUnknown = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("Unknown", x))
                    .ToProperty(this, nameof(Unknown), deferSubscription: true);
            _obsUpdateCheck = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("UpdateCheck", x))
                    .ToProperty(this, nameof(UpdateCheck), deferSubscription: true);
            _obsUsernameTag = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("UsernameTag", x))
                    .ToProperty(this, nameof(UsernameTag), deferSubscription: true);
            _obsVods = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("Vods", x))
                    .ToProperty(this, nameof(Vods), deferSubscription: true);
            _obsWelcomeMessage = this.WhenAnyValue(x => x.Culture)
                    .Select(x => Get("WelcomeMessage", x))
                    .ToProperty(this, nameof(WelcomeMessage), deferSubscription: true);
        }

        private readonly ObservableAsPropertyHelper<string> _obsAbout;
        public string About { get => _obsAbout.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsButtonBrowse;
        public string ButtonBrowse { get => _obsButtonBrowse.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsChooseIdentityRequirements;
        public string ChooseIdentityRequirements { get => _obsChooseIdentityRequirements.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsChooseProfilePicture;
        public string ChooseProfilePicture { get => _obsChooseProfilePicture.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsChooseUsername;
        public string ChooseUsername { get => _obsChooseUsername.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsClips;
        public string Clips { get => _obsClips.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsControlPanel;
        public string ControlPanel { get => _obsControlPanel.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsCreateUserInstruction;
        public string CreateUserInstruction { get => _obsCreateUserInstruction.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsDesktopWindows;
        public string DesktopWindows { get => _obsDesktopWindows.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsDeviceFriendly;
        public string DeviceFriendly { get => _obsDeviceFriendly.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsDeviceId;
        public string DeviceId { get => _obsDeviceId.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsDeviceType;
        public string DeviceType { get => _obsDeviceType.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsDialogCancel;
        public string DialogCancel { get => _obsDialogCancel.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsDialogEdit;
        public string DialogEdit { get => _obsDialogEdit.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsDialogOk;
        public string DialogOk { get => _obsDialogOk.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsDialogSave;
        public string DialogSave { get => _obsDialogSave.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsDragAndDropOrClick;
        public string DragAndDropOrClick { get => _obsDragAndDropOrClick.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsErrorStorageFolder;
        public string ErrorStorageFolder { get => _obsErrorStorageFolder.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKey0;
        public string Key0 { get => _obsKey0.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKey1;
        public string Key1 { get => _obsKey1.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKey2;
        public string Key2 { get => _obsKey2.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKey3;
        public string Key3 { get => _obsKey3.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKey4;
        public string Key4 { get => _obsKey4.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKey5;
        public string Key5 { get => _obsKey5.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKey6;
        public string Key6 { get => _obsKey6.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKey7;
        public string Key7 { get => _obsKey7.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKey8;
        public string Key8 { get => _obsKey8.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKey9;
        public string Key9 { get => _obsKey9.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyA;
        public string KeyA { get => _obsKeyA.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyAlt;
        public string KeyAlt { get => _obsKeyAlt.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyB;
        public string KeyB { get => _obsKeyB.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyBack;
        public string KeyBack { get => _obsKeyBack.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyC;
        public string KeyC { get => _obsKeyC.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyCapsLock;
        public string KeyCapsLock { get => _obsKeyCapsLock.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyControl;
        public string KeyControl { get => _obsKeyControl.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyD;
        public string KeyD { get => _obsKeyD.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyDelete;
        public string KeyDelete { get => _obsKeyDelete.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyDown;
        public string KeyDown { get => _obsKeyDown.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyE;
        public string KeyE { get => _obsKeyE.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyEnd;
        public string KeyEnd { get => _obsKeyEnd.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyEnter;
        public string KeyEnter { get => _obsKeyEnter.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyEscape;
        public string KeyEscape { get => _obsKeyEscape.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF;
        public string KeyF { get => _obsKeyF.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF1;
        public string KeyF1 { get => _obsKeyF1.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF10;
        public string KeyF10 { get => _obsKeyF10.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF11;
        public string KeyF11 { get => _obsKeyF11.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF12;
        public string KeyF12 { get => _obsKeyF12.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF13;
        public string KeyF13 { get => _obsKeyF13.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF14;
        public string KeyF14 { get => _obsKeyF14.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF15;
        public string KeyF15 { get => _obsKeyF15.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF16;
        public string KeyF16 { get => _obsKeyF16.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF17;
        public string KeyF17 { get => _obsKeyF17.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF18;
        public string KeyF18 { get => _obsKeyF18.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF19;
        public string KeyF19 { get => _obsKeyF19.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF2;
        public string KeyF2 { get => _obsKeyF2.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF20;
        public string KeyF20 { get => _obsKeyF20.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF21;
        public string KeyF21 { get => _obsKeyF21.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF22;
        public string KeyF22 { get => _obsKeyF22.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF23;
        public string KeyF23 { get => _obsKeyF23.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF24;
        public string KeyF24 { get => _obsKeyF24.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF3;
        public string KeyF3 { get => _obsKeyF3.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF4;
        public string KeyF4 { get => _obsKeyF4.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF5;
        public string KeyF5 { get => _obsKeyF5.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF6;
        public string KeyF6 { get => _obsKeyF6.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF7;
        public string KeyF7 { get => _obsKeyF7.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF8;
        public string KeyF8 { get => _obsKeyF8.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyF9;
        public string KeyF9 { get => _obsKeyF9.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyG;
        public string KeyG { get => _obsKeyG.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyH;
        public string KeyH { get => _obsKeyH.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyHelp;
        public string KeyHelp { get => _obsKeyHelp.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyHome;
        public string KeyHome { get => _obsKeyHome.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyI;
        public string KeyI { get => _obsKeyI.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyInsert;
        public string KeyInsert { get => _obsKeyInsert.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyJ;
        public string KeyJ { get => _obsKeyJ.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyK;
        public string KeyK { get => _obsKeyK.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyL;
        public string KeyL { get => _obsKeyL.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyLeft;
        public string KeyLeft { get => _obsKeyLeft.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyLeftAlt;
        public string KeyLeftAlt { get => _obsKeyLeftAlt.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyLeftControl;
        public string KeyLeftControl { get => _obsKeyLeftControl.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyLeftMouseButton;
        public string KeyLeftMouseButton { get => _obsKeyLeftMouseButton.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyLeftShift;
        public string KeyLeftShift { get => _obsKeyLeftShift.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyM;
        public string KeyM { get => _obsKeyM.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyMiddleMouseButton;
        public string KeyMiddleMouseButton { get => _obsKeyMiddleMouseButton.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyMouseButton1;
        public string KeyMouseButton1 { get => _obsKeyMouseButton1.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyMouseButton2;
        public string KeyMouseButton2 { get => _obsKeyMouseButton2.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyN;
        public string KeyN { get => _obsKeyN.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyNumLock;
        public string KeyNumLock { get => _obsKeyNumLock.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyNumPad0;
        public string KeyNumPad0 { get => _obsKeyNumPad0.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyNumPad1;
        public string KeyNumPad1 { get => _obsKeyNumPad1.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyNumPad2;
        public string KeyNumPad2 { get => _obsKeyNumPad2.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyNumPad3;
        public string KeyNumPad3 { get => _obsKeyNumPad3.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyNumPad4;
        public string KeyNumPad4 { get => _obsKeyNumPad4.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyNumPad5;
        public string KeyNumPad5 { get => _obsKeyNumPad5.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyNumPad6;
        public string KeyNumPad6 { get => _obsKeyNumPad6.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyNumPad7;
        public string KeyNumPad7 { get => _obsKeyNumPad7.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyNumPad8;
        public string KeyNumPad8 { get => _obsKeyNumPad8.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyNumPad9;
        public string KeyNumPad9 { get => _obsKeyNumPad9.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyO;
        public string KeyO { get => _obsKeyO.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyP;
        public string KeyP { get => _obsKeyP.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyPageDown;
        public string KeyPageDown { get => _obsKeyPageDown.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyPageup;
        public string KeyPageup { get => _obsKeyPageup.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyPause;
        public string KeyPause { get => _obsKeyPause.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyPrintScreen;
        public string KeyPrintScreen { get => _obsKeyPrintScreen.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyQ;
        public string KeyQ { get => _obsKeyQ.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyR;
        public string KeyR { get => _obsKeyR.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyRight;
        public string KeyRight { get => _obsKeyRight.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyRightAlt;
        public string KeyRightAlt { get => _obsKeyRightAlt.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyRightControl;
        public string KeyRightControl { get => _obsKeyRightControl.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyRightMouseButton;
        public string KeyRightMouseButton { get => _obsKeyRightMouseButton.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyRightShift;
        public string KeyRightShift { get => _obsKeyRightShift.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyS;
        public string KeyS { get => _obsKeyS.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyScrollLock;
        public string KeyScrollLock { get => _obsKeyScrollLock.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyShift;
        public string KeyShift { get => _obsKeyShift.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeySpace;
        public string KeySpace { get => _obsKeySpace.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyT;
        public string KeyT { get => _obsKeyT.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyTab;
        public string KeyTab { get => _obsKeyTab.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyU;
        public string KeyU { get => _obsKeyU.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyUp;
        public string KeyUp { get => _obsKeyUp.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyV;
        public string KeyV { get => _obsKeyV.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyW;
        public string KeyW { get => _obsKeyW.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyX;
        public string KeyX { get => _obsKeyX.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyY;
        public string KeyY { get => _obsKeyY.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsKeyZ;
        public string KeyZ { get => _obsKeyZ.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsLanguageEnglish;
        public string LanguageEnglish { get => _obsLanguageEnglish.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsLanguageFrench;
        public string LanguageFrench { get => _obsLanguageFrench.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsLanguageSelect;
        public string LanguageSelect { get => _obsLanguageSelect.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsLanguageSpanish;
        public string LanguageSpanish { get => _obsLanguageSpanish.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsLoadingIdentity;
        public string LoadingIdentity { get => _obsLoadingIdentity.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsNavHome;
        public string NavHome { get => _obsNavHome.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsNavSettings;
        public string NavSettings { get => _obsNavSettings.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsNoClips;
        public string NoClips { get => _obsNoClips.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsNoScreenshots;
        public string NoScreenshots { get => _obsNoScreenshots.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsNoStats;
        public string NoStats { get => _obsNoStats.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsNoUpdateAvailable;
        public string NoUpdateAvailable { get => _obsNoUpdateAvailable.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsNoVods;
        public string NoVods { get => _obsNoVods.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsProfilePicture;
        public string ProfilePicture { get => _obsProfilePicture.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsProfilePictureChooseBuiltIn;
        public string ProfilePictureChooseBuiltIn { get => _obsProfilePictureChooseBuiltIn.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsProfilePictureUploadOwn;
        public string ProfilePictureUploadOwn { get => _obsProfilePictureUploadOwn.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsQuitConfirm;
        public string QuitConfirm { get => _obsQuitConfirm.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsQuitText;
        public string QuitText { get => _obsQuitText.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsScreenshotHotkey;
        public string ScreenshotHotkey { get => _obsScreenshotHotkey.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsScreenshots;
        public string Screenshots { get => _obsScreenshots.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsAbout;
        public string SettingsAbout { get => _obsSettingsAbout.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsApp;
        public string SettingsApp { get => _obsSettingsApp.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsAudioRecord;
        public string SettingsAudioRecord { get => _obsSettingsAudioRecord.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsCheckForUpdates;
        public string SettingsCheckForUpdates { get => _obsSettingsCheckForUpdates.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsClip;
        public string SettingsClip { get => _obsSettingsClip.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsCustomGames;
        public string SettingsCustomGames { get => _obsSettingsCustomGames.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsDevices;
        public string SettingsDevices { get => _obsSettingsDevices.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsGames;
        public string SettingsGames { get => _obsSettingsGames.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsGameSupport;
        public string SettingsGameSupport { get => _obsSettingsGameSupport.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsLanguage;
        public string SettingsLanguage { get => _obsSettingsLanguage.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsOther;
        public string SettingsOther { get => _obsSettingsOther.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsOverlay;
        public string SettingsOverlay { get => _obsSettingsOverlay.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsProfile;
        public string SettingsProfile { get => _obsSettingsProfile.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsRecord;
        public string SettingsRecord { get => _obsSettingsRecord.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsScreenshot;
        public string SettingsScreenshot { get => _obsSettingsScreenshot.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsStorage;
        public string SettingsStorage { get => _obsSettingsStorage.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsSystem;
        public string SettingsSystem { get => _obsSettingsSystem.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsUser;
        public string SettingsUser { get => _obsSettingsUser.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSettingsVideoRecord;
        public string SettingsVideoRecord { get => _obsSettingsVideoRecord.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSplashLoading;
        public string SplashLoading { get => _obsSplashLoading.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStats;
        public string Stats { get => _obsStats.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStatusInGame;
        public string StatusInGame { get => _obsStatusInGame.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStatusPaused;
        public string StatusPaused { get => _obsStatusPaused.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStatusReady;
        public string StatusReady { get => _obsStatusReady.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStatusRecording;
        public string StatusRecording { get => _obsStatusRecording.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStatusStopped;
        public string StatusStopped { get => _obsStatusStopped.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStorageClipLocation;
        public string StorageClipLocation { get => _obsStorageClipLocation.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStorageClipLocationTooltip;
        public string StorageClipLocationTooltip { get => _obsStorageClipLocationTooltip.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStorageLogLocation;
        public string StorageLogLocation { get => _obsStorageLogLocation.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStorageLogLocationTooltip;
        public string StorageLogLocationTooltip { get => _obsStorageLogLocationTooltip.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStorageMatchLocation;
        public string StorageMatchLocation { get => _obsStorageMatchLocation.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStorageMatchLocationTooltip;
        public string StorageMatchLocationTooltip { get => _obsStorageMatchLocationTooltip.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStorageScreenshotLocation;
        public string StorageScreenshotLocation { get => _obsStorageScreenshotLocation.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStorageScreenshotLocationTooltip;
        public string StorageScreenshotLocationTooltip { get => _obsStorageScreenshotLocationTooltip.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStorageVodLocation;
        public string StorageVodLocation { get => _obsStorageVodLocation.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsStorageVodLocationTooltip;
        public string StorageVodLocationTooltip { get => _obsStorageVodLocationTooltip.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSystemMinimizeOnClose;
        public string SystemMinimizeOnClose { get => _obsSystemMinimizeOnClose.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsSystemMinimizeToSysTray;
        public string SystemMinimizeToSysTray { get => _obsSystemMinimizeToSysTray.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsUnderConstruction;
        public string UnderConstruction { get => _obsUnderConstruction.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsUnknown;
        public string Unknown { get => _obsUnknown.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsUpdateCheck;
        public string UpdateCheck { get => _obsUpdateCheck.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsUsernameTag;
        public string UsernameTag { get => _obsUsernameTag.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsVods;
        public string Vods { get => _obsVods.Value; }

        private readonly ObservableAsPropertyHelper<string> _obsWelcomeMessage;
        public string WelcomeMessage { get => _obsWelcomeMessage.Value; }


        public string Get(string key, CultureInfo? info = null)
        {
            return _manager.GetString(key, info) ?? "<INVALID>";
        }
    }
}
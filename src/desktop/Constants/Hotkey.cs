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
using Avalonia.Input;

namespace SquadOV.Constants
{
    public enum LogicalAction
    {
        Unknown,
        Screenshot,
    }

    public class Keys
    {
        // For Windows: https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
        public enum Codes
        {
            Unknown,
            Lmb = 0x01,
            Rmb = 0x02,
            Mmb = 0x04,
            Xbutton1 = 0x05,
            Xbutton2 = 0x06,
            Back = 0x08,
            Tab = 0x09,
            Enter = 0x0d,
            Shift = 0x10,
            Control = 0x11,
            Alt = 0x12,
            Pause = 0x13,
            CapsLock = 0x14,
            Escape = 0x1b,
            Space = 0x20,
            PageUp = 0x21,
            PageDown = 0x22,
            End = 0x23,
            Home = 0x24,
            Left = 0x25,
            Up = 0x26,
            Right = 0x27,
            Down = 0x28,
            Snapshot = 0x2c,
            Insert = 0x2d,
            Delete = 0x2e,
            Help = 0x2f,
            Key0 = 0x30,
            Key1,
            Key2,
            Key3,
            Key4,
            Key5,
            Key6,
            Key7,
            Key8,
            Key9,
            KeyA = 0x41,
            KeyB,
            KeyC,
            KeyD,
            KeyE,
            KeyF,
            KeyG,
            KeyH,
            KeyI,
            KeyJ,
            KeyK,
            KeyL,
            KeyM,
            KeyN,
            KeyO,
            KeyP,
            KeyQ,
            KeyR,
            KeyS,
            KeyT,
            KeyU,
            KeyV,
            KeyW,
            KeyX,
            KeyY,
            KeyZ,
            NumPad0 = 0x60,
            NumPad1,
            NumPad2,
            NumPad3,
            NumPad4,
            NumPad5,
            NumPad6,
            NumPad7,
            NumPad8,
            NumPad9,
            F1 = 0x70,
            F2,
            F3,
            F4,
            F5,
            F6,
            F7,
            F8,
            F9,
            F10,
            F11,
            F12,
            F13,
            F14,
            F15,
            F16,
            F17,
            F18,
            F19,
            F20,
            F21,
            F22,
            F23,
            F24,
            NumLock = 0x90,
            ScrollLock = 0x91,
            LeftShift = 0xa0,
            RightShift = 0xa1,
            LeftControl = 0xa2,
            RightControl = 0xa3,
            LeftAlt = 0xa4,
            RightAlt = 0xa5,
        }

        public static Codes VirtualToCode(int k)
        {
            return Enum.IsDefined(typeof(Codes), k) ? (Codes)k : Codes.Unknown;
        }

        public static string CodeToLocKey(Codes code)
        {
            switch (code)
            {
                case Codes.Lmb:
                    return "KeyLeftMouseButton";
                case Codes.Rmb:
                    return "KeyRightMouseButton";
                case Codes.Mmb:
                    return "KeyMiddleMouseButton";
                case Codes.Xbutton1:
                    return "KeyMouseButton1";
                case Codes.Xbutton2:
                    return "KeyMouseButton2";
                case Codes.Back:
                    return "KeyBack";
                case Codes.Tab:
                    return "KeyTab";
                case Codes.Enter:
                    return "KeyEnter";
                case Codes.Shift:
                    return "KeyShift";
                case Codes.Control:
                    return "KeyControl";
                case Codes.Alt:
                    return "KeyAlt";
                case Codes.Pause:
                    return "KeyPause";
                case Codes.CapsLock:
                    return "KeyCapsLock";
                case Codes.Escape:
                    return "KeyEscape";
                case Codes.Space:
                    return "KeySpace";
                case Codes.PageUp:
                    return "KeyPageup";
                case Codes.PageDown:
                    return "KeyPageDown";
                case Codes.End:
                    return "KeyEnd";
                case Codes.Home:
                    return "KeyHome";
                case Codes.Left:
                    return "KeyLeft";
                case Codes.Up:
                    return "KeyUp";
                case Codes.Right:
                    return "KeyRight";
                case Codes.Down:
                    return "KeyDown";
                case Codes.Snapshot:
                    return "KeyPrintScreen";
                case Codes.Insert:
                    return "KeyInsert";
                case Codes.Delete:
                    return "KeyDelete";
                case Codes.Help:
                    return "KeyHelp";
                case Codes.Key0:
                case Codes.Key1:
                case Codes.Key2:
                case Codes.Key3:
                case Codes.Key4:
                case Codes.Key5:
                case Codes.Key6:
                case Codes.Key7:
                case Codes.Key8:
                case Codes.Key9:
                    return $"Key{0 + (int)code - (int)Codes.Key0}";
                case Codes.KeyA:
                case Codes.KeyB:
                case Codes.KeyC:
                case Codes.KeyD:
                case Codes.KeyE:
                case Codes.KeyF:
                case Codes.KeyG:
                case Codes.KeyH:
                case Codes.KeyI:
                case Codes.KeyJ:
                case Codes.KeyK:
                case Codes.KeyL:
                case Codes.KeyM:
                case Codes.KeyN:
                case Codes.KeyO:
                case Codes.KeyP:
                case Codes.KeyQ:
                case Codes.KeyR:
                case Codes.KeyS:
                case Codes.KeyT:
                case Codes.KeyU:
                case Codes.KeyV:
                case Codes.KeyW:
                case Codes.KeyX:
                case Codes.KeyY:
                case Codes.KeyZ:
                    return $"Key{(char)('A' + (char)((int)code - (int)Codes.KeyA))}";
                case Codes.NumPad0:
                case Codes.NumPad1:
                case Codes.NumPad2:
                case Codes.NumPad3:
                case Codes.NumPad4:
                case Codes.NumPad5:
                case Codes.NumPad6:
                case Codes.NumPad7:
                case Codes.NumPad8:
                case Codes.NumPad9:
                    return $"KeyNumPad{0 + (int)code - (int)Codes.NumPad0}";
                case Codes.F1:
                case Codes.F2:
                case Codes.F3:
                case Codes.F4:
                case Codes.F5:
                case Codes.F6:
                case Codes.F7:
                case Codes.F8:
                case Codes.F9:
                case Codes.F10:
                case Codes.F11:
                case Codes.F12:
                case Codes.F13:
                case Codes.F14:
                case Codes.F15:
                case Codes.F16:
                case Codes.F17:
                case Codes.F18:
                case Codes.F19:
                case Codes.F20:
                case Codes.F21:
                case Codes.F22:
                case Codes.F23:
                case Codes.F24:
                    return $"KeyF{1 + (int)code - (int)Codes.F1}";
                case Codes.NumLock:
                    return "KeyNumLock";
                case Codes.ScrollLock:
                    return "KeyScrollLock";
                case Codes.LeftShift:
                    return "KeyLeftShift";
                case Codes.RightShift:
                    return "KeyRightShift";
                case Codes.LeftControl:
                    return "KeyLeftControl";
                case Codes.RightControl:
                    return "KeyRightControl";
                case Codes.LeftAlt:
                    return "KeyLeftAlt";
                case Codes.RightAlt:
                    return "KeyRightAlt";
            }
            return "Unknown";
        }

        // Below is copy pasted from Avalonia.Win32.Input.KeyInterop.
        // Not using it directly since I don't want to bring in another dependency on Avalonia.Win32.Interopability
        private static readonly Dictionary<Key, int> s_virtualKeyFromKey = new Dictionary<Key, int>
        {
            { Key.None, 0 },
            { Key.Cancel, 3 },
            { Key.Back, 8 },
            { Key.Tab, 9 },
            { Key.LineFeed, 0 },
            { Key.Clear, 12 },
            { Key.Return, 13 },
            { Key.Pause, 19 },
            { Key.Capital, 20 },
            { Key.KanaMode, 21 },
            { Key.JunjaMode, 23 },
            { Key.FinalMode, 24 },
            { Key.HanjaMode, 25 },
            { Key.Escape, 27 },
            { Key.ImeConvert, 28 },
            { Key.ImeNonConvert, 29 },
            { Key.ImeAccept, 30 },
            { Key.ImeModeChange, 31 },
            { Key.Space, 32 },
            { Key.PageUp, 33 },
            { Key.Next, 34 },
            { Key.End, 35 },
            { Key.Home, 36 },
            { Key.Left, 37 },
            { Key.Up, 38 },
            { Key.Right, 39 },
            { Key.Down, 40 },
            { Key.Select, 41 },
            { Key.Print, 42 },
            { Key.Execute, 43 },
            { Key.Snapshot, 44 },
            { Key.Insert, 45 },
            { Key.Delete, 46 },
            { Key.Help, 47 },
            { Key.D0, 48 },
            { Key.D1, 49 },
            { Key.D2, 50 },
            { Key.D3, 51 },
            { Key.D4, 52 },
            { Key.D5, 53 },
            { Key.D6, 54 },
            { Key.D7, 55 },
            { Key.D8, 56 },
            { Key.D9, 57 },
            { Key.A, 65 },
            { Key.B, 66 },
            { Key.C, 67 },
            { Key.D, 68 },
            { Key.E, 69 },
            { Key.F, 70 },
            { Key.G, 71 },
            { Key.H, 72 },
            { Key.I, 73 },
            { Key.J, 74 },
            { Key.K, 75 },
            { Key.L, 76 },
            { Key.M, 77 },
            { Key.N, 78 },
            { Key.O, 79 },
            { Key.P, 80 },
            { Key.Q, 81 },
            { Key.R, 82 },
            { Key.S, 83 },
            { Key.T, 84 },
            { Key.U, 85 },
            { Key.V, 86 },
            { Key.W, 87 },
            { Key.X, 88 },
            { Key.Y, 89 },
            { Key.Z, 90 },
            { Key.LWin, 91 },
            { Key.RWin, 92 },
            { Key.Apps, 93 },
            { Key.Sleep, 95 },
            { Key.NumPad0, 96 },
            { Key.NumPad1, 97 },
            { Key.NumPad2, 98 },
            { Key.NumPad3, 99 },
            { Key.NumPad4, 100 },
            { Key.NumPad5, 101 },
            { Key.NumPad6, 102 },
            { Key.NumPad7, 103 },
            { Key.NumPad8, 104 },
            { Key.NumPad9, 105 },
            { Key.Multiply, 106 },
            { Key.Add, 107 },
            { Key.Separator, 108 },
            { Key.Subtract, 109 },
            { Key.Decimal, 110 },
            { Key.Divide, 111 },
            { Key.F1, 112 },
            { Key.F2, 113 },
            { Key.F3, 114 },
            { Key.F4, 115 },
            { Key.F5, 116 },
            { Key.F6, 117 },
            { Key.F7, 118 },
            { Key.F8, 119 },
            { Key.F9, 120 },
            { Key.F10, 121 },
            { Key.F11, 122 },
            { Key.F12, 123 },
            { Key.F13, 124 },
            { Key.F14, 125 },
            { Key.F15, 126 },
            { Key.F16, 127 },
            { Key.F17, 128 },
            { Key.F18, 129 },
            { Key.F19, 130 },
            { Key.F20, 131 },
            { Key.F21, 132 },
            { Key.F22, 133 },
            { Key.F23, 134 },
            { Key.F24, 135 },
            { Key.NumLock, 144 },
            { Key.Scroll, 145 },
            { Key.LeftShift, 160 },
            { Key.RightShift, 161 },
            { Key.LeftCtrl, 162 },
            { Key.RightCtrl, 163 },
            { Key.LeftAlt, 164 },
            { Key.RightAlt, 165 },
            { Key.BrowserBack, 166 },
            { Key.BrowserForward, 167 },
            { Key.BrowserRefresh, 168 },
            { Key.BrowserStop, 169 },
            { Key.BrowserSearch, 170 },
            { Key.BrowserFavorites, 171 },
            { Key.BrowserHome, 172 },
            { Key.VolumeMute, 173 },
            { Key.VolumeDown, 174 },
            { Key.VolumeUp, 175 },
            { Key.MediaNextTrack, 176 },
            { Key.MediaPreviousTrack, 177 },
            { Key.MediaStop, 178 },
            { Key.MediaPlayPause, 179 },
            { Key.LaunchMail, 180 },
            { Key.SelectMedia, 181 },
            { Key.LaunchApplication1, 182 },
            { Key.LaunchApplication2, 183 },
            { Key.Oem1, 186 },
            { Key.OemPlus, 187 },
            { Key.OemComma, 188 },
            { Key.OemMinus, 189 },
            { Key.OemPeriod, 190 },
            { Key.OemQuestion, 191 },
            { Key.Oem3, 192 },
            { Key.AbntC1, 193 },
            { Key.AbntC2, 194 },
            { Key.OemOpenBrackets, 219 },
            { Key.Oem5, 220 },
            { Key.Oem6, 221 },
            { Key.OemQuotes, 222 },
            { Key.Oem8, 223 },
            { Key.OemBackslash, 226 },
            { Key.ImeProcessed, 229 },
            { Key.System, 0 },
            { Key.OemAttn, 240 },
            { Key.OemFinish, 241 },
            { Key.OemCopy, 242 },
            { Key.DbeSbcsChar, 243 },
            { Key.OemEnlw, 244 },
            { Key.OemBackTab, 245 },
            { Key.DbeNoRoman, 246 },
            { Key.DbeEnterWordRegisterMode, 247 },
            { Key.DbeEnterImeConfigureMode, 248 },
            { Key.EraseEof, 249 },
            { Key.Play, 250 },
            { Key.DbeNoCodeInput, 251 },
            { Key.NoName, 252 },
            { Key.Pa1, 253 },
            { Key.OemClear, 254 },
            { Key.DeadCharProcessed, 0 },
        };

        public static int VirtualKeyFromKey(Key key)
        {
            s_virtualKeyFromKey.TryGetValue(key, out var result);

            return result;
        }
    }
}
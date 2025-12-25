using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace WindowSlate
{
    public class HotKeyManager
    {
        public static event EventHandler<HotKeyEventArgs>? HotKeyPressed;

        // Dictionary to store registered hotkeys: id -> (key, modifiers)
        private static Dictionary<int, (Keys key, KeyModifiers modifiers)> _registeredHotKeys = new();
        private static int _nextId = 0;

        // Hook handle
        private static IntPtr _hookId = IntPtr.Zero;

        // We must keep a reference to the delegate to prevent it from being garbage collected
        private static Win32.LowLevelKeyboardProc _hookProc = HookCallback;

        // Virtual key codes for modifier keys
        private const int VK_LSHIFT = 0xA0;
        private const int VK_RSHIFT = 0xA1;
        private const int VK_LCONTROL = 0xA2;
        private const int VK_RCONTROL = 0xA3;
        private const int VK_LMENU = 0xA4;  // Left Alt
        private const int VK_RMENU = 0xA5;  // Right Alt
        private const int VK_LWIN = 0x5B;
        private const int VK_RWIN = 0x5C;

        static HotKeyManager()
        {
            InstallHook();
        }

        private static void InstallHook()
        {
            if (_hookId != IntPtr.Zero)
            {
                return;
            }

            using (Process curProcess = Process.GetCurrentProcess())
            {
                if (curProcess?.MainModule?.ModuleName != null)
                {
                    IntPtr moduleHandle = Win32.GetModuleHandle(curProcess.MainModule.ModuleName);
                    _hookId = Win32.SetWindowsHookEx(Win32.WH_KEYBOARD_LL, _hookProc, moduleHandle, 0);
                }
            }
        }

        private static void UninstallHook()
        {
            if (_hookId != IntPtr.Zero)
            {
                Win32.UnhookWindowsHookEx(_hookId);
                _hookId = IntPtr.Zero;
            }
        }

        public static int RegisterHotKey(Keys key, KeyModifiers modifiers)
        {
            int id = Interlocked.Increment(ref _nextId);
            lock (_registeredHotKeys)
            {
                _registeredHotKeys[id] = (key, modifiers);
            }
            return id;
        }

        public static bool UnregisterHotKey(int id)
        {
            lock (_registeredHotKeys)
            {
                return _registeredHotKeys.Remove(id);
            }
        }

        private static KeyModifiers GetCurrentModifiers()
        {
            KeyModifiers modifiers = 0;

            // Check Control key
            if ((Win32.GetAsyncKeyState(VK_LCONTROL) & 0x8000) != 0 ||
                (Win32.GetAsyncKeyState(VK_RCONTROL) & 0x8000) != 0)
            {
                modifiers |= KeyModifiers.Control;
            }

            // Check Alt key
            if ((Win32.GetAsyncKeyState(VK_LMENU) & 0x8000) != 0 ||
                (Win32.GetAsyncKeyState(VK_RMENU) & 0x8000) != 0)
            {
                modifiers |= KeyModifiers.Alt;
            }

            // Check Shift key
            if ((Win32.GetAsyncKeyState(VK_LSHIFT) & 0x8000) != 0 ||
                (Win32.GetAsyncKeyState(VK_RSHIFT) & 0x8000) != 0)
            {
                modifiers |= KeyModifiers.Shift;
            }

            // Check Windows key
            if ((Win32.GetAsyncKeyState(VK_LWIN) & 0x8000) != 0 ||
                (Win32.GetAsyncKeyState(VK_RWIN) & 0x8000) != 0)
            {
                modifiers |= KeyModifiers.Windows;
            }

            return modifiers;
        }

        private static bool IsModifierKey(int vkCode)
        {
            return vkCode == VK_LSHIFT || vkCode == VK_RSHIFT ||
                   vkCode == VK_LCONTROL || vkCode == VK_RCONTROL ||
                   vkCode == VK_LMENU || vkCode == VK_RMENU ||
                   vkCode == VK_LWIN || vkCode == VK_RWIN;
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int msg = wParam.ToInt32();

                // Only process key down events
                if (msg == Win32.WM_KEYDOWN || msg == Win32.WM_SYSKEYDOWN)
                {
                    KBDLLHOOKSTRUCT hookStruct = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);
                    int vkCode = (int)hookStruct.vkCode;

                    // Skip if this is a modifier key itself
                    if (!IsModifierKey(vkCode))
                    {
                        Keys key = (Keys)vkCode;
                        KeyModifiers currentModifiers = GetCurrentModifiers();

                        // Check if this key combination matches any registered hotkey
                        lock (_registeredHotKeys)
                        {
                            foreach (var kvp in _registeredHotKeys)
                            {
                                if (kvp.Value.key == key && kvp.Value.modifiers == currentModifiers)
                                {
                                    // Found a match - fire the event and suppress the key
                                    OnHotKeyPressed(new HotKeyEventArgs(key, currentModifiers));

                                    // Return non-zero to suppress the key from being processed by Windows
                                    return (IntPtr)1;
                                }
                            }
                        }
                    }
                }
            }

            // Pass the hook to the next hook in the chain
            return Win32.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        protected static void OnHotKeyPressed(HotKeyEventArgs e)
        {
            HotKeyPressed?.Invoke(null, e);
        }
    }

    public class HotKeyEventArgs : EventArgs
    {
        public readonly Keys Key;
        public readonly KeyModifiers Modifiers;

        public HotKeyEventArgs(Keys key, KeyModifiers modifiers)
        {
            this.Key = key;
            this.Modifiers = modifiers;
        }

        public HotKeyEventArgs(IntPtr hotKeyParam)
        {
            uint param = (uint)hotKeyParam.ToInt64();
            Key = (Keys)((param & 0xffff0000) >> 16);
            Modifiers = (KeyModifiers)(param & 0x0000ffff);
        }
    }

    [Flags]
    public enum KeyModifiers
    {
        Alt = 1,
        Control = 2,
        Shift = 4,
        Windows = 8,
        NoRepeat = 0x4000
    }
}

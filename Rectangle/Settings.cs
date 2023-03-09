using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rectangle
{
    public partial class Settings : Form
    {
        HotKeyInput inputMaximize;
        HotKeyInput inputUnmaximize;
        HotKeyInput inputHalfLeft;
        HotKeyInput inputHalfRight;
        HotKeyInput inputMiddleTwoThirds;
        HotKeyInput inputTopLeft;
        HotKeyInput inputTopRight;
        HotKeyInput inputBottomLeft;
        HotKeyInput inputBottomRight;
        HotKeyInput inputPreviousDisplay;
        HotKeyInput inputNextDisplay;

        List<HotKeyInput> allHotKeyInputs;

        public Settings()
        {
            this.KeyPreview = true;
            this.inputMaximize = new HotKeyInput() { 
                Description = "Maximize", 
                RegisterHotKeysHandler = this.RegisterHotKeys, 
                UnregisterHotKeysHandler = this.UnregisterHotKeys,
                SaveSettingsHandler = this.SaveSettings,
            };
            this.inputUnmaximize = new HotKeyInput() { 
                Description = "Unmaximize",
                RegisterHotKeysHandler = this.RegisterHotKeys, 
                UnregisterHotKeysHandler = this.UnregisterHotKeys,
                SaveSettingsHandler = this.SaveSettings,
            };
            this.inputHalfLeft = new HotKeyInput() { 
                Description = "Half-left", 
                RegisterHotKeysHandler = this.RegisterHotKeys, 
                UnregisterHotKeysHandler = this.UnregisterHotKeys,
                SaveSettingsHandler = this.SaveSettings,
            };
            this.inputHalfRight = new HotKeyInput() { 
                Description = "Half-right", 
                RegisterHotKeysHandler = this.RegisterHotKeys, 
                UnregisterHotKeysHandler = this.UnregisterHotKeys,
                SaveSettingsHandler = this.SaveSettings,
            };
            this.inputMiddleTwoThirds = new HotKeyInput() { 
                Description = "Middle",
                RegisterHotKeysHandler = this.RegisterHotKeys, 
                UnregisterHotKeysHandler = this.UnregisterHotKeys,
                SaveSettingsHandler = this.SaveSettings,
            };
            this.inputTopLeft = new HotKeyInput() { 
                Description = "Top left", 
                RegisterHotKeysHandler = this.RegisterHotKeys, 
                UnregisterHotKeysHandler = this.UnregisterHotKeys,
                SaveSettingsHandler = this.SaveSettings,
            };
            this.inputTopRight = new HotKeyInput() { 
                Description = "Top right",
                RegisterHotKeysHandler = this.RegisterHotKeys, 
                UnregisterHotKeysHandler = this.UnregisterHotKeys,
                SaveSettingsHandler = this.SaveSettings,
            };
            this.inputBottomLeft = new HotKeyInput() { 
                Description = "Bottom left",
                RegisterHotKeysHandler = this.RegisterHotKeys, 
                UnregisterHotKeysHandler = this.UnregisterHotKeys,
                SaveSettingsHandler = this.SaveSettings,
            };
            this.inputBottomRight = new HotKeyInput() { 
                Description = "Bottom right",
                RegisterHotKeysHandler = this.RegisterHotKeys, 
                UnregisterHotKeysHandler = this.UnregisterHotKeys,
                SaveSettingsHandler = this.SaveSettings,
            };
            this.inputPreviousDisplay = new HotKeyInput() { 
                Description = "Previous display",
                RegisterHotKeysHandler = this.RegisterHotKeys, 
                UnregisterHotKeysHandler = this.UnregisterHotKeys,
                SaveSettingsHandler = this.SaveSettings,
            };
            this.inputNextDisplay = new HotKeyInput() { 
                Description = "Next display",
                RegisterHotKeysHandler = this.RegisterHotKeys, 
                UnregisterHotKeysHandler = this.UnregisterHotKeys,
                SaveSettingsHandler = this.SaveSettings,
            };

            this.allHotKeyInputs = new()
            {
                inputMaximize,
                inputUnmaximize,
                inputHalfLeft,
                inputHalfRight,
                inputMiddleTwoThirds,
                inputTopLeft,
                inputTopRight,
                inputBottomLeft,
                inputBottomRight,
                inputPreviousDisplay,
                inputNextDisplay,
            };

            InitializeComponent();

            this.KeyDown += Settings_KeyDown;

            this.inputMaximize.HotKeyHandler = this.Maximize;
            this.inputUnmaximize.HotKeyHandler = this.Unmaximize;
            this.inputHalfLeft.HotKeyHandler = this.HalfLeft;
            this.inputHalfRight.HotKeyHandler = this.HalfRight;
            this.inputMiddleTwoThirds.HotKeyHandler = this.MiddleTwoThirds;
            this.inputTopLeft.HotKeyHandler = this.TopLeft;
            this.inputTopRight.HotKeyHandler = this.TopRight;
            this.inputBottomLeft.HotKeyHandler = this.BottomLeft;
            this.inputBottomRight.HotKeyHandler = this.BottomRight;
            this.inputPreviousDisplay.HotKeyHandler = this.PreviousDisplay;
            this.inputNextDisplay.HotKeyHandler = this.NextDisplay;

            var storedSettings = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Rectangle");
            if (storedSettings == null)
            {
                storedSettings = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Rectangle");
            }
            try
            {
                var settingHalfLeft = storedSettings.GetValue("half-left");
                if (settingHalfLeft != null)
                {
                    if ((string)settingHalfLeft != "")
                    {
                        this.inputHalfLeft.HotKey = new HotKey((string)settingHalfLeft);
                    }
                }
                else
                {
                    this.inputHalfLeft.HotKey = new HotKey(Keys.H, KeyModifiers.Control | KeyModifiers.Alt);
                }

                var settingTopLeft = storedSettings.GetValue("top-left");
                if (settingTopLeft != null)
                {
                    if ((string)settingTopLeft != "")
                    {
                        this.inputTopLeft.HotKey = new HotKey((string)settingTopLeft);
                    }
                }
                else
                {
                    this.inputTopLeft.HotKey = new HotKey(Keys.H, KeyModifiers.Control | KeyModifiers.Windows);
                }

                var settingPreviousDisplay = storedSettings.GetValue("previous-display");
                if (settingPreviousDisplay != null)
                {
                    if ((string)settingPreviousDisplay != "")
                    {
                        this.inputPreviousDisplay.HotKey = new HotKey((string)settingPreviousDisplay);
                    }
                }
                else
                {
                    this.inputPreviousDisplay.HotKey = new HotKey(Keys.H, KeyModifiers.Control | KeyModifiers.Shift | KeyModifiers.Alt);
                }

                var settingMiddleTwoThirds = storedSettings.GetValue("middle-two-thirds");
                if (settingMiddleTwoThirds != null)
                {
                    if ((string)settingMiddleTwoThirds != "")
                    {
                        this.inputMiddleTwoThirds.HotKey = new HotKey((string)settingMiddleTwoThirds);
                    }
                }
                else
                {
                    this.inputMiddleTwoThirds.HotKey = new HotKey(Keys.I, KeyModifiers.Control | KeyModifiers.Alt);
                }

                var settingBottomLeft = storedSettings.GetValue("bottom-left");
                if (settingBottomLeft != null)
                {
                    if ((string)settingBottomLeft != "")
                    {
                        this.inputBottomLeft.HotKey = new HotKey((string)settingBottomLeft);
                    }
                }
                else
                {
                    this.inputBottomLeft.HotKey = new HotKey(Keys.J, KeyModifiers.Control | KeyModifiers.Windows);
                }

                var settingUnmaximize = storedSettings.GetValue("unmaximize");
                if (settingUnmaximize != null)
                {
                    if ((string)settingUnmaximize != "")
                    {
                        this.inputUnmaximize.HotKey = new HotKey((string)settingUnmaximize);
                    }
                }
                else
                {
                    this.inputUnmaximize.HotKey = new HotKey(Keys.J, KeyModifiers.Control | KeyModifiers.Alt);
                }

                var settingBottomRight = storedSettings.GetValue("bottom-right");
                if (settingBottomRight != null)
                {
                    if ((string)settingBottomRight != "")
                    {
                        this.inputBottomRight.HotKey = new HotKey((string)settingBottomRight);
                    }
                } 
                else
                {
                    this.inputBottomRight.HotKey = new HotKey(Keys.K, KeyModifiers.Control | KeyModifiers.Windows);
                }

                var settingMaximize = storedSettings.GetValue("maximize");
                if (settingMaximize != null)
                {
                    if ((string)settingMaximize != "")
                    {
                        this.inputMaximize.HotKey = new HotKey((string)settingMaximize);
                    }
                }
                else
                {
                    this.inputMaximize.HotKey = new HotKey(Keys.K, KeyModifiers.Control | KeyModifiers.Alt);
                }

                var settingHalfRight = storedSettings.GetValue("half-right");
                if (settingHalfRight != null)
                {
                    if ((string)settingHalfRight != "")
                    {
                        this.inputHalfRight.HotKey = new HotKey((string)settingHalfRight);
                    }
                }
                else
                {
                    this.inputHalfRight.HotKey = new HotKey(Keys.L, KeyModifiers.Control | KeyModifiers.Alt);
                }

                var settingTopRight = storedSettings.GetValue("top-right");
                if (settingTopRight != null)
                {
                    if ((string)settingTopRight != "")
                    {
                        this.inputTopRight.HotKey = new HotKey((string)settingTopRight);
                    }
                }
                else
                {
                    this.inputTopRight.HotKey = new HotKey(Keys.L, KeyModifiers.Control | KeyModifiers.Windows);
                }

                var settingNextDisplay = storedSettings.GetValue("next-display");
                if (settingNextDisplay != null)
                {
                    if ((string)settingNextDisplay != "")
                    {
                        this.inputNextDisplay.HotKey = new HotKey((string)settingNextDisplay);
                    }
                }
                else
                {
                    this.inputNextDisplay.HotKey = new HotKey(Keys.L, KeyModifiers.Control | KeyModifiers.Shift | KeyModifiers.Alt);
                }
            } finally
            {
                storedSettings.Close();
            }

            HotKeyManager.HotKeyPressed += HotKeyManager_HotKeyPressed;

            this.Resize += Settings_Resize;
            this.trayIcon.MouseDoubleClick += trayIcon_DoubleClick;

            this.trayIconContextMenu.Items.Add("&Show", null, this.showToolstripItem_Click);
            this.trayIconContextMenu.Items.Add("E&xit", null, this.exitToolstripItem_Click);
        }

        private void Settings_KeyDown(object? sender, KeyEventArgs e)
        {
            foreach (var input in allHotKeyInputs)
            {
                if (input.IsCapturing)
                {
                    input.CaptureKeyDown(sender, e);
                }
            }
        }

        #region Event Handlers
        private void Settings_Resize(object? sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void trayIcon_DoubleClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void showToolstripItem_Click(object? sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        
        private void exitToolstripItem_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void HotKeyManager_HotKeyPressed(object? sender, HotKeyEventArgs e)
        {
            try
            {
                foreach (var input in this.allHotKeyInputs)
                {
                    if (input.HotKey == null) { continue; }

                    if (e.Key == input.HotKey.Key)
                    {
                        if (e.Modifiers == input.HotKey.KeyModifiers)
                        {
                            if (input.HotKeyHandler != null)
                            {
                                input.HotKeyHandler();
                                return;
                            }
                        }
                    }
                }
            }
            catch { } // not great!
        }
        #endregion

        public void RegisterHotKeys()
        {
            foreach(var input in this.allHotKeyInputs)
            {
                input.Register();
            }
        }
        public void SaveSettings()
        {
            var storedSettings = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Rectangle", true);
            if (storedSettings == null)
            {
                storedSettings = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Rectangle", true);
            }
            try
            {
                storedSettings.SetValue("half-left", inputHalfLeft.HotKey != null ? inputHalfLeft.HotKey.ToString() : "");
                storedSettings.SetValue("top-left", inputTopLeft.HotKey != null ? inputTopLeft.HotKey.ToString() : "");
                storedSettings.SetValue("previous-display", inputPreviousDisplay.HotKey != null ? inputPreviousDisplay.HotKey.ToString() : "");
                storedSettings.SetValue("middle-two-thirds", inputMiddleTwoThirds.HotKey != null ? inputMiddleTwoThirds.HotKey.ToString() : "");
                storedSettings.SetValue("bottom-left", inputBottomLeft.HotKey != null ? inputBottomLeft.HotKey.ToString() : "");
                storedSettings.SetValue("unmaximize", inputUnmaximize.HotKey != null ? inputUnmaximize.HotKey.ToString() : "");
                storedSettings.SetValue("bottom-right", inputBottomRight.HotKey != null ? inputBottomRight.HotKey.ToString() : "");
                storedSettings.SetValue("maximize", inputMaximize.HotKey != null ? inputMaximize.HotKey.ToString() : "");
                storedSettings.SetValue("maximize", inputMaximize.HotKey != null ? inputMaximize.HotKey.ToString() : "");
                storedSettings.SetValue("half-right", inputHalfRight.HotKey != null ? inputHalfRight.HotKey.ToString() : "");
                storedSettings.SetValue("top-right", inputTopRight.HotKey != null ? inputTopRight.HotKey.ToString() : "");
                storedSettings.SetValue("next-display", inputNextDisplay.HotKey != null ? inputNextDisplay.HotKey.ToString() : "");
            }
            finally
            {
                storedSettings.Close();
            }
        }
        public void UnregisterHotKeys()
        {
            foreach(var input in this.allHotKeyInputs)
            {
                input.Unregister();
            }
        }

        #region Movement Handlers
        private void Maximize()
        {
            var window = Win32Util.GetForegroundWindow();
            this.SetWindowMaximizedState(window, ShowWindowCommands.Maximized);
        }
        private void Unmaximize()
        {
            var window = Win32Util.GetForegroundWindow();
            this.SetWindowMaximizedState(window, ShowWindowCommands.Normal);
        }
        private void HalfLeft()
        {
            var window = Win32Util.GetForegroundWindow();
            this.SetWindowMaximizedState(window, ShowWindowCommands.Normal);
            var monitor = Win32Util.GetMonitorInfo(window);
            var windowRect = Win32Util.GetWindowRectInner(window);

            var windowWidth = windowRect.Right - windowRect.Left;
            var monitorRect = monitor.rcMonitor;
            var taskbarRect = Win32Util.GetTaskBarRect();
            var taskbarHeight = taskbarRect.Bottom - taskbarRect.Top;
            var monitorWidth = monitorRect.Right - monitorRect.Left;

            var half = monitorRect.Left + (monitorWidth >> 1);
            var third = monitorRect.Left + (monitorWidth / 3);
            var twoThird = monitorRect.Left + ((monitorWidth / 3) << 1);

            var newCorners = new List<Int32>() {
                half,
                twoThird,
                third,
            };
            var newBottom = this.IsPrimary() ? monitorRect.Bottom - taskbarHeight : monitorRect.Bottom;
            var newRight = half;
            if (
                windowRect.Top == monitorRect.Top &&
                windowRect.Bottom == newBottom &&
                newCorners.Any(_ => _ == windowRect.Right)
            )
            {
                var currentIndex = newCorners.IndexOf(windowRect.Right);
                if (currentIndex >= 0)
                {
                    newRight = newCorners[(currentIndex + 1) % newCorners.Count];
                }
            }

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = monitorRect.Left,
                Top = monitorRect.Top,
                Right = newRight,
                Bottom = newBottom,
            });
        }
        private void HalfRight()
        {
            var window = Win32Util.GetForegroundWindow();
            this.SetWindowMaximizedState(window, ShowWindowCommands.Normal);
            var monitor = Win32Util.GetMonitorInfo(window);
            var windowRect = Win32Util.GetWindowRectInner(window);

            var windowWidth = windowRect.Right - windowRect.Left;
            var monitorRect = monitor.rcMonitor;
            var taskbarRect = Win32Util.GetTaskBarRect();
            var taskbarHeight = taskbarRect.Bottom - taskbarRect.Top;

            var monitorWidth = monitorRect.Right - monitorRect.Left;

            var half = monitorRect.Right - (monitorWidth >> 1);
            var third = monitorRect.Right - (monitorWidth / 3);
            var twoThird = monitorRect.Right - ((monitorWidth / 3) << 1);

            var newCorners = new List<Int32>() {
                half,
                twoThird,
                third,
            };
            var newBottom = this.IsPrimary() ? monitorRect.Bottom - taskbarHeight : monitorRect.Bottom;
            var newLeft = half;
            if (
                windowRect.Top == monitorRect.Top &&
                windowRect.Bottom == newBottom &&
                newCorners.Any(_ => _ == windowRect.Left)
            )
            {
                var currentIndex = newCorners.IndexOf(windowRect.Left);
                if (currentIndex >= 0)
                {
                    newLeft = newCorners[(currentIndex + 1) % newCorners.Count];
                }
            }

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = newLeft,
                Top = monitorRect.Top,
                Right = monitorRect.Right,
                Bottom = newBottom,
            });
        }
        private void MiddleTwoThirds()
        {
            var window = Win32Util.GetForegroundWindow();
            this.SetWindowMaximizedState(window, ShowWindowCommands.Normal);
            var monitor = Win32Util.GetMonitorInfo(window);
            var windowRect = Win32Util.GetWindowRectInner(window);

            var windowWidth = windowRect.Right - windowRect.Left;

            var monitorRect = monitor.rcMonitor;
            var taskbarRect = Win32Util.GetTaskBarRect();
            var taskbarHeight = taskbarRect.Bottom - taskbarRect.Top;

            var monitorWidth = Math.Abs(monitorRect.Right - monitorRect.Left);

            var half = monitorWidth >> 1;
            var third = monitorWidth / 3;
            var twoThird = third << 1;

            var newWidth = half;
            if (windowWidth == half)
            {
                newWidth = twoThird;
            } 
            else if (windowWidth == third)
            {
                newWidth = half;
            }
            else if (windowWidth == twoThird)
            {
                newWidth = third;
            }

            var midpoint = monitorRect.Left + (monitorWidth >> 1);

            var newLeft = midpoint - (newWidth >> 1);
            var newRight = midpoint + (newWidth >> 1);

            var newBottom = this.IsPrimary() ? monitorRect.Bottom - taskbarHeight : monitorRect.Bottom;

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = newLeft,
                Top = monitorRect.Top,
                Right = newRight,
                Bottom = newBottom,
            });
        }
        private void TopLeft()
        {
            var window = Win32Util.GetForegroundWindow();
            this.SetWindowMaximizedState(window, ShowWindowCommands.Normal);
            var monitor = Win32Util.GetMonitorInfo(window);
            var windowRect = Win32Util.GetWindowRectInner(window);
            var windowWidth = windowRect.Right - windowRect.Left;
            var monitorRect = monitor.rcMonitor;
            var monitorWidth = monitorRect.Right - monitorRect.Left;
            var monitorHeight = monitorRect.Bottom - monitorRect.Top;

            var half = monitorRect.Left + (monitorWidth >> 1);
            var third = monitorRect.Left + (monitorWidth / 3);
            var twoThird = monitorRect.Left + ((monitorWidth / 3) << 1);

            var newCorners = new List<Int32>() {
                half,
                twoThird,
                third,
            };
            var newBottom = monitorRect.Top + (monitorHeight >> 1);
            var newRight = half;
            if (
                windowRect.Top == monitorRect.Top &&
                windowRect.Bottom == newBottom &&
                newCorners.Any(_ => _ == windowRect.Right)
            )
            {
                var currentIndex = newCorners.IndexOf(windowRect.Right);
                if (currentIndex >= 0)
                {
                    newRight = newCorners[(currentIndex + 1) % newCorners.Count];
                }
            }

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = monitorRect.Left,
                Top = monitorRect.Top,
                Right = newRight,
                Bottom = newBottom,
            });
        }
        private void TopRight()
        {
            var window = Win32Util.GetForegroundWindow();
            this.SetWindowMaximizedState(window, ShowWindowCommands.Normal);
            var monitor = Win32Util.GetMonitorInfo(window);
            var windowRect = Win32Util.GetWindowRectInner(window);
            var windowWidth = windowRect.Right - windowRect.Left;
            var monitorRect = monitor.rcMonitor;
            var monitorWidth = monitorRect.Right - monitorRect.Left;
            var monitorHeight = monitorRect.Bottom - monitorRect.Top;

            var half = monitorRect.Right - (monitorWidth >> 1);
            var third = monitorRect.Right - (monitorWidth / 3);
            var twoThird = monitorRect.Right - ((monitorWidth / 3) << 1);

            var newCorners = new List<Int32>() {
                half,
                twoThird,
                third,
            };

            var newBottom = monitorRect.Top + (monitorHeight >> 1);
            var newLeft = half;
            if (
                windowRect.Top == monitorRect.Top &&
                windowRect.Bottom == newBottom &&
                newCorners.Any(_ => _ == windowRect.Left)
            )
            {
                var currentIndex = newCorners.IndexOf(windowRect.Left);
                if (currentIndex >= 0)
                {
                    newLeft = newCorners[(currentIndex + 1) % newCorners.Count];
                }
            }

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = newLeft,
                Top = monitorRect.Top,
                Right = monitorRect.Right,
                Bottom = newBottom,
            });
        }
        private void BottomLeft()
        {
            var window = Win32Util.GetForegroundWindow();
            this.SetWindowMaximizedState(window, ShowWindowCommands.Normal);
            var monitor = Win32Util.GetMonitorInfo(window);
            var windowRect = Win32Util.GetWindowRectInner(window);
            var monitorRect = monitor.rcMonitor;

            var taskbarRect = Win32Util.GetTaskBarRect();
            var taskbarHeight = taskbarRect.Bottom - taskbarRect.Top;

            var monitorWidth = monitorRect.Right - monitorRect.Left;
            var monitorHeight = monitorRect.Bottom - monitorRect.Top;

            var half = monitorRect.Left + (monitorWidth >> 1);
            var third = monitorRect.Left + (monitorWidth / 3);
            var twoThird = monitorRect.Left + ((monitorWidth / 3) << 1);

            var newCorners = new List<Int32>() {
                half,
                twoThird,
                third,
            };
            var newTop = monitorRect.Top + (monitorHeight >> 1);
            var newBottom = this.IsPrimary() ? monitorRect.Bottom - taskbarHeight : monitorRect.Bottom;
            var newRight = half;
            if (
                windowRect.Top == newTop &&
                windowRect.Bottom == newBottom &&
                newCorners.Any(_ => _ == windowRect.Right)
            )
            {
                var currentIndex = newCorners.IndexOf(windowRect.Right);
                if (currentIndex >= 0)
                {
                    newRight = newCorners[(currentIndex + 1) % newCorners.Count];
                }
            }

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = monitorRect.Left,
                Top = newTop,
                Right = newRight,
                Bottom = newBottom,
            });
        }
        private void BottomRight()
        {
            var window = Win32Util.GetForegroundWindow();
            this.SetWindowMaximizedState(window, ShowWindowCommands.Normal);
            var monitor = Win32Util.GetMonitorInfo(window);
            var windowRect = Win32Util.GetWindowRectInner(window);

            var monitorRect = monitor.rcMonitor;

            var taskbarRect = Win32Util.GetTaskBarRect();
            var taskbarHeight = taskbarRect.Bottom - taskbarRect.Top;

            var monitorWidth = monitorRect.Right - monitorRect.Left;
            var monitorHeight = monitorRect.Bottom - monitorRect.Top;

            var half = monitorRect.Right - (monitorWidth >> 1);
            var third = monitorRect.Right - (monitorWidth / 3);
            var twoThird = monitorRect.Right - ((monitorWidth / 3) << 1);

            var newCorners = new List<Int32>() {
                half,
                twoThird,
                third,
            };
            var newTop = monitorRect.Top + (monitorHeight >> 1);
            var newBottom = this.IsPrimary() ? monitorRect.Bottom - taskbarHeight : monitorRect.Bottom;
            var newLeft = half;
            if (
                windowRect.Top == newTop &&
                windowRect.Bottom == newBottom &&
                newCorners.Any(_ => _ == windowRect.Left)
            )
            {
                var currentIndex = newCorners.IndexOf(windowRect.Left);
                if (currentIndex >= 0)
                {
                    newLeft = newCorners[(currentIndex + 1) % newCorners.Count];
                }
            }

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = newLeft,
                Top = newTop,
                Right = monitorRect.Right,
                Bottom = newBottom,
            });
        }
        private void PreviousDisplay()
        {
            var window = Win32Util.GetForegroundWindow();
            this.SetWindowMaximizedState(window, ShowWindowCommands.Normal);
            var windowRect = Win32Util.GetWindowRect(window);

            var windowWidth = windowRect.Right - windowRect.Left;
            var windowHeight = windowRect.Bottom - windowRect.Top;

            var monitorIndex = this.GetMonitorIndex() - 1;
            if (monitorIndex < 0)
            {
                monitorIndex = Screen.AllScreens.Length - 1;
            }
            var nextScreen = Screen.AllScreens[monitorIndex];

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = nextScreen.Bounds.X,
                Top = nextScreen.Bounds.Y,
                Right = nextScreen.Bounds.X + windowWidth,
                Bottom = nextScreen.Bounds.Y + windowHeight
            });
        }
        private void NextDisplay()
        {
            var window = Win32Util.GetForegroundWindow();
            this.SetWindowMaximizedState(window, ShowWindowCommands.Normal);
            var windowRect = Win32Util.GetWindowRect(window);

            var windowWidth = windowRect.Right - windowRect.Left;
            var windowHeight = windowRect.Bottom - windowRect.Top;

            var monitorIndex = this.GetMonitorIndex();
            var nextScreen = Screen.AllScreens[(monitorIndex + 1) % Screen.AllScreens.Length];

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = nextScreen.Bounds.X,
                Top = nextScreen.Bounds.Y,
                Right = nextScreen.Bounds.X + windowWidth,
                Bottom = nextScreen.Bounds.Y + windowHeight
            });
        }
        #endregion

        #region Helpers
        private void SetWindowMaximizedState(IntPtr hWnd, ShowWindowCommands state)
        {
            var windowPos = Win32Util.GetWindowPlacement(hWnd);
            switch (state)
            {
                case ShowWindowCommands.Normal:
                    if (windowPos.showCmd == state)
                    {
                        return;
                    }
                    windowPos.showCmd = state;
                    Win32.SetWindowPlacement(hWnd, ref windowPos);
                    break;
                case ShowWindowCommands.Maximized:
                    if (windowPos.showCmd == state)
                    {
                        return;
                    }
                    windowPos.showCmd = state;
                    Win32.SetWindowPlacement(hWnd, ref windowPos);
                    break;
            }
        }

        private bool IsPrimary()
        {
            var window = Win32Util.GetForegroundWindow();
            var monitor = Win32Util.GetMonitorInfo(window);
            return monitor.dwFlags == 0x1;
        }

        private int GetMonitorIndex()
        {
            var window = Win32Util.GetForegroundWindow();
            var monitor = Win32Util.GetMonitorInfo(window);
            var monitorDeviceName = new String(monitor.szDevice);
            var monitorDeviceNameClean = monitorDeviceName.Substring(0, monitorDeviceName.IndexOf((char)0));

            for (int index = 0; index < Screen.AllScreens.Length; index++)
            {
                var screen = Screen.AllScreens[index];
                if (screen.DeviceName == monitorDeviceNameClean)
                {
                    return index;
                }
            }
            throw new Exception($"Could not find index of monitor with device name {monitorDeviceNameClean}");
        }
        #endregion
    }
}

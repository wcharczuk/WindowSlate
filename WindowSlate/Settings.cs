using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowSlate
{
    public struct HotKeyInfo
    {
        public HotKeyInfo()
        {
            this.Description = "";
            this.SettingsKey = "";
            this.DefaultHotKey = new HotKey();
            this.MovementHandler = () => { };
            this.Input = new HotKeyInput();
        }

        public string Description { get; set; }
        public string SettingsKey { get; set; }
        public HotKey DefaultHotKey { get; set; }
        public Action MovementHandler { get; set; }
        public HotKeyInput Input { get; set; }
    }

    public partial class Settings : Form
    {
        private readonly List<HotKeyInfo> hotkeys;
    
        static string SETTINGS_KEY_START_MINIMIZED = "start-minimized";

        public Settings()
        {
            this.KeyPreview = true;
            this.ShowInTaskbar = false;

            this.hotkeys = new()
            {
                new HotKeyInfo() with {
                    Description = "Maximize",
                    SettingsKey = "maximize",
                    DefaultHotKey = new HotKey(Keys.K, KeyModifiers.Control | KeyModifiers.Alt),
                    MovementHandler = this.Maximize
                },
                new HotKeyInfo() with {
                    Description = "Unmaximize",
                    SettingsKey = "unmaximize",
                    DefaultHotKey = new HotKey(Keys.J, KeyModifiers.Control | KeyModifiers.Alt),
                    MovementHandler = this.Unmaximize
                },
                new HotKeyInfo() with {
                    Description = "Middle",
                    SettingsKey = "middle-two-thirds",
                    DefaultHotKey = new HotKey(Keys.I, KeyModifiers.Control | KeyModifiers.Alt),
                    MovementHandler = this.MiddleTwoThirds
                },
                new HotKeyInfo() with {
                    Description = "Half-Left",
                    SettingsKey = "half-left",
                    DefaultHotKey = new HotKey(Keys.H, KeyModifiers.Control | KeyModifiers.Alt),
                    MovementHandler = this.HalfLeft,
                },
                new HotKeyInfo() with {
                    Description = "Half-Right",
                    SettingsKey = "half-right",
                    DefaultHotKey = new HotKey(Keys.L, KeyModifiers.Control | KeyModifiers.Alt),
                    MovementHandler = this.HalfRight,
                },

                new HotKeyInfo() with {
                    Description = "Top-Left",
                    SettingsKey = "top-left",
                    DefaultHotKey = new HotKey(Keys.H, KeyModifiers.Control | KeyModifiers.Windows),
                    MovementHandler = this.TopLeft,
                },
                new HotKeyInfo() with {
                    Description = "Top-Right",
                    SettingsKey = "top-right",
                    DefaultHotKey = new HotKey(Keys.L, KeyModifiers.Control | KeyModifiers.Windows),
                    MovementHandler = this.TopRight,
                },
                new HotKeyInfo() with {
                    Description = "Bottom-Left",
                    SettingsKey = "bottom-left",
                    DefaultHotKey = new HotKey(Keys.J, KeyModifiers.Control | KeyModifiers.Windows),
                    MovementHandler = this.BottomLeft,
                },
                new HotKeyInfo() with {
                    Description = "Bottom-Right",
                    SettingsKey = "bottom-right",
                    DefaultHotKey = new HotKey(Keys.K, KeyModifiers.Control | KeyModifiers.Windows),
                    MovementHandler = this.BottomRight,
                },

                new HotKeyInfo() with {
                    Description = "Previous-Display",
                    SettingsKey = "previous-display",
                    DefaultHotKey = new HotKey(Keys.L, KeyModifiers.Control | KeyModifiers.Shift | KeyModifiers.Alt),
                    MovementHandler = this.PreviousDisplay,
                },
                new HotKeyInfo() with {
                    Description = "Next-display",
                    SettingsKey = "next-display",
                    DefaultHotKey = new HotKey(Keys.H, KeyModifiers.Control | KeyModifiers.Shift | KeyModifiers.Alt),
                    MovementHandler = this.NextDisplay,
                },
            };

            var storedSettings = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\WindowSlate");
            if (storedSettings == null)
            {
                storedSettings = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\WindowSlate");
            }

            try
            {
                foreach (var hotkey in this.hotkeys)
                {
                    hotkey.Input.Description = hotkey.Description;
                    hotkey.Input.RegisterHotKeysHandler = this.RegisterHotKeys;
                    hotkey.Input.UnregisterHotKeysHandler = this.UnregisterHotKeys;
                    hotkey.Input.SaveSettingsHandler = this.SaveSettings;

                    var setting = storedSettings.GetValue(hotkey.SettingsKey);
                    if (setting != null)
                    {
                        if ((string)setting != "")
                        {
                            try
                            {
                                var parsedHotkey = new HotKey((string)setting);
                                hotkey.Input.HotKey = parsedHotkey;
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "Settings Error");
                            }
                        }
                    }
                    else
                    {
                        hotkey.Input.HotKey = hotkey.DefaultHotKey;
                    }
                }
            }
            finally
            {
                storedSettings.Close();
            }

            InitializeComponent();

            using (var runOnStart = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run"))
            {
                if (runOnStart != null)
                {
                    var runOnStartSetting = runOnStart.GetValue("WindowSlate");
                    runOnStartCheckbox.Checked = runOnStartSetting != null;
                }
            }
            using (storedSettings = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\WindowSlate"))
            {
                if (storedSettings != null)
                {
                    var startMinimizedSetting = storedSettings.GetValue(SETTINGS_KEY_START_MINIMIZED);
                    startMinimizedCheckbox.Checked = startMinimizedSetting != null;
                }
            }

            // inputY is the position we start drawing controls from within the settings window
            var inputY = 20;
            groupBox.SuspendLayout();
            this.SuspendLayout();
            foreach (var hotkey in hotkeys)
            {
                groupBox.Controls.Add(hotkey.Input);
                hotkey.Input.Location = new Point(20, inputY);
                inputY += hotkey.Input.Height;
            }
            this.trayIconContextMenu.Items.Add("&Show", null, this.showToolstripItem_Click);
            this.trayIconContextMenu.Items.Add("E&xit", null, this.exitToolstripItem_Click);

            groupBox.ResumeLayout();
            this.ResumeLayout();

            // register event handlers
            this.KeyDown += Settings_KeyDown;
            HotKeyManager.HotKeyPressed += HotKeyManager_HotKeyPressed;
            this.Resize += Settings_Resize;
            this.trayIcon.MouseDoubleClick += trayIcon_DoubleClick;
            this.runOnStartCheckbox.CheckedChanged += runOnStartCheckbox_CheckedChanged;
            this.startMinimizedCheckbox.CheckedChanged += startMinimizedCheckbox_CheckedChanged;

            if (startMinimizedCheckbox.Checked)
            {
                this.SelfMinimizeToTray();
            }
        }

        #region Event Handlers

        private void Settings_Resize(object? sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.SelfMinimizeToTray();
            }
        }

        private void Settings_KeyDown(object? sender, KeyEventArgs e)
        {
            foreach (var hotkey in hotkeys)
            {
                if (hotkey.Input.IsCapturing)
                {
                    hotkey.Input.CaptureKeyDown(sender, e);
                }
            }
        }

        private void trayIcon_DoubleClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.SelfShowFromTray();
            }
        }

        private void showToolstripItem_Click(object? sender, EventArgs e)
        {
            this.SelfShowFromTray();
        }

        private void exitToolstripItem_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void HotKeyManager_HotKeyPressed(object? sender, HotKeyEventArgs e)
        {
            try
            {
                // TODO(wc): we can ~likely~ make this faster
                // by storing the hotkeys as a hash of their string form
                // and look them up by that hash but this works fine for now!
                foreach (var hotkey in this.hotkeys)
                {
                    if (hotkey.Input.HotKey == null) { continue; }

                    if (e.Key == hotkey.Input.HotKey.Key)
                    {
                        if (e.Modifiers == hotkey.Input.HotKey.KeyModifiers)
                        {
                            hotkey.MovementHandler();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                this.trayIcon.ShowBalloonTip(5000, "Window Movement Error", ex.ToString(), ToolTipIcon.Error);
            }
        }
       
        private void runOnStartCheckbox_CheckedChanged(object? sender, EventArgs e)
        {
            using (var runOnStart = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", /*writable*/ true))
            {
                if (runOnStart != null)
                {
                    if (this.runOnStartCheckbox.Checked)
                    {
                        var args = Environment.GetCommandLineArgs();
                        if (args != null && args.Length > 0)
                        {
                            var exePath = args[0];
                            if (exePath.EndsWith(".dll"))
                            {
                                exePath = TrimSuffix(exePath, ".dll") + ".exe";
                            }
                            runOnStart.SetValue("WindowSlate", exePath);
                        }
                    }
                    else
                    {
                        runOnStart.DeleteValue("WindowSlate");
                    }
                }
            }
        }

        private void startMinimizedCheckbox_CheckedChanged(object? sender, EventArgs e)
        {
            var storedSettings = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\WindowSlate", true);
            if (storedSettings == null)
            {
                storedSettings = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\WindowSlate");
            }
            try
            {
                if(startMinimizedCheckbox.Checked)
                {
                    storedSettings.SetValue(SETTINGS_KEY_START_MINIMIZED, "true");
                }
                else
                {
                    if (storedSettings.GetValue(SETTINGS_KEY_START_MINIMIZED) != null)
                    {
                        storedSettings.DeleteValue(SETTINGS_KEY_START_MINIMIZED);
                    }
                }
            }
            finally
            {
                storedSettings.Close();
            }
        }

        #endregion

        #region Input Helpers
        
        public void RegisterHotKeys()
        {
            foreach (var hotkey in this.hotkeys)
            {
                hotkey.Input.Register();
            }
        }

        public void SaveSettings()
        {
            var storedSettings = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\WindowSlate", true);
            if (storedSettings == null)
            {
                storedSettings = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\WindowSlate", true);
            }
            try
            {
                foreach (var hotkey in hotkeys)
                {
                    if (hotkey.Input != null && hotkey.Input.HotKey != null)
                    {
                        storedSettings.SetValue(hotkey.SettingsKey, hotkey.Input.HotKey != null ? hotkey.Input.HotKey.ToString() : "");
                    }
                }
            }
            catch(Exception ex)
            {
                this.trayIcon.ShowBalloonTip(5000, "Save Settings Error", ex.ToString(), ToolTipIcon.Error);
            }
            finally
            {
                storedSettings.Close();
                this.trayIcon.ShowBalloonTip(5000, "Status", "Settings Saved!", ToolTipIcon.Info);
            }
        }

        public void UnregisterHotKeys()
        {
            foreach (var hotkey in this.hotkeys)
            {
                hotkey.Input.Unregister();
            }
        }
        
        #endregion

        #region Movement Handlers

        private void Maximize()
        {
            var window = Win32Util.GetForegroundWindow();
            this.SetTargetWindowMaximizedState(window, ShowWindowCommands.Maximized);
        }

        private void Unmaximize()
        {
            var window = Win32Util.GetForegroundWindow();
            this.SetTargetWindowMaximizedState(window, ShowWindowCommands.Normal);
        }

        private void HalfLeft()
        {
            var window = Win32Util.GetForegroundWindow();
            this.SetTargetWindowMaximizedState(window, ShowWindowCommands.Normal);
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
            var newBottom = this.IsMonitorPrimary() ? monitorRect.Bottom - taskbarHeight : monitorRect.Bottom;
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
            this.SetTargetWindowMaximizedState(window, ShowWindowCommands.Normal);
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
            var newBottom = this.IsMonitorPrimary() ? monitorRect.Bottom - taskbarHeight : monitorRect.Bottom;
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
            this.SetTargetWindowMaximizedState(window, ShowWindowCommands.Normal);
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

            var newBottom = this.IsMonitorPrimary() ? monitorRect.Bottom - taskbarHeight : monitorRect.Bottom;

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
            this.SetTargetWindowMaximizedState(window, ShowWindowCommands.Normal);
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
            this.SetTargetWindowMaximizedState(window, ShowWindowCommands.Normal);
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
            this.SetTargetWindowMaximizedState(window, ShowWindowCommands.Normal);
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
            var newBottom = this.IsMonitorPrimary() ? monitorRect.Bottom - taskbarHeight : monitorRect.Bottom;
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
            this.SetTargetWindowMaximizedState(window, ShowWindowCommands.Normal);
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
            var newBottom = this.IsMonitorPrimary() ? monitorRect.Bottom - taskbarHeight : monitorRect.Bottom;
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
            this.SetTargetWindowMaximizedState(window, ShowWindowCommands.Normal);
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
            this.SetTargetWindowMaximizedState(window, ShowWindowCommands.Normal);
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

        private void SelfMinimizeToTray()
        {
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            this.trayIcon.Visible = true;
        }

        private void SelfShowFromTray()
        {
            this.Show();
            this.trayIcon.Visible = false;
            this.WindowState = FormWindowState.Normal;
        }

        private void SetTargetWindowMaximizedState(IntPtr hWnd, ShowWindowCommands state)
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

        /// <summary>
        /// IsMonitorPrimary returns if the current "foreground" window is the "primary" monitor.
        /// 
        /// This specifically affects if the taskbar is drawn on the monitor, and we need to account
        /// for the taskbar height when we size windows.
        /// </summary>
        /// <returns></returns>
        private bool IsMonitorPrimary()
        {
            var window = Win32Util.GetForegroundWindow();
            var monitor = Win32Util.GetMonitorInfo(window);
            return monitor.dwFlags == 0x1;
        }

        /// <summary>
        /// GetMonitorIndex gets the monitor "index" of the current "foreground" window.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        // TrimSuffix is a helper string function that removes a suffix from a string
        // but only if it's present at the end of the string.
        private static string TrimSuffix(string str, string suffix)
        {
            if (str.EndsWith(suffix))
            {
                return str.Substring(0, str.Length - suffix.Length);
            }
            else
            {
                return str;
            }
        }
        #endregion
    }
}

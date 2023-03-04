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
        public Settings()
        {
            InitializeComponent();

            HotKeyManager.RegisterHotKey(Keys.H, KeyModifiers.Control | KeyModifiers.Windows);
            HotKeyManager.RegisterHotKey(Keys.L, KeyModifiers.Control | KeyModifiers.Windows);
            HotKeyManager.RegisterHotKey(Keys.J, KeyModifiers.Control | KeyModifiers.Windows);
            HotKeyManager.RegisterHotKey(Keys.K, KeyModifiers.Control | KeyModifiers.Windows);

            HotKeyManager.RegisterHotKey(Keys.H, KeyModifiers.Control | KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.I, KeyModifiers.Control | KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.L, KeyModifiers.Control | KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.J, KeyModifiers.Control | KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.K, KeyModifiers.Control | KeyModifiers.Alt);

            HotKeyManager.RegisterHotKey(Keys.H, KeyModifiers.Control | KeyModifiers.Shift | KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.L, KeyModifiers.Control | KeyModifiers.Shift | KeyModifiers.Alt);

            HotKeyManager.HotKeyPressed += HotKeyManager_HotKeyPressed;

            this.Resize += Settings_Resize;
            this.trayIcon.MouseDoubleClick += NotifyIcon1_DoubleClick;

            this.trayIconContextMenu.Items.Add("&Show", null, this.showToolstripItem_Click);
            this.trayIconContextMenu.Items.Add("E&xit", null, this.exitToolstripItem_Click);
        }

        private void Settings_Resize(object? sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void NotifyIcon1_DoubleClick(object? sender, MouseEventArgs e)
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
            switch (e.Key)
            {
                case Keys.H:
                    if (e.Modifiers == (KeyModifiers.Control | KeyModifiers.Windows))
                    {
                        this.TopLeft();
                    }
                    else if (e.Modifiers == (KeyModifiers.Control | KeyModifiers.Alt))
                    {
                        this.HalfLeft();
                    }
                    else if (e.Modifiers == (KeyModifiers.Control | KeyModifiers.Shift | KeyModifiers.Alt))
                    {
                        this.PreviousDisplay();
                    }
                    break;
                case Keys.I:
                    if (e.Modifiers == (KeyModifiers.Control | KeyModifiers.Alt))
                    {
                        this.MiddleTwoThirds();
                    }
                    break;
                case Keys.L:
                    if (e.Modifiers == (KeyModifiers.Control | KeyModifiers.Windows))
                    {
                        this.TopRight();
                    }
                    else if (e.Modifiers == (KeyModifiers.Control | KeyModifiers.Alt))
                    {
                        this.HalfRight();
                    }
                    else if (e.Modifiers == (KeyModifiers.Control | KeyModifiers.Shift | KeyModifiers.Alt))
                    {
                        this.NextDisplay();
                    }
                    break;
                case Keys.J:
                    if (e.Modifiers == (KeyModifiers.Control | KeyModifiers.Windows))
                    {
                        this.BottomLeft();
                    }
                    else if (e.Modifiers == (KeyModifiers.Control | KeyModifiers.Alt))
                    {
                        this.Unmaximize();
                    }
                    break;
                case Keys.K:
                    if (e.Modifiers == (KeyModifiers.Control | KeyModifiers.Windows))
                    {
                        this.BottomRight();
                    }
                    else if (e.Modifiers == (KeyModifiers.Control | KeyModifiers.Alt))
                    {
                        this.Maximize();
                    }
                    else if (e.Modifiers == (KeyModifiers.Control | KeyModifiers.Shift | KeyModifiers.Alt))
                    {
                        this.FullScreen();
                    }
                    break;
            }
        }

        private void FullScreen()
        {
            var window = Win32Util.GetForegroundWindow();
            var monitor = Win32Util.GetMonitorInfo(window);
            var windowRect = Win32Util.GetWindowRect(window);
            var monitorRect = monitor.rcMonitor;
            var taskbarRect = Win32Util.GetTaskBarRect();
            var taskbarHeight = taskbarRect.Bottom - taskbarRect.Top;

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = monitorRect.Left,
                Top = monitorRect.Top,
                Right = monitorRect.Right,
                Bottom = this.IsPrimary() ? monitorRect.Bottom - taskbarHeight : monitorRect.Bottom,
            });
        }

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

            var monitorWidth = monitorRect.Right - monitorRect.Left;

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

            var midpoint = monitorRect.Left + monitorWidth >> 1;

            var newLeft = midpoint - (newWidth>>1);
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
    }
}

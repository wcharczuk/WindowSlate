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
            HotKeyManager.RegisterHotKey(Keys.L, KeyModifiers.Control | KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.J, KeyModifiers.Control | KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.K, KeyModifiers.Control | KeyModifiers.Alt);

            HotKeyManager.RegisterHotKey(Keys.H, KeyModifiers.Control | KeyModifiers.Shift | KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.L, KeyModifiers.Control | KeyModifiers.Shift | KeyModifiers.Alt);

            HotKeyManager.HotKeyPressed += HotKeyManager_HotKeyPressed;

            this.Resize += Settings_Resize;
            this.notifyIcon1.MouseClick += NotifyIcon1_Click;
            this.notifyIcon1.MouseDoubleClick += NotifyIcon1_Click;
        }

        private void Settings_Resize(object? sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void NotifyIcon1_Click(object? sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
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
                    break;
            }
        }

        private void Maximize()
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

        private void HalfLeft()
        {
            var window = Win32Util.GetForegroundWindow();
            var monitor = Win32Util.GetMonitorInfo(window);
            var windowRect = Win32Util.GetWindowRectInner(window);

            var windowWidth = windowRect.Right - windowRect.Left;
            var monitorRect = monitor.rcMonitor;
            var taskbarRect = Win32Util.GetTaskBarRect();
            var taskbarHeight = taskbarRect.Bottom - taskbarRect.Top;
            var monitorWidth = monitorRect.Right - monitorRect.Left;

            var half = monitorWidth >> 1;
            var third = monitorWidth / 3;
            var twoThird = (monitorWidth / 3) << 1;

            var newWidth = half; // default is 1/2
            if (windowWidth == third)
            {
                newWidth = half;
            }
            else if (windowWidth == half)
            {
                newWidth = twoThird;
            }
            else if (windowWidth == twoThird)
            {
                newWidth = third;
            }

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = monitorRect.Left,
                Top = monitorRect.Top,
                Right = monitorRect.Left + newWidth,
                Bottom = this.IsPrimary() ? monitorRect.Bottom - taskbarHeight : monitorRect.Bottom,
            });
        }
        private void HalfRight()
        {
            var window = Win32Util.GetForegroundWindow();
            var monitor = Win32Util.GetMonitorInfo(window);
            var windowRect = Win32Util.GetWindowRectInner(window);

            var windowWidth = windowRect.Right - windowRect.Left;
            var monitorRect = monitor.rcMonitor;
            var taskbarRect = Win32Util.GetTaskBarRect();
            var taskbarHeight = taskbarRect.Bottom - taskbarRect.Top;

            var monitorWidth = monitorRect.Right - monitorRect.Left;

            var half = monitorWidth >> 1;
            var third = monitorWidth / 3;
            var twoThird = (monitorWidth / 3) << 1;

            var newWidth = half; // default is 1/2
            if (windowWidth == third)
            {
                newWidth = half;
            }
            else if (windowWidth == half)
            {
                newWidth = twoThird;
            }
            else if (windowWidth == twoThird)
            {
                newWidth = third;
            }

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = monitorRect.Right - newWidth,
                Top = monitorRect.Top,
                Right = monitorRect.Right,
                Bottom = this.IsPrimary() ? monitorRect.Bottom - taskbarHeight : monitorRect.Bottom,
            });
        }

        private void TopLeft()
        {
            var window = Win32Util.GetForegroundWindow();
            var monitor = Win32Util.GetMonitorInfo(window);
            var windowRect = Win32Util.GetWindowRect(window);
            var monitorRect = monitor.rcMonitor;
            var monitorWidth = monitorRect.Right - monitorRect.Left;
            var monitorHeight = monitorRect.Bottom - monitorRect.Top;

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = monitorRect.Left,
                Top = monitorRect.Top,
                Right = monitorRect.Left + (monitorWidth >> 1),
                Bottom = monitorRect.Top + (monitorHeight >> 1)
            });
        }
        private void TopRight()
        {
            var window = Win32Util.GetForegroundWindow();
            var monitor = Win32Util.GetMonitorInfo(window);
            var windowRect = Win32Util.GetWindowRect(window);
            var monitorRect = monitor.rcMonitor;
            var monitorWidth = monitorRect.Right - monitorRect.Left;
            var monitorHeight = monitorRect.Bottom - monitorRect.Top;

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = monitorRect.Left + (monitorWidth >> 1),
                Top = monitorRect.Top,
                Right = monitorRect.Right,
                Bottom = monitorRect.Top + (monitorHeight >> 1),
            });
        }

        private void BottomLeft()
        {
            var window = Win32Util.GetForegroundWindow();
            var monitor = Win32Util.GetMonitorInfo(window);
            var windowRect = Win32Util.GetWindowRect(window);
            var monitorRect = monitor.rcMonitor;

            var taskbarRect = Win32Util.GetTaskBarRect();
            var taskbarHeight = taskbarRect.Bottom - taskbarRect.Top;

            var monitorWidth = monitorRect.Right - monitorRect.Left;
            var monitorHeight = monitorRect.Bottom - monitorRect.Top;

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = monitorRect.Left,
                Top = monitorRect.Top + (monitorHeight >> 1),
                Right = monitorRect.Left + (monitorWidth >> 1),
                Bottom = this.IsPrimary() ? monitorRect.Bottom - taskbarHeight : monitorRect.Bottom,
            });
        }
        private void BottomRight()
        {
            var window = Win32Util.GetForegroundWindow();
            var monitor = Win32Util.GetMonitorInfo(window);
            var windowRect = Win32Util.GetWindowRect(window);

            var monitorRect = monitor.rcMonitor;

            var taskbarRect = Win32Util.GetTaskBarRect();
            var taskbarHeight = taskbarRect.Bottom - taskbarRect.Top;

            var monitorWidth = monitorRect.Right - monitorRect.Left;
            var monitorHeight = monitorRect.Bottom - monitorRect.Top;

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = monitorRect.Left + (monitorWidth >> 1),
                Top = monitorRect.Top + (monitorHeight >> 1),
                Right = monitorRect.Right,
                Bottom = this.IsPrimary() ? monitorRect.Bottom - taskbarHeight : monitorRect.Bottom,
            });
        }

        private void PreviousDisplay()
        {
            var window = Win32Util.GetForegroundWindow();
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

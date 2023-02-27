using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
                Left = 0,
                Top = 0,
                Right = monitorRect.Right,
                Bottom = monitorRect.Bottom - taskbarHeight
            }); ;
        }
        private void TopLeft()
        {
            var window = Win32Util.GetForegroundWindow();
            var monitor = Win32Util.GetMonitorInfo(window);
            var windowRect = Win32Util.GetWindowRect(window);
            var monitorRect = monitor.rcMonitor;

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = 0,
                Top = 0,
                Right = monitorRect.Right >> 1,
                Bottom = monitorRect.Bottom >> 1
            });
        }
        private void HalfLeft()
        {
            var window = Win32Util.GetForegroundWindow();
            var monitor = Win32Util.GetMonitorInfo(window);
            var windowRect = Win32Util.GetWindowRect(window);
            var monitorRect = monitor.rcMonitor;
            var taskbarRect = Win32Util.GetTaskBarRect();
            var taskbarHeight = taskbarRect.Bottom - taskbarRect.Top;

            // if in 1/3 -> 1/2
            // if in 1/2 -> 2/3
            // if in 2/3 -> 1/3
            // else -> 1/2

            var half = monitorRect.Right >> 1;
            var third = monitorRect.Right / 3;
            var twoThird = (monitorRect.Right / 3) << 1;

            var right = half; // default is 1/2
            if (windowRect.Right == third)
            {
                right = half;
            }
            else if (windowRect.Right == half)
            {
                right = twoThird;
            }
            else if (windowRect.Right == twoThird)
            {
                right = third;
            }

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = 0,
                Top = 0,
                Right = right,
                Bottom = monitorRect.Bottom - taskbarHeight
            });
        }
        private void TopRight()
        {
            var window = Win32Util.GetForegroundWindow();
            var monitor = Win32Util.GetMonitorInfo(window);
            var windowRect = Win32Util.GetWindowRect(window);
            var monitorRect = monitor.rcMonitor;

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = monitorRect.Right >> 1,
                Top = 0,
                Right = monitorRect.Right,
                Bottom = monitorRect.Bottom >> 1
            });
        }
        private void HalfRight()
        {
            var window = Win32Util.GetForegroundWindow();
            var monitor = Win32Util.GetMonitorInfo(window);
            var windowRect = Win32Util.GetWindowRect(window);
            var monitorRect = monitor.rcMonitor;
            var taskbarRect = Win32Util.GetTaskBarRect();
            var taskbarHeight = taskbarRect.Bottom - taskbarRect.Top;

            // if in 1/3 -> 1/2
            // if in 1/2 -> 2/3
            // if in 2/3 -> 1/3
            // else -> 1/2

            var half = monitorRect.Right >> 1;
            var twoThird = monitorRect.Right / 3;
            var third = (monitorRect.Right / 3) << 1;

            var left = half; // default is 1/2
            if (windowRect.Left == third)
            {
                left = half;
            }
            else if (windowRect.Left == half)
            {
                left = twoThird;
            }
            else if (windowRect.Left == twoThird)
            {
                left = third;
            }

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = left,
                Top = 0,
                Right = monitorRect.Right,
                Bottom = monitorRect.Bottom - taskbarHeight
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

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = 0,
                Top = monitorRect.Bottom >> 1,
                Right = monitorRect.Right >> 1,
                Bottom = monitorRect.Bottom - taskbarHeight
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

            Win32Util.MoveWindow(window, windowRect with
            {
                Left = monitorRect.Right >> 1,
                Top = monitorRect.Bottom >> 1,
                Right = monitorRect.Right,
                Bottom = monitorRect.Bottom - taskbarHeight
            });
        }
    }
}

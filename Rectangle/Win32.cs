using System.Runtime.InteropServices;
using System.Text;

namespace Rectangle
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;        // x position of upper-left corner
        public int Top;         // y position of upper-left corner
        public int Right;       // x position of lower-right corner
        public int Bottom;      // y position of lower-right corner
    }

    public enum MonitorFlag : uint
    {
        /// <summary>Returns NULL.</summary>
        MONITOR_DEFAULTTONULL = 0,
        /// <summary>Returns a handle to the primary display monitor.</summary>
        MONITOR_DEFAULTTOPRIMARY = 1,
        /// <summary>Returns a handle to the display monitor that is nearest to the window.</summary>
        MONITOR_DEFAULTTONEAREST = 2
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
    public class MONITORINFOEX
    {
        public int cbSize = Marshal.SizeOf(typeof(MONITORINFOEX));
        public RECT rcMonitor = new RECT();
        public RECT rcWork = new RECT();
        public int dwFlags = 0;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public char[] szDevice = new char[32];
    }

    static class Win32Util
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        public static string GetWindowText(IntPtr hWnd)
        {
            var windowTextLength = Win32.GetWindowTextLength(hWnd);
            var stringBuffer = new StringBuilder(windowTextLength + 1);
            if (!Win32.GetWindowText(hWnd, stringBuffer, windowTextLength))
            {
                throw new Exception($"Could not get window text for window with pointer: {hWnd}");
            }

            return stringBuffer.ToString();
        }

        public static RECT GetWindowRect(IntPtr hWnd)
        {
            RECT rct;
            if (!Win32.GetWindowRect(hWnd, out rct))
            {
                throw new Exception($"Could not get window rect for window with pointer: {hWnd}");
            }
            return rct;
        }

        public static void MoveWindow(IntPtr hWnd, RECT rct)
        {
            if (!Win32.MoveWindow(hWnd, rct.Left, rct.Top, rct.Right - rct.Left, rct.Bottom - rct.Top, true))
            {
                throw new Exception($"Could not move window rect for window with pointer: {hWnd}");
            }
        }

        public static MONITORINFOEX GetMonitorInfo(IntPtr hWnd)
        {
            var hmonitor = Win32.MonitorFromWindow(hWnd, MonitorFlag.MONITOR_DEFAULTTONEAREST);
            var minfo = new MONITORINFOEX();
            Win32.GetMonitorInfo(hmonitor, minfo);
            return minfo;
        }

        public static RECT GetTaskBarRect()
        {
            var taskbar = Win32.FindWindow("Shell_traywnd", "");
            return Win32Util.GetWindowRect(taskbar);
        }
    }

    static class Win32
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowText(IntPtr hWnd, StringBuilder lpString,
            int nMaxCount);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int Width, int Height, bool Repaint);

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hWnd, MonitorFlag flag);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMonitorInfo(IntPtr hmonitor, [In, Out] MONITORINFOEX info);

        // Registers a hot key with Windows.
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        // Unregisters the hot key with Windows.
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }
}

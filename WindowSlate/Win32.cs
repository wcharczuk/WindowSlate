using System.Runtime.InteropServices;
using System.Text;

namespace WindowSlate
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

    [Flags]
    public enum DwmWindowAttribute : uint
    {
        DWMWA_NCRENDERING_ENABLED = 1,
        DWMWA_NCRENDERING_POLICY,
        DWMWA_TRANSITIONS_FORCEDISABLED,
        DWMWA_ALLOW_NCPAINT,
        DWMWA_CAPTION_BUTTON_BOUNDS,
        DWMWA_NONCLIENT_RTL_LAYOUT,
        DWMWA_FORCE_ICONIC_REPRESENTATION,
        DWMWA_FLIP3D_POLICY,
        DWMWA_EXTENDED_FRAME_BOUNDS,
        DWMWA_HAS_ICONIC_BITMAP,
        DWMWA_DISALLOW_PEEK,
        DWMWA_EXCLUDED_FROM_PEEK,
        DWMWA_CLOAK,
        DWMWA_CLOAKED,
        DWMWA_FREEZE_REPRESENTATION,
        DWMWA_LAST
    }

    public enum ShowWindowCommands : int
    {
        Hide = 0,
        Normal = 1,
        Minimized = 2,
        Maximized = 3,
    }


    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    internal struct WINDOWPLACEMENT
    {
        public int length;
        public int flags;
        public ShowWindowCommands showCmd;
        public System.Drawing.Point ptMinPosition;
        public System.Drawing.Point ptMaxPosition;
        public System.Drawing.Rectangle rcNormalPosition;
    }

    static class Win32Util
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        public static string GetWindowText(IntPtr hWnd)
        {
            var windowTextLength = Win32.GetWindowTextLength(hWnd);
            var stringBuffer = new StringBuilder(windowTextLength + 1);
            if (!Win32.GetWindowText(hWnd, stringBuffer, windowTextLength + 1))
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

        /// <summary>
        /// Moves a window to a given target rect factoring the shadow in the final dimensions.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="rct"></param>
        /// <exception cref="Exception"></exception>
        public static void MoveWindow(IntPtr hWnd, RECT rct)
        {
            var outerRect = Win32Util.GetWindowRect(hWnd);
            var innerRect = Win32Util.GetWindowRectInner(hWnd);

            var shadowLeft = Math.Abs(outerRect.Left - innerRect.Left);
            var shadowRight = Math.Abs(outerRect.Right - innerRect.Right);
            var shadowBottom = Math.Abs(outerRect.Bottom - innerRect.Bottom);
            var shadowTop = Math.Abs(outerRect.Top - innerRect.Top);

            var left = rct.Left - shadowLeft; // use shadow as margin
            var top = rct.Top - shadowTop; // use shadow as margin
            var width = (rct.Right - rct.Left) + (shadowLeft + shadowRight);
            var height = rct.Bottom - rct.Top + (shadowTop + shadowBottom);

            if (!Win32.MoveWindow(hWnd, left, top, width, height, true))
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

        public static RECT GetWindowRectInner(IntPtr hWnd)
        {
            RECT rect;
            int size = Marshal.SizeOf(typeof(RECT));
            Win32.DwmGetWindowAttribute(hWnd, (int)DwmWindowAttribute.DWMWA_EXTENDED_FRAME_BOUNDS, out rect, size);
            return rect;
        }

        public static WINDOWPLACEMENT GetWindowPlacement(IntPtr hWnd)
        {
            WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);
            if (!Win32.GetWindowPlacement(hWnd, ref placement))
            {
                throw new Exception($"Could not get window placement for window with pointer: {hWnd}");
            }
            return placement;
        }
    }

    // Structure for low-level keyboard hook
    [StructLayout(LayoutKind.Sequential)]
    public struct KBDLLHOOKSTRUCT
    {
        public uint vkCode;
        public uint scanCode;
        public uint flags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    static class Win32
    {
        // Low-level keyboard hook constants
        public const int WH_KEYBOARD_LL = 13;
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_SYSKEYUP = 0x0105;

        // Low-level keyboard hook delegate
        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out RECT pvAttribute, int cbAttribute);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int Width, int Height, bool Repaint);

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hWnd, MonitorFlag flag);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMonitorInfo(IntPtr hmonitor, [In, Out] MONITORINFOEX info);

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
    }
}

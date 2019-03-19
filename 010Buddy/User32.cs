using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _010Buddy
{
    public static class User32
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }

        public const UInt32 SW_HIDE            = 0;
        public const UInt32 SW_SHOWNORMAL      = 1;
        public const UInt32 SW_NORMAL          = 1;
        public const UInt32 SW_SHOWMINIMIZED   = 2;
        public const UInt32 SW_SHOWMAXIMIZED   = 3;
        public const UInt32 SW_MAXIMIZE        = 3;
        public const UInt32 SW_SHOWNOACTIVATE  = 4;
        public const UInt32 SW_SHOW            = 5;
        public const UInt32 SW_MINIMIZE        = 6;
        public const UInt32 SW_SHOWMINNOACTIVE = 7;
        public const UInt32 SW_SHOWNA          = 8;
        public const UInt32 SW_RESTORE         = 9;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT Rect);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int Width, int Height, bool Repaint);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl);
    }
}

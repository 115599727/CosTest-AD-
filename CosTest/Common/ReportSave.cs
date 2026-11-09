using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Threading;

namespace CosTest
{
    class ReportSave
    {
        [DllImport("User32.dll")]
        public static extern int GetWindowText(IntPtr WinHandle, StringBuilder Title, int size);
        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr WinHandle, StringBuilder Type, int size);
        public delegate bool EnumChildWindow(IntPtr WindowHandle, string num);
        [DllImport("User32.dll")]
        public static extern int EnumChildWindows(IntPtr WinHandle, EnumChildWindow ecw, string name);
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        private static extern int PostMessage(IntPtr hWnd, int Msg, string wParam, string lParam);
        [DllImport("User32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        private static extern int SendMessage(IntPtr hWnd, int WM_CHAR, int wParam, int lParam);
        [DllImport("User32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        private static extern int SendMessage(IntPtr hWnd, int WM_CHAR, string wParam, string lParam);
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        private static extern int PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_KEYDOWN = 0x0100;
        private const int WM_IME_KEYDOWN = 0x0290;
        private const int WM_IME_KEYUP = 0x0291;
        private const int WM_SETTEXT = 0x000C;

        /// <summary>
        /// 文件地址
        /// </summary>
        internal static string DstFilePath { get; set; }

        internal static void Do()
        {
            if (DstFilePath == null || DstFilePath == "")
                return;

            try
            {
                int hWndCount = 0;
                var hWnd = IntPtr.Zero;
                var hChild = IntPtr.Zero;

                // Find Save File Dialog Box
                while (hWnd == IntPtr.Zero)
                {
                    Thread.Sleep(500);
                    hWnd = FindWindow("#32770", "文件另存为");
                    hWndCount++;
                    if (hWndCount >= 4)
                        break;
                }

                // Enter fileName
                EnumChildWindows(hWnd, (handle, s) =>
                {
                    //####取类型
                    StringBuilder type = new StringBuilder(100);
                    GetClassName(handle, type, 100);//取类型
                    if (type.ToString() == s)
                    {
                        hChild = handle;
                        return false;
                    }

                    return true;
                }, "Edit");
                SendMessage(hChild, WM_SETTEXT, null, DstFilePath);

                // Press Save button
                hChild = FindWindowEx(hWnd, IntPtr.Zero, "Button", "保存(&S)");
                int VK_RETURN = 0x0D;
                PostMessage(hChild, WM_IME_KEYDOWN, VK_RETURN, 0);
                PostMessage(hChild, WM_IME_KEYUP, VK_RETURN, 0);
            }
            catch(Exception ex)
            {
                ErrLog.WriteLine("另存为xps失败 " + DstFilePath + "\r\n" + ex.ToString());
            }
        }

    }
}

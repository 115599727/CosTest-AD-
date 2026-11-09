using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CosTest

{
    class ProcessCheck
    {
        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        public static bool IsRunning()
        {
            // 当前程序的所在进程
            Process curProc = Process.GetCurrentProcess();
            // 要启动的进程名称，可以在任务管理器里查看，一般是不带.exe后缀的
            string pName = curProc.ProcessName;
            // 在所有已启动的进程中查找需要的进程
            Process[] temp = Process.GetProcessesByName(pName);
            // 检查进程是否已存在
            foreach(Process proc in temp)
            {
                if(proc.Id != curProc.Id)
                {
                    // 显示已有进程在最前
                    SwitchToThisWindow(proc.MainWindowHandle, true);
                    return true;
                }
            }
            // 默认值
            return false;
        }
    }
}

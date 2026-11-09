using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace IRIP_API_LIB
{
    public class DllInvoke
    {
        bool isCreate = false;
        [DllImport("kernel32.dll")]
        private extern static IntPtr LoadLibrary(string path);

        [DllImport("kernel32.dll")]
        private extern static IntPtr GetProcAddress(IntPtr lib, string funcName);

        [DllImport("kernel32.dll")]
        private extern static bool FreeLibrary(IntPtr lib);

        private IntPtr hLib;
        public DllInvoke(string DLLPath)
        {
            if (File.Exists(DLLPath))
            {
                hLib = LoadLibrary(DLLPath);
                if (hLib == IntPtr.Zero)
                {
                    isCreate = false;
                }
                else
                {
                    isCreate = true;
                }
            }
            else
            {
                isCreate = false;
            }
        }

        public bool IsCreate()
        {
            return isCreate;
        }

        ~DllInvoke()
        {
            isCreate = false;
            FreeLibrary(hLib);
        }

        //将要执行的函数转换为委托 
        public Delegate Invoke(string APIName, Type t)
        {
            if (isCreate && hLib != IntPtr.Zero)
            {
                IntPtr api = GetProcAddress(hLib, APIName);
                if(api == IntPtr.Zero)
                {
                    Console.WriteLine("This GetProcAddress is Null, APIName: " + APIName);
                    return null;
                }
                return Marshal.GetDelegateForFunctionPointer(api, t);
            }
            return null;
        }
    }

}

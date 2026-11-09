using System;
using System.ComponentModel;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security;


namespace Microsoft.Win32.Dialogs.UnsafeNativeMethods
{

    [SuppressUnmanagedCodeSecurity]
    internal static class Shell32
    {
        [DllImport("shell32.dll")]
        public static extern Int32 SHGetSpecialFolderLocation(IntPtr hwnd, Int32 csidl, ref IntPtr ppidl);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern Boolean SHGetPathFromIDList(IntPtr pidl, IntPtr pszPath);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SHBrowseForFolder([In] BrowseInfo lpbi);

        [DllImport("shell32.dll")]
        public static extern Int32 SHGetMalloc([MarshalAs(UnmanagedType.LPArray)][Out] IMalloc[] ppMalloc);
    }







    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    [SuppressUnmanagedCodeSecurity]
    internal class BrowseInfo
    {
        public IntPtr hwndOwner;
        public IntPtr pidlRoot;
        public IntPtr pszDisplayName;
        public String lpszTitle;
        public Int32 ulFlags;
        public BrowseCallbackProc lpfn;
        public IntPtr lParam;
        public Int32 iImage;
    }








    [SuppressUnmanagedCodeSecurity]
    internal delegate Int32 BrowseCallbackProc(IntPtr hwnd, Int32 msg, IntPtr lParam, IntPtr lpData);








    [Guid("00000002-0000-0000-c000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), SuppressUnmanagedCodeSecurity]
    [ComImport]
    [SuppressUnmanagedCodeSecurity]
    internal interface IMalloc
    {
        [PreserveSig]
        IntPtr Alloc(Int32 cb);

        [PreserveSig]
        IntPtr Realloc(IntPtr pv, Int32 cb);

        [PreserveSig]
        void Free(IntPtr pv);

        [PreserveSig]
        Int32 GetSize(IntPtr pv);

        [PreserveSig]
        Int32 DidAlloc(IntPtr pv);

        [PreserveSig]
        void HeapMinimize();
    }









    [SuppressUnmanagedCodeSecurity]
    internal static class Ole32
    {
        [DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        internal static extern void CoTaskMemFree(IntPtr pv);
    }











    [SuppressUnmanagedCodeSecurity]
    internal static class User32
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, Int32 msg, Int32 wParam, String lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, Int32 msg, Int32 wParam, Int32 lParam);
    }
}

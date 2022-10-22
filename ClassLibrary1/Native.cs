using System;
using System.Collections.Generic;
using RIS = System.Runtime.InteropServices;

namespace ClassLibrary1
{
    public static class Native
    {
        public const string DllName = "VtkDll.dll";
        public const RIS.CallingConvention CallingConvention = RIS.CallingConvention.Cdecl;
    }
}

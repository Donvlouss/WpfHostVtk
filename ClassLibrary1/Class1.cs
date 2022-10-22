using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public static partial class Invoke
    {
        [DllImport(Native.DllName, CallingConvention = Native.CallingConvention)]
        public static extern void CreateInstance();

        [DllImport(Native.DllName, CallingConvention = Native.CallingConvention)]
        public static extern void InitMyWindow(IntPtr window);
        [DllImport(Native.DllName, CallingConvention = Native.CallingConvention)]
        public static extern void AddCylinder();
        [DllImport(Native.DllName, CallingConvention = Native.CallingConvention)]
        public static extern void Refresh();

        [DllImport(Native.DllName, CallingConvention = Native.CallingConvention)]
        public static extern void CloseRender();
    }

    public class VtkWindow
    {
        public void CreateInstance()
        {
            Invoke.CreateInstance();
        }
        public void InitWindow(IntPtr theWnd)
        {
            Invoke.InitMyWindow(theWnd);
        }

        public void Refresh()
        {
            Invoke.Refresh();
        }

        public void AddCylinder()
        {
            Invoke.AddCylinder();
        }

        public void CloseRender()
        {
            Invoke.CloseRender();
        }
    }
}

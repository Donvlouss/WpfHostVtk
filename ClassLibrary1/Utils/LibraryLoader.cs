using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    class LibraryLoader
    {
        static readonly string[] AdditionalLibraries = new[]
        {
            //maybe people like copying the libraries to the same folder, rather than properly installing it.
            "concrt140",
            "liblzma",
            "swresample-2",
            "swscale-4",
            "vccorlib140",
            "zlib",
            "WinSparkle",
            "vcruntime140",
        };

        internal static void LoadLibraries()
        {
            //windows only for now...

            DllLoadUtils loader = new DllLoadUtilsWindows();

            string dir;
            dir = IntPtr.Size == 8 ? "x64/" : "x86/";

            //by loading the library before dllimport use, we can effectively remap them to wherever we've loaded it from.
            loader.LoadLibrary($"{dir}{Native.DllName}.dll");

            foreach (var name in AdditionalLibraries)
            {
                var filePath = $"{dir}{name}.dll";
                if (File.Exists(filePath))
                    loader.LoadLibrary(filePath);
            }
        }
    }

    interface DllLoadUtils
    {
        IntPtr LoadLibrary(string fileName);
        void FreeLibrary(IntPtr handle);
        IntPtr GetProcAddress(IntPtr dllHandle, string name);
    }

    public class DllLoadUtilsWindows : DllLoadUtils
    {
        void DllLoadUtils.FreeLibrary(IntPtr handle)
        {
            FreeLibrary(handle);
        }

        IntPtr DllLoadUtils.GetProcAddress(IntPtr dllHandle, string name)
        {
            return GetProcAddress(dllHandle, name);
        }

        IntPtr DllLoadUtils.LoadLibrary(string fileName)
        {
            return LoadLibrary(fileName);
        }

        [DllImport("kernel32")]
        private static extern IntPtr LoadLibrary(string fileName);

        [DllImport("kernel32.dll")]
        private static extern int FreeLibrary(IntPtr handle);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr handle, string procedureName);
    }

    internal class DllLoadUtilsLinux : DllLoadUtils
    {
        public IntPtr LoadLibrary(string fileName)
        {
            return dlopen(fileName, RTLD_NOW);
        }

        public void FreeLibrary(IntPtr handle)
        {
            dlclose(handle);
        }

        public IntPtr GetProcAddress(IntPtr dllHandle, string name)
        {
            // clear previous errors if any
            dlerror();
            var res = dlsym(dllHandle, name);
            var errPtr = dlerror();
            if (errPtr != IntPtr.Zero)
            {
                throw new Exception("dlsym: " + Marshal.PtrToStringAnsi(errPtr));
            }
            return res;
        }

        const int RTLD_NOW = 2;

        [DllImport("libdl.so")]
        private static extern IntPtr dlopen(String fileName, int flags);

        [DllImport("libdl.so")]
        private static extern IntPtr dlsym(IntPtr handle, String symbol);

        [DllImport("libdl.so")]
        private static extern int dlclose(IntPtr handle);

        [DllImport("libdl.so")]
        private static extern IntPtr dlerror();
    }
}

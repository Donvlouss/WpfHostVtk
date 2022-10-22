using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary1
{
    public partial class Form1 : Form
    {
        private readonly bool Initalized = false;
        private readonly VtkWindow vtkWindow;
        public Form1()
        {
            InitializeComponent();
            BackColor = Color.Black;
            vtkWindow = new VtkWindow();
            vtkWindow.CreateInstance();
            Initalized = true;
        }

        public void InitWindow(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero)
            {
                vtkWindow.InitWindow(Handle);
            }
            else
            {
                vtkWindow.InitWindow(hwnd);
            }
        }

        public void RefreshWindow()
        {
            vtkWindow.Refresh();
        }

        public void AddCylinder()
        {
            vtkWindow.AddCylinder();
        }

        public void CloseRender()
        {
            if (Initalized)
            {
                vtkWindow.CloseRender();
            }
        }
    }
}

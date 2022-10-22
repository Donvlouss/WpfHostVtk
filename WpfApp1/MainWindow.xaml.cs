using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary1;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Form1 form;
        private IntPtr ptr;
        public MainWindow()
        {
            InitializeComponent();
            form = new Form1();
            form.Show();
            windowsHost.Child = form;
            Loaded += new RoutedEventHandler(MainWindow_Loaded);
            Unloaded += (s, e) =>
            {
                form.CloseRender();
            };
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            ptr = helper.Handle;

        }

        private void Btn_AddCylinder(object sender, RoutedEventArgs e)
        {
            form.AddCylinder();
        }

        private void Btn_Refresh(object sender, RoutedEventArgs e)
        {
            form.RefreshWindow();
        }

        private void Btn_Init(object sender, RoutedEventArgs e)
        {
            form.InitWindow(IntPtr.Zero);
        }

        private void Btn_CloseRender(object sender, RoutedEventArgs e)
        {
            form.CloseRender();
        }
    }
}

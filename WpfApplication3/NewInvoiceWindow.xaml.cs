using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for NewInvoiceWindow.xaml
    /// </summary>
    public partial class NewInvoiceWindow
    {        
        public static NewInvoiceWindow _window { get; set; }

        public static void ShowSingleWindow()
        {
            if (_window == null)
            {
                _window = new NewInvoiceWindow();
                _window.Owner = App.Current.MainWindow;
                _window.Show();
            }
            else
            {
                _window.Activate();
            }
        }

        private NewInvoiceWindow()
        {
            InitializeComponent();
            Closing += NewInvoiceWindow_Closing;
        }

        private static void NewInvoiceWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _window = null;
        }
    }
}

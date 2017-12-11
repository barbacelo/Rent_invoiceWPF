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
    /// Interaction logic for NewKupciWindow.xaml
    /// </summary>
    public partial class NewKupciWindow : Window
    {
        public static NewKupciWindow Window { get; set; }

        public static void ShowSingleWindow()
        {
            if (Window == null)
            {
                Window = new NewKupciWindow { Owner = Application.Current.MainWindow };
                Window.Show();
            }
            else
            {
                Window.Activate();
            }
        }

        private NewKupciWindow()
        {
            InitializeComponent();
            Closing += NewKupciWindow_Closing;
        }

        private static void NewKupciWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window = null;
        }
    }
}

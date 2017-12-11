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
    /// Interaction logic for NewRobaWindow.xaml
    /// </summary>
    public partial class NewRobaWindow : Window
    {
        public static NewRobaWindow Window { get; set; }

        public static void ShowSingleWindow()
        {
            if (Window == null)
            {
                Window = new NewRobaWindow { Owner = Application.Current.MainWindow };
                Window.Show();
            }
            else
            {
                Window.Activate();
            }
        }

        private NewRobaWindow()
        {
            InitializeComponent();
            Closing += NewRobaWindow_Closing;
        }

        private static void NewRobaWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window = null;
        }
    }
}

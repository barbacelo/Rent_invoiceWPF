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

namespace WpfApplication3.Views
{
    /// <summary>
    /// Interaction logic for ChooseReportWindow.xaml
    /// </summary>
    public partial class ChooseReportWindow
    {
        public static ChooseReportWindow Window { get; set; }

        public static void ShowSingleWindow()
        {
            if (Window == null)
            {
                Window = new ChooseReportWindow { Owner = Application.Current.MainWindow };
                Window.ShowDialog();
            }
            else
            {
                Window.Activate();
            }
        }

        private ChooseReportWindow()
        {
            InitializeComponent();
            Closing += ChooseReportWindow_Closing;
        }

        private static void ChooseReportWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window = null;
        }
    }
}

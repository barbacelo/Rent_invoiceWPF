using System.Windows;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for NewInvoiceWindow.xaml
    /// </summary>
    public partial class NewInvoiceWindow
    {        
        public static NewInvoiceWindow Window { get; set; }

        public static void ShowSingleWindow()
        {
            if (Window == null)
            {
                Window = new NewInvoiceWindow { Owner = Application.Current.MainWindow };
                Window.Show();
            }
            else
            {
                Window.Activate();
            }
        }

        private NewInvoiceWindow()
        {
            InitializeComponent();
            Closing += NewInvoiceWindow_Closing;
        }

        private static void NewInvoiceWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window = null;
        }
    }
}

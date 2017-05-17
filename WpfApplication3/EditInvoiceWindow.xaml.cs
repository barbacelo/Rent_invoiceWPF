using System.Windows;
using WpfApplication3.ViewModel;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for EditInvoiceWindow.xaml
    /// </summary>
    public partial class EditInvoiceWindow
    {
        public static EditInvoiceWindow Window { get; set; }

        public static void ShowSingleWindow(RacuniViewModel racuni)
        {
            if (Window == null)
            {
                Window = new EditInvoiceWindow(racuni) { Owner = Application.Current.MainWindow };
                Window.ShowDialog();
            }
            else
            {
                Window.Activate();
            }
        }

        private EditInvoiceWindow(RacuniViewModel racuni)
        {
            this.DataContext = new EditingRacuniViewModel(racuni);
            InitializeComponent();
            Closing += EditInvoiceWindow_Closing;
        }

        private static void EditInvoiceWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window = null;
        }
    }
}

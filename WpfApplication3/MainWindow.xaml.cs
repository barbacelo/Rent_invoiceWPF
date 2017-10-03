using System.Windows;
using System.Data.Entity;
using WpfApplication3.Models;

namespace WpfApplication3
{
    public partial class MainWindow
    {
        private readonly ReversiEntities _context = new ReversiEntities();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Racunis.Load();
        }
    }
}
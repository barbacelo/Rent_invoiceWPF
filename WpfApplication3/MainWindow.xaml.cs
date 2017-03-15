using System.Windows;
using System.Data.Entity;

namespace WpfApplication3
{
    public partial class MainWindow
    {
        private readonly reversiEntities _context = new reversiEntities();

        public MainWindow()
        {                        
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.racuni.Load();
        }
    }
}
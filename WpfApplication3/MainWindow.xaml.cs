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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        reversiEntities context = new reversiEntities();
        CollectionViewSource kupciViewSource;
        CollectionViewSource racuniViewSource;
        CollectionViewSource robaViewSource;
        public MainWindow()
        {
            InitializeComponent();
            kupciViewSource = ((CollectionViewSource)(FindResource("kupciViewSource")));
            robaViewSource = ((CollectionViewSource)(FindResource("robaViewSource")));
            racuniViewSource = ((CollectionViewSource)(FindResource("racuniViewSource")));
            DataContext = this;           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context.kupci.Load();
            context.racuni.Load();
            context.roba.Load();

            kupciViewSource.Source = context.kupci.Local;
            robaViewSource.Source = context.roba.Local;
            racuniViewSource.Source = context.racuni.Local;            
        }
    }
}

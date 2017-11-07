using GalaSoft.MvvmLight;

namespace WpfApplication3.ViewModel
{
    public class PrintInvoiceLineViewModel : ViewModelBase
    {
        public string Roba { get; set; }
        public decimal? Amount { get; set; }
        public decimal Price { get; set; }
    }
}

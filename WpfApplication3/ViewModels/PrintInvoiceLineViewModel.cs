using GalaSoft.MvvmLight;
using System;

namespace WpfApplication3.ViewModel
{
    public class PrintInvoiceLineViewModel : ViewModelBase
    {
        public string Roba { get; set; }
        public decimal? Amount { get; set; }
        public decimal Price { get; set; }

        public DateTime Date { get; set; }
        public int? Days { get; set; }
        public decimal? Value { get; set; }
    }
}

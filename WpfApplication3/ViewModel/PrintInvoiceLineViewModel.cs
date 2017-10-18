using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication3.ViewModel
{
    public class PrintInvoiceLineViewModel : ViewModelBase
    {
        public string Roba { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
    }
}

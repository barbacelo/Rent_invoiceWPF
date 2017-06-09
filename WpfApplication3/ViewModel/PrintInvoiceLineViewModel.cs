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
        public int Amount { get; set; }
        public int Price { get; set; }
    }
}

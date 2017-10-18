using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication3.ViewModel
{
    public class PrintInvoiceViewModel : ViewModelBase
    {
        public string Jmbg { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }

        public List<PrintInvoiceLineViewModel> Items { get; set; } = new List<PrintInvoiceLineViewModel>();

    }
}

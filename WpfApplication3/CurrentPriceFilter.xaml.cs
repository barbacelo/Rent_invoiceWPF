using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataGridExtensions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for CurrentPriceFilter.xaml
    /// </summary>
    public partial class CurrentPriceFilter
    {
        public CurrentPriceFilter()
        {
            InitializeComponent();
        }

        public bool PaidInvoices
        {
            get { return (bool)GetValue(PaidInvoicesProperty); }
            set { SetValue(PaidInvoicesProperty, value); }
        }
        /// <summary>
        /// Identifies the PaidInvoices dependency property
        /// </summary>
        public static readonly DependencyProperty PaidInvoicesProperty = DependencyProperty.Register("PaidInvoices", typeof(bool), typeof(CurrentPriceFilter)
                , new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, e) => ((CurrentPriceFilter)sender).Range_Changed()));

        public bool OutstandingInvoices
        {
            get { return (bool)GetValue(OutstandingInvoicesProperty); }
            set { SetValue(OutstandingInvoicesProperty, value); }
        }
        /// <summary>
        /// Identifies the OutstandingInvoices dependency property
        /// </summary>
        public static readonly DependencyProperty OutstandingInvoicesProperty = DependencyProperty.Register("OutstandingInvoices", typeof(bool), typeof(CurrentPriceFilter)
                , new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, e) => ((CurrentPriceFilter)sender).Range_Changed()));

        public bool AllInvoices
        {
            get { return (bool)GetValue(AllInvoicesProperty); }
            set { SetValue(AllInvoicesProperty, value); }
        }
        /// <summary>
        /// Identifies the AllInvoices dependency property
        /// </summary>
        public static readonly DependencyProperty AllInvoicesProperty = DependencyProperty.Register("AllInvoices", typeof(bool), typeof(CurrentPriceFilter)
                , new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, e) => ((CurrentPriceFilter)sender).Range_Changed()));

        private void Range_Changed()
        {
            Filter = AllInvoices || OutstandingInvoices || PaidInvoices ? new ContentFilter(AllInvoices, OutstandingInvoices, PaidInvoices) : null;
        }

        public IContentFilter Filter
        {
            get { return (IContentFilter)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }
        /// <summary>
        /// Identifies the Filter dependency property
        /// </summary>
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(IContentFilter), typeof(CurrentPriceFilter), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, e) => ((CurrentPriceFilter)sender).Filter_Changed()));


        private void Filter_Changed()
        {
            var filter = Filter as ContentFilter;
            if (filter == null)
                return;

            PaidInvoices = filter.Paid;
            OutstandingInvoices = filter.Outstanding;
            AllInvoices = filter.All;
        }

        class ContentFilter : IContentFilter
        {
            private readonly bool _paid;
            private readonly bool _outstanding;
            private readonly bool _all;

            public ContentFilter(bool paid, bool outstanding, bool all)
            {
                _paid = paid;
                _outstanding = outstanding;
                _all = all;
            }

            public bool Paid
            {
                get
                {
                    return _paid;
                }
            }

            public bool Outstanding
            {
                get
                {
                    return _outstanding;
                }
            }

            public bool All
            {
                get
                {
                    return _all;
                }
            }

            public bool IsMatch(object value)
            {
                if (value == null)
                    return false;

                double number;

                if (!double.TryParse(value.ToString(), out number))
                {
                    return false;
                }

                if (_all == true)
                {
                    return true;
                }

                if (_outstanding == true && number > 0)
                {
                    return true;
                }

                if (_paid == true && number == 0)
                {
                    return true;
                }
                return true;
            }
        }
    }
}

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
using DataGridExtensions;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for FilterWithPopupControl.xaml
    /// </summary>
    public partial class FilterWithPopupControl
    {
        public FilterWithPopupControl()
        {
            InitializeComponent();
        }

        public bool Minimum
        {
            get { return (bool)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        /// <summary>
        /// Identifies the Minimum dependency property
        /// </summary>
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(bool), typeof(FilterWithPopupControl)
                , new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, e) => ((FilterWithPopupControl)sender).Range_Changed()));

        public bool Maximum
        {
            get { return (bool)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        /// <summary>
        /// Identifies the Maximum dependency property
        /// </summary>
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(bool), typeof(FilterWithPopupControl)
                , new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, e) => ((FilterWithPopupControl)sender).Range_Changed()));


        private void Range_Changed()
        {
            Filter = Maximum || Minimum ? new ContentFilter(Minimum, Maximum) : null;
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
            DependencyProperty.Register("Filter", typeof(IContentFilter), typeof(FilterWithPopupControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, e) => ((FilterWithPopupControl)sender).Filter_Changed()));


        private void Filter_Changed()
        {
            var filter = Filter as ContentFilter;
            if (filter == null)
                return;

            Minimum = filter.Min;
            Maximum = filter.Max;
        }

        class ContentFilter : IContentFilter
        {
            private readonly bool _min;
            private readonly bool _max;

            public ContentFilter(bool min, bool max)
            {
                _min = min;
                _max = max;
            }

            public bool Min
            {
                get
                {
                    return _min;
                }
            }

            public bool Max
            {
                get
                {
                    return _max;
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

                if (number == 0 && _min)
                    return false;

                if (number > 0 && _max)
                    return false;

                return true;
            }
        }

    }
}

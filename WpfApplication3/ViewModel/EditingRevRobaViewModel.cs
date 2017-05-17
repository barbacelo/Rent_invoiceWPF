using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace WpfApplication3.ViewModel
{
    public class EditingRevRobaViewModel : ViewModelBase
    {
        private RobaViewModel _roba;
        private decimal? _kolic;
        private decimal? _kolicraz;
        private DateTime _datum;        
        
        
        public RobaViewModel Roba
        {
            get { return _roba; }
            set
            {
                _roba = value;
                RaisePropertyChanged();
            }
        }
        public decimal? Kolic
        {
          get { return _kolic; }
          set
            {
                _kolic = value;
                RaisePropertyChanged();
            }
        }
        public decimal? Kolicraz
        {
            get { return _kolicraz; }
            set
            {
                _kolicraz = value;
                RaisePropertyChanged();                
            }
        }
        public DateTime Datum
        {
            get { return _datum; }
            set
            {
                _datum = value;
                RaisePropertyChanged();
            }
        }
        public decimal Cena { get; set; }
        public int? Utro { get; set; }

        public void Clear()
        {
            Datum = DateTime.Now;
            Cena = 0;
            Kolic = null;
            Roba = null;
        }

    }

}

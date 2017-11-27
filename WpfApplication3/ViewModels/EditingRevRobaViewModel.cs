using System;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace WpfApplication3.ViewModel
{
    public class EditingRevRobaViewModel : ViewModelBase
    {
        public ICommand RemoveInvoiceLineCommand => new RelayCommand(RemoveInvoiceLine);

        private bool _ischeckedd;
        private RobaViewModel _roba;
        private decimal? _kolic;
        private decimal? _kolicraz;
        private DateTime _datum;
        private decimal _cena;
        private bool _isDeleted;        
        
        public bool IsCheckedd
        {
            get { return _ischeckedd; }
            set
            {
                _ischeckedd = value;
                if (value)
                    Kolicraz = Kolic;
                else
                    Kolicraz = null;
                RaisePropertyChanged();
            }
        }
        public RobaViewModel Roba
        {
            get { return _roba; }
            set
            {
                _roba = value;
                RaisePropertyChanged();
                if (value != null)
                {
                    Cena = value.Cena;
                }
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
        public decimal Cena
        {
            get { return _cena; }
            set
            {
                _cena = value;
                RaisePropertyChanged();
            }
        }
        public int? Utro { get; set; }

        public bool IsDeleted
        {
            get { return _isDeleted; }
            set
            {
                _isDeleted = value;
                RaisePropertyChanged();
            }
        }

        private void RemoveInvoiceLine()
        {            
            if (IsDeleted == true)
                IsDeleted = false;

            else IsDeleted = true;
        }
        public void Clear()
        {
            Datum = DateTime.Now;
            Cena = 0;
            Kolic = null;
            Roba = null;
        }

    }

}

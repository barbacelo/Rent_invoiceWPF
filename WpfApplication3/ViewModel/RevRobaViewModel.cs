using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;

namespace WpfApplication3.ViewModel
{
    public class RevRobaViewModel : ViewModelBase
    {
        private int _pk;
        private DateTime _datum;
        private RobaViewModel _roba;
        private decimal? _kolic;
        private int? _utro;
        private decimal _cena;
        public bool Changed { get; set; }

        public int Pk
        {
            get { return _pk; }
            set
            {
                _pk = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public RobaViewModel Roba
        {
            get { return _roba; }
            set
            {
                _roba = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public int Brev
        {
            get { return _brev; }
            set
            {
                _brev = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public DateTime Datum
        {
            get { return _datum; }
            set
            {
                _datum = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public decimal? Kolic
        {
            get { return _kolic; }
            set
            {
                _kolic = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public int? Utro
        {
            get { return _utro; }
            set
            {
                _utro = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public decimal Cena
        {
            get { return _cena; }
            set
            {
                _cena = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        private readonly revroba _model;

        public RevRobaViewModel()
        {
            _model = new revroba();
        }

        public RevRobaViewModel(revroba k, IEnumerable<RobaViewModel> robas)
        {
            _model = k;

            Pk = k.pk;
            Brev = k.brev;
            Datum = k.datum;
            Roba = robas.FirstOrDefault(r => r.Idbroj == k.idbrojr);
            Kolic = k.kolic;
            Utro = k.utro;
            Cena = k.cena;

            Changed = false;
        }

        public revroba GetModel()
        {
            _model.brev = Brev;
            _model.datum = Datum;
            _model.idbrojr = Roba?.Idbroj ?? 0;
            _model.kolic = Kolic;
            _model.utro = Utro;
            _model.cena = Cena;
            _model.pk = Pk;

            return _model;
        }
        private bool _isDeleted;
        private int _brev;

        public bool IsDeleted
        {
            get { return _isDeleted; }
            set
            {
                _isDeleted = value;
                RaisePropertyChanged();
            }
        }

        public void Clear()
        {
            Brev = 0;
            Cena = 0;
            Kolic = null;
            Roba = null;
        }
    }
}

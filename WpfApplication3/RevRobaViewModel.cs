﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace WpfApplication3
{
    public class RevRobaViewModel : ViewModelBase
    {
        private int _pk;
        private DateTime _datum;
        private RobaViewModel _roba;
        private decimal? _kolic;
        private decimal? _utro;
        private decimal _cena;
        public bool Changed { get; set; }

        public int pk
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
        public DateTime datum
        {
            get { return _datum; }
            set
            {
                _datum = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public decimal? kolic
        {
            get { return _kolic; }
            set
            {
                _kolic = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }        
        public decimal? utro
        {
            get { return _utro; }
            set
            {
                _utro = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public decimal cena
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

            datum = k.datum;
            Roba = robas.FirstOrDefault(r => r.idbroj == k.idbrojr);
            kolic = k.kolic;
            utro = k.utro;
            cena = k.cena;

            Changed = false;            
        }

        public revroba GetModel()
        {
            _model.datum = datum;
            _model.idbrojr = Roba?.idbroj ?? 0;
            _model.kolic = kolic;
            _model.utro = utro;
            _model.cena = cena;

            return _model;
        }
        private bool _isDeleted;
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set
            {              
                _isDeleted = value;
                RaisePropertyChanged();
            }
        }

    }
}

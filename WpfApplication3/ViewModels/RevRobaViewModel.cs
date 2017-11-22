using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using WpfApplication3.Models;

namespace WpfApplication3.ViewModel
{
    public class RevRobaViewModel : ViewModelBase
    {
        private int _revrobaId;
        private DateTime _datum;
        private RobaViewModel _roba;
        private decimal? _kolic;
        private int? _utro;
        private decimal _cena;
        public bool Changed { get; set; }

        public int RevRobaID
        {
            get { return _revrobaId; }
            set
            {
                _revrobaId = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }

        public decimal? CurrentPrice
        {
            get { return ((DateTime.Today - Datum).Days + 1) * Cena * Kolic; }
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
        public int RacuniID
        {
            get { return _racuniId; }
            set
            {
                _racuniId = value;
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
                RaisePropertyChanged("CurrentPrice");
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
                RaisePropertyChanged("CurrentPrice");
                Changed = true;
            }
        }
        private readonly RevRoba _model;

        public RevRobaViewModel()
        {
            _model = new RevRoba();
        }

        public RevRobaViewModel(RevRoba k, IEnumerable<RobaViewModel> robas)
        {
            _model = k;

            RevRobaID = k.RevRobaID;
            RacuniID = k.RacuniID;
            Datum = k.Datum;
            Roba = robas.FirstOrDefault(r => r.Idbroj == k.RobaID);
            Kolic = k.Kolic;
            Utro = k.Utro;
            Cena = k.Cena;
            //CurrentPrice = ((DateTime.Today - Datum).Days + 1) *Cena * Kolic;
            Changed = false;
        }

        public void Commit()
        {
            _model.RacuniID = RacuniID;
            _model.Datum    = Datum;
            _model.RobaID   = Roba?.Idbroj ?? 0;
            _model.Kolic    = Kolic;
            _model.Utro     = Utro;
            _model.Cena     = Cena;
        }

        private bool _isDeleted;
        private int _racuniId;

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
            RacuniID = 0;
            Cena = 0;
            Kolic = null;
            Roba = null;
        }

        public void Delete(DAL dal)
        {
            dal.DeleteRevRoba(_model);
        }

        public void Save(DAL dal, int racuniId)
        {
            RacuniID = racuniId;
            Commit();
            
            if (_model.RevRobaID == 0)
                dal.AddRevRoba(_model);

            dal.SaveChanges();
        }
    }
}

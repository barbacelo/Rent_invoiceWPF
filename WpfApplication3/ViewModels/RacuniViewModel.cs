using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using WpfApplication3.Models;
using System.ComponentModel;

namespace WpfApplication3.ViewModel
{
    public class RacuniViewModel : ViewModelBase
    {
        private readonly DAL _dal;
        private readonly Racuni _model;

        private int _brev;
        private DateTime _datum;
        private KupciViewModel _kupci;
        private bool _changed;
        private bool _isDeleted;  
            
        public ICommand AddNewInvoiceLineCommand => new RelayCommand(AddNewInvoiceLine, CanAddNewInvoiceLine);

        public bool Changed
        {
            get
            {
                return _changed || RevRobas.Items.Any(x => x.Changed);
            }
            set
            {
                if (RevRobas != null)
                    foreach (var rr in RevRobas.Items)
                        rr.Changed = false;
                _changed = value;
            }
        }
        public KupciViewModel Kupci
        {
            get { return _kupci; }
            set
            {
                _kupci = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public RevRobasViewModel RevRobas { get; }
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
                if (_datum == value)
                    return;
                _datum = value;
                RevRobas.NoviRedReversa.Datum = _datum;
                RaisePropertyChanged();
                Changed = true;
            }
        }
       

        public decimal? CurrentPrice
        {
            get { return RevRobas.Items.Sum(x => x.CurrentPrice); }
        }

        public void CurrentPriceHandler(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged &&  e.PropertyDescriptor.Name == "CurrentPrice")
            {
                RaisePropertyChanged("CurrentPrice");
            }

        }

        public bool IsDeleted
        {
            get { return _isDeleted; }
            set
            {
                foreach (var rr in RevRobas.Items)
                    rr.IsDeleted = value;

                _isDeleted = value;
                RaisePropertyChanged();
            }
        }

        public RacuniViewModel(DAL dal)
        {
            _dal = dal;
            RevRobas = new RevRobasViewModel(new List<RevRobaViewModel>());
            Datum = DateTime.Now;
            _model = new Racuni();
            
        }

        public RacuniViewModel(DAL dal, Racuni k, IEnumerable<KupciViewModel> kupcis, RevRobasViewModel revRobas)
        {
            _dal = dal;
            _model = k;

            Brev = k.Brev;
            RevRobas = revRobas;
            RevRobas.Items.ListChanged += CurrentPriceHandler;
            Datum = k.Datum;
            Kupci = kupcis.FirstOrDefault(r => r.Idbroj == k.KupciID);

            Changed = false;
        }

        private bool CanAddNewInvoiceLine()
        {
            if (RevRobas.NoviRedReversa.Cena == 0 || RevRobas.NoviRedReversa.Kolic == null || RevRobas.NoviRedReversa.Roba == null)
            {
                return false;
            }
            return true;
        }

        private void AddNewInvoiceLine()
        {
            var rr = new RevRobaViewModel
            {
                RacuniID = RevRobas.NoviRedReversa.RacuniID,
                Roba = RevRobas.NoviRedReversa.Roba,
                Kolic    = RevRobas.NoviRedReversa.Kolic,
                Cena = RevRobas.NoviRedReversa.Cena,
                Datum    = RevRobas.NoviRedReversa.Datum
            };

            RevRobas.Items.Add(rr);
            RevRobas.NoviRedReversa.Clear();
        }

        public void Save()
        {
            if (Brev == 0)
                Brev = _dal.GetNextBrev(Datum.Year);

            Commit();

            if (CheckDelete())
                return;
            
            if (_model.RacuniID == 0)
                _dal.AddRacuni(_model);

            _dal.SaveChanges();

            foreach (var rr in RevRobas.Items)
                rr.Save(_dal, _model.RacuniID);

            _dal.UpdateStockLevels();

            foreach (var r in RevRobas.Items)
            {
                r.Roba.Zaliha = _dal.GetStockLevel(r.Roba.Idbroj);
            }

            Changed = false;

            _dal.SaveChanges();
        }

        private bool CheckDelete()
        {
            if (IsDeleted)
            {
                if (_model.RacuniID == 0)
                    return true;

                _dal.DeleteRacuni(_model);
                return true;
            }

            return false;
        }

        public void Commit()
        {
            _model.Brev = Brev;
            _model.Datum = Datum;
            _model.KupciID = Kupci?.Idbroj ?? 0;
        }
    }
}
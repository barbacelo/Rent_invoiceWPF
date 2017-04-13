using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;

namespace WpfApplication3.ViewModel
{

    public class RacuniViewModel : ViewModelBase
    {
        private readonly DAL _dal;
        public ICommand AddNewInvoiceLineCommand => new RelayCommand(AddNewInvoiceLine);        

        public void Save()
        {
            if (IsDeleted)
            {
                _dal.DeleteRacuni(GetModel());
            }
            else
            {                                        
                _dal.SaveRacuni(GetModel());
                foreach (RevRobaViewModel r in RevRobas.Items)
                {
                    r.Roba.Zaliha = _dal.GetStockLevel(r.Roba.Idbroj);
                    Changed = false;
                }                    
            }
        }                
        private void AddNewInvoiceLine()
        {
            var rr = new RevRobaViewModel();
            rr.Brev = RevRobas.NoviRedReversa.Brev;
            rr.Cena = RevRobas.NoviRedReversa.Cena;
            rr.Datum = RevRobas.NoviRedReversa.Datum;
            rr.Kolic = RevRobas.NoviRedReversa.Kolic;
            rr.Roba = RevRobas.NoviRedReversa.Roba;
            rr.Datum = Datum;
            RevRobas.Items.Add(rr);
            RevRobas.NoviRedReversa.Brev = 0;
            RevRobas.NoviRedReversa.Cena = 0;
            RevRobas.NoviRedReversa.Kolic = null;
            RevRobas.NoviRedReversa.Roba = null;
        }

        private int _brev;
        private DateTime _datum;
        private KupciViewModel _kupci;
        private bool _changed;
        public bool Changed
        {
            get
            {
                return _changed || RevRobas.Items.Any(x => x.Changed);
            }
            set
            {
                if (RevRobas != null )
                foreach (RevRobaViewModel rr in RevRobas.Items)
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
                _datum = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }

        private readonly racuni _model;

        public RacuniViewModel(DAL dal)
        {
            _dal = dal;
            Datum = DateTime.Now;       
            _model = new racuni();
            RevRobas = new RevRobasViewModel(new List<RevRobaViewModel>());
        }
        
        public RacuniViewModel(DAL dal,racuni k, IEnumerable<KupciViewModel> kupcis, RevRobasViewModel revRobas)
        {
            _dal = dal;
            _model = k;

            Brev = k.brev;
            Datum = k.datum;
            Kupci = kupcis.FirstOrDefault(r => r.Idbroj == k.idbrojk);

            RevRobas = revRobas;

            Changed = false;
        }

        public racuni GetModel()
        {
            _model.brev = Brev;
            _model.datum = Datum;
            _model.idbrojk = Kupci?.Idbroj ?? 0;

            _model.revroba.Clear();
            foreach (var rr in RevRobas.Items)
                _model.revroba.Add(rr.GetModel());

            // linq version:
            // _model.revroba = new ObservableCollection<revroba>(RevRobas.Items.Select(x => x.GetModel()));

            return _model;
        }
        private bool _isDeleted;
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
    }
}
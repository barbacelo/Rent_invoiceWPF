using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace WpfApplication3.ViewModel
{
    public class RacuniViewModel : ViewModelBase
    {
        private readonly DAL _dal;
        private readonly racuni _model;

        private int _brev;
        private DateTime _datum;
        private KupciViewModel _kupci;
        private bool _changed;
        private bool _isDeleted;

        public ICommand AddNewInvoiceLineCommand => new RelayCommand(AddNewInvoiceLine);

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
                _datum = value;
                RaisePropertyChanged();
                Changed = true;
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
            Datum = DateTime.Now;
            _model = new racuni();
            RevRobas = new RevRobasViewModel(new List<RevRobaViewModel>());
        }

        public RacuniViewModel(DAL dal, racuni k, IEnumerable<KupciViewModel> kupcis, RevRobasViewModel revRobas)
        {
            _dal = dal;
            _model = k;

            Brev = k.brev;
            Datum = k.datum;
            Kupci = kupcis.FirstOrDefault(r => r.Idbroj == k.idbrojk);

            RevRobas = revRobas;

            Changed = false;
        }

        private void AddNewInvoiceLine()
        {
            var rr = new RevRobaViewModel
            {
                Brev = RevRobas.NoviRedReversa.Brev,
                Cena = RevRobas.NoviRedReversa.Cena,
                Kolic = RevRobas.NoviRedReversa.Kolic,
                Roba = RevRobas.NoviRedReversa.Roba,
                Datum = Datum
            };

            RevRobas.Items.Add(rr);
            RevRobas.NoviRedReversa.Clear();
        }

        public void Save()
        {
            var model = GetModel();

            if (IsDeleted)
                _dal.DeleteRacuni(model);
            else
                _dal.SaveRacuni(model);

            Brev = model.brev;

            _dal.UpdateStockLevels();

            foreach (var r in RevRobas.Items)
                r.Roba.Zaliha = _dal.GetStockLevel(r.Roba.Idbroj);

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
    }
}
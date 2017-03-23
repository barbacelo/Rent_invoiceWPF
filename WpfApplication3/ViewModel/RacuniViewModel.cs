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
        public ICommand AddNewInvoiceLineCommand => new RelayCommand(AddNewInvoiceLine);
         public ICommand AddNewInvoiceCommand => new RelayCommand(AddNewInvoice);
                        
        private void AddNewInvoiceLine()
        {            
            RevRobas.Items.Add(RevRobas.NoviRedReversa);
        }

        private void AddNewInvoice()
        {
            _dal.SaveRacuni(GetModel());
        }

        private string _brev;
        private DateTime _datum;
        private KupciViewModel _kupci;

        public bool Changed { get; set; }

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
        public string Brev
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
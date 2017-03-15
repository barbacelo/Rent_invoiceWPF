using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WpfApplication3
{

    public class RacuniViewModel : ViewModelBase
    {
        private string _brev;
        private DateTime _datum;
        private KupciViewModel _kupci;
        private RevRobasViewModel _revrobas;

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

        public RevRobasViewModel RevRobas
        {
            get { return _revrobas; }
            set
            {
                _revrobas = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public string brev
        {
            get { return _brev; }
            set
            {
                _brev = value;
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

        private readonly racuni _model;

        public RacuniViewModel()
        {
            _model = new racuni();
        }

        public RacuniViewModel(racuni k, IEnumerable<KupciViewModel> kupcis, RevRobasViewModel revRobas)
        {
            _model = k;

            brev = k.brev;
            datum = k.datum;
            Kupci = kupcis.FirstOrDefault(r => r.idbroj == k.idbrojk);

            RevRobas = revRobas;

            Changed = false;
        }
        
        public racuni GetModel()
        {
            _model.brev = brev;
            _model.datum = datum;            
            _model.idbrojk = Kupci?.idbroj ?? 0;

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
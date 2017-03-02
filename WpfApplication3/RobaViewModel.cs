using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication3;

namespace WpfApplication3
{

    public class RobaViewModel : ViewModelBase
    {

        public override string ToString()
        {
            return naziv;
        }

        private int _idbroj;
        private string _naziv;
        private string _jm;
        private decimal _kol;
        private decimal _zaliha;
        private decimal _cena;

        public bool Changed { get; set; }
        public int idbroj
        {
            get { return _idbroj; }
            set
            {
                _idbroj = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public string naziv
        {
            get { return _naziv; }
            set
            {
                _naziv = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public string jm
        {
            get { return _jm; }
            set
            {
                _jm = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public decimal kol
        {
            get { return _kol; }
            set
            {
                _kol = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public decimal zaliha
        {
            get { return _zaliha; }
            set
            {
                _zaliha = value;
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

        private readonly roba _model;

        public RobaViewModel()
        {
            _model = new roba();
        }
        public RobaViewModel(roba k)
        {
            _model = k;

            idbroj = k.idbroj;
            naziv = k.naziv;
            jm = k.jm;
            kol = k.kol;
            zaliha = k.zaliha;
            cena = k.cena;

            Changed = false;
        }


        public roba GetModel()
        {
            _model.naziv = naziv;
            _model.jm = jm;
            _model.kol = kol;
            _model.zaliha = zaliha;
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
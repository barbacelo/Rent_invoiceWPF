using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication3;

namespace WpfApplication3
{

    public class KupciViewModel : ViewModelBase
    {
        public override string ToString()
        {
            return ime;
        }
        private int _idbroj;
        private string _ime;
        private string _jmbg;
        private string _adresa;
        private string _mesto;
        private string _telefon;
        private decimal _dug;
        private decimal _pot;
        private decimal? _saldo;

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
        public string ime
        {
            get { return _ime; }
            set
            {
                _ime = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public string jmbg
        {
            get { return _jmbg; }
            set
            {
                _jmbg = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public string adresa
        {
            get { return _adresa; }
            set
            {
                _adresa = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public string mesto
        {
            get { return _mesto; }
            set
            {
                _mesto = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public string telefon
        {
            get { return _telefon; }
            set
            {
                _telefon = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public decimal dug
        {
            get { return _dug; }
            set
            {
                _dug = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public decimal pot
        {
            get { return _pot; }
            set
            {
                _pot = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public decimal? saldo
        {
            get { return _saldo; }
            set
            {
                _saldo = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }

        private readonly kupci _model;

        public KupciViewModel()
        {
            _model = new kupci();
        }
        public KupciViewModel(kupci k)
        {
            _model = k;

            idbroj = k.idbroj;
            ime = k.ime;
            jmbg = k.jmbg;
            adresa = k.adresa;
            mesto = k.mesto;
            telefon = k.telefon;
            dug = k.dug;
            pot = k.pot;
            saldo = k.saldo;

            Changed = false;
        }


        public kupci GetModel()
        {
            _model.ime = ime;
            _model.jmbg = jmbg;
            _model.adresa = adresa;
            _model.mesto = mesto;
            _model.telefon = telefon;
            _model.dug = dug;
            _model.pot = pot;
            _model.saldo = saldo;

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
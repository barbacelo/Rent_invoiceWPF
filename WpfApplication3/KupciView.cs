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
        private int _idbroj;
        private string _ime;
        private string _jmbg;
        private string _adresa;
        private string _mesto;
        private string _telefon;
        private decimal _dug;
        private decimal _pot;
        private decimal? _saldo;

        public int idbroj
        {
            get { return _idbroj; }
            set
            {
                _idbroj = value;
                RaisePropertyChanged();
            }
        }
        public string ime
        {
            get { return _ime; }
            set
            {
                _ime = value;
                RaisePropertyChanged();
            }
        }
        public string jmbg
        {
            get { return _jmbg; }
            set
            {
                _jmbg = value;
                RaisePropertyChanged();
            }
        }
        public string adresa
        {
            get { return _adresa; }
            set
            {
                _adresa = value;
                RaisePropertyChanged();
            }
        }
        public string mesto
        {
            get { return _mesto; }
            set
            {
                _mesto = value;
                RaisePropertyChanged();
            }
        }
        public string telefon
        {
            get { return _telefon; }
            set
            {
                _telefon = value;
                RaisePropertyChanged();
            }
        }
        public decimal dug
        {
            get { return _dug; }
            set
            {
                _dug = value;
                RaisePropertyChanged();
            }
        }
        public decimal pot
        {
            get { return _pot; }
            set
            {
                _pot = value;
                RaisePropertyChanged();
            }
        }
        public decimal? saldo
        {
            get { return _saldo; }
            set
            {
                _saldo = value;
                RaisePropertyChanged();
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
        }


        public kupci GetModel()
        {
            _model.idbroj = idbroj;
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
    }
}
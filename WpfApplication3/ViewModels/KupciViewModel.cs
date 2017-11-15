using GalaSoft.MvvmLight;
using WpfApplication3.Models;

namespace WpfApplication3.ViewModel
{

    public class KupciViewModel : ViewModelBase
    {
        public override string ToString()
        {
            return Ime;
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
        public int Idbroj
        {
            get { return _idbroj; }
            set
            {
                _idbroj = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public string Ime
        {
            get { return _ime; }
            set
            {
                _ime = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public string Jmbg
        {
            get { return _jmbg; }
            set
            {
                _jmbg = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public string Adresa
        {
            get { return _adresa; }
            set
            {
                _adresa = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public string Mesto
        {
            get { return _mesto; }
            set
            {
                _mesto = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public string Telefon
        {
            get { return _telefon; }
            set
            {
                _telefon = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public decimal Dug
        {
            get { return _dug; }
            set
            {
                _dug = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public decimal Pot
        {
            get { return _pot; }
            set
            {
                _pot = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public decimal? Saldo
        {
            get { return _saldo; }
            set
            {
                _saldo = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }

        private readonly Kupci _model;

        public KupciViewModel()
        {
            _model = new Kupci();
        }
        public KupciViewModel(Kupci k)
        {
            _model = k;

            Idbroj  = k.KupciID;
            Ime     = k.Ime;
            Jmbg    = k.Jmbg;
            Adresa  = k.Adresa;
            Mesto   = k.Mesto;
            Telefon = k.Telefon;
            Dug     = k.Dug;
            Pot     = k.Pot;
            Saldo   = k.Dug - k.Pot;

            Changed = false;
        }


        public Kupci GetModel()
        {
            _model.Ime     = Ime;
            _model.Jmbg    = Jmbg;
            _model.Adresa  = Adresa;
            _model.Mesto   = Mesto;
            _model.Telefon = Telefon;
            _model.Dug     = Dug;
            _model.Pot     = Pot;
            _model.Saldo   = Saldo;

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
using GalaSoft.MvvmLight;
using WpfApplication3.Models;

namespace WpfApplication3.ViewModel
{

    public class RobaViewModel : ViewModelBase
    {

        public override string ToString()
        {
            return Naziv;
        }

        private int _idbroj;
        private string _naziv;
        private string _jm;
        private decimal _kol;
        private decimal _zaliha;
        private decimal _cena;

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
        public string Naziv
        {
            get { return _naziv; }
            set
            {
                _naziv = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public string Jm
        {
            get { return _jm; }
            set
            {
                _jm = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public decimal Kol
        {
            get { return _kol; }
            set
            {
                _kol = value;
                RaisePropertyChanged();
                Changed = true;
            }
        }
        public decimal Zaliha
        {
            get { return _zaliha; }
            set
            {
                _zaliha = value;
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
                Changed = true;
            }
        }

        private readonly Roba _model;

        public RobaViewModel()
        {
            _model = new Roba();
        }
        public RobaViewModel(Roba k)
        {
            _model = k;

            Idbroj = k.RobaID;
            Naziv  = k.Naziv;
            Jm     = k.Jm;
            Kol    = k.Kol;
            Zaliha = k.Zaliha;
            Cena   = k.Cena;

            Changed = false;
        }


        public Roba GetModel()
        {
            _model.Naziv = Naziv;
            _model.Jm = Jm;
            _model.Kol = Kol;
            _model.Zaliha = Zaliha;
            _model.Cena = Cena;

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
using GalaSoft.MvvmLight;

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

        private readonly roba _model;

        public RobaViewModel()
        {
            _model = new roba();
        }
        public RobaViewModel(roba k)
        {
            _model = k;

            Idbroj = k.idbroj;
            Naziv = k.naziv;
            Jm = k.jm;
            Kol = k.kol;
            Zaliha = k.zaliha;
            Cena = k.cena;

            Changed = false;
        }


        public roba GetModel()
        {
            _model.naziv = Naziv;
            _model.jm = Jm;
            _model.kol = Kol;
            _model.zaliha = Zaliha;
            _model.cena = Cena;

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
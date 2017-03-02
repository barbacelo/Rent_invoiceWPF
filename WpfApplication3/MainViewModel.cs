using GalaSoft.MvvmLight;

namespace WpfApplication3
{
    public class MainViewModel : ViewModelBase
    {
        private readonly DAL _dal;

        public KupcisViewModel Kupcis { get; }
        public RobasViewModel Robas { get; }
        public RacunisViewModel Racunis { get; }
        public RevRobasViewModel RevRobas { get; }
        public MainViewModel()
        {
            _dal = new DAL();
            Kupcis = new KupcisViewModel(_dal);
            Robas = new RobasViewModel(_dal);
            RevRobas = new RevRobasViewModel(_dal, Robas.Robas);
            Racunis = new RacunisViewModel(_dal, Kupcis.Kupcis, Robas.Robas);
        }
    }
}
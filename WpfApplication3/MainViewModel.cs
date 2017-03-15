using GalaSoft.MvvmLight;
using System.Linq;

namespace WpfApplication3
{
    public class MainViewModel : ViewModelBase
    {
        private readonly DAL _dal;

        public KupcisViewModel Kupcis { get; }
        public RobasViewModel Robas { get; }
        public RacunisViewModel Racunis { get; }

        public MainViewModel()
        {
            _dal = new DAL();
            Kupcis = new KupcisViewModel(_dal);
            Robas = new RobasViewModel(_dal);
            var revRobas = _dal.GetRevRoba().Select(x => new RevRobaViewModel(x, Robas.Robas));
            Racunis = new RacunisViewModel(_dal, Kupcis.Kupcis, revRobas);
        }
    }
}
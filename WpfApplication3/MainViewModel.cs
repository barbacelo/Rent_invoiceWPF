using GalaSoft.MvvmLight;
using System.Linq;
using WpfApplication3.ViewModel;

namespace WpfApplication3
{
    public class MainViewModel : ViewModelBase
    {
        public KupcisViewModel Kupcis { get; }
        public RobasViewModel Robas { get; }
        public RacunisViewModel Racunis { get; }

        public MainViewModel()
        {
            var dal = new DAL();

            Kupcis = new KupcisViewModel(dal);
            Robas = new RobasViewModel(dal);
            var revRobas = dal.GetRevRoba().Select(x => new RevRobaViewModel(x, Robas.Robas));
            Racunis = new RacunisViewModel(dal, Kupcis.Kupcis, revRobas);
        }
    }
}
using WpfApplication3.ViewModel;

namespace WpfApplication3.ModelExtensions
{
    public static class RevRobaExtensions
    {
        public static void Load(this RevRoba rr, RevRobaViewModel rvm)
        {
            rr.Cena     = rvm.Cena;
            rr.Datum    = rvm.Datum;
            rr.Kolic    = rvm.Kolic;
            rr.RacuniID = rvm.RacuniID;
            rr.RobaID   = rvm.Roba.Idbroj;
            rr.Utro     = rvm.Utro;
        }
    }
}
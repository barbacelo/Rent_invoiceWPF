using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace WpfApplication3.ViewModel
{
    public class RevRobasViewModel : ViewModelBase
    {        
        private RevRobaViewModel _selectedRevRoba;

        public RevRobaViewModel SelectedRevRoba
        {
            get { return _selectedRevRoba; }
            set
            {
                _selectedRevRoba = value;
                RaisePropertyChanged();
            }
        }

        private RevRobaViewModel _noviRedReversa;
        public RevRobaViewModel NoviRedReversa
        {
            get { return _noviRedReversa; }
            set
            {
                _noviRedReversa = value;
                RaisePropertyChanged();
            }
        }
        
        public ObservableCollection<RevRobaViewModel> Items { get; }

        public RevRobasViewModel(IEnumerable<RevRobaViewModel> revrobas)
        {
            Items = new ObservableCollection<RevRobaViewModel>(revrobas);
            NoviRedReversa = new RevRobaViewModel();
        }
    }
}

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
        //private RevRobaViewModel _selectedRowUtro;
        //public RevRobaViewModel SelectedRowUtro
        //{
        //    get { return _selectedRowUtro; }
        //    set
        //    {
        //        _selectedRowUtro = value;
        //        var selectedId = _selectedRowUtro.pk;
        //        RaisePropertyChanged();
        //    }
        //}

        //private RevRobaViewModel _selectedRowCena;
        //public RevRobaViewModel SelectedRowCena
        //{
        //    get { return _selectedRowCena; }
        //    set
        //    {                
        //        _selectedRowCena = value;
        //        var selectedId = _selectedRowCena.pk;
        //        _selectedRowUtro = 
        //        RaisePropertyChanged();
        //    } 
        //}
        public ObservableCollection<RevRobaViewModel> Items { get; }

        public RevRobasViewModel(IEnumerable<RevRobaViewModel> revrobas)
        {
            Items = new ObservableCollection<RevRobaViewModel>(revrobas);
        }
    }
}

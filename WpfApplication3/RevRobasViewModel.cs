using System.Collections.Generic;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Linq;

namespace WpfApplication3
{
    public class RevRobasViewModel : ViewModelBase
    {
        private readonly DAL _dal;
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
        public ObservableCollection<RevRobaViewModel> RevRobas { get; }

        public RevRobasViewModel(DAL dal, IEnumerable<RobaViewModel> robas)
        {
            _dal = dal;
            RevRobas = new ObservableCollection<RevRobaViewModel>(_dal.GetRevRoba().Select(x => new RevRobaViewModel(x, robas)).ToList());
        }
    }
}

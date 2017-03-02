using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace WpfApplication3
{
    public class RacunisViewModel : ViewModelBase
    {
        private readonly DAL _dal;
        private RacuniViewModel _selectedRacuni;
        //private NewInvoiceWindow myWindow { get; set; } = new NewInvoiceWindow();


        public ICommand SaveCommand => new RelayCommand(Save, CanSave);
        public ICommand DeleteCommand => new RelayCommand(Delete, CanDelete);
        public ICommand AddCommand => new RelayCommand<DataGrid>(Add);
        public ICommand UndoCommand => new RelayCommand(Undo, CanUndo);
        public ICommand NewInvoiceCommand => new RelayCommand(NewInvoice, CanNewInvoice);

        public RacuniViewModel SelectedRacuni
        {
            get { return _selectedRacuni; }
            set
            {
                _selectedRacuni = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<RacuniViewModel> Racunis { get; }

        public RacunisViewModel(DAL dal, IEnumerable<KupciViewModel> kupcis, IEnumerable<RobaViewModel> robas)
        {
            _dal = dal;
            Racunis = new ObservableCollection<RacuniViewModel>(_dal.GetRacuni().Select(x => new RacuniViewModel(x, kupcis, robas)).ToList());
        }
        private bool CanSave()
        {
            return Racunis.Any(x => x.Changed || x.IsDeleted);
        }

        private void Save()
        {
            var deleted = new List<RacuniViewModel>();

            foreach (var k in Racunis.Where(x => x.Changed || x.IsDeleted))
            {
                if (k.IsDeleted)
                {
                    deleted.Add(k);
                    _dal.DeleteRacuni(k.GetModel());
                }
                else
                {
                    _dal.SaveRacuni(k.GetModel());
                    k.Changed = false;
                }
            }
            _dal.SaveChanges();
            foreach (var d in deleted)
                Racunis.Remove(d);
        }        
        private bool CanDelete()
        {
            if (SelectedRacuni == null)
                return false;

            if (SelectedRacuni.IsDeleted)
                return false;

            return true;
        }
        private void Delete()
        {
            if (SelectedRacuni == null)
                return;

            SelectedRacuni.IsDeleted = true;
        }
        private void Add(DataGrid grid)
        {
            var newItem = new RacuniViewModel();
            Racunis.Add(newItem);

            int idx;

            for (idx = 0; idx < grid.Items.Count; idx++)
            {
                if (newItem == grid.Items[idx])
                    break;
            }

            grid.SelectionUnit = DataGridSelectionUnit.Cell;
            grid.Focus();
            grid.CurrentCell = new DataGridCellInfo(grid.Items[idx], grid.Columns[0]);
            grid.BeginEdit();
            grid.SelectionUnit = DataGridSelectionUnit.FullRow;
        }
        private bool CanUndo()
        {
            if (SelectedRacuni == null)
                return false;

            if (SelectedRacuni.IsDeleted)
                return true;

            return false;
        }
        private void Undo()
        {
            if (SelectedRacuni == null)
                return;
            SelectedRacuni.IsDeleted = false;
        }
        private void NewInvoice()
        {
            NewInvoiceWindow.ShowSingleWindow();           
        }

        private bool CanNewInvoice()
        {
            if (NewInvoiceWindow._window == null)
                return true;

            return false;
        }
    }
}
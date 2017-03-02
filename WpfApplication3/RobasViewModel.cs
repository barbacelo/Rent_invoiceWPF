using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace WpfApplication3
{
    public class RobasViewModel : ViewModelBase
    {
        private readonly DAL _dal;
        private RobaViewModel _selectedRoba;

        public ICommand SaveCommand => new RelayCommand(Save, CanSave);
        public ICommand DeleteCommand => new RelayCommand(Delete, CanDelete);
        public ICommand AddCommand => new RelayCommand<DataGrid>(Add);
        public ICommand UndoCommand => new RelayCommand(Undo, CanUndo);
        public RobaViewModel SelectedRoba
        {
            get { return _selectedRoba; }
            set
            {
                _selectedRoba = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<RobaViewModel> Robas { get; }

        public RobasViewModel(DAL dal)
        {
            _dal = dal;
            Robas = new ObservableCollection<RobaViewModel>(_dal.GetRoba().Select(x => new RobaViewModel(x)).ToList());
        }

        private bool CanSave()
        {
            return Robas.Any(x => x.Changed || x.IsDeleted);
        }

        private void Save()
        {
            var deleted = new List<RobaViewModel>();

            foreach (var k in Robas.Where(x => x.Changed || x.IsDeleted))
            {
                if (k.IsDeleted)
                {
                    deleted.Add(k);
                    _dal.DeleteRoba(k.GetModel());
                }
                else
                {
                    _dal.SaveRoba(k.GetModel());
                    k.Changed = false;
                }
            }

            _dal.SaveChanges();

            foreach (var d in deleted)
                Robas.Remove(d);
        }
        private bool CanDelete()
        {
            if (SelectedRoba == null)
                return false;

            if (SelectedRoba.IsDeleted)
                return false;

            return true;
        }
        private void Delete()
        {
            if (SelectedRoba == null)
                return;

            SelectedRoba.IsDeleted = true;
        }
        private void Add(DataGrid grid)
        {
            var newItem = new RobaViewModel();
            Robas.Add(newItem);

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
            if (SelectedRoba == null)
                return false;

            if (SelectedRoba.IsDeleted)
                return true;

            return false;
        }
        private void Undo()
        {
            if (SelectedRoba == null)
                return;
            SelectedRoba.IsDeleted = false;
        }
    }
}
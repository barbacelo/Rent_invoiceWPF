using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace WpfApplication3.ViewModel
{
    public class KupcisViewModel : ViewModelBase
    {
        private readonly DAL _dal;
        private KupciViewModel _selectedKupci;
        private KupciViewModel _newKupci;

        public ICommand SaveCommand => new RelayCommand(Save, CanSave);
        public ICommand DeleteCommand => new RelayCommand(Delete, CanDelete);
        public ICommand NewKupciWindowCommand => new RelayCommand(OpenNewKupciWindow, CanOpenNewKupciWindow);
        public ICommand AddNewKupciCommand => new RelayCommand(AddNewKupci, CanAddNewKupci);
        public ICommand UndoCommand => new RelayCommand(Undo, CanUndo);

        public KupciViewModel SelectedKupci
        {
            get { return _selectedKupci; }
            set
            {
                _selectedKupci = value;
                RaisePropertyChanged();
            }
        }

        public KupciViewModel NewKupci
        {
            get { return _newKupci; }
            set
            {
                _newKupci = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<KupciViewModel> Kupcis { get; }

        public KupcisViewModel(DAL dal)
        {
            _dal = dal;
            Kupcis = new ObservableCollection<KupciViewModel>(_dal.GetKupci().Select(x => new KupciViewModel(x)).ToList());
        }

        private bool CanSave()
        {
           return Kupcis.Any(x => x.Changed || x.IsDeleted);
        }
        private void Save()
        {
            var deleted = new List<KupciViewModel>();

            foreach (var k in Kupcis.Where(x => x.Changed || x.IsDeleted))
            {
                if (k.IsDeleted)
                {
                    deleted.Add(k);
                    _dal.Delete(k.GetModel());
                }
                else
                {
                    _dal.SaveKupci(k.GetModel());
                    k.Changed = false;
                }
            }

            _dal.SaveChanges();

            foreach (var d in deleted)
                Kupcis.Remove(d);
        }

        private bool CanDelete()
        {
            if (SelectedKupci == null)
                return false;

            if (SelectedKupci.IsDeleted)
                return false;

            return true;
        }
        private void Delete()
        {
            if (SelectedKupci == null)
                return;

            SelectedKupci.IsDeleted = true;
        }

        private bool CanUndo()
        {
            if (SelectedKupci == null)
                return false;

            if (SelectedKupci.IsDeleted)
                return true;

            return false;
        }
        private void Undo()
        {
            if (SelectedKupci == null)
                return;
            SelectedKupci.IsDeleted = false;
        }

        private void AddNewKupci()
        {
            NewKupci.save();
            Kupcis.Add(NewKupci);
            NewKupciWindow.Window.Close();
        }

        private void Add(DataGrid grid)
        {
            var newItem = new KupciViewModel();
            Kupcis.Add(newItem);

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

        private void OpenNewKupciWindow()
        {
            NewKupci = new KupciViewModel(_dal);
            NewKupciWindow.ShowSingleWindow();
        }

        private bool CanOpenNewKupciWindow()
        {
            if (NewKupciWindow.Window == null)
                return true;

            return false;
        }

        private bool CanAddNewKupci()
        {
            if(NewKupci.Ime == null || NewKupci.Jmbg == null || NewKupci.Mesto == null || NewKupci.Telefon == null || NewKupci.Adresa == null)
            {
                return false;
            }
            return true;
        }

    }
}
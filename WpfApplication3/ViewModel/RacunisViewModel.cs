using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace WpfApplication3.ViewModel
{
    public class RacunisViewModel : ViewModelBase
    {
        private readonly DAL _dal;
        private RacuniViewModel _selectedRacuni;

        public ICommand SaveCommand => new RelayCommand(Save, CanSave);
        public ICommand DeleteCommand => new RelayCommand(Delete, CanDelete);
        public ICommand AddCommand => new RelayCommand<DataGrid>(Add);
        public ICommand UndoCommand => new RelayCommand(Undo, CanUndo);
        public ICommand NewInvoiceWindowCommand => new RelayCommand(NewInvoice, CanNewInvoice);
        public ICommand AddNewInvoiceCommand => new RelayCommand(AddNewInvoice);

        private RacuniViewModel _noviRevers;
        public RacuniViewModel NoviRevers
        {
            get { return _noviRevers; }
            set
            {
                _noviRevers = value;
                RaisePropertyChanged();
            }
        }
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

        public RacunisViewModel(DAL dal, IEnumerable<KupciViewModel> kupcis, IEnumerable<RevRobaViewModel> revRobas)
        {            
            _dal = dal;
            Racunis = new ObservableCollection<RacuniViewModel>(_dal.GetRacuni().Select(x => new RacuniViewModel(dal,x, kupcis, new RevRobasViewModel(revRobas.Where(rr => rr.Brev == x.pk).ToList()))));
        }

        private void AddNewInvoice()
        {
            var model = NoviRevers.GetModel();
            _dal.SaveRacuni(model);
            NoviRevers.Brev = model.brev;
           // NoviRevers.RevRobas.NoviRedReversa.
            _dal.SaveChanges();
            
            Racunis.Add(NoviRevers);
            NoviRevers.Changed = false;
            NewInvoiceWindow.Window.Close();
        }

        private bool CanSave()
        {
            return Racunis.Any(x => x.Changed || x.IsDeleted);
        }

        private void Save()
        {
            var deleted = new List<RacuniViewModel>();

            foreach (var k in Racunis.Where(x => x.Changed || x.IsDeleted).ToArray())
            {
                k.Save();
                if (k.IsDeleted == true)
                    Racunis.Remove(k);
            }
            _dal.SaveChanges();                        
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
            var newItem = new RacuniViewModel(_dal);
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
            NoviRevers = new RacuniViewModel(_dal);
            NewInvoiceWindow.ShowSingleWindow();
        }

        private bool CanNewInvoice()
        {
            if (NewInvoiceWindow.Window == null)
                return true;

            return false;
        }
    }
}
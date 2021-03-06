﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace WpfApplication3.ViewModel
{
    public class EditingRacuniViewModel : ViewModelBase
    {

        public ICommand ClearAllInvoicesCommand => new RelayCommand(ClearAllInvoices);
        public ICommand SaveEditedInvoiceCommand => new RelayCommand(Save);
        public ICommand AddInvoiceLineCommand => new RelayCommand(AddInvoiceLine, CanAddInvoiceLine);

        private EditingRevRobaViewModel _newrevroba;
        private DateTime _datepickerdate;
        private RacuniViewModel _original;
        public RacuniViewModel Editable { get; }
        public EditingRevRobaViewModel Newrevroba
        {
            get { return _newrevroba; }
            set
            {
                _newrevroba = value;
                RaisePropertyChanged();
            }
        }
        public DateTime DatepickerDate
        {
            get { return _datepickerdate; }
            set
            {
                _datepickerdate = value;
               // RaisePropertyChanged();
               // C
            }
        }        
        public ObservableCollection<EditingRevRobaViewModel> InvoiceLineSummary { get; }        
       

        public EditingRacuniViewModel(RacuniViewModel rvm)
        {

            Newrevroba = new EditingRevRobaViewModel();
            Newrevroba.Datum = DateTime.Now;
            _original = rvm;
            
            Editable = new RacuniViewModel(null);
            Editable.Brev = _original.Brev;
            Editable.Datum = _original.Datum;
            Editable.RevRobas.NoviRedReversa.Datum = DateTime.Now;
            Editable.Kupci = _original.Kupci;
            foreach (RevRobaViewModel rr in _original.RevRobas.Items)
            {
                var newRR = new RevRobaViewModel();
                newRR.RacuniID = rr.RacuniID;
                newRR.Datum = rr.Datum;
                newRR.Kolic = rr.Kolic;
                newRR.RevRobaID = rr.RevRobaID;
                newRR.Roba = rr.Roba;
                newRR.Utro = rr.Utro;
                newRR.Cena = rr.Cena;
                Editable.RevRobas.Items.Add(newRR);
            }

             var revrobaquery = from rr in _original.RevRobas.Items
                               group rr by rr.Roba into GRoba
                               select new EditingRevRobaViewModel { Roba = GRoba.Key, Kolic = GRoba.Sum(x => x.Kolic), Datum = GRoba.Min(t => t.Datum), Cena = GRoba.Average(x => x.Cena), Utro = Convert.ToInt32(GRoba.Min(t => t.Datum).Subtract(_original.Datum).TotalDays) };
             InvoiceLineSummary = new ObservableCollection<EditingRevRobaViewModel>(revrobaquery);


        }

        private void AddInvoiceLine()
        {
            var rr = new EditingRevRobaViewModel
            {
                Roba = Newrevroba.Roba,
                Kolic = Newrevroba.Kolic,
                Cena = Newrevroba.Cena,
                Datum = Newrevroba.Datum
            };

            InvoiceLineSummary.Add(rr);
            Newrevroba.Clear();
        }
        
        private bool CanAddInvoiceLine()
        {
            if(Newrevroba.Cena == 0 || Newrevroba.Kolic == null || Newrevroba.Roba == null)
            {
                return false;
            }
            return true;
        }

        public void Save()
        {
            foreach (EditingRevRobaViewModel rr in InvoiceLineSummary)
            {
                if (!rr.IsDeleted && rr.Kolicraz != null && rr.Kolicraz != 0)
                {
                    var newRRRaz = new RevRobaViewModel();
                    newRRRaz.RacuniID = _original.Brev;
                    newRRRaz.Cena = rr.Cena;
                    newRRRaz.Datum = Newrevroba.Datum;
                    newRRRaz.Kolic = -rr.Kolicraz;
                    newRRRaz.Roba = rr.Roba;
                    _original.RevRobas.Items.Add(newRRRaz);
                    _original.RaisePropertyChanged("CurrentPrice");
                }
                else
                {
                    var existingrevroba = _original.RevRobas.Items.FirstOrDefault(x => x.Roba == rr.Roba);
                    if (existingrevroba == null)
                    {
                        var newrevroba = new RevRobaViewModel();
                        newrevroba.RacuniID = _original.Brev;
                        newrevroba.Cena = rr.Cena;
                        newrevroba.Datum = rr.Datum;
                        newrevroba.Kolic = rr.Kolic;
                        newrevroba.Roba = rr.Roba;
                        _original.RevRobas.Items.Add(newrevroba);                      
                    }
                }
            }
                
            foreach (EditingRevRobaViewModel nn in InvoiceLineSummary.Where(x => x.IsDeleted == true))
            {
                var deletedrevroba = _original.RevRobas.Items.Where(x => x.Roba == nn.Roba);
                foreach (RevRobaViewModel tt in deletedrevroba.ToList())
                {
                    tt.IsDeleted = true;
                    _original.Save();
                    _original.RevRobas.Items.Remove(tt);
                }
            }
            _original.Save();
            EditInvoiceWindow.Window.Close();
        }

        public void ClearAllInvoices()
        {
            foreach (EditingRevRobaViewModel rr in InvoiceLineSummary)
                rr.IsCheckedd = true;                
        }
    }
}

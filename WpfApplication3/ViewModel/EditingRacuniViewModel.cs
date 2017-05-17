﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApplication3.ViewModel
{
    public class EditingRacuniViewModel : ViewModelBase
    {
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
                newRR.Brev = rr.Brev;
                newRR.Datum = rr.Datum;
                newRR.Kolic = rr.Kolic;
                newRR.Pk = rr.Pk;
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

        public ICommand SaveEditedInvoiceCommand => new RelayCommand(Save);
        public ICommand RemoveInvoiceLineCommand => new RelayCommand<RevRobaViewModel>(RemoveInvoiceLine);
        public ICommand AddInvoiceLineCommand => new RelayCommand(AddInvoiceLine);
        private void AddInvoiceLine()
        {
            var rr = new EditingRevRobaViewModel
            {                
                Cena = Newrevroba.Cena,
                Kolic = Newrevroba.Kolic,
                Roba = Newrevroba.Roba,
                Datum = Newrevroba.Datum
            };

            InvoiceLineSummary.Add(rr);
            Newrevroba.Clear();
        }
        private void RemoveInvoiceLine(RevRobaViewModel rr)
        {

        }

        public void Save()
        {
            _original.Brev = Editable.Brev;
            _original.Datum = Editable.Datum;
            _original.Kupci = Editable.Kupci;
            foreach (RevRobaViewModel rr in Editable.RevRobas.Items)
            {
                var existingrevroba = _original.RevRobas.Items.FirstOrDefault(x => x.Pk == rr.Pk);
                if (existingrevroba == null || existingrevroba.Pk == 0)
                {
                    var newRR = new RevRobaViewModel();
                    newRR.Brev = rr.Brev;
                    newRR.Cena = rr.Cena;
                    newRR.Datum = rr.Datum;
                    newRR.Kolic = rr.Kolic;
                    newRR.Pk = rr.Pk;
                    newRR.Roba = rr.Roba;
                    newRR.Utro = rr.Utro;
                    _original.RevRobas.Items.Add(newRR);
                }
                else
                {
                    existingrevroba.Brev = rr.Brev;
                    existingrevroba.Cena = rr.Cena;
                    existingrevroba.Datum = rr.Datum;
                    existingrevroba.Kolic = rr.Kolic;
                    existingrevroba.Pk = rr.Pk;
                    existingrevroba.Roba = rr.Roba;
                    existingrevroba.Utro = rr.Utro;
                }
            }
            _original.Save();
        }
    }
}
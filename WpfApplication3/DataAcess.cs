﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;

namespace WpfApplication3
{
    public class DAL
    {
        public void Delete(kupci kupci)
        {
            var existing = _context.kupci.FirstOrDefault(x => x.idbroj == kupci.idbroj);

            if (existing != null)
                _context.kupci.Remove(kupci);
        }
        public void DeleteRoba(roba roba)
        {
            var existing = _context.roba.FirstOrDefault(x => x.idbroj == roba.idbroj);

            if (existing != null)
                _context.roba.Remove(roba);
        }

        public void DeleteRacuni(racuni racuni)
        {
            var existing = _context.racuni.FirstOrDefault(x => x.brev == racuni.brev);

            if (existing != null)
                _context.racuni.Remove(racuni);
        }

        private readonly reversiEntities _context = new reversiEntities();

        public List<racuni> GetRacuni()
        {
            return _context.Set<racuni>().ToList();
        }
        public List<kupci> GetKupci()
        {
            return _context.Set<kupci>().ToList();
        }
        public List<roba> GetRoba()
        {
            return _context.Set<roba>().ToList();
        }
        public List<revroba> GetRevRoba()
        {
            return _context.Set<revroba>().ToList();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void SaveKupci(kupci kupci)
        {
            try
            {
                var existing = _context.kupci.FirstOrDefault(x => x.idbroj == kupci.idbroj);

                if (existing == null)
                    _context.kupci.Add(kupci);
                else
                    _context.Entry(existing).CurrentValues.SetValues(kupci);

            }
            catch (DbEntityValidationException dbx)
            {
                foreach (var er in dbx.EntityValidationErrors)
                    MessageBox.Show(string.Join(Environment.NewLine, er.ValidationErrors.Select(x => x.PropertyName + ": " + x.ErrorMessage)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SaveRoba(roba roba)
        {
            try
            {
                var existing = _context.roba.FirstOrDefault(x => x.idbroj == roba.idbroj);

                if (existing == null)
                    _context.roba.Add(roba);
                else
                    _context.Entry(existing).CurrentValues.SetValues(roba);

            }
            catch (DbEntityValidationException dbx)
            {
                foreach (var er in dbx.EntityValidationErrors)
                    MessageBox.Show(string.Join(Environment.NewLine, er.ValidationErrors.Select(x => x.PropertyName + ": " + x.ErrorMessage)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SaveRacuni(racuni racuni)
        {
            try
            {
                var existing = _context.racuni.FirstOrDefault(x => x.brev == racuni.brev);

                if (_context.racuni.Count() == 0)
                    racuni.brev = 1;

                if (racuni.brev == 0)
                    racuni.brev = _context.racuni.Where(x => x.datum.Year == racuni.datum.Year).Max(x => x.brev) + 1;

                if (existing == null) // <-- the invoice doesnt exist
                    _context.racuni.Add(racuni);
                else
                {
                    existing.datum = racuni.datum;
                    existing.idbrojk = racuni.idbrojk;
                    existing.revroba = racuni.revroba;
                    //  _context.Entry(existing).CurrentValues.SetValues(racuni);
                }
                _context.revroba.AddRange(racuni.revroba);

            }
            catch (DbEntityValidationException dbx)
            {
                foreach (var er in dbx.EntityValidationErrors)
                    MessageBox.Show(string.Join(Environment.NewLine, er.ValidationErrors.Select(x => x.PropertyName + ": " + x.ErrorMessage)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
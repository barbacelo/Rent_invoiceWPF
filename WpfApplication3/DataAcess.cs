﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;

namespace WpfApplication3
{
    public class DAL
    {
        private readonly reversiEntities _context = new reversiEntities();

        public void Delete(Kupci kupci)
        {
            var existing = _context.kupci.FirstOrDefault(x => x.KupciID == kupci.KupciID);

            if (existing != null)
                _context.kupci.Remove(kupci);
        }

        public void DeleteRoba(Roba roba)
        {
            var existing = _context.roba.FirstOrDefault(x => x.RobaID == roba.RobaID);

            if (existing != null)
                _context.roba.Remove(roba);
        }

        public void DeleteRacuni(Racuni racuni)
        {
            if (racuni.RacuniID == 0)
                return;
            
            _context.revroba.RemoveRange(_context.revroba.Where(f => f.RacuniID == racuni.RacuniID));
            _context.racuni.Remove(racuni);
            
            SaveChanges();
        }

        public void DeleteRevRoba(RevRoba revroba)
        {
            var existing = _context.revroba.FirstOrDefault(x => x.RevRobaID == revroba.RevRobaID);

            if (existing != null)
                _context.revroba.Remove(revroba);

            SaveChanges();
        }

        public List<Racuni> GetRacuni()
        {
            return _context.Set<Racuni>().ToList();
        }

        public List<Kupci> GetKupci()
        {
            return _context.Set<Kupci>().ToList();
        }

        public List<Roba> GetRoba()
        {
            return _context.Set<Roba>().ToList();
        }

        public List<RevRoba> GetRevRoba()
        {
            return _context.Set<RevRoba>().ToList();
        }

        public void SaveKupci(Kupci kupci)
        {
            try
            {
                var existing = _context.kupci.FirstOrDefault(x => x.KupciID == kupci.KupciID);

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

        public void SaveRoba(Roba roba)
        {
            try
            {
                var existing = _context.roba.FirstOrDefault(x => x.RobaID == roba.RobaID);

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

        public int GetNextBrev(int year)
        {
            try
            {
                return _context.racuni.Where(x => x.Datum.Year == year).Max(x => x.Brev) + 1;
            }
            catch
            {
                return 1;
            }
        }

        public void UpdateStockLevels()
        {
            _context.Database.ExecuteSqlCommand("EXEC dbo.p_get_stock_level2");
        }

        public decimal GetStockLevel(int id)
        {
            return _context.Database.SqlQuery<decimal>(@"SELECT zaliha FROM roba WHERE RobaID = @p0", id).Single();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void AddRacuni(Racuni model)
        {
            _context.racuni.Add(model);
        }

        public void AddRevRoba(RevRoba model)
        {
            _context.revroba.Add(model);
        }
    }
}
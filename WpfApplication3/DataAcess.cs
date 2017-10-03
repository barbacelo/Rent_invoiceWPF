using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using WpfApplication3.Models;

namespace WpfApplication3
{
    public class DAL
    {
        private readonly ReversiEntities _context = new ReversiEntities();

        public void Delete(Kupci kupci)
        {
            var existing = _context.Kupcis.FirstOrDefault(x => x.KupciID == kupci.KupciID);

            if (existing != null)
                _context.Kupcis.Remove(kupci);
        }

        public void DeleteRoba(Roba roba)
        {
            var existing = _context.Robas.FirstOrDefault(x => x.RobaID == roba.RobaID);

            if (existing != null)
                _context.Robas.Remove(roba);
        }

        public void DeleteRacuni(Racuni racuni)
        {
            if (racuni.RacuniID == 0)
                return;
            
            _context.RevRobas.RemoveRange(_context.RevRobas.Where(f => f.RacuniID == racuni.RacuniID));
            _context.Racunis.Remove(racuni);
            
            SaveChanges();
        }

        public void DeleteRevRoba(RevRoba revroba)
        {
            var existing = _context.RevRobas.FirstOrDefault(x => x.RevRobaID == revroba.RevRobaID);

            if (existing != null)
                _context.RevRobas.Remove(revroba);

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
                var existing = _context.Kupcis.FirstOrDefault(x => x.KupciID == kupci.KupciID);

                if (existing == null)
                    _context.Kupcis.Add(kupci);
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
                var existing = _context.Robas.FirstOrDefault(x => x.RobaID == roba.RobaID);

                if (existing == null)
                    _context.Robas.Add(roba);
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
                return _context.Racunis.Where(x => x.Datum.Year == year).Max(x => x.Brev) + 1;
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
            _context.Racunis.Add(model);
        }

        public void AddRevRoba(RevRoba model)
        {
            _context.RevRobas.Add(model);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;

namespace WpfApplication3
{
    public class DAL
    {
        private readonly reversiEntities _context = new reversiEntities();

        public void Delete(Kupci Kupci)
        {
            var existing = _context.kupci.FirstOrDefault(x => x.KupciID == Kupci.KupciID);

            if (existing != null)
                _context.kupci.Remove(Kupci);
        }

        public void DeleteRoba(Roba roba)
        {
            var existing = _context.roba.FirstOrDefault(x => x.RobaID == roba.RobaID);

            if (existing != null)
                _context.roba.Remove(roba);
        }

        public void DeleteRacuni(Racuni racuni)
        {
            var existing = _context.racuni.FirstOrDefault(x => x.RacuniID == racuni.RacuniID);

            foreach (var rr in racuni.revroba.ToArray())
                if (rr.RevRobaID != 0)
                    _context.revroba.Remove(rr);

            if (existing != null)
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

        public void SaveKupci(Kupci Kupci)
        {
            try
            {
                var existing = _context.kupci.FirstOrDefault(x => x.KupciID == Kupci.KupciID);

                if (existing == null)
                    _context.kupci.Add(Kupci);
                else
                    _context.Entry(existing).CurrentValues.SetValues(Kupci);

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

        public int SaveRacuni(Racuni racuni)
        {
            try
            {              
                var existing = _context.racuni.FirstOrDefault(x => x.Brev == racuni.Brev);              

                SetRacuniBrev(racuni);

                if (existing == null)
                    _context.racuni.Add(racuni);
                else
                {
                    existing.Datum = racuni.Datum;
                    existing.KupciID = racuni.KupciID;
                    existing.revroba = racuni.revroba;
                }

                foreach (var rr in racuni.revroba.ToArray())
                    SaveRevRoba(rr);

                SaveChanges();

                return racuni.RacuniID;
            }
            catch (DbEntityValidationException dbx)
            {
                foreach (var er in dbx.EntityValidationErrors)
                    MessageBox.Show(string.Join(Environment.NewLine, er.ValidationErrors.Select(x => x.PropertyName + ": " + x.ErrorMessage)));
                return racuni.RacuniID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return racuni.RacuniID;
            }
        }

        private void SetRacuniBrev(Racuni racuni)
        {
            if (!_context.racuni.Any())
                racuni.Brev = 1;

            if (racuni.Brev == 0)
                racuni.Brev = _context.racuni.Where(x => x.Datum.Year == racuni.Datum.Year).Max(x => x.Brev) + 1;
        }

        private void SaveRevRoba(RevRoba rr)
        {
            try
            {
                var existing = _context.revroba.FirstOrDefault(x => x.RevRobaID == rr.RevRobaID);

                if (existing == null)
                {
                    _context.revroba.Add(rr);
                    SaveChanges();
                }
              
                else
                {
                    existing.RacuniID = rr.RacuniID;
                    existing.RobaID = rr.RobaID;
                    existing.Datum = rr.Datum;
                    existing.Cena = rr.Cena;
                    existing.racuni = rr.racuni;
                    existing.Utro = rr.Utro;
                }
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
    }
}
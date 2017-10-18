namespace WpfApplication3.Models
{
    using System.Data.Entity;

    public partial class ReversiEntities : DbContext
    {
        public ReversiEntities()
            : base("name=ReversiEntities")
        {
        }

        public virtual DbSet<Kupci> Kupcis { get; set; }
        public virtual DbSet<Racuni> Racunis { get; set; }
        public virtual DbSet<RevRoba> RevRobas { get; set; }
        public virtual DbSet<Roba> Robas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kupci>()
                .Property(e => e.Ime)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Kupci>()
                .Property(e => e.Jmbg)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Kupci>()
                .Property(e => e.Adresa)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Kupci>()
                .Property(e => e.Mesto)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Kupci>()
                .Property(e => e.Telefon)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Kupci>()
                .Property(e => e.Dug)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Kupci>()
                .Property(e => e.Pot)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Kupci>()
                .Property(e => e.Saldo)
                .HasPrecision(13, 2);

            modelBuilder.Entity<Kupci>()
                .HasMany(e => e.Racunis)
                .WithRequired(e => e.Kupci)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Racuni>()
                .HasMany(e => e.RevRobas)
                .WithRequired(e => e.Racuni)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RevRoba>()
                .Property(e => e.Kolic)
                .HasPrecision(6, 0);

            modelBuilder.Entity<RevRoba>()
                .Property(e => e.Cena)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Roba>()
                .Property(e => e.Naziv)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Roba>()
                .Property(e => e.Jm)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Roba>()
                .Property(e => e.Kol)
                .HasPrecision(6, 0);

            modelBuilder.Entity<Roba>()
                .Property(e => e.Zaliha)
                .HasPrecision(6, 0);

            modelBuilder.Entity<Roba>()
                .Property(e => e.Cena)
                .HasPrecision(9, 2);

            modelBuilder.Entity<Roba>()
                .HasMany(e => e.RevRobas)
                .WithRequired(e => e.Roba)
                .WillCascadeOnDelete(false);
        }
    }
}

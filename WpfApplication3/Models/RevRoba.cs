namespace WpfApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RevRoba")]
    public partial class RevRoba
    {
        public int RevRobaID { get; set; }

        public int RacuniID { get; set; }

        public DateTime Datum { get; set; }

        public int RobaID { get; set; }

        public decimal? Kolic { get; set; }

        public int? Utro { get; set; }

        public decimal Cena { get; set; }

        public virtual Racuni Racuni { get; set; }

        public virtual Roba Roba { get; set; }
    }
}

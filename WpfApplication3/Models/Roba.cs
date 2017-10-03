namespace WpfApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Roba")]
    public partial class Roba
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Roba()
        {
            RevRobas = new HashSet<RevRoba>();
        }

        public int RobaID { get; set; }

        [Required]
        [StringLength(40)]
        public string Naziv { get; set; }

        [Required]
        [StringLength(3)]
        public string Jm { get; set; }

        public decimal Kol { get; set; }

        public decimal Zaliha { get; set; }

        public decimal Cena { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RevRoba> RevRobas { get; set; }
    }
}

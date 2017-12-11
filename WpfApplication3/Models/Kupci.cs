namespace WpfApplication3.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Kupci")]
    public partial class Kupci
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kupci()
        {
            Racunis = new HashSet<Racuni>();
        }

        public int KupciID { get; set; }

        [Required]
        [StringLength(40)]
        public string Ime { get; set; }

        [Required]
        [StringLength(13)]
        public string Jmbg { get; set; }

        [Required]
        [StringLength(40)]
        public string Adresa { get; set; }

        [Required]
        [StringLength(25)]
        public string Mesto { get; set; }

        [Required]
        [StringLength(15)]
        public string Telefon { get; set; }

        public decimal Dug { get; set; }

        public decimal Pot { get; set; }

        public decimal? Saldo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Racuni> Racunis { get; set; }
    }
}

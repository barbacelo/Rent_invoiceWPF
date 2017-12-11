namespace WpfApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Racuni")]
    public partial class Racuni
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Racuni()
        {
            RevRobas = new HashSet<RevRoba>();
        }

        public int Brev { get; set; }

        public DateTime Datum { get; set; }

        public int KupciID { get; set; }

        public int RacuniID { get; set; }

        public virtual Kupci Kupci { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RevRoba> RevRobas { get; set; }
    }
}

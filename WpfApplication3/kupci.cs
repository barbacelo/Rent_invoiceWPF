//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApplication3
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class Kupci
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kupci()
        {
            this.racuni = new ObservableCollection<Racuni>();
        }
    
        public int KupciID { get; set; }
        public string Ime { get; set; }
        public string Jmbg { get; set; }
        public string Adresa { get; set; }
        public string Mesto { get; set; }
        public string Telefon { get; set; }
        public decimal Dug { get; set; }
        public decimal Pot { get; set; }
        public Nullable<decimal> Saldo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Racuni> racuni { get; set; }
    }
}

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
    
    public partial class Racuni
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Racuni()
        {
            this.revroba = new ObservableCollection<RevRoba>();
        }
    
        public int Brev { get; set; }
        public System.DateTime Datum { get; set; }
        public int KupciID { get; set; }
        public int RacuniID { get; set; }
    
        public virtual Kupci kupci { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<RevRoba> revroba { get; set; }
    }
}

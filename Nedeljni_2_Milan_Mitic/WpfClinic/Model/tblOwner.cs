//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfClinic.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblOwner
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblOwner()
        {
            this.tblClinics = new HashSet<tblClinic>();
        }
    
        public int OwnerID { get; set; }
        public string FullName { get; set; }
        public string JMBG { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblClinic> tblClinics { get; set; }
    }
}

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
    
    public partial class tblPatient
    {
        public int PatientID { get; set; }
        public Nullable<int> AccountID { get; set; }
        public string InsuranceCardNumber { get; set; }
        public System.DateTime InsuranceCardExpiry { get; set; }
        public string DoctorNumber { get; set; }
    
        public virtual tblAccount tblAccount { get; set; }
        public virtual tblDoctor tblDoctor { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HospitalApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class vwRequest
    {
        public int RequestId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Reason { get; set; }
        public string Company { get; set; }
        public bool IsUrgent { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public int PatientId { get; set; }
        public Nullable<int> DoctorId { get; set; }
        public string Expr1 { get; set; }
        public int Expr2 { get; set; }
        public int Expr3 { get; set; }
        public string DoctorFullName { get; set; }
    }
}

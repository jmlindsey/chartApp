//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChartApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vital
    {
        public int VitalId { get; set; }
        public Nullable<int> PatientId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public System.DateTime DateTimeDone { get; set; }
        public string UpperLeftSound { get; set; }
        public string UpperRightSound { get; set; }
        public string LowerLeftSound { get; set; }
        public string LowerRightSound { get; set; }
        public string MiddleSound { get; set; }
        public Nullable<int> RR { get; set; }
        public Nullable<int> HR { get; set; }
        public string BP { get; set; }
        public Nullable<int> Saturation { get; set; }
        public string Cough { get; set; }
        public string O2Device { get; set; }
        public Nullable<int> Flow { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Patient Patient { get; set; }
    }
}

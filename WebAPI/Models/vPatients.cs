//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class vPatients
    {
        public int patientId { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public Nullable<System.DateTime> lastConsultation { get; set; }
        public string emeci { get; set; }
        public string sex { get; set; }
        public Nullable<System.DateTime> birthDate { get; set; }
        public string groupRH { get; set; }
        public string allergies { get; set; }
        public string reserved { get; set; }
        public string relevantPathologies { get; set; }
    }
}
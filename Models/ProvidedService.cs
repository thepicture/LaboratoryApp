//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LaboratoryApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProvidedService
    {
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public int TypeOfAnalyzerId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int TimeInSeconds { get; set; }
    
        public virtual Service Service { get; set; }
        public virtual TypeOfAnalyzer TypeOfAnalyzer { get; set; }
        public virtual User User { get; set; }
    }
}

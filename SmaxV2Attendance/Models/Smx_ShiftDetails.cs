//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmaxV2Attendance.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Smx_ShiftDetails
    {
        public Smx_ShiftDetails()
        {
            this.Smx_ShiftAssignmentDetails = new HashSet<Smx_ShiftAssignmentDetails>();
        }
    
        public decimal Sftd_Id { get; set; }
        public string Sftd_Name { get; set; }
        public string Sftd_StartTime { get; set; }
        public string Sftd_EndTime { get; set; }
        public Nullable<decimal> Sftd_Hours_Id { get; set; }
        public Nullable<System.DateTime> Sftd_Created { get; set; }
        public Nullable<System.DateTime> Sftd_Modified { get; set; }
        public string Sftd_Modifiedby { get; set; }
    
        public virtual ICollection<Smx_ShiftAssignmentDetails> Smx_ShiftAssignmentDetails { get; set; }
    }
}

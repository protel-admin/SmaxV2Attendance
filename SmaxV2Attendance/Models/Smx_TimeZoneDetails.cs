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
    
    public partial class Smx_TimeZoneDetails
    {
        public decimal TZD_ID { get; set; }
        public decimal TZD_TZ_ID { get; set; }
        public string TZD_DAYS { get; set; }
        public string TZD_START_TIME { get; set; }
        public string TZD_END_TIME { get; set; }
        public Nullable<System.DateTime> TZD_SPECIFIC_DATE { get; set; }
        public Nullable<System.DateTime> TZD_CREATED { get; set; }
        public Nullable<System.DateTime> TZD_MODIFIED { get; set; }
        public string TZD_MODIFIEDBY { get; set; }
    
        public virtual Smx_TimeZone Smx_TimeZone { get; set; }
    }
}

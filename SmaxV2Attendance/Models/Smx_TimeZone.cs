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
    
    public partial class Smx_TimeZone
    {
        public Smx_TimeZone()
        {
            this.Smx_TimeZoneDetails = new HashSet<Smx_TimeZoneDetails>();
            this.Smx_AccessLevelDetails = new HashSet<Smx_AccessLevelDetails>();
        }
    
        public decimal TZ_ID { get; set; }
        public string TZ_NAME { get; set; }
        public Nullable<bool> TZ_UPDATE_STATUS { get; set; }
        public Nullable<System.DateTime> TZ_CREATED { get; set; }
        public Nullable<System.DateTime> TZ_MODIFIED { get; set; }
        public string TZ_MODIFIEDBY { get; set; }
    
        public virtual ICollection<Smx_TimeZoneDetails> Smx_TimeZoneDetails { get; set; }
        public virtual ICollection<Smx_AccessLevelDetails> Smx_AccessLevelDetails { get; set; }
    }
}

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
    
    public partial class ScreenName
    {
        public ScreenName()
        {
            this.Entitlements = new HashSet<Entitlement>();
        }
    
        public string SC_ScreenName { get; set; }
        public string SC_Description { get; set; }
    
        public virtual ICollection<Entitlement> Entitlements { get; set; }
    }
}

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
    
    public partial class Smx_Areas
    {
        public decimal AR_ID { get; set; }
        public string AR_NAME { get; set; }
        public Nullable<int> AR_NODEID { get; set; }
        public string AR_TYPE { get; set; }
        public Nullable<decimal> AR_APB { get; set; }
        public Nullable<int> AR_LN_ID { get; set; }
        public string AR_IPADDRESS { get; set; }
        public Nullable<decimal> AR_APBNUMBER { get; set; }
        public string AR_STATUS { get; set; }
        public Nullable<bool> AR_DELETED { get; set; }
    
        public virtual Smx_Location Smx_Location { get; set; }
    }
}

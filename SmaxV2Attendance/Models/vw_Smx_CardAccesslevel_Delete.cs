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
    
    public partial class vw_Smx_CardAccesslevel_Delete
    {
        public string Ch_CardNo { get; set; }
        public string Ch_EmpId { get; set; }
        public string Ch_FName { get; set; }
        public Nullable<int> Ch_PinNo { get; set; }
        public Nullable<System.DateTime> Ch_ValidTo { get; set; }
        public bool Ch_ISBio { get; set; }
        public bool Ch_ISCard { get; set; }
        public bool Ch_IsCardBio { get; set; }
        public bool Ch_ISPin { get; set; }
        public bool Ch_AntiPassBack { get; set; }
        public Nullable<bool> Ch_AccessValidation { get; set; }
        public Nullable<decimal> CAL_AL_ID { get; set; }
        public Nullable<bool> CAL_Deleted { get; set; }
        public Nullable<bool> CAL_DWStatus { get; set; }
        public decimal ALD_TZ_ID { get; set; }
        public string ALD_READER_IPADDRESS { get; set; }
        public int ALD_LN_ID { get; set; }
        public Nullable<bool> Ch_IsCardBasedBio { get; set; }
    }
}

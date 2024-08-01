using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Models
{

    [MetadataType(typeof(Smx_LeaveMetadata))]

    public partial class Smx_Leave
    {

    }

    public class Smx_LeaveMetadata
    {
        [Display(Name = "Short Description", Prompt = "e.g. CL , ML...")]
        [Required(ErrorMessage = "Please Enter Short Description")]
        public string Lv_ShortDesc { get; set; }

        [Display(Name = "Leave Description", Prompt = "e.g. Casual Leave , Medical Leave...")]
        [Required(ErrorMessage = "Please Enter Leave Description")]
        public string Lv_Description { get; set; }

        [Display(Name = "Maximum Number of Leave", Prompt = "e.g. 10,15,20...")]
        [Required(ErrorMessage = "Please Enter Maximum Number of Leave")]
        public Nullable<int> Lv_MaxDays { get; set; }

        [Display(Name = "Maximum Number of continuous Leave", Prompt = "e.g. 2,3,5...")]
        [Required(ErrorMessage = "Please Enter Maximum Number of continuous Leave")]
        public string Lv_MaxAllowed { get; set; }

        [Display(Name = "Created Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> Lv_Created { get; set; }

        [Display(Name = "Created Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> LV_Modified { get; set; }

        [Display(Name = "Modified By", Prompt = "Logged User", Description = "Hidden")]
        public string Lv_Modifiedby { get; set; }
    }
}
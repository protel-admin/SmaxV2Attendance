using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Models
{
    [MetadataType(typeof(Smx_ShiftDetailsMetadata))]
    public partial class Smx_ShiftDetails
    {

    }  

    public class Smx_ShiftDetailsMetadata
    {
        [Display(Name = "Shift", Prompt = "e.g. General")]
        [Required(ErrorMessage = "Please Choose Group Name")]
        public string Sftd_Name { get; set; }

        [Display(Name = "Shift Starting Time", Prompt = "e.g. HH:MM")]
        [Required(ErrorMessage = "Please Enter Starting Time")]
        public string Sftd_StartTime { get; set; }

        [Display(Name = "Shift End Time", Prompt = "e.g. HH:MM")]
        [Required(ErrorMessage = "Please Enter End Time")]
        public string Sftd_EndTime { get; set; }

        [Display(Name = "Hours ID", Prompt = "e.g. Hours Id")]
        [Required(ErrorMessage = "Please Enter End Time")]
        public Nullable<decimal> Sftd_Hours_Id { get; set; }

        [Display(Name = "Created Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> Sftd_Created { get; set; }

        [Display(Name = "Modified Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> Sftd_Modified { get; set; }

        [Display(Name = "Modified By", Prompt = "Logged User", Description = "Hidden")]
        public string Sftd_Modifiedby { get; set; }
    }
}
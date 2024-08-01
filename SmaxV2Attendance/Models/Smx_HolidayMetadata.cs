using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Models
{
    [MetadataType(typeof(Smx_HolidayMetadata))]
    public partial class Smx_Holiday
    {

    }

    public class Smx_HolidayMetadata
    {
        [Required(ErrorMessage = "Please enter a valid date")]
        [Display(Name = "Holiday Date", Prompt = "dd/MM/yyyy")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public System.DateTime HD_DATE { get; set; }

        [Required(ErrorMessage = "Please enter a valid description.")]
        [Display(Name = "Description", Prompt = "")]
        [StringLength(50, ErrorMessage = "Description cannot be longer than 50 characters.")]
        public string HD_DESC { get; set; }

        [Display(Name = "Is this holiday to be downloaded to Reader?", Prompt = "")]
        public bool HD_ISREADERDOWNLOAD { get; set; }

        [Display(Name = "Reader Downloaded Status", Prompt = "", Description = "Hidden")]
        public Nullable<bool> HD_UPDATE_STATUS { get; set; }

        [Display(Name = "Created Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> HD_CREATED { get; set; }

        [Display(Name = "Modified Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> HD_MODIFIED { get; set; }

        [Display(Name = "Modified By", Prompt = "Logged User", Description = "Hidden")]
        public string HD_MODIFIEDBY { get; set; }
    }
}
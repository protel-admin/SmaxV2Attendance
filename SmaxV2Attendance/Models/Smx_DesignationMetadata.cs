using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Models
{
    [MetadataType(typeof(Smx_DesignationMetadata))]
    public partial class Smx_Designation
    {

    }
    public class Smx_DesignationMetadata
    {
        [Display(Name = "Designation Name", Prompt = "")]
        [Required(ErrorMessage = "Please enter Designation Name")]
        [StringLength(50, ErrorMessage = "Designation Name cannot be longer than 50 characters.")]
        public string DN_NAME { get; set; }

        [Display(Name = "Short Name", Prompt = "")]
        [Required(ErrorMessage = "Please enter Short Name")]
        [StringLength(25, ErrorMessage = "Short Name cannot be longer than 25 characters.")]
        public string DN_SHORTNAME { get; set; }

        [Display(Name = "Created Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> DN_CREATED { get; set; }

        [Display(Name = "Modified Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> DN_MODIFIED { get; set; }

        [Display(Name = "Modified By", Prompt = "Logged User", Description = "Hidden")]
        public string DN_MODIFIEDBY { get; set; }
    }
}
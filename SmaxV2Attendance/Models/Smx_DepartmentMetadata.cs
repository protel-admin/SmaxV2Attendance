using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Models
{
    [MetadataType(typeof(Smx_DepartmentMetadata))]
    public partial class Smx_Department
    {

    }
    public class Smx_DepartmentMetadata
    {
        [Display(Name = "Department Name", Prompt = "")]
        [Required(ErrorMessage = "Please enter Department Name")]
        [StringLength(50, ErrorMessage = "Department Name cannot be longer than 50 characters.")]
        public string DP_NAME { get; set; }

        [Display(Name = "Short Name", Prompt = "")]
        [Required(ErrorMessage = "Please enter Short Name")]
        [StringLength(25, ErrorMessage = "Short Name cannot be longer than 25 characters.")]
        public string DP_SHORTNAME { get; set; }

        [Display(Name = "Created Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> DP_CREATED { get; set; }

        [Display(Name = "Modified Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> DP_MODIFIED { get; set; }

        [Display(Name = "Modified By", Prompt = "Logged User", Description = "Hidden")]
        public string DP_MODIFIEDBY { get; set; }
    }
}
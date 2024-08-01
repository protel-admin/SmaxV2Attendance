using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Models
{
    [MetadataType(typeof(Smx_BranchMetadata))]
    public partial class Smx_Branch
    {

    }
    public class Smx_BranchMetadata
    {
        [Display(Name = "Branch Name", Prompt = "e.g. Chennai")]
        [Required(ErrorMessage = "Please enter Branch Name")]
        [StringLength(50, ErrorMessage = "Branch Name cannot be longer than 50 characters.")]
        public string BR_Name { get; set; }

        [Display(Name = "Created Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> BR_CREATED { get; set; }

        [Display(Name = "Modified Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> BR_MODIFIED { get; set; }

        [Display(Name = "Modified By", Prompt = "Logged User", Description = "Hidden")]
        public string BR_MODIFIEDBY { get; set; }
    }
}
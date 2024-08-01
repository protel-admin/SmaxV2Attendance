using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Models
{
    [MetadataType(typeof(Smx_GroupMetadata))]
    public partial class Smx_Group
    {

    }
    public class Smx_GroupMetadata
    {
        [Display(Name = "Group Name", Prompt = "e.g. Software")]
        [Required(ErrorMessage = "Please enter Group Name")]
        [StringLength(50, ErrorMessage = "Group Name cannot be longer than 50 characters.")]
        public string Gr_Name { get; set; }

        [Display(Name = "Short Name", Prompt = "e.g. SW")]
        [StringLength(10, ErrorMessage = "Short Name cannot be longer than 10 characters.")]
        public string Gr_ShortName { get; set; }

        [Display(Name = "Created Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public System.DateTime Gr_Created { get; set; }

        [Display(Name = "Modified Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public System.DateTime Gr_Modified { get; set; }

        [Display(Name = "Modified By", Prompt = "Logged User", Description = "Hidden")]
        public string Gr_Modifiedby { get; set; }
    }
}
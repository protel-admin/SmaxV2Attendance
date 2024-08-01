using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Models
{
    [MetadataType(typeof(Smx_UnitMetadata))]
    public partial class Smx_Unit
    {

    }
    public class Smx_UnitMetadata
    {
        [Display(Name = "Unit Name", Prompt = "")]
        [Required(ErrorMessage = "Please enter Unit Name")]
        [StringLength(50, ErrorMessage = "Unit Name cannot be longer than 50 characters.")]
        public string UT_NAME { get; set; }

        [Display(Name = "Unit Description", Prompt = "")]
        [Required(ErrorMessage = "Please enter Unit Description")]
        [StringLength(150, ErrorMessage = "Unit Description cannot be longer than 150 characters.")]
        public string UT_DESCRIPTION { get; set; }

        [Display(Name = "Created Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> UT_CREATED { get; set; }

        [Display(Name = "Modified Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> UT_MODIFIED { get; set; }

        [Display(Name = "Modified By", Prompt = "Logged User", Description = "Hidden")]
        public string UT_MODIFIEDBY { get; set; }
    }
}
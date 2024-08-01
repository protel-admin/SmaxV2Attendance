using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Models
{
    [MetadataType(typeof(Smx_CategoryMetadata))]
    public partial class Smx_Category
    {

    }
    public class Smx_CategoryMetadata
    {
        [Display(Name = "Category Name", Prompt = "e.g. Chennai")]
        [Required(ErrorMessage = "Please enter Category Name")]
        [StringLength(50, ErrorMessage = "Category Name cannot be longer than 50 characters.")]
        public string CT_NAME { get; set; }

        [Display(Name = "Short Name", Prompt = "")]
        [Required(ErrorMessage = "Please enter Short Name")]
        [StringLength(25, ErrorMessage = "Short Name cannot be longer than 25 characters.")]
        public string CT_SHORTNAME { get; set; }

        [Display(Name = "Created Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> CT_CREATED { get; set; }

        [Display(Name = "Modified Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> CT_MODIFIED { get; set; }

        [Display(Name = "Modified By", Prompt = "Logged User", Description = "Hidden")]
        public string CT_MODIFIEDBY { get; set; }
    }
}
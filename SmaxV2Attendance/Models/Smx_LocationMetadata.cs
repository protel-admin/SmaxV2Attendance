using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Models
{
    [MetadataType(typeof(Smx_LocationMetadata))]
    public partial class Smx_Location
    {

    }
    public class Smx_LocationMetadata
    {
        [Display(Name = "Location Name", Prompt = "")]
        [Required(ErrorMessage = "Please enter Location Name")]
        [StringLength(50, ErrorMessage = "Location Name cannot be longer than 50 characters.")]
        public string LN_NAME { get; set; }

        [Display(Name = "Location Address", Prompt = "")]
        [Required(ErrorMessage = "Please enter Location Address")]
        [StringLength(150, ErrorMessage = "Location Address cannot be longer than 150 characters.")]
        public string LN_ADDRESS { get; set; }

        [Display(Name = "Short Name", Prompt = "")]
        [Required(ErrorMessage = "Please enter Short Name")]
        [StringLength(25, ErrorMessage = "Short Name cannot be longer than 25 characters.")]
        public string LN_SHORTNAME { get; set; }

        [Display(Name = "Created Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> LN_CREATED { get; set; }

        [Display(Name = "Modified Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> LN_MODIFIED { get; set; }

        [Display(Name = "Modified By", Prompt = "Logged User", Description = "Hidden")]
        public string LN_MODIFIEDBY { get; set; }
    }
}
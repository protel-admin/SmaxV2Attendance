using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Models
{
    [MetadataType(typeof(Smx_MessageMetadata))]
    public partial class Smx_Message
    {

    }

    public class Smx_MessageMetadata
    {
        [Display(Name = "Message Name", Prompt = "Enter Name between 8 to 50 letters")]
        [Required(ErrorMessage = "Please Select the Message Name")]
        public string MS_NAME { get; set; }

        [Display(Name = "Message", Prompt = "Message should be less than 16 letters")]
        [Required(ErrorMessage = "Please enter a valid Message")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Message Name must be between 6 and 16 characters.")]
        public string MS_LINE1 { get; set; }

        [Display(Name = "Message Line2", Description = "Hidden")]
        public string MS_LINE2 { get; set; }

        [Display(Name = "Created Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> MS_CREATED { get; set; }

        [Display(Name = "Modified Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> MS_MODIFIED { get; set; }

        [Display(Name = "Modified By", Prompt = "Logged User", Description = "Hidden")]
        public string MS_MODIFIEDBY { get; set; }
    }
}
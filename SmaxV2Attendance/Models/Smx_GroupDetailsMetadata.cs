using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Models
{
    [MetadataType(typeof(Smx_GroupDetailsMetadata))]

    public partial class Smx_GroupDetails
    {

    }

    public class Smx_GroupDetailsMetadata
    {
        [Display(Name = "Group Name", Prompt = "e.g. SoftwareDevelopment")]
        [Required(ErrorMessage = "Please Choose Group Name")]
        public decimal FK_GD_Id { get; set; }

        [Display(Name = "Unit", Prompt = "e.g. Plant-I")]
        [Required(ErrorMessage = "Please Choose Unit")]
        public int FK_GD_Unit { get; set; }

        [Display(Name = "Group", Prompt = "Software")]
        [Required(ErrorMessage = "Please Choose Group")]
        public string GD_Chid { get; set; }

        [Display(Name = "Shift", Prompt = "e.g. General", Description = "Hidden")]
        [Required(ErrorMessage = "Please Enter Shift Id")]
        public Nullable<decimal> FK_Sft_Id { get; set; }

        [Display(Name = "Created Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public System.DateTime GD_Created { get; set; }

        [Display(Name = "Modified Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public System.DateTime GD_Modified { get; set; }


        [Display(Name = "Modified By", Prompt = "Logged User", Description = "Hidden")]
        public string GD_Modifiedby { get; set; }

    }
}
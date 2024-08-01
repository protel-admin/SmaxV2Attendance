using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Models
{
    [MetadataType(typeof(Smx_CompanyMetadata))]
    public partial class Smx_Company
    {

    }

    public class Smx_CompanyMetadata
    {
        [Display(Name = "Company Name", Prompt = "")]
        [Required(ErrorMessage = "Please enter Company Name")]
        [StringLength(150, ErrorMessage = "Company Name cannot be longer than 150 characters.")]
        public string CG_NAME { get; set; }

        [Display(Name = "Company Short Name", Prompt = "")]
        [Required(ErrorMessage = "Please enter Company Short Name")]
        [StringLength(25, ErrorMessage = "Company Short Name cannot be longer than 25 characters.")]
        public string CG_SHORTNAME { get; set; }

        [Display(Name = "Company Head", Prompt = "")]
        [Required(ErrorMessage = "Please enter Company Company Head")]
        [StringLength(50, ErrorMessage = "Company Head cannot be longer than 50 characters.")]
        public string CG_HEAD { get; set; }

        [Display(Name = "Address", Prompt = "")]
        [StringLength(150, ErrorMessage = "Address cannot be longer than 150 characters.")]
        public string CG_ADDRESS { get; set; }

        [Display(Name = "City", Prompt = "")]
        [StringLength(50, ErrorMessage = "City cannot be longer than 50 characters.")]
        public string CG_CITY { get; set; }

        [Display(Name = "Phone", Prompt = "e.g. +91-44-67545678 / 914467545678")]
        [Phone]
        public string CG_PHONE { get; set; }

        [Display(Name = "Fax", Prompt = "e.g. +91-44-67545678 / 914467545678")]
        [Phone]
        public string CG_FAX { get; set; }

        [Display(Name = "Email", Prompt = "e.g. someone@xyz.com")]
        [EmailAddress]
        public string CG_EMAIL { get; set; }

        //[ScaffoldColumn(false)]
        [Display(Name = "Created Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public System.DateTime CG_CREATED { get; set; }

        [Display(Name = "Modified Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public System.DateTime CG_MODIFIED { get; set; }

        [Display(Name = "Modified By", Prompt = "Logged User", Description = "Hidden")]
        public string CG_MODIFIEDBY { get; set; }
    }
}
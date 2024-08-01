using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Models
{
    [MetadataType(typeof(Smx_CardHolderMetadata))]

    public partial class Smx_CardHolder
    {

    }

    public class Smx_CardHolderMetadata
    {

        [Display(Name = "Csnnumber", Prompt = "Enter Csnnumber")]
        [Required(ErrorMessage = "Please enter valid Csnnumber.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Csnnumber must be between 3 and 20 characters.")]
        public string Ch_Csnnumber { get; set; }
        
        [Display(Name = "Employee Id", Prompt = "Enter Employee Id")]
        [Required(ErrorMessage = "Please enter Employee ID.")]
        public string Ch_EmpId { get; set; }

        [Display(Name = "Title", Prompt = "")]
        public string Ch_Title { get; set; }

        [Display(Name = "First Name", Prompt = "")]
        [Required(ErrorMessage = "Please enter First Name.")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "First Name must be between 3 and 25 characters.")]
        public string Ch_FName { get; set; }

        [Display(Name = "Last Name", Prompt = "")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "Last Name must be between 1 and 25 characters.")]
        public string Ch_LName { get; set; }

        [Display(Name = "Short Name", Prompt = "")]
        [Required(ErrorMessage = "Please enter Short Name.")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Short Name must be between 3 and 25 characters.")]
        public string Ch_ShortName { get; set; }

        [Display(Name = "Date of Birth", Prompt = "")]
        public Nullable<System.DateTime> Ch_Dob { get; set; }

        [Display(Name = "Gender", Prompt = "")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Please Select a Gender.")]
        public string Ch_Gender { get; set; }

        [Display(Name = "Nationality", Prompt = "")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Nationality must be between 5 and 10 characters.")]
        public string Ch_Nationality { get; set; }

        [Display(Name = "Address", Prompt = "")]
        public string Ch_ContactAddress { get; set; }

        [Display(Name = "Phone", Prompt = "")]
        public string Ch_PhoneNumber { get; set; }

        [Display(Name = "Email", Prompt = "")]
        public string Ch_MailId { get; set; }

        [Display(Name = "Designation Name", Prompt = "")]
        //[Required(ErrorMessage = "Please Select Designation.")]
        public Nullable<int> Ch_Dn_Id { get; set; }

        [Display(Name = "Category Name", Prompt = "")]
        public Nullable<int> Ch_Ct_Id { get; set; }

        [Display(Name = "Company", Prompt = "")]
        //[Required(ErrorMessage = "Please Select Company.")]
        public Nullable<int> Ch_Cg_Id { get; set; }

        [Display(Name = "Unit", Prompt = "")]
        public Nullable<int> Ch_Ut_Id { get; set; }

        [Display(Name = "Branch", Prompt = "")]
        public Nullable<int> Ch_Br_Id { get; set; }

        [Display(Name = "Location", Prompt = "")]
        public Nullable<int> Ch_Ln_Id { get; set; }

        [Display(Name = "Department", Prompt = "")]
        //[Required(ErrorMessage = "Please Select Department.")]
        public Nullable<int> Ch_Dp_Id { get; set; }

        [Display(Name = "Photo", Prompt = "")]
        public byte[] Ch_Photo { get; set; }
        //public HttpPostedFileBase Ch_Photo { get; set; }

        [Display(Name = "Finger1 Captured", Prompt = "")]
        public byte[] Ch_Finger1 { get; set; }

        [Display(Name = "Finger2 Captured", Prompt = "")]
        public byte[] Ch_Finger2 { get; set; }

        //[Display(Name = "PIN Number", Prompt = "")]
        //[Range (0,9999, ErrorMessage = "Please enter a valid PIN")]
        
        //[RegularExpression(@"^[0-9]{4}$", ErrorMessage = "PIN Number should contain only numbesrs with 4 digits")]
        //public Nullable<int> Ch_PinNo { get; set; }

        [Display(Name = "Card Expiry Date", Prompt = "")]
        public Nullable<System.DateTime> Ch_ValidTo { get; set; }

        [Display(Name = "Card Status", Prompt = "")]
        //[Required(ErrorMessage = "Please Select Card Status.")]
        public Nullable<int> Ch_CS_Id { get; set; }

        [Display(Name = "Bio Enabled", Prompt = "")]
        public bool Ch_ISBio { get; set; }

        [Display(Name = "Card Enabled", Prompt = "")]
        public bool Ch_ISCard { get; set; }

        [Display(Name = "TriggerbyCard Enabled", Prompt = "")]
        public bool Ch_IsCardBio { get; set; }

        //[Display(Name = "PIN Enabled", Prompt = "")]
        //public bool Ch_ISPin { get; set; }

        //[Display(Name = "Anti-Passback Enabled", Prompt = "")]
        //public bool Ch_AntiPassBack { get; set; }

        [Display(Name = "Card Issued", Prompt = "")]
        public bool Ch_CardIssued { get; set; }

        [Display(Name = "Track Card", Prompt = "", Description = "Hidden")]
        public bool Ch_TrackCard { get; set; }

        [Display(Name = "Message", Prompt = "", Description = "Hidden")]
        public Nullable<int> Ch_MS_Id { get; set; }

        [Display(Name = "Date of Joining", Prompt = "")]
        public Nullable<System.DateTime> Ch_DOJ { get; set; }

        [Display(Name = "Employee Status", Prompt = "")]
        public Nullable<int> Ch_Es_ID { get; set; }

        [Display(Name = "Date of Superanum", Prompt = "")]
        public Nullable<System.DateTime> Ch_DOS { get; set; }

        [Display(Name = "Created Date", Prompt = "Current Date", Description = "Hidden")]
        public System.DateTime Ch_Created { get; set; }

        [Display(Name = "Modified Date", Prompt = "Modified Date", Description = "Hidden")]
        public System.DateTime Ch_Modified { get; set; }

        [Display(Name = "Modified By", Prompt = "Logged User", Description = "Hidden")]
        public string Ch_Modifiedby { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace SmaxV2Attendance.Models

{
    [MetadataType(typeof(UserMetadata))]

    public partial class User
    {

    }

    public class UserMetadata
    {
        [Display(Name = "User Name", Prompt = "Enter User Name")]
        [Required(ErrorMessage = "Please enter User Name")]
        [StringLength(150, ErrorMessage = "User Name cannot be longer than 150 characters.")]
        public string US_User { get; set; }

        [Display(Name = "Login Id", Prompt = "Enter Employee Id")]
        [Required(ErrorMessage = "Please enter Login Name")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{5,15}$", ErrorMessage = @"Error. Login Name must contain: Minimum 5 and Maximum 15 characters atleast 1 Alphabet and 1 Number")]      
        public string US_Login { get; set; }

        [Display(Name = "Password", Prompt = "Enter Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter Password")]
       
       // [StringLength(100, MinimumLength = 8, ErrorMessage = "Password should be 8 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}", ErrorMessage = @"Error. password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character")]
        public string US_Password { 
            get;
            set; }

        [Display(Name = "User Group")]
        [Required(ErrorMessage = "Please select User Group ")]        
        public int FK_UG_GroupId { get; set; }

        [Display(Name = "Created Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public System.DateTime US_Created { get; set; }

        [Display(Name = "Modified Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public System.DateTime US_Modified { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Models
{
    [MetadataType(typeof(Smx_DevicesMetadata))]
    public partial class Smx_Devices
    {

    }
    public class Smx_DevicesMetadata
    {
        [Display(Name = "Reader IP Address", Prompt = "e.g. 192.168.1.47")]
        [Required(ErrorMessage = "Please enter IP Address.")]
        //[RegularExpression("/^([0-9]{1,3}).([0-9]{1,3}).([0-9]{1,3}).([0-9]{1,3});([0-9]‌​{1,5})$/", ErrorMessage = "Please enter valid IP address \n (eg.)192.168.1.1")]
        [CustomValidation(typeof(Smx_DevicesMetadata), "ValidateDuplicate")]
        public string DE_IPADDRESS { get; set; }

        [Display(Name = "Node Id", Prompt = "a unique id number for the reader (1 to 255)")]
        [Required(ErrorMessage = "Please enter Node Id.")]
        public int DE_NODEID { get; set; }

        [Display(Name = "Device Name", Prompt = "Give a valid Device Name")]
        [Required(ErrorMessage = "Please enter a Device Name.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Device Name must be between 8 and 50 characters.")]
        public string DE_NAME { get; set; }

        [Display(Name = "Location Name", Prompt = "Give a valid Device Name")]
        [Required(ErrorMessage = "Please select a Location where the device is installed.")]
        public int DE_LN_ID { get; set; }

        [Display(Name = "Reader Message", Prompt = "Message to be Displaed on First Line")]
        [Required(ErrorMessage = "Please enter a Message.")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Message Name must be between 8 and 16 characters.")]
        public string DE_MESSAGE { get; set; }

        [Display(Name = "Reader Type")]
        [Required(ErrorMessage = "Please Select Device Type")]
        [Range(1, 3, ErrorMessage = "Can only be between 1 .. 3")]
        public string DE_READERTYPE { get; set; }

        //[Display(Name = "Reader Mode")]
        //[Required(ErrorMessage = "Please Select Device Mode")]
        //public string DE_READERMODE { get; set; }

        [Display(Name = "Relay Time", Prompt = "Please enter a value between 0 to 59")]
        [Required(ErrorMessage = "Please enter Relay Time.")]
        [Range(0, 59, ErrorMessage = "Can only be between 0 .. 59")]
        public Nullable<int> DE_RELAYTIME { get; set; }

        [Display(Name = "Reader Door Open Time Limit", Prompt = "Please enter a value between 0 to 59")]
        //[Required(ErrorMessage = "Please enter relay time.")]
        [Range(0, 59, ErrorMessage = "Enter value 0 to 59")]
        public Nullable<int> DE_DOTL { get; set; }

        [Display(Name = "Reader Door Open Time Zone")]
        public Nullable<decimal> DE_DOTZ { get; set; }

        [Display(Name = "Input1 Name")]
        public string DE_IP1_NAME { get; set; }

        [Display(Name = "Input2 Name")]
        public string DE_IP2_NAME { get; set; }

        [Display(Name = "Input1 NONC")]
        public string DE_IP1_NONC { get; set; }

        [Display(Name = "Input2 NONC")]
        public string DE_IP2_NONC { get; set; }

        [Display(Name = "Memory")]
        public string DE_MEMORY { get; set; }

        [Display(Name = "Reader Status")]
        public string DE_OPERATIONAL { get; set; }

        [Display(Name = "Model Name", Prompt = "Enter the Device Model Name")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Model Name must be between 6 and 50 characters.")]
        public string DE_MODEL { get; set; }

        [Display(Name = "Firmware", Prompt = "")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Firmware must be between 6 and 50 characters.")]
        public string DE_FIRMWARE { get; set; }

        [Display(Name = "Fire Alarm", Prompt = "")]
        public Nullable<int> DE_FIREALARM { get; set; }

        [Display(Name = "Created Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> DE_CREATED { get; set; }

        [Display(Name = "Modified Date", Prompt = "Current Date", Description = "Hidden")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> DE_MODIFIED { get; set; }

        [Display(Name = "Modified By", Prompt = "Logged User", Description = "Hidden")]
        public string DE_MODIFIEDBY { get; set; }

        public static ValidationResult ValidateDuplicate(string ipaddress)
        {
            bool isValid;

            using (var db = new SMAXV2Entities())
            {
                string str = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values["Action"].ToString(); //RequestContext.RouteData.Values["Action"];
                if (str == "Create")
                {
                    if (db.Smx_Devices.Where(e => e.DE_IPADDRESS.Equals(ipaddress)).Count() > 0)
                    {
                        isValid = false;
                    }
                    else
                    {
                        isValid = true;
                    }
                }
                else
                {
                    isValid = true;
                }
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Reader IPAddress already exists");
            }

        }
    }
}
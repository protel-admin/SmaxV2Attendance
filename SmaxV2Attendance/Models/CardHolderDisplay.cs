using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Models
{
    public partial class CardHolderDisplay
    {
        [Display(Name = "Id")]
        public decimal Ch_Id { get; set; }
        [Display(Name = "Csn Number")]
        public string Ch_Csnnumber { get; set; }
        [Display(Name = "Employee Id")]
        public string Ch_EmpId { get; set; }
        [Display(Name = "First Name")]
        public string Ch_FName { get; set; }
        [Display(Name = "Last Name")]
        public string Ch_LName { get; set; }
        [Display(Name = "Location Name")]
        public string LN_Name { get; set; }
        [Display(Name = "Card Status")]
        public string CS_Name { get; set; }
        [Display(Name = "Employee Status")]
        public string ES_Name { get; set; }
        [Display(Name = "Card Issued")]
        public bool Ch_CardIssued { get; set; }

    }
}
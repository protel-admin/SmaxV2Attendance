using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Models
{
    [MetadataType(typeof(Smx_TimeZoneMetadata))]
    public partial class Smx_TimeZone
    {

    }

    public class Smx_TimeZoneMetadata
    {
        [Display(Name = "Time Zone Name", Prompt = "e.g. General Shift")]
        public string TZ_NAME { get; set; }

        [Display(Name = "Reader Downloaded Status", Prompt = "")]
        public Nullable<bool> TZ_UPDATE_STATUS { get; set; }

        [Display(Name = "Created Date", Prompt = "")]
        public Nullable<System.DateTime> TZ_CREATED { get; set; }

        [Display(Name = "Modified Date", Prompt = "")]
        public Nullable<System.DateTime> TZ_MODIFIED { get; set; }

        [Display(Name = "Modified by", Prompt = "")]
        public string TZ_MODIFIEDBY { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmaxV2Attendance.Models
{
    [MetadataType(typeof(Smx_EventTaskMetadata))]
    public partial class Smx_EventTask
    {

    }
    public class Smx_EventTaskMetadata
    {
        [Display(Name = "Select Device", Prompt = "")]
        [Required(ErrorMessage = "Please Select a Device from the list")]
        public string ET_Device_IPAddress { get; set; }

        [Display(Name = "Node Id", Description = "Hidden")]
        public int ET_NodeId { get; set; }

        public string ET_IP3ACT_R3_Status { get; set; }
        public Nullable<int> ET_IP3ACT_R3_Time { get; set; }
        public Nullable<bool> ET_IP3ACT_R3_Message { get; set; }
        public Nullable<int> ET_IP3ACT_R3_Message_Duration { get; set; }
        public string ET_IP3NRM_R3_Status { get; set; }
        public Nullable<int> ET_IP3NRM_R3_Time { get; set; }
        public Nullable<bool> ET_IP3NRM_R3_Message { get; set; }
        public Nullable<int> ET_IP3NRM_R3_Message_Duration { get; set; }
        public string ET_IP4ACT_R3_Status { get; set; }
        public Nullable<int> ET_IP4ACT_R3_Time { get; set; }
        public Nullable<bool> ET_IP4ACT_R3_Message { get; set; }
        public Nullable<int> ET_IP4ACT_R3_Message_Duration { get; set; }
        public string ET_IP4NRM_R3_Status { get; set; }
        public Nullable<int> ET_IP4NRM_R3_Time { get; set; }
        public Nullable<bool> ET_IP4NRM_R3_Message { get; set; }
        public Nullable<int> ET_IP4NRM_R3_Message_Duration { get; set; }
    }
}
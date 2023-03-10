using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public class sp_VolumeByAppoint
    {
        [Key]
        public long? RowIndex { get; set; }
        public DateTime? Appointment_Date { get; set; }
        public string Appointment_Time { get; set; }
        public int? CountTM { get; set; }
        public string GoodsIssue_No { get; set; }
        public DateTime? GoodsIssue_Date { get; set; }
        public int? ASRS { get; set; }
        public int? Totebox { get; set; }
        public int? Labeling { get; set; }
    }
}

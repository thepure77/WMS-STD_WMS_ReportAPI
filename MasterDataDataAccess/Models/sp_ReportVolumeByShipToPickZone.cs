using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_ReportVolumeByShipToPickZone
    {
        
        public DateTime? Appointment_Date { get; set; }
        public string Appointment_Time { get; set; }
        public string TruckLoad_No        { get; set; }
        [Key]
        public string Branch           { get; set; }
        public string Shipto_id       { get; set; }
        public string Shipto_name { get; set; }
        public string Province { get; set; }
        public int? ASRS                { get; set; }
        public int? Totebox             { get; set; }
        public int? Labeling            { get; set; }
    }
}

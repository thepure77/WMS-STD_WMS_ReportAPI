using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.VolumeByAppointmentPickZone
{
    public class VolumeByAppointmentPickZoneViewModel
    {
        public int? rowNo { get; set; }
        public string appointment_Date { get; set; }
        public string appointment_Time { get; set; }
        public int? total_Shipment { get; set; }
        public string good_Issue_No { get; set; }
        public DateTime? wave_Date { get; set; }
        public int? asrs { get; set; }
        public int? toteBox { get; set; }
        public int? labelling { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }
    }
}

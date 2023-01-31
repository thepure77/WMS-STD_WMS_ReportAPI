using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportVolumeByShipToPickZoneViewModel
{
    public class ReportVolumeByShipToPickZoneViewModel
    {
        public int rowNo { get; set; }
        public string shipment_Date { get; set; }
        public string shipment_Time { get; set; }
        public string shipment_No { get; set; }
        public string branch_Code { get; set; }
        public string ship_To { get; set; }
        public string ship_To_Name { get; set; }
        public string province { get; set; }
        public int? aSRS { get; set; }
        public int? totebox { get; set; }
        public int? labeling { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }

    }
}

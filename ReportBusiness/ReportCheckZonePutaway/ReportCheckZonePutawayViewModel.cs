using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportCheckZonePutaway
{
    public class ReportCheckZonePutawayViewModel
    {
        public int? rowNum { get; set; }
        public string zoneputaway_Id { get; set; }
        public string zoneputaway_Name { get; set; }
        public int? countLocation_Name { get; set; }
        public int? countProduct { get; set; }
        //public string location_Name { get; set; }
        //Count Location_Name
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }
    }
}

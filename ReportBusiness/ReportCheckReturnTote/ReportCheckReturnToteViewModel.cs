using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportCheckReturnTote
{
    public class ReportCheckReturnToteViewModel
    {
        public int? rowNum { get; set; }
        public string truckLoad_No { get; set; }
        public DateTime? truck_Load_Return_Date { get; set; }
        public string return_Tote_MAX_XL { get; set; }
        public string return_Tote_MAX_M { get; set; }
        public string doc_Return_Max { get; set; }
        public int? return_Tote_Qty_XL { get; set; }
        public int? return_Tote_Qty_M { get; set; }
        public int? return_Tote_Qty_DMG_XL { get; set; }
        public int? return_Tote_Qty_DMG_M { get; set; }
        public int? return_Doc { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }
    }
}

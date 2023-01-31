using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.CheckGrPutaway
{
    public class CheckGrPutawayViewModel
    {
        public int rowNo { get; set; }
        public string goodsReceive_No { get; set; }
        public string documentType_Name { get; set; }
        public string po_No { get; set; }
        public string asp_No { get; set; }
        public string appointment_id { get; set; }
        public string goodsReceive_Date { get; set; }
        public string tag_No { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string product_Lot { get; set; }
        public string mfg_Date { get; set; }
        public string exp_Date { get; set; }
        public decimal? bu_Qty { get; set; }
        public string bu_Unit { get; set; }
        public decimal? su_Qty { get; set; }
        public string su_Unit { get; set; }
        public string suggest_Location_Name { get; set; }
        public string locationType_Name { get; set; }
        public string putaway_Date { get; set; }
        public int? tag_Status { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }
    }
}

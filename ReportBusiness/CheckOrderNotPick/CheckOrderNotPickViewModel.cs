using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.CheckOrderNotPick
{
    public class CheckOrderNotPickViewModel
    {
        public int rowNo { get; set; }
        public string truckLoad_No { get; set; }
        public string appointment_Id { get; set; }
        public string dock_Name { get; set; }
        public string appointment_Date { get; set; }
        public string appointment_Time { get; set; }
        public string planGoodsIssue_No { get; set; }
        public string shipTo_Id { get; set; }
        public string shipTo_Name { get; set; }
        public string branchCode { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public decimal? order_Qty { get; set; }
        public string order_Unit { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.CheckGIEXP
{
    public class CheckGIEXPViewModel
    {
        public int rowNo { get; set; }
        public string goodsIssue_No { get; set; }
        public string goodsIssue_Date { get; set; }
        public string planGoodsIssue_No { get; set; }
        public string shipTo_Id { get; set; }
        public string shipTo_Name { get; set; }
        public string product_ID { get; set; }
        public string product_Name { get; set; }
        public string product_Lot { get; set; }
        public DateTime? goodsReceive_EXP_Date { get; set; }
        public decimal? qty { get; set; }
        public decimal? totalQty { get; set; }
        public string erp_Location { get; set; }
        public string create_By { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }
    }
}

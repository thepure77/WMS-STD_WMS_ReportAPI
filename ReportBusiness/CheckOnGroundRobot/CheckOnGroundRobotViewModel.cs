using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.CheckOnGroundRobot
{
    public class CheckOnGroundRobotViewModel
    {
        public int rowNo { get; set; }
        
        public string product_ID { get; set; }
        public string product_Name { get; set; }
        public string location_Name { get; set; }
        public string tag_No { get; set; }
        public string product_Lot { get; set; }
        public string GoodsIssue_MFG_Date { get; set; }
        public string GoodsIssue_EXP_Date { get; set; }
        public decimal? bu_QtyOnHand { get; set; }
        public string bu_UNIT { get; set; }
        public decimal? su_QtyOnHand { get; set; }
        public string su_UNIT { get; set; }
        public string documentRef_No { get; set; }
        public int? robotGroup { get; set; }
        public string erp_Location { get; set; }
        public Guid? binBalance_Index { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }
    }
}

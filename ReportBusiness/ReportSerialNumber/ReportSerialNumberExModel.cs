using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportSerialNumber
{
    public class ReportSerialNumberExModel
    {
        public long? rowNo { get; set; }
        public string Tag_No { get; set; }
        public string GoodsIssue_No { get; set; }
        public string Wave_Round { get; set; }
        public string GoodsIssue_Date { get; set; }
        public string PlanGoodsIssue_No { get; set; }
        public string ShipTo_Id { get; set; }
        public string ShipTo_Name { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Serial { get; set; }
        public string Date_now_form { get; set; }
        
        public string ambientRoom { get; set; }
    }
}

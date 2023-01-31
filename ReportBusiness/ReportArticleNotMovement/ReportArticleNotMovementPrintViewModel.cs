using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportArticleNotMovement
{
    public class ReportArticleNotMovementPrintViewModel
    {
        public long? row_num { get; set; }
        public string ambientRoom { get; set; }
        public string tag_No { get; set; }
        public string location { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string business_Name { get; set; }
        public string productConversion_Name { get; set; }
        public string su_Unit { get; set; }
        public string su_Qty { get; set; }
        public string update_Date { get; set; }
        public string diff_Movement { get; set; }
        public string wms_Sloc{ get; set; }
        public string sap_Sloc { get; set; }
        public string status_Item { get; set; }
        public string mfg_date { get; set; }
        public string exp_date { get; set; }
        public string product_Lot { get; set; }
        public string date { get; set; }
    }
}

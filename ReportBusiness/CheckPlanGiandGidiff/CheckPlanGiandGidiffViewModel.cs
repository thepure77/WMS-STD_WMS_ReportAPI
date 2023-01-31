using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.CheckPlanGiandGidiff
{
    public class CheckPlanGiandGidiffViewModel
    {
        public int rowNo { get; set; }
        public string appointment_Id { get; set; }
        public string appointment_Date { get; set; }
        public string appointment_Time { get; set; }
        public string truckLoad_No { get; set; }
        public string order_Seq { get; set; }
        public string planGoodsIssue_No { get; set; }
        public string lineNum { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string runWave_Status { get; set; }
        public decimal? bu_Order_TotalQty { get; set; }
        public decimal? bu_GI_TotalQty { get; set; }
        public decimal? su_Order_TotalQty { get; set; }
        public decimal? su_GI_TotalQty { get; set; }
        public string su_Unit { get; set; }
        public string erp_Location { get; set; }
        public string product_Lot { get; set; }
        public decimal? diff { get; set; }
        public string document_Status { get; set; }
        public string goodsIssue_No { get; set; }
        public string document_Remark { get; set; }
        public string documentRef_No3 { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }
    }
}

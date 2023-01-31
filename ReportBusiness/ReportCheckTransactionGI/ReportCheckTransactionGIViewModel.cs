using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportCheckTransactionGI
{
    public class ReportCheckTransactionGIViewModel
    {
        public int? rowNum { get; set; }
        public string truckLoad_No { get; set; }
        public string appointment_Id { get; set; }
        public string dock_Name { get; set; }
        public string appointment_Date { get; set; }
        public string appointment_Time { get; set; }
        public string planGoodsIssue_No { get; set; }
        public string shipTo_Id { get; set; }
        public string shipTo_Name { get; set; }
        public string province { get; set; }
        public string branchCode { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public decimal? order_Qty { get; set; }
        public decimal? wHGI_QTY { get; set; }
        public decimal? tRGI_QTY { get; set; }
        public string order_UNIT { get; set; }
        public string goodsIssue_No { get; set; }
        public string goodsIssue_Date { get; set; }
        public string bill_No { get; set; }
        public string matdoc { get; set; }
        public decimal? bu_Order_Qty { get; set; }
        public decimal? bu_WHGIQty { get; set; }
        public decimal? bu_TRGI_QTY { get; set; }
        public string bu_Unit { get; set; }
        public decimal? su_Order_QTY { get; set; }
        public decimal? su_WHGIQty { get; set; }
        public decimal? su_TRGI_QTY { get; set; }
        public string su_Unit { get; set; }
        public string document_Remark { get; set; }
        public string documentRef_No3 { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }
    }
}

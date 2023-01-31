using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportCheckTransactionGR
{
    public class ReportCheckTransactionGRViewModel
    {
        public int? rowNum { get; set; }
        public string pO_No { get; set; }
        public string aSN_No { get; set; }
        public string aSN_Date { get; set; }
        public string aSN_Linenum { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public decimal? plan_Qty { get; set; }
        public decimal? gR_Qty { get; set; }
        public decimal? pending_Qty { get; set; }
        public string aSN_UNIT { get; set; }
        public decimal? bU_ASN_QTY { get; set; }
        public decimal? bU_GRQty { get; set; }
        public string bU_Unit { get; set; }
        public decimal? sU_ASN_QTY { get; set; }
        public decimal? sU_GRQty { get; set; }
        public string sU_Unit { get; set; }
        public string goodsReceive_No { get; set; }
        public string goodsReceive_Date { get; set; }
        public string matdoc { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string planGoodsReceive_No { get; set; }
        public string ambientRoom { get; set; }
        public string remark { get; set; }


    }
}

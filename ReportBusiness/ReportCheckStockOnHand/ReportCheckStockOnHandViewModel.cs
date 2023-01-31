using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportCheckStockOnHand
{
    public class ReportCheckStockOnHandViewModel
    {
        public int? rowNum { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string location_Name { get; set; }
        public string tag_No { get; set; }
        public string product_Lot { get; set; }
        public string goodsReceive_No { get; set; }
        public string itemStatus_Name { get; set; }
        public string goodsReceive_MFG_Date { get; set; }
        public string goodsReceive_EXP_Date { get; set; }
        public string goodsReceive_Date { get; set; }
        public decimal? bU_QtyBal { get; set; }
        public decimal? bU_QtyReserve { get; set; }
        public decimal? bU_QtyOnHand { get; set; }
        public string bU_UNIT { get; set; }
        public decimal? sU_QtyBal { get; set; }
        public decimal? sU_QtyReserve { get; set; }
        public decimal? sU_QtyOnHand { get; set; }
        public string sU_UNIT { get; set; }
        public string documentRef_No { get; set; }
        public string eRP_Location { get; set; }
        public int? ageRemain { get; set; }
        public int? productShelfLife_D { get; set; }
        public int? shelfLife_Remian { get; set; }
        public string pO_No { get; set; }
        public string aSN_NO { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }
    }
}

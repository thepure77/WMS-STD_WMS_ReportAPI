using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportCheckStockOnHandPartial
{
    public class ReportCheckStockOnHandPartialViewModel
    {
        public int? rowNum { get; set; }
        public string location_Name { get; set; }
        public string tag_No { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string product_Lot { get; set; }
        public string itemStatus_Name { get; set; }
        public string goodsReceive_EXP_Date { get; set; }
        public decimal? su_QtyBal { get; set; }
        public decimal? su_QtyReserve { get; set; }
        public decimal? su_QtyOnHand { get; set; }
        public string su_UNIT { get; set; }
        public string erp_Location { get; set; }
        public int? ageRemain { get; set; }
        public int? productShelfLife_D { get; set; }
        public int? shelfLife_Remian { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }
    }
}

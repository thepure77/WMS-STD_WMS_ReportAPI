using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.CheckReplenishByOrder
{
    public class CheckReplenishByOrderViewModel
    {
        public int rowNo { get; set; }
        public string goodsIssue_No { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public decimal? bu_QTY { get; set; }
        public decimal? order_QTY { get; set; }
        public string order_Unit { get; set; }
        public decimal? su_QTY { get; set; }
        public string su_UNIT { get; set; }
        public decimal? su_Weight { get; set; }
        public decimal? su_W { get; set; }
        public decimal? su_L { get; set; }
        public decimal? su_H { get; set; }
        public string isPeicePick { get; set; }
        public decimal? qtyInPiecePick_1 { get; set; }
        public decimal? qtyInPiecePick_2 { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }
    }
}

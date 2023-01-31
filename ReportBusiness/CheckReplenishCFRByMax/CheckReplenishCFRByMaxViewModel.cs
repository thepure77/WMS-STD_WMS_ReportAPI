using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.CheckReplenishCFRByMax
{
    public class CheckReplenishCFRByMaxViewModel
    {
        public int rowNo { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string productConversion_Name { get; set; }
        public decimal? productConversion_Ratio { get; set; }
        public string text { get; set; }
        public decimal? maxQty { get; set; }
        public decimal? pp_SuQty_1 { get; set; }
        public decimal? bb_SuQty_1 { get; set; }
        public decimal? replenQty { get; set; }
        public decimal? pp_QtyBal_2 { get; set; }
        public decimal? bb_QtyBal_2 { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }
    }
}

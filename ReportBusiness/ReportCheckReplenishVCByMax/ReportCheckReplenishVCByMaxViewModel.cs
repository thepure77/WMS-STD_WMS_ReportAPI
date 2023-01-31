using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportCheckReplenishVCByMax
{
    public class ReportCheckReplenishVCByMaxViewModel
    {
        public int? rowNum { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string productConversion_Name { get; set; }
        public decimal? productConversion_Ratio { get; set; }
        public string maxQty { get; set; }
        public decimal? pP_SuQty { get; set; }
        public decimal? bB_SuQty { get; set; }
        public decimal? replenQty { get; set; }
        public decimal? pP_QtyBal { get; set; }
        public decimal? bB_QtyBal { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }
    }
}

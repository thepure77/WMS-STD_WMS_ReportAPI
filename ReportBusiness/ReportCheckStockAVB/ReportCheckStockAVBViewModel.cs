using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportCheckStockAVB
{
    public class ReportCheckStockAVBViewModel
    {
        public int? rowNum { get; set; }
        public string currentDatetime { get; set; }
        public string last5Days { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public decimal? bu_QtyOnHand { get; set; }
        public decimal? bu_GIQty_5_Day { get; set; }
        public decimal? open_BU_Qty { get; set; }
        public decimal? bu_Balance { get; set; }
        public string bu_UNIT { get; set; }
        public decimal? bu_Converage_Day { get; set; }
        public decimal? su_QtyOnHand { get; set; }
        public decimal? su_GIQty_5_Day { get; set; }
        public decimal? open_SU_Qty { get; set; }
        public decimal? su_Balance { get; set; }
        public string su_UNIT { get; set; }
        public decimal? su_Converage_Day { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }

    }
}

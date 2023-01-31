using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.Report2
{
    public class Report2ViewModel
    {
        public Guid? goodsReceive_Index { get; set; }
                
        public string goodsReceive_No { get; set; }
        
        public string goodsReceive_Date { get; set; }

        public string goodsReceive_date { get; set; }

        public string goodsReceive_date_To { get; set; }

        public string product_Id { get; set; }

        public string product_Name { get; set; }

        public decimal? qtyUU { get; set; }

        public decimal? qtyQI { get; set; }
        public decimal? sumqty { get; set; }

        public bool checkQuery { get; set; }
    }


}

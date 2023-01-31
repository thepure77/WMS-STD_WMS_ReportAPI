using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportStockAging
{
    public class ReportStockAgingViewModel
    {
        public string owner_Id { get; set; }

        public string owner_Name { get; set; }

        public string product_Index { get; set; }

        public string product_Id { get; set; }

        public string product_Name { get; set; }

        public string productType_Name { get; set; }

        public string productConversion_Name { get; set; }

        public string goodsReceive_MFG_Date { get; set; } 

        public string goodsReceive_Date { get; set; }

        public string goodsReceive_date { get; set; }

        public string goodsReceive_date_To { get; set; }

        public int? age { get; set; }

        public string date_Print { get; set; }
        
        public int? rowCount { get; set; }

        public int? sumCount { get; set; }

        public bool checkQuery { get; set; }
    }


}

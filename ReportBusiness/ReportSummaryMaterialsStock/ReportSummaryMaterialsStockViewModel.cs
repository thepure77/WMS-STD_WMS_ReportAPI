using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportSummaryMaterialsStock
{
    public class ReportSummaryMaterialsStockViewModel
    {
        public Guid? goodsReceive_Index { get; set; }
                
        public string goodsReceive_No { get; set; }
        
        public string goodsReceive_Date { get; set; }

        public string goodsReceive_date { get; set; }

        public string goodsReceive_date_To { get; set; }

        public string product_Id { get; set; }

        public string productType_Name { get; set; }

        public string product_Name { get; set; }

        public string owner_Id { get; set; }

        public string owner_Name { get; set; }

        public decimal? qtyUU { get; set; }
        public decimal? totalQty { get; set; }
        public decimal? totalReserve { get; set; }

        public decimal? qtyQI { get; set; }
        public decimal? grl_TotalQty { get; set; }

        public decimal? sumqty { get; set; }

        public bool checkQuery { get; set; }

        public decimal? count { get; set; }
        public decimal? totalToPay { get; set; }

        public string productCategory_Id { get; set; }
        public string productCategory_Name { get; set; }
        public string ref_No3 { get; set; }
        public string ref_document_No { get; set; }

        public Guid? suggest_Location_Index { get; set; }
        public string suggest_Location_Id { get; set; }
        public string suggest_Location_Name { get; set; }
    }


}

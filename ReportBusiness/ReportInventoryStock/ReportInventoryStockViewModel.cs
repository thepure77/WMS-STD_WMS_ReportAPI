using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportInventoryStock
{
    public class ReportInventoryStockViewModel
    {
            
        public string goodsReceive_Date { get; set; }

        public string goodsReceive_date { get; set; }

        public string goodsReceive_date_To { get; set; }

        public string product_Id { get; set; }

        public string product_Name { get; set; }
        public string productType_Id { get; set; }
        public string productType_Name { get; set; }

        public string owner_Id { get; set; }

        public string owner_Name { get; set; }

        public string productConversion_Name { get; set; }

        public decimal? binBalance_QtyBal { get; set; }

        public decimal? binBalance_QtyBal_UR { get; set; }

        public decimal? binBalance_QtyBal_GR { get; set; }

        public decimal? binBalance_QtyBal_QI { get; set; }
        public decimal? binBalance_QtyReserve { get; set; }
        public decimal? binBalance_UnitHeightBal { get; set; }
        public decimal? binBalance_UnitWeightBal { get; set; }

        public decimal? stock { get; set; }
        public decimal? percentageStock { get; set; }


        public string dateToday { get; set; }
        public string ItemStatus_Id { get; set; }
        public string ItemStatus_Name { get; set; }
        public string gi_Date { get; set; }
        public string productCategory_Name { get; set; }
        public string ref_No3 { get; set; }
        public decimal? productConversion_Height { get; set; }
        public decimal? locationVol_Height { get; set; }
        public decimal? productConversion_Weight { get; set; }
    }
}

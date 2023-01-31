using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.Report7
{
    public class Report7ViewModel
    {
        public Guid? planGoodsIssue_Index { get; set; }

        public string planGoodsIssue_No { get; set; }

        public string goodsIssue_No { get; set; }

        public string planGoodsIssue_Date { get; set; }

        public string planGoodsIssue_date { get; set; }

        public string planGoodsIssue_date_To { get; set; }

        public string product_Id { get; set; }

        public string product_Name { get; set; }

        public decimal? qtyGI { get; set; }

        public decimal? SumQty { get; set; }

        public string goodsIssue_Date { get; set; }

        public string productConversion_Name { get; set; }

        public string productConversion_Name_SumQty { get; set; }

        public string create_By { get; set; }

        public string costCenter_Id { get; set; }

        public string costCenter_Name { get; set; }

        public Guid? index { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string key { get; set; }

        public bool checkQuery { get; set; }

        public string mc { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }
        public string shipTO_Id { get; set; }
        public string shipTO_Name { get; set; }
        public string sold_Id { get; set; }
        public string sold_Name { get; set; }
        public string location_Id { get; set; }
        public string location_Name { get; set; }
        public string user_Assign { get; set; }
        public decimal? qtyPGI { get; set; }
        public string picking_By { get; set; }
    }
}

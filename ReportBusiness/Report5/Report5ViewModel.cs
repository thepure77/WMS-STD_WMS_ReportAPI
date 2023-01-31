using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.Report5
{
    public class Report5ViewModel
    {
        public Guid? planGoodsIssue_Index { get; set; }

        public string planGoodsIssue_No { get; set; }

        public Guid? goodsIssue_Index { get; set; }

        public string goodsIssue_No { get; set; }

        public string goodsIssue_Date { get; set; }

        public string goodsIssue_date { get; set; }

        public string goodsIssue_date_To { get; set; }

        public string product_Id { get; set; }

        public string product_Name { get; set; }

        public decimal? qty { get; set; }
                
        public string productConversion_Name { get; set; }

        public string create_By { get; set; }

        public string location_Name { get; set; }

        public string documentRef_No2 { get; set; }

        public string goodsIssue_Time { get; set; }

        public string costCenter_Id { get; set; }

        public string costCenter_Name { get; set; }

        public string vendor_Id { get; set; }

        public string vendor_Name { get; set; }

        public string userAssign { get; set; }

        public bool checkQuery { get; set; }
    }


}

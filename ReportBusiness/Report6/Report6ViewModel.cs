using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.Report6
{
    public class Report6ViewModel
    {
        public long? row_Index { get; set; }
        public Guid? goodsIssue_Index { get; set; }
        public string goodsIssue_No { get; set; }
        public string planGoodsIssue_No { get; set; }
        public Guid? ref_Document_Index { get; set; }
        public Guid? ref_DocumentItem_Index { get; set; }
        public string ref_Document_No { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string location_Id { get; set; }
        public string location_Name { get; set; }
        public decimal? qty { get; set; }
        public decimal? totalQty { get; set; }
        public string productConversion_Name { get; set; }
        public string goodsIssue_Date { get; set; }
        public string goodsIssue_date { get; set; }
        public string goodsIssue_date_To { get; set; }
        public string create_By { get; set; }
        public string costCenter_Id { get; set; }
        public string vendor_Name { get; set; }
        public string userAssign { get; set; }

        public bool checkQuery { get; set; }

        public string mc { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }
        public string shipTO_Id { get; set; }
        public string shipTO_Name { get; set; }
        public string sold_Id { get; set; }
        public string sold_Name { get; set; }
    }


}

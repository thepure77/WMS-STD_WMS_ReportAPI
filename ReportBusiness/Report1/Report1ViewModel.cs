using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.Report1
{
    public partial class Report1ViewModel
    {
        public Report1ViewModel()
        {
            
            status = new List<statusViewModel>();

        }
        public Guid? planGoodsReceive_Index { get; set; }
        public Guid? planGoodsReceiveItem_Index { get; set; }
        public Guid? product_Index { get; set; }
        public Guid? productConversion_Index { get; set; }
        public string planGoodsReceive_Date { get; set; }
        public string planGoodsReceive_No { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }
        public string documentRef_No2 { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string planGoodsReceive_Due_Date { get; set; }
        public string productCategory_Name { get; set; }
        public string ref_No3 { get; set; }
        public decimal? totalQty { get; set; }
        public string productConversion_Name { get; set; }
        public string min_GoodsReceive_Date { get; set; }
        public decimal? gr_TotalQty { get; set; }
        public decimal? remain_TotalQty { get; set; }
        public string max_GoodsReceive_Date { get; set; }


        public string planGoodsReceive_date { get; set; }
        public string planGoodsReceive_date_To { get; set; }

        public List<statusViewModel> status { get; set; }

        public class statusViewModel
        {
            public int value { get; set; }
            public string display { get; set; }
            public int seq { get; set; }
        }
    }
}

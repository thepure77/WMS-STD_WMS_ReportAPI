using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ReportBusiness.ReportRetrun
{
   public class ReportRetrunViewModel
    {
        public long? Row_Index { get; set; }
        public string planGoodsReceive_Date_From { get; set; }
        public string planGoodsReceive_Date_To { get; set; }
        public string planGoodsReceive_Date { get; set; }
        public string plant { get; set; }
        public string planGoodsReceive_No { get; set; }
        public string goodsReceive_No { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string unit { get; set; }
        public decimal qty_WMS { get; set; }
        public decimal? qty_SAP { get; set; }
        public string status { get; set; }
    }
}

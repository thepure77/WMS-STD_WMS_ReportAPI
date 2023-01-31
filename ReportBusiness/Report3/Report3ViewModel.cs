using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ReportBusiness.Report3
{
    public class Report3ViewModel
    {
        public string owner_Id { get; set; }
        public string documentType_Index { get; set; }
        public string owner_Name { get; set; }
        public string goodsReceive_No { get; set; }
        public string po_no { get; set; }
        public string productCategory_Name { get; set; }
        public string ref_No3 { get; set; }
        public string putawayLocation_Id { get; set; }
        public string putawayLocation_Name { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public decimal? qty { get; set; }
        public string tag_No { get; set; }
        public string productConversion_Id { get; set; }
        public string productConversion_Name { get; set; }
        public string pack_Size { get; set; }
        public string TixHi { get; set; }
        public decimal? productConversion_Height { get; set; }
        public decimal? locationVol_Height { get; set; }
        public decimal? productConversion_Weight { get; set; }
        public decimal? qty_Per_Tag { get; set; }
        public string putaway_Date { get; set; }
        public string putaway_Time { get; set; }
        public string itemStatus_Id { get; set; }
        public string itemStatus_Name { get; set; }
        public string putaway_By { get; set; }
        public string goodsReceive_Date { get; set; }
        public string goodsReceive_date { get; set; }
        public string goodsReceive_date_To { get; set; }
        public string PO_No { get; set; }
    }
}

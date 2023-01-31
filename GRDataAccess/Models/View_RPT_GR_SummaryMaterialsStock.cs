using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GRDataAccess.Models
{
    public partial class View_RPT_GR_SummaryMaterialsStock
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid? GoodsReceive_Index { get; set; }
        public Guid? GoodsReceiveItem_Index { get; set; }
        public string GoodsReceive_No { get; set; }
        public DateTime? GoodsReceive_Date { get; set; }
        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public Guid? Owner_Index { get; set; }
        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }
        public decimal? TotalQty { get; set; }
        public decimal? GRL_TotalQty { get; set; }
        public decimal? Tag_TotalQty { get; set; }
        public decimal? TotalReserve { get; set; }
        public Guid? Ref_document_Index { get; set; }
        public Guid? Ref_documentitem_Index { get; set; }
        public string Ref_document_No { get; set; }
        public Guid? Suggest_Location_Index { get; set; }
        public string Suggest_Location_Id { get; set; }
        public string Suggest_Location_Name { get; set; }
    }
}

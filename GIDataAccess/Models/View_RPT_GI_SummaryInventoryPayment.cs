using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace GIDataAccess.Models
{
    public partial class View_RPT_GI_SummaryInventoryPayment
    {
 
        [Key]
        public long? Row_Index { get; set; }
        public Guid? GoodsIssue_Index { get; set; }
        public string GoodsIssue_No { get; set; }
        public Guid? Ref_Document_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public DateTime? GoodsIssue_Date { get; set; }
        public string Update_By { get; set; }
        public Guid? Location_Index { get; set; }
        public string Location_Id { get; set; }
        public string Location_Name { get; set; }
        public string GoodsIssue_Time { get; set; }
        public Guid? GoodsReceiveItem_Index { get; set; }
        public string Owner_Name { get; set; }
        public Guid? Owner_Index { get; set; }
        public string Owner_Id { get; set; }
        public decimal? totalQty { get; set; }
        public string Picking_By { get; set; }
        public DateTime? Picking_Date { get; set; }


    }
}

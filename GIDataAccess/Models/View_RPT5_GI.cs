using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace GIDataAccess.Models
{
    public partial class View_RPT5_GI
    {
 
        [Key]
        public long? Row_Index { get; set; }
        public Guid? GoodsIssue_Index { get; set; }
        public string GoodsIssue_No { get; set; }
        public Guid? Ref_Document_Index { get; set; }
        public Guid? Ref_DocumentItem_Index { get; set; }
        public string Ref_Document_No { get; set; }

        public string Product_Id { get; set; }
        public string Product_Name { get; set; }

        public decimal? Qty { get; set; }
        public decimal? TotalQty { get; set; }

        public string ProductConversion_Name { get; set; }
        public DateTime? GoodsIssue_Date { get; set; }
        public string Create_By { get; set; }
        public string Location_Name { get; set; }
        public string DocumentRef_No2 { get; set; }
        public string GoodsIssue_Time { get; set; }
        public string Picking_By { get; set; }
        public Guid? GoodsReceiveItem_Index { get; set; }
        


    }
}

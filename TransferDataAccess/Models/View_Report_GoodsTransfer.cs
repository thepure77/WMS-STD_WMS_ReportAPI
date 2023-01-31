using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TransferDataAccess.Models
{
    public partial class View_Report_GoodsTransfer
    {
        [Key]
        public long? RowIndex { get; set; }
        //public int? RowNum { get; set; }
        public Guid? GoodsTransfer_Index { get; set; }
        public string GoodsTransfer_No { get; set; }
        public Guid? DocumentType_Index { get; set; }
        public string DocumentType_Id { get; set; }
        public string DocumentType_Name { get; set; }
        public DateTime? Create_Date { get; set; }
        public DateTime? GoodsTransfer_Date { get; set; }
        public string Tag_No { get; set; }
        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_Lot { get; set; }
        public decimal? Qty { get; set; }
        public decimal? SumQty { get; set; }
        public string Unit { get; set; }
        public decimal? TotalQty { get; set; }
        public decimal? SumTotalQty { get; set; }
        public string SubUnit { get; set; }
        public string ItemStatus_Name { get; set; }
        public string ItemStatus_Name_To { get; set; }
        public Guid? Location_Index { get; set; }
        public string Location_Name { get; set; }
        public Guid? Location_Index_To { get; set; }
        public string Location_Name_To { get; set; }
        public string ERP_Location { get; set; }
        public string ERP_Location_To { get; set; }
        public int? Document_Status { get; set; }
        public string DocumentStatus_Name { get; set; }
        public int? Item_Document_Status { get; set; }
        public Guid? GoodsReceiveItemLocation_Index { get; set; }
        public Guid GoodsTransferItem_Index { get; set; }
        public string Update_By { get; set; }
        public string Create_By { get; set; }
        public Guid? LocationType_Index{ get; set; }
        public string LocationType_Id{ get; set; }
        public string LocationType_Name { get; set; }
    }
}

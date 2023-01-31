using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_ReportCheckTransfer
    {
        [Key]
        public long? RowIndex { get; set; }
        public string GoodsTransfer_No { get; set; }
        public string DocumentType_Name { get; set; }
        public DateTime? Create_Date                        { get; set; }
        public string Create_By                          { get; set; }
        public string Tag_No                             { get; set; }
        public string Product_Id                         { get; set; }
        public string Product_Name                       { get; set; }
        public decimal? Qty                                { get; set; }
        public decimal? TotalQty                           { get; set; }
        public string Location_Name                      { get; set; }
        public string Location_Name_To                   { get; set; }
        public string ItemStatus_Name                    { get; set; }
        public string ItemStatus_Name_To                 { get; set; }
        public string ERP_Location                       { get; set; }
        public string ERP_Location_To                    { get; set; }
        public int? Document_Status                    { get; set; }
        public int? Item_Document_Status               { get; set; }
        public Guid? GoodsReceiveItemLocation_Index     { get; set; }
        public Guid? GoodsTransferItem_Index            { get; set; }
    }
}

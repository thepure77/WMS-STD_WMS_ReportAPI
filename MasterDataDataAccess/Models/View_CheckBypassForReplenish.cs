using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public class View_CheckBypassForReplenish
    {
        [Key]
        public long? RowIndex { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string ReplenishmentLocation { get; set; }
        public string Location_Name { get; set; }
        public int? Location_is { get; set; }
        public string Tag_No { get; set; }
        public decimal? MaxQty { get; set; }
        public decimal? MinQty { get; set; }
        public decimal? PiecePickQty { get; set; }
        public decimal? SU_BinBalance_QtyBal { get; set; }
        public decimal? SU_BinBalance_QtyReserve { get; set; }
        public decimal? SU_Qty_Remain { get; set; }
        public string SaleUnit { get; set; }
        public string ERP_Location { get; set; }
        public string ItemStatus_Name { get; set; }
        public DateTime? GoodsReceive_EXP_Date { get; set; }
        public int? ShelfLife { get; set; }
        public int? AgeRemain { get; set; }

    }
}

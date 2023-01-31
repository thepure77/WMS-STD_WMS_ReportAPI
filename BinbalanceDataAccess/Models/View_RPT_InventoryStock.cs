using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinBalanceDataAccess.Models
{

    public partial class View_RPT_InventoryStock
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public Guid? Owner_Index { get; set; }
        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }
        public string ProductConversion_Name { get; set; }
        public decimal? BinBalance_QtyBal { get; set; }
        public decimal? BinBalance_QtyBal_UR { get; set; }
        public decimal? BinBalance_QtyBal_GR { get; set; }
        public decimal? BinBalance_QtyBal_QI { get; set; }
        public decimal? BinBalance_QtyReserve { get; set; }
        public decimal? BinBalance_UnitHeightBal { get; set; }
        public decimal? BinBalance_UnitWeightBal { get; set; }
        public Guid? Location_Index { get; set; }
        public DateTime? GoodsReceive_Date { get; set; }
        public DateTime? GI_Date { get; set; }
        public Guid? ItemStatus_Index { get; set; }
        public string ItemStatus_Id { get; set; }
        public string ItemStatus_Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinBalanceDataAccess.Models
{

    public partial class View_RPT_BinBalance_SummaryMaterialsStock
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid? BinBalance_Index { get; set; }
        public Guid? GoodsReceiveItemLocation_Index { get; set; }
        public decimal? BinBalance_QtyBal { get; set; }
        public string GoodsReceive_No { get; set; }
        public DateTime? GoodsReceive_Date { get; set; }
        public Guid? ItemStatus_Index { get; set; }
        public string ItemStatus_Id { get; set; }
        public string ItemStatus_Name { get; set; }
        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }

    }
}

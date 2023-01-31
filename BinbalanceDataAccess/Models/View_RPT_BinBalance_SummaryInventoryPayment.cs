using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinBalanceDataAccess.Models
{

    public partial class View_RPT_BinBalance_SummaryInventoryPayment
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid? BinBalance_Index { get; set; }
        public Guid? GoodsReceiveItemLocation_Index { get; set; }
        public DateTime? GoodsReceive_Date { get; set; }
        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }

    }
}

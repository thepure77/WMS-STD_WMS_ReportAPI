using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinBalanceDataAccess.Models
{

    public partial class View_RPT14_BinBalance
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid? BinBalance_Index { get; set; }
        public decimal? BinBalance_QtyReserve { get; set; }
        public decimal? BinBalance_QtyBal_UU { get; set; }
        public decimal? BinBalance_QtyBal_QI { get; set; }
        public DateTime? GoodsReceive_Date { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string ProductConversion_Name { get; set; }
        
    }
}

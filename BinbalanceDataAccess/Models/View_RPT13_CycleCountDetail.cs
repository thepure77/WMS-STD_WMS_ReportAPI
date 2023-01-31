using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinBalanceDataAccess.Models
{

    public partial class View_RPT13_CycleCountDetail
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public DateTime? CycleCount_Date { get; set; }
        public decimal? Qty_Count { get; set; }
        public decimal? Qty_Bal { get; set; }
    }
}

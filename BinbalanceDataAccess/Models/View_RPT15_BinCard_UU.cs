using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinBalanceDataAccess.Models
{

    public partial class View_RPT15_BinCard_UU
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string ProductConversion_Name { get; set; }
        public DateTime? BinCard_Date { get; set; }
        public decimal? BinCard_QtyUU { get; set; }
        public decimal? BinCard_QtyQI { get; set; }




    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinBalanceDataAccess.Models
{

    public partial class View_RPT16_BinCard
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid? Process_Index { get; set; }
        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string ProductConversion_Name { get; set; }
        public string DocumentType_Name { get; set; }
        public DateTime? BinCard_Date { get; set; }
        public decimal? BinCard_QtyIn { get; set; }
        public decimal? BinCard_QtyOut { get; set; }
        public Guid? Owner_Index { get; set; }
        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }
        public string Movement_Type { get; set; }


    }
}

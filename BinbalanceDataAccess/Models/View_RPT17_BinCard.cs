using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinBalanceDataAccess.Models
{

    public partial class View_RPT17_BinCard
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid? BinCard_Index { get; set; }
        public Guid? Owner_Index { get; set; }
        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }
        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public Guid? ProductConversion_Index { get; set; }
        public string ProductConversion_Id { get; set; }
        public string ProductConversion_Name { get; set; }
        public decimal? BinCard_QtyIn { get; set; }
        public decimal? BinCard_QtyOut { get; set; }
        public Guid? Ref_Document_Index { get; set; }
        public DateTime? Create_Date { get; set; }
        public string Create_By { get; set; }
        public DateTime? BinCard_Date { get; set; }
        public int? BinCard_Month { get; set; }



    }
}

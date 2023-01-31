using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GRDataAccess.Models
{
    public partial class View_RPT_GR
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid? GoodsReceive_Index { get; set; }
        public Guid? Ref_Document_Index { get; set; }
        public Guid? Ref_DocumentItem_Index { get; set; }
        public string Ref_Document_No { get; set; }

        public string Product_Id { get; set; }
        public string Product_Name { get; set; }

        public decimal? Qty { get; set; }
        public decimal? TotalQty { get; set; }

        public string ProductConversion_Name { get; set; }
        public DateTime? GoodsReceive_Date { get; set; }
   
    }
}

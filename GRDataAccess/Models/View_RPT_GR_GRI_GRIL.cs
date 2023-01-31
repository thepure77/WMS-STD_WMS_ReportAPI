using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GRDataAccess.Models
{
    public partial class View_RPT_GR_GRI_GRIL
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid? GoodsReceive_Index { get; set; }
        public Guid? GoodsReceiveItem_Index { get; set; }
        public Guid? GoodsReceiveItemLocation_Index { get; set; }
        public string GoodsReceive_No { get; set; }
        public DateTime? GoodsReceive_Date { get; set; }
        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public decimal? Qty { get; set; }

    }
}

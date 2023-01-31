using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GRDataAccess.Models
{


    public partial class View_PrintOutRetrun
    {
        [Key]
        public long? Row_Index { get; set; }
        public long? Number { get; set; }
        public DateTime? PlanGoodsReceive_Date { get; set; }
        public string Plant { get; set; }
        public string PlanGoodsReceive_No { get; set; }
        public string GoodsReceive_No { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Unit { get; set; }
        public decimal Qty_WMS { get; set; }
        public decimal? Qty_SAP { get; set; }
        public string Status { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PlanGRDataAccess.Models
{
    public partial class View_RPT_PlanGR
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid PlanGoodsReceive_Index { get; set; }
        public Guid PlanGoodsReceiveItem_Index { get; set; }
        public string PlanGoodsReceive_No { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? PlanGoodsReceive_Date { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? PlanGoodsReceive_Due_Date { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Qty { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Ratio { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TotalQty { get; set; }

        public string ProductConversion_Name { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PlanGRDataAccess.Models
{
    public partial class View_RPT_PlanGRV2
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid PlanGoodsReceiveItem_Index { get; set; }
        public Guid PlanGoodsReceive_Index { get; set; }
        public Guid? Product_Index { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? PlanGoodsReceive_Date { get; set; }
        public string PlanGoodsReceive_No { get; set; }
        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }
        public string DocumentRef_No2 { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? PlanGoodsReceive_Due_Date { get; set; }
        [Column(TypeName = "numeric")]
        public decimal TotalQty { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace PlanGIDataAccess.Models
{

    public partial class View_RPT_PlanGI
    {

        [Key]
        public long? Row_Index { get; set; }
        public Guid? PlanGoodsIssue_Index { get; set; }
        public Guid? PlanGoodsIssueItem_Index { get; set; }
        public string PlanGoodsIssue_No { get; set; }
        public DateTime? PlanGoodsIssue_Date { get; set; }
        public DateTime? PlanGoodsIssue_Due_Date { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Ratio { get; set; }
        public decimal? TotalQty { get; set; }
        public string ProductConversion_Name { get; set; }
        public string Create_By { get; set; }
        public Guid? CostCenter_Index { get; set; }
        public string CostCenter_Id { get; set; }
        public string CostCenter_Name { get; set; }
        public Guid? SoldTo_Index { get; set; }
        public string SoldTo_Id { get; set; }
        public string SoldTo_Name { get; set; }
        public Guid? ShipTo_Index { get; set; }
        public string ShipTo_Id { get; set; }
        public string ShipTo_Name { get; set; }
        public Guid? Owner_Index { get; set; }
        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }
    }
}

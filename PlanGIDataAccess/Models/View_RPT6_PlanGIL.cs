using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace PlanGIDataAccess.Models
{

    public partial class View_RPT6_PlanGIL
    {
        [Key]
        public Guid? PlanGoodsIssue_Index { get; set; }
        public string PlanGoodsIssue_No { get; set; }
        public Guid? CostCenter_Index { get; set; }
        public string CostCenter_Id { get; set; }
        public string CostCenter_Name { get; set; }
    }
}

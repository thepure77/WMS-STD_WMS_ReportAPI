using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace PlanGIDataAccess.Models
{

    public partial class View_RPT_PlanGI_SummaryInventoryPayment
    {

        [Key]
        public long? Row_Index { get; set; }
        public Guid? PlanGoodsIssue_Index { get; set; }
        public string PlanGoodsIssue_No { get; set; }
        public string ProductConversion_Name { get; set; }
        public string SoldTo_Name { get; set; }
        public string ShipTo_Name { get; set; }
        public string ShipTo_Address { get; set; }
        public string DocumentItem_Remark { get; set; }
        public decimal? Qty { get; set; }

    }
}

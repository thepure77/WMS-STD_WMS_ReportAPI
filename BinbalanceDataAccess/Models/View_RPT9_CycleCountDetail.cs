using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinBalanceDataAccess.Models
{

    public partial class View_RPT9_CycleCountDetail
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid? CycleCountItem_Index { get; set; }
        public Guid? CycleCountDetail_Index { get; set; }
        public Guid? CycleCount_Index { get; set; }
        public Guid? Location_Index { get; set; }
        public string Location_Id { get; set; }
        public string Location_Name { get; set; }
        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public Guid? Owner_Index { get; set; }
        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }
        public Guid? ItemStatus_Index { get; set; }
        public string ItemStatus_Id { get; set; }
        public string ItemStatus_Name { get; set; }
        public decimal? Qty_Bal { get; set; }
        public decimal? Qty_Count { get; set; }
        public decimal? Qty_Diff { get; set; }
        public string Count_status { get; set; }
        public DateTime? Create_Date { get; set; }
        public string Create_By { get; set; }

    }
}

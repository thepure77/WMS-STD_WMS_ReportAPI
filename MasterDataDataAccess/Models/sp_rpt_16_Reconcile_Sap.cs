using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace MasterDataDataAccess.Models
{
    public partial class sp_rpt_16_Reconcile_Sap
    {
        [Key]
        public Guid Row_Index { get; set; }
        public Int64 Row_No { get; set; }
        public string Warehouse_Type { get; set; }
        public Guid BusinessUnit_Index { get; set; }
        public string BusinessUnit_Name { get; set; }
        public Guid Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public decimal? Bu_Qty { get; set; }
        public string Bu_Unit { get; set; }
        public decimal? Su_Qty { get; set; }
        public decimal? Su_Ratio { get; set; }
        public string Su_Unit { get; set; }
        public string Plant { get; set; }
        public string Sap_Sloc { get; set; }
    }
}

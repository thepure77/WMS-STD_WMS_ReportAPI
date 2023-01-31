using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_rpt_01_Traceability
    {
        [Key]
        public Guid Row_Index { get; set; }
        public long Row_No { get; set; }
        public string Warehouse_Type { get; set; }
        public string BusinessUnit_Name { get; set; }
        public Guid? Vendor_Index { get; set; }
        public string Vendor_Id { get; set; }
        public string Vendor_Name { get; set; }
        public string PurchaseOrder_No { get; set; }
        public Guid? GoodsReceiveItem_Index { get; set; }
        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public decimal? PO_Qty { get; set; }
        public decimal? Inbound_SumQty { get; set; }
        public decimal? Percent_GR { get; set; }
        public decimal? Outbound_SumQty { get; set; }
        public decimal? Percent_GI { get; set; }
        public DateTime? MFG_Date { get; set; }
        public DateTime? EXP_Date { get; set; }
        public DateTime? GoodsReceive_Date { get; set; }
        public DateTime? GoodsIssue_Date { get; set; }        
        public string Product_Lot { get; set; }
        public string ERP_Location { get; set; }
        public string ShipTo_Id { get; set; }
        public string Branch_Code { get; set; }
        public string Sale_Unit { get; set; }
    }

}

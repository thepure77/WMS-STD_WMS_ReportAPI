using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_ReportCheckStockOnHand
    {
        [Key]
        public long? RowIndex { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Location_Name { get; set; }
        public string Tag_No { get; set; }
        public string Product_Lot { get; set; }
        public string GoodsReceive_No { get; set; }
        public string ItemStatus_Name { get; set; }
        public DateTime? GoodsReceive_MFG_Date { get; set; }
        public DateTime? GoodsReceive_EXP_Date { get; set; }
        public DateTime? GoodsReceive_Date { get; set; }
        public decimal? BU_QtyBal { get; set; }
        public decimal? BU_QtyReserve { get; set; }
        public decimal? BU_QtyOnHand { get; set; }
        public string BU_UNIT { get; set; }
        public decimal? SU_QtyBal { get; set; }
        public decimal? SU_QtyReserve { get; set; }
        public decimal? SU_QtyOnHand { get; set; }
        public string SU_UNIT { get; set; }
        public string DocumentRef_No { get; set; }
        public string ERP_Location { get; set; }
        public int? AgeRemain { get; set; }
        public int? ProductShelfLife_D { get; set; }
        public int? ShelfLife_Remian { get; set; }
        public string PO_No { get; set; }
        public string ASN_NO { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace MasterDataDataAccess.Models
{
    public partial class sp_rpt_18_Movement
    {
        [Key]
        public Guid Row_Index { get; set; }
        public Int64 Row_No { get; set; }
        public string Warehouse_Type { get; set; }
        public Guid BusinessUnit_Index { get; set; }
        public string BusinessUnit_Name { get; set; }
        public Guid? Vendor_Index { get; set; }
        public string Vendor_Id { get; set; }
        public string Vendor_Name { get; set; }
        public Guid Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public Guid ProductConversion_Index { get; set; }
        public string ProductConversion_Id { get; set; }
        public string ProductConversion_Name { get; set; }
        public decimal? BinCard_QtyIn { get; set; }
        public decimal? binCard_QtyOut { get; set; }
        public string TypeMovement { get; set; }
        public DateTime? GoodsReceive_Date { get; set; }
        public DateTime? Update_Date { get; set; }
        public Guid Tag_Index { get; set; }
        public string Tag_No { get; set; }
        public string WMS_Sloc { get; set; }
        public string SAP_Sloc { get; set; }
        public string Product_Lot { get; set; }
        public DateTime? GoodsReceive_MFG_Date { get; set; }
        public DateTime? GoodsReceive_EXP_Date { get; set; }

    }
}

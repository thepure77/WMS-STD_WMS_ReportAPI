using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace MasterDataDataAccess.Models
{
    public partial class sp_rpt_20_Ageging
    {
        [Key]
        public Guid Row_Index { get; set; }

        public string BusinessUnit_Name { get; set; }

        public string Tag_No { get; set; }

        public string Location_Name { get; set; }

        public string Owner_Id { get; set; }

        public string Owner_Name { get; set; }

        public string Product_Id { get; set; }

        public string Product_Name { get; set; }

        public string ProductConversion_Name { get; set; }

        public Int32? EXP_DATE { get; set; }

        public string WMS_Sloc { get; set; }

        public string SAP_Sloc { get; set; }

        public DateTime? GoodsReceive_Date { get; set; }

        public DateTime? GoodsReceive_MFG_Date { get; set; }

        public DateTime? GoodsReceive_EXP_Date { get; set; }

        public string Product_Lot { get; set; }

        public string Warehouse_Type { get; set; }
    }
}

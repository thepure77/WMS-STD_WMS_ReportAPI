using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
    {
        public partial class sp_GetUniversalSearch
        {
            [Key]
            public Guid RowIndex { get; set; }

            public string Location_Id { get; set; }

            public string Tag_No { get; set; }

            public string Product_Id { get; set; }

            public string Product_Name { get; set; }

            public decimal? QtySaleUnit { get; set; }

            public string Sale_Unit { get; set; }

            public DateTime? MFG_Date { get; set; }

            public DateTime? EXP_Date { get; set; }

            public DateTime? Receive_Date { get; set; }

            public string Product_Lot { get; set; }

            public string ERP_Location { get; set; }

            public decimal? QtyBaseUnit { get; set; }

            public string Base_Unit { get; set; }

            public string Business_Unit { get; set; }

            public string TempCondition_Name { get; set; }

            public string Vendor_Name { get; set; }

            public string ItemStatus_Name { get; set; }

        }
    }




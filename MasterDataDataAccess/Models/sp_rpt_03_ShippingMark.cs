using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_rpt_03_ShippingMark
    {
        [Key]
        public Guid RowIndex { get; set; }
        public string Warehouse { get; set; }
        public string Business_Unit { get; set; }
        public string DO_NO { get; set; }
        public string SO_NO { get; set; }
        public string Pallet { get; set; }
        public string Product { get; set; }
        public string Product_Name { get; set; }
        public decimal? Qty { get; set; }
        public string Unit { get; set; }
        public DateTime? EXP_Date { get; set; }
        public string Batch_Lot { get; set; }
        public string Shipto_Address { get; set; }
        public DateTime? Doc_date { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_rpt_15_Space_Utilization
    {
        [Key]
        public Guid Row_Index { get; set; }
        public string Warehouse_Type { get; set; }
        public string LocationType_Name { get; set; }
        public int? Location_Year { get; set; }
        public int? Count_Location { get; set; }
        public int? Location_Month { get; set; }        
        public int? D01 { get; set; }
        public int? D02 { get; set; }
        public int? D03 { get; set; }
        public int? D04 { get; set; }
        public int? D05 { get; set; }
        public int? D06 { get; set; }
        public int? D07 { get; set; }
        public int? D08 { get; set; }
        public int? D09 { get; set; }
        public int? D10 { get; set; }
        public int? D11 { get; set; }
        public int? D12 { get; set; }
        public int? D13 { get; set; }
        public int? D14 { get; set; }
        public int? D15 { get; set; }
        public int? D16 { get; set; }
        public int? D17 { get; set; }
        public int? D18 { get; set; }
        public int? D19 { get; set; }
        public int? D20 { get; set; }
        public int? D21 { get; set; }
        public int? D22 { get; set; }
        public int? D23 { get; set; }
        public int? D24 { get; set; }
        public int? D25 { get; set; }
        public int? D26 { get; set; }
        public int? D27 { get; set; }
        public int? D28 { get; set; }
        public int? D29 { get; set; }
        public int? D30 { get; set; }
        public int? D31 { get; set; }
        public int? IsCal { get; set; }

    }
}

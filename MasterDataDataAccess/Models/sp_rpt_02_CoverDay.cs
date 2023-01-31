using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_rpt_02_CoverDay
    {
        [Key]
        public Guid Row_Index { get; set; }
        public string Warehouse_Type { get; set; }
        public string BusinessUnit_Name { get; set; }
        public string Product_Lot { get; set; }
        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public decimal? Ratio { get; set; }
        public string UOM { get; set; }
        public decimal? StockOnHand { get; set; }
        public decimal? Cover_Qty { get; set; }
        public decimal? GI_AVG_Qty { get; set; }
        public decimal? Balance_AVG { get; set; }
        public int? SumCalDay { get; set; }
        public decimal? WHGI { get; set; }
        public decimal? D01 { get; set; }
        public decimal? D02 { get; set; }
        public decimal? D03 { get; set; }
        public decimal? D04 { get; set; }
        public decimal? D05 { get; set; }
        public decimal? D06 { get; set; }
        public decimal? D07 { get; set; }
        public decimal? D08 { get; set; }
        public decimal? D09 { get; set; }
        public decimal? D10 { get; set; }
        public decimal? D11 { get; set; }
        public decimal? D12 { get; set; }
        public decimal? D13 { get; set; }
        public decimal? D14 { get; set; }
        public decimal? D15 { get; set; }
        public decimal? D16 { get; set; }
        public decimal? D17 { get; set; }
        public decimal? D18 { get; set; }
        public decimal? D19 { get; set; }
        public decimal? D20 { get; set; }
        public decimal? D21 { get; set; }
        public decimal? D22 { get; set; }
        public decimal? D23 { get; set; }
        public decimal? D24 { get; set; }
        public decimal? D25 { get; set; }
        public decimal? D26 { get; set; }
        public decimal? D27 { get; set; }
        public decimal? D28 { get; set; }
        public decimal? D29 { get; set; }
        public decimal? D30 { get; set; }
        public decimal? D31 { get; set; }

    }
}

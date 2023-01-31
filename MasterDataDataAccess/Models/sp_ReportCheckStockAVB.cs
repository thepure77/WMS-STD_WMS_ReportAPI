using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_ReportCheckStockAVB
    {
        [Key]
        public long? RowIndex { get; set; }
        public string Product_Id { get; set; }
        public string CurrentDatetime   { get; set; }
        public DateTime? Last5Days            { get; set; }
        public string Product_Name         { get; set; }
        public decimal? BU_QtyOnHand       { get; set; }
        public decimal? BU_GIQty_5_Day     { get; set; }
        public decimal? Open_BU_Qty        { get; set; }
        public decimal? BU_Balance           { get; set; }
        public string BU_UNIT              { get; set; }
        public decimal? BU_Converage_Day     { get; set; }
        public decimal? SU_QtyOnHand       { get; set; }
        public decimal? SU_GIQty_5_Day     { get; set; }
        public decimal? Open_SU_Qty        { get; set; }
        public decimal? SU_Balance           { get; set; }
        public string SU_UNIT              { get; set; }
        public decimal? SU_Converage_Day     { get; set; }
    }
}

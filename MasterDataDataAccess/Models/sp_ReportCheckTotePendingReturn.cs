using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_ReportCheckTotePendingReturn
    {
        [Key]
        public string TruckLoad_No { get; set; }
        public DateTime? TruckLoad_Return_Date { get; set; }
        public string Return_Tote_MAX_XL { get; set; }
        public string Return_Tote_MAX_M { get; set; }
        public string DocReturn_Max { get; set; }
        public int? Return_Tote_Qty_XL { get; set; }
        public int? Return_Tote_Qty_M { get; set; }
        public int? Return_Tote_Qty_DMG_XL { get; set; }
        public int? Return_Tote_Qty_DMG_M { get; set; }
        public int? Return_Doc { get; set; }
    }
}

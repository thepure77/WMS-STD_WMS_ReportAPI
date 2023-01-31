using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GIDataAccess.Models
{
    public partial class TB_RPT_picking_Performance
    {
        [Key]
        public Guid Picking_Index { get; set; }
        public Guid GoodsIssue_Index { get; set; }
        public string GoodsIssue_No { get; set; }
        public DateTime GoodsIssue_Date { get; set; }
        public string TruckLoad_No { get; set; }
        public DateTime TruckLoad_Date { get; set; }
        public string Dock_Name { get; set; }
        public int? Rollcage_Use { get; set; }
        public string Chute_No { get; set; }
        public string Round_Name { get; set; }
        public DateTime? Start_Wave { get; set; }
        public DateTime? Closed_Wave { get; set; }
        public decimal? CBM { get; set; }
        public int? tag_ASRS { get; set; }
        public int? tag_LBL { get; set; }
        public int? tag_CFR_XL { get; set; }
        public int? tag_CFR_M { get; set; }
        public int? Total_tag { get; set; }
        public DateTime? Last_Scanin { get; set; }
        public DateTime? Last_Selecting { get; set; }
        public DateTime? Last_Inpection { get; set; }
        public int? Duration_ASRS { get; set; }
        public int? Duration_LBL { get; set; }
        public int? Duration_PP { get; set; }
        public int? Picking_Wave { get; set; }
    }
}

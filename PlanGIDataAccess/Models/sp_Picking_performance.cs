using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlanGIDataAccess.Models
{
    public class sp_Picking_performance
    {
        [Key]
        public Guid GoodsIssue_Index { get; set; }
        public string GoodsIssue_No { get; set; }
        public DateTime GoodsIssue_Date { get; set; }
        public string TruckLoad_No { get; set; }
        public DateTime TruckLoad_Date { get; set; }
        public string Dock_Name { get; set; }
        public int? Rollcage_Use { get; set; }
        public string Chute_No { get; set; }
        public string Round_Name { get; set; }
        public string Start_Wave { get; set; }
        public string Closed_Wave { get; set; }
        public decimal? CBM { get; set; }
        public int? tag_ASRS { get; set; }
        public int? tag_LBL { get; set; }
        public int? tag_CFR_XL { get; set; }
        public int? tag_CFR_M { get; set; }
        public int? Total_tag { get; set; }
        public string Last_Scanin { get; set; }
        public string Last_Selecting { get; set; }
        public string Last_Inpection { get; set; }
        public string Duration_ASRS { get; set; }
        public string Duration_LBL { get; set; }
        public string Duration_PP { get; set; }
        public string Picking_Wave { get; set; }
    }
}

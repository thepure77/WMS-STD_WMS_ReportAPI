using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportPickingPerformanceRecords
{
    public class ReportPickingPerformanceRecordsViewModel
    {
        //public Guid GoodsIssue_Index { get; set; }
        public string GoodsIssue_No { get; set; }
        public string GoodsIssue_Date { get; set; }
        public string GoodsIssue_Date_To { get; set; }
        public string TruckLoad_No { get; set; }
        public string TruckLoad_Date { get; set; }
        public string Round { get; set; }
        //public string Round_index { get; set; }
        public string Round_Id { get; set; }
        //@GI  = GI_index
        //@TL = truckload_No
        //@RND   = Round_index
        //@GI_DATE = date
        //@GI_DATE_TO = date_to
        public decimal? CBM { get; set; }
        public string Dock_Name { get; set; }
        public int? Rollcage_Use { get; set; }
        public string Chute_No { get; set; }
        public string Round_Name { get; set; }
        public string Start_Wave { get; set; }
        public string Closed_Wave { get; set; }
        public int? Tag_ASRS { get; set; }
        public int? Tag_LBL { get; set; }
        public int? Tag_CFR_XL { get; set; }
        public int? Tag_CFR_M { get; set; }
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

using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportPickingPerformance
{
    public class ReportPickingPerformanceViewModel
    {
        public Guid Row_Index { get; set; }
        public int Row_No { get; set; }
        public string Warehouse_Type { get; set; }
        public Guid? GoodsIssue_Index { get; set; }
        public string GoodsIssue_No { get; set; }
        public string GoodsIssue_Date { get; set; }
        public string GoodsIssue_Date_To { get; set; }
        public int CountTruckLoad_GI { get; set; }
        public string TruckLoad_No { get; set; }
        public DateTime? TruckLoad_Date { get; set; }
        public decimal CBM { get; set; }
        public decimal Sum_CBM { get; set; }
        public string Dock_Name { get; set; }
        public int CountAllRollcage_GI { get; set; }
        public int TruckLoadRollcage_Use { get; set; }
        public string Chute_No { get; set; }
        public string WaveRound { get; set; }
        public DateTime? WaveStart_Date { get; set; }
        public DateTime? WaveComplete_Date { get; set; }
        public int Tag_ASRS { get; set; }
        public int Tag_LBL { get; set; }
        public int Tag_CFR_XL { get; set; }
        public int Tag_CFR_M { get; set; }
        public int Total_Tag { get; set; }
        public int CountTagOut_GI { get; set; }
        public DateTime? LastCartonScanIn { get; set; }
        public DateTime? LastPickingSelective { get; set; }
        public DateTime? LastInspectionTote { get; set; }
        public string Hrs_ASRS { get; set; }
        public string Hrs_LBL { get; set; }
        public string Hrs_CFR { get; set; }
        public string Hrs_PickingWave { get; set; }

        public string BusinessUnit_Index { get; set; }
        public string BusinessUnit_Id { get; set; }
        public string BusinessUnit_Name { get; set; }
        public string TruckLoad_Index { get; set; }
        public string ambientRoom { get; set; }
        public string ambientRoom_name { get; set; }
        public int rowNum { get; set; }
        public BusinessUnitViewModel businessUnitList { get; set; }
    }
}

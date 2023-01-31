using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportCycleCount
{
    public class ReportCycleCountViewModel
    {
        public Guid cycleCount_Index { get; set; }
        public string cycleCount_No { get; set; }
        public string create_By { get; set; }
        public DateTime? create_Date { get; set; }
        public Guid locationType_Index { get; set; }
        public string locationType_Id { get; set; }
        public string locationType_Name { get; set; }
        public Guid location_Index { get; set; }
        public string location_Id { get; set; }
        public string location_Name { get; set; }
        public Guid tagItem_Index { get; set; }
        public Guid tag_Index { get; set; }
        public string tag_No { get; set; }
        public Guid product_Index { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public Guid owner_Index { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }
        public string eRP_Location { get; set; }
        public Guid itemStatus_Index { get; set; }
        public string itemStatus_Id { get; set; }
        public string itemStatus_Name { get; set; }
        public decimal? binBalance_QtyBal { get; set; }
        public Guid goodsReceive_ProductConversion_Index { get; set; }
        public string goodsReceive_ProductConversion_Id { get; set; }
        public string goodsReceive_ProductConversion_Name { get; set; }
        public decimal? sale_Unit { get; set; }
        public decimal? sALE_ProductConversion_Ratio { get; set; }
        public string sALE_ProductConversion_Name { get; set; }
        public decimal? qty_Count { get; set; }
        public decimal? qty_Diff { get; set; }
        public string status_Diff_Count { get; set; }
        public string status_Diff_Count_Check { get; set; }
        public string count_by { get; set; }
        public string count_Date { get; set; }
        public string product_Lot { get; set; }
        public string goodsReceive_MFG_Date { get; set; }
        public string goodsReceive_EXP_Date { get; set; }
        public string goodsReceive_Date { get; set; }
        public string goodsReceive_No { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public int? rowNum { get; set; }
        public string ambientRoom { get; set; }
        public string businessUnit_Name { get; set; }
        public BusinessUnitViewModel businessUnitList { get; set; }


    }
}

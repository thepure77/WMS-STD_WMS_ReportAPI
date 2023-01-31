using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportStockbyZoneReportAgeging
{
    public class ReportStockbyZoneReportAgegingViewModel
    {
        public Guid Row_Index { get; set; }

        public string TempCondition_Name { get; set; }

        public string BusinessUnit_Name { get; set; }

        public string Tag_No { get; set; }

        public string Location_Name { get; set; }

        public string Owner_Id { get; set; }

        public string Owner_Name { get; set; }

        public string Product_Id { get; set; }

        public string Product_Name { get; set; }

        public string ProductConversion_Name { get; set; }

        public string EXP_DATE { get; set; }

        public string WMS_Sloc { get; set; }

        public string SAP_Sloc { get; set; }

        public Guid? TempCondition_Index { get; set; }

        public Guid? BusinessUnit_Index { get; set; }

        public Guid? Owner_Index { get; set; }

        public Guid? Product_Index { get; set; }

        public string GoodsReceive_Date { get; set; }

        public string GoodsReceive_Date_To { get; set; }

        public string Product_Lot { get; set; }

        public string GoodsReceive_MFG_Date { get; set; }

        public string GoodsReceive_MFG_Date_To { get; set; }

        public string GoodsReceive_EXP_Date { get; set; }

        public string GoodsReceive_EXP_Date_To { get; set; }

        public int Row_No { get; set; }

        public string ambientRoom { get; set; }

        public string ambientRoom_name { get; set; }

        public BusinessUnitViewModel businessUnitList { get; set; }

    }


}

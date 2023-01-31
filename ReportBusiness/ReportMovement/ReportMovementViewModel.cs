using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportMovement
{
    public class ReportMovementViewModel
    {
        public int? rowNum { get; set; }
        public Guid row_Index { get; set; }
        public Int64 row_No { get; set; }
        public string warehouse_Type { get; set; }
        public Guid businessUnit_Index { get; set; }
        public string businessUnit_Name { get; set; }
        public Guid vendor_Index { get; set; }
        public string vendor_Id { get; set; }
        public string vendor_Name { get; set; }
        public Guid? product_Index { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public Guid productConversion_Index { get; set; }
        public string productConversion_Id { get; set; }
        public string productConversion_Name { get; set; }
        public decimal? binCard_QtyIn { get; set; }
        public decimal? binCard_QtyOut { get; set; }
        public string typeMovement { get; set; }
        public string goodsReceive_Date { get; set; }
        public string goodsReceive_Date_To { get; set; }
        public string goodsReceive_Date_Show { get; set; }
        public string goodsReceive_Date_To_Show { get; set; }
        public string update_Date { get; set; }
        public Guid tag_Index { get; set; }
        public string tag_No { get; set; }
        public string wMS_Sloc { get; set; }
        public string sAP_Sloc { get; set; }
        public string product_Lot { get; set; }
        public string goodsReceive_MFG_Date { get; set; }
        public string goodsReceive_MFG_Date_To { get; set; }
        public string goodsReceive_EXP_Date { get; set; }
        public string goodsReceive_EXP_Date_To { get; set; }
        public BusinessUnitViewModel businessUnitList { get; set; }
        public string showDate { get; set; }


    }
}

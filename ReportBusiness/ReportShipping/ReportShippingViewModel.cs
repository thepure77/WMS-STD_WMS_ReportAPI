using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportShipping
{
    public class ReportShippingViewModel
    {
        public int? rowNum { get; set; }
        public Guid row_Index { get; set; }
        public string tempCondition { get; set; }
        public string businessUnit_Name { get; set; }
        public string expect_Delivery_Date { get; set; }
        public string expect_Delivery_Date_To { get; set; }
        public string appointment_Date { get; set; }
        public string appointment_Date_To { get; set; }
        public string truckLoad_No { get; set; }
        public string appointment_Id { get; set; }
        public string appointment_Time { get; set; }
        public string dock_Name { get; set; }
        public string goodsIssue_No { get; set; }
        public string goodsIssue_Date { get; set; }
        public string goodsIssue_Date_To { get; set; }
        public string planGoodsIssue_No { get; set; }
        public string billing { get; set; }
        public string matdoc { get; set; }
        public string shipTo_Id { get; set; }
        public string shipTo_Name { get; set; }
        public string province { get; set; }
        public string branch { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public decimal? qty { get; set; }
        public string productConversion_Name { get; set; }
        public decimal? gI_Qty { get; set; }
        public decimal? ratio { get; set; }
        public string sU_Conversion { get; set; }
        public decimal? cBM { get; set; }
        public string productConversion_Name_P { get; set; }
        public string vehicleType_Name { get; set; }
        public string vehicle_No { get; set; }
        public string vehicleCompany_Name { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }
        public string shipment { get; set; }
        public string tag_no { get; set; }
        public int? countitem { get; set; }
        public BusinessUnitViewModel businessUnitList { get; set; }
        public string palletID { get; set; }

    }
}

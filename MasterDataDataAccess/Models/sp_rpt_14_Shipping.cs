using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace MasterDataDataAccess.Models
{
    public partial class sp_rpt_14_Shipping
    {
        [Key]
        public Guid Row_Index { get; set; }
        public string TempCondition_Name { get; set; }
        public string BusinessUnit_Name { get; set; }
        public DateTime? Expect_Delivery_Date { get; set; }
        public string Appointment_Time { get; set; }
        public string TruckLoad_No { get; set; }
        public string Appointment_Id { get; set; }
        public string Dock_Name { get; set; }
        public string GoodsIssue_No { get; set; }
        public DateTime? GoodsIssue_Date { get; set; }
        public string PlanGoodsIssue_No { get; set; }
        public string Billing { get; set; }
        public string Matdoc { get; set; }
        public string ShipTo_Id { get; set; }
        public string ShipTo_Name { get; set; }
        public string Province { get; set; }
        public string Branch { get; set; }
        //public string Product_Id { get; set; }
        //public string Product_Name { get; set; }
        //public decimal? Qty { get; set; }
        //public string ProductConversion_Name { get; set; }
        //public decimal? GI_Qty { get; set; }
        //public decimal? Ratio { get; set; }
        //public string SU_Conversion { get; set; }
        public decimal? CBM { get; set; }
        //public string ProductConversion_Name_P { get; set; }
        public string VehicleType_Name { get; set; }
        public string Vehicle_No { get; set; }
        public string VehicleCompany_Name { get; set; }
        public int? Countitem { get; set; }
        public DateTime? Appointment_Date { get; set; }
        public string PalletID { get; set; }
        

    }
}

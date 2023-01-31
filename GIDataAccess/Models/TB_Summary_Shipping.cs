using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GIDataAccess.Models
{
    public partial class TB_Summary_Shipping
    {
        [Key]
        public Guid Shipping_index { get; set; }
        public DateTime? Expect_Delivery_Date { get; set; }
        public DateTime? Appointment_Date { get; set; }
        public string Appointment_Time { get; set; }
        public string TruckLoad_No { get; set; }
        public string Appointment_Id { get; set; }
        public string Dock_Name { get; set; }
        public string GoodsIssue_No { get; set; }
        public DateTime? GoodsIssue_Date { get; set; }
        public string PlanGoodsIssue_No { get; set; }
        public string Bill_No { get; set; }
        public string Matdoc { get; set; }
        public string ShipTo_Id { get; set; }
        public string ShipTo_Name { get; set; }
        public string Province { get; set; }
        public string BranchCode { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public decimal? Order_Qty { get; set; }
        public string Order_UNIT { get; set; }
        public decimal? SU_Order_QTY { get; set; }
        public string SU_Unit { get; set; }
        public decimal? BU_Order_Qty { get; set; }
        public string BU_Unit { get; set; }
        public decimal? CBM { get; set; }
        public string Document_Remark { get; set; }
        public string DocumentRef_No3 { get; set; }
        public string VehicleCompany_Name { get; set; }
        public string VehicleType_Name { get; set; }
        public string Vehicle_Registration { get; set; }
    }
}

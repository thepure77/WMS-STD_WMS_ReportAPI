using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportSummaryInventoryPayment
{
    public class ReportSummaryInventoryPaymentViewModel
    {
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string PlanGoodsIssue_Date { get; set; }
        public string PlanGoodsIssue_Date_To { get; set; }
        //public DateTime? Create_Date_OMS { get; set; }

        //public string Create_Time_OMS { get; set; }

        //public string SO_No { get; set; }

        //public Guid? PlanGoodsIssue_Index { get; set; }

        //public string PlanGoodsIssue_No { get; set; }

        //public Guid? TruckLoad_Index { get; set; }

        //public string TruckLoad_No { get; set; }

        //public DateTime? Load_Date { get; set; }

        //public string Load_Time { get; set; }

        //public string GoodsIssue_No { get; set; }

        //public DateTime? Wave_Date { get; set; }

        //public string Wave_Time { get; set; }

        //public string ItemStatus_Name { get; set; }
        //public string Product_Id { get; set; }

        //public string Product_Name { get; set; }

        //public string Product_Lot { get; set; }

        //public decimal? Order_Qty { get; set; }

        //public string Order_Unit { get; set; }

        //public decimal? Pick_Qty { get; set; }

        //public string Pick_Unit { get; set; }

        //public int Short_ship { get; set; }

        //public string Short_Unit { get; set; }

        //public string Tote { get; set; }

        //public string Pick_location { get; set; }

        //public DateTime? GI_Date { get; set; }

        //public string GI_Time { get; set; }

        //public string Dock { get; set; }

        public string Billing_No { get; set; }

        //public string Matdoc { get; set; }

        //public string Zone { get; set; }

        //public string Sub_Zone { get; set; }

        //public string Province { get; set; }

        //public string Branch { get; set; }

        //public string ShipTo_Id { get; set; }
        //public string ShipTo_Address { get; set; }
        //public string Vehicle_Registration { get; set; }

        public string TruckLoad_No { get; set; }
        public string Appointment_Id { get; set; }
        public string Dock_Name { get; set; }
        public DateTime? Appointment_Date { get; set; }
        public string Appointment_Time { get; set; }
        public string PlanGoodsIssue_No { get; set; }
        //public DateTime? PlanGoodsIssue_Date { get; set; }
        public string ShipTo_Id { get; set; }
        public string ShipTo_Name { get; set; }
        public string ShipTo_Address { get; set; }
        public string Province { get; set; }
        public string BranchCode { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public decimal? Order_Qty { get; set; }
        public decimal? WHGI_QTY { get; set; }
        public decimal? TRGI_QTY { get; set; }
        public string Order_UNIT { get; set; }
        public string GoodsIssue_No { get; set; }
        public DateTime? GoodsIssue_Date { get; set; }
        public string Bill_No { get; set; }
        public string Matdoc { get; set; }
        public decimal? BU_Order_Qty { get; set; }
        public decimal? BU_WHGIQty { get; set; }
        public decimal? BU_TRGI_QTY { get; set; }
        public string BU_Unit { get; set; }
        public decimal? SU_Order_QTY { get; set; }
        public decimal? SU_WHGIQty { get; set; }
        public decimal? SU_TRGI_QTY { get; set; }
        public string SU_Unit { get; set; }
        public string Document_Remark { get; set; }
        public string DocumentRef_No3 { get; set; }
        public string BusinessUnit_Name { get; set; }
        public DateTime? EXP_date { get; set; }
        public DateTime? MFG_date { get; set; }
    }


}

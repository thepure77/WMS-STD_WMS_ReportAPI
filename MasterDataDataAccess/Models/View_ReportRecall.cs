using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataDataAccess.Models
{
    public class View_ReportRecall
    {
        [Key]
        public long? Row_Index { get; set; }
        public string PO_No { get; set; }
        public string Vendor_Id { get; set; }
        public string Vendor_Name { get; set; }
        public string GoodsReceive_No { get; set; }
        public DateTime? GoodsReceive_Date { get; set; }
        public string Tag_No { get; set; }
        public string Tag_Pick { get; set; }
        public DateTime? MFG_Date { get; set; }
        public DateTime? EXP_Date { get; set; }
        public string Product_Lot { get; set; }
        public string ERP_Location { get; set; }
        public string GoodsIssue_No { get; set; }
        public DateTime? GoodsIssue_Date { get; set; }
        public string PlanGoodsIssue_No { get; set; }
        public string TruckLoad_No { get; set; }
        public DateTime? truckLoad_Date { get; set; }
        public string Appointment_Time { get; set; }
        public string ref_document_No { get; set; }
        public string ShipTo_Id { get; set; }
        public string ShipTo_Name { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_Lot_GI { get; set; }
        public decimal? Sale_BUQty { get; set; }
        public string Sale_BUConversion { get; set; }
        public decimal? Sale_SUQty { get; set; }
        public string Sale_SUConversion { get; set; }
        public DateTime? Billing_Date { get; set; }


    }
}

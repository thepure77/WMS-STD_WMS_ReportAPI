using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public class View_ReportRecall_Inbound_Excel
    {
        [Key]
        public Guid RowIndex { get; set; }
        public string Tag_No { get; set; }
        public string GoodsReceive_No { get; set; }
        public string PO_No { get; set; }
        public DateTime? GoodsReceive_Date { get; set; }
        public string Vendor_Id { get; set; }
        public string Vendor_Name { get; set; }
        public string Product_Lot_GR { get; set; }
        public DateTime? MFG_Date { get; set; }
        public DateTime? EXP_Date { get; set; }
        public string GoodsIssue_No { get; set; }
        public string PlanGoodsIssue_No { get; set; }
        public string Tag_Pick { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_Lot_GI { get; set; }
        public string Order_BUConversion { get; set; }
        public decimal? Sale_BUQty { get; set; }
        public string Sale_BUConversion { get; set; }
        public decimal? Sale_SUQty { get; set; }
        public string Sale_SUConversion { get; set; }
        public string ERP_Location { get; set; }
        public string Billing_Matdoc_GR { get; set; }

        //รอเปลี่ยน type ข้อมูลหลัง update 
        public string No_ASN { get; set; }
        public decimal? GR_QTY { get; set; }
        public string GR_unit { get; set; }
        public string Match_Name { get; set; }
        public DateTime? Billing_Date { get; set; }
    }
}

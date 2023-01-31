using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace MasterDataDataAccess.Models
{
    public partial class sp_rpt_05_Pickinglist
    {
        [Key]
        public Guid RowIndex { get; set; }
        public string TempCondition { get; set; }
        public string Business_Unit { get; set; }
        public string DO_NO { get; set; }
        public string SO_NO { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public DateTime? Doc_date { get; set; }
        public string Shipto_Address { get; set; }
        public string Status { get; set; }
        public string Batch_Lot { get; set; }
        public string Vendor_ID { get; set; }
        public string Vendor_Name { get; set; }
        public decimal? Qty_Bal { get; set; }
        public decimal? Qty_Reserve { get; set; }
        public decimal? Qty_Amount { get; set; }
        public string SUB_UNIT { get; set; }
        public decimal? Sale_Qty { get; set; }
        public string Sale_Unit { get; set; }
        public decimal? Weight { get; set; }
        public decimal? NetWeight_KG { get; set; }
        public decimal? GrsWeight_KG { get; set; }
        public string Location_Type_Name { get; set; }
        public string ERP_Location { get; set; }
        public decimal? CBM_SU { get; set; }
        public decimal? CBM { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GRDataAccess.Models
{
    public class View_Report1_GR
    {
        [Key]
        public long? RowIndex { get; set; }
        public int? RowNum { get; set; }
        public Guid? GoodsReceiveItem_Index { get; set; }
        public string GoodsReceive_No { get; set; }
        public Guid? GoodsReceive_Index { get; set; }
        public DateTime? GoodsReceive_date { get; set; }
        public string Po_no { get; set; }
        public DateTime? Po_date { get; set; }
        public string PlanGoodsReceive_no { get; set; }
        public DateTime? PlanGoodsReceive_date { get; set; }
        public string product_id { get; set; }
        public string Product_Name { get; set; }
        public decimal? Qty { get; set; }
        public string ProductConversion_Name { get; set; }
        public string Product_lot { get; set; }
        public decimal? Qty_po { get; set; }
        public string Po_unit { get; set; }
        public decimal? Qty_asn { get; set; }
        public string Asn_unit { get; set; }
        public decimal? Qty_base_unit_gr { get; set; }
        public string Base_unit_gr { get; set; }
        public string ItemStatus_Name { get; set; }
        public DateTime? Mfg_date { get; set; }
        public DateTime? Exp_date { get; set; }
        public string Erp_location { get; set; }
        public string DocumentRef_No1 { get; set; }
        public Guid? DocumentType_Index { get; set; }
        public string DocumentType_name { get; set; }
        public string DocumentRef_no2 { get; set; }
        public int Document_status { get; set; }
        public string Processstatus_name { get; set; }
        public string Document_remark { get; set; }
        public string Create_By { get; set; }
    }
}

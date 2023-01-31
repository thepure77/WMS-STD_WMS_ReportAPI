using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_ReportCheckTransactionGR
    {
        [Key]
        public Guid RowIndex { get; set; }
        public string PO_No { get; set; }
        public string ASN_No              { get; set; }
        public DateTime? ASN_Date            { get; set; }
        public string ASN_Linenum         { get; set; }
        public string Product_Id          { get; set; }
        public string Product_Name        { get; set; }
        public decimal? Plan_Qty            { get; set; }
        public decimal? GR_Qty              { get; set; }
        public decimal? Pending_Qty         { get; set; }
        public string ASN_UNIT            { get; set; }
        public decimal? BU_ASN_QTY          { get; set; }
        public decimal? BU_GRQty            { get; set; }
        public string BU_Unit             { get; set; }
        public decimal? SU_ASN_QTY          { get; set; }
        public decimal? SU_GRQty            { get; set; }
        public string SU_Unit             { get; set; }
        public string GoodsReceive_No     { get; set; }
        public DateTime? GoodsReceive_Date   { get; set; }
        public string Matdoc              { get; set; }
        public string Remark { get; set; }
    }
}

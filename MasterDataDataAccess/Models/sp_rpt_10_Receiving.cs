using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_rpt_10_Receiving
    {



        //[Key]
        //public long   Row_Index                   { get; set; }
        //public int Row_No                      { get; set; }
        //public string Warehouse_Type              { get; set; }
        //public string BusinessUnit_Name           { get; set; }
        //public Guid? Vendor_Index                { get; set; }
        //public string Vendor_Id                   { get; set; }
        //public string Vendor_Name                 { get; set; }
        //public DateTime? PurchaseOrder_Due_Date   { get; set; }
        //public string GoodsReceive_No             { get; set; }
        //public DateTime? GoodsReceive_Date        { get; set; }
        //public string PurchaseOrder_No            { get; set; }
        //public DateTime? PurchaseOrder_Date       { get; set; }
        //public string PlanGoodsReceive_No         { get; set; }
        //public string Ref_Document_No             { get; set; }
        //public DateTime? PlanGoodsReceive_Date    { get; set; }
        //public string Product_Index               { get; set; }
        //public string Product_Id                  { get; set; }
        //public string Product_Name                { get; set; }
        //public string Product_Lot                 { get; set; }
        //public string PutAway_SumQty              { get; set; }
        //public string PutAway_SU                  { get; set; }
        //public string PO_SumQty                   { get; set; }
        //public string PO_SALE_SU                  { get; set; }
        //public string ASN_SumQty                  { get; set; }
        //public string ASN_SALE_SU                 { get; set; }
        //public string Remaining_ReceiveQty        { get; set; }
        //public string Remaining_Receive_SALE_SU   { get; set; }
        //public string ItemStatus_Name             { get; set; }
        //public DateTime? MFG_Date                 { get; set; }
        //public DateTime? EXP_Date                 { get; set; }
        //public string SAP_Sloc                    { get; set; }
        //public string WMS_Sloc                    { get; set; }
        //public string DocumentType_Index          { get; set; }
        //public string DocumentType_Name           { get; set; }
        //public string Document_Status             { get; set; }
        //public string Matdoc                      { get; set; }
        //public string DocumentRef_No2             { get; set; }
        //public string ProcessStatus_Name          { get; set; }
        //public string Document_Remark             { get; set; }

        //public string BusinessUnit_Index { get; set; }
        //public DateTime? GR_Date_From { get; set; }
        //public DateTime? GR_Date_To { get; set; }
        //public string GoodsReceive_Index { get; set; }
        //public string PurchaseOrder_Index { get; set; }
        //public string PlanGoodsReceive_Index { get; set; }

        [Key]
        public Guid? Row_Index { get; set; }
        public long Row_No { get; set; }
        public string Warehouse_Type { get; set; }
        public string BusinessUnit_Name { get; set; }
        public Guid? Vendor_Index { get; set; }
        public string Vendor_Id { get; set; }
        public string Vendor_Name { get; set; }
        public DateTime? PurchaseOrder_Due_Date { get; set; }
        public string GoodsReceive_No { get; set; }
        public DateTime? GoodsReceive_Date { get; set; }
        public string PurchaseOrder_No { get; set; }
        public DateTime? PurchaseOrder_Date { get; set; }
        public string PlanGoodsReceive_No { get; set; }
        public string Ref_Document_No { get; set; }
        public DateTime? PlanGoodsReceive_Date { get; set; }
        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_Lot { get; set; }
        public decimal PutAway_SumQty { get; set; }
        public string PutAway_SU { get; set; }
        public decimal PO_SumQty { get; set; }
        public string PO_SALE_SU { get; set; }
        public decimal ASN_SumQty { get; set; }
        public string ASN_SALE_SU { get; set; }
        //public string PutAway_SumQty { get; set; }
        public decimal Remaining_ReceiveQty { get; set; }
        public string Remaining_Receive_SALE_SU { get; set; }
        public string ItemStatus_Name { get; set; }
        public DateTime? MFG_Date { get; set; }
        public DateTime? EXP_Date { get; set; }
        public string SAP_Sloc { get; set; }
        public string WMS_Sloc { get; set; }
        public string Plant { get; set; }
        public DateTime? Putaway_Date { get; set; }
        public Guid? DocumentType_Index { get; set; }
        public string DocumentType_Name { get; set; }
        public int Document_Status { get; set; }
        public string Matdoc { get; set; }
        public DateTime? Matdoc_Date { get; set; }
        public string DocumentRef_No2 { get; set; }
        public string ProcessStatus_Name { get; set; }
        public string Document_Remark { get; set; }


        //public string BusinessUnit_Index { get; set; }

    }
}

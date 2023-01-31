using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportReceiving
{
    public class ReportReceivingPrintViewModel
    {
        //public Guid cycleCount_Index { get; set; }
        //public string cycleCount_No { get; set; }
        //public string create_By { get; set; }
        //public DateTime? create_Date { get; set; }
        //public Guid locationType_Index { get; set; }
        //public string locationType_Id { get; set; }
        //public string locationType_Name { get; set; }
        //public Guid location_Index { get; set; }
        //public string location_Id { get; set; }
        //public string location_Name { get; set; }
        //public Guid tagItem_Index { get; set; }
        //public Guid tag_Index { get; set; }
        //public string tag_No { get; set; }
        //public Guid product_Index { get; set; }
        //public string product_Id { get; set; }
        //public string product_Name { get; set; }
        //public Guid owner_Index { get; set; }
        //public string owner_Id { get; set; }
        //public string owner_Name { get; set; }
        //public string eRP_Location { get; set; }
        //public Guid itemStatus_Index { get; set; }
        //public string itemStatus_Id { get; set; }
        //public string itemStatus_Name { get; set; }
        //public decimal? binBalance_QtyBal { get; set; }
        //public Guid goodsReceive_ProductConversion_Index { get; set; }
        //public string goodsReceive_ProductConversion_Id { get; set; }
        //public string goodsReceive_ProductConversion_Name { get; set; }
        //public decimal? sale_Unit { get; set; }
        //public decimal? sALE_ProductConversion_Ratio { get; set; }
        //public string sALE_ProductConversion_Name { get; set; }
        //public decimal? qty_Count { get; set; }
        //public decimal? qty_Diff { get; set; }
        //public string status_Diff_Count { get; set; }
        //public string count_by { get; set; }
        //public DateTime? count_Date { get; set; }
        //public string product_Lot { get; set; }
        //public DateTime? goodsReceive_MFG_Date { get; set; }
        //public DateTime? goodsReceive_EXP_Date { get; set; }
        //public DateTime? goodsReceive_Date { get; set; }
        //public string goodsReceive_No { get; set; }
        //public string report_date_to { get; set; }
        //public string report_date { get; set; }
        //public int? rowNum { get; set; }
        //public string ambientRoom { get; set; }
        //public string businessUnit_Name { get; set; }




        /// <summary>
        /// ///////////////////////////////
        /// </summary>
        public long? row_Index { get; set; }
        public int? row_No { get; set; }
        public string warehouse_Type { get; set; }
        public string businessUnit_Name { get; set; }
        public Guid vendor_Index { get; set; }
        public string vendor_Id { get; set; }
        public string vendor_Name { get; set; }
        public string purchaseOrder_Due_Date { get; set; }
        public string purchaseOrder_Due_Time { get; set; }
        public string goodsReceive_No { get; set; }  
        public string goodsReceive_Date { get; set; }
        public string purchaseOrder_No { get; set; }
        public string purchaseOrder_Date { get; set; }
        public string planGoodsReceive_No { get; set; }
        public string ref_Document_No { get; set; }
        public string planGoodsReceive_Date { get; set; }
        public Guid product_Index { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string product_Lot { get; set; }
        public string putAway_SumQty { get; set; }
        public string putAway_SU { get; set; }
        public string pO_SumQty { get; set; }
        public string pO_SALE_SU { get; set; }
        public string aSN_SumQty { get; set; }
        public string aSN_SALE_SU { get; set; }
        public string remaining_ReceiveQty { get; set; }
        public string remaining_Receive_SALE_SU { get; set; }
        public string itemStatus_Name { get; set; }
        public string mFG_Date { get; set; }
        public string eXP_Date { get; set; }
        public string sAP_Sloc { get; set; }
        public string wMS_Sloc { get; set; }
        public string plant { get; set; }
        public string putaway_Date { get; set; }
        public string putaway_Time { get; set; }
        public Guid documentType_Index { get; set; }
        public string documentType_Name { get; set; }
        public string document_Status { get; set; }
        public string matdoc { get; set; }
        public string matdoc_Date { get; set; }
        public string matdoc_Time { get; set; }
        public string documentRef_No2 { get; set; }
        public string processStatus_Name { get; set; }
        public string document_Remark { get; set; }

        public Guid businessUnit_Index { get; set; }
        public Guid goodsReceive_Index { get; set; }
        public Guid purchaseOrder_Index { get; set; }
        public Guid planGoodsReceive_Index { get; set; }

        public string report_date { get; set; }
        public string report_date_to { get; set; }
        public string ambientRoom { get; set; }
    }
}

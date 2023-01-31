using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.Report8
{
    public class Report8ViewModelV2
    {
        //public int? rowNum { get; set; }
        public Guid? goodsTransfer_Index { get; set; }
        public string goodsTransfer_No { get; set; }
        public Guid? documentType_Index { get; set; }
        public string documentType_Id { get; set; }
        public string documentType_Name { get; set; }
        public string create_Date { get; set; }
        public string goodsTransfer_Date { get; set; }
        public string goodsTransfer_date { get; set; }
        public string goodsTransfer_date_To { get; set; }
        public string tag_No { get; set; }
        public Guid? product_Index { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string product_Lot { get; set; }
        public decimal? qty { get; set; }
        public decimal? sumQty { get; set; }
        public string unit { get; set; }
        public decimal? totalQty { get; set; }
        public decimal? sumTotalQty { get; set; }
        public string subUnit { get; set; }
        public string itemStatus_Name { get; set; }
        public string itemStatus_Name_To { get; set; }
        public Guid? location_Index { get; set; }
        public string location_Name { get; set; }
        public Guid? location_Index_To { get; set; }
        public string location_Name_To { get; set; }
        public string erp_Location { get; set; }
        public string erp_Location_To { get; set; }
        public int? document_Status { get; set; }
        public string documentStatus_Name { get; set; }
        public int? item_Document_Status { get; set; }
        public Guid? goodsReceiveItemLocation_Index { get; set; }
        public Guid goodsTransferItem_Index { get; set; }
        public string update_By { get; set; }
        public string create_By { get; set; }
        public Guid? locationType_Index { get; set; }
        public string locationType_Id{ get; set; }
        public string locationType_Name{ get; set; }
    }
}

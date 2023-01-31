using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportLaborIncentiveScheme
{
    public class ReportLaborIncentiveSchemeViewModel
    {
        public int? rowNum { get; set; }
        public string goodsTransfer_No { get; set; }
        public string documentType_Name { get; set; }
        public DateTime? create_Date { get; set; }
        public string create_By { get; set; }
        public string tag_No { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public decimal? qty { get; set; }
        public decimal? totalQty { get; set; }
        public string location_Name	 { get; set; }
        public string location_Name_To { get; set; }
        public string item_Status_Name { get; set; }
        public string item_Status_Name_To { get; set; }
        public string eRP_Location { get; set; }
        public string eRP_Location_To { get; set; }
        public int? document_Status { get; set; }
        public int? item_Document_Status { get; set; }
        public Guid? goodsReceiveItemLocation_Index { get; set; }
        public Guid? goodsTransferItem_Index { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }

    }
}

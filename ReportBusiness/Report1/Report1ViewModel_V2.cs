using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.Report1
{
    public class Report1ViewModel_V2
    {
        public Report1ViewModel_V2()
        {

            status = new List<statusViewModel>();

        }
        public Guid? goodsReceiveItem_Index { get; set; }
        public string goodsReceive_No { get; set; }
        public Guid? goodsReceive_Index { get; set; }
        public string goodsReceive_Date { get; set; }
        public string po_no { get; set; }
        public string po_date { get; set; }
        public string planGoodsReceive_no { get; set; }
        public string planGoodsReceive_date { get; set; }
        public string product_id { get; set; }
        public string product_Name { get; set; }
        public decimal? qty { get; set; }
        public string productConversion_Name { get; set; }
        public string product_lot { get; set; }
        public decimal? qty_po { get; set; }
        public string po_unit { get; set; }
        public decimal? qty_asn { get; set; }
        public string asn_unit { get; set; }
        public decimal? qty_base_unit_gr { get; set; }
        public string base_unit_gr { get; set; }
        public decimal? remainQty { get; set; }
        public string itemStatus_Name { get; set; }
        public string mfg_date { get; set; }
        public string exp_date { get; set; }
        public string erp_location { get; set; }
        public string documentRef_No1 { get; set; }
        public string documentType_name { get; set; }
        public string documentRef_no2 { get; set; }
        public int? document_status { get; set; }
        public string processstatus_name { get; set; }
        public string document_remark { get; set; }
        public string create_By { get; set; }
        public Guid? documentType_Index { get; set; }
        public string documentType_Id { get; set; }
        public string documentType_Name { get; set; }
        public int? rowNum { get; set; }

        public List<statusViewModel> status { get; set; }
        public string key { get; set; }
        public Guid? key_Index { get; set; }
        public string goodsReceive_date { get; set; }
        public string goodsReceive_date_to { get; set; }
        public string printDate  { get; set; }
        public class statusViewModel
        {
            public int value { get; set; }
            public string display { get; set; }
            public int seq { get; set; }
        }
    }
}

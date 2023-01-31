using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportRecall
{
    public class ReportRecallRequestModel
    {

        public string goodsReceive_date { get; set; }
        public string goodsReceive_date_To { get; set; }
        public bool advanceSearch { get; set; }
        public string sku { get; set; }
        public string sloc { get; set; }
        public string po_No { get; set; }
        public string planGoodsIssue_No { get; set; }
        public string batch_lot { get; set; }
        public string shipTo_ID { get; set; }
        public string billing_macdoc { get; set; }
        
        public string goodsReceive_No { get; set; }
        public string GoodsIssue_No { get; set; }
        public string truckLoad_No { get; set; }
        public string NoTag { get; set; }

        //Material No.
        public string materialNo { get; set; }

        // id / name supplier
        public string vendorId { get; set; }

        //DATE 
        public string goodsIssue_date { get; set; }
        public string goodsIssue_date_to { get; set; }
        public string date_exp { get; set; }
        public string date_exp_to { get; set; }
        public string date_mfg { get; set; }
        public string date_mfg_to { get; set; }
        public string date_load { get; set; }
        public string date_load_to { get; set; }
        public string date_GR { get; set; }
        public string date_GR_to { get; set; }

        public string date_do { get; set; }
        public string date_do_to { get; set; }

        //เลือกห้อง
        public string ambientRoom { get; set; }
        //public DateTime? goodsReceive_date { get; set; }
        //public DateTime? goodsReceive_date_To { get; set; }
        //public DateTime? date_GI { get; set; }
        //public DateTime? date_Exp { get; set; }
        //public DateTime? date_MFG { get; set; }
        //public DateTime? date_load { get; set; }
        //public DateTime? date_GR { get; set; }
        //public bool advanceSearch { get; set; }
        //public string sku { get; set; }
        //public string sloc { get; set; }
        //public string name { get; set; }
        //public string key { get; set; }
        //public string planGoodsIssue_No { get; set; }
        //public string batch_lot { get; set; }
        //public string shipTo_ID { get; set; }
        //public string billing_macdoc { get; set; }


    }
}

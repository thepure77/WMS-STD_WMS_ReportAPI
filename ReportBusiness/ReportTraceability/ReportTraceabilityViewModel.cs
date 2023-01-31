using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportTraceability
{
    public class ReportTraceabilityViewModel
    {
        public int? rowNum { get; set; }
        public string shipment_date { get; set; }
        public string business_unit { get; set; }
        public string product_Name { get; set; }
        public string product_Id { get; set; }
        //public string product_Index { get; set; }
        public int? po_Qty { get; set; }
        public int? inbound_SumQty { get; set; }
        public decimal? percent_GR { get; set; }
        public decimal? percent_GI { get; set; }       
        public int? outbound_SumQty { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public Guid? product_Index { get; set; }
        public BusinessUnitViewModel businessUnitList { get; set; }
        public string ambientRoom { get; set; }
        public string exp_Date { get; set; }
        public string mfg_Date { get; set; }
        public string goodsReceive_Date { get; set; }
        public string goodsIssue_Date { get; set; }
        public string product_Lot { get; set; }
        public Guid? vendor_Index { get; set; }
        public string vendor_Id { get; set; }
        public string vendor_Name { get; set; }
        public string goodsReceive_date_to { get; set; }
        public string goodsReceive_date_from { get; set; }
        public string month { get; set; }
        public string shipTo_Id { get; set; }
        public string branch_Code { get; set; }
        public string sale_Unit { get; set; }

    }
}

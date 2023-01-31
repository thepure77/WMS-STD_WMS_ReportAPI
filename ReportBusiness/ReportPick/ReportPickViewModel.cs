using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportPick
{
    public class ReportPickViewModel
    {
        public Guid rowIndex { get; set; }
        public string tempCondition { get; set; }
        public string business_Unit { get; set; }
        public string dO_NO { get; set; }
        public string sO_NO { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string doc_date { get; set; }
        public string shipto_Address { get; set; }
        public string status { get; set; }
        public string batch_Lot { get; set; }
        public string vendor_ID { get; set; }
        public string vendor_Name { get; set; }
        public decimal? qty_Bal { get; set; }
        public decimal? qty_Reserve { get; set; }
        public decimal? qty_Amount { get; set; }
        public string sUB_UNIT { get; set; }
        public decimal? sale_Qty { get; set; }
        public string sale_Unit { get; set; }
        public decimal? weight { get; set; }
        public decimal? netWeight_KG { get; set; }
        public decimal? grsWeight_KG { get; set; }
        public string location_Type_Name { get; set; }
        public string eRP_Location { get; set; }
        public decimal? cBM_SU { get; set; }
        public decimal? cBM { get; set; }
        public string report_date { get; set; }
        public string report_date_to { get; set; }
        public int? rowNum { get; set; }
        public BusinessUnitViewModel businessUnitList { get; set; }

    }
}

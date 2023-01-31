using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportShippingMark
{
    public class ReportShippingMarkViewModel
    {
        public int? rowNum { get; set; }
        public Guid rowIndex { get; set; }
        public string warehouse { get; set; }
        public string business_Unit { get; set; }
        public string dO_NO { get; set; }
        public string sO_NO { get; set; }
        public string pallet { get; set; }
        public string product { get; set; }
        public string product_Name { get; set; }
        public decimal? qty { get; set; }
        public string unit { get; set; }
        public string eXP_Date { get; set; }
        public string batch_Lot { get; set; }
        public string shipto_Address { get; set; }
        public string doc_date { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }
        public BusinessUnitViewModel businessUnitList { get; set; }


    }
}

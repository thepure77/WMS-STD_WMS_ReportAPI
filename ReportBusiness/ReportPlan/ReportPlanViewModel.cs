using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportPlan
{
    public class ReportPlanViewModel
    {
        public int? rowNum { get; set; }
        public Guid rowIndex { get; set; }
        public string tempCondition { get; set; }
        public string business_Unit { get; set; }
        public string dO_NO { get; set; }
        public string sO_NO { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string doc_date { get; set; }
        public string shipto_Address { get; set; }
        public string ship_to { get; set; }
        public string tote { get; set; }
        public decimal? total_qty { get; set; }
        public string unit_BU { get; set; }
        public decimal? qty { get; set; }
        public string unit_SU { get; set; }
        public decimal? bu_Qty { get; set; }
        public string bU_SU { get; set; }
        public decimal? wEIGHT_PC_KG { get; set; }
        public decimal? netWeight_KG { get; set; }
        public decimal? grsWeight_KG { get; set; }
        public decimal? cBM_SU { get; set; }
        public decimal? cBM { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }
        public decimal? productConversion_Width { get; set; }
        public decimal? productConversion_Length { get; set; }
        public decimal? productConversion_Height { get; set; }
        public string ref_No1 { get; set; }
        public string ref_No2 { get; set; }
        public BusinessUnitViewModel businessUnitList { get; set; }

    }
}

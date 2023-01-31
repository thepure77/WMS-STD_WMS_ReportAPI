using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.CheckBalanceAllLocation
{
    public class CheckBalanceAllLocationViewModel
    {
        public int? rowNo { get; set; }
        public string location_Name { get; set; }
        public string location_No { get; set; }
        public string locationType_Name { get; set; }
        public string tag_No { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public decimal? binBalance_BU { get; set; }
        public string productConversion_Name_BU { get; set; }
        public decimal? binBalance_SU { get; set; }
        public string productConversion_Name_SU { get; set; }
        public string create_By { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }
    }
}

using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportArticleNotMovement
{
   public class ReportArticleNotMovementViewModel
    {
        public string ambientRoom { get; set; }
        public string product_Id { get; set; }
        public string tag_No { get; set; }
        public string quantity { get; set; }
        public string report_date { get; set; }
        public string report_date_to { get; set; }
        public string mfg_Date_From { get; set; }
        public string mfg_Date_To { get; set; }
        public string exp_Date_From { get; set; }
        public string exp_Date_To { get; set; }
        public string date_Now_From { get; set; }
        public string date_Now_To { get; set; }
        public LocationTypeViewModel locationTypeList { get; set; }
        public BusinessUnitViewModel businessUnitList { get; set; }
        public string batch { get; set; }
        
    }
}

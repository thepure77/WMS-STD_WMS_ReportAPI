using BinbalanceBusiness;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportWrongLocation
{
    public class ReportWrongLocationModel : Pagination
    {
        public string key { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string location_Name { get; set; }
        public string tag_No { get; set; }
        public string create_By { get; set; }
        public string create_Date { get; set; }
    }
    public class ReportWrongLocationModelAct
    {
        public IList<ReportWrongLocationModel> itemsview { get; set; }
        public Pagination pagination { get; set; }
    }
}

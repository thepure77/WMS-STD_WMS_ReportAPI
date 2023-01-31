using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.Report10
{
    public class Report10ViewModel
    {

        public string date { get; set; }

        public string warehouse_Name { get; set; }

        public string Zone_Id { get; set; }

        public string Zone_name { get; set; }
        
        public decimal? count { get; set; }

        public decimal? number { get; set; }

        public decimal? countAll { get; set; }

        public decimal? countUse { get; set; }

        public decimal? countEmpty { get; set; }

        public decimal? percenAll { get; set; }

        public decimal? percenUse { get; set; }

        public decimal? percenEmpty { get; set; }

        public bool checkQuery { get; set; }

        public string Location_Aisle { get; set; }
        public int? Location_Level { get; set; }
        public string Location_Prefix { get; set; }

        public string key { get; set; }
        public string name { get; set; }
        public string locationLock_Id { get; set; }
        public string location_Level { get; set; }
        public string location_Prefix_Desc { get; set; }
        public string zone_Id { get; set; }

        public string zone_name { get; set; }
    }


}

using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.Report13
{
    public class Report13ViewModel
    {
        
        public string product_Index { get; set; }
               
        public string product_Id { get; set; }

        public string product_Name { get; set; }

        public decimal? qty_Count { get; set; }

        public decimal? qty_Bal { get; set; }

        public string cycleCount_Date { get; set; }
        
        public string cycleCount_date { get; set; }

        public string cycleCount_date_To { get; set; }

        public decimal? date_Percen { get; set; }

        public string timeNow { get; set; }

        public bool checkQuery { get; set; }
    }


}

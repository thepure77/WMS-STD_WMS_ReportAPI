using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportSpaceUtilization
{
    public class ReportSpaceUtilizationViewModel
    {
        public string Row_Num { get; set; }

        public string LocationType_Name { get; set; }
        public int? Count_location { get; set; }
        public int? Count_IsUse { get; set; }
        public int? Count_Empty { get; set; }
        public int? Count_Block { get; set; }
        public decimal? Per_IsUser { get; set; }
        public decimal? Per_Empty { get; set; }
        public decimal? Per_Block { get; set; }

        public string Current_Date { get; set; }
        public string Current_Time { get; set; }
    }
}

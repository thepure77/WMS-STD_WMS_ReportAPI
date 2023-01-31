using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.CheckPickTime
{
    public class CheckPickTimeViewModel
    {
        public int rowNo { get; set; }
        public string goodsIssue_No { get; set; }
        public string goodsIssue_Date { get; set; }
        public string documentRef_No1 { get; set; }
        public string pick_type { get; set; }
        public string min_Date { get; set; }
        public TimeSpan? min_Time { get; set; }
        public string max_Date { get; set; }
        public TimeSpan? max_Time { get; set; }
        public string report_date_to { get; set; }
        public string report_date { get; set; }
        public string ambientRoom { get; set; }
    }
}

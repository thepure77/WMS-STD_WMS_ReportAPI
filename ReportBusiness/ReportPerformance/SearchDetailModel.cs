using BinbalanceBusiness;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportPerformance
{
    public class SearchDetailModel : Pagination
    {

        public string wo_Date { get; set; }

        public string wo_Date_To { get; set; }

        public string firstName { get; set; }

        public class actionResultViewModel
        {
            public IList<SearchDetailModel> itemsWO { get; set; }
            public Pagination pagination { get; set; }

            public Pagination pagination_Tab1 { get; set; }
            public Pagination pagination_Tab2 { get; set; }
            public int? total_Tab1 { get; set; }
            public int? total_Tab2 { get; set; }

            public int? countAll { get; set; }
        }

    }
}


using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.Report15
{
    public class Report15ViewModel
    {
        public Guid? product_Index { get; set; }

        public string product_Id { get; set; }
      
        public string product_Name { get; set; }

        public string productConversion_Name { get; set; }

        public decimal? binCard_QtyUU { get; set; }

        public decimal? binCard_QtyQI { get; set; }

        public decimal? sumBinCard_Qty { get; set; }

        public string binCard_date { get; set; }

        //public DateTime? binCard_date { get; set; }

        public string binCard_Date { get; set; }

        public bool checkQuery { get; set; }

        public string key { get; set; }

        public string name { get; set; }

        public string owner_Id { get; set; }

        public string productCategory_Id { get; set; }

    }


}

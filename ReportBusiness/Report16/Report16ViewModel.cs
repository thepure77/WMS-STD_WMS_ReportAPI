using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.Report16
{
    public class Report16ViewModel
    {
        public Guid? binCard_Index { get; set; }

        public string product_Id { get; set; }
      
        public string product_Name { get; set; }

        public string productConversion_Name { get; set; }

        public decimal? binCard_QtyIn { get; set; }

        public decimal? binCard_QtyOut { get; set; }

        public string binCard_Date { get; set; }

        public string binCard_MaxDate { get; set; }

        public string binCard_date { get; set; }

        public string binCard_date_To { get; set; }

        public bool checkQuery { get; set; }

        public Guid? Owner_Index { get; set; }
        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }
        public string MC { get; set; }
        public string Size { get; set; }
        public string binCard_DateIn { get; set; }
        public string binCard_DateOut { get; set; }

        public string movement_Type { get; set; }


    }


}

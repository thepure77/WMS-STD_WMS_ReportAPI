using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.Report18
{
    public class Report18ViewModel
    {
        public Guid? binCard_Index { get; set; }

        public string product_Id { get; set; }
      
        public string product_Name { get; set; }

        public string productConversion_Name { get; set; }

        public decimal? binCard_QtyIn { get; set; }

        public decimal? binCard_QtyOut { get; set; }

        public Guid? costCenter_Index { get; set; }
        public string costCenter_Id { get; set; }
        public string costCenter_Name { get; set; }

        public string user_Assign { get; set; }
        public string create_Date { get; set; }

        public string create_By { get; set; }

        public string binCard_Date { get; set; }

        public string binCard_date { get; set; }
         
        public string binCard_date_To { get; set; }

        public bool checkQuery { get; set; }

        public string owner_Id { get; set; }
        public string owner_Name { get; set; }
        public string productCategory_Id { get; set; }

        public decimal? percentage_BinCard_QtyIn { get; set; }

        public decimal? percentage_BinCard_QtyOut { get; set; }
        public Guid? owner_Index { get; set; }


        public Guid? productCategory_Index { get; set; }


        public string productCategory_Name { get; set; }
    }


}

using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.Report17
{
    public class Report17ViewModel
    {
        public Guid? binCard_Index { get; set; }

        public Guid? ref_Document_Index { get; set; }

        public Guid? ref_DocumentItem_Index { get; set; }

        public string product_Id { get; set; }
      
        public string product_Name { get; set; }

        public string productConversion_Name { get; set; }

        public decimal? binCard_QtyIn { get; set; }

        public decimal? binCard_QtyOut { get; set; }

        public string binCard_Date { get; set; }

        public string vendor_Name { get; set; }

        public string costcenter_Name { get; set; }

        public string soldTo_Name { get; set; }

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

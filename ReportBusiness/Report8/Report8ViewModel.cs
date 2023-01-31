using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ReportBusiness.Report8
{
   public class Report8ViewModel
    {
        public Guid? goodsTransfer_Index { get; set; }
        public string goodsTransfer_No { get; set; }
        public string tag_No { get; set; }
        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }
        public string location_Id { get; set; }
        public string location_Name { get; set; }
        public string location_Id_To { get; set; }
        public string location_Name_To { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public decimal? binbalance_Qty { get; set; }
        public decimal? qty { get; set; }
        public string productConversion_Id { get; set; }
        public string productConversion_Name { get; set; }
        public string goodsTransfer_Date { get; set; }
        public string goodsReceive_Date { get; set; }
        public string Update_By { get; set; }
        public string create_Date { get; set; }
        public string goodsTransfer_date { get; set; }
        public string goodsTransfer_date_To { get; set; }
        public string key { get; set; }
        public string name { get; set; }

    }
}

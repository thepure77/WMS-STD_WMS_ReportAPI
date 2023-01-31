using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.Report14
{
    public class Report14ViewModel
    {
            
        public DateTime? goodsReceive_Date { get; set; }

        public string goodsReceive_date { get; set; }

        public string goodsReceive_date_To { get; set; }

        public string product_Id { get; set; }

        public string product_Name { get; set; }

        //public Guid? productType_Index { get; set; }

        //public string productType_Id { get; set; }

        //public string productType_Name { get; set; }
                
        public string productConversion_Name { get; set; }

        public decimal? binBalance_QtyReserve { get; set; }

        public string binBalance_QtyReserveRound { get; set; }

        public decimal? binBalance_QtyBal_UU { get; set; }

        public string binBalance_QtyBal_UURound { get; set; }

        public decimal? binBalance_QtyBal_QI { get; set; }

        public string binBalance_QtyBal_QIRound { get; set; }

        public decimal? sumUUandQI { get; set; }

        public string sumUUandQIRound { get; set; }

        public decimal? stockRO { get; set; }

        public string stockRORound { get; set; }

        public decimal? percentageStock { get; set; }

        public string percentageStock2 { get; set; }

        public string dateToday { get; set; }

        public bool checkQuery { get; set; }

    }


}

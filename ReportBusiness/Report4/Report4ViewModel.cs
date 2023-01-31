using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ReportBusiness.Report4
{
   public class Report4ViewModel
    {
       
        public long? row_Index { get; set; }
        
        public Guid? goodsReceive_Index { get; set; }
        
        public string goodsReceive_No { get; set; }
     
        public string goodsReceive_Date { get; set; }

        public string goodsReceive_date { get; set; }

        public string goodsReceive_date_To { get; set; }

        public string product_Id { get; set; }
        
        public string product_Name { get; set; }
       
        public decimal? qty { get; set; }
       
        public string tag_No { get; set; }

        public Guid? putawayLocation_Index { get; set; }

        public string putawayLocation_Name { get; set; }

        public string putawayLocation_Id { get; set; }

        public string itemStatus_Id { get; set; }
        
        public string itemStatus_Name { get; set; }

        public string create_Date { get; set; }

        public string suggest_Location_Id { get; set; }

        public string operation { get; set; }

        public bool checkQuery { get; set; }

        //public string dateStart { get; set; }

        //public string dateEnd { get; set; }
    }
}

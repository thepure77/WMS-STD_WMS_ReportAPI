using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.Report20
{
    public class Report20ViewModel
    {
        public Guid? product_Index { get; set; }

        public string product_Id { get; set; }

        public string product_Name { get; set; }

        public string product_SecondName { get; set; }

        public string product_ThirdName { get; set; }

        public Guid? productConversion_Index { get; set; }

        public string productConversion_Id { get; set; }

        public string productConversion_Name { get; set; }

        public string Last_Date { get; set; }

        public string binCard_date { get; set; }

        public string binCard_date_To { get; set; }

        public int? LastMove_Day { get; set; }

        public bool checkQuery { get; set; }
    }


}

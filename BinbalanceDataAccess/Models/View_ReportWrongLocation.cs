using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BinBalanceDataAccess.Models
{
    public class View_ReportWrongLocation
    {
        [Key]
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Location_Name { get; set; }
        public string Tag_No { get; set; }
        public string Create_By { get; set; }
        public DateTime? Create_Date { get; set; }

    }
}

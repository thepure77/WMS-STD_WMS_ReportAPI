using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public class sp_ReportGI_SN
    {
        [Key]
        public Guid RowIndex { get; set; }
        public string Tag_No { get; set; }
        public string GoodsIssue_No { get; set; }
        public string Wave_Round { get; set; }
        public DateTime? GoodsIssue_Date { get; set; }
        public string DO_No { get; set; }
        public string ShipTo_Id { get; set; }
        public string ShipTo_Name { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Serial { get; set; }
        
    }
}

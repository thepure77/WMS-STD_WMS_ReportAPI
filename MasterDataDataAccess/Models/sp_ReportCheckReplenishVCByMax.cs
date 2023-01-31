using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_ReportCheckReplenishVCByMax
    {
        [Key]
        public long? RowIndex { get; set; }
        public string SKU { get; set; }
        public string MatDescription { get; set; }
        public string ProductConversion_Name { get; set; }
        public decimal? ProductConversion_Ratio { get; set; }
        public string MaxQty { get; set; }
        public decimal? PP_SuQty { get; set; }
        public decimal? BB_SuQty { get; set; }
        public decimal? ReplenQty { get; set; }
        public decimal? PP_QtyBal { get; set; }
        public decimal? BB_QtyBal { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GRDataAccess.Models
{
    public partial class View_RPT_GRV2
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid? Ref_DocumentItem_Index { get; set; }
        public Guid? Ref_Document_Index { get; set; }
        public DateTime? MIN_GoodsReceive_Date { get; set; }
        public DateTime? MAX_GoodsReceive_Date { get; set; }
        public decimal? GR_TotalQty { get; set; }

    }
}

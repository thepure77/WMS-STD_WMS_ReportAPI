using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GRDataAccess.Models
{
    public partial class View_RPT5_GRI
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid? GoodsReceiveItem_Index { get; set; }
        public Guid? Ref_Document_Index { get; set; }
    }
}

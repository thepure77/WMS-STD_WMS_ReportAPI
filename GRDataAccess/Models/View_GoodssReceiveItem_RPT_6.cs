using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GRDataAccess.Models
{


    public partial class View_GoodssReceiveItem_RPT_6
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid? Ref_Document_Index { get; set; }
        public Guid? GoodsReceiveItem_Index { get; set; }

      


    }
}

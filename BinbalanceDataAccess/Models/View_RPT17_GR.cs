using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BinBalanceDataAccess.Models
{


    public partial class View_RPT17_GR
    {
        [Key]
        public Guid? GoodsReceive_Index { get; set; }

        public Guid? Ref_DocumentItem_Index { get; set; }

        public string vendor_Name { get; set; }

        public string CostCenter_Name { get; set; }

        public string SoldTo_NAme { get; set; }




    }
}

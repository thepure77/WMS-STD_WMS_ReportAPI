using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GRDataAccess.Models
{


    public partial class View_RPT_GRTI
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid? GoodsReceive_Index { get; set; }

        public Guid? GoodsReceiveItem_Index { get; set; }

        public Guid? GoodsReceiveItemLocation_Index { get; set; }
        public string GoodsReceive_No { get; set; }

        public DateTime? GoodsReceive_Date { get; set; }
       
        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }

        public decimal Qty { get; set; }
        public Guid? ItemStatus_Index { get; set; }
        public string ItemStatus_Id { get; set; }
        public string ItemStatus_Name { get; set; }

        public Guid? TagItem_Index { get; set; }
        public Guid? Tag_Index { get; set; }
        public string Tag_No { get; set; }

        public Guid? PutawayLocation_Index { get; set; }
        public string PutawayLocation_Id { get; set; }
        public string PutawayLocation_Name { get; set; }

        public Guid? Suggest_Location_Index { get; set; }
        public string Suggest_Location_Id { get; set; }
        public string Suggest_Location_Name { get; set; }


    }
}

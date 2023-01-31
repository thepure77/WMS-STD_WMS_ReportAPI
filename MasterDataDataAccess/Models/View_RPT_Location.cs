using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_RPT_Location
    {
        [Key]
        public long? RowIndex { get; set; }
        public string Warehouse_Name { get; set; }

        public string Room_Name { get; set; }

        public string Zone_Id { get; set; }

        public string Zone_Name { get; set; }


        public string Location_Name { get; set; }


        public string LocationType_Name { get; set; }


        public string Location_Aisle { get; set; }

        public int? Location_Bay { get; set; }

        public int? Location_Level { get; set; }

        public string Location_Position { get; set; }

        public decimal Location_Height { get; set; }

        public string Location_Prefix { get; set; }

        public decimal? Max_Qty { get; set; }

        public decimal? Max_Weight { get; set; }

        public decimal? Max_Volume { get; set; }

        public decimal? Max_Pallet { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public Guid Location_Index { get; set; }

        public string Location_Id { get; set; }

        public Guid? LocationType_Index { get; set; }

        public string LocationType_Id { get; set; }

        public Guid Warehouse_Index { get; set; }

        public string Warehouse_Id { get; set; }

        public Guid Room_Index { get; set; }
        public string Room_Id { get; set; }

        public Guid LocationAisle_Index { get; set; }

        public string LocationLock_Id { get; set; }

        public int? BlockPut { get; set;}
        public int? BlockPick { get; set; }
    }
}

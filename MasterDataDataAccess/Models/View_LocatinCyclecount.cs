using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_LocatinCyclecount
    {
        [Key]
        public long? RowIndex { get; set; }

        public Guid? Location_Index { get; set; }

        public string Location_Id { get; set; }

        public string Location_Name { get; set; }

        public Guid? Zone_Index { get; set; }

        public string Zone_Id { get; set; }

        public string Zone_Name { get; set; }

        public Guid? LocationType_Index { get; set; }

        public string LocationType_Id { get; set; }

        public string LocationType_Name { get; set; }

        //public Guid? Warehouse_Index { get; set; }

        //public string Warehouse_Id { get; set; }
        //public string Warehouse_Name { get; set; }
    }
}

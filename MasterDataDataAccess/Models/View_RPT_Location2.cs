using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_RPT_Location2
    {
        [Key]
        public long? RowIndex { get; set; }
        public string Warehouse_Name { get; set; }

        public string Room_Name { get; set; }

        public string Zone_Id { get; set; }

        public string Zone_Name { get; set; }

        public string Yn { get; set; }

        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        public string Location_Name { get; set; }


        public string LocationType_Name { get; set; }
       
        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public Guid Location_Index { get; set; }

        public string Location_Id { get; set; }

        public Guid? LocationType_Index { get; set; }

        public string LocationType_Id { get; set; }

        public Guid Warehouse_Index { get; set; }

        public string Warehouse_Id { get; set; }

        //public Guid Room_Index { get; set; }
        //public string Room_Id { get; set; }

        //public Guid LocationAisle_Index { get; set; }

        //public string LocationLock_Id { get; set; }

        public string BlockPut { get; set;}
        public string BlockPick { get; set; }
    }
}

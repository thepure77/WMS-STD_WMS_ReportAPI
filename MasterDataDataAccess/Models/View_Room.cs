using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_Room
    {
        [Key]
        public Guid Room_Index { get; set; }
        public string Room_Id { get; set; }
        public string Room_Name { get; set; }

        public Guid Warehouse_Index { get; set; }
        public string Warehouse_Id { get; set; }
        public string Warehouse_Name { get; set; }

        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }

    }
}

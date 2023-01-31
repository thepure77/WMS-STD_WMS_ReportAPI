using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_OwnerSoldTo
    {
        [Key]
        public Guid OwnerSoldTo_Index { get; set; }
        public string OwnerSoldTo_Id { get; set; }
         
        public Guid? Owner_Index { get; set; }
        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }

        public Guid? SoldTo_Index { get; set; }
        public string SoldTo_Id { get; set; }
        public string SoldTo_Name { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

    }
}

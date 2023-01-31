using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataDataAccess.Models
{


    public partial class View_ProductOwner
    {
        [Key]
        public Guid ProductOwner_Index { get; set; }
        public string ProductOwner_Id { get; set; }

        public Guid? Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }

        public Guid? Owner_Index { get; set; }
        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

    }
}

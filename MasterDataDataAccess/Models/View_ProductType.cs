using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_ProductType
    {
        [Key]
        public Guid ProductType_Index { get; set; }
        public string ProductType_Id { get; set; }
        public string ProductType_Name { get; set; }

        public Guid ProductCategory_Index { get; set; }
        public string ProductCategory_Id { get; set; }
        public string ProductCategory_Name { get; set; }

        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }

    }
}

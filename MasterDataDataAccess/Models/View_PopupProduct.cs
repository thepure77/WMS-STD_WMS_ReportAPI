using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_PopupProduct
    {
        [Key]
        public Guid Product_Index { get; set; }

        public string Product_Id { get; set; }

        public string Product_SecondName { get; set; }

        public string Product_ThirdName { get; set; }

        public Guid Owner_Index { get; set; }

        public string Owner_Id { get; set; }

        public string Owner_Name { get; set; }

      
    }
}

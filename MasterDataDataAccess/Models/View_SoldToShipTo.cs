using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_SoldToShipTo
    {
        [Key]
        public Guid SoldToShipTo_Index { get; set; }
        public string SoldToShipTo_Id { get; set; }
 

        public Guid SoldTo_Index { get; set; }
        public string SoldTo_Id { get; set; }
        public string SoldTo_Name { get; set; }

        public Guid ShipTo_Index { get; set; }
        public string ShipTo_Id { get; set; }
        public string ShipTo_Name { get; set; }

        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }
      

    }
}

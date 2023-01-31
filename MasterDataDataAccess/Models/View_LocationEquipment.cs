using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_LocationEquipment
    {
        [Key]
        public Guid? LocationEquipment_Index { get; set; }
        public string LocationEquipment_Id { get; set; }
 

        public Guid? Location_Index { get; set; }
        public string Location_Id { get; set; }
        public string Location_Name { get; set; }

        public Guid? Equipment_Index { get; set; }
        public string Equipment_Id { get; set; }
        public string Equipment_Name { get; set; }

        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }
      

    }
}

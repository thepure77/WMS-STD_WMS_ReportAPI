using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_EquipmentSubType
    {
        [Key]
        public Guid EquipmentSubType_Index { get; set; }
        public string EquipmentSubType_Id { get; set; }
        public string EquipmentSubType_Name { get; set; }
        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }

        public Guid EquipmentType_Index { get; set; }
        public string EquipmentType_Id { get; set; }
        public string EquipmentType_Name { get; set; }

    }
}

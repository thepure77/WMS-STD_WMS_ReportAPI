using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_TaskGroupEquipment
    {
        [Key]

        public Guid TaskGroupEquipment_Index { get; set; }
        public string TaskGroupEquipment_Id { get; set; }
        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }

        public Guid TaskGroup_Index { get; set; }
        public string TaskGroup_Id { get; set; }
        public string TaskGroup_Name { get; set; }
 

        public Guid Equipment_Index { get; set; }
        public string Equipment_Id { get; set; }
        public string Equipment_Name { get; set; }

    }
}

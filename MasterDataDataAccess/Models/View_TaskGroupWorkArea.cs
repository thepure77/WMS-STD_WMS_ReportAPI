using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_TaskGroupWorkArea
    {
        [Key]

        public Guid TaskGroupWorkArea_Index { get; set; }
        public string TaskGroupWorkArea_Id { get; set; }
        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }

        public Guid TaskGroup_Index { get; set; }
        public string TaskGroup_Id { get; set; }
        public string TaskGroup_Name { get; set; }
 

        public Guid WorkArea_Index { get; set; }
        public string WorkArea_Id { get; set; }
        public string WorkArea_Name { get; set; }

    }
}

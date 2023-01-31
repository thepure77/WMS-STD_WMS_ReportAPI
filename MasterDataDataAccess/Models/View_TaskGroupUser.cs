using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_TaskGroupUser
    {
        [Key]
        public Guid TaskGroupUser_Index { get; set; }
        public string TaskGroupUser_Id { get; set; }
        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }

        public Guid TaskGroup_Index { get; set; }
        public string TaskGroup_Id { get; set; }
        public string TaskGroup_Name { get; set; }
 

        public Guid User_Index { get; set; }
        public string User_Id { get; set; }
        public string User_Name { get; set; }

    }
}

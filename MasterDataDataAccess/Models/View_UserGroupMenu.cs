using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_UserGroupMenu
    {
        [Key]
        public Guid UserGroupMenu_Index { get; set; }
        public string UserGroupMenu_Id { get; set; }

        public Guid UserGroup_Index { get; set; }
        public string UserGroup_Name { get; set; }
        public string UserGroup_Id { get; set; }

        public Guid Menu_Index { get; set; }
        public string Menu_Id { get; set; }
        public string Menu_Name { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_User
    {
        [Key]
        public Guid User_Index { get; set; }

        public string User_Id { get; set; }

        public string User_Name { get; set; }
        public string User_Password { get; set; }


        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public Guid UserGroup_Index { get; set; }

        public string UserGroup_Id { get; set; }
        public string UserGroup_Name { get; set; }

    }
}

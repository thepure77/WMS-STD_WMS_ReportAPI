using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_UserGroupZone
    {
        [Key]
        public Guid UserGroupZone_Index { get; set; }
        public string UserGroupZone_Id { get; set; }

        public Guid UserGroup_Index { get; set; }
        public string UserGroup_Name { get; set; }
        public string UserGroup_Id { get; set; }

        public Guid Zone_Index { get; set; }
        public string Zone_Id { get; set; }
        public string Zone_Name { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }


    }
}

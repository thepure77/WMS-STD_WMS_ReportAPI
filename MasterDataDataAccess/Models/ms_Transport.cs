using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class ms_Transport
    {
        [Key]
        public Guid? Transport_Index { get; set; }
        public string Transport_Id { get; set; }
        public string Transport_Name { get; set; }
    }
}

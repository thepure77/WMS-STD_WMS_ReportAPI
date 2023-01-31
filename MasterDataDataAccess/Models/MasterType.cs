using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public class MasterType: Identity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorType_Index { get; set; }
        public string VendorType_Id { get; set; }
        public string VendorType_Name { get; set; }
        public int IsSystem { get; set; }
    }
}

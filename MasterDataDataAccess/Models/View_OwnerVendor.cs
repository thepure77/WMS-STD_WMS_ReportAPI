using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_OwnerVendor
    {
        [Key]
        public Guid OwnerVendor_Index { get; set; }
        public string OwnerVendor_Id { get; set; }

        public Guid? Vendor_Index { get; set; }
        public string Vendor_Id { get; set; }
        public string Vendor_Name { get; set; }

        public Guid? Owner_Index { get; set; }
        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

    }
}

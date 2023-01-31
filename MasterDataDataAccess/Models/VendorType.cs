using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public class VendorType: Identity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid VendorType_Index { get; set; }
        public string VendorType_Id { get; set; }
        public string VendorType_Name { get; set; }
        public string Vendor_Id { get; set; }
        public string Vendor_Name { get; set; }
        public string Vendor_Address { get; set; }
        public string Vendor_District { get; set; }
        public string Vendor_SubDistrict { get; set; }
        public string Vendor_Province { get; set; }
        public string Vendor_Country { get; set; }
        public string Vendor_PostCode { get; set; }
    }
}

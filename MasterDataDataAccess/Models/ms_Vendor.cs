using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MasterDataDataAccess.Models
{

    public partial class MS_Vendor
    {
        [Key]
        public Guid Vendor_Index { get; set; }

        [StringLength(50)]
        public string Vendor_Id { get; set; }

        [StringLength(200)]
        public string Vendor_Name { get; set; }

        [StringLength(200)]
        public string VendorType_Name { get; set; }
        
        [StringLength(200)]
        public string Vendor_Address { get; set; }

        public Guid? VendorType_Index { get; set; }

        public Guid? SubDistrict_Index { get; set; }

        public Guid? District_Index { get; set; }

        public Guid? Province_Index { get; set; }

        public Guid? Country_Index { get; set; }

        public Guid? Postcode_Index { get; set; }

        [StringLength(200)]
        public string Vendor_TaxID { get; set; }

        [StringLength(200)]
        public string Vendor_Email { get; set; }

        [StringLength(200)]
        public string Vendor_Fax { get; set; }

        [StringLength(200)]
        public string Vendor_Tel { get; set; }

        [StringLength(200)]
        public string Vendor_Mobile { get; set; }

        [StringLength(200)]
        public string Vendor_Barcode { get; set; }

        [StringLength(200)]
        public string Contact_Person { get; set; }

        [StringLength(200)]
        public string Contact_Person2 { get; set; }

        [StringLength(200)]
        public string Contact_Person3 { get; set; }

        [StringLength(200)]
        public string Contact_Tel { get; set; }

        [StringLength(200)]
        public string Contact_Tel2 { get; set; }

        [StringLength(200)]
        public string Contact_Tel3 { get; set; }

        [StringLength(200)]
        public string Contact_Email { get; set; }

        [StringLength(200)]
        public string Contact_Email2 { get; set; }

        [StringLength(200)]
        public string Contact_Email3 { get; set; }

        public int IsActive { get; set; }

        public int IsDelete { get; set; }

        public int IsSystem { get; set; }

        public int Status_Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Create_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Create_Date { get; set; }

        [StringLength(200)]
        public string Update_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Update_Date { get; set; }

        [StringLength(200)]
        public string Cancel_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Cancel_Date { get; set; }

        public string Postcode_Id { get; set; }

        public string Postcode_Name { get; set; }
    }
}

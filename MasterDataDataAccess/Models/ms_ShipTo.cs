using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MasterDataDataAccess.Models
{

    public partial class MS_ShipTo
    {

        [Key]
        public Guid ShipTo_Index { get; set; }

        [StringLength(50)]
        public string ShipTo_Id { get; set; }

        [StringLength(200)]
        public string ShipTo_Name { get; set; }
       
        [StringLength(200)]
        public string ShipTo_Address { get; set; }

        public Guid ShipToType_Index { get; set; }

        public string ShipTo_Mobile { get; set;}

        public string ShipTo_TaxID { get; set; }

        public string ShipTo_Email { get; set; }

        public string ShipTo_Fax { get; set; }

        public string ShipTo_Tel { get; set; }

        public string ShipTo_Barcode { get; set; }

        public string Contact_Person { get; set; }

        public string Contact_Tel { get; set; }

        public string Contact_Email { get; set; }

        public Guid? SubDistrict_Index { get; set; }

        public Guid? District_Index { get; set; }

        public Guid? Province_Index { get; set; }

        public Guid? Country_Index { get; set; }

        public Guid? Postcode_Index { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public int? IsSystem { get; set; }

        public int? Status_Id { get; set; }

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

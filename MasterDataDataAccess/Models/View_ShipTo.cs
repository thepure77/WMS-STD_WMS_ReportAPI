using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_ShipTo
    {
        [Key]
        public Guid ShipTo_Index { get; set; }
        public string ShipTo_Id { get; set; }
        public string ShipTo_Name { get; set; }
        public string ShipTo_Address { get; set; }

        public Guid? ShipToType_Index { get; set; }
        public string ShipToType_Id { get; set; }
        public string ShipToType_Name { get; set; }

        public Guid? SubDistrict_Index { get; set; }
        public string SubDistrict__Id { get; set; }
        public string SubDistrict_Name { get; set; }

        public Guid? District_Index { get; set; }
        public string District_Id { get; set; }
        public string District_Name { get; set; }

        public Guid? Province_Index { get; set; }
        public string Province_Id { get; set; }
        public string Province_Name { get; set; }

        public Guid? Country_Index { get; set; }
        public string Country_Id { get; set; }
        public string Country_Name { get; set; }

        public Guid? Postcode_Index { get; set; }
        public string Postcode_Id { get; set; }
        public string Postcode_Name { get; set; }

        public string ShipTo_TaxID { get; set; }
        public string ShipTo_Email { get; set; }
        public string ShipTo_Fax { get; set; }
        public string ShipTo_Tel { get; set; }
        public string ShipTo_Mobile { get; set; }
        public string ShipTo_Barcode { get; set; }
        public string Contact_Person { get; set; }
        public string Contact_Tel { get; set; }
        public string Contact_Email { get; set; }

        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }
      

    }
}

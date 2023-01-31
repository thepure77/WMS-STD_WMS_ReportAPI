using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataDataAccess.Models
{
    public partial class MS_AddressDistrict
    {
        [Key]
        public Guid District_Index { get; set; }

        [StringLength(50)]
        public string District_Id { get; set; }

        [StringLength(200)]
        public string District_Name { get; set; }

        [StringLength(200)]
        public string District_NameEN { get; set; }

        public Guid Province_Index { get; set; }

        public Guid Country_Index { get; set; }

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
    }
}

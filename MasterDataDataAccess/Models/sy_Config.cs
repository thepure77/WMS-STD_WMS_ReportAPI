using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sy_Config
    {
        [Key]
        public int Config_Index { get; set; }

        public string Config_Name { get; set; }

        public string Config_Key { get; set; }
        public string Config_Value { get; set; }


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

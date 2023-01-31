using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_GetLocationWorkArea
    {
        [Key]
        public Guid LocationWorkArea_Index { get; set; }

        [StringLength(50)]
        public string LocationWorkArea_Id { get; set; }

        public Guid Location_Index { get; set; }

        public string Location_Name { get; set; }

        public Guid WorkArea_Index { get; set; }

        public string WorkArea_Name { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }


        public int? Status_Id { get; set; }

        public int? IsSystem { get; set; }


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

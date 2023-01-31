using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataDataAccess.Models
{


    public partial class sy_Menu
    {
        [Key]
        public Guid Menu_Index { get; set; }

        public Guid? MenuType_Index { get; set; }

        [StringLength(50)]
        public string Menu_Id { get; set; }

        [StringLength(200)]
        public string MenuControl_Name { get; set; }

        [StringLength(200)]
        public string Menu_Name { get; set; }

        [StringLength(200)]
        public string Menu_SecondName { get; set; }

        [StringLength(200)]
        public string Menu_ThirdName { get; set; }

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

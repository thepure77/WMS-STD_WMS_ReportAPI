using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_WaveRule
    {
        [Key]
        public Guid WaveRule_Index { get; set; }

        public string WaveRule_Id { get; set; }

        public int? WaveRule_Seq { get; set; }
        public Guid Wave_Index { get; set; }


        public string Wave_Id { get; set; }

        public string Wave_Name { get; set; }

        public Guid Rule_Index { get; set; }

        public string Rule_Id { get; set; }
        public string Rule_Name { get; set; }

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
        public Guid Process_Index { get; set; }

    }
}

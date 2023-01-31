using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataDataAccess.Models
{
  

    public partial class sy_RuleConditionField
    {
        [Key]
        public Guid RuleConditionField_Index { get; set; }

        public Guid? Process_Index { get; set; }

        [StringLength(200)]
        public string RuleConditionField_Name { get; set; }

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

        //public virtual sy_Process sy_Process { get; set; }

        public int? IsSearch { get; set; }

        public int? IsSort { get; set; }

        public int? IsDestination { get; set; }

        public int? IsSource { get; set; }

    }
}

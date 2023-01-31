using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataDataAccess.Models
{


    public partial class MS_RuleputawayCondition
    {
        [Key]
        public Guid RuleputawayCondition_Index { get; set; }

        public string RuleputawayCondition_Id { get; set; }

        public string RuleputawayCondition_Name { get; set; }

        public Guid? RuleputawayConditionField_Index { get; set; }

        public string RuleputawayConditionOperator { get; set; }

        public string RuleputawayCondition_Param { get; set; }

        public Guid? Zoneputaway_Index { get; set; }
        
        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public int? IsSystem { get; set; }

        public int? Status_Id { get; set; }

        public string Create_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Create_Date { get; set; }

        [StringLength(400)]
        public string Update_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Update_Date { get; set; }

        [StringLength(400)]
        public string Cancel_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Cancel_Date { get; set; }

    }
}

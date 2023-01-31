using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_RuleCondition
    {
        [Key]
        public Guid RuleCondition_Index { get; set; }
        public string RuleCondition_Param { get; set; }
        public int? RuleCondition_Seq { get; set; }
        public int? Status_Id { get; set; }
        public int? IsSystem { get; set; }
        public int? IsSearch { get; set; }
        public int? IsSort { get; set; }
        public int? IsDestination { get; set; }
        public int? IsSource { get; set; }
        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }

        public Guid Rule_Index { get; set; }
        public string Rule_Id { get; set; }
        public string Rule_Name { get; set; }

        public Guid RuleConditionOperation_Index { get; set; }
        public string RuleConditionOperationType { get; set; }
        public string RuleConditionOperation { get; set; }

        public Guid RuleConditionField_Index { get; set; }
        public string RuleConditionField_Name { get; set; }
    }
}

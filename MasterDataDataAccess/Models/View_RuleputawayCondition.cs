using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_RuleputawayCondition
    {
        [Key]
        public Guid RuleputawayCondition_Index { get; set; }
        public string RuleputawayCondition_Id { get; set; }
        public string RuleputawayCondition_Name { get; set; }
        public string RuleputawayConditionOperator { get; set; }
        public string RuleputawayCondition_Param { get; set; }
        public Guid? RuleputawayConditionField_Index { get; set; }
        public string RuleputawayConditionField_Id { get; set; }
        public string RuleputawayConditionField_Name { get; set; }
        public Guid? Zoneputaway_Index { get; set; }
        public string Zoneputaway_Id { get; set; }
        public string Zoneputaway_Name { get; set; }
        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }
      
    }
}

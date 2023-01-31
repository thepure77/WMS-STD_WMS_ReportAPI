using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_RuleConditionOperation
    {
        [Key]
        public Guid? RuleConditionOperation_Index { get; set; }
        public string RuleConditionOperationType { get; set; }
        public string RuleConditionOperation { get; set; }
        

        public Guid? RuleConditionField_Index { get; set; }
        public string RuleConditionField_Name { get; set; }
   
        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }
        public int? IsSystem { get; set; }
        public int? Status_Id { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_RuleConditionField
    {
        [Key]
        public Guid RuleConditionField_Index { get; set; }
        public string RuleConditionField_Name { get; set; }

        public Guid? Process_Index { get; set; }
        public string Process_Id { get; set; }
        public string Process_Name { get; set; }
        public int? IsSearch { get; set; }
        public int? IsSort { get; set; }
        public int? IsDestination { get; set; }
        public int? IsSource { get; set; }
        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }
      
    }
}

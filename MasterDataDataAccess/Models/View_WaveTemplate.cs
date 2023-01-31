using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_WaveTemplate
    {


        [Column(Order = 0)]
        public Guid Wave_Index { get; set; }

        [StringLength(50)]
        public string Wave_Id { get; set; }

        [StringLength(200)]
        public string Wave_Name { get; set; }

        [StringLength(50)]
        public string WaveRule_Id { get; set; }

        public int? WaveRule_Seq { get; set; }


        [Column(Order = 1)]
        public Guid WaveRule_Index { get; set; }


        [Column(Order = 2)]
        public Guid Process_Index { get; set; }

        [StringLength(50)]
        public string Process_Id { get; set; }

        [StringLength(200)]
        public string Process_Name { get; set; }


        [Column(Order = 3)]
        public Guid Rule_Index { get; set; }

        [StringLength(50)]
        public string Rule_Id { get; set; }

        [StringLength(200)]
        public string Rule_Name { get; set; }

        public int? Rule_Seq { get; set; }


        [Column(Order = 4)]
        public Guid RuleConditionField_Index { get; set; }

        [StringLength(200)]
        public string RuleConditionField_Name { get; set; }


        [Column(Order = 5)]
        public Guid RuleConditionOperation_Index { get; set; }

        [StringLength(200)]
        public string RuleConditionOperationType { get; set; }

        [StringLength(200)]
        public string RuleConditionOperation { get; set; }


        [Column(Order = 6)]
        public Guid RuleCondition_Index { get; set; }

        [StringLength(200)]
        public string RuleCondition_Param { get; set; }

        public int? RuleCondition_Seq { get; set; }

        public int? IsSearch { get; set; }

        public int? IsSort { get; set; }

        public int? IsSource { get; set; }

        public int? IsDestination { get; set; }

        [Key]
        public long? RowIndex { get; set; }
    }
}

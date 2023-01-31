using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace GIDataAccess.Models
{

    public partial class View_TaskGroupLocationWorkArea
    {
        [Key]
        public Guid? TaskGroup_Index { get; set; }
        public string TaskGroup_Id { get; set; }
        public string TaskGroup_Name { get; set; }
        public Guid? TaskGroupWorkArea_Index { get; set; }
        public string TaskGroupWorkArea_Id { get; set; }
        public Guid? WorkArea_Index { get; set; }
        public string WorkArea_Id { get; set; }
        public string WorkArea_Name { get; set; }
        public int? Task_IsActive { get; set; }
        public int? TaskGroupWorkArea_IsActive { get; set; }
        public int? WorkArea_IsActive { get; set; }
        public Guid? Location_Index { get; set; }
        public string Location_Id { get; set; }
        public string Location_Name { get; set; }
        public Guid? LocationWorkArea_Index { get; set; }
        public string LocationWorkArea_Id  { get; set; }

    }
}

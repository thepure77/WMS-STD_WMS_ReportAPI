using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace GIDataAccess.Models
{

    public partial class View_RPT18_Task
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid? Task_Index { get; set; }
        public string Picking_By { get; set; }

        
    }
}

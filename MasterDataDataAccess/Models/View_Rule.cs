using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_Rule
    {
        [Key]
        public Guid Rule_Index { get; set; }
        public string Rule_Id { get; set; }
        public string Rule_Name { get; set; }

        public Guid Process_Index { get; set; }
        public string Process_Id { get; set; }
        public string Process_Name { get; set; }

        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }

    }
}

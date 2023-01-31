using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_LocationWorkArea
    {
        [Key]
        public Guid LocationWorkArea_Index { get; set; }
        public string LocationWorkArea_Id { get; set; }
 

        public Guid Location_Index { get; set; }
        public string Location_Id { get; set; }
        public string Location_Name { get; set; }

        public Guid WorkArea_Index { get; set; }
        public string WorkArea_Id { get; set; }
        public string WorkArea_Name { get; set; }

        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }
      

    }
}

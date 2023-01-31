using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_ZoneLocation
    {
        [Key]
        public Guid ZoneLocation_Index { get; set; }
        public string ZoneLocation_Id { get; set; }

        public Guid? Zone_Index { get; set; }
        public string Zone_Id { get; set; }
        public string Zone_Name { get; set; }

        public Guid? Location_Index { get; set; }
        public string Location_Id { get; set; }
        public string Location_Name { get; set; }
                
        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }
      

    }
}

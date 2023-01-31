using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_Facility
    {
        [Key]
        public Guid Facility_Index { get; set; }
        public string Facility_Id { get; set; }
        public string Facility_Name { get; set; }
        public Guid? FacilityType_Index { get; set; }
        public string FacilityType_Id { get; set; }
        public string FacilityType_Name { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_Count_location
    {
        [Key]
        public string LocationType_Name { get; set; }
        public int? Count_location { get; set; }
        public int? Count_IsUse { get; set; }
        public int? Count_Empty { get; set; }
        public int? Count_Block { get; set; }
        public decimal? Per_IsUser { get; set; }
        public decimal? Per_Empty { get; set; }
        public decimal? Per_Block { get; set; }
    }
}

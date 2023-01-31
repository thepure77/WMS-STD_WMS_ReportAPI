using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_RPT_MS_SummaryMaterialsStock
    {
        [Key]
        public long? Row_Index { get; set; }
        public string ProductType_Name { get; set; }
        public Guid? Product_Index { get; set; }
    }
}

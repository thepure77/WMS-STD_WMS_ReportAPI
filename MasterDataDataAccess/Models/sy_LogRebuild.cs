using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataDataAccess.Models
{
    public partial class sy_LogRebuild
    {
        [Key]
        public Guid Rebuild_Index { get; set; }

        [StringLength(50)]
        public string Rebuild_By { get; set; }

        public DateTime? Rebuild_Date_Start { get; set; }

        public DateTime? Rebuild_Date_End { get; set; }

    }
}



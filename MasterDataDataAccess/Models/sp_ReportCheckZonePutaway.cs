using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_ReportCheckZonePutaway
    {
       [Key]
       public string Zoneputaway_Id          {get; set;}
       public string Zoneputaway_Name        {get; set;}
       public int? CountLocation_Name      {get; set;}
       public int? CountProduct { get; set; }
    }
}

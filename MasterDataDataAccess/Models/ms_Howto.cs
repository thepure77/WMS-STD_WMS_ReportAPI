using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class ms_Howto
    {   
        [Key]
        public Guid Howto_index { get; set; }
        public string Howto_Id { get; set; }
        public string Howto_Name { get; set; }
        public string Howto_Directlink { get; set; }
        public string Howto_type { get; set; }
        public int IsActive { get; set; }
        public int IsDelete { get; set; }
        public string Create_By { get; set; }
        public DateTime? Create_Date { get; set; }
        public string Update_By { get; set; }
        public DateTime? Update_Date { get; set; }
        public string Cancel_By { get; set; }
        public DateTime? Cancel_Date { get; set; }

    }
}

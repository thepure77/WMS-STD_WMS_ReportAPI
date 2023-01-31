using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDataDataAccess.Models
{
    public abstract class Identity {
        public Identity() {
            Create_Date = DateTime.Now;
            Update_Date = DateTime.Now;
            Create_By = "";
            Update_By = "";
            IsActive = true;
            IsDelete = false;
        }
        
        public DateTime? Create_Date { get; set; }
        [MaxLength(128)]
        public string Create_By { get; set; }
        public DateTime? Update_Date { get; set; }
        [MaxLength(128)]
        public string Update_By { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_DocumentType
    {
        [Key]
        public Guid DocumentType_Index { get; set; }
        public string DocumentType_Id { get; set; }
        public string DocumentType_Name { get; set; }
        public string Format_Text { get; set; }
        public string Format_Date { get; set; }
        public string Format_Running { get; set; }
        public string Format_Document { get; set; }
        public int? IsResetByYear { get; set; }
        public int? IsResetByMonth { get; set; }
        public int? IsResetByDay { get; set; }

        public Guid? Process_Index { get; set; }
        public string Process_Id { get; set; }
        public string Process_Name { get; set; }

        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }

    }
}

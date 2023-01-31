using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public class sp_LogCancel
    {
        [Key]
        public long RowIndex { get; set; }

        public string WMS_ID { get; set; }

        public string DOC_LINK { get; set; }

        public string Json { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string WMS_ID_STATUS { get; set; }

        public string Type { get; set; }

        public string Mat_Doc { get; set; }

        public string MESSAGE { get; set; }

    }
}

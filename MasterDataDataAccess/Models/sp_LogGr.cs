using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_LogGr
    {
        [Key]
        //public long RowIndexgr { get; set; }
        //public string WMS_IDgr { get; set; }
        //public string DOC_LINKgr { get; set; }
        ////public int? IsActive { get; set; }
        ////public int? IsDelete { get; set; }
        //public string Jsongr { get; set; }
        //public string MESSAGEgr { get; set; }
        //public DateTime? CreatedDategr { get; set; }
        //public string WMS_ID_STATUSgr { get; set; }
        //public string Typegr { get; set; }

        public long RowIndex { get; set; }
        public string WMS_ID { get; set; }
        public string DOC_LINK { get; set; }
        //public int? IsActive { get; set; }
        //public int? IsDelete { get; set; }
        public string Json { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string WMS_ID_STATUS { get; set; }
        public string Type { get; set; }
        public string Mat_Doc { get; set; }
        public string MESSAGE { get; set; }
    }
}
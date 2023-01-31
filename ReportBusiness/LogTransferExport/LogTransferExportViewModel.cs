using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.LogTransferExport
{
    public class LogTransferExportViewModel
    {

        public string key { get; set; }
        public string date { get; set; }
        public string Goodstransfer_Due_Date { get; set; }
        public string Goodstransfer_Due_Date_To { get; set; }

        public long rownum { get; set; }

        public long rowIndex { get; set; }
        public string wms_ID { get; set; }
        public string doc_LINK { get; set; }
        public string json { get; set; }
        public string createDate { get; set; }
        public string wms_ID_STATUS { get; set; }
        public string type { get; set; }
        public string mat_Doc { get; set; }
        public string mESSAGE { get; set; }
        //เงื่อนไขเเยกห้อง
        public string room_Name { get; set; }
        public LogTransferExportViewModel()
        {
            status = new List<statusExportViewModelCancel>();
            status_SAP = new List<statusExportViewModelCancel>();

        }
        public List<statusExportViewModelCancel> status { get; set; }
        public List<statusExportViewModelCancel> status_SAP { get; set; }
    }

    public class statusExportViewModelCancel
    {
        public string value { get; set; }
        public string display { get; set; }
        public int seq { get; set; }
    }
}
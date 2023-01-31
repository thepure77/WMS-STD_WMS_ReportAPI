using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.LogGiExport
{
    public class LogGiExportViewModel
    {

        public string key { get; set; }
        public string key1 { get; set; }
        public string date { get; set; }
        public string PlanGoodsIssue_Due_Date { get; set; }
        public string PlanGoodsIssue_Due_Date_To { get; set; }

        public long rowIndexgi { get; set; }
        public string wms_IDgi { get; set; }
        public string doc_LINKgi { get; set; }
        //public int? IsActive { get; set; }
        //public int? IsDelete { get; set; }
        public string jsongi { get; set; }
        public string createdDategi { get; set; }
        public string wms_ID_STATUSgi { get; set; }
        public string typegi { get; set; }
        public string mat_Docgi { get; set; }
        public string mESSAGEgi { get; set; }
        //เงื่อนไขเเยกห้อง
        public string room_Name { get; set; }
        public long numrow { get; set; }
        public LogGiExportViewModel()
        {
            statusgi = new List<statusExportgiViewModel>();
            status_gi = new List<statusExportgiViewModel>();

        }
        public List<statusExportgiViewModel> statusgi { get; set; }
        public List<statusExportgiViewModel> status_gi { get; set; }
    }

    public class statusExportgiViewModel
    {
        public string value { get; set; }
        public string display { get; set; }
        public int seq { get; set; }
    }
    public class actionResultLoggiExportViewModel
    {
        public IList<LogGiExportViewModel> itemsLoggi { get; set; }
    }
}
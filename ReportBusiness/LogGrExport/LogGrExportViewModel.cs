using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.LogGrExport
{
    public class LogGrExportViewModel
    {
        public string purchaseOrder_No { get; set; }
        public string planGoodsReceive_No { get; set; }
        public string goodsReceive_No { get; set; }
        public string date { get; set; }
        public string PlanGoodsReceive_Due_Date { get; set; }
        public string PlanGoodsReceive_Due_Date_To { get; set; }
        public string order_remark { get; set; }

        public long rowIndexgr { get; set; }
        public string wms_IDgr { get; set; }
        public string doc_LINKgr { get; set; }
        //public int? IsActive { get; set; }
        //public int? IsDelete { get; set; }
        public string jsongr { get; set; }
        public string createdDategr { get; set; }
        public string wms_ID_STATUSgr { get; set; }
        public string typegr { get; set; }
        public string mat_Docgr { get; set; }
        public string mESSAGEgr { get; set; }
        //เงื่อนไขเเยกห้อง
        public string room_Name { get; set; }
        public long numrow { get; set; }
        public LogGrExportViewModel()
        {
            statusgr = new List<statusExportgrViewModel>();
            status_gr = new List<statusExportgrViewModel>();

        }
        public List<statusExportgrViewModel> statusgr { get; set; }
        public List<statusExportgrViewModel> status_gr { get; set; }
    }

    public class statusExportgrViewModel
    {
        public string value { get; set; }
        public string display { get; set; }
        public int seq { get; set; }
    }
    public class actionResultLoggrExportViewModel
    {
        public IList<LogGrExportViewModel> itemsLoggr { get; set; }
    }
}
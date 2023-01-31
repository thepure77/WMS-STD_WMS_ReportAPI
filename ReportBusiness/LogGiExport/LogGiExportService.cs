using AspNetCore.Reporting;
using Business.Library;
using Common.Utils;
using DataAccess;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using PlanGRBusiness.Libs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ReportBusiness.LogGiExport
{
    public class LogGiExportService
    {
        private MasterDataDbContext db;

        public LogGiExportService()
        {
            db = new MasterDataDbContext();
        }

        public LogGiExportService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region Exportgi
        public dynamic Exportgi(LogGiExportViewModel data, string rootPath = "")
        {
            try
            {
                var Master_DBContext = new MasterDataDbContext();
                var temp_Master_DBContext = new temp_MasterDataDbContext();
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                Master_DBContext.Database.SetCommandTimeout(360);

                var PlanGoodsIssue_No = new SqlParameter("@PlanGoodsIssue_No", "");

                if (!string.IsNullOrEmpty(data.key))
                {
                    PlanGoodsIssue_No = new SqlParameter("@PlanGoodsIssue_No", data.key);
                }

                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var PlanGoodsIssue_Due_Date = new SqlParameter("@PlanGoodsIssue_Due_Date", "");
                var PlanGoodsIssue_Due_Date_To = new SqlParameter("@PlanGoodsIssue_Due_Date_To", "");
                if (!string.IsNullOrEmpty(data.PlanGoodsIssue_Due_Date_To) || !string.IsNullOrEmpty(data.PlanGoodsIssue_Due_Date))
                {
                    dateStart = data.PlanGoodsIssue_Due_Date.toBetweenDate().start;
                    dateEnd = data.PlanGoodsIssue_Due_Date_To.toBetweenDate().end;
                    PlanGoodsIssue_Due_Date = new SqlParameter("@PlanGoodsIssue_Due_Date", dateStart);
                    PlanGoodsIssue_Due_Date_To = new SqlParameter("@PlanGoodsIssue_Due_Date_To", dateEnd);
                }
                var resultquery = new List<MasterDataDataAccess.Models.sp_LogGi>();
                if (data.room_Name == "01")
                {
                    resultquery = temp_Master_DBContext.sp_LogGi.FromSql("sp_LogGi @PlanGoodsIssue_No , @PlanGoodsIssue_Due_Date , @PlanGoodsIssue_Due_Date_To", PlanGoodsIssue_No, PlanGoodsIssue_Due_Date, PlanGoodsIssue_Due_Date_To).ToList();
                }
                else
                {
                    resultquery = Master_DBContext.sp_LogGi.FromSql("sp_LogGi @PlanGoodsIssue_No , @PlanGoodsIssue_Due_Date , @PlanGoodsIssue_Due_Date_To", PlanGoodsIssue_No, PlanGoodsIssue_Due_Date, PlanGoodsIssue_Due_Date_To).ToList();
                }
                

                var statusModels = new List<string>();
                var status_SAPModels = new List<string>();
                //var sortModels = new List<SortModel>();

                if (data.statusgi.Count > 0)
                {
                    foreach (var item in data.statusgi)
                    {
                        if (item.value == "ส่ง")
                        {
                            statusModels.Add("ส่ง");
                        }
                        if (item.value == "ตอบกลับ")
                        {
                            statusModels.Add("ตอบกลับ");
                        }
                        if (item.value == "ส่งไม่สำเร็จ")
                        {
                            statusModels.Add("ส่งไม่สำเร็จ");
                        }

                    }
                    resultquery = resultquery.Where(c => statusModels.Contains(c.Type)).ToList();
                }

                if (data.status_gi.Count > 0)
                {
                    foreach (var item in data.status_gi)
                    {
                        if (item.value == "1")
                        {
                            status_SAPModels.Add("1");
                        }
                        if (item.value == "-1")
                        {
                            status_SAPModels.Add("-1");
                        }
                        if (item.value == "E")
                        {
                            status_SAPModels.Add("E");
                        }
                        if (item.value == "C")
                        {
                            status_SAPModels.Add("C");
                        }
                        if (item.value == "P")
                        {
                            status_SAPModels.Add("P");
                        }
                        if (item.value == "EV")
                        {
                            status_SAPModels.Add("EV");
                        }

                    }
                    resultquery = resultquery.Where(c => status_SAPModels.Contains(c.WMS_ID_STATUS)).ToList();
                }

                var Item = new List<sp_LogGi>();
                var TotalRow = new List<sp_LogGi>();

                TotalRow = resultquery;

                Item = resultquery.OrderBy(o => o.CreatedDate).ToList();

                var result = new List<LogGiExportViewModel>();
                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new LogGiExportViewModel();
                    resultItem.numrow = num + 1;
                    resultItem.rowIndexgi = item.RowIndex;
                    resultItem.wms_IDgi = item.WMS_ID;
                    resultItem.doc_LINKgi = item.DOC_LINK;
                    resultItem.jsongi = item.Json;
                    resultItem.createdDategi = item.CreatedDate != null ? item.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.wms_ID_STATUSgi = item.WMS_ID_STATUS;
                    resultItem.typegi = item.Type;
                    resultItem.mat_Docgi = item.Mat_Doc;
                    resultItem.mESSAGEgi = item.MESSAGE;

                    result.Add(resultItem);
                    num++;
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("LogGiExport");

                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "tmpReport";

                Utils objReport = new Utils();
                var renderedBytes = report.Execute(RenderType.Excel);
                fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);
                var saveLocation = objReport.PhysicalPath(fileName + ".xls", rootPath);
                return saveLocation;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

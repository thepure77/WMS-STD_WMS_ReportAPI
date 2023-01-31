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

namespace ReportBusiness.LogCancelExport
{
    public class LogCancelExportService
    {
        private MasterDataDbContext db;

        public LogCancelExportService()
        {
            db = new MasterDataDbContext();
        }

        public LogCancelExportService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region ExportCancel
        public string ExportCancel(LogCancelExportViewModel data, string rootPath = "")
        {
            try
            {
                var Master_DBContext = new MasterDataDbContext();
                var temp_Master_DBContext = new temp_MasterDataDbContext();

                temp_Master_DBContext.Database.SetCommandTimeout(360);
                Master_DBContext.Database.SetCommandTimeout(360);

                var DOC_LINK_No = new SqlParameter("@DOC_LINK_No", "");

                if (!string.IsNullOrEmpty(data.key))
                {
                    DOC_LINK_No = new SqlParameter("@DOC_LINK_No", data.key);
                }

                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var create_Date = new SqlParameter("@DOC_LINK_Due_Date", "");
                var create_Date_To = new SqlParameter("@DOC_LINK_Due_Date_To", "");
                if (!string.IsNullOrEmpty(data.create_Date_To) || !string.IsNullOrEmpty(data.create_Date))
                {
                    dateStart = data.create_Date.toBetweenDate().start;
                    dateEnd = data.create_Date_To.toBetweenDate().end;
                    create_Date = new SqlParameter("@DOC_LINK_Due_Date", dateStart);
                    create_Date_To = new SqlParameter("@DOC_LINK_Due_Date_To", dateEnd);
                }
                var resultquery = new List<MasterDataDataAccess.Models.sp_LogCancel>();

                if (data.room_Name == "01")
                {
                    resultquery = temp_Master_DBContext.sp_LogCancel.FromSql("sp_LogCancel @DOC_LINK_No , @DOC_LINK_Due_Date , @DOC_LINK_Due_Date_To", DOC_LINK_No, create_Date, create_Date_To).ToList();
                }
                else
                {
                    resultquery = Master_DBContext.sp_LogCancel.FromSql("sp_LogCancel @DOC_LINK_No , @DOC_LINK_Due_Date , @DOC_LINK_Due_Date_To", DOC_LINK_No, create_Date, create_Date_To).ToList();
                }

                var statusModels = new List<string>();
                var status_SAPModels = new List<string>();
                //var sortModels = new List<SortModel>();

                if (data.status.Count > 0)
                {
                    foreach (var item in data.status)
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

                if (data.status_SAP.Count > 0)
                {
                    foreach (var item in data.status_SAP)
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



                var Item = new List<sp_LogCancel>();
                var TotalRow = new List<sp_LogCancel>();

                TotalRow = resultquery;

                Item = resultquery.OrderBy(o => o.WMS_ID).ToList();

                var result = new List<LogCancelExportViewModel>();
                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new LogCancelExportViewModel();

                    resultItem.rownum = num + 1;
                    resultItem.rowIndex = item.RowIndex;
                    resultItem.wms_ID = item.WMS_ID;
                    resultItem.doc_LINK = item.DOC_LINK;
                    resultItem.json = item.Json;
                    resultItem.createDate = item.CreatedDate != null ? item.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                    resultItem.wms_ID_STATUS = item.WMS_ID_STATUS;
                    resultItem.type = item.Type;
                    resultItem.mat_Doc = item.Mat_Doc;
                    resultItem.mESSAGE = item.MESSAGE;

                    result.Add(resultItem);
                    num++;
                }


                //var LogCancelExportViewModel = new actionResultLogExportViewModelCancel();
                //LogCancelExportViewModel.itemsLogCancel = result.ToList();

                //return LogCancelExportViewModel;


                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Log_Cancel_Export");

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

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

namespace ReportBusiness.LogTransferExport
{
    public class LogTransferExportService
    {
        private MasterDataDbContext db;

        public LogTransferExportService()
        {
            db = new MasterDataDbContext();
        }

        public LogTransferExportService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region ExportTransfer
        public string ExportTransfer(LogTransferExportViewModel data, string rootPath = "")
        {
            try
            {
                var Master_DBContext = new MasterDataDbContext();
                var temp_Master_DBContext = new temp_MasterDataDbContext();

                temp_Master_DBContext.Database.SetCommandTimeout(360);
                Master_DBContext.Database.SetCommandTimeout(360);

                var Goodstransfer_No = new SqlParameter("@Goodstransfer_No", "");

                if (!string.IsNullOrEmpty(data.key))
                {
                    Goodstransfer_No = new SqlParameter("@Goodstransfer_No", data.key);
                }

                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var Goodstransfer_Due_Date = new SqlParameter("@Goodstransfer_Due_Date", "");
                var Goodstransfer_Due_Date_To = new SqlParameter("@Goodstransfer_Due_Date_To", "");
                if (!string.IsNullOrEmpty(data.Goodstransfer_Due_Date_To) || !string.IsNullOrEmpty(data.Goodstransfer_Due_Date))
                {
                    dateStart = data.Goodstransfer_Due_Date.toBetweenDate().start;
                    dateEnd = data.Goodstransfer_Due_Date_To.toBetweenDate().end;
                    Goodstransfer_Due_Date = new SqlParameter("@Goodstransfer_Due_Date", dateStart);
                    Goodstransfer_Due_Date_To = new SqlParameter("@Goodstransfer_Due_Date_To", dateEnd);
                }
                var resultquery = new List<MasterDataDataAccess.Models.sp_LogTransfer>();
                if (data.room_Name == "01")
                {
                    resultquery = temp_Master_DBContext.sp_LogTransfer.FromSql("sp_LogTransfer @Goodstransfer_No , @Goodstransfer_Due_Date , @Goodstransfer_Due_Date_To", Goodstransfer_No, Goodstransfer_Due_Date, Goodstransfer_Due_Date_To).ToList();
                }
                else
                {
                    resultquery = Master_DBContext.sp_LogTransfer.FromSql("sp_LogTransfer @Goodstransfer_No , @Goodstransfer_Due_Date , @Goodstransfer_Due_Date_To", Goodstransfer_No, Goodstransfer_Due_Date, Goodstransfer_Due_Date_To).ToList();
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



                var Item = new List<sp_LogTransfer>();
                var TotalRow = new List<sp_LogTransfer>();

                TotalRow = resultquery;

                Item = resultquery.OrderBy(o => o.CreatedDate).ToList();

                var result = new List<LogTransferExportViewModel>();
                int num = 0;
                foreach (var item in Item)
                {
                    var resultItem = new LogTransferExportViewModel();
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

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Log_Transfer_Export");

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

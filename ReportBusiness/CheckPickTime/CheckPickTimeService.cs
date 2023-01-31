using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using PlanGRBusiness.Libs;
using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportBusiness.CheckPickTime
{
    public class CheckPickTimeService
    {
        #region printReportLaborPerformance
        public dynamic printReportPan(CheckPickTimeViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<CheckPickTimeViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var goodsIssue_No = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;


                //var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();

                var statusModels = new List<int?>();

                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    goodsIssue_No = data.goodsIssue_No;
                }

                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }

                //@GI_NO
                //@GI_DATE
                //@GI_DATE_TO

                var gi_No = new SqlParameter("@GI_NO", goodsIssue_No);
                var gi_date = new SqlParameter("@GI_DATE", dateStart);
                var gi_date_to = new SqlParameter("@GI_DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_CheckPickTime>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_CheckPickTime.FromSql("sp_CheckPickTime @GI_NO , @GI_DATE , @GI_DATE_TO", gi_No, gi_date, gi_date_to).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_CheckPickTime.FromSql("sp_CheckPickTime @GI_NO , @GI_DATE , @GI_DATE_TO", gi_No, gi_date, gi_date_to).ToList();
                }

                //var resultquery = Master_DBContext.sp_CheckPickTime.FromSql("sp_CheckPickTime @GI_NO , @GI_DATE , @GI_DATE_TO", gi_No, gi_date, gi_date_to).ToList();
                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new CheckPickTimeViewModel();
                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.report_date = startDate;
                    resultItem.report_date_to = endDate;
                    //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    result.Add(resultItem);
                }
                else
                {
                    int num = 0;
                    foreach (var item in resultquery)
                    {
                        var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new CheckPickTimeViewModel();
                        resultItem.rowNo = num + 1;
                        resultItem.goodsIssue_No = item.GoodsIssue_No;
                        resultItem.goodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.documentRef_No1 = item.RoundWave;
                        resultItem.pick_type = item.PICKTYPE;
                        resultItem.min_Date = item.Min_Date != null ? item.Min_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.min_Time = item.Min_Time;
                        resultItem.max_Date = item.Max_Date != null ? item.Max_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.max_Time = item.Max_Time;
                        if (data.ambientRoom != "02")
                        {
                            resultItem.ambientRoom = "Ambient";
                        }
                        else
                        {
                            resultItem.ambientRoom = "Freeze";
                        }
                        result.Add(resultItem);
                        num++;
                    }
                }






                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("CheckPickTime");
                //var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSpaceUtilization");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "tmpReport" + DateTime.Now.ToString("yyyyMMddHHmmss");

                var renderedBytes = report.Execute(RenderType.Pdf);

                Utils objReport = new Utils();
                fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".pdf", rootPath);
                var saveLocation = objReport.PhysicalPath(fileName + ".pdf", rootPath);
                return saveLocation;


            }
            catch (Exception ex)
            {
                //olog.logging("ReportKPI", ex.Message);
                throw ex;
            }
        }
        #endregion
        public string ExportExcel(CheckPickTimeViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<CheckPickTimeViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var goodsIssue_No = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;


                //var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();

                var statusModels = new List<int?>();

                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    goodsIssue_No = data.goodsIssue_No;
                }

                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }

                //@GI_NO
                //@GI_DATE
                //@GI_DATE_TO

                var gi_No = new SqlParameter("@GI_NO", goodsIssue_No);
                var gi_date = new SqlParameter("@GI_DATE", dateStart);
                var gi_date_to = new SqlParameter("@GI_DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_CheckPickTime>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_CheckPickTime.FromSql("sp_CheckPickTime @GI_NO , @GI_DATE , @GI_DATE_TO", gi_No, gi_date, gi_date_to).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_CheckPickTime.FromSql("sp_CheckPickTime @GI_NO , @GI_DATE , @GI_DATE_TO", gi_No, gi_date, gi_date_to).ToList();
                }

                //var resultquery = Master_DBContext.sp_CheckPickTime.FromSql("sp_CheckPickTime @GI_NO , @GI_DATE , @GI_DATE_TO", gi_No, gi_date, gi_date_to).ToList();
                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new CheckPickTimeViewModel();
                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.report_date = startDate;
                    resultItem.report_date_to = endDate;
                    //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    result.Add(resultItem);
                }
                else
                {
                    int num = 0;
                    foreach (var item in resultquery)
                    {
                        var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new CheckPickTimeViewModel();
                        resultItem.rowNo = num + 1;
                        resultItem.goodsIssue_No = item.GoodsIssue_No;
                        resultItem.goodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.documentRef_No1 = item.RoundWave;
                        resultItem.pick_type = item.PICKTYPE;
                        resultItem.min_Date = item.Min_Date != null ? item.Min_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.min_Time = item.Min_Time;
                        resultItem.max_Date = item.Max_Date != null ? item.Max_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.max_Time = item.Max_Time;
                        if (data.ambientRoom != "02")
                        {
                            resultItem.ambientRoom = "Ambient";
                        }
                        else
                        {
                            resultItem.ambientRoom = "Freeze";
                        }
                        result.Add(resultItem);
                        num++;
                    }
                }




                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("CheckPickTime");

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
    }
}

using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using PlanGRBusiness.Libs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ReportBusiness.ReportLaborPerformance
{
    public class ReportLaborPerformanceService
    {
        #region printReportLaborPerformance
        public dynamic printReportLaborPerformance(ReportLaborPerformanceViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            //var BB_DBContext = new BinbalanceDbContext();


            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportLaborPerformanceViewModel>();

            try
            {
                var user_Id = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                if (!string.IsNullOrEmpty(data.User_Id))
                {
                    user_Id = data.User_Id;
                }
                olog.logging("ReportLabor", "E");
                if (!string.IsNullOrEmpty(data.Report_Date) && !string.IsNullOrEmpty(data.Report_Date_To))
                {
                    dateStart = data.Report_Date.toBetweenDate().start;
                    dateEnd = data.Report_Date_To.toBetweenDate().end;

                }

                var User_Id = new SqlParameter("@USERID", user_Id);
                var date = new SqlParameter("@DATE", dateStart);
                var date_to = new SqlParameter("@DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_LogLabor>();

                resultquery = Master_DBContext.sp_LogLabor.FromSql("sp_LogLabor @USERID , @DATE , @DATE_TO", User_Id, date, date_to).ToList();
                //resultquery = Master_DBContext.sp_LogLabor.FromSql("sp_LogLabor  @DATE , @DATE_TO",  date, date_to).ToList();

                //if (!string.IsNullOrEmpty(data.User_Id))
                //{
                //    resultquery = resultquery.Where(c => c.User_Id.Contains(data.User_Id)
                //                            || c.User_Name.Contains(data.User_Id)
                //                            || c.First_Name.Contains(data.User_Id)
                //                            || c.Last_Name.Contains(data.User_Id)
                //                            ).ToList();
                //}
                olog.logging("ReportLabor", resultquery.Count.ToString());
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportLaborPerformanceViewModel();
                    var startDate = DateTime.ParseExact(data.Report_Date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.Report_Date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.Report_Date = startDate;
                    resultItem.Report_Date_To = endDate;
                    //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    result.Add(resultItem);
                }
                else
                {
                    int num = 0;
                    foreach (var item in resultquery)
                    {
                        var startDate = DateTime.ParseExact(data.Report_Date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.Report_Date_To.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new ReportLaborPerformanceViewModel();
                        resultItem.rowNo = num + 1; 
                        resultItem.User_Id = item.User_Id;
                        //resultItem.Create_By = item.Create_By;
                        resultItem.Create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                        resultItem.TotalTime = item.UsedTime;
                        resultItem.First_Name = item.First_Name;
                        resultItem.Last_Name = item.Last_Name;
                        resultItem.Menu_Name = item.Menu_Name;
                        resultItem.Sub_Menu_Name = item.Sub_Menu_Name;
                        resultItem.start_process = item.StartProcess != null ? item.StartProcess.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        resultItem.end_proces = item.EndProcess != null ? item.EndProcess.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        resultItem.Operations = item.Operations;
                        result.Add(resultItem);
                        num++;
                    }
                }


                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportLaborPerformance");
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
                olog.logging("ReportLabor", ex.Message);
                throw ex;
            }
        }
        #endregion

        public string ExportExcel(ReportLaborPerformanceViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            //var BB_DBContext = new BinbalanceDbContext();


            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportLaborPerformanceViewModel>();

            try
            {
                var user_Id = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                if (!string.IsNullOrEmpty(data.User_Id))
                {
                    user_Id = data.User_Id;
                }
                if (!string.IsNullOrEmpty(data.Report_Date) && !string.IsNullOrEmpty(data.Report_Date_To))
                {
                    dateStart = data.Report_Date.toBetweenDate().start;
                    dateEnd = data.Report_Date_To.toBetweenDate().end;
                }

                var User_Id = new SqlParameter("@USERID", user_Id);
                var date = new SqlParameter("@DATE", dateStart);
                var date_to = new SqlParameter("@DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_LogLabor>();

                resultquery = Master_DBContext.sp_LogLabor.FromSql("sp_LogLabor @USERID , @DATE , @DATE_TO", User_Id, date, date_to).ToList();

                if (!string.IsNullOrEmpty(data.User_Id))
                {
                    resultquery = resultquery.Where(c => c.User_Id.Contains(data.User_Id)
                                            || c.User_Name.Contains(data.User_Id)
                                            || c.First_Name.Contains(data.User_Id)
                                            || c.Last_Name.Contains(data.User_Id)
                                            ).ToList();
                }
                
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportLaborPerformanceViewModel();
                    var startDate = DateTime.ParseExact(data.Report_Date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.Report_Date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.Report_Date = startDate;
                    resultItem.Report_Date_To = endDate;
                    //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    result.Add(resultItem);
                }
                else
                {
                    int num = 0;
                    foreach (var item in resultquery)
                    {
                        var startDate = DateTime.ParseExact(data.Report_Date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.Report_Date_To.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new ReportLaborPerformanceViewModel();
                        resultItem.rowNo = num + 1; 
                        resultItem.User_Id = item.User_Id;
                        //resultItem.Create_By = item.Create_By;
                        resultItem.Create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
                        resultItem.TotalTime = item.UsedTime;
                        resultItem.First_Name = item.First_Name;
                        resultItem.Last_Name = item.Last_Name;
                        resultItem.Menu_Name = item.Menu_Name;
                        resultItem.Sub_Menu_Name = item.Sub_Menu_Name;
                        resultItem.start_process = item.StartProcess != null ? item.StartProcess.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        resultItem.end_proces = item.EndProcess != null ? item.EndProcess.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        resultItem.Operations = item.Operations;
                        result.Add(resultItem);
                        num++;
                    }
                }

                
                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportLaborPerformance");
                LocalReport report = new LocalReport(reportPath);

                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "ReportLaborPerformance" + DateTime.Now.ToString("yyyyMMddHHmmss");

                var renderedBytes = report.Execute(RenderType.Excel);
                //fullPath = saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);

                Utils objReport = new Utils();
                fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);
                var saveLocation = objReport.PhysicalPath(fileName + ".xls", rootPath);
                return saveLocation;


                //var saveLocation = rootPath + fullPath;
                ////File.Delete(saveLocation);
                ////ExcelRefresh(reportPath);
                //return saveLocation;
            }
            catch (Exception ex)
            {
                //olog.logging("ReportLabor", ex.Message);
                throw ex;
            }

        }

        //public string saveReport(byte[] file, string name, string rootPath)
        //{
        //    var saveLocation = PhysicalPath(name, rootPath);
        //    FileStream fs = new FileStream(saveLocation, FileMode.Create);
        //    BinaryWriter bw = new BinaryWriter(fs);
        //    try
        //    {
        //        try
        //        {
        //            bw.Write(file);
        //        }
        //        finally
        //        {
        //            fs.Close();
        //            bw.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return VirtualPath(name);
        //}

        //public string PhysicalPath(string name, string rootPath)
        //{
        //    var filename = name;
        //    var vPath = ReportPath;
        //    var path = rootPath + vPath;
        //    if (!System.IO.Directory.Exists(path))
        //    {
        //        System.IO.Directory.CreateDirectory(path);
        //    }

        //    var saveLocation = System.IO.Path.Combine(path, filename);
        //    return saveLocation;
        //}
        //public string VirtualPath(string name)
        //{
        //    var filename = name;
        //    var vPath = ReportPath;
        //    vPath = vPath.Replace("~", "");
        //    return vPath + filename;
        //}
        //private string ReportPath
        //{
        //    get
        //    {
        //        var url = "\\ReportGenerator\\";
        //        return url;
        //    }
        //}
    }
}

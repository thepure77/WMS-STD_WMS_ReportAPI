using AspNetCore.Reporting;
using BinbalanceBusiness;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using PlanGRBusiness.Libs;
using ReportDataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using static ReportBusiness.ReportPerformance.SearchDetailModel;

namespace ReportBusiness.ReportPerformance
{
    public class ReportPerformanceService
    {
        #region printReportPerformance
        public dynamic printReportPerformance(ReportPerformanceViewModel data, string rootPath = "")
        {

            //var BB_DBContext = new BinbalanceDbContext();
            //var M_DBContext = new MasterDataDbContext();
            

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            //var olog = new logtxt();

            var result = new List<ReportPerformanceViewModel>();

            try
            {
                var Log_DBContext = new LogDbContext();

                var dateNow = DateTime.Now;
                var Current_Date = dateNow.ToString("dd/MM/yyyy");
                var Current_Time = dateNow.ToString("HH:mm:ss");

                var queryLog = Log_DBContext.sy_Logs_Request.AsQueryable();

                if (!string.IsNullOrEmpty(data.User_Id))
                {
                    queryLog = queryLog.Where(c => c.User_Id == data.User_Id);
                }

                if (!string.IsNullOrEmpty(data.Report_Date) && !string.IsNullOrEmpty(data.Report_Date_To))
                {
                    var dateStart = data.Report_Date.toBetweenDate();
                    var dateEnd = data.Report_Date_To.toBetweenDate();
                    queryLog = queryLog.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.Report_Date))
                {
                    var Report_Date_From = data.Report_Date.toBetweenDate();
                    queryLog = queryLog.Where(c => c.Create_Date >= Report_Date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.Report_Date_To))
                {
                    var Report_Date_To = data.Report_Date_To.toBetweenDate();
                    queryLog = queryLog.Where(c => c.Create_Date <= Report_Date_To.start);
                }
                
                var query = queryLog.OrderBy(o => o.Cancel_Date).ToList();

                foreach (var item in query)
                {
                    var resultItem = new ReportPerformanceViewModel();
                    //resultItem.UserGroup_Index = item.UserGroup_Index;
                    resultItem.Create_Date = item.Create_Date.ToString("dd-MM-yyyy");
                    resultItem.UserGroup_Id = item.UserGroup_Id;
                    resultItem.UserGroup_Name = item.UserGroup_Name;
                    resultItem.User_Id = item.User_Id;
                    //resultItem.User_Index = item.User_Index;
                    resultItem.First_Name = item.First_Name;
                    resultItem.Last_Name = item.Last_Name;
                    //resultItem.Menu_Index = item.Menu_Index;
                    //resultItem.MenuType_Index = item.MenuType_Index;
                    resultItem.Menu_Id = item.Menu_Id;
                    resultItem.Menu_Name = item.Menu_Name;
                    //resultItem.Sub_Menu_Index = item.Sub_Menu_Index;
                    //resultItem.Sub_MenuType_Index = item.Sub_MenuType_Index;
                    resultItem.Sub_Menu_Id = item.Sub_Menu_Id;
                    resultItem.Sub_Menu_Name = item.Sub_Menu_Name;
                    resultItem.Operations = item.Operations;
                    //resultItem.Ref_Document_Index = item.Ref_Document_Index;
                    resultItem.Ref_Document_No = item.Ref_Document_No;
                    resultItem.Request_URL = item.Request_URL;
                    resultItem.Request_Body = item.Request_Body;
                    resultItem.UDF_1 = item.UDF_1;
                    resultItem.UDF_2 = item.UDF_2;
                    resultItem.UDF_3 = item.UDF_3;
                    resultItem.UDF_4 = item.UDF_4;
                    resultItem.UDF_5 = item.UDF_5;
                    if (!string.IsNullOrEmpty(item.UDF_3))
                    {
                        resultItem.UDF_3 = item.UDF_3;
                    }
                    else
                    {
                        resultItem.UDF_3 = item.Create_Date.ToString("HH:mm");
                    }
                    if(!string.IsNullOrEmpty(item.UDF_3) && !string.IsNullOrEmpty(item.UDF_5))
                    {
                        TimeSpan time1 = TimeSpan.Parse(item.UDF_3);
                        TimeSpan time2 = TimeSpan.Parse(item.UDF_5);

                        DateTime startTime = DateTime.ParseExact(item.UDF_2 + " " + time1, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture);
                        DateTime endTime = DateTime.ParseExact(item.UDF_4 + " " + time2, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture);
                        TimeSpan span = endTime.Subtract(startTime);
                        resultItem.TotalTime = span.TotalMinutes.ToString();
                    }
                    else
                    {
                        resultItem.TotalTime = "0";
                    }
                        

                    result.Add(resultItem);
                }

                //result.Add(new ReportPerformanceViewModel() { Create_Date = "20/12/2021",User_Id="001",User_Name="Admin",First_Name="Admin",Last_Name="Admi",Menu_Name= "จัดการสินค้า ขาออก",
                //    Sub_Menu_Name= "ใบขอโอนสินค้า",UDF_2="20/12/2021",UDF_3="14:00"
                //});
                //result.Add(new ReportPerformanceViewModel(){Create_Date = "20/12/2021",User_Id = "001",User_Name = "Admin",First_Name = "Admin",Last_Name = "Admi",Menu_Name = "จัดการสินค้า ขาออก",Sub_Menu_Name = "ใบขอโอนสินค้า",UDF_2 = "21/12/2021",UDF_3 = "10:00"
                //});



                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportPerformance");
                //var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSpaceUtilization");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "ReportPerformance" + DateTime.Now.ToString("yyyyMMddHHmmss");

                var renderedBytes = report.Execute(RenderType.Pdf);

                Utils objReport = new Utils();
                fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".pdf", rootPath);
                var saveLocation = objReport.PhysicalPath(fileName + ".pdf", rootPath);
                return saveLocation;


            }
            catch (Exception ex)
            {
                //olog.logging("ReportPerformance", ex.Message);
                throw ex;
            }
        }
        #endregion

        public string ExportExcel(ReportPerformanceViewModel data, string rootPath = "")
        {

            var M_DBContext = new MasterDataDbContext();
            var Log_DBContext = new LogDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            //var olog = new logtxt();
            var result = new List<ReportPerformanceViewModel>();

            try
            {
                var queryLog = Log_DBContext.sy_Logs_Request.AsQueryable();

                if (!string.IsNullOrEmpty(data.User_Id))
                {
                    queryLog = queryLog.Where(c => c.User_Id == data.User_Id);
                }

                if (!string.IsNullOrEmpty(data.Report_Date) && !string.IsNullOrEmpty(data.Report_Date_To))
                {
                    var dateStart = data.Report_Date.toBetweenDate();
                    var dateEnd = data.Report_Date_To.toBetweenDate();
                    queryLog = queryLog.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.Report_Date))
                {
                    var Report_Date_From = data.Report_Date.toBetweenDate();
                    queryLog = queryLog.Where(c => c.Create_Date >= Report_Date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.Report_Date_To))
                {
                    var Report_Date_To = data.Report_Date_To.toBetweenDate();
                    queryLog = queryLog.Where(c => c.Create_Date <= Report_Date_To.start);
                }

                var query = queryLog.OrderBy(o => o.Cancel_Date).ToList();

                foreach (var item in query)
                {
                    var resultItem = new ReportPerformanceViewModel();
                    resultItem.Create_Date = item.Create_Date.ToString("dd-MM-yyyy");
                    resultItem.UserGroup_Id = item.UserGroup_Id;
                    resultItem.UserGroup_Name = item.UserGroup_Name;
                    resultItem.User_Id = item.User_Id;
                    //resultItem.User_Index = item.User_Index;
                    resultItem.First_Name = item.First_Name;
                    resultItem.Last_Name = item.Last_Name;
                    //resultItem.Menu_Index = item.Menu_Index;
                    //resultItem.MenuType_Index = item.MenuType_Index;
                    resultItem.Menu_Id = item.Menu_Id;
                    resultItem.Menu_Name = item.Menu_Name;
                    //resultItem.Sub_Menu_Index = item.Sub_Menu_Index;
                    //resultItem.Sub_MenuType_Index = item.Sub_MenuType_Index;
                    resultItem.Sub_Menu_Id = item.Sub_Menu_Id;
                    resultItem.Sub_Menu_Name = item.Sub_Menu_Name;
                    resultItem.Operations = item.Operations;
                    //resultItem.Ref_Document_Index = item.Ref_Document_Index;
                    resultItem.Ref_Document_No = item.Ref_Document_No;
                    resultItem.Request_URL = item.Request_URL;
                    resultItem.Request_Body = item.Request_Body;
                    resultItem.UDF_1 = item.UDF_1;
                    resultItem.UDF_2 = item.UDF_2;
                    resultItem.UDF_3 = item.UDF_3;
                    resultItem.UDF_4 = item.UDF_4;
                    resultItem.UDF_5 = item.UDF_5;
                    if (!string.IsNullOrEmpty(item.UDF_3))
                    {
                        resultItem.UDF_3 = item.UDF_3;
                    }
                    else
                    {
                        resultItem.UDF_3 = item.Create_Date.ToString("HH:mm");
                    }
                    if (!string.IsNullOrEmpty(item.UDF_3) && !string.IsNullOrEmpty(item.UDF_5))
                    {
                        TimeSpan time1 = TimeSpan.Parse(item.UDF_3);
                        TimeSpan time2 = TimeSpan.Parse(item.UDF_5);

                        DateTime startTime = DateTime.ParseExact(item.UDF_2 + " " + time1, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture);
                        DateTime endTime = DateTime.ParseExact(item.UDF_4 + " " + time2, "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture);
                        TimeSpan span = endTime.Subtract(startTime);
                        resultItem.TotalTime = span.TotalMinutes.ToString();
                    }
                    else
                    {
                        resultItem.TotalTime = "0";
                    }

                    result.Add(resultItem);
                }



                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportPerformance");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "ReportPerformance" + DateTime.Now.ToString("yyyyMMddHHmmss");

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

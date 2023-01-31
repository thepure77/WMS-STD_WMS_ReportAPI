using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using PlanGRBusiness.Libs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportBusiness.Report13
{
    public class Report13Service
    {

        #region printReport13
        public dynamic printReport13(Report13ViewModel data, string rootPath = "")
        {

            var BB_DBContext = new BinbalanceDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report13ViewModel>();

            try
            {

                var queryCC = BB_DBContext.View_RPT13_CycleCountDetail.AsQueryable();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryCC = queryCC.Where(c => c.Product_Id == data.product_Id);
                }

                if (!string.IsNullOrEmpty(data.cycleCount_date) && !string.IsNullOrEmpty(data.cycleCount_date_To))
                {
                    var dateStart = data.cycleCount_date.toBetweenDate();
                    var dateEnd = data.cycleCount_date_To.toBetweenDate();
                    queryCC = queryCC.Where(c => c.CycleCount_Date >= dateStart.start && c.CycleCount_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.cycleCount_date))
                {
                    var cycleCount_date_From = data.cycleCount_date.toBetweenDate();
                    queryCC = queryCC.Where(c => c.CycleCount_Date >= cycleCount_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.cycleCount_date_To))
                {
                    var cycleCount_date_To = data.cycleCount_date_To.toBetweenDate();
                    queryCC = queryCC.Where(c => c.CycleCount_Date <= cycleCount_date_To.start);
                }

                var queryRPT_CC = queryCC.ToList();
                if (queryRPT_CC.Count == 0)
                {
                    var resultItem = new Report13ViewModel();
                    var startDate = DateTime.ParseExact(data.cycleCount_date.Substring(0, 8), "yyyyMMdd",
                   System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.cycleCount_date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.checkQuery = true;
                    resultItem.timeNow = DateTime.Now.ToString("HH:mm");
                    resultItem.qty_Count = 0;
                    resultItem.qty_Bal = 0;
                    resultItem.cycleCount_date = startDate;
                    resultItem.cycleCount_date_To = endDate;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in queryRPT_CC)
                    {

                        var resultItem = new Report13ViewModel();

                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.qty_Count = item.Qty_Count;
                        resultItem.qty_Bal = item.Qty_Bal;
                        if (item.Qty_Bal != 0)
                        {
                            var percenResult = ((item.Qty_Count / item.Qty_Bal) * 130);
                            resultItem.date_Percen = percenResult;
                        }
                        else
                        {
                            resultItem.date_Percen = 0;
                        }

                        var startDate = DateTime.ParseExact(data.cycleCount_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.cycleCount_date_To.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.cycleCount_date = startDate;
                        resultItem.cycleCount_date_To = endDate;
                        resultItem.timeNow = DateTime.Now.ToString("HH:mm");
                        result.Add(resultItem);
                    }
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report13\\Report13.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report13");
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

                throw ex;
            }
        }
        #endregion

        public string ExportExcel(Report13ViewModel data, string rootPath = "")
        {
            var BB_DBContext = new BinbalanceDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report13ViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("Report13");

            try
            {

                var queryCC = BB_DBContext.View_RPT13_CycleCountDetail.AsQueryable();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryCC = queryCC.Where(c => c.Product_Id == data.product_Id);
                }

                if (!string.IsNullOrEmpty(data.cycleCount_date) && !string.IsNullOrEmpty(data.cycleCount_date_To))
                {
                    var dateStart = data.cycleCount_date.toBetweenDate();
                    var dateEnd = data.cycleCount_date_To.toBetweenDate();
                    queryCC = queryCC.Where(c => c.CycleCount_Date >= dateStart.start && c.CycleCount_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.cycleCount_date))
                {
                    var cycleCount_date_From = data.cycleCount_date.toBetweenDate();
                    queryCC = queryCC.Where(c => c.CycleCount_Date >= cycleCount_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.cycleCount_date_To))
                {
                    var cycleCount_date_To = data.cycleCount_date_To.toBetweenDate();
                    queryCC = queryCC.Where(c => c.CycleCount_Date <= cycleCount_date_To.start);
                }

                var queryRPT_CC = queryCC.ToList();
                if (queryRPT_CC.Count == 0)
                {
                    var resultItem = new Report13ViewModel();
                    var startDate = DateTime.ParseExact(data.cycleCount_date.Substring(0, 8), "yyyyMMdd",
                   System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.cycleCount_date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.checkQuery = true;
                    resultItem.timeNow = DateTime.Now.ToString("HH:mm");
                    resultItem.qty_Count = 0;
                    resultItem.qty_Bal = 0;
                    resultItem.cycleCount_date = startDate;
                    resultItem.cycleCount_date_To = endDate;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in queryRPT_CC)
                    {

                        var resultItem = new Report13ViewModel();

                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.qty_Count = item.Qty_Count;
                        resultItem.qty_Bal = item.Qty_Bal;
                        if (item.Qty_Bal != 0)
                        {
                            var percenResult = ((item.Qty_Count / item.Qty_Bal) * 130);
                            resultItem.date_Percen = percenResult;
                        }
                        else
                        {
                            resultItem.date_Percen = 0;
                        }

                        var startDate = DateTime.ParseExact(data.cycleCount_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.cycleCount_date_To.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.cycleCount_date = startDate;
                        resultItem.cycleCount_date_To = endDate;
                        resultItem.timeNow = DateTime.Now.ToString("HH:mm");
                        result.Add(resultItem);
                    }
                }


                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "tmpReport";

                var renderedBytes = report.Execute(RenderType.Excel);
                fullPath = saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);


                var saveLocation = rootPath + fullPath;
                //File.Delete(saveLocation);
                //ExcelRefresh(reportPath);
                return saveLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string saveReport(byte[] file, string name, string rootPath)
        {
            var saveLocation = PhysicalPath(name, rootPath);
            FileStream fs = new FileStream(saveLocation, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            try
            {
                try
                {
                    bw.Write(file);
                }
                finally
                {
                    fs.Close();
                    bw.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return VirtualPath(name);
        }

        public string PhysicalPath(string name, string rootPath)
        {
            var filename = name;
            var vPath = ReportPath;
            var path = rootPath + vPath;
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            var saveLocation = System.IO.Path.Combine(path, filename);
            return saveLocation;
        }
        public string VirtualPath(string name)
        {
            var filename = name;
            var vPath = ReportPath;
            vPath = vPath.Replace("~", "");
            return vPath + filename;
        }
        private string ReportPath
        {
            get
            {
                var url = "\\ReportGenerator\\";
                return url;
            }
        }

    }
}

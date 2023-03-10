using BinBalanceBusiness;
using AspNetCore.Reporting;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Library;
using PlanGRBusiness.Libs;
using Microsoft.EntityFrameworkCore;
using Common.Utils;
using Microsoft.AspNetCore.Http;
using ReportBusiness.Report18;
using System.Globalization;
using System.IO;

namespace ReportBusiness.Report19
{
    public class Report19Service
    {

        #region report19
        public string printReport19(Report19ViewModel data, string rootPath = "")
        {

            var BB_DBContext = new BinbalanceDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var queryBB = BB_DBContext.View_RPT19_BinBalance.AsQueryable();
                var result = new List<Report19ViewModel>();
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryBB = queryBB.Where(c => c.Product_Id.Contains(data.product_Id));
                }

                if (!string.IsNullOrEmpty(data.goodsReceive_date) && !string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var dateStart = data.goodsReceive_date.toBetweenDate();
                    var dateEnd = data.goodsReceive_date_To.toBetweenDate();
                    queryBB = queryBB.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date))
                {
                    var goodsReceive_date_From = data.goodsReceive_date.toBetweenDate();
                    queryBB = queryBB.Where(c => c.GoodsReceive_Date >= goodsReceive_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var goodsReceive_date_To = data.goodsReceive_date_To.toBetweenDate();
                    queryBB = queryBB.Where(c => c.GoodsReceive_Date <= goodsReceive_date_To.start);
                }

                var query = queryBB.ToList();


                if (query.Count == 0)
                {
                    var resultItem = new Report19ViewModel();

                    var startDate = DateTime.ParseExact(data.goodsReceive_date.Substring(0, 8), "yyyyMMdd",
                   System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.goodsReceive_date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.goodsReceive_date = startDate;
                    resultItem.goodsReceive_date_To = endDate;
                    resultItem.checkQuery = true;


                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {

                        string date = item.GoodsReceive_Date.toString();
                        string GRDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);


                        var resultItem = new Report19ViewModel();

                        resultItem.goodsReceive_Date = GRDate;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.Age = item.Age;

                        var startDate = DateTime.ParseExact(data.goodsReceive_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.goodsReceive_date_To.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.goodsReceive_date = startDate;
                        resultItem.goodsReceive_date_To = endDate;


                        result.Add(resultItem);
                    }
                    result.ToList();
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report19\\Report19.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report19");
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

        public string ExportExcel(Report19ViewModel data, string rootPath = "")
        {

            var BB_DBContext = new BinbalanceDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report19ViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("Report19");

            try
            {
                var queryBB = BB_DBContext.View_RPT19_BinBalance.AsQueryable();
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryBB = queryBB.Where(c => c.Product_Id.Contains(data.product_Id));
                }

                if (!string.IsNullOrEmpty(data.goodsReceive_date) && !string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var dateStart = data.goodsReceive_date.toBetweenDate();
                    var dateEnd = data.goodsReceive_date_To.toBetweenDate();
                    queryBB = queryBB.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date))
                {
                    var goodsReceive_date_From = data.goodsReceive_date.toBetweenDate();
                    queryBB = queryBB.Where(c => c.GoodsReceive_Date >= goodsReceive_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var goodsReceive_date_To = data.goodsReceive_date_To.toBetweenDate();
                    queryBB = queryBB.Where(c => c.GoodsReceive_Date <= goodsReceive_date_To.start);
                }

                var query = queryBB.ToList();


                if (query.Count == 0)
                {
                    var resultItem = new Report19ViewModel();

                    var startDate = DateTime.ParseExact(data.goodsReceive_date.Substring(0, 8), "yyyyMMdd",
                   System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.goodsReceive_date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.goodsReceive_date = startDate;
                    resultItem.goodsReceive_date_To = endDate;
                    resultItem.checkQuery = true;


                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {

                        string date = item.GoodsReceive_Date.toString();
                        string GRDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);


                        var resultItem = new Report19ViewModel();

                        resultItem.goodsReceive_Date = GRDate;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.Age = item.Age;

                        var startDate = DateTime.ParseExact(data.goodsReceive_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.goodsReceive_date_To.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.goodsReceive_date = startDate;
                        resultItem.goodsReceive_date_To = endDate;


                        result.Add(resultItem);
                    }
                    result.ToList();
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

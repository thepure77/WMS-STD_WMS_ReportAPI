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

namespace ReportBusiness.Report12
{
    public class Report12Service
    {

        #region printReport12
        public dynamic printReport12(Report12ViewModel data, string rootPath = "")
        {
            var BC_DB = new BinbalanceDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report12ViewModel>();

            try
            {

                var queryBC = BC_DB.wm_BinCard.AsQueryable();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryBC = queryBC.Where(c => c.Product_Id == data.product_Id);
                }
                var dateStart = data.date.toBetweenDate();
                var dateEnd = data.date.toBetweenDate();
                var queryRPT_BC = queryBC.ToList();

                 var queryBinCard = queryRPT_BC.Where(c => c.BinCard_Date <= dateEnd.end).GroupBy(c => new
                 {
                     c.Product_Index,
                     c.Product_Id,
                     c.Product_Name,
                     c.Location_Index,
                     c.Location_Id,
                     c.Location_Name
                 })
                .Select(c => new
                {
                    c.Key.Product_Index,
                    c.Key.Product_Id,
                    c.Key.Product_Name,
                    c.Key.Location_Index,
                    c.Key.Location_Id,
                    c.Key.Location_Name,
                    BinCard_QtyIn = c.Sum(s => s.BinCard_QtyIn),
                    BinCard_QtyOut = c.Sum(s => s.BinCard_QtyOut),
                    BinCard_QtySign = c.Sum(s => s.BinCard_QtySign)
                }).ToList();
                var queryBinC = queryBinCard.Where(c => c.BinCard_QtySign > 0).ToList();
                var queryBinCardCount = queryBinC.GroupBy(g => new
                {
                    g.Product_Index,
                    g.Product_Id,
                    g.Product_Name
                }).Select(c => new
                {
                    c.Key.Product_Index,
                    c.Key.Product_Id,
                    c.Key.Product_Name,
                    CountLocUse = c.Select(s => s.Location_Index).Count(),
                }).ToList();

               string selectDate = DateTime.ParseExact(data.date.Substring(0, 8), "yyyyMMdd",
               System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);
                if (queryBinCardCount.Count == 0)
                {
                    var resultItem = new Report12ViewModel();
                    resultItem.date = selectDate;
                    resultItem.checkQuery = true;
                    resultItem.countUse = 0;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in queryBinCardCount)
                    {

                        var resultItem = new Report12ViewModel();

                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.countUse = item.CountLocUse;
                        resultItem.date = selectDate;



                        result.Add(resultItem);
                    }
                    result.ToList();
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report12\\Report12.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report12");
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

        public string ExportExcel(Report12ViewModel data, string rootPath = "")
        {
            var BC_DB = new BinbalanceDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report12ViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("Report12");

            try
            {


                var queryBC = BC_DB.wm_BinCard.AsQueryable();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryBC = queryBC.Where(c => c.Product_Id == data.product_Id);
                }
                var dateStart = data.date.toBetweenDate();
                var dateEnd = data.date.toBetweenDate();
                var queryRPT_BC = queryBC.ToList();

                var queryBinCard = queryRPT_BC.Where(c => c.BinCard_Date <= dateEnd.end).GroupBy(c => new
                {
                    c.Product_Index,
                    c.Product_Id,
                    c.Product_Name,
                    c.Location_Index,
                    c.Location_Id,
                    c.Location_Name
                })
               .Select(c => new
               {
                   c.Key.Product_Index,
                   c.Key.Product_Id,
                   c.Key.Product_Name,
                   c.Key.Location_Index,
                   c.Key.Location_Id,
                   c.Key.Location_Name,
                   BinCard_QtyIn = c.Sum(s => s.BinCard_QtyIn),
                   BinCard_QtyOut = c.Sum(s => s.BinCard_QtyOut),
                   BinCard_QtySign = c.Sum(s => s.BinCard_QtySign)
               }).ToList();
                var queryBinC = queryBinCard.Where(c => c.BinCard_QtySign > 0).ToList();
                var queryBinCardCount = queryBinC.GroupBy(g => new
                {
                    g.Product_Index,
                    g.Product_Id,
                    g.Product_Name
                }).Select(c => new
                {
                    c.Key.Product_Index,
                    c.Key.Product_Id,
                    c.Key.Product_Name,
                    CountLocUse = c.Select(s => s.Location_Index).Count(),
                }).ToList();

                string selectDate = DateTime.ParseExact(data.date.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);
                if (queryBinCardCount.Count == 0)
                {
                    var resultItem = new Report12ViewModel();
                    resultItem.date = selectDate;
                    resultItem.checkQuery = true;
                    resultItem.countUse = 0;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in queryBinCardCount)
                    {

                        var resultItem = new Report12ViewModel();

                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.countUse = item.CountLocUse;
                        resultItem.date = selectDate;



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

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

namespace ReportBusiness.Report14
{
    public class Report14Service
    {

        #region printReport14
        public dynamic printReport14(Report14ViewModel data, string rootPath = "")
        {
            var MS_DBContext = new MasterDataDbContext();
            var BB_DBContext = new BinbalanceDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report14ViewModel>();

            try
            {
                var queryBB = BB_DBContext.View_RPT14_BinBalance.AsQueryable();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryBB = queryBB.Where(c => c.Product_Id == data.product_Id);
                }
                if (!string.IsNullOrEmpty(data.goodsReceive_date) && !string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var dateStart = data.goodsReceive_date.toBetweenDate();
                    var dateEnd = data.goodsReceive_date_To.toBetweenDate();
                    queryBB = queryBB.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date))
                {
                    var GoodsReceive_date_From = data.goodsReceive_date.toBetweenDate();
                    queryBB = queryBB.Where(c => c.GoodsReceive_Date >= GoodsReceive_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var GoodsReceive_date_To = data.goodsReceive_date.toBetweenDate();
                    queryBB = queryBB.Where(c => c.GoodsReceive_Date <= GoodsReceive_date_To.start);
                }

                string startDate = data.goodsReceive_date;
                string GRDateStart = DateTime.ParseExact(startDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                string endDate = data.goodsReceive_date_To;
                string GRDateEnd = DateTime.ParseExact(endDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                var query = queryBB.ToList();
                if (query.Count == 0)
                {
                    var resultItem = new Report14ViewModel();
                    resultItem.checkQuery = true;
                    resultItem.goodsReceive_date = GRDateStart;
                    resultItem.goodsReceive_date_To = GRDateEnd;
                    resultItem.dateToday = DateTime.Now.ToString("HH:mm");
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {


                        var resultItem = new Report14ViewModel();
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.binBalance_QtyBal_UU = item.BinBalance_QtyBal_UU;
                        resultItem.binBalance_QtyBal_QI = item.BinBalance_QtyBal_QI;
                        resultItem.binBalance_QtyReserve = item.BinBalance_QtyReserve;
                        resultItem.sumUUandQI = (resultItem.binBalance_QtyBal_UU) + (resultItem.binBalance_QtyBal_QI);
                        resultItem.stockRO = (resultItem.sumUUandQI) - (resultItem.binBalance_QtyReserve);
                        resultItem.goodsReceive_Date = item.GoodsReceive_Date;
                        resultItem.percentageStock = resultItem.binBalance_QtyReserve == 0 && resultItem.sumUUandQI == 0 ? 0 : (resultItem.binBalance_QtyReserve / resultItem.sumUUandQI * 140);
                        resultItem.goodsReceive_date = GRDateStart;
                        resultItem.goodsReceive_date_To = GRDateEnd;
                        resultItem.dateToday = DateTime.Now.ToString("HH:mm");



                        result.Add(resultItem);
                    }
                    result.ToList();

                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report14\\Report14.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report14");
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


        public string ExportExcel(Report14ViewModel data, string rootPath = "")
        {
            var MS_DBContext = new MasterDataDbContext();
            var BB_DBContext = new BinbalanceDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report14ViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("Report14");

            try
            {



                var queryBB = BB_DBContext.View_RPT14_BinBalance.AsQueryable();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryBB = queryBB.Where(c => c.Product_Id == data.product_Id);
                }
                if (!string.IsNullOrEmpty(data.goodsReceive_date) && !string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var dateStart = data.goodsReceive_date.toBetweenDate();
                    var dateEnd = data.goodsReceive_date_To.toBetweenDate();
                    queryBB = queryBB.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date))
                {
                    var GoodsReceive_date_From = data.goodsReceive_date.toBetweenDate();
                    queryBB = queryBB.Where(c => c.GoodsReceive_Date >= GoodsReceive_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var GoodsReceive_date_To = data.goodsReceive_date.toBetweenDate();
                    queryBB = queryBB.Where(c => c.GoodsReceive_Date <= GoodsReceive_date_To.start);
                }

                string startDate = data.goodsReceive_date;
                string GRDateStart = DateTime.ParseExact(startDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                string endDate = data.goodsReceive_date_To;
                string GRDateEnd = DateTime.ParseExact(endDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                var query = queryBB.ToList();
                if (query.Count == 0)
                {
                    var resultItem = new Report14ViewModel();
                    resultItem.checkQuery = true;
                    resultItem.goodsReceive_date = GRDateStart;
                    resultItem.goodsReceive_date_To = GRDateEnd;
                    resultItem.dateToday = DateTime.Now.ToString("HH:mm");
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {


                        var resultItem = new Report14ViewModel();
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.binBalance_QtyBal_UU = item.BinBalance_QtyBal_UU;
                        resultItem.binBalance_QtyBal_QI = item.BinBalance_QtyBal_QI;
                        resultItem.binBalance_QtyReserve = item.BinBalance_QtyReserve;
                        resultItem.sumUUandQI = (resultItem.binBalance_QtyBal_UU) + (resultItem.binBalance_QtyBal_QI);
                        resultItem.stockRO = (resultItem.sumUUandQI) - (resultItem.binBalance_QtyReserve);
                        resultItem.goodsReceive_Date = item.GoodsReceive_Date;
                        resultItem.percentageStock = resultItem.binBalance_QtyReserve == 0 && resultItem.sumUUandQI == 0 ? 0 : (resultItem.binBalance_QtyReserve / resultItem.sumUUandQI * 140);
                        resultItem.goodsReceive_date = GRDateStart;
                        resultItem.goodsReceive_date_To = GRDateEnd;
                        resultItem.dateToday = DateTime.Now.ToString("HH:mm");



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

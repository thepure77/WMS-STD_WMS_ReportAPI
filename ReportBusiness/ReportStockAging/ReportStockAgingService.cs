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
using MasterDataBusiness.ViewModels;
using System.IO;

namespace ReportBusiness.ReportStockAging
{
    public class ReportStockAgingService
    {

        #region ReportStockAging
        public string printReportStockAging(ReportStockAgingViewModel data, string rootPath = "")
        {

            var BB_DBContext = new BinbalanceDbContext();
            var MS_DBContext = new MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var queryBB = BB_DBContext.View_RPT_StockAging.AsQueryable();
                var queryMS = MS_DBContext.MS_Product.AsQueryable();

                var result = new List<ReportStockAgingViewModel>();
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryBB = queryBB.Where(c => c.Product_Id.Contains(data.product_Id));
                }
                if (!string.IsNullOrEmpty(data.owner_Id))
                {
                    queryBB = queryBB.Where(c => c.Owner_Id == data.owner_Id);
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
                var count = 1;

                long DateTicks = DateTime.Now.Ticks;
                int day = new DateTime(DateTicks).Day;
                int month = new DateTime(DateTicks).Month;
                int year = new DateTime(DateTicks).Year;
                var time = DateTime.Now.ToString("HH:mm");


                var thaiMonth = "";
                switch (month)
                {
                    case 1:
                        thaiMonth = "มกราคม";
                        break;
                    case 2:
                        thaiMonth = "กุมภาพันธ์";
                        break;
                    case 3:
                        thaiMonth = "มีนาคม";
                        break;
                    case 4:
                        thaiMonth = "เมษายน";
                        break;
                    case 5:
                        thaiMonth = "พฤษภาคม";
                        break;
                    case 6:
                        thaiMonth = "มิถุนายน";
                        break;
                    case 7:
                        thaiMonth = "กรกฎาคม";
                        break;
                    case 8:
                        thaiMonth = "สิงหาคม";
                        break;
                    case 9:
                        thaiMonth = "กันยายน";
                        break;
                    case 10:
                        thaiMonth = "ตุลาคม";
                        break;
                    case 11:
                        thaiMonth = "พฤศจิกายน";
                        break;
                    case 12:
                        thaiMonth = "ธันวาคม";
                        break;
                }


                if (query.Count == 0)
                {
                    var resultItem = new ReportStockAgingViewModel();

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


                        var resultItem = new ReportStockAgingViewModel();

                        resultItem.goodsReceive_Date = GRDate;
                        resultItem.owner_Id = item.Owner_Id;
                        resultItem.owner_Name = item.Owner_Name;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.productType_Name = queryMS?.FirstOrDefault(c=>c.Product_Index == item.Product_Index)?.ProductType_Name;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.date_Print = "ข้อมูล ณ วันที่ " + day.ToString() + " " + thaiMonth + " " + year.ToString() + " เวลา " + time + " น.";
                        resultItem.age = item.Age;
                        resultItem.rowCount = count;
                        resultItem.sumCount = query.Count();
                        count++;
                        result.Add(resultItem);
                    }
                    result.ToList();
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\ReportStockAging\\ReportStockAging.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportStockAging");
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

        #region autoSearchOwner
        public List<ItemListViewModel> autoSearchOwner(ItemListViewModel data)
        {
            try
            {

                using (var context = new BinbalanceDbContext())
                {


                    var query = context.View_RPT_StockAging.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key) && data.key != "-")
                    {
                        query = query.Where(c => c.Owner_Id.Contains(data.key)
                        || c.Owner_Name.Contains(data.key));

                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Owner_Index, c.Owner_Id, c.Owner_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Owner_Index,
                            id = item.Owner_Id,
                            name = item.Owner_Name,

                        };

                        items.Add(resultItem);
                    }

                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public string ExportExcel(ReportStockAgingViewModel data, string rootPath = "")
        {
            var BB_DBContext = new BinbalanceDbContext();
            var MS_DBContext = new MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<ReportStockAgingViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportStockAging");

            try
            {

                var queryBB = BB_DBContext.View_RPT_StockAging.AsQueryable();
                var queryMS = MS_DBContext.MS_Product.AsQueryable();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryBB = queryBB.Where(c => c.Product_Id.Contains(data.product_Id));
                }
                if (!string.IsNullOrEmpty(data.owner_Name))
                {
                    queryBB = queryBB.Where(c => c.Owner_Name.Contains(data.owner_Name));
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
                var count = 1;

                long DateTicks = DateTime.Now.Ticks;
                int day = new DateTime(DateTicks).Day;
                int month = new DateTime(DateTicks).Month;
                int year = new DateTime(DateTicks).Year;
                var time = DateTime.Now.ToString("HH:mm");


                var thaiMonth = "";
                switch (month)
                {
                    case 1:
                        thaiMonth = "มกราคม";
                        break;
                    case 2:
                        thaiMonth = "กุมภาพันธ์";
                        break;
                    case 3:
                        thaiMonth = "มีนาคม";
                        break;
                    case 4:
                        thaiMonth = "เมษายน";
                        break;
                    case 5:
                        thaiMonth = "พฤษภาคม";
                        break;
                    case 6:
                        thaiMonth = "มิถุนายน";
                        break;
                    case 7:
                        thaiMonth = "กรกฎาคม";
                        break;
                    case 8:
                        thaiMonth = "สิงหาคม";
                        break;
                    case 9:
                        thaiMonth = "กันยายน";
                        break;
                    case 10:
                        thaiMonth = "ตุลาคม";
                        break;
                    case 11:
                        thaiMonth = "พฤศจิกายน";
                        break;
                    case 12:
                        thaiMonth = "ธันวาคม";
                        break;
                }


                if (query.Count == 0)
                {
                    var resultItem = new ReportStockAgingViewModel();

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


                        var resultItem = new ReportStockAgingViewModel();

                        resultItem.goodsReceive_Date = GRDate;
                        resultItem.owner_Id = item.Owner_Id;
                        resultItem.owner_Name = item.Owner_Name;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.productType_Name = queryMS?.FirstOrDefault(c => c.Product_Index == item.Product_Index)?.ProductType_Name;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.date_Print = "ข้อมูล ณ วันที่ " + day.ToString() + " " + thaiMonth + " " + year.ToString() + " เวลา " + time + " น.";
                        resultItem.age = item.Age;
                        resultItem.rowCount = count;
                        resultItem.sumCount = query.Count();
                        count++;
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

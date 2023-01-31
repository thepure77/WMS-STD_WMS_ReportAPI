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

namespace ReportBusiness.Report2
{
    public class Report2Service
    {

        #region printReport2
        public dynamic printReport2(Report2ViewModel data, string rootPath = "")
        {
            var GR_DBContext = new GRDbContext();
            var BB_DBContext = new BinbalanceDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report2ViewModel>();

            try
            {
                var queryGR = GR_DBContext.View_RPT_GR_GRI_GRIL.AsQueryable();
                var queryBB = BB_DBContext.View_RPT_BinBalance.AsQueryable();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryGR = queryGR.Where(c => c.Product_Id == data.product_Id);
                }

                if (!string.IsNullOrEmpty(data.goodsReceive_date) && !string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var dateStart = data.goodsReceive_date.toBetweenDate();
                    var dateEnd = data.goodsReceive_date_To.toBetweenDate();
                    queryGR = queryGR.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date))
                {
                    var planGoodsReceive_date_From = data.goodsReceive_date.toBetweenDate();
                    queryGR = queryGR.Where(c => c.GoodsReceive_Date >= planGoodsReceive_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var planGoodsReceive_date_To = data.goodsReceive_date_To.toBetweenDate();
                    queryGR = queryGR.Where(c => c.GoodsReceive_Date <= planGoodsReceive_date_To.start);
                }

                var queryRPT_GR = queryGR.ToList();
                var queryRPT_BB = queryBB.ToList();

                var query = (from GR in queryRPT_GR
                             join BB in queryRPT_BB on GR.GoodsReceiveItemLocation_Index equals BB.GoodsReceiveItemLocation_Index into ps
                             from r in ps
                             where r.ItemStatus_Id == "UU" || r.ItemStatus_Id == "QI"
                             select new
                             {
                                 Gr = GR,
                                 Bin = r
                             }).ToList();
                if (query.Count == 0)
                {
                    var resultItem = new Report2ViewModel();
                    var startDate = DateTime.ParseExact(data.goodsReceive_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.goodsReceive_date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.checkQuery = true;
                    resultItem.qtyUU = 0;
                    resultItem.qtyQI = 0;
                    resultItem.sumqty = 0;
                    resultItem.goodsReceive_date = startDate;
                    resultItem.goodsReceive_date_To = endDate;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {

                        string date = item.Bin.GoodsReceive_Date.toString();
                        string GRDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new Report2ViewModel();

                        resultItem.goodsReceive_No = item.Bin.GoodsReceive_No;
                        resultItem.goodsReceive_Date = GRDate;
                        resultItem.product_Id = item.Bin.Product_Id;
                        resultItem.product_Name = item.Bin.Product_Name;
                        if (item.Bin.ItemStatus_Id == "UU")
                        {
                            resultItem.qtyUU = item.Bin.BinBalance_QtyBal;
                            resultItem.qtyQI = 0;
                        }
                        if (item.Bin.ItemStatus_Id == "QI")
                        {
                            resultItem.qtyQI = item.Bin.BinBalance_QtyBal;
                            resultItem.qtyUU = 0;
                        }

                        resultItem.sumqty = resultItem.qtyUU + resultItem.qtyQI;
                        var startDate = DateTime.ParseExact(data.goodsReceive_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.goodsReceive_date_To.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.goodsReceive_date = startDate;
                        resultItem.goodsReceive_date_To = endDate;

                        result.Add(resultItem);
                    }
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report2\\Report2.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report2");
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

        public string ExportExcel(Report2ViewModel data, string rootPath = "")
        {
            var GR_DBContext = new GRDbContext();
            var BB_DBContext = new BinbalanceDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();


            try
            {

                var result = new List<Report2ViewModel>();
                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report2");

                var queryGR = GR_DBContext.View_RPT_GR_GRI_GRIL.AsQueryable();
                var queryBB = BB_DBContext.View_RPT_BinBalance.AsQueryable();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryGR = queryGR.Where(c => c.Product_Id == data.product_Id);
                }

                if (!string.IsNullOrEmpty(data.goodsReceive_date) && !string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var dateStart = data.goodsReceive_date.toBetweenDate();
                    var dateEnd = data.goodsReceive_date_To.toBetweenDate();
                    queryGR = queryGR.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date))
                {
                    var planGoodsReceive_date_From = data.goodsReceive_date.toBetweenDate();
                    queryGR = queryGR.Where(c => c.GoodsReceive_Date >= planGoodsReceive_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var planGoodsReceive_date_To = data.goodsReceive_date_To.toBetweenDate();
                    queryGR = queryGR.Where(c => c.GoodsReceive_Date <= planGoodsReceive_date_To.start);
                }

                var queryRPT_GR = queryGR.ToList();
                var queryRPT_BB = queryBB.ToList();

                var query = (from GR in queryRPT_GR
                             join BB in queryRPT_BB on GR.GoodsReceiveItemLocation_Index equals BB.GoodsReceiveItemLocation_Index into ps
                             from r in ps
                             where r.ItemStatus_Id == "UU" || r.ItemStatus_Id == "QI"
                             select new
                             {
                                 Gr = GR,
                                 Bin = r
                             }).ToList();
                if (query.Count == 0)
                {
                    var resultItem = new Report2ViewModel();
                    var startDate = DateTime.ParseExact(data.goodsReceive_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.goodsReceive_date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.checkQuery = true;
                    resultItem.qtyUU = 0;
                    resultItem.qtyQI = 0;
                    resultItem.sumqty = 0;
                    resultItem.goodsReceive_date = startDate;
                    resultItem.goodsReceive_date_To = endDate;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {

                        string date = item.Bin.GoodsReceive_Date.toString();
                        string GRDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new Report2ViewModel();

                        resultItem.goodsReceive_No = item.Bin.GoodsReceive_No;
                        resultItem.goodsReceive_Date = GRDate;
                        resultItem.product_Id = item.Bin.Product_Id;
                        resultItem.product_Name = item.Bin.Product_Name;
                        if (item.Bin.ItemStatus_Id == "UU")
                        {
                            resultItem.qtyUU = item.Bin.BinBalance_QtyBal;
                            resultItem.qtyQI = 0;
                        }
                        if (item.Bin.ItemStatus_Id == "QI")
                        {
                            resultItem.qtyQI = item.Bin.BinBalance_QtyBal;
                            resultItem.qtyUU = 0;
                        }

                        resultItem.sumqty = resultItem.qtyUU + resultItem.qtyQI;
                        var startDate = DateTime.ParseExact(data.goodsReceive_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.goodsReceive_date_To.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.goodsReceive_date = startDate;
                        resultItem.goodsReceive_date_To = endDate;

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

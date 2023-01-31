using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using MasterDataBusiness.ViewModels;
using PlanGRBusiness.Libs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportBusiness.ReportSummaryMaterialsStock
{
    public class ReportSummaryMaterialsStockService
    {

        #region printReportSummaryMaterialsStockOld
        //public dynamic printReportSummaryMaterialsStockOld(ReportSummaryMaterialsStockViewModel data, string rootPath = "")
        //{
        //    var GR_DBContext = new GRDbContext();
        //    var BB_DBContext = new BinbalanceDbContext();
        //    var Master_DBContext = new MasterDataDbContext();
        //    var culture = new System.Globalization.CultureInfo("en-US");
        //    String State = "Start";
        //    String msglog = "";
        //    var olog = new logtxt();

        //    var result = new List<ReportSummaryMaterialsStockViewModel>();

        //    try
        //    {
        //        var querrMaster = Master_DBContext.View_RPT_MS_SummaryMaterialsStock.AsQueryable();
        //        var queryGR = GR_DBContext.View_RPT_GR_SummaryMaterialsStock.AsQueryable();

        //        var queryBB = BB_DBContext.View_RPT_BinBalance_SummaryMaterialsStock.AsQueryable();

        //        if (!string.IsNullOrEmpty(data.product_Id))
        //        {
        //            queryGR = queryGR.Where(c => c.Product_Id == data.product_Id);
        //        }
        //        if (!string.IsNullOrEmpty(data.owner_Name))
        //        {
        //            queryBB = queryBB.Where(c => c.Owner_Name == data.owner_Name);
        //        }

        //        if (!string.IsNullOrEmpty(data.goodsReceive_date) && !string.IsNullOrEmpty(data.goodsReceive_date_To))
        //        {
        //            var dateStart = data.goodsReceive_date.toBetweenDate();
        //            var dateEnd = data.goodsReceive_date_To.toBetweenDate();
        //            queryGR = queryGR.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end);
        //        }
        //        else if (!string.IsNullOrEmpty(data.goodsReceive_date))
        //        {
        //            var planGoodsReceive_date_From = data.goodsReceive_date.toBetweenDate();
        //            queryGR = queryGR.Where(c => c.GoodsReceive_Date >= planGoodsReceive_date_From.start);
        //        }
        //        else if (!string.IsNullOrEmpty(data.goodsReceive_date_To))
        //        {
        //            var planGoodsReceive_date_To = data.goodsReceive_date_To.toBetweenDate();
        //            queryGR = queryGR.Where(c => c.GoodsReceive_Date <= planGoodsReceive_date_To.start);
        //        }

        //        var queryRPT_GR = queryGR.ToList();
        //        var queryRPT_BB = queryBB.ToList();
        //        var queryRPT_MS = querrMaster.ToList();

        //        var queryGR_BB = (from GR in queryRPT_GR
        //                          join BB in queryRPT_BB on GR.GoodsReceiveItemLocation_Index equals BB.GoodsReceiveItemLocation_Index into ps
        //                          from r in ps
        //                          where r.ItemStatus_Id == "UU" || r.ItemStatus_Id == "QI"
        //                          select new
        //                          {
        //                              Gr = GR,
        //                              Bin = r
        //                          }).ToList();

        //        var query = (from GR_BB in queryGR_BB
        //                     join MS in queryRPT_MS on GR_BB.Gr.Product_Index equals MS.Product_Index into ps
        //                     from r in ps
        //                     select new
        //                     {
        //                         Gr_Bb = GR_BB,
        //                         Master = r
        //                     }).ToList();
        //        if (query.Count == 0)
        //        {
        //            var resultItem = new ReportSummaryMaterialsStockViewModel();
        //            var startDate = DateTime.ParseExact(data.goodsReceive_date.Substring(0, 8), "yyyyMMdd",
        //            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //            var endDate = DateTime.ParseExact(data.goodsReceive_date_To.Substring(0, 8), "yyyyMMdd",
        //            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //            resultItem.checkQuery = true;
        //            resultItem.qtyUU = 0;
        //            resultItem.qtyQI = 0;
        //            resultItem.sumqty = 0;
        //            resultItem.goodsReceive_date = startDate;
        //            resultItem.goodsReceive_date_To = endDate;
        //            result.Add(resultItem);
        //        }
        //        else
        //        {
        //            foreach (var item in query)
        //            {

        //                string date = item.Gr_Bb.Bin.GoodsReceive_Date.toString();
        //                string GRDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                var resultItem = new ReportSummaryMaterialsStockViewModel();

        //                resultItem.goodsReceive_No = item.Gr_Bb.Bin.GoodsReceive_No;
        //                resultItem.goodsReceive_Date = GRDate;
        //                resultItem.product_Id = item.Gr_Bb.Bin.Product_Id;
        //                resultItem.product_Name = item.Gr_Bb.Bin.Product_Name;
        //                resultItem.owner_Name = item.Gr_Bb.Bin.Owner_Name;
        //                resultItem.owner_Id = item.Gr_Bb.Bin.Owner_Id;
        //                resultItem.productType_Name = item.Master.ProductType_Name;
        //                if (item.Gr_Bb.Bin.ItemStatus_Id == "UU")
        //                {
        //                    resultItem.qtyUU = item.Gr_Bb.Bin.BinBalance_QtyBal;
        //                    resultItem.qtyQI = 0;
        //                }
        //                if (item.Gr_Bb.Bin.ItemStatus_Id == "QI")
        //                {
        //                    resultItem.qtyQI = item.Gr_Bb.Bin.BinBalance_QtyBal;
        //                    resultItem.qtyUU = 0;
        //                }


        //                resultItem.sumqty = resultItem.qtyUU + resultItem.qtyQI;
        //                var startDate = DateTime.ParseExact(data.goodsReceive_date.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                var endDate = DateTime.ParseExact(data.goodsReceive_date_To.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                resultItem.goodsReceive_date = startDate;
        //                resultItem.goodsReceive_date_To = endDate;
        //                resultItem.count = query.Count;
        //                result.Add(resultItem);
        //            }
        //        }

        //        rootPath = rootPath.Replace("\\ReportAPI", "");
        //        //var reportPath = rootPath + "\\ReportBusiness\\ReportSummaryMaterialsStock\\ReportSummaryMaterialsStock.rdlc";
        //        var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSummaryMaterialsStock");
        //        LocalReport report = new LocalReport(reportPath);
        //        report.AddDataSource("DataSet1", result);

        //        System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        //        string fileName = "";
        //        string fullPath = "";
        //        fileName = "tmpReport" + DateTime.Now.ToString("yyyyMMddHHmmss");

        //        var renderedBytes = report.Execute(RenderType.Pdf);

        //        Utils objReport = new Utils();
        //        fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".pdf", rootPath);
        //        var saveLocation = objReport.PhysicalPath(fileName + ".pdf", rootPath);
        //        return saveLocation;


        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        #endregion

        #region printReportSummaryMaterialsStock
        public dynamic printReportSummaryMaterialsStock(ReportSummaryMaterialsStockViewModel data, string rootPath = "")
        {
            var GR_DBContext = new GRDbContext();
            //var BB_DBContext = new BinbalanceDbContext();
            var Master_DBContext = new MasterDataDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<ReportSummaryMaterialsStockViewModel>();

            try
            {
                var queryGR = GR_DBContext.View_RPT_GR_SummaryMaterialsStock.AsQueryable();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryGR = queryGR.Where(c => c.Product_Id == data.product_Id);
                }
                if (!string.IsNullOrEmpty(data.owner_Id))
                {
                    queryGR = queryGR.Where(c => c.Owner_Id == data.owner_Id);
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

                var query = (from GR in queryRPT_GR
                             join P in Master_DBContext.MS_Product.Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList() on GR.Product_Index equals P.Product_Index into ps
                             from P in ps
                             select new
                             {
                                 GR.GoodsReceive_Index,
                                 GR.GoodsReceiveItem_Index,
                                 GR.GoodsReceive_No,
                                 GR.GoodsReceive_Date,
                                 GR.Product_Index,
                                 GR.Product_Id,
                                 GR.Product_Name,
                                 P.ProductCategory_Id,
                                 P.ProductCategory_Name,
                                 P.Ref_No3,
                                 GR.Owner_Index,
                                 GR.Owner_Id,
                                 GR.Owner_Name,
                                 GR.TotalQty,
                                 GR.GRL_TotalQty,
                                 GR.Tag_TotalQty,
                                 GR.TotalReserve,
                                 GR.Ref_document_Index,
                                 GR.Ref_document_No,
                                 GR.Ref_documentitem_Index,
                                 GR.Suggest_Location_Index,
                                 GR.Suggest_Location_Id,
                                 GR.Suggest_Location_Name,
                             }).ToList();
                if (query.Count == 0)
                {
                    var resultItem = new ReportSummaryMaterialsStockViewModel();
                    var startDate = DateTime.ParseExact(data.goodsReceive_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.goodsReceive_date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.checkQuery = true;
                    resultItem.totalQty = 0;
                    resultItem.totalReserve = 0;
                    resultItem.sumqty = 0;
                    resultItem.goodsReceive_date = startDate;
                    resultItem.goodsReceive_date_To = endDate;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {

                        string date = item.GoodsReceive_Date.toString();
                        string GRDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new ReportSummaryMaterialsStockViewModel();

                        resultItem.goodsReceive_No = item.GoodsReceive_No;
                        resultItem.goodsReceive_Date = item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy");
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.productCategory_Id = item.ProductCategory_Id;
                        resultItem.productCategory_Name = item.ProductCategory_Name;
                        resultItem.ref_No3 = item.Ref_No3;
                        resultItem.owner_Id = item.Owner_Id;
                        resultItem.owner_Name = item.Owner_Name;
                        resultItem.totalQty = item.TotalQty;
                        resultItem.totalReserve = item.TotalReserve;
                        resultItem.grl_TotalQty = item.GRL_TotalQty;
                        resultItem.totalToPay = (item.GRL_TotalQty ?? 0) - (item.Tag_TotalQty ?? 0);
                        resultItem.ref_document_No = item.Ref_document_No;
                        resultItem.suggest_Location_Index = item.Suggest_Location_Index;
                        resultItem.suggest_Location_Id = item.Suggest_Location_Id;
                        resultItem.suggest_Location_Name = item.Suggest_Location_Name;

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
                //var reportPath = rootPath + "\\ReportBusiness\\ReportSummaryMaterialsStock\\ReportSummaryMaterialsStock.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSummaryMaterialsStock");
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

                using (var context = new GRDbContext())
                {


                    var query = context.View_RPT_GR_SummaryMaterialsStock.AsQueryable();

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

        public string ExportExcel(ReportSummaryMaterialsStockViewModel data, string rootPath = "")
        {
            var GR_DBContext = new GRDbContext();
            //var BB_DBContext = new BinbalanceDbContext();
            var Master_DBContext = new MasterDataDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<ReportSummaryMaterialsStockViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSummaryMaterialsStock");

            try
            {

                var queryGR = GR_DBContext.View_RPT_GR_SummaryMaterialsStock.AsQueryable();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryGR = queryGR.Where(c => c.Product_Id == data.product_Id);
                }
                if (!string.IsNullOrEmpty(data.owner_Id))
                {
                    queryGR = queryGR.Where(c => c.Owner_Id == data.owner_Id);
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

                var query = (from GR in queryRPT_GR
                             join P in Master_DBContext.MS_Product.Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList() on GR.Product_Index equals P.Product_Index into ps
                             from P in ps
                             select new
                             {
                                 GR.GoodsReceive_Index,
                                 GR.GoodsReceiveItem_Index,
                                 GR.GoodsReceive_No,
                                 GR.GoodsReceive_Date,
                                 GR.Product_Index,
                                 GR.Product_Id,
                                 GR.Product_Name,
                                 P.ProductCategory_Id,
                                 P.ProductCategory_Name,
                                 P.Ref_No3,
                                 GR.Owner_Index,
                                 GR.Owner_Id,
                                 GR.Owner_Name,
                                 GR.TotalQty,
                                 GR.GRL_TotalQty,
                                 GR.Tag_TotalQty,
                                 GR.TotalReserve,
                                 GR.Ref_document_Index,
                                 GR.Ref_document_No,
                                 GR.Ref_documentitem_Index,
                                 GR.Suggest_Location_Index,
                                 GR.Suggest_Location_Id,
                                 GR.Suggest_Location_Name,
                             }).ToList();
                if (query.Count == 0)
                {
                    var resultItem = new ReportSummaryMaterialsStockViewModel();
                    var startDate = DateTime.ParseExact(data.goodsReceive_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.goodsReceive_date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.checkQuery = true;
                    resultItem.totalQty = 0;
                    resultItem.totalReserve = 0;
                    resultItem.sumqty = 0;
                    resultItem.goodsReceive_date = startDate;
                    resultItem.goodsReceive_date_To = endDate;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {

                        string date = item.GoodsReceive_Date.toString();
                        string GRDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new ReportSummaryMaterialsStockViewModel();

                        resultItem.goodsReceive_No = item.GoodsReceive_No;
                        resultItem.goodsReceive_Date = item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy");
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.productCategory_Id = item.ProductCategory_Id;
                        resultItem.productCategory_Name = item.ProductCategory_Name;
                        resultItem.ref_No3 = item.Ref_No3;
                        resultItem.owner_Id = item.Owner_Id;
                        resultItem.owner_Name = item.Owner_Name;
                        resultItem.totalQty = item.TotalQty;
                        resultItem.totalReserve = item.TotalReserve;
                        resultItem.grl_TotalQty = item.GRL_TotalQty;
                        resultItem.totalToPay = (item.GRL_TotalQty ?? 0) - (item.Tag_TotalQty ?? 0);
                        resultItem.ref_document_No = item.Ref_document_No;
                        resultItem.suggest_Location_Index = item.Suggest_Location_Index;
                        resultItem.suggest_Location_Id = item.Suggest_Location_Id;
                        resultItem.suggest_Location_Name = item.Suggest_Location_Name;

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

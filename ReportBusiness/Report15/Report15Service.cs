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
using ReportBusiness.Report15;
using System.Globalization;
using ReportBusiness.ReportInventoryStock;
using MasterDataBusiness.ViewModels;
using System.IO;

namespace ReportBusiness.Report15
{
    public class Report15Service
    {

        #region report15Old
        public string printReport15Old(Report15ViewModel data, string rootPath = "")
        {

            var BC_DB = new BinbalanceDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {


                var queryGoods = BC_DB.wm_BinCard.AsQueryable();
                var queryNotGoods = BC_DB.wm_BinCard.AsQueryable();


                var result = new List<Report15ViewModel>();
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryGoods = queryGoods.Where(c => c.Product_Name == data.product_Id);
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryNotGoods = queryNotGoods.Where(c => c.Product_Name == data.product_Id);
                }



             var query1 =  queryGoods.Where(c => c.BinCard_Date <= data.binCard_date.toBetweenDate().start && c.ItemStatus_Id == "15")
                .GroupBy
                (c => new
                {
                    c.Product_Id,
                    c.Product_Name,
                    c.ProductConversion_Name,
                })
                .Select(c => new
                {
                    c.Key.Product_Id,
                    c.Key.Product_Name,
                    c.Key.ProductConversion_Name,
                    Qty = Convert.ToDecimal(c.Sum(s => s.BinCard_QtyIn) - c.Sum(s => s.BinCard_QtyOut)),
                    BinCard_QtyBal = Convert.ToDecimal(0.00),
                }).Distinct();

             var query2 = queryNotGoods.Where(c => c.BinCard_Date <= data.binCard_date.toBetweenDate().start && c.ItemStatus_Id != "15")
                 .GroupBy
                (c => new
                {
                    c.Product_Id,
                    c.Product_Name,
                    c.ProductConversion_Name,
                })
                .Select(c => new
                {
                    c.Key.Product_Id,
                    c.Key.Product_Name,
                    c.Key.ProductConversion_Name,
                    Qty = Convert.ToDecimal(0.00),
                    BinCard_QtyBal =  Convert.ToDecimal(c.Sum(s => s.BinCard_QtyIn) - c.Sum(s => s.BinCard_QtyOut)),
                }).Distinct();

                var query = query1.Union(query2).OrderBy(o => o.Product_Id).ToList();




                //.GroupBy
                //(c => new {
                //    c.Process_Index,
                //    c.Product_Id,
                //    c.Product_Name,
                //    c.ProductConversion_Name,
                //})
                //.Select(c => new {
                //    c.Key.Process_Index,
                //    c.Key.Product_Id,
                //    c.Key.Product_Name,
                //    c.Key.ProductConversion_Name,
                //    BinCard_QtyIn = c.Sum(s => s.BinCard_QtyIn),
                //    BinCard_QtyOut = c.Sum(s => s.BinCard_QtyOut),
                //    BinCard_QtyBal = c.Sum(s => s.BinCard_QtyIn) - c.Sum(s => s.BinCard_QtyOut),
                //}).ToList();




                //var query = queryGoods.Select(s => new
                //{
                //    s.Product_Index,
                //    s.Product_Id,
                //    s.Product_Name,
                //    s.ProductConversion_Name,

                //}).Union(queryNotGoods.Select(ss => new
                //{
                //    ss.Product_Index,
                //    ss.Product_Id,
                //    ss.Product_Name,
                //    ss.ProductConversion_Name,

                //})).GroupBy(g => new
                //{
                //    g.Product_Index,
                //    g.Product_Id,
                //    g.Product_Name,
                //    g.ProductConversion_Name,
                //}).Select(r => new
                //{
                //    r.Key.Product_Id,
                //    r.Key.Product_Name,
                //    r.Key.ProductConversion_Name,
                //}).ToList();

                string selectDate = DateTime.ParseExact(data.binCard_date.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                if (query.Count == 0)
                {
                    var resultItem = new Report15ViewModel();
                    resultItem.checkQuery = true;
                  
                    resultItem.binCard_Date = selectDate;
                    resultItem.binCard_QtyQI = 0;
                    resultItem.binCard_QtyUU = 0;
                    resultItem.sumBinCard_Qty = 0;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {
                        var resultItem = new Report15ViewModel();
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.binCard_QtyQI = item.BinCard_QtyBal;
                        resultItem.binCard_QtyUU = item.Qty;
                        resultItem.sumBinCard_Qty = resultItem.binCard_QtyUU + resultItem.binCard_QtyQI;
                        //resultItem.sumBinCard_Qty = item.BinCard_QtyBal;
                        resultItem.binCard_Date = selectDate;
                        result.Add(resultItem);
                    }
                    result.ToList();
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report15\\Report15.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report15");
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

        #region report15
        public string printReport15(Report15ViewModel data, string rootPath = "")
        {

            var BC_DB = new BinbalanceDbContext();
            var M_DB = new MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var queryBB = BC_DB.View_RPT_InventoryStock.AsQueryable();

                var result = new List<ReportInventoryStockViewModel>();
      
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryBB = queryBB.Where(c => c.Product_Id == data.product_Id);
                }

                if (!string.IsNullOrEmpty(data.owner_Id))
                {
                    queryBB = queryBB.Where(c => c.Owner_Id == data.owner_Id);
                }

                var query = (from BB in queryBB.ToList()
                             join PRDCAT in M_DB.MS_Product.Where(c => c.IsActive == 1 && c.IsDelete == 0 ).Select(s => new
                             {
                                 s.Product_Index,
                                 s.Product_Id,
                                 s.ProductCategory_Index,
                                 s.ProductCategory_Id,
                                 s.ProductCategory_Name,
                                 s.Ref_No3,
                             }).ToList() on BB.Product_Index equals PRDCAT.Product_Index into PDC
                             from PRDCAT in PDC.DefaultIfEmpty()
                             where !string.IsNullOrEmpty(data.productCategory_Id) ? PRDCAT?.ProductCategory_Id == data.productCategory_Id : PRDCAT?.ProductCategory_Id == PRDCAT?.ProductCategory_Id
                             orderby BB.Owner_Id , BB.Product_Id
                             group BB by new
                             {
                                 BB.Owner_Id,
                                 BB.Owner_Name,
                                 PRDCAT?.ProductCategory_Id,
                                 PRDCAT?.ProductCategory_Name,
                                 PRDCAT?.Ref_No3,
                                 BB.Product_Index,
                                 BB.Product_Id,
                                 BB.Product_Name,
                                 BB.BinBalance_QtyBal,
                                 BB.BinBalance_QtyBal_UR,
                                 BB.BinBalance_QtyBal_GR,
                                 BB.BinBalance_QtyBal_QI,
                                 BB.BinBalance_QtyReserve,
                                 BB.ItemStatus_Index,
                                 BB.ItemStatus_Id,
                                 BB.ItemStatus_Name,

                             } into G
                             select new
                             {
                                 G.Key.Owner_Id,
                                 G.Key.Owner_Name,
                                 G.Key.ProductCategory_Id,
                                 G.Key.ProductCategory_Name,
                                 G.Key.Ref_No3,
                                 G.Key.Product_Index,
                                 G.Key.Product_Id,
                                 G.Key.Product_Name,
                                 G.Key.BinBalance_QtyBal,
                                 G.Key.BinBalance_QtyBal_UR,
                                 G.Key.BinBalance_QtyBal_GR,
                                 G.Key.BinBalance_QtyBal_QI,
                                 G.Key.BinBalance_QtyReserve,
                                 Stock = ((G.Key.BinBalance_QtyBal ?? 0) - (G.Key.BinBalance_QtyReserve ?? 0)),
                                 G.Key.ItemStatus_Index,
                                 G.Key.ItemStatus_Id,
                                 G.Key.ItemStatus_Name,
                             }).ToList();


                //string selectDate = DateTime.ParseExact(data.binCard_date.Substring(0, 8), "yyyyMMdd",
                //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                if (query.Count == 0)
                {
                    var resultItem = new ReportInventoryStockViewModel();

                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {
                        var resultItem = new ReportInventoryStockViewModel();
                        resultItem.owner_Id = item.Owner_Id;
                        resultItem.owner_Name = item.Owner_Name;
                        resultItem.productCategory_Name = item.ProductCategory_Id;
                        resultItem.ref_No3 = item.Ref_No3;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.binBalance_QtyBal = item.BinBalance_QtyBal;
                        resultItem.binBalance_QtyBal_UR = item.BinBalance_QtyBal_UR;
                        resultItem.binBalance_QtyBal_GR = item.BinBalance_QtyBal_GR;
                        resultItem.binBalance_QtyBal_QI = item.BinBalance_QtyBal_QI;
                        resultItem.binBalance_QtyReserve = item.BinBalance_QtyReserve;
                        resultItem.stock = item.Stock;
                        resultItem.percentageStock = resultItem.binBalance_QtyReserve == 0 && resultItem.binBalance_QtyBal == 0 ? 0 : (resultItem.binBalance_QtyReserve / resultItem.binBalance_QtyBal * 150);
                        resultItem.ItemStatus_Id = item.ItemStatus_Id;
                        resultItem.ItemStatus_Name = item.ItemStatus_Name;
                        //resultItem.sumBinCard_Qty = item.BinCard_QtyBal;
                        result.Add(resultItem);
                    }
                    result.ToList();
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report15\\Report15.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report15");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = !string.IsNullOrEmpty(data.owner_Id) ? "STK" + DateTime.Now.ToString("yyyyMMdd") + data.owner_Id : DateTime.Now.ToString("yyyyMMddHHmmss");

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


                    var query = context.View_RPT_InventoryStock.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key) && data.key != "-")
                    {
                        query = query.Where(c => c.Owner_Id.Contains(data.key)
                                      || c.Owner_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new {c.Owner_Index, c.Owner_Id, c.Owner_Name }).Distinct().Take(10).ToList();

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

        #region autoSearchOwner
        public List<Report15ViewModel> autoSearchMC(Report15ViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {


                    var query = context.MS_ProductCategory.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                    if (!string.IsNullOrEmpty(data.key) && data.key != "-")
                    {
                        query = query.Where(c => c.ProductCategory_Id.Contains(data.key));

                    }

                    var items = new List<Report15ViewModel>();

                    var result = query.Select(c => new { c.ProductCategory_Id }).Distinct().Take(15).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new Report15ViewModel
                        {

                            name = item.ProductCategory_Id,

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

        public string ExportExcel(Report15ViewModel data, string rootPath = "")
        {
            var BC_DB = new BinbalanceDbContext();
            var M_DB = new MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<ReportInventoryStockViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("Report15");

            try
            {


                var queryBB = BC_DB.View_RPT_InventoryStock.AsQueryable();


                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryBB = queryBB.Where(c => c.Product_Id == data.product_Id);
                }

                if (!string.IsNullOrEmpty(data.owner_Id))
                {
                    queryBB = queryBB.Where(c => c.Owner_Id == data.owner_Id);
                }

                var query = (from BB in queryBB.ToList()
                             join PRDCAT in M_DB.MS_Product.Where(c => c.IsActive == 1 && c.IsDelete == 0).Select(s => new
                             {
                                 s.Product_Index,
                                 s.Product_Id,
                                 s.ProductCategory_Index,
                                 s.ProductCategory_Id,
                                 s.ProductCategory_Name,
                                 s.Ref_No3,
                             }).ToList() on BB.Product_Index equals PRDCAT.Product_Index into PDC
                             from PRDCAT in PDC.DefaultIfEmpty()
                             where !string.IsNullOrEmpty(data.productCategory_Id) ? PRDCAT?.ProductCategory_Id == data.productCategory_Id : PRDCAT?.ProductCategory_Id == PRDCAT?.ProductCategory_Id
                             orderby BB.Owner_Id, BB.Product_Id
                             group BB by new
                             {
                                 BB.Owner_Id,
                                 BB.Owner_Name,
                                 PRDCAT?.ProductCategory_Id,
                                 PRDCAT?.ProductCategory_Name,
                                 PRDCAT?.Ref_No3,
                                 BB.Product_Index,
                                 BB.Product_Id,
                                 BB.Product_Name,
                                 BB.BinBalance_QtyBal,
                                 BB.BinBalance_QtyBal_UR,
                                 BB.BinBalance_QtyBal_GR,
                                 BB.BinBalance_QtyBal_QI,
                                 BB.BinBalance_QtyReserve,
                                 BB.ItemStatus_Index,
                                 BB.ItemStatus_Id,
                                 BB.ItemStatus_Name,

                             } into G
                             select new
                             {
                                 G.Key.Owner_Id,
                                 G.Key.Owner_Name,
                                 G.Key.ProductCategory_Id,
                                 G.Key.ProductCategory_Name,
                                 G.Key.Ref_No3,
                                 G.Key.Product_Index,
                                 G.Key.Product_Id,
                                 G.Key.Product_Name,
                                 G.Key.BinBalance_QtyBal,
                                 G.Key.BinBalance_QtyBal_UR,
                                 G.Key.BinBalance_QtyBal_GR,
                                 G.Key.BinBalance_QtyBal_QI,
                                 G.Key.BinBalance_QtyReserve,
                                 Stock = ((G.Key.BinBalance_QtyBal ?? 0) - (G.Key.BinBalance_QtyReserve ?? 0)),
                                 G.Key.ItemStatus_Index,
                                 G.Key.ItemStatus_Id,
                                 G.Key.ItemStatus_Name,
                             }).ToList();


                string selectDate = DateTime.ParseExact(data.binCard_date.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                if (query.Count == 0)
                {
                    var resultItem = new ReportInventoryStockViewModel();

                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {
                        var resultItem = new ReportInventoryStockViewModel();
                        resultItem.owner_Id = item.Owner_Id;
                        resultItem.owner_Name = item.Owner_Name;
                        resultItem.productCategory_Name = item.ProductCategory_Id;
                        resultItem.ref_No3 = item.Ref_No3;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.binBalance_QtyBal = item.BinBalance_QtyBal;
                        resultItem.binBalance_QtyBal_UR = item.BinBalance_QtyBal_UR;
                        resultItem.binBalance_QtyBal_GR = item.BinBalance_QtyBal_GR;
                        resultItem.binBalance_QtyBal_QI = item.BinBalance_QtyBal_QI;
                        resultItem.binBalance_QtyReserve = item.BinBalance_QtyReserve;
                        resultItem.stock = item.Stock;
                        resultItem.percentageStock = resultItem.binBalance_QtyReserve == 0 && resultItem.binBalance_QtyBal == 0 ? 0 : (resultItem.binBalance_QtyReserve / resultItem.binBalance_QtyBal * 150);
                        resultItem.ItemStatus_Id = item.ItemStatus_Id;
                        resultItem.ItemStatus_Name = item.ItemStatus_Name;
                        //resultItem.sumBinCard_Qty = item.BinCard_QtyBal;
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

        #region report15
        public string autoGenReport15(string rootPath = "")
        {
            try
            {
                var ownerid = new List<string>();
                using (var context = new BinbalanceDbContext())
                {
                    ownerid = context.View_RPT_InventoryStock.GroupBy(g => g.Owner_Id).Select(s => s.Key).ToList();
                }
                foreach (var owner in ownerid)
                {
                    var PhysicalPath = printReport15(new Report15ViewModel { owner_Id = owner }, rootPath);
                    var pathDaily = new AppSettingConfig().GetUrl("ReportDaily") + DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                    if (!System.IO.Directory.Exists(pathDaily))
                    {
                        System.IO.Directory.CreateDirectory(pathDaily);
                    }
                    File.Move(PhysicalPath, pathDaily + "\\" + PhysicalPath.Replace(rootPath.Replace("\\ReportAPI","")+ "\\ReportGenerator\\", ""));
                }
                return "";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
    }
}

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

namespace ReportBusiness.ReportInventoryStock
{
    public class ReportInventoryStockService
    {

        #region printReportInventoryStock
        public dynamic printReportInventoryStock(ReportInventoryStockViewModel data, string rootPath = "")
        {
            var MS_DBContext = new MasterDataDbContext();
            var BB_DBContext = new BinbalanceDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<ReportInventoryStockViewModel>();

            try
            {
                var queryBB = BB_DBContext.View_RPT_InventoryStock.AsQueryable();
                var queryMS = MS_DBContext.MS_Product.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryBB = queryBB.Where(c => c.Product_Id == data.product_Id);
                }
                if (!string.IsNullOrEmpty(data.owner_Id))
                {
                    queryBB = queryBB.Where(c => c.Owner_Id == data.owner_Id);
                }
                if (!string.IsNullOrEmpty(data.productType_Id))
                {
                    queryMS = queryMS.Where(c => c.ProductType_Id == data.productType_Id);
                }
                //if (!string.IsNullOrEmpty(data.goodsReceive_date))
                //{
                //    var GoodsReceive_date_From = data.goodsReceive_date.toBetweenDate();
                //    queryBB = queryBB.Where(c => c.GoodsReceive_Date >= GoodsReceive_date_From.start);
                //}
                //if (!string.IsNullOrEmpty(data.goodsReceive_date) && !string.IsNullOrEmpty(data.goodsReceive_date_To))
                //{
                //    var dateStart = data.goodsReceive_date.toBetweenDate();
                //    var dateEnd = data.goodsReceive_date_To.toBetweenDate();
                //    queryBB = queryBB.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end);
                //}
                //else if (!string.IsNullOrEmpty(data.goodsReceive_date))
                //{
                //    var GoodsReceive_date_From = data.goodsReceive_date.toBetweenDate();
                //    queryBB = queryBB.Where(c => c.GoodsReceive_Date >= GoodsReceive_date_From.start);
                //}
                //else if (!string.IsNullOrEmpty(data.goodsReceive_date_To))
                //{
                //    var GoodsReceive_date_To = data.goodsReceive_date.toBetweenDate();
                //    queryBB = queryBB.Where(c => c.GoodsReceive_Date <= GoodsReceive_date_To.start);
                //}

                string startDate = data.goodsReceive_date;
                string GRDateStart = DateTime.ParseExact(startDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                string endDate = data.goodsReceive_date;
                string GRDateEnd = DateTime.ParseExact(endDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                var queryRPT_BB = queryBB.ToList();
                var queryRPT_MS = queryMS.ToList();


                var query = (from BB in queryRPT_BB
                                 //join PRDCAT in queryMS.Where(c => c.IsActive == 1 && c.IsDelete == 0).Join(MS_DBContext.MS_ProductConversion.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.ProductConversion_Name.ToUpper() == "PAL"),
                                 //PRD => PRD.ProductConversion_Index,
                                 //PRDCAT => PRDCAT.ProductConversion_Index,
                                 //(PRD, PRDCAT) => new
                                 //{
                                 //    PRD.Product_Index,
                                 //    PRD.Product_Id,
                                 //    PRD.ProductCategory_Index,
                                 //    PRD.ProductCategory_Id,
                                 //    PRD.ProductCategory_Name,
                                 //    PRD.Ref_No3,
                                 //    PRDCAT.ProductConversion_Height,
                                 //    PRDCAT.ProductConversion_Weight,
                                 //}).ToList() on BB.Product_Index equals PRDCAT.Product_Index into PDC
                             join PRDCAT in (from PRD in queryRPT_MS
                                             join PRDCAT in MS_DBContext.MS_ProductConversion.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.ProductConversion_Name.ToUpper() == "PAL").ToList() on PRD.Product_Id equals PRDCAT.Product_Id into PDC
                                             from PRDCAT in PDC.DefaultIfEmpty()
                                             select new
                                             {
                                                 PRD.Product_Index,
                                                 PRD.Product_Id,
                                                 PRD.ProductCategory_Index,
                                                 PRD.ProductCategory_Id,
                                                 PRD.ProductCategory_Name,
                                                 PRD.Ref_No3,
                                                 PRDCAT?.ProductConversion_Height,
                                                 PRDCAT?.ProductConversion_Weight,
                                             }).ToList() on BB.Product_Index equals PRDCAT.Product_Index //into PRDCAT
                             //from PRDCAT in PDC.DefaultIfEmpty()
                             //join LO in MS_DBContext.MS_Location.Where(c => c.IsActive == 1 && c.IsDelete == 0)
                             //                                   .Select(s => new { s.Location_Index, s.Location_Id, s.Location_Name, s.LocationVol_Height })
                             //                                   on BB.Location_Index equals LO.Location_Index into LOC
                             //from LO in LOC.DefaultIfEmpty()
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
                                 //BB.BinBalance_QtyBal,
                                 //BB.BinBalance_QtyBal_UR,
                                 //BB.BinBalance_QtyBal_GR,
                                 //BB.BinBalance_QtyBal_QI,
                                 //BB.BinBalance_QtyReserve,
                                 BB.ItemStatus_Index,
                                 BB.ItemStatus_Id,
                                 BB.ItemStatus_Name,
                                 BB.ProductConversion_Name,
                                 PRDCAT?.ProductConversion_Height,
                                 //LO?.LocationVol_Height,
                                 PRDCAT?.ProductConversion_Weight,
                                 BB.BinBalance_UnitHeightBal,
                                 BB.BinBalance_UnitWeightBal,
                                 //BB.Location_Index,
                                 //BB.GoodsReceive_Date,
                                 BB.GI_Date,

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
                                 BinBalance_QtyBal = G.Sum(gs => gs.BinBalance_QtyBal), //G.Key.BinBalance_QtyBal,
                                 BinBalance_QtyBal_UR = G.Sum(gs => gs.BinBalance_QtyBal_UR),//G.Key.BinBalance_QtyBal_UR,
                                 BinBalance_QtyBal_GR = G.Sum(gs => gs.BinBalance_QtyBal_GR),//G.Key.BinBalance_QtyBal_GR,
                                 BinBalance_QtyBal_QI = G.Sum(gs => gs.BinBalance_QtyBal_QI),//G.Key.BinBalance_QtyBal_QI,
                                 BinBalance_QtyReserve = G.Sum(gs => gs.BinBalance_QtyReserve),//G.Key.BinBalance_QtyReserve,
                                 Stock = G.Sum(gs => gs.BinBalance_QtyBal) - G.Sum(gs => gs.BinBalance_QtyReserve),  //((G.Key.BinBalance_QtyBal ?? 0) - (G.Key.BinBalance_QtyReserve ?? 0)),
                                 G.Key.ItemStatus_Index,
                                 G.Key.ItemStatus_Id,
                                 G.Key.ItemStatus_Name,
                                 G.Key.ProductConversion_Name,
                                 G.Key.ProductConversion_Height,
                                 //G.Key.LocationVol_Height,
                                 G.Key.ProductConversion_Weight,
                                 G.Key.BinBalance_UnitHeightBal,
                                 G.Key.BinBalance_UnitWeightBal,
                                 //G.Key.Location_Index,
                                 GoodsReceive_Date = G.Max(m => m.GoodsReceive_Date),
                                 G.Key.GI_Date,
                             }).ToList();

                if (query.Count == 0)
                {
                    var resultItem = new ReportInventoryStockViewModel();
                    resultItem.goodsReceive_date = GRDateStart;
                    resultItem.goodsReceive_date_To = GRDateEnd;
                    resultItem.dateToday = DateTime.Now.ToString("HH:mm");
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
                        resultItem.percentageStock = resultItem.binBalance_QtyBal_QI == 0 && resultItem.binBalance_QtyBal == 0 ? 0 : (resultItem.binBalance_QtyBal_QI / resultItem.binBalance_QtyBal * 100);
                        resultItem.ItemStatus_Id = item.ItemStatus_Id;
                        resultItem.ItemStatus_Name = item.ItemStatus_Name;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.productConversion_Height = item.ProductConversion_Height;
                        //resultItem.locationVol_Height = item.LocationVol_Height;
                        resultItem.productConversion_Weight = item.ProductConversion_Weight;
                        resultItem.binBalance_UnitHeightBal = item.BinBalance_UnitHeightBal;
                        resultItem.binBalance_UnitWeightBal = item.BinBalance_UnitWeightBal;
                        resultItem.goodsReceive_Date = item.GoodsReceive_Date == null ? "" : item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy");
                        resultItem.gi_Date = item.GI_Date == null ? "" : item.GI_Date.Value.ToString("dd/MM/yyyy");


                        resultItem.goodsReceive_date = GRDateStart;
                        resultItem.goodsReceive_date_To = GRDateEnd;
                        resultItem.dateToday = DateTime.Now.ToString("HH:mm");



                        result.Add(resultItem);
                    }
                    result.ToList();

                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\ReportInventoryStock\\ReportInventoryStock.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportInventoryStock");
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


                    var query = context.View_RPT_InventoryStock.AsQueryable();

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

        public string ExportExcel(ReportInventoryStockViewModel data, string rootPath = "")
        {
            var MS_DBContext = new MasterDataDbContext();
            var BB_DBContext = new BinbalanceDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<ReportInventoryStockViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportInventoryStock");

            try
            {

                var queryBB = BB_DBContext.View_RPT_InventoryStock.AsQueryable();
                var queryMS = MS_DBContext.MS_Product.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryBB = queryBB.Where(c => c.Product_Id == data.product_Id);
                }
                if (!string.IsNullOrEmpty(data.owner_Id))
                {
                    queryBB = queryBB.Where(c => c.Owner_Id == data.owner_Id);
                }
                if (!string.IsNullOrEmpty(data.productType_Name))
                {
                    queryMS = queryMS.Where(c => c.ProductType_Name == data.productType_Name);
                }
                //if (!string.IsNullOrEmpty(data.goodsReceive_date))
                //{
                //    var GoodsReceive_date_From = data.goodsReceive_date.toBetweenDate();
                //    queryBB = queryBB.Where(c => c.GoodsReceive_Date >= GoodsReceive_date_From.start);
                //}
                //if (!string.IsNullOrEmpty(data.goodsReceive_date) && !string.IsNullOrEmpty(data.goodsReceive_date_To))
                //{
                //    var dateStart = data.goodsReceive_date.toBetweenDate();
                //    var dateEnd = data.goodsReceive_date_To.toBetweenDate();
                //    queryBB = queryBB.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end);
                //}
                //else if (!string.IsNullOrEmpty(data.goodsReceive_date))
                //{
                //    var GoodsReceive_date_From = data.goodsReceive_date.toBetweenDate();
                //    queryBB = queryBB.Where(c => c.GoodsReceive_Date >= GoodsReceive_date_From.start);
                //}
                //else if (!string.IsNullOrEmpty(data.goodsReceive_date_To))
                //{
                //    var GoodsReceive_date_To = data.goodsReceive_date.toBetweenDate();
                //    queryBB = queryBB.Where(c => c.GoodsReceive_Date <= GoodsReceive_date_To.start);
                //}

                string startDate = data.goodsReceive_date;
                string GRDateStart = DateTime.ParseExact(startDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                string endDate = data.goodsReceive_date;
                string GRDateEnd = DateTime.ParseExact(endDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                var queryRPT_BB = queryBB.ToList();
                var queryRPT_MS = queryMS.ToList();


                var query = (from BB in queryRPT_BB
                                 //join PRDCAT in queryMS.Where(c => c.IsActive == 1 && c.IsDelete == 0).Join(MS_DBContext.MS_ProductConversion.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.ProductConversion_Name.ToUpper() == "PAL"),
                                 //PRD => PRD.ProductConversion_Index,
                                 //PRDCAT => PRDCAT.ProductConversion_Index,
                                 //(PRD, PRDCAT) => new
                                 //{
                                 //    PRD.Product_Index,
                                 //    PRD.Product_Id,
                                 //    PRD.ProductCategory_Index,
                                 //    PRD.ProductCategory_Id,
                                 //    PRD.ProductCategory_Name,
                                 //    PRD.Ref_No3,
                                 //    PRDCAT.ProductConversion_Height,
                                 //    PRDCAT.ProductConversion_Weight,
                                 //}).ToList() on BB.Product_Index equals PRDCAT.Product_Index into PDC
                             join PRDCAT in (from PRD in queryMS
                                             join PRDCAT in MS_DBContext.MS_ProductConversion.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.ProductConversion_Name.ToUpper() == "PAL").ToList() on PRD.Product_Id equals PRDCAT.Product_Id into PDC
                                             from PRDCAT in PDC.DefaultIfEmpty()
                                             select new
                                             {
                                                 PRD.Product_Index,
                                                 PRD.Product_Id,
                                                 PRD.ProductCategory_Index,
                                                 PRD.ProductCategory_Id,
                                                 PRD.ProductCategory_Name,
                                                 PRD.Ref_No3,
                                                 PRDCAT.ProductConversion_Height,
                                                 PRDCAT.ProductConversion_Weight,
                                             }).ToList() on BB.Product_Index equals PRDCAT.Product_Index into PDC
                             from PRDCAT in PDC.DefaultIfEmpty()
                                 //join LO in MS_DBContext.MS_Location.Where(c => c.IsActive == 1 && c.IsDelete == 0)
                                 //                                   .Select(s => new { s.Location_Index, s.Location_Id, s.Location_Name, s.LocationVol_Height })
                                 //                                   on BB.Location_Index equals LO.Location_Index into LOC
                                 //from LO in LOC.DefaultIfEmpty()
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
                                 //BB.BinBalance_QtyBal,
                                 //BB.BinBalance_QtyBal_UR,
                                 //BB.BinBalance_QtyBal_GR,
                                 //BB.BinBalance_QtyBal_QI,
                                 //BB.BinBalance_QtyReserve,
                                 BB.ItemStatus_Index,
                                 BB.ItemStatus_Id,
                                 BB.ItemStatus_Name,
                                 BB.ProductConversion_Name,
                                 PRDCAT?.ProductConversion_Height,
                                 //LO?.LocationVol_Height,
                                 PRDCAT?.ProductConversion_Weight,
                                 BB.BinBalance_UnitHeightBal,
                                 BB.BinBalance_UnitWeightBal,
                                 //BB.Location_Index,
                                 //BB.GoodsReceive_Date,
                                 BB.GI_Date,

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
                                 BinBalance_QtyBal = G.Sum(gs => gs.BinBalance_QtyBal), //G.Key.BinBalance_QtyBal,
                                 BinBalance_QtyBal_UR = G.Sum(gs => gs.BinBalance_QtyBal_UR),//G.Key.BinBalance_QtyBal_UR,
                                 BinBalance_QtyBal_GR = G.Sum(gs => gs.BinBalance_QtyBal_GR),//G.Key.BinBalance_QtyBal_GR,
                                 BinBalance_QtyBal_QI = G.Sum(gs => gs.BinBalance_QtyBal_QI),//G.Key.BinBalance_QtyBal_QI,
                                 BinBalance_QtyReserve = G.Sum(gs => gs.BinBalance_QtyReserve),//G.Key.BinBalance_QtyReserve,
                                 Stock = G.Sum(gs => gs.BinBalance_QtyBal) - G.Sum(gs => gs.BinBalance_QtyReserve),  //((G.Key.BinBalance_QtyBal ?? 0) - (G.Key.BinBalance_QtyReserve ?? 0)),
                                 G.Key.ItemStatus_Index,
                                 G.Key.ItemStatus_Id,
                                 G.Key.ItemStatus_Name,
                                 G.Key.ProductConversion_Name,
                                 G.Key.ProductConversion_Height,
                                 //G.Key.LocationVol_Height,
                                 G.Key.ProductConversion_Weight,
                                 G.Key.BinBalance_UnitHeightBal,
                                 G.Key.BinBalance_UnitWeightBal,
                                 //G.Key.Location_Index,
                                 GoodsReceive_Date = G.Max(m => m.GoodsReceive_Date),
                                 G.Key.GI_Date,
                             }).ToList();

                if (query.Count == 0)
                {
                    var resultItem = new ReportInventoryStockViewModel();
                    resultItem.goodsReceive_date = GRDateStart;
                    resultItem.goodsReceive_date_To = GRDateEnd;
                    resultItem.dateToday = DateTime.Now.ToString("HH:mm");
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
                        resultItem.percentageStock = resultItem.binBalance_QtyBal_QI == 0 && resultItem.binBalance_QtyBal == 0 ? 0 : (resultItem.binBalance_QtyBal_QI / resultItem.binBalance_QtyBal * 100);
                        resultItem.ItemStatus_Id = item.ItemStatus_Id;
                        resultItem.ItemStatus_Name = item.ItemStatus_Name;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.productConversion_Height = item.ProductConversion_Height;
                        //resultItem.locationVol_Height = item.LocationVol_Height;
                        resultItem.productConversion_Weight = item.ProductConversion_Weight;
                        resultItem.binBalance_UnitHeightBal = item.BinBalance_UnitHeightBal;
                        resultItem.binBalance_UnitWeightBal = item.BinBalance_UnitWeightBal;
                        resultItem.goodsReceive_Date = item.GoodsReceive_Date == null ? "" : item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy");
                        resultItem.gi_Date = item.GI_Date == null ? "" : item.GI_Date.Value.ToString("dd/MM/yyyy");


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

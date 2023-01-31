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
using System.IO;

namespace ReportBusiness.Report17
{
    public class Report17Service
    {

        #region printReport17
        public dynamic printReport17(Report17ViewModel data, string rootPath = "")
        {
            var BC_DBContext = new BinbalanceDbContext();
            var M_DBContext = new MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report17ViewModel>();

            try
            {
                var queryBC = BC_DBContext.View_RPT17_BinCard.AsQueryable();
                var queryGR = BC_DBContext.View_RPT17_GR.AsQueryable();
                var ProductCategoryDB = M_DBContext.MS_ProductCategory.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();
                if (!string.IsNullOrEmpty(data?.productCategory_Index?.ToString()))
                {
                    ProductCategoryDB = ProductCategoryDB.Where(c => c.ProductCategory_Index == data.productCategory_Index);
                }

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryBC = queryBC.Where(c => c.Product_Id == data.product_Id);
                }

                //if (!string.IsNullOrEmpty(data.soldTo_Name))
                //{
                //    queryGR = queryGR.Where(c => c.SoldTo_NAme == data.soldTo_Name);
                //}

                //if (!string.IsNullOrEmpty(data.costcenter_Name))
                //{
                //    queryGR = queryGR.Where(c => c.CostCenter_Name == data.costcenter_Name);
                //}
                if (!string.IsNullOrEmpty(data?.owner_Index?.ToString()))
                {
                    queryBC = queryBC.Where(c => c.Owner_Index == data.owner_Index);
                }
                if (!string.IsNullOrEmpty(data.binCard_Date))
                {

                    queryBC = queryBC.Where(c => c.BinCard_Month == Convert.ToInt32(data.binCard_Date));
                }

                var queryRPT_BC = queryBC.ToList();
                var queryRPT_GR = queryGR.ToList();

                //var query = (from BC in queryRPT_BC
                //             join GR in queryRPT_GR on BC.Ref_Document_Index equals GR.GoodsReceive_Index into ps
                //             from r in ps.DefaultIfEmpty()
                //             select new
                //             {
                //                 bc = BC,
                //                 gr = r
                //             }).ToList();
                var query = (from BC in queryRPT_BC
                             join GR in queryRPT_GR on BC.Ref_Document_Index equals GR.GoodsReceive_Index into ps
                             from r in ps.DefaultIfEmpty()
                             join PC in M_DBContext.MS_Product.Where(c => c.IsActive == 1 && c.IsDelete == 0).Join(ProductCategoryDB,
                            p => p.ProductCategory_Index,
                            pc => pc.ProductCategory_Index,
                            (p, pc) => new
                            {
                                p.Product_Index,
                                pc.ProductCategory_Index,
                                pc.ProductCategory_Id,
                                pc.ProductCategory_Name
                            }).ToList() on BC.Product_Index equals PC.Product_Index
                             group BC by new
                             {
                                 BC?.Ref_Document_Index,
                                 BC?.Owner_Index,
                                 BC?.Owner_Id,
                                 BC?.Owner_Name,
                                 BC?.Product_Index,
                                 BC?.Product_Id,
                                 BC?.Product_Name,
                                 BC?.ProductConversion_Name,
                                 r?.CostCenter_Name,
                                 r?.vendor_Name,
                                 r?.SoldTo_NAme,
                                 PC?.ProductCategory_Id,
                                 PC?.ProductCategory_Name,
                             } into G
                             select new
                             {
                                 G.Key.Ref_Document_Index,
                                 G.Key.Owner_Index,
                                 G.Key.Owner_Id,
                                 G.Key.Owner_Name,
                                 G.Key.Product_Index,
                                 G.Key.Product_Id,
                                 G.Key.Product_Name,
                                 G.Key.ProductConversion_Name,
                                 BinCard_QtyIn = G.Sum(s => s.BinCard_QtyIn),
                                 BinCard_QtyOut = G.Sum(s => s.BinCard_QtyOut),
                                 G.Key.CostCenter_Name,
                                 G.Key.vendor_Name,
                                 G.Key.SoldTo_NAme,
                                 G.Key.ProductCategory_Id,
                                 G.Key.ProductCategory_Name,
                             }
                             ).ToList();

                var ResultMonth = "";
                if (data.binCard_Date == "1")
                {
                    ResultMonth = "มกราคม";
                }
                else if (data.binCard_Date == "2")
                {
                    ResultMonth = "กุมภาพันธ์";
                }
                else if (data.binCard_Date == "3")
                {
                    ResultMonth = "มีนาคม";
                }
                else if (data.binCard_Date == "4")
                {
                    ResultMonth = "เมษายน";
                }
                else if (data.binCard_Date == "5")
                {
                    ResultMonth = "พฤษภาคม";
                }
                else if (data.binCard_Date == "6")
                {
                    ResultMonth = "มิถุนายน";
                }
                else if (data.binCard_Date == "7")
                {
                    ResultMonth = "กรกฏาคม";
                }
                else if (data.binCard_Date == "8")
                {
                    ResultMonth = "สิงหาคม";
                }
                else if (data.binCard_Date == "9")
                {
                    ResultMonth = "กันยายน";
                }
                else if (data.binCard_Date == "10")
                {
                    ResultMonth = "ตุลาคม";
                }
                else if (data.binCard_Date == "11")
                {
                    ResultMonth = "พฤศจิกายน";
                }
                else if (data.binCard_Date == "12")
                {
                    ResultMonth = "ธันวาคม";
                }

                if (query.Count == 0)
                {
                    var resultItem = new Report17ViewModel();
                    resultItem.checkQuery = true;
                    resultItem.binCard_QtyIn = 0;
                    resultItem.binCard_QtyOut = 0;
                    resultItem.binCard_Date = ResultMonth;

                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {

                        var resultItem = new Report17ViewModel();

                        resultItem.binCard_Date = ResultMonth;
                        resultItem.ref_Document_Index = item?.Ref_Document_Index;
                        resultItem.product_Id = item?.Product_Id;
                        resultItem.product_Name = item?.Product_Name;
                        resultItem.productConversion_Name = item?.ProductConversion_Name;
                        resultItem.binCard_QtyIn = item?.BinCard_QtyIn;
                        resultItem.binCard_QtyOut = item?.BinCard_QtyOut;

                        resultItem.costcenter_Name = item?.CostCenter_Name;
                        resultItem.vendor_Name = item?.vendor_Name;
                        resultItem.soldTo_Name = item?.SoldTo_NAme;


                        resultItem.owner_Id = item?.Owner_Id;
                        resultItem.owner_Name = item?.Owner_Name;
                        resultItem.productCategory_Id = item?.ProductCategory_Id;

                        resultItem.percentage_BinCard_QtyIn = item?.BinCard_QtyIn != 0 ?(item?.BinCard_QtyIn / query.Where(c => c.Owner_Index == item.Owner_Index).Sum(s => s.BinCard_QtyIn)) * 100 : 0;
                        resultItem.percentage_BinCard_QtyOut = item?.BinCard_QtyOut != 0 ? (item?.BinCard_QtyOut / query.Where(c => c.Owner_Index == item.Owner_Index).Sum(s => s.BinCard_QtyOut)) * 100 :0;


                        result.Add(resultItem);
                    }

                    //foreach (var item in query)
                    //{

                    //    var resultItem = new Report17ViewModel();

                    //    resultItem.binCard_Date = ResultMonth;
                    //    resultItem.ref_Document_Index = item.bc.Ref_Document_Index;
                    //    resultItem.product_Id = item.bc.Product_Id;
                    //    resultItem.product_Name = item.bc.Product_Name;
                    //    resultItem.productConversion_Name = item.bc.ProductConversion_Name;
                    //    resultItem.binCard_QtyIn = item.bc.BinCard_QtyIn;
                    //    resultItem.binCard_QtyOut = item.bc.BinCard_QtyOut;

                    //    if (item.gr == null)
                    //    {
                    //        resultItem.costcenter_Name = null;
                    //        resultItem.vendor_Name = null;
                    //        resultItem.soldTo_Name = null;
                    //    }
                    //    else
                    //    {
                    //        resultItem.costcenter_Name = item.gr.CostCenter_Name;
                    //        resultItem.vendor_Name = item.gr.vendor_Name;
                    //        resultItem.soldTo_Name = item.gr.SoldTo_NAme;
                    //    }


                    //    result.Add(resultItem);
                    //}
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report17\\Report17.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report17");
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


        public string ExportExcel(Report17ViewModel data, string rootPath = "")
        {
            var BC_DBContext = new BinbalanceDbContext();
            var M_DBContext = new MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report17ViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("Report17");

            try
            {

                var queryBC = BC_DBContext.View_RPT17_BinCard.AsQueryable();
                var queryGR = BC_DBContext.View_RPT17_GR.AsQueryable();
                var ProductCategoryDB = M_DBContext.MS_ProductCategory.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();
                if (!string.IsNullOrEmpty(data?.productCategory_Index?.ToString()))
                {
                    ProductCategoryDB = ProductCategoryDB.Where(c => c.ProductCategory_Index == data.productCategory_Index);
                }

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryBC = queryBC.Where(c => c.Product_Id == data.product_Id);
                }

                //if (!string.IsNullOrEmpty(data.soldTo_Name))
                //{
                //    queryGR = queryGR.Where(c => c.SoldTo_NAme == data.soldTo_Name);
                //}

                //if (!string.IsNullOrEmpty(data.costcenter_Name))
                //{
                //    queryGR = queryGR.Where(c => c.CostCenter_Name == data.costcenter_Name);
                //}
                if (!string.IsNullOrEmpty(data?.owner_Index?.ToString()))
                {
                    queryBC = queryBC.Where(c => c.Owner_Index == data.owner_Index);
                }
                if (!string.IsNullOrEmpty(data.binCard_Date))
                {

                    queryBC = queryBC.Where(c => c.BinCard_Month == Convert.ToInt32(data.binCard_Date));
                }

                var queryRPT_BC = queryBC.ToList();
                var queryRPT_GR = queryGR.ToList();

                //var query = (from BC in queryRPT_BC
                //             join GR in queryRPT_GR on BC.Ref_Document_Index equals GR.GoodsReceive_Index into ps
                //             from r in ps.DefaultIfEmpty()
                //             select new
                //             {
                //                 bc = BC,
                //                 gr = r
                //             }).ToList();
                var query = (from BC in queryRPT_BC
                             join GR in queryRPT_GR on BC.Ref_Document_Index equals GR.GoodsReceive_Index into ps
                             from r in ps.DefaultIfEmpty()
                             join PC in M_DBContext.MS_Product.Where(c => c.IsActive == 1 && c.IsDelete == 0).Join(ProductCategoryDB,
                             p => p.ProductCategory_Index,
                             pc => pc.ProductCategory_Index,
                             (p, pc) => new
                             {
                                 p.Product_Index,
                                 pc.ProductCategory_Index,
                                 pc.ProductCategory_Id,
                                 pc.ProductCategory_Name
                             }).ToList() on BC.Product_Index equals PC.Product_Index
                             group BC by new
                             {
                                 BC?.Ref_Document_Index,
                                 BC?.Owner_Index,
                                 BC?.Owner_Id,
                                 BC?.Owner_Name,
                                 BC?.Product_Index,
                                 BC?.Product_Id,
                                 BC?.Product_Name,
                                 BC?.ProductConversion_Name,
                                 r?.CostCenter_Name,
                                 r?.vendor_Name,
                                 r?.SoldTo_NAme,
                                 PC?.ProductCategory_Id,
                                 PC?.ProductCategory_Name,
                             } into G
                             select new
                             {
                                 G.Key.Ref_Document_Index,
                                 G.Key.Owner_Index,
                                 G.Key.Owner_Id,
                                 G.Key.Owner_Name,
                                 G.Key.Product_Index,
                                 G.Key.Product_Id,
                                 G.Key.Product_Name,
                                 G.Key.ProductConversion_Name,
                                 BinCard_QtyIn = G.Sum(s => s.BinCard_QtyIn),
                                 BinCard_QtyOut = G.Sum(s => s.BinCard_QtyOut),
                                 G.Key.CostCenter_Name,
                                 G.Key.vendor_Name,
                                 G.Key.SoldTo_NAme,
                                 G.Key.ProductCategory_Id,
                                 G.Key.ProductCategory_Name,
                             }
                             ).ToList();

                var ResultMonth = "";
                if (data.binCard_Date == "1")
                {
                    ResultMonth = "มกราคม";
                }
                else if (data.binCard_Date == "2")
                {
                    ResultMonth = "กุมภาพันธ์";
                }
                else if (data.binCard_Date == "3")
                {
                    ResultMonth = "มีนาคม";
                }
                else if (data.binCard_Date == "4")
                {
                    ResultMonth = "เมษายน";
                }
                else if (data.binCard_Date == "5")
                {
                    ResultMonth = "พฤษภาคม";
                }
                else if (data.binCard_Date == "6")
                {
                    ResultMonth = "มิถุนายน";
                }
                else if (data.binCard_Date == "7")
                {
                    ResultMonth = "กรกฏาคม";
                }
                else if (data.binCard_Date == "8")
                {
                    ResultMonth = "สิงหาคม";
                }
                else if (data.binCard_Date == "9")
                {
                    ResultMonth = "กันยายน";
                }
                else if (data.binCard_Date == "10")
                {
                    ResultMonth = "ตุลาคม";
                }
                else if (data.binCard_Date == "11")
                {
                    ResultMonth = "พฤศจิกายน";
                }
                else if (data.binCard_Date == "12")
                {
                    ResultMonth = "ธันวาคม";
                }

                if (query.Count == 0)
                {
                    var resultItem = new Report17ViewModel();
                    resultItem.checkQuery = true;
                    resultItem.binCard_QtyIn = 0;
                    resultItem.binCard_QtyOut = 0;
                    resultItem.binCard_Date = ResultMonth;

                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {

                        var resultItem = new Report17ViewModel();

                        resultItem.binCard_Date = ResultMonth;
                        resultItem.ref_Document_Index = item?.Ref_Document_Index;
                        resultItem.product_Id = item?.Product_Id;
                        resultItem.product_Name = item?.Product_Name;
                        resultItem.productConversion_Name = item?.ProductConversion_Name;
                        resultItem.binCard_QtyIn = item?.BinCard_QtyIn;
                        resultItem.binCard_QtyOut = item?.BinCard_QtyOut;

                        resultItem.costcenter_Name = item?.CostCenter_Name;
                        resultItem.vendor_Name = item?.vendor_Name;
                        resultItem.soldTo_Name = item?.SoldTo_NAme;


                        resultItem.owner_Id = item?.Owner_Id;
                        resultItem.owner_Name = item?.Owner_Name;
                        resultItem.productCategory_Id = item?.ProductCategory_Id;

                        resultItem.percentage_BinCard_QtyIn = item?.BinCard_QtyIn != 0 ? (item?.BinCard_QtyIn / query.Where(c => c.Owner_Index == item.Owner_Index).Sum(s => s.BinCard_QtyIn)) * 100 : 0;
                        resultItem.percentage_BinCard_QtyOut = item?.BinCard_QtyOut != 0 ? (item?.BinCard_QtyOut / query.Where(c => c.Owner_Index == item.Owner_Index).Sum(s => s.BinCard_QtyOut)) * 100 : 0;


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
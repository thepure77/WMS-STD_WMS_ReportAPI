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
using ReportBusiness.Report8;
using System.IO;

namespace ReportBusiness.Report18
{
    public class Report18Service
    {

        #region report18
        public string printReport18(Report18ViewModel data, string rootPath = "")
        {

            var BC_DB = new BinbalanceDbContext();
            var M_DBContext = new MasterDataDbContext();
            var GI_DB = new GIDbContext();
            var PLG_DB = new PlanGIDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var queryBC = BC_DB.View_RPT18_BinCard.AsQueryable();
                var queryGI = GI_DB.IM_GoodsIssueItemLocation.AsQueryable();
                var queryPLG = PLG_DB.im_PlanGoodsIssue.AsQueryable();


                var ProductCategoryDB = M_DBContext.MS_ProductCategory.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();
                if (!string.IsNullOrEmpty(data?.productCategory_Index?.ToString()))
                {
                    ProductCategoryDB = ProductCategoryDB.Where(c => c.ProductCategory_Index == data.productCategory_Index);
                }

                var result = new List<Report18ViewModel>();
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryBC = queryBC.Where(c => c.Product_Name.Contains(data.product_Id));
                }
                if (!string.IsNullOrEmpty(data.create_By))
                {
                    queryBC = queryBC.Where(c => c.Create_By.Contains(data.create_By));
                }
                if (!string.IsNullOrEmpty(data.costCenter_Id))
                {
                    queryPLG = queryPLG.Where(c => c.CostCenter_Id.Contains(data.costCenter_Id));
                }

                var yearNow = DateTime.ParseExact(DateTime.Now.toString().Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy", culture);
                if (!string.IsNullOrEmpty(data.binCard_Date))
                {
                    queryBC = queryBC.Where(c => c.Bin_year.ToString() == data.binCard_Date);
                }
                else
                {

                    queryBC = queryBC.Where(c => c.Bin_year.ToString() == yearNow);
                }


                var queryRPT_BC = queryBC.ToList();
                var queryRPT_GI = queryGI.ToList();
                var queryRPT_PLG = queryPLG.ToList();

                var query = (from BC in queryBC
                             join GI in queryRPT_GI on BC.Ref_DocumentItem_Index equals GI.GoodsIssueItemLocation_Index into ps
                             from r in ps
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
                             select new
                             {
                                 bc = BC,
                                 gi = r,
                                 pc = PC
                             }).ToList();
                var query2 = (from GI in query
                              join PLG in queryRPT_PLG on GI.gi.Ref_Document_Index equals PLG.PlanGoodsIssue_Index into ps
                              from r in ps
                              select new
                              {
                                  gi = GI,
                                  plg = r
                              }).ToList();


                if (query2.Count == 0)
                {
                    var resultItem = new Report18ViewModel();
                    resultItem.checkQuery = true;
                    resultItem.binCard_QtyIn = 0;
                    resultItem.binCard_QtyOut = 0;
                    resultItem.binCard_date = data.binCard_Date;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query2)
                    {

                        var resultItem = new Report18ViewModel();
                        resultItem.product_Id = item.gi.bc.Product_Id;
                        resultItem.product_Name = item.gi.bc.Product_Name;
                        resultItem.productConversion_Name = item.gi.bc.ProductConversion_Name;
                        resultItem.binCard_QtyIn = item.gi.bc.BinCard_QtyIn;
                        resultItem.binCard_QtyOut = item.gi.bc.BinCard_QtyOut;
                        resultItem.create_By = item.gi.bc.Create_By;
                        resultItem.binCard_date = item.gi.bc.Bin_year.ToString();
                        resultItem.costCenter_Id = item.plg.CostCenter_Id;

                        resultItem.owner_Id = item?.gi.bc.Owner_Id;
                        resultItem.owner_Name = item?.gi.bc.Owner_Name;
                        resultItem.productCategory_Id = item?.gi?.pc?.ProductCategory_Id;

                        resultItem.percentage_BinCard_QtyIn = item.gi.bc.BinCard_QtyIn != 0 ? (item.gi.bc.BinCard_QtyIn / query2.Where(c => c.gi.bc.Owner_Index == item.gi.bc.Owner_Index).Sum(s => s.gi.bc.BinCard_QtyIn)) * 100 : 0;
                        resultItem.percentage_BinCard_QtyOut = item.gi.bc.BinCard_QtyOut != 0 ? (item.gi.bc.BinCard_QtyOut / query2.Where(c => c.gi.bc.Owner_Index == item.gi.bc.Owner_Index).Sum(s => s.gi.bc.BinCard_QtyOut)) * 100 : 0;


                        result.Add(resultItem);
                    }
                    result.ToList();
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report18\\Report18.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report18");
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

        public string ExportExcel(Report18ViewModel data, string rootPath = "")
        {
            var BC_DB = new BinbalanceDbContext();
            var M_DBContext = new MasterDataDbContext();
            var GI_DB = new GIDbContext();
            var PLG_DB = new PlanGIDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report18ViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("Report18");

            try
            {
                var queryBC = BC_DB.View_RPT18_BinCard.AsQueryable();
                var queryGI = GI_DB.IM_GoodsIssueItemLocation.AsQueryable();
                var queryPLG = PLG_DB.im_PlanGoodsIssue.AsQueryable();

                var ProductCategoryDB = M_DBContext.MS_ProductCategory.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();
                if (!string.IsNullOrEmpty(data?.productCategory_Index?.ToString()))
                {
                    ProductCategoryDB = ProductCategoryDB.Where(c => c.ProductCategory_Index == data.productCategory_Index);
                }

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryBC = queryBC.Where(c => c.Product_Name.Contains(data.product_Id));
                }
                //if (!string.IsNullOrEmpty(data.create_By))
                //{
                //    queryBC = queryBC.Where(c => c.Create_By.Contains(data.create_By));
                //}
                //if (!string.IsNullOrEmpty(data.costCenter_Id))
                //{
                //    queryPLG = queryPLG.Where(c => c.CostCenter_Id.Contains(data.costCenter_Id));
                //}

                if (!string.IsNullOrEmpty(data?.owner_Index?.ToString()))
                {
                    queryBC = queryBC.Where(c => c.Owner_Index == data.owner_Index);
                }

                var yearNow = DateTime.ParseExact(DateTime.Now.toString().Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy", culture);
                if (!string.IsNullOrEmpty(data.binCard_Date))
                {
                    queryBC = queryBC.Where(c => c.Bin_year.ToString() == data.binCard_Date);
                }
                else
                {

                    queryBC = queryBC.Where(c => c.Bin_year.ToString() == yearNow);
                }


                var queryRPT_BC = queryBC.ToList();
                var queryRPT_GI = queryGI.ToList();
                var queryRPT_PLG = queryPLG.ToList();

                var query = (from BC in queryBC
                             join GI in queryRPT_GI on BC.Ref_DocumentItem_Index equals GI.GoodsIssueItemLocation_Index into ps
                             from r in ps
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
                             select new
                             {
                                 bc = BC,
                                 gi = r,
                                 pc = PC
                             }).ToList();
                var query2 = (from GI in query
                              join PLG in queryRPT_PLG on GI.gi.Ref_Document_Index equals PLG.PlanGoodsIssue_Index into ps
                              from r in ps
                              select new
                              {
                                  gi = GI,
                                  plg = r
                              }).ToList();


                if (query2.Count == 0)
                {
                    var resultItem = new Report18ViewModel();
                    resultItem.checkQuery = true;
                    resultItem.binCard_QtyIn = 0;
                    resultItem.binCard_QtyOut = 0;
                    resultItem.binCard_date = data.binCard_Date;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query2)
                    {

                        var resultItem = new Report18ViewModel();
                        resultItem.product_Id = item.gi.bc.Product_Id;
                        resultItem.product_Name = item.gi.bc.Product_Name;
                        resultItem.productConversion_Name = item.gi.bc.ProductConversion_Name;
                        resultItem.binCard_QtyIn = item.gi.bc.BinCard_QtyIn;
                        resultItem.binCard_QtyOut = item.gi.bc.BinCard_QtyOut;
                        resultItem.create_By = item.gi.bc.Create_By;
                        resultItem.binCard_date = item.gi.bc.Bin_year.ToString();
                        resultItem.costCenter_Id = item.plg.CostCenter_Id;

                        resultItem.owner_Id = item?.gi.bc.Owner_Id;
                        resultItem.owner_Name = item?.gi.bc.Owner_Name;
                        resultItem.productCategory_Id = item?.gi?.pc?.ProductCategory_Id;

                        resultItem.percentage_BinCard_QtyIn = item.gi.bc.BinCard_QtyIn != 0 ? (item.gi.bc.BinCard_QtyIn / query2.Where(c => c.gi.bc.Owner_Index == item.gi.bc.Owner_Index).Sum(s => s.gi.bc.BinCard_QtyIn)) * 100 : 0;
                        resultItem.percentage_BinCard_QtyOut = item.gi.bc.BinCard_QtyOut != 0 ? (item.gi.bc.BinCard_QtyOut / query2.Where(c => c.gi.bc.Owner_Index == item.gi.bc.Owner_Index).Sum(s => s.gi.bc.BinCard_QtyOut)) * 100 : 0;


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

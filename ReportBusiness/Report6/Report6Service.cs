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

namespace ReportBusiness.Report6
{
    public class Report6Service
    {
        #region printReport6
        public dynamic printReport6(Report6ViewModel data, string rootPath = "")
        {
            var GI_DBContext = new GIDbContext();
            var PlanGI_DBContext = new PlanGIDbContext();
            var GRI_DBContext = new GRDbContext();
            var PlanGR_DBContext = new PlanGRDbContext();
            var M_DBContext = new MasterDataDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report6ViewModel>();

            try
            {
                var queryGI = GI_DBContext.View_RPT6_GIL.AsQueryable();
                var queryPlanGI = PlanGI_DBContext.im_PlanGoodsIssue.AsQueryable();
                var queryGRI = GRI_DBContext.View_GoodssReceiveItem_RPT_6.AsQueryable();
                var queryPlanGR = PlanGR_DBContext.IM_PlanGoodsReceive.AsQueryable();


                if (!string.IsNullOrEmpty(data.create_By))
                {
                    queryGI = queryGI.Where(c => c.Create_By == data.create_By);
                }

                if (!string.IsNullOrEmpty(data.userAssign))
                {
                    queryGI = queryGI.Where(c => c.Picking_By == data.userAssign);
                }

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryGI = queryGI.Where(c => c.Product_Id == data.product_Id);
                }

                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    queryGI = queryGI.Where(c => c.GoodsIssue_No == data.goodsIssue_No);
                }

                if (!string.IsNullOrEmpty(data.goodsIssue_date) && !string.IsNullOrEmpty(data.goodsIssue_date_To))
                {
                    var dateStart = data.goodsIssue_date.toBetweenDate();
                    var dateEnd = data.goodsIssue_date_To.toBetweenDate();
                    queryGI = queryGI.Where(c => c.GoodsIssue_Date >= dateStart.start && c.GoodsIssue_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.goodsIssue_date))
                {
                    var goodsIssue_date_From = data.goodsIssue_date.toBetweenDate();
                    queryGI = queryGI.Where(c => c.GoodsIssue_Date >= goodsIssue_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.goodsIssue_date_To))
                {
                    var goodsIssue_date_To = data.goodsIssue_date_To.toBetweenDate();
                    queryGI = queryGI.Where(c => c.GoodsIssue_Date <= goodsIssue_date_To.start);
                }

                var queryRPT_GI = queryGI.ToList();
                var queryRPT_PlanGI = queryPlanGI.ToList();
                var queryRPT_GRI = queryGRI.ToList();
                var queryRPT_PlanGR = queryPlanGR.ToList();

                var query = (from GI in queryRPT_GI
                             join PlanGI in queryRPT_PlanGI on GI.Ref_Document_Index equals PlanGI.PlanGoodsIssue_Index into ps
                             from r in ps
                             select new
                             {
                                 gi = GI,
                                 plangi = r
                             }).ToList();
                var query2 = (from GI in query
                              join GRI in queryRPT_GRI on GI.gi.GoodsReceiveItem_Index equals GRI.GoodsReceiveItem_Index into ps
                              from r in ps.DefaultIfEmpty()
                              select new
                              {
                                  gi1 = GI,
                                  gri = r
                              }).ToList();
                var query3 = (from GRI in query2
                              join PlanGR in queryRPT_PlanGR on GRI.gri.Ref_Document_Index equals PlanGR.PlanGoodsReceive_Index into ps
                              from r in ps.DefaultIfEmpty()
                              select new
                              {
                                  gi2 = GRI,
                                  plangr = r
                              }).ToList();

                if (query3.Count == 0)
                {
                    var resultItem = new Report6ViewModel();

                    var startDate = DateTime.ParseExact(data.goodsIssue_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.goodsIssue_date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.checkQuery = true;
                    resultItem.qty = 0;
                    resultItem.goodsIssue_date = startDate;
                    resultItem.goodsIssue_date_To = endDate;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query3)
                    {

                        var resultItem = new Report6ViewModel();


                        resultItem.planGoodsIssue_No = item?.gi2?.gi1?.plangi?.PlanGoodsIssue_No;

                        var GI_date = GI_DBContext.IM_GoodsIssue.Find(item?.gi2?.gi1?.gi?.GoodsIssue_Index);

                        if (GI_date != null)
                        {
                            var Date = DateTime.ParseExact(GI_date.GoodsIssue_Date.toString().Substring(0, 8), "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                            resultItem.goodsIssue_Date = Date + " " + GI_date.GoodsIssue_Time;

                        }


                        resultItem.product_Id = item?.gi2?.gi1?.gi?.Product_Id;
                        resultItem.product_Name = item?.gi2?.gi1?.gi?.Product_Name;
                        resultItem.goodsIssue_No = item?.gi2?.gi1?.gi?.GoodsIssue_No;
                        resultItem.qty = item?.gi2?.gi1?.gi?.Qty;
                        resultItem.productConversion_Name = item?.gi2?.gi1?.gi?.ProductConversion_Name;
                        resultItem.location_Name = item?.gi2?.gi1?.gi?.Location_Name;
                        resultItem.costCenter_Id = item.gi2.gi1.plangi.CostCenter_Id;
                        resultItem.create_By = item?.gi2?.gi1?.gi?.Create_By;
                        resultItem.userAssign = item?.gi2?.gi1?.gi?.Picking_By;
                        resultItem.vendor_Name = item?.plangr?.Vendor_Name;

                        resultItem.sold_Id = item?.gi2?.gi1?.plangi?.SoldTo_Id;
                        resultItem.sold_Name = item?.gi2?.gi1?.plangi?.SoldTo_Name;
                        resultItem.shipTO_Id = item?.gi2?.gi1?.plangi?.ShipTo_Id;
                        resultItem.shipTO_Name = item?.gi2?.gi1?.plangi?.ShipTo_Name;
                        resultItem.owner_Id = item?.gi2?.gi1?.plangi?.Owner_Id;
                        resultItem.owner_Name = item?.gi2?.gi1?.plangi?.Owner_Name;
                        resultItem.mc = M_DBContext.MS_Product.FirstOrDefault(f => f.Product_Id == resultItem.product_Id)?.ProductCategory_Id;


                        var startDate = DateTime.ParseExact(data.goodsIssue_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.goodsIssue_date_To.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.goodsIssue_date = startDate;
                        resultItem.goodsIssue_date_To = endDate;

                        result.Add(resultItem);
                    }
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report6\\Report6.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report6");
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


        public string ExportExcel(Report6ViewModel data, string rootPath = "")
        {
            var GI_DBContext = new GIDbContext();
            var PlanGI_DBContext = new PlanGIDbContext();
            var GRI_DBContext = new GRDbContext();
            var PlanGR_DBContext = new PlanGRDbContext();
            var M_DBContext = new MasterDataDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report6ViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("Report6");

            try
            {

                var queryGI = GI_DBContext.View_RPT6_GIL.AsQueryable();
                var queryPlanGI = PlanGI_DBContext.im_PlanGoodsIssue.AsQueryable();
                var queryGRI = GRI_DBContext.View_GoodssReceiveItem_RPT_6.AsQueryable();
                var queryPlanGR = PlanGR_DBContext.IM_PlanGoodsReceive.AsQueryable();

                if (!string.IsNullOrEmpty(data.create_By))
                {
                    queryGI = queryGI.Where(c => c.Create_By == data.create_By);
                }

                if (!string.IsNullOrEmpty(data.userAssign))
                {
                    queryGI = queryGI.Where(c => c.Picking_By == data.userAssign);
                }

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryGI = queryGI.Where(c => c.Product_Id == data.product_Id);
                }

                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    queryGI = queryGI.Where(c => c.GoodsIssue_No == data.goodsIssue_No);
                }

                if (!string.IsNullOrEmpty(data.goodsIssue_date) && !string.IsNullOrEmpty(data.goodsIssue_date_To))
                {
                    var dateStart = data.goodsIssue_date.toBetweenDate();
                    var dateEnd = data.goodsIssue_date_To.toBetweenDate();
                    queryGI = queryGI.Where(c => c.GoodsIssue_Date >= dateStart.start && c.GoodsIssue_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.goodsIssue_date))
                {
                    var goodsIssue_date_From = data.goodsIssue_date.toBetweenDate();
                    queryGI = queryGI.Where(c => c.GoodsIssue_Date >= goodsIssue_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.goodsIssue_date_To))
                {
                    var goodsIssue_date_To = data.goodsIssue_date_To.toBetweenDate();
                    queryGI = queryGI.Where(c => c.GoodsIssue_Date <= goodsIssue_date_To.start);
                }

                var queryRPT_GI = queryGI.ToList();
                var queryRPT_PlanGI = queryPlanGI.ToList();
                var queryRPT_GRI = queryGRI.ToList();
                var queryRPT_PlanGR = queryPlanGR.ToList();

                var query = (from GI in queryRPT_GI
                             join PlanGI in queryRPT_PlanGI on GI.Ref_Document_Index equals PlanGI.PlanGoodsIssue_Index into ps
                             from r in ps
                             select new
                             {
                                 gi = GI,
                                 plangi = r
                             }).ToList();
                var query2 = (from GI in query
                              join GRI in queryRPT_GRI on GI.gi.GoodsReceiveItem_Index equals GRI.GoodsReceiveItem_Index into ps
                              from r in ps.DefaultIfEmpty()
                              select new
                              {
                                  gi1 = GI,
                                  gri = r
                              }).ToList();
                var query3 = (from GRI in query2
                              join PlanGR in queryRPT_PlanGR on GRI.gri.Ref_Document_Index equals PlanGR.PlanGoodsReceive_Index into ps
                              from r in ps.DefaultIfEmpty()
                              select new
                              {
                                  gi2 = GRI,
                                  plangr = r
                              }).ToList();



                if (query3.Count == 0)
                {
                    var resultItem = new Report6ViewModel();

                    var startDate = DateTime.ParseExact(data.goodsIssue_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.goodsIssue_date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.checkQuery = true;
                    resultItem.qty = 0;
                    resultItem.goodsIssue_date = startDate;
                    resultItem.goodsIssue_date_To = endDate;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query3)
                    {

                        var resultItem = new Report6ViewModel();

                        var GI_date = GI_DBContext.IM_GoodsIssue.Find(item?.gi2?.gi1?.gi?.GoodsIssue_Index);

                        if (GI_date != null)
                        {
                            var Date = DateTime.ParseExact(GI_date.GoodsIssue_Date.toString().Substring(0, 8), "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                            resultItem.goodsIssue_Date = Date + " " + GI_date.GoodsIssue_Time;

                        }

                        resultItem.planGoodsIssue_No = item?.gi2?.gi1?.plangi?.PlanGoodsIssue_No;
                        //resultItem.goodsIssue_Date = DateTime.Now.ToString("dd/MM/yyyy", culture);
                        resultItem.product_Id = item?.gi2?.gi1?.gi?.Product_Id;
                        resultItem.product_Name = item?.gi2?.gi1?.gi?.Product_Name;
                        resultItem.goodsIssue_No = item?.gi2?.gi1?.gi?.GoodsIssue_No;
                        resultItem.qty = item?.gi2?.gi1?.gi?.Qty;
                        resultItem.productConversion_Name = item?.gi2?.gi1?.gi?.ProductConversion_Name;
                        resultItem.location_Name = item?.gi2?.gi1?.gi?.Location_Name;
                        resultItem.costCenter_Id = item.gi2.gi1.plangi.CostCenter_Id;
                        resultItem.create_By = item?.gi2?.gi1?.gi?.Create_By;
                        resultItem.userAssign = item?.gi2?.gi1?.gi?.Picking_By;
                        resultItem.vendor_Name = item?.plangr?.Vendor_Name;

                        resultItem.sold_Id = item?.gi2?.gi1?.plangi?.SoldTo_Id;
                        resultItem.sold_Name = item?.gi2?.gi1?.plangi?.SoldTo_Name;
                        resultItem.shipTO_Id = item?.gi2?.gi1?.plangi?.ShipTo_Id;
                        resultItem.shipTO_Name = item?.gi2?.gi1?.plangi?.ShipTo_Name;
                        resultItem.owner_Id = item?.gi2?.gi1?.plangi?.Owner_Id;
                        resultItem.owner_Name = item?.gi2?.gi1?.plangi?.Owner_Name;
                        resultItem.mc = M_DBContext.MS_Product.FirstOrDefault(f => f.Product_Id == resultItem.product_Id)?.ProductCategory_Id;


                        var startDate = DateTime.ParseExact(data.goodsIssue_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.goodsIssue_date_To.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.goodsIssue_date = startDate;
                        resultItem.goodsIssue_date_To = endDate;

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

        #region autoSearchTag
        public List<ItemListViewModel> autoSearchUser(ItemListViewModel data)
        {
            try
            {

                using (var context = new GIDbContext())
                {


                    var query = context.View_RPT6_GIL.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key) && data.key != "-")
                    {
                        query = query.Where(c => c.Create_By.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Create_By }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            id = item.Create_By,
                            name = item.Create_By,
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

        #region autoSearchUserPick
        public List<ItemListViewModel> autoSearchUserPick(ItemListViewModel data)
        {
            try
            {

                using (var context = new GIDbContext())
                {


                    var query = context.View_RPT6_GIL.Where(c => !string.IsNullOrEmpty(c.Picking_By)).AsQueryable();

                    if (!string.IsNullOrEmpty(data.key) && data.key != "-")
                    {
                        query = query.Where(c => c.Picking_By.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Picking_By }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            id = item.Picking_By,
                            name = item.Picking_By,
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
    }
}

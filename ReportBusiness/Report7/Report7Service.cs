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

namespace ReportBusiness.Report7
{
    public class Report7Service
    {

        #region printReport7
        public dynamic printReport7(Report7ViewModel data, string rootPath = "")
        {
            var PlanGI_DBContext = new PlanGIDbContext();
            var GI_DBContext = new GIDbContext();
            var M_DBContext = new MasterDataDbContext();


            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report7ViewModel>();

            try
            {
                var queryGI_DB = GI_DBContext.View_RPT_GI.AsQueryable();
                var PlanGI_DB = PlanGI_DBContext.View_RPT_PlanGI.AsQueryable();

                if (!string.IsNullOrEmpty(data.planGoodsIssue_No))
                {
                    PlanGI_DB = PlanGI_DB.Where(c => c.PlanGoodsIssue_No.Contains(data.planGoodsIssue_No));
                }

                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    queryGI_DB = queryGI_DB.Where(c => c.GoodsIssue_No == data.goodsIssue_No);
                }

                if (!string.IsNullOrEmpty(data.create_By))
                {
                    queryGI_DB = queryGI_DB.Where(c => c.Create_By == data.create_By);
                }

                if (!string.IsNullOrEmpty(data.planGoodsIssue_date) && !string.IsNullOrEmpty(data.planGoodsIssue_date_To))
                {
                    var dateStart = data.planGoodsIssue_date.toBetweenDate();
                    var dateEnd = data.planGoodsIssue_date_To.toBetweenDate();
                    PlanGI_DB = PlanGI_DB.Where(c => c.PlanGoodsIssue_Date >= dateStart.start && c.PlanGoodsIssue_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.planGoodsIssue_date))
                {
                    var planGoodsReceive_date_From = data.planGoodsIssue_date.toBetweenDate();
                    PlanGI_DB = PlanGI_DB.Where(c => c.PlanGoodsIssue_Date >= planGoodsReceive_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.planGoodsIssue_date_To))
                {
                    var planGoodsReceive_date_To = data.planGoodsIssue_date_To.toBetweenDate();
                    PlanGI_DB = PlanGI_DB.Where(c => c.PlanGoodsIssue_Date <= planGoodsReceive_date_To.start);
                }

                var queryGI = queryGI_DB.ToList();
                var PlanGI = PlanGI_DB.ToList();

                var query = (from PGI in PlanGI
                             join GI in queryGI on PGI.PlanGoodsIssueItem_Index equals GI.Ref_DocumentItem_Index into ps
                             from r in ps.DefaultIfEmpty()
                             where (r?.Picking_By ?? "") == ""
                             select new
                             {
                                 PlanGI = PGI,
                                 GI = r
                             }).ToList();


                //if gi no 
                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    query = (from PGI in PlanGI
                             join GI in queryGI on PGI.PlanGoodsIssueItem_Index equals GI.Ref_DocumentItem_Index into ps
                             from r in ps
                             where r.GoodsIssue_No == data.goodsIssue_No
                             && (r?.Picking_By ?? "") == ""
                             select new
                             {
                                 PlanGI = PGI,
                                 GI = r
                             }).ToList();
                }

                if (!string.IsNullOrEmpty(data.create_By))
                {
                    query = (from PGI in PlanGI
                             join GI in queryGI on PGI.PlanGoodsIssueItem_Index equals GI.Ref_DocumentItem_Index into ps
                             from r in ps
                             where r.Create_By == data.create_By
                             && (r?.Picking_By ?? "") == ""
                             orderby PGI.PlanGoodsIssue_No descending, r.GoodsIssue_No descending
                             select new
                             {
                                 PlanGI = PGI,
                                 GI = r
                             }).ToList();
                }


                if (query.Count == 0)
                {
                    var resultItem = new Report7ViewModel();

                    var startDate = DateTime.ParseExact(data.planGoodsIssue_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.planGoodsIssue_date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.checkQuery = true;
                    resultItem.qtyGI = 0;
                    resultItem.SumQty = 0;
                    resultItem.planGoodsIssue_date = startDate;
                    resultItem.planGoodsIssue_date_To = endDate;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {

                        string date = item.PlanGI.PlanGoodsIssue_Date.toString();
                        string planGIDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new Report7ViewModel();
                        resultItem.planGoodsIssue_No = item.PlanGI.PlanGoodsIssue_No;



                        resultItem.planGoodsIssue_Date = planGIDate + " " + PlanGI_DBContext.im_PlanGoodsIssue.Find(item.PlanGI.PlanGoodsIssue_Index)?.PlanGoodsIssue_Time;
                        resultItem.product_Id = item.PlanGI.Product_Id;
                        resultItem.product_Name = item.PlanGI.Product_Name;
                        resultItem.productConversion_Name = item.PlanGI.ProductConversion_Name;
                        resultItem.costCenter_Id = item.PlanGI.CostCenter_Id;
                        resultItem.costCenter_Name = item.PlanGI.CostCenter_Name;
                        resultItem.qtyPGI = item.PlanGI.Qty;
                        if (item.GI == null)
                        {
                            resultItem.goodsIssue_No = null;
                            resultItem.qtyGI = null;
                            resultItem.SumQty = item.PlanGI.Qty;
                            resultItem.productConversion_Name_SumQty = null;



                        }
                        else
                        {
                            resultItem.goodsIssue_No = item.GI.GoodsIssue_No;
                            resultItem.qtyGI = item.GI.Qty;
                            resultItem.SumQty = item.PlanGI.Qty - item.GI.Qty;
                            resultItem.create_By = item.GI.Task_Create;
                            resultItem.user_Assign = item.GI.Picking_By;
                            resultItem.location_Id = item.GI.Location_Id;
                            resultItem.location_Name = item.GI.Location_Name;
                            resultItem.picking_By = item.GI.Picking_By;
                        }
                        resultItem.costCenter_Name = null;

                        resultItem.mc = M_DBContext.MS_Product.FirstOrDefault(f => f.Product_Id == resultItem.product_Id)?.ProductCategory_Id;
                        resultItem.sold_Id = item.PlanGI.SoldTo_Id;
                        resultItem.sold_Name = item.PlanGI.SoldTo_Name;
                        resultItem.shipTO_Id = item.PlanGI.ShipTo_Id;
                        resultItem.shipTO_Name = item.PlanGI.ShipTo_Name;
                        resultItem.owner_Id = item.PlanGI.Owner_Id;
                        resultItem.owner_Name = item.PlanGI.Owner_Name;






                        var startDate = DateTime.ParseExact(data.planGoodsIssue_date.Substring(0, 8), "yyyyMMdd",
                       System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.planGoodsIssue_date_To.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.planGoodsIssue_date = startDate;
                        resultItem.planGoodsIssue_date_To = endDate;

                        result.Add(resultItem);
                    }
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report7\\Report7.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report7");
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

        public string ExportExcel(Report7ViewModel data, string rootPath = "")
        {
            var PlanGI_DBContext = new PlanGIDbContext();
            var GI_DBContext = new GIDbContext();
            var M_DBContext = new MasterDataDbContext();


            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report7ViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("Report7");

            try
            {

                var queryGI_DB = GI_DBContext.View_RPT_GI.AsQueryable();
                var PlanGI_DB = PlanGI_DBContext.View_RPT_PlanGI.AsQueryable();

                if (!string.IsNullOrEmpty(data.planGoodsIssue_No))
                {
                    PlanGI_DB = PlanGI_DB.Where(c => c.PlanGoodsIssue_No.Contains(data.planGoodsIssue_No));
                }

                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    queryGI_DB = queryGI_DB.Where(c => c.GoodsIssue_No == data.goodsIssue_No);
                }

                if (!string.IsNullOrEmpty(data.create_By))
                {
                    queryGI_DB = queryGI_DB.Where(c => c.Create_By == data.create_By);
                }

                if (!string.IsNullOrEmpty(data.planGoodsIssue_date) && !string.IsNullOrEmpty(data.planGoodsIssue_date_To))
                {
                    var dateStart = data.planGoodsIssue_date.toBetweenDate();
                    var dateEnd = data.planGoodsIssue_date_To.toBetweenDate();
                    PlanGI_DB = PlanGI_DB.Where(c => c.PlanGoodsIssue_Date >= dateStart.start && c.PlanGoodsIssue_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.planGoodsIssue_date))
                {
                    var planGoodsReceive_date_From = data.planGoodsIssue_date.toBetweenDate();
                    PlanGI_DB = PlanGI_DB.Where(c => c.PlanGoodsIssue_Date >= planGoodsReceive_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.planGoodsIssue_date_To))
                {
                    var planGoodsReceive_date_To = data.planGoodsIssue_date_To.toBetweenDate();
                    PlanGI_DB = PlanGI_DB.Where(c => c.PlanGoodsIssue_Date <= planGoodsReceive_date_To.start);
                }

                var queryGI = queryGI_DB.ToList();
                var PlanGI = PlanGI_DB.ToList();

                var query = (from PGI in PlanGI
                             join GI in queryGI on PGI.PlanGoodsIssueItem_Index equals GI.Ref_DocumentItem_Index into ps
                             from r in ps.DefaultIfEmpty()
                             where (r?.Picking_By ?? "") == ""
                             select new
                             {
                                 PlanGI = PGI,
                                 GI = r
                             }).ToList();


                //if gi no 
                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    query = (from PGI in PlanGI
                             join GI in queryGI on PGI.PlanGoodsIssueItem_Index equals GI.Ref_DocumentItem_Index into ps
                             from r in ps
                             where r.GoodsIssue_No == data.goodsIssue_No
                             && (r?.Picking_By ?? "") == ""
                             select new
                             {
                                 PlanGI = PGI,
                                 GI = r
                             }).ToList();
                }

                if (!string.IsNullOrEmpty(data.create_By))
                {
                    query = (from PGI in PlanGI
                             join GI in queryGI on PGI.PlanGoodsIssueItem_Index equals GI.Ref_DocumentItem_Index into ps
                             from r in ps
                             where r.Create_By == data.create_By
                             && (r?.Picking_By ?? "") == ""
                             orderby PGI.PlanGoodsIssue_No descending, r.GoodsIssue_No descending
                             select new
                             {
                                 PlanGI = PGI,
                                 GI = r
                             }).ToList();
                }


                if (query.Count == 0)
                {
                    var resultItem = new Report7ViewModel();

                    var startDate = DateTime.ParseExact(data.planGoodsIssue_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.planGoodsIssue_date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.checkQuery = true;
                    resultItem.qtyGI = 0;
                    resultItem.SumQty = 0;
                    resultItem.planGoodsIssue_date = startDate;
                    resultItem.planGoodsIssue_date_To = endDate;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {

                        string date = item.PlanGI.PlanGoodsIssue_Date.toString();
                        string planGIDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new Report7ViewModel();
                        resultItem.planGoodsIssue_No = item.PlanGI.PlanGoodsIssue_No;

                        resultItem.planGoodsIssue_Date = planGIDate;
                        resultItem.product_Id = item.PlanGI.Product_Id;
                        resultItem.product_Name = item.PlanGI.Product_Name;
                        resultItem.productConversion_Name = item.PlanGI.ProductConversion_Name;
                        resultItem.costCenter_Id = item.PlanGI.CostCenter_Id;
                        resultItem.costCenter_Name = item.PlanGI.CostCenter_Name;
                        resultItem.qtyPGI = item.PlanGI.Qty;
                        if (item.GI == null)
                        {
                            resultItem.goodsIssue_No = null;
                            resultItem.qtyGI = null;
                            resultItem.SumQty = item.PlanGI.Qty;
                            resultItem.productConversion_Name_SumQty = null;

                        }
                        else
                        {
                            resultItem.goodsIssue_No = item.GI.GoodsIssue_No;
                            resultItem.qtyGI = item.GI.Qty;
                            resultItem.SumQty = item.PlanGI.Qty - item.GI.Qty;
                            resultItem.create_By = item.GI.Task_Create;
                            resultItem.user_Assign = item.GI.Picking_By;
                            resultItem.location_Id = item.GI.Location_Id;
                            resultItem.location_Name = item.GI.Location_Name;
                            resultItem.picking_By = item.GI.Picking_By;

                        }
                        resultItem.costCenter_Name = null;

                        resultItem.mc = M_DBContext.MS_Product.FirstOrDefault(f => f.Product_Id == resultItem.product_Id)?.ProductCategory_Id;
                        resultItem.sold_Id = item.PlanGI.SoldTo_Id;
                        resultItem.sold_Name = item.PlanGI.SoldTo_Name;
                        resultItem.shipTO_Id = item.PlanGI.ShipTo_Id;
                        resultItem.shipTO_Name = item.PlanGI.ShipTo_Name;
                        resultItem.owner_Id = item.PlanGI.Owner_Id;
                        resultItem.owner_Name = item.PlanGI.Owner_Name;






                        var startDate = DateTime.ParseExact(data.planGoodsIssue_date.Substring(0, 8), "yyyyMMdd",
                       System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.planGoodsIssue_date_To.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.planGoodsIssue_date = startDate;
                        resultItem.planGoodsIssue_date_To = endDate;

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


                    var query = context.View_RPT_GI.AsQueryable();

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
    }
}

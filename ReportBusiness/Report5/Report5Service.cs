using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using PlanGRBusiness.Libs;
using ReportBusiness.Report5;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportBusiness.Report5
{
    public class Report5Service
    {

        #region printReport5
        public dynamic printReport5(Report5ViewModel data, string rootPath = "")
        {
            var GI_DBContext = new GIDbContext();
            var PlanGI_DBContext = new PlanGIDbContext();
            var GR_DBContext = new GRDbContext();
            var PlanGR_DBContext = new PlanGRDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report5ViewModel>();

            try
            {
                var queryGI = GI_DBContext.View_RPT5_GI.AsQueryable();
                var queryPlanGI = PlanGI_DBContext.View_RPT5_PlanGI.AsQueryable();
                var queryGRI = GR_DBContext.View_RPT5_GRI.AsQueryable();
                var queryPlanGR = PlanGR_DBContext.IM_PlanGoodsReceive.AsQueryable();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryGI = queryGI.Where(c => c.Product_Id == data.product_Id);
                }
                
                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    queryGI = queryGI.Where(c => c.GoodsIssue_No == data.goodsIssue_No);
                }

                if(!string.IsNullOrEmpty(data.create_By))
                {
                    queryGI = queryGI.Where(c => c.Create_By == data.create_By);
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

                var GiJoinInbound = (from GI in queryRPT_GI
                                     join GRI in queryRPT_GRI on GI.GoodsReceiveItem_Index equals GRI.GoodsReceiveItem_Index into GiJoinGri
                                     from GGRI in GiJoinGri.DefaultIfEmpty()
                                     join PlanGR in queryRPT_PlanGR on GGRI.Ref_Document_Index equals PlanGR.PlanGoodsReceive_Index into GGRIJoinPlanGR
                                     from i in GGRIJoinPlanGR.DefaultIfEmpty()
                                     select new
                                     {
                                         gi=GI,
                                         plangr = i
                                     }).ToList();

                var query = (from GI in GiJoinInbound
                             join PlanGI in queryRPT_PlanGI on GI.gi.Ref_Document_Index equals PlanGI.PlanGoodsIssue_Index into ps
                             from r in ps
                             orderby GI.gi.GoodsIssue_Date,GI.gi.GoodsIssue_Time descending
                             select new
                             {
                                 gi = GI.gi,
                                 plangi = r,
                                 plangr = GI.plangr
                             }).ToList();

                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                     query = (from GI in GiJoinInbound
                                 join PlanGI in queryRPT_PlanGI on GI.gi.Ref_Document_Index equals PlanGI.PlanGoodsIssue_Index into ps
                                 from r in ps
                                 where GI.gi.GoodsIssue_No == data.goodsIssue_No
                              orderby GI.gi.GoodsIssue_Date, GI.gi.GoodsIssue_Time descending
                                 select new
                                 {
                                     gi = GI.gi,
                                     plangi = r,
                                     plangr = GI.plangr
                                 }).ToList();
                }

                if (!string.IsNullOrEmpty(data.create_By))
                {
                    query = (from GI in GiJoinInbound
                             join PlanGI in queryRPT_PlanGI on GI.gi.Ref_Document_Index equals PlanGI.PlanGoodsIssue_Index into ps
                             from r in ps
                             where GI.gi.Create_By == data.create_By 
                             orderby GI.gi.GoodsIssue_Date, GI.gi.GoodsIssue_Time descending
                             select new
                             {
                                 gi = GI.gi,
                                 plangi = r,
                                 plangr = GI.plangr
                             }).ToList();
                }

                if (query.Count == 0)
                {
                    var resultItem = new Report5ViewModel();
 
                    var startDate = DateTime.ParseExact(data.goodsIssue_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.goodsIssue_date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.checkQuery = true;
                    resultItem.product_Id = data.product_Id;
                    resultItem.product_Name = data.product_Name;
                    resultItem.qty = 0;
                    resultItem.goodsIssue_date = startDate;
                    resultItem.goodsIssue_date_To = endDate;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {

                        string date = item.gi.GoodsIssue_Date.toString();
                        string GIDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new Report5ViewModel();

                        resultItem.planGoodsIssue_No = item.plangi.PlanGoodsIssue_No;
                        resultItem.goodsIssue_Date = GIDate;
                        resultItem.product_Id = item.gi.Product_Id;
                        resultItem.product_Name = item.gi.Product_Name;
                        resultItem.goodsIssue_Time = item.gi.GoodsIssue_Time;
                        resultItem.goodsIssue_No = item.gi.GoodsIssue_No;
                        resultItem.documentRef_No2 = item.gi.DocumentRef_No2;
                        resultItem.qty = item.gi.Qty;
                        resultItem.productConversion_Name = item.gi.ProductConversion_Name;
                        resultItem.location_Name = item.gi.Location_Name;
                        resultItem.create_By = item.gi.Create_By;
                        resultItem.userAssign = item.gi.Picking_By;
                        if (item.plangi == null)
                        {
                            resultItem.costCenter_Id = null;
                            resultItem.costCenter_Name = null;
                            resultItem.vendor_Id = null;
                            resultItem.vendor_Name = null;
                        }
                        else
                        {
                            resultItem.costCenter_Id = item.plangi.CostCenter_Id;
                            resultItem.costCenter_Name = item.plangi.CostCenter_Name;
                            resultItem.vendor_Id = item.plangr.Vendor_Id;
                            resultItem.vendor_Name = item.plangr.Vendor_Name;
                        }

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
                //var reportPath = rootPath + "\\ReportBusiness\\Report5\\Report5.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report5");
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

        public string ExportExcel(Report5ViewModel data, string rootPath = "")
        {
            var GI_DBContext = new GIDbContext();
            var PlanGI_DBContext = new PlanGIDbContext();
            var GR_DBContext = new GRDbContext();
            var PlanGR_DBContext = new PlanGRDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report5ViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("Report5");

            try
            {


                var queryGI = GI_DBContext.View_RPT5_GI.AsQueryable();
                var queryPlanGI = PlanGI_DBContext.View_RPT5_PlanGI.AsQueryable();
                var queryGRI = GR_DBContext.View_RPT5_GRI.AsQueryable();
                var queryPlanGR = PlanGR_DBContext.IM_PlanGoodsReceive.AsQueryable();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryGI = queryGI.Where(c => c.Product_Id == data.product_Id);
                }

                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    queryGI = queryGI.Where(c => c.GoodsIssue_No == data.goodsIssue_No);
                }

                if (!string.IsNullOrEmpty(data.create_By))
                {
                    queryGI = queryGI.Where(c => c.Create_By == data.create_By);
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

                var GiJoinInbound = (from GI in queryRPT_GI
                                     join GRI in queryRPT_GRI on GI.GoodsReceiveItem_Index equals GRI.GoodsReceiveItem_Index into GiJoinGri
                                     from GGRI in GiJoinGri.DefaultIfEmpty()
                                     join PlanGR in queryRPT_PlanGR on GGRI.Ref_Document_Index equals PlanGR.PlanGoodsReceive_Index into GGRIJoinPlanGR
                                     from i in GGRIJoinPlanGR.DefaultIfEmpty()
                                     select new
                                     {
                                         gi = GI,
                                         plangr = i
                                     }).ToList();

                var query = (from GI in GiJoinInbound
                             join PlanGI in queryRPT_PlanGI on GI.gi.Ref_Document_Index equals PlanGI.PlanGoodsIssue_Index into ps
                             from r in ps
                             orderby GI.gi.GoodsIssue_Date, GI.gi.GoodsIssue_Time descending
                             select new
                             {
                                 gi = GI.gi,
                                 plangi = r,
                                 plangr = GI.plangr
                             }).ToList();

                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    query = (from GI in GiJoinInbound
                             join PlanGI in queryRPT_PlanGI on GI.gi.Ref_Document_Index equals PlanGI.PlanGoodsIssue_Index into ps
                             from r in ps
                             where GI.gi.GoodsIssue_No == data.goodsIssue_No
                             orderby GI.gi.GoodsIssue_Date, GI.gi.GoodsIssue_Time descending
                             select new
                             {
                                 gi = GI.gi,
                                 plangi = r,
                                 plangr = GI.plangr
                             }).ToList();
                }

                if (!string.IsNullOrEmpty(data.create_By))
                {
                    query = (from GI in GiJoinInbound
                             join PlanGI in queryRPT_PlanGI on GI.gi.Ref_Document_Index equals PlanGI.PlanGoodsIssue_Index into ps
                             from r in ps
                             where GI.gi.Create_By == data.create_By
                             orderby GI.gi.GoodsIssue_Date, GI.gi.GoodsIssue_Time descending
                             select new
                             {
                                 gi = GI.gi,
                                 plangi = r,
                                 plangr = GI.plangr
                             }).ToList();
                }

                if (query.Count == 0)
                {
                    var resultItem = new Report5ViewModel();

                    var startDate = DateTime.ParseExact(data.goodsIssue_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.goodsIssue_date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.checkQuery = true;
                    resultItem.product_Id = data.product_Id;
                    resultItem.product_Name = data.product_Name;
                    resultItem.qty = 0;
                    resultItem.goodsIssue_date = startDate;
                    resultItem.goodsIssue_date_To = endDate;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {

                        string date = item.gi.GoodsIssue_Date.toString();
                        string GIDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new Report5ViewModel();

                        resultItem.planGoodsIssue_No = item.plangi.PlanGoodsIssue_No;
                        resultItem.goodsIssue_Date = GIDate;
                        resultItem.product_Id = item.gi.Product_Id;
                        resultItem.product_Name = item.gi.Product_Name;
                        resultItem.goodsIssue_Time = item.gi.GoodsIssue_Time;
                        resultItem.goodsIssue_No = item.gi.GoodsIssue_No;
                        resultItem.documentRef_No2 = item.gi.DocumentRef_No2;
                        resultItem.qty = item.gi.Qty;
                        resultItem.productConversion_Name = item.gi.ProductConversion_Name;
                        resultItem.location_Name = item.gi.Location_Name;
                        resultItem.create_By = item.gi.Create_By;
                        resultItem.userAssign = item.gi.Picking_By;
                        if (item.plangi == null)
                        {
                            resultItem.costCenter_Id = null;
                            resultItem.costCenter_Name = null;
                            resultItem.vendor_Id = null;
                            resultItem.vendor_Name = null;
                        }
                        else
                        {
                            resultItem.costCenter_Id = item.plangi.CostCenter_Id;
                            resultItem.costCenter_Name = item.plangi.CostCenter_Name;
                            resultItem.vendor_Id = item.plangr.Vendor_Id;
                            resultItem.vendor_Name = item.plangr.Vendor_Name;
                        }

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

    }
}

using AspNetCore.Reporting;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlanGRBusiness.Libs;
using Common.Utils;
using ReportBusiness.ReportRetrun;
using AspNetCore.Reporting.ReportExecutionService;
using Business.Library;
using System.IO;

namespace ReportBusiness.ReportGoodsReceive
{
    public class ReportRetrunService
    {

        #region printReportRetrun
        public string printReportRetrun(ReportRetrunViewModel data, string rootPath = "")
        {
            try
            {
                string saveLocation = "";
                using (var GR_DB = new GRDbContext())
                {
                    var queryGR = GR_DB.View_PrintOutRetrun.AsQueryable();
                    var result = new List<ReportRetrunViewModel>();
                    if (!string.IsNullOrEmpty(data.goodsReceive_No))
                    {
                        queryGR = queryGR.Where(c => c.GoodsReceive_No.Contains(data.goodsReceive_No));
                    }

                    if (!string.IsNullOrEmpty(data.planGoodsReceive_Date_From) && !string.IsNullOrEmpty(data.planGoodsReceive_Date_To))
                    {
                        var dateStart = data.planGoodsReceive_Date_From.toBetweenDate();
                        var dateEnd = data.planGoodsReceive_Date_To.toBetweenDate();
                        queryGR = queryGR.Where(c => c.PlanGoodsReceive_Date >= dateStart.start && c.PlanGoodsReceive_Date <= dateEnd.end);
                    }
                    else if (!string.IsNullOrEmpty(data.planGoodsReceive_Date_From))
                    {
                        var goodsReceive_date_From = data.planGoodsReceive_Date_From.toBetweenDate();
                        queryGR = queryGR.Where(c => c.PlanGoodsReceive_Date >= goodsReceive_date_From.start);
                    }
                    else if (!string.IsNullOrEmpty(data.planGoodsReceive_Date_To))
                    {
                        var goodsReceive_date_To = data.planGoodsReceive_Date_To.toBetweenDate();
                        queryGR = queryGR.Where(c => c.PlanGoodsReceive_Date <= goodsReceive_date_To.start);
                    }

                    if (queryGR.Count() == 0)
                    {
                        var resultItem = new ReportRetrunViewModel();
                        resultItem.planGoodsReceive_Date = DateTime.Now.ToString("dd/MM/yyyy");
                        resultItem.planGoodsReceive_Date_From = data.planGoodsReceive_Date_From != null ? data.planGoodsReceive_Date_From.toDate().sParse<DateTime>().ToString("dd/MM/yyyy") : "";
                        resultItem.planGoodsReceive_Date_To = data.planGoodsReceive_Date_To != null ? data.planGoodsReceive_Date_To.toDate().sParse<DateTime>().ToString("dd/MM/yyyy") : "";

                        result.Add(resultItem);
                    }

                    var query = queryGR.OrderBy(o => o.PlanGoodsReceive_No).ToList();
                    long? isuse = 0;
                    foreach (var item in query)
                    {
                        var resultItem = new ReportRetrunViewModel();

                        if (isuse != item.Number)
                        {
                            isuse = item.Number;

                            resultItem.planGoodsReceive_Date = DateTime.Now.ToString("dd/MM/yyyy");
                            resultItem.planGoodsReceive_Date_From = data.planGoodsReceive_Date_From != null ? data.planGoodsReceive_Date_From.toDate().sParse<DateTime>().ToString("dd/MM/yyyy") : "";
                            resultItem.planGoodsReceive_Date_To = data.planGoodsReceive_Date_To != null ? data.planGoodsReceive_Date_To.toDate().sParse<DateTime>().ToString("dd/MM/yyyy") : "";
                            resultItem.plant = item.Plant;
                            resultItem.planGoodsReceive_No = item.PlanGoodsReceive_No;
                            resultItem.goodsReceive_No = item.GoodsReceive_No;
                            resultItem.product_Id = item.Product_Id;
                            resultItem.product_Name = item.Product_Name;
                            resultItem.unit = item.Unit;
                            resultItem.qty_WMS = item.Qty_WMS;
                            resultItem.qty_SAP = item.Qty_SAP;
                            resultItem.status = item.Status;
                        }
                        else
                        {

                            resultItem.product_Id = item.Product_Id;
                            resultItem.product_Name = item.Product_Name;
                            resultItem.unit = item.Unit;
                            resultItem.qty_WMS = item.Qty_WMS;
                            resultItem.qty_SAP = item.Qty_SAP;
                            resultItem.status = item.Status;
                        }

                        result.Add(resultItem);
                    }
                    result.ToList();
                    rootPath = rootPath.Replace("\\ReportAPI", "");
                    //var reportPath = rootPath + "\\ReportBusiness\\ReportRetrun\\ReportRetrun.rdlc";
                    //var reportPath = rootPath + "\\ReportRetrun\\ReportRetrun.rdlc";
                    var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportRetrun");

                    LocalReport report = new LocalReport(reportPath);


                    report.AddDataSource("DataSet1", result);

                    System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    string fileName = "";
                    string fullPath = "";
                    fileName = "tmpReport" + DateTime.Now.ToString("yyyyMMddHHmmss");

                    var renderedBytes = report.Execute(RenderType.Pdf);

                    Utils objReport = new Utils();
                    fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".pdf", rootPath);
                    saveLocation = objReport.PhysicalPath(fileName + ".pdf", rootPath);
                }

                return saveLocation;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        public string ExportExcel(ReportRetrunViewModel data, string rootPath = "")
        {

            var GR_DB = new GRDbContext();

            var result = new List<ReportRetrunViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportRetrun");

            try
            {

                var queryGR = GR_DB.View_PrintOutRetrun.AsQueryable();
                if (!string.IsNullOrEmpty(data.goodsReceive_No))
                {
                    queryGR = queryGR.Where(c => c.GoodsReceive_No.Contains(data.goodsReceive_No));
                }

                if (!string.IsNullOrEmpty(data.planGoodsReceive_Date_From) && !string.IsNullOrEmpty(data.planGoodsReceive_Date_To))
                {
                    var dateStart = data.planGoodsReceive_Date_From.toBetweenDate();
                    var dateEnd = data.planGoodsReceive_Date_To.toBetweenDate();
                    queryGR = queryGR.Where(c => c.PlanGoodsReceive_Date >= dateStart.start && c.PlanGoodsReceive_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.planGoodsReceive_Date_From))
                {
                    var goodsReceive_date_From = data.planGoodsReceive_Date_From.toBetweenDate();
                    queryGR = queryGR.Where(c => c.PlanGoodsReceive_Date >= goodsReceive_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.planGoodsReceive_Date_To))
                {
                    var goodsReceive_date_To = data.planGoodsReceive_Date_To.toBetweenDate();
                    queryGR = queryGR.Where(c => c.PlanGoodsReceive_Date <= goodsReceive_date_To.start);
                }

                if (queryGR.Count() == 0)
                {
                    var resultItem = new ReportRetrunViewModel();
                    resultItem.planGoodsReceive_Date = DateTime.Now.ToString("dd/MM/yyyy");
                    resultItem.planGoodsReceive_Date_From = data.planGoodsReceive_Date_From != null ? data.planGoodsReceive_Date_From.toDate().sParse<DateTime>().ToString("dd/MM/yyyy") : "";
                    resultItem.planGoodsReceive_Date_To = data.planGoodsReceive_Date_To != null ? data.planGoodsReceive_Date_To.toDate().sParse<DateTime>().ToString("dd/MM/yyyy") : "";

                    result.Add(resultItem);
                }

                var query = queryGR.OrderBy(o => o.PlanGoodsReceive_No).ToList();
                long? isuse = 0;
                foreach (var item in query)
                {
                    var resultItem = new ReportRetrunViewModel();

                    if (isuse != item.Number)
                    {
                        isuse = item.Number;

                        resultItem.planGoodsReceive_Date = DateTime.Now.ToString("dd/MM/yyyy");
                        resultItem.planGoodsReceive_Date_From = data.planGoodsReceive_Date_From != null ? data.planGoodsReceive_Date_From.toDate().sParse<DateTime>().ToString("dd/MM/yyyy") : "";
                        resultItem.planGoodsReceive_Date_To = data.planGoodsReceive_Date_To != null ? data.planGoodsReceive_Date_To.toDate().sParse<DateTime>().ToString("dd/MM/yyyy") : "";
                        resultItem.plant = item.Plant;
                        resultItem.planGoodsReceive_No = item.PlanGoodsReceive_No;
                        resultItem.goodsReceive_No = item.GoodsReceive_No;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.unit = item.Unit;
                        resultItem.qty_WMS = item.Qty_WMS;
                        resultItem.qty_SAP = item.Qty_SAP;
                        resultItem.status = item.Status;
                    }
                    else
                    {

                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.unit = item.Unit;
                        resultItem.qty_WMS = item.Qty_WMS;
                        resultItem.qty_SAP = item.Qty_SAP;
                        resultItem.status = item.Status;
                    }

                    result.Add(resultItem);
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

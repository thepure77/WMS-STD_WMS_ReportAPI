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
using ReportBusiness.Report4;
using System.IO;

namespace ReportBusiness.Report4
{
    public class Report4Service
    {
       
        #region report4
        public string PrintReport4(Report4ViewModel data, string rootPath = "")
        {

            var GR_DB = new GRDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            
            try
            {
                var queryGR = GR_DB.View_RPT_GRTI.AsQueryable();
                var result = new List<Report4ViewModel>();
                if (!string.IsNullOrEmpty(data.goodsReceive_No))
                {
                    queryGR = queryGR.Where(c => c.GoodsReceive_No.Contains(data.goodsReceive_No));
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryGR = queryGR.Where(c => c.Product_Id.Contains(data.product_Id));
                }
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    queryGR = queryGR.Where(c => c.Tag_No.Contains(data.tag_No));
                }
                if (!string.IsNullOrEmpty(data.goodsReceive_date) && !string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var dateStart = data.goodsReceive_date.toBetweenDate();
                    var dateEnd = data.goodsReceive_date_To.toBetweenDate();
                    queryGR = queryGR.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date))
                {
                    var goodsReceive_date_From = data.goodsReceive_date.toBetweenDate();
                    queryGR = queryGR.Where(c => c.GoodsReceive_Date >= goodsReceive_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var goodsReceive_date_To = data.goodsReceive_date_To.toBetweenDate();
                    queryGR = queryGR.Where(c => c.GoodsReceive_Date <= goodsReceive_date_To.start);
                }

            
                string startDate = data.goodsReceive_date;
                string GRDateStart = DateTime.ParseExact(startDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                string endDate = data.goodsReceive_date_To;
                string GRDateEnd = DateTime.ParseExact(endDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                var query = queryGR.OrderBy(x => x.GoodsReceive_Date).ToList();

                if (query.Count == 0)
                {
                    var resultItem = new Report4ViewModel();

                    resultItem.checkQuery = true;
                    resultItem.goodsReceive_date = GRDateStart;
                    resultItem.goodsReceive_date_To = GRDateEnd;
                    result.Add(resultItem);
                }
                else
                {

                    foreach (var item in query)
                    {
                        string date = item.GoodsReceive_Date.toString();
                        string GoodReceiveDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new Report4ViewModel();
                        resultItem.goodsReceive_Index = item.GoodsReceive_Index;
                        resultItem.goodsReceive_No = item.GoodsReceive_No;
                        resultItem.goodsReceive_Date = GoodReceiveDate;
                        resultItem.putawayLocation_Id = item.PutawayLocation_Id;
                        resultItem.suggest_Location_Id = item.Suggest_Location_Id;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.qty = Convert.ToInt32(item.Qty);
                        resultItem.tag_No = item.Tag_No;
                        resultItem.create_Date = DateTime.Now.ToString("dd/MM/yyyy", culture);
                        resultItem.goodsReceive_date = GRDateStart;
                        resultItem.goodsReceive_date_To = GRDateEnd;
                        if (resultItem.suggest_Location_Id == resultItem.putawayLocation_Id)
                        {
                            resultItem.operation = "จัดการเก็บตามตำแหน่งแนะนำ";
                        }
                        else
                        {
                            resultItem.operation = "เลือกเอง";
                        }


                        result.Add(resultItem);
                    }
                    result.ToList();
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report4\\Report4.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report4");
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

        public string ExportExcel(Report4ViewModel data, string rootPath = "")
        {
            var GR_DB = new GRDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();


            var result = new List<Report4ViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("Report4");

            try
            {
                var queryGR = GR_DB.View_RPT_GRTI.AsQueryable();
                if (!string.IsNullOrEmpty(data.goodsReceive_No))
                {
                    queryGR = queryGR.Where(c => c.GoodsReceive_No.Contains(data.goodsReceive_No));
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryGR = queryGR.Where(c => c.Product_Id.Contains(data.product_Id));
                }
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    queryGR = queryGR.Where(c => c.Tag_No.Contains(data.tag_No));
                }
                if (!string.IsNullOrEmpty(data.goodsReceive_date) && !string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var dateStart = data.goodsReceive_date.toBetweenDate();
                    var dateEnd = data.goodsReceive_date_To.toBetweenDate();
                    queryGR = queryGR.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date))
                {
                    var goodsReceive_date_From = data.goodsReceive_date.toBetweenDate();
                    queryGR = queryGR.Where(c => c.GoodsReceive_Date >= goodsReceive_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var goodsReceive_date_To = data.goodsReceive_date_To.toBetweenDate();
                    queryGR = queryGR.Where(c => c.GoodsReceive_Date <= goodsReceive_date_To.start);
                }


                string startDate = data.goodsReceive_date;
                string GRDateStart = DateTime.ParseExact(startDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                string endDate = data.goodsReceive_date_To;
                string GRDateEnd = DateTime.ParseExact(endDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                var query = queryGR.OrderBy(x => x.GoodsReceive_Date).ToList();

                if (query.Count == 0)
                {
                    var resultItem = new Report4ViewModel();

                    resultItem.checkQuery = true;
                    resultItem.goodsReceive_date = GRDateStart;
                    resultItem.goodsReceive_date_To = GRDateEnd;
                    result.Add(resultItem);
                }
                else
                {

                    foreach (var item in query)
                    {
                        string date = item.GoodsReceive_Date.toString();
                        string GoodReceiveDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new Report4ViewModel();
                        resultItem.goodsReceive_Index = item.GoodsReceive_Index;
                        resultItem.goodsReceive_No = item.GoodsReceive_No;
                        resultItem.goodsReceive_Date = GoodReceiveDate;
                        resultItem.putawayLocation_Id = item.PutawayLocation_Id;
                        resultItem.suggest_Location_Id = item.Suggest_Location_Id;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.qty = Convert.ToInt32(item.Qty);
                        resultItem.tag_No = item.Tag_No;
                        resultItem.create_Date = DateTime.Now.ToString("dd/MM/yyyy", culture);
                        resultItem.goodsReceive_date = GRDateStart;
                        resultItem.goodsReceive_date_To = GRDateEnd;
                        if (resultItem.suggest_Location_Id == resultItem.putawayLocation_Id)
                        {
                            resultItem.operation = "จัดการเก็บตามตำแหน่งแนะนำ";
                        }
                        else
                        {
                            resultItem.operation = "เลือกเอง";
                        }


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

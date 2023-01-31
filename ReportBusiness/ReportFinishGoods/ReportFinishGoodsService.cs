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
using ReportBusiness.ReportFinishGoods;
using System.IO;
using MasterDataBusiness.ViewModels;

namespace ReportBusiness.ReportFinishGoodsService
{
    public class ReportFinishGoodsService
    {

        #region ReportFinishGoodsService
        public string printReportFinishGoods(ReportFinishGoodsViewModel data, string rootPath = "")
        {

            var GR_DBContext = new GRDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var queryWhere = GR_DBContext.View_GoodsReceive_Finish_Good_BOM.AsQueryable();
                var result = new List<ReportFinishGoodsViewModel>();
                if (!string.IsNullOrEmpty(data.Owner_Id))
                {
                    queryWhere = queryWhere.Where(c => c.Owner_Id.Contains(data.Owner_Id));
                }

                if (!string.IsNullOrEmpty(data.GoodsReceive_Date_Start) && !string.IsNullOrEmpty(data.GoodsReceive_Date_End))
                {
                    var dateStart = data.GoodsReceive_Date_Start.toBetweenDate();
                    var dateEnd = data.GoodsReceive_Date_End.toBetweenDate();
                    queryWhere = queryWhere.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.GoodsReceive_Date_Start))
                {
                    var binCard_date_From = data.GoodsReceive_Date_Start.toBetweenDate();
                    queryWhere = queryWhere.Where(c => c.GoodsReceive_Date >= binCard_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.GoodsReceive_Date_End))
                {
                    var binCard_date_To = data.GoodsReceive_Date_End.toBetweenDate();
                    queryWhere = queryWhere.Where(c => c.GoodsReceive_Date <= binCard_date_To.start);
                }

                var query = queryWhere.OrderBy(x => x.GoodsReceive_Date).ToList();
                //var query = queryWhere.ToList();


                if (query.Count == 0)
                    {
                        var resultItem = new ReportFinishGoodsViewModel();

                        var startDate = DateTime.ParseExact(data.GoodsReceive_Date_Start.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.GoodsReceive_Date_End.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.GoodsReceive_Date_Start = startDate;
                        resultItem.GoodsReceive_Date_End = endDate;
                        //resultItem.checkQuery = true;


                        result.Add(resultItem);
                    }
                    else
                    {
                        int row = 1;
                        foreach (var item in query)
                        {

                            string date = item.GoodsReceive_Date.toString();
                            string BCDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);


                            var resultItem = new ReportFinishGoodsViewModel();
                            resultItem.Rownum = row;
                            resultItem.GoodsReceive_No = item.GoodsReceive_No;
                            resultItem.GoodsReceive_Date = BCDate;
                            resultItem.Owner_Id = item.Owner_Id;
                            resultItem.Owner_Name = item.Owner_Name;
                            resultItem.BOM_Document = item.BOM_Document;
                            resultItem.ProductConversionBarcode = item.ProductConversionBarcode;
                            resultItem.Product_Id = item.Product_Id;
                            resultItem.Product_Name = item.Product_Name;
                            resultItem.Qty_Bom = item.Qty_Bom;
                            resultItem.ProductConversion_Name = item.ProductConversion_Name;
                            resultItem.PutawayLocation_Id = item.PutawayLocation_Id;

                            var startDate = DateTime.ParseExact(data.GoodsReceive_Date_Start.Substring(0, 8), "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                            var endDate = DateTime.ParseExact(data.GoodsReceive_Date_End.Substring(0, 8), "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                            resultItem.GoodsReceive_Date_Start = startDate;
                            resultItem.GoodsReceive_Date_End = endDate;
                            row++;


                            result.Add(resultItem);
                        }
                        result.ToList();
                    }
                    rootPath = rootPath.Replace("\\ReportAPI", "");
                    //var reportPath = rootPath + "\\ReportBusiness\\Report20\\Report20.rdlc";
                    var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportFinishGoods");
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


                    var query = context.View_GoodsReceive_Finish_Good_BOM.AsQueryable();

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
        public string ExportExcel(ReportFinishGoodsViewModel data, string rootPath = "")
        {

            var GR_DBContext = new GRDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<ReportFinishGoodsViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportFinishGoods");

            try
            {
                var queryWhere = GR_DBContext.View_GoodsReceive_Finish_Good_BOM.AsQueryable();
                if (!string.IsNullOrEmpty(data.Owner_Id))
                {
                    queryWhere = queryWhere.Where(c => c.Owner_Id.Contains(data.Owner_Id));
                }

                if (!string.IsNullOrEmpty(data.GoodsReceive_Date_Start) && !string.IsNullOrEmpty(data.GoodsReceive_Date_End))
                {
                    var dateStart = data.GoodsReceive_Date_Start.toBetweenDate();
                    var dateEnd = data.GoodsReceive_Date_End.toBetweenDate();
                    queryWhere = queryWhere.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.GoodsReceive_Date_Start))
                {
                    var binCard_date_From = data.GoodsReceive_Date_Start.toBetweenDate();
                    queryWhere = queryWhere.Where(c => c.GoodsReceive_Date >= binCard_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.GoodsReceive_Date_End))
                {
                    var binCard_date_To = data.GoodsReceive_Date_End.toBetweenDate();
                    queryWhere = queryWhere.Where(c => c.GoodsReceive_Date <= binCard_date_To.start);
                }

                var query = queryWhere.OrderBy(x => x.GoodsReceive_Date).ToList();
                //var query = queryWhere.ToList();


                if (query.Count == 0)
                {
                    var resultItem = new ReportFinishGoodsViewModel();

                    var startDate = DateTime.ParseExact(data.GoodsReceive_Date_Start.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.GoodsReceive_Date_End.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.GoodsReceive_Date_Start = startDate;
                    resultItem.GoodsReceive_Date_End = endDate;
                    //resultItem.checkQuery = true;


                    result.Add(resultItem);
                }
                else
                {
                    int row = 1;
                    foreach (var item in query)
                    {

                        string date = item.GoodsReceive_Date.toString();
                        string BCDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);


                        var resultItem = new ReportFinishGoodsViewModel();
                        resultItem.Rownum = row;
                        resultItem.GoodsReceive_No = item.GoodsReceive_No;
                        resultItem.GoodsReceive_Date = BCDate;
                        resultItem.Owner_Id = item.Owner_Id;
                        resultItem.Owner_Name = item.Owner_Name;
                        resultItem.BOM_Document = item.BOM_Document;
                        resultItem.ProductConversionBarcode = item.ProductConversionBarcode;
                        resultItem.Product_Id = item.Product_Id;
                        resultItem.Product_Name = item.Product_Name;
                        resultItem.Qty_Bom = item.Qty_Bom;
                        resultItem.ProductConversion_Name = item.ProductConversion_Name;
                        resultItem.PutawayLocation_Id = item.PutawayLocation_Id;

                        var startDate = DateTime.ParseExact(data.GoodsReceive_Date_Start.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.GoodsReceive_Date_End.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.GoodsReceive_Date_Start = startDate;
                        resultItem.GoodsReceive_Date_End = endDate;
                        row++;


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

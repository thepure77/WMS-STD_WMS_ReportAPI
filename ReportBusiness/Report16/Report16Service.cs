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
using ReportBusiness.Report16;
using System.Globalization;
using System.IO;

namespace ReportBusiness.Report16
{
    public class Report16Service
    {

        #region report16
        public string printReport16(Report16ViewModel data, string rootPath = "")
        {
            var MS_DBContext = new MasterDataDbContext();
            var BC_DB = new BinbalanceDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                data.binCard_date = data.binCard_date.Replace("/", "");
                data.binCard_date_To = data.binCard_date_To.Replace("/", "");

                var queryBC = BC_DB.View_RPT16_BinCard.AsQueryable();
                var queryBC_MD = BC_DB.View_RPT16_BinCard_MaxDate.AsQueryable();
                var queryMS_Product = MS_DBContext.MS_Product.AsQueryable();
                var result = new List<Report16ViewModel>();
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryBC = queryBC.Where(c => c.Product_Id.Contains(data.product_Id));
                }
                if (!string.IsNullOrEmpty(data.binCard_date) && !string.IsNullOrEmpty(data.binCard_date_To))
                {
                    var dateStart = data.binCard_date.toBetweenDate();
                    var dateEnd = data.binCard_date_To.toBetweenDate();
                    queryBC = queryBC.Where(c => c.BinCard_Date >= dateStart.start && c.BinCard_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.binCard_date))
                {
                    var binCard_date_From = data.binCard_date.toBetweenDate();
                    queryBC = queryBC.Where(c => c.BinCard_Date >= binCard_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.binCard_date_To))
                {
                    var binCard_date_To = data.binCard_date_To.toBetweenDate();
                    queryBC = queryBC.Where(c => c.BinCard_Date <= binCard_date_To.start);
                }

                string startDate = data.binCard_date;
                string DateStart = DateTime.ParseExact(startDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                string endDate = data.binCard_date_To;
                string DateEnd = DateTime.ParseExact(endDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                var queryRPT_BC = queryBC.ToList();
                var queryRPT_BC_MD = queryBC_MD.ToList();
                var queryProduct = queryMS_Product.ToList();

                //var query = queryBC.Where(c => queryRPT_BC_MD.Select(s => s.Product_Index).Contains(c.Product_Index)).ToList();
                //var queryjoin = (from RPT_BC in queryRPT_BC
                //                 join Product in queryProduct on RPT_BC.Product_Index equals Product.Product_Index
                //                 select new
                //                 {
                //                     RPT_BC,
                //                     Product.ProductType_Name,Product.Ref_No3
                //                 }).ToList();


                var query = (from BC in queryRPT_BC
                             join Product in queryProduct on BC.Product_Index equals Product.Product_Index
                             join BC_MD in queryRPT_BC_MD on BC.Product_Index equals BC_MD.Product_Index 
                             into ps
                             from r in ps
                             orderby r.Product_Id,Product.Product_Id ascending
                             select new
                             {
                                 bc = BC,
                                 bcmd = r,
                                 Product.ProductCategory_Id,
                                 Product.ProductCategory_Name,
                                 Product.Ref_No3
                             }).ToList();

                if (query.Count == 0)
                {
                    var resultItem = new Report16ViewModel();
                    resultItem.checkQuery = true;
                    resultItem.binCard_QtyIn = 0;
                    resultItem.binCard_QtyOut = 0;
                    resultItem.binCard_date = DateStart;
                    resultItem.binCard_date_To = DateEnd;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {
                        string date = item.bcmd.Max_BinCard_Date.toString();
                        string MaxDate = DateTime.ParseExact(date.Substring(0, 14), "yyyyMMddHHmmss",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy HH:mm", culture);
                        var resultItem = new Report16ViewModel();

                        resultItem.product_Id = item.bc.Product_Id;
                        resultItem.product_Name = item.bc.Product_Name;
                        resultItem.productConversion_Name = item.bc.ProductConversion_Name;
                        resultItem.binCard_QtyIn = item.bc.BinCard_QtyIn;
                        resultItem.binCard_QtyOut = item.bc.BinCard_QtyOut;
                        resultItem.Owner_Name = item.bc.Owner_Name;
                        resultItem.MC = item.ProductCategory_Id;
                        resultItem.Size = item.Ref_No3;
                        resultItem.binCard_DateIn = null;
                        resultItem.binCard_DateOut = null;
                        if (item.bc.BinCard_QtyIn > 0)
                        {
                            resultItem.binCard_DateIn =  item?.bc?.BinCard_Date.Value.ToString("dd/MM/yyyy");
                        }
                        if (item.bc.BinCard_QtyOut > 0)
                        {
                            resultItem.binCard_DateOut = item?.bc?.BinCard_Date.Value.ToString("dd/MM/yyyy");
                        }

                        resultItem.binCard_MaxDate = MaxDate;
                        resultItem.binCard_date = DateStart;
                        resultItem.binCard_date_To = DateEnd;

                        resultItem.movement_Type = item.bc.Movement_Type;

                        result.Add(resultItem);
                    }
                    result.ToList();
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report16\\Report16.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report16");
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

        public string ExportExcel(Report16ViewModel data, string rootPath = "")
        {
            var MS_DBContext = new MasterDataDbContext();
            var BC_DB = new BinbalanceDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();


            var result = new List<Report16ViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("Report16");

            try
            {



                data.binCard_date = data.binCard_date.Replace("/", "");
                data.binCard_date_To = data.binCard_date_To.Replace("/", "");

                var queryBC = BC_DB.View_RPT16_BinCard.AsQueryable();
                var queryBC_MD = BC_DB.View_RPT16_BinCard_MaxDate.AsQueryable();
                var queryMS_Product = MS_DBContext.MS_Product.AsQueryable();
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryBC = queryBC.Where(c => c.Product_Id.Contains(data.product_Id));
                }
                if (!string.IsNullOrEmpty(data.binCard_date) && !string.IsNullOrEmpty(data.binCard_date_To))
                {
                    var dateStart = data.binCard_date.toBetweenDate();
                    var dateEnd = data.binCard_date_To.toBetweenDate();
                    queryBC = queryBC.Where(c => c.BinCard_Date >= dateStart.start && c.BinCard_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.binCard_date))
                {
                    var binCard_date_From = data.binCard_date.toBetweenDate();
                    queryBC = queryBC.Where(c => c.BinCard_Date >= binCard_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.binCard_date_To))
                {
                    var binCard_date_To = data.binCard_date_To.toBetweenDate();
                    queryBC = queryBC.Where(c => c.BinCard_Date <= binCard_date_To.start);
                }

                string startDate = data.binCard_date;
                string DateStart = DateTime.ParseExact(startDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                string endDate = data.binCard_date_To;
                string DateEnd = DateTime.ParseExact(endDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                var queryRPT_BC = queryBC.ToList();
                var queryRPT_BC_MD = queryBC_MD.ToList();
                var queryProduct = queryMS_Product.ToList();

                //var query = queryBC.Where(c => queryRPT_BC_MD.Select(s => s.Product_Index).Contains(c.Product_Index)).ToList();
                //var queryjoin = (from RPT_BC in queryRPT_BC
                //                 join Product in queryProduct on RPT_BC.Product_Index equals Product.Product_Index
                //                 select new
                //                 {
                //                     RPT_BC,
                //                     Product.ProductType_Name,Product.Ref_No3
                //                 }).ToList();


                var query = (from BC in queryRPT_BC
                             join Product in queryProduct on BC.Product_Index equals Product.Product_Index
                             join BC_MD in queryRPT_BC_MD on BC.Product_Index equals BC_MD.Product_Index
                             into ps
                             from r in ps
                             orderby r.Product_Id, Product.Product_Id ascending
                             select new
                             {
                                 bc = BC,
                                 bcmd = r,
                                 Product.ProductCategory_Id,
                                 Product.ProductCategory_Name,
                                 Product.Ref_No3
                             }).ToList();

                if (query.Count == 0)
                {
                    var resultItem = new Report16ViewModel();
                    resultItem.checkQuery = true;
                    resultItem.binCard_QtyIn = 0;
                    resultItem.binCard_QtyOut = 0;
                    resultItem.binCard_date = DateStart;
                    resultItem.binCard_date_To = DateEnd;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {
                        string date = item.bcmd.Max_BinCard_Date.toString();
                        string MaxDate = DateTime.ParseExact(date.Substring(0, 14), "yyyyMMddHHmmss",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy HH:mm", culture);
                        var resultItem = new Report16ViewModel();

                        resultItem.product_Id = item.bc.Product_Id;
                        resultItem.product_Name = item.bc.Product_Name;
                        resultItem.productConversion_Name = item.bc.ProductConversion_Name;
                        resultItem.binCard_QtyIn = item.bc.BinCard_QtyIn;
                        resultItem.binCard_QtyOut = item.bc.BinCard_QtyOut;
                        resultItem.Owner_Name = item.bc.Owner_Name;
                        resultItem.MC = item.ProductCategory_Id;
                        resultItem.Size = item.Ref_No3;
                        resultItem.binCard_DateIn = null;
                        resultItem.binCard_DateOut = null;
                        if (item.bc.BinCard_QtyIn > 0)
                        {
                            resultItem.binCard_DateIn = item?.bc?.BinCard_Date.Value.ToString("dd/MM/yyyy");
                        }
                        if (item.bc.BinCard_QtyOut > 0)
                        {
                            resultItem.binCard_DateOut = item?.bc?.BinCard_Date.Value.ToString("dd/MM/yyyy");
                        }

                        resultItem.binCard_MaxDate = MaxDate;
                        resultItem.binCard_date = DateStart;
                        resultItem.binCard_date_To = DateEnd;

                        resultItem.movement_Type = item.bc.Movement_Type;

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

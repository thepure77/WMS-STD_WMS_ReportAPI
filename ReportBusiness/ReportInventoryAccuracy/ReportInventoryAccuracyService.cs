using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using PlanGRBusiness.Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportBusiness.ReportInventoryAccuracy
{
    public class ReportInventoryAccuracyService
    {
        #region printReportInventoryAccuracy
        public dynamic printReportInventoryAccuracy(ReportInventoryAccuracyViewModel data, string rootPath = "")
        {

            //var BB_DBContext = new BinbalanceDbContext();
            //var M_DBContext = new MasterDataDbContext();
            var GR_DBContext = new GRDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<ReportInventoryAccuracyViewModel>();

            try
            {
                
                var query = GR_DBContext.sp_Inventory_Accuracy.FromSql("EXEC sp_Inventory_Accuracy").ToList();
                if (!string.IsNullOrEmpty(data.Product_Id))
                {
                    query = query.Where(c => c.Product_Id.Contains(data.Product_Id)).ToList();
                }
                if (!string.IsNullOrEmpty(data.Product_Lot))
                {
                    query = query.Where(c => c.Product_Lot == data.Product_Lot).ToList();
                }
                if (!string.IsNullOrEmpty(data.Sloc))
                {
                    query = query.Where(c => c.ERP_Location == data.Sloc).ToList();
                }
                
                if (!string.IsNullOrEmpty(data.ItemStatus_Index))
                {
                    query = query.Where(c => c.ItemStatus_Index == Guid.Parse(data.ItemStatus_Index)).ToList();
                }

                query = query.OrderBy(o => o.Product_Id).ToList();

                foreach(var item in query)
                {
                    var resultItem = new ReportInventoryAccuracyViewModel();
                    //resultItem.ItemStatus_Index = item.ItemStatus_Index.ToString();
                    resultItem.ItemStatus_Name = item.ItemStatus_Name;
                    resultItem.Product_Id = item.Product_Id;
                    resultItem.Product_Name = item.Product_Name;
                    resultItem.Product_Lot = item.Product_Lot;
                    resultItem.CBM = item.CBM;
                    resultItem.SU_QtyBal = item.SU_QtyBal;
                    resultItem.SU_QtyReserve = item.SU_QtyReserve;
                    resultItem.SU_QtyOnHand = item.SU_QtyOnHand;
                    resultItem.Per_SU_QtyReserve = item.Per_SU_QtyReserve;
                    resultItem.Per_SU_QtyOnHand = item.Per_SU_QtyOnHand;
                    resultItem.SU_UNIT = item.SU_UNIT;
                    resultItem.ERP_Location = item.ERP_Location;

                    result.Add(resultItem);
                }


                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportInventoryAccuracy");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "ReportInventoryAccuracy" + DateTime.Now.ToString("yyyyMMddHHmmss");

                var renderedBytes = report.Execute(RenderType.Pdf);

                Utils objReport = new Utils();
                fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".pdf", rootPath);
                var saveLocation = objReport.PhysicalPath(fileName + ".pdf", rootPath);
                return saveLocation;


            }
            catch (Exception ex)
            {
                //olog.logging("ReportInventoryAccuracy", ex.Message);
                throw ex;
            }
        }
        #endregion

        public string ExportExcel(ReportInventoryAccuracyViewModel data, string rootPath = "")
        {

            //var M_DBContext = new MasterDataDbContext();
            var GR_DBContext = new GRDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportInventoryAccuracyViewModel>();

            try
            {

                var query = GR_DBContext.sp_Inventory_Accuracy.FromSql("EXEC sp_Inventory_Accuracy").ToList();
                if (!string.IsNullOrEmpty(data.Product_Id))
                {
                    query = query.Where(c => c.Product_Id.Contains(data.Product_Id)).ToList();
                }
                if (!string.IsNullOrEmpty(data.Product_Lot))
                {
                    query = query.Where(c => c.Product_Lot == data.Product_Lot).ToList();
                }
                if (!string.IsNullOrEmpty(data.Sloc))
                {
                    query = query.Where(c => c.ERP_Location == data.Sloc).ToList();
                }

                if (!string.IsNullOrEmpty(data.ItemStatus_Index))
                {
                    query = query.Where(c => c.ItemStatus_Index == Guid.Parse(data.ItemStatus_Index)).ToList();
                }

                query = query.OrderBy(o => o.Product_Id).ToList();

                foreach (var item in query)
                {
                    var resultItem = new ReportInventoryAccuracyViewModel();
                    //resultItem.ItemStatus_Index = item.ItemStatus_Index.ToString();
                    resultItem.ItemStatus_Name = item.ItemStatus_Name;
                    resultItem.Product_Id = item.Product_Id;
                    resultItem.Product_Name = item.Product_Name;
                    resultItem.Product_Lot = item.Product_Lot;
                    resultItem.CBM = item.CBM;
                    resultItem.SU_QtyBal = item.SU_QtyBal;
                    resultItem.SU_QtyReserve = item.SU_QtyReserve;
                    resultItem.SU_QtyOnHand = item.SU_QtyOnHand;
                    resultItem.Per_SU_QtyReserve = item.Per_SU_QtyReserve;
                    resultItem.Per_SU_QtyOnHand = item.Per_SU_QtyOnHand;
                    resultItem.SU_UNIT = item.SU_UNIT;
                    resultItem.ERP_Location = item.ERP_Location;

                    result.Add(resultItem);
                }


                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportInventoryAccuracy");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "ReportInventoryAccuracy" + DateTime.Now.ToString("yyyyMMddHHmmss");

                var renderedBytes = report.Execute(RenderType.Excel);
                //fullPath = saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);

                Utils objReport = new Utils();
                fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);
                var saveLocation = objReport.PhysicalPath(fileName + ".xls", rootPath);
                return saveLocation;


                //var saveLocation = rootPath + fullPath;
                ////File.Delete(saveLocation);
                ////ExcelRefresh(reportPath);
                //return saveLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //public string saveReport(byte[] file, string name, string rootPath)
        //{
        //    var saveLocation = PhysicalPath(name, rootPath);
        //    FileStream fs = new FileStream(saveLocation, FileMode.Create);
        //    BinaryWriter bw = new BinaryWriter(fs);
        //    try
        //    {
        //        try
        //        {
        //            bw.Write(file);
        //        }
        //        finally
        //        {
        //            fs.Close();
        //            bw.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return VirtualPath(name);
        //}

        //public string PhysicalPath(string name, string rootPath)
        //{
        //    var filename = name;
        //    var vPath = ReportPath;
        //    var path = rootPath + vPath;
        //    if (!System.IO.Directory.Exists(path))
        //    {
        //        System.IO.Directory.CreateDirectory(path);
        //    }

        //    var saveLocation = System.IO.Path.Combine(path, filename);
        //    return saveLocation;
        //}
        //public string VirtualPath(string name)
        //{
        //    var filename = name;
        //    var vPath = ReportPath;
        //    vPath = vPath.Replace("~", "");
        //    return vPath + filename;
        //}
        //private string ReportPath
        //{
        //    get
        //    {
        //        var url = "\\ReportGenerator\\";
        //        return url;
        //    }
        //}
    }
}

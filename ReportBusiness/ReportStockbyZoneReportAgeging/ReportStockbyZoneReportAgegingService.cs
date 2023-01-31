using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using PlanGRBusiness.Libs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ReportBusiness.ReportStockbyZoneReportAgeging
{
    public class ReportStockbyZoneReportAgegingService
    {

        #region ReportStockbyZoneReportAgeging
        public string printReportStockbyZoneReportAgeging(ReportStockbyZoneReportAgegingViewModel data, string rootPath = "")
        {

            var db = new MasterDataDbContext();
            var db_temp = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<ReportStockbyZoneReportAgegingViewModel>();
            try
            {
                db.Database.SetCommandTimeout(360);
                db_temp.Database.SetCommandTimeout(360);
                var BusinessUnit_Index = "";
                var Owner_Index = "";
                var Product_Index = "";
                var GoodsReceive_Date = "";
                var GoodsReceive_Date_To = "";
                var Product_Lot = "";
                var Tag_No = "";
                var GoodsReceive_MFG_Date = "";
                var GoodsReceive_MFG_Date_To = "";
                var GoodsReceive_EXP_Date = "";
                var GoodsReceive_EXP_Date_To = "";

                if (data.businessUnitList != null)
                {
                    BusinessUnit_Index = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.Owner_Name))
                {
                    var query_Vendor = db.MS_Vendor.Where(c => c.Vendor_Id.Contains(data.Owner_Name)
                    || c.Vendor_Name.Contains(data.Owner_Name)).FirstOrDefault();
                    Owner_Index = query_Vendor.Vendor_Id;


                    //var queryOwner_Index = db.MS_Owner.Where(C => C.Owner_Name.Contains(data.Owner_Name)).FirstOrDefault();
                    //Owner_Index = queryOwner_Index.Owner_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.Product_Id))
                {
                    var queryProduct_Index = db.MS_Product.Where(C => C.Product_Id.Contains(data.Product_Id)).FirstOrDefault();
                    Product_Index = queryProduct_Index.Product_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.GoodsReceive_Date) && !string.IsNullOrEmpty(data.GoodsReceive_Date_To))
                {
                    GoodsReceive_Date = data.GoodsReceive_Date;
                    GoodsReceive_Date_To = data.GoodsReceive_Date_To;
                }
                if (!string.IsNullOrEmpty(data.Product_Lot))
                {
                    Product_Lot = data.Product_Lot;
                }
                if (!string.IsNullOrEmpty(data.Tag_No))
                {
                    Tag_No = data.Tag_No;
                }
                if (!string.IsNullOrEmpty(data.GoodsReceive_MFG_Date) && !string.IsNullOrEmpty(data.GoodsReceive_MFG_Date_To))
                {
                    GoodsReceive_MFG_Date = data.GoodsReceive_MFG_Date;
                    GoodsReceive_MFG_Date_To = data.GoodsReceive_MFG_Date_To;
                }
                if (!string.IsNullOrEmpty(data.GoodsReceive_EXP_Date) && !string.IsNullOrEmpty(data.GoodsReceive_EXP_Date_To))
                {
                    GoodsReceive_EXP_Date = data.GoodsReceive_EXP_Date;
                    GoodsReceive_EXP_Date_To = data.GoodsReceive_EXP_Date_To;
                }

                var businessUnit_Index = new SqlParameter("@BusinessUnit_Index", BusinessUnit_Index);
                var owner_Index = new SqlParameter("@Vendor_Id", Owner_Index);
                var product_Index = new SqlParameter("@Product_Index", Product_Index);
                var goodsReceive_Date = new SqlParameter("@GoodsReceive_Date", GoodsReceive_Date);
                var goodsReceive_Date_To = new SqlParameter("@GoodsReceive_Date_To", GoodsReceive_Date_To);
                var product_Lot = new SqlParameter("@Product_Lot", Product_Lot);
                var tag_No = new SqlParameter("@Tag_No", Tag_No);
                var goodsReceive_MFG_Date = new SqlParameter("@GoodsReceive_MFG_Date", GoodsReceive_MFG_Date);
                var goodsReceive_MFG_Date_To = new SqlParameter("@GoodsReceive_MFG_Date_To", GoodsReceive_MFG_Date_To);
                var goodsReceive_EXP_Date = new SqlParameter("@GoodsReceive_EXP_Date", GoodsReceive_EXP_Date);
                var goodsReceive_EXP_Date_To = new SqlParameter("@GoodsReceive_EXP_Date_To", GoodsReceive_EXP_Date_To);

                var query = new List<MasterDataDataAccess.Models.sp_rpt_20_Ageging>();

                var ambientRoom_name = "";
                if (data.ambientRoom != "02")
                {
                    olog.logging("ReportStockbyZoneReportAgeging", "Step 1");
                    query = db.sp_rpt_20_Ageging.FromSql("sp_rpt_20_Ageging @BusinessUnit_Index, @Vendor_Id, @Product_Index, @GoodsReceive_Date, @GoodsReceive_Date_To, @Product_Lot, @Tag_No, @GoodsReceive_MFG_Date, @GoodsReceive_MFG_Date_To, @GoodsReceive_EXP_Date, @GoodsReceive_EXP_Date_To",
                        businessUnit_Index, owner_Index, product_Index, goodsReceive_Date, goodsReceive_Date_To, product_Lot, tag_No, goodsReceive_MFG_Date, goodsReceive_MFG_Date_To, goodsReceive_EXP_Date, goodsReceive_EXP_Date_To).ToList();
                    olog.logging("ReportStockbyZoneReportAgeging", "Step 2");
                    ambientRoom_name = "Ambient";
                }
                else
                {
                    olog.logging("ReportStockbyZoneReportAgeging", "Step 1");
                    query = db_temp.sp_rpt_20_Ageging.FromSql("sp_rpt_20_Ageging @BusinessUnit_Index, @Vendor_Id, @Product_Index, @GoodsReceive_Date, @GoodsReceive_Date_To, @Product_Lot, @Tag_No, @GoodsReceive_MFG_Date, @GoodsReceive_MFG_Date_To, @GoodsReceive_EXP_Date, @GoodsReceive_EXP_Date_To",
                        businessUnit_Index, owner_Index, product_Index, goodsReceive_Date, goodsReceive_Date_To, product_Lot, tag_No, goodsReceive_MFG_Date, goodsReceive_MFG_Date_To, goodsReceive_EXP_Date, goodsReceive_EXP_Date_To).ToList();
                    olog.logging("ReportStockbyZoneReportAgeging", "Step 2");
                    ambientRoom_name = "Freeze";
                }
                int num = 0;
                foreach (var item in query)
                {
                    //resultItem.GoodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy") : "";
                    var resultItem = new ReportStockbyZoneReportAgegingViewModel();

                    num = num + 1;
                    resultItem.Row_No = num;
                    resultItem.ambientRoom_name = item.Warehouse_Type;
                    resultItem.BusinessUnit_Name = item.BusinessUnit_Name;
                    resultItem.Tag_No = item.Tag_No;
                    resultItem.Location_Name = item.Location_Name;
                    resultItem.Owner_Id = item.Owner_Id;
                    resultItem.Owner_Name = item.Owner_Name;
                    resultItem.Product_Id = item.Product_Id;
                    resultItem.Product_Name = item.Product_Name;
                    resultItem.ProductConversion_Name = item.ProductConversion_Name;
                    resultItem.EXP_DATE = item.EXP_DATE.ToString();
                    resultItem.WMS_Sloc = item.WMS_Sloc;
                    resultItem.SAP_Sloc = item.SAP_Sloc;
                    resultItem.GoodsReceive_Date = item.GoodsReceive_Date != null ? item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.GoodsReceive_MFG_Date = item.GoodsReceive_MFG_Date != null ? item.GoodsReceive_MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.GoodsReceive_EXP_Date = item.GoodsReceive_EXP_Date != null ? item.GoodsReceive_EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.Product_Lot = item.Product_Lot;
                    result.Add(resultItem);
                }


                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportAgeging");
                //var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSpaceUtilization");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "ReportStockbyZoneReportAgeging" + DateTime.Now.ToString("yyyyMMddHHmmss");

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

        //#region autoSearchOwner
        //public List<ItemListViewModel> autoSearchOwner(ItemListViewModel data)
        //{
        //    try
        //    {

        //        using (var context = new BinbalanceDbContext())
        //        {


        //            var query = context.View_RPT_StockAging.AsQueryable();

        //            if (!string.IsNullOrEmpty(data.key) && data.key != "-")
        //            {
        //                query = query.Where(c => c.Owner_Id.Contains(data.key)
        //                || c.Owner_Name.Contains(data.key));

        //            }

        //            var items = new List<ItemListViewModel>();

        //            var result = query.Select(c => new { c.Owner_Index, c.Owner_Id, c.Owner_Name }).Distinct().Take(10).ToList();

        //            foreach (var item in result)
        //            {
        //                var resultItem = new ItemListViewModel
        //                {
        //                    index = item.Owner_Index,
        //                    id = item.Owner_Id,
        //                    name = item.Owner_Name,

        //                };

        //                items.Add(resultItem);
        //            }

        //            return items;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //#endregion

        public string ExportExcel(ReportStockbyZoneReportAgegingViewModel data, string rootPath = "")
        {
            var db = new MasterDataDbContext();
            var db_temp = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<ReportStockbyZoneReportAgegingViewModel>();
            try
            {
                db.Database.SetCommandTimeout(360);
                db_temp.Database.SetCommandTimeout(360);
                var BusinessUnit_Index = "";
                var Owner_Index = "";
                var Product_Index = "";
                var GoodsReceive_Date = "";
                var GoodsReceive_Date_To = "";
                var Product_Lot = "";
                var Tag_No = "";
                var GoodsReceive_MFG_Date = "";
                var GoodsReceive_MFG_Date_To = "";
                var GoodsReceive_EXP_Date = "";
                var GoodsReceive_EXP_Date_To = "";

                if (data.businessUnitList != null)
                {
                    BusinessUnit_Index = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.Owner_Name))
                {
                    var query_Vendor = db.MS_Vendor.Where(c => c.Vendor_Id.Contains(data.Owner_Name)
                    || c.Vendor_Name.Contains(data.Owner_Name)).FirstOrDefault();
                    Owner_Index = query_Vendor.Vendor_Id;


                    //var queryOwner_Index = db.MS_Owner.Where(C => C.Owner_Name.Contains(data.Owner_Name)).FirstOrDefault();
                    //Owner_Index = queryOwner_Index.Owner_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.Product_Id))
                {
                    var queryProduct_Index = db.MS_Product.Where(C => C.Product_Id.Contains(data.Product_Id)).FirstOrDefault();
                    Product_Index = queryProduct_Index.Product_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.GoodsReceive_Date) && !string.IsNullOrEmpty(data.GoodsReceive_Date_To))
                {
                    GoodsReceive_Date = data.GoodsReceive_Date;
                    GoodsReceive_Date_To = data.GoodsReceive_Date_To;
                }
                if (!string.IsNullOrEmpty(data.Product_Lot))
                {
                    Product_Lot = data.Product_Lot;
                }
                if (!string.IsNullOrEmpty(data.Tag_No))
                {
                    Tag_No = data.Tag_No;
                }
                if (!string.IsNullOrEmpty(data.GoodsReceive_MFG_Date) && !string.IsNullOrEmpty(data.GoodsReceive_MFG_Date_To))
                {
                    GoodsReceive_MFG_Date = data.GoodsReceive_MFG_Date;
                    GoodsReceive_MFG_Date_To = data.GoodsReceive_MFG_Date_To;
                }
                if (!string.IsNullOrEmpty(data.GoodsReceive_EXP_Date) && !string.IsNullOrEmpty(data.GoodsReceive_EXP_Date_To))
                {
                    GoodsReceive_EXP_Date = data.GoodsReceive_EXP_Date;
                    GoodsReceive_EXP_Date_To = data.GoodsReceive_EXP_Date_To;
                }

                var businessUnit_Index = new SqlParameter("@BusinessUnit_Index", BusinessUnit_Index);
                var owner_Index = new SqlParameter("@Vendor_Id", Owner_Index);
                var product_Index = new SqlParameter("@Product_Index", Product_Index);
                var goodsReceive_Date = new SqlParameter("@GoodsReceive_Date", GoodsReceive_Date);
                var goodsReceive_Date_To = new SqlParameter("@GoodsReceive_Date_To", GoodsReceive_Date_To);
                var product_Lot = new SqlParameter("@Product_Lot", Product_Lot);
                var tag_No = new SqlParameter("@Tag_No", Tag_No);
                var goodsReceive_MFG_Date = new SqlParameter("@GoodsReceive_MFG_Date", GoodsReceive_MFG_Date);
                var goodsReceive_MFG_Date_To = new SqlParameter("@GoodsReceive_MFG_Date_To", GoodsReceive_MFG_Date_To);
                var goodsReceive_EXP_Date = new SqlParameter("@GoodsReceive_EXP_Date", GoodsReceive_EXP_Date);
                var goodsReceive_EXP_Date_To = new SqlParameter("@GoodsReceive_EXP_Date_To", GoodsReceive_EXP_Date_To);

                var query = new List<MasterDataDataAccess.Models.sp_rpt_20_Ageging>();

                var ambientRoom_name = "";
                if (data.ambientRoom != "02")
                {
                    olog.logging("ReportStockbyZoneReportAgeging", "Step 1");
                    query = db.sp_rpt_20_Ageging.FromSql("sp_rpt_20_Ageging @BusinessUnit_Index, @Vendor_Id, @Product_Index, @GoodsReceive_Date, @GoodsReceive_Date_To, @Product_Lot, @Tag_No, @GoodsReceive_MFG_Date, @GoodsReceive_MFG_Date_To, @GoodsReceive_EXP_Date, @GoodsReceive_EXP_Date_To",
                        businessUnit_Index, owner_Index, product_Index, goodsReceive_Date, goodsReceive_Date_To, product_Lot, tag_No, goodsReceive_MFG_Date, goodsReceive_MFG_Date_To, goodsReceive_EXP_Date, goodsReceive_EXP_Date_To).ToList();
                    olog.logging("ReportStockbyZoneReportAgeging", "Step 2");
                    ambientRoom_name = "Ambient";
                }
                else
                {
                    olog.logging("ReportStockbyZoneReportAgeging", "Step 1");
                    query = db_temp.sp_rpt_20_Ageging.FromSql("sp_rpt_20_Ageging @BusinessUnit_Index, @Vendor_Id, @Product_Index, @GoodsReceive_Date, @GoodsReceive_Date_To, @Product_Lot, @Tag_No, @GoodsReceive_MFG_Date, @GoodsReceive_MFG_Date_To, @GoodsReceive_EXP_Date, @GoodsReceive_EXP_Date_To",
                        businessUnit_Index, owner_Index, product_Index, goodsReceive_Date, goodsReceive_Date_To, product_Lot, tag_No, goodsReceive_MFG_Date, goodsReceive_MFG_Date_To, goodsReceive_EXP_Date, goodsReceive_EXP_Date_To).ToList();
                    olog.logging("ReportStockbyZoneReportAgeging", "Step 2");
                    ambientRoom_name = "Freeze";
                }
                int num = 0;
                foreach (var item in query)
                {
                    //resultItem.GoodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy") : "";
                    var resultItem = new ReportStockbyZoneReportAgegingViewModel();

                    num = num + 1;
                    resultItem.Row_No = num;
                    resultItem.ambientRoom_name = item.Warehouse_Type;
                    resultItem.BusinessUnit_Name = item.BusinessUnit_Name;
                    resultItem.Tag_No = item.Tag_No;
                    resultItem.Location_Name = item.Location_Name;
                    resultItem.Owner_Id = item.Owner_Id;
                    resultItem.Owner_Name = item.Owner_Name;
                    resultItem.Product_Id = item.Product_Id;
                    resultItem.Product_Name = item.Product_Name;
                    resultItem.ProductConversion_Name = item.ProductConversion_Name;
                    resultItem.EXP_DATE = item.EXP_DATE.ToString();
                    resultItem.WMS_Sloc = item.WMS_Sloc;
                    resultItem.SAP_Sloc = item.SAP_Sloc;
                    resultItem.GoodsReceive_Date = item.GoodsReceive_Date != null ? item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.GoodsReceive_MFG_Date = item.GoodsReceive_MFG_Date != null ? item.GoodsReceive_MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.GoodsReceive_EXP_Date = item.GoodsReceive_EXP_Date != null ? item.GoodsReceive_EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.Product_Lot = item.Product_Lot;
                    result.Add(resultItem);
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportAgeging");

                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "ReportStockbyZoneReportAgeging";

                Utils objReport = new Utils();
                var renderedBytes = report.Execute(RenderType.Excel);
                fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);
                var saveLocation = objReport.PhysicalPath(fileName + ".xls", rootPath);
                return saveLocation;
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

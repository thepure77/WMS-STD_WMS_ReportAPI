using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using PlanGRBusiness.Libs;
using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportBusiness.CheckReplenishCFRByMax
{
    public class CheckReplenishCFRByMaxService
    {
        #region printReportLaborPerformance
        public dynamic printReportPan(CheckReplenishCFRByMaxViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<CheckReplenishCFRByMaxViewModel>();

            try
            {

                var product_Id = "";
                //var product_Name = "";

                //var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();

                var statusModels = new List<int?>();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                //if (!string.IsNullOrEmpty(data.product_Name))
                //{
                //    product_Name = data.product_Name;
                //}

                //@PRO_ID 
                //@PRO_NAME

                var pro_Id = new SqlParameter("@PRO_ID", product_Id);
                //var pro_Name = new SqlParameter("@PRO_NAME", product_Name);
                var resultquery = new List<MasterDataDataAccess.Models.sp_CheckReplenishCFRByMax>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_CheckReplenishCFRByMax.FromSql("sp_CheckReplenishCFRByMax @PRO_ID ", pro_Id).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_CheckReplenishCFRByMax.FromSql("sp_CheckReplenishCFRByMax @PRO_ID ", pro_Id).ToList();
                }

                //var resultquery = Master_DBContext.sp_CheckReplenishCFRByMax.FromSql("sp_CheckReplenishCFRByMax @PRO_ID ", pro_Id).ToList();
                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new CheckReplenishCFRByMaxViewModel();
                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.report_date = startDate;
                    resultItem.report_date_to = endDate;
                    //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    result.Add(resultItem);
                }
                else
                {
                    int num = 0;
                    foreach (var item in resultquery)
                    {
                        var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new CheckReplenishCFRByMaxViewModel();
                        resultItem.rowNo = num + 1; ;
                        resultItem.product_Id = item.SKU;
                        resultItem.product_Name = item.MatDescription;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.productConversion_Ratio = item.ProductConversion_Ratio;
                        resultItem.maxQty = item.MaxQty;
                        resultItem.pp_SuQty_1 = item.PP_SuQty;
                        resultItem.bb_SuQty_1 = item.BB_SuQty;
                        resultItem.replenQty = item.ReplenQty;
                        resultItem.pp_QtyBal_2 = item.PP_QtyBal;
                        resultItem.bb_QtyBal_2 = item.BB_QtyBal;
                        if (data.ambientRoom != "02")
                        {
                            resultItem.ambientRoom = "Ambient";
                        }
                        else
                        {
                            resultItem.ambientRoom = "Freeze";
                        }
                        result.Add(resultItem);
                        num++;
                    }
                }





                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("CheckReplenishCFRByMax");
                //var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSpaceUtilization");
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
                //olog.logging("ReportKPI", ex.Message);
                throw ex;
            }
        }
        #endregion
        public string ExportExcel(CheckReplenishCFRByMaxViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<CheckReplenishCFRByMaxViewModel>();

            try
            {

                var product_Id = "";
                //var product_Name = "";

                //var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();

                var statusModels = new List<int?>();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                //if (!string.IsNullOrEmpty(data.product_Name))
                //{
                //    product_Name = data.product_Name;
                //}

                //@PRO_ID 
                //@PRO_NAME

                var pro_Id = new SqlParameter("@PRO_ID", product_Id);
                //var pro_Name = new SqlParameter("@PRO_NAME", product_Name);
                var resultquery = new List<MasterDataDataAccess.Models.sp_CheckReplenishCFRByMax>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_CheckReplenishCFRByMax.FromSql("sp_CheckReplenishCFRByMax @PRO_ID ", pro_Id).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_CheckReplenishCFRByMax.FromSql("sp_CheckReplenishCFRByMax @PRO_ID ", pro_Id).ToList();
                }

                //var resultquery = Master_DBContext.sp_CheckReplenishCFRByMax.FromSql("sp_CheckReplenishCFRByMax @PRO_ID ", pro_Id).ToList();
                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new CheckReplenishCFRByMaxViewModel();
                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.report_date = startDate;
                    resultItem.report_date_to = endDate;
                    //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    result.Add(resultItem);
                }
                else
                {
                    int num = 0;
                    foreach (var item in resultquery)
                    {
                        var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new CheckReplenishCFRByMaxViewModel();
                        resultItem.rowNo = num + 1; ;
                        resultItem.product_Id = item.SKU;
                        resultItem.product_Name = item.MatDescription;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.productConversion_Ratio = item.ProductConversion_Ratio;
                        resultItem.maxQty = item.MaxQty;
                        resultItem.pp_SuQty_1 = item.PP_SuQty;
                        resultItem.bb_SuQty_1 = item.BB_SuQty;
                        resultItem.replenQty = item.ReplenQty;
                        resultItem.pp_QtyBal_2 = item.PP_QtyBal;
                        resultItem.bb_QtyBal_2 = item.BB_QtyBal;
                        if (data.ambientRoom != "02")
                        {
                            resultItem.ambientRoom = "Ambient";
                        }
                        else
                        {
                            resultItem.ambientRoom = "Freeze";
                        }
                        result.Add(resultItem);
                        num++;
                    }
                }




                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("CheckReplenishCFRByMax");

                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "tmpReport";

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
    }
}

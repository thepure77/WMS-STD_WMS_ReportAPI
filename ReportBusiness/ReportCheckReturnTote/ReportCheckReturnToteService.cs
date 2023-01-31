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

namespace ReportBusiness.ReportCheckReturnTote
{
    public class ReportCheckReturnToteService
    {
        #region printReportReplenishVCByMax
        public dynamic printCheckReturnTote(ReportCheckReturnToteViewModel data, string rootPath = "")
        {

            //var BB_DBContext = new BinbalanceDbContext();

            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportCheckReturnToteViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var truckLoad_No = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                //var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();
                var statusModels = new List<int?>();
                if (!string.IsNullOrEmpty(data.truckLoad_No))
                {
                    truckLoad_No = data.truckLoad_No;
                }
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
     
                }
                
                var tl = new SqlParameter("@TL", truckLoad_No);
                var tl_date = new SqlParameter("@TL_DATE", dateStart);
                var tl_date_to = new SqlParameter("@TL_DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_ReportCheckReturnTote>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_ReportCheckReturnTote.FromSql("sp_ReportCheckReturnTote @TL,@TL_DATE,@TL_DATE_TO", tl, tl_date, tl_date_to).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_ReportCheckReturnTote.FromSql("sp_ReportCheckReturnTote @TL,@TL_DATE,@TL_DATE_TO", tl, tl_date, tl_date_to).ToList();
                }

                //var resultquery = Master_DBContext.sp_ReportCheckReturnTote.FromSql("sp_ReportCheckReturnTote @TL,@TL_DATE,@TL_DATE_TO", tl, tl_date, tl_date_to).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportCheckReturnToteViewModel();
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

                        var resultItem = new ReportCheckReturnToteViewModel();
                        resultItem.rowNum = num + 1;
                        resultItem.truckLoad_No = item.TruckLoad_No;
                        resultItem.truck_Load_Return_Date = item.TruckLoad_Return_Date;
                        resultItem.return_Tote_MAX_XL = item.Return_Tote_MAX_XL; 
                        resultItem.return_Tote_MAX_M = item.Return_Tote_MAX_M;
                        resultItem.doc_Return_Max = item.DocReturn_Max;
                        resultItem.return_Tote_Qty_XL = item.Return_Tote_Qty_XL;
                        resultItem.return_Tote_Qty_M = item.Return_Tote_Qty_M; 
                        resultItem.return_Tote_Qty_DMG_XL = item.Return_Tote_Qty_DMG_XL;
                        resultItem.return_Tote_Qty_DMG_M = item.Return_Tote_Qty_DMG_M;
                        resultItem.return_Doc = item.Return_Doc;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportCheckReturnTote");
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

        public string ExportExcel(ReportCheckReturnToteViewModel data, string rootPath = "")
        {
            var DBContext = new PlanGRDbContext();
            var GR_DBContext = new GRDbContext();
            var M_DBContext = new MasterDataDbContext();
            var GI_DBContext = new PlanGIDbContext();
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportCheckReturnToteViewModel>();
            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var truckLoad_No = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                //var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();
                var statusModels = new List<int?>();
                if (!string.IsNullOrEmpty(data.truckLoad_No))
                {
                    truckLoad_No = data.truckLoad_No;
                }
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;

                }

                var tl = new SqlParameter("@TL", truckLoad_No);
                var tl_date = new SqlParameter("@TL_DATE", dateStart);
                var tl_date_to = new SqlParameter("@TL_DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_ReportCheckReturnTote>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_ReportCheckReturnTote.FromSql("sp_ReportCheckReturnTote @TL,@TL_DATE,@TL_DATE_TO", tl, tl_date, tl_date_to).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_ReportCheckReturnTote.FromSql("sp_ReportCheckReturnTote @TL,@TL_DATE,@TL_DATE_TO", tl, tl_date, tl_date_to).ToList();
                }

                //var resultquery = Master_DBContext.sp_ReportCheckReturnTote.FromSql("sp_ReportCheckReturnTote @TL,@TL_DATE,@TL_DATE_TO", tl, tl_date, tl_date_to).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportCheckReturnToteViewModel();
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

                        var resultItem = new ReportCheckReturnToteViewModel();
                        resultItem.rowNum = num + 1;
                        resultItem.truckLoad_No = item.TruckLoad_No;
                        resultItem.truck_Load_Return_Date = item.TruckLoad_Return_Date;
                        resultItem.return_Tote_MAX_XL = item.Return_Tote_MAX_XL;
                        resultItem.return_Tote_MAX_M = item.Return_Tote_MAX_M;
                        resultItem.doc_Return_Max = item.DocReturn_Max;
                        resultItem.return_Tote_Qty_XL = item.Return_Tote_Qty_XL;
                        resultItem.return_Tote_Qty_M = item.Return_Tote_Qty_M;
                        resultItem.return_Tote_Qty_DMG_XL = item.Return_Tote_Qty_DMG_XL;
                        resultItem.return_Tote_Qty_DMG_M = item.Return_Tote_Qty_DMG_M;
                        resultItem.return_Doc = item.Return_Doc;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportCheckReturnTote");

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

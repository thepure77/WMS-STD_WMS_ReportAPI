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

namespace ReportBusiness.ReportCheckZonePutaway
{
    public class ReportCheckZonePutawayService
    {
        #region printReport
        public dynamic printReportCheckZonePutaway(ReportCheckZonePutawayViewModel data, string rootPath = "")
        {

            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var GI_DBContext = new PlanGIDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportCheckZonePutawayViewModel>();

            try
            {
                var zoneputaway_Id = "";
                var zoneputaway_Name = "";
                var statusModels = new List<int?>();
                if (!string.IsNullOrEmpty(data.zoneputaway_Id))
                {
                    zoneputaway_Id = data.zoneputaway_Id;
                }
                if (!string.IsNullOrEmpty(data.zoneputaway_Name))
                {
                    zoneputaway_Name = data.zoneputaway_Name;
                }

                var zone_id = new SqlParameter("@ZP_ID", zoneputaway_Id);
                var zone_name = new SqlParameter("@ZP_NAME", zoneputaway_Name);
                var resultquery = new List<MasterDataDataAccess.Models.sp_ReportCheckZonePutaway>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_ReportCheckZonePutaway.FromSql("sp_ReportCheckZonePutaway @ZP_ID,@ZP_NAME", zone_id, zone_name).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_ReportCheckZonePutaway.FromSql("sp_ReportCheckZonePutaway @ZP_ID,@ZP_NAME", zone_id, zone_name).ToList();
                }

                //var resultquery = Master_DBContext.sp_ReportCheckZonePutaway.FromSql("sp_ReportCheckZonePutaway @ZP_ID,@ZP_NAME", zone_id, zone_name).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportCheckZonePutawayViewModel();
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

                        var resultItem = new ReportCheckZonePutawayViewModel();
                        resultItem.rowNum = num + 1; ;
                        resultItem.zoneputaway_Id = item.Zoneputaway_Id;
                        resultItem.zoneputaway_Name = item.Zoneputaway_Name;
                        resultItem.countProduct = item.CountProduct;
                        resultItem.countLocation_Name = item.CountLocation_Name;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportCheckZonePutaway");
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

        public string ExportExcel(ReportCheckZonePutawayViewModel data, string rootPath = "")
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
            var result = new List<ReportCheckZonePutawayViewModel>();
            try
            {
                var zoneputaway_Id = "";
                var zoneputaway_Name = "";
                var statusModels = new List<int?>();
                if (!string.IsNullOrEmpty(data.zoneputaway_Id))
                {
                    zoneputaway_Id = data.zoneputaway_Id;
                }
                if (!string.IsNullOrEmpty(data.zoneputaway_Name))
                {
                    zoneputaway_Name = data.zoneputaway_Name;
                }

                var zone_id = new SqlParameter("@ZP_ID", zoneputaway_Id);
                var zone_name = new SqlParameter("@ZP_NAME", zoneputaway_Name);
                var resultquery = new List<MasterDataDataAccess.Models.sp_ReportCheckZonePutaway>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_ReportCheckZonePutaway.FromSql("sp_ReportCheckZonePutaway @ZP_ID,@ZP_NAME", zone_id, zone_name).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_ReportCheckZonePutaway.FromSql("sp_ReportCheckZonePutaway @ZP_ID,@ZP_NAME", zone_id, zone_name).ToList();
                }

                //var resultquery = Master_DBContext.sp_ReportCheckZonePutaway.FromSql("sp_ReportCheckZonePutaway @ZP_ID,@ZP_NAME", zone_id, zone_name).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportCheckZonePutawayViewModel();
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

                        var resultItem = new ReportCheckZonePutawayViewModel();
                        resultItem.rowNum = num + 1; ;
                        resultItem.zoneputaway_Id = item.Zoneputaway_Id;
                        resultItem.zoneputaway_Name = item.Zoneputaway_Name;
                        resultItem.countProduct = item.CountProduct;
                        resultItem.countLocation_Name = item.CountLocation_Name;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportCheckZonePutaway");

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

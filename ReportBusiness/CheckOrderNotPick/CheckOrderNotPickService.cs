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

namespace ReportBusiness.CheckOrderNotPick
{
    public class CheckOrderNotPickService
    {
        #region printReportLaborPerformance
        public dynamic printReportPan(CheckOrderNotPickViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<CheckOrderNotPickViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var appointment_Id = "";
                var truckLoad_No = "";
                var planGoodsIssue_No = "";

                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                //var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();

                var statusModels = new List<int?>();

                if (!string.IsNullOrEmpty(data.appointment_Id))
                {
                    appointment_Id = data.appointment_Id;
                }
                if (!string.IsNullOrEmpty(data.truckLoad_No))
                {
                    truckLoad_No = data.truckLoad_No;
                }
                if (!string.IsNullOrEmpty(data.planGoodsIssue_No))
                {
                    planGoodsIssue_No = data.planGoodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }
                //@APP_ID
                //@TL_NO
                //@PLAN
                //@APP_DATE
                //@APP_DATE

                var app_Id = new SqlParameter("@APP_ID", appointment_Id);
                var tl_No = new SqlParameter("@TL_NO", truckLoad_No);
                var plan = new SqlParameter("@PLAN", planGoodsIssue_No);
                var app_date = new SqlParameter("@APP_DATE", dateStart);
                var app_date_to = new SqlParameter("@APP_DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_CheckOrderNotPick>();

                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_CheckOrderNotPick.FromSql("sp_CheckOrderNotPick @APP_ID , @TL_NO , @PLAN , @APP_DATE , @APP_DATE_TO", app_Id, tl_No, plan, app_date, app_date_to).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_CheckOrderNotPick.FromSql("sp_CheckOrderNotPick @APP_ID , @TL_NO , @PLAN , @APP_DATE , @APP_DATE_TO", app_Id, tl_No, plan, app_date, app_date_to).ToList();
                }


                //var resultquery = Master_DBContext.sp_CheckOrderNotPick.FromSql("sp_CheckOrderNotPick @APP_ID , @TL_NO , @PLAN , @APP_DATE , @APP_DATE_TO", app_Id, tl_No, plan, app_date, app_date_to).ToList();                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new CheckOrderNotPickViewModel();
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

                        var resultItem = new CheckOrderNotPickViewModel();
                        resultItem.rowNo = num + 1; ;
                        resultItem.truckLoad_No = item.TruckLoad_No;
                        resultItem.appointment_Id = item.Appointment_Id;
                        resultItem.dock_Name = item.Dock_Name;
                        resultItem.appointment_Date = item.Appointment_Date != null ? item.Appointment_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.appointment_Time = item.Appointment_Time;
                        resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No;
                        resultItem.shipTo_Id = item.ShipTo_Id;
                        resultItem.shipTo_Name = item.ShipTo_Name;
                        resultItem.branchCode = item.BranchCode;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.order_Qty = item.Order_Qty;
                        resultItem.order_Unit = item.Order_Unit;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("CheckOrderNotPick");
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
        public string ExportExcel(CheckOrderNotPickViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<CheckOrderNotPickViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var appointment_Id = "";
                var truckLoad_No = "";
                var planGoodsIssue_No = "";

                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                //var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();

                var statusModels = new List<int?>();

                if (!string.IsNullOrEmpty(data.appointment_Id))
                {
                    appointment_Id = data.appointment_Id;
                }
                if (!string.IsNullOrEmpty(data.truckLoad_No))
                {
                    truckLoad_No = data.truckLoad_No;
                }
                if (!string.IsNullOrEmpty(data.planGoodsIssue_No))
                {
                    planGoodsIssue_No = data.planGoodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }
                //@APP_ID
                //@TL_NO
                //@PLAN
                //@APP_DATE
                //@APP_DATE

                var app_Id = new SqlParameter("@APP_ID", appointment_Id);
                var tl_No = new SqlParameter("@TL_NO", truckLoad_No);
                var plan = new SqlParameter("@PLAN", planGoodsIssue_No);
                var app_date = new SqlParameter("@APP_DATE", dateStart);
                var app_date_to = new SqlParameter("@APP_DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_CheckOrderNotPick>();

                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_CheckOrderNotPick.FromSql("sp_CheckOrderNotPick @APP_ID , @TL_NO , @PLAN , @APP_DATE , @APP_DATE_TO", app_Id, tl_No, plan, app_date, app_date_to).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_CheckOrderNotPick.FromSql("sp_CheckOrderNotPick @APP_ID , @TL_NO , @PLAN , @APP_DATE , @APP_DATE_TO", app_Id, tl_No, plan, app_date, app_date_to).ToList();
                }


                //var resultquery = Master_DBContext.sp_CheckOrderNotPick.FromSql("sp_CheckOrderNotPick @APP_ID , @TL_NO , @PLAN , @APP_DATE , @APP_DATE_TO", app_Id, tl_No, plan, app_date, app_date_to).ToList();
                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new CheckOrderNotPickViewModel();
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

                        var resultItem = new CheckOrderNotPickViewModel();
                        resultItem.rowNo = num + 1; ;
                        resultItem.truckLoad_No = item.TruckLoad_No;
                        resultItem.appointment_Id = item.Appointment_Id;
                        resultItem.dock_Name = item.Dock_Name;
                        resultItem.appointment_Date = item.Appointment_Date != null ? item.Appointment_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.appointment_Time = item.Appointment_Time;
                        resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No;
                        resultItem.shipTo_Id = item.ShipTo_Id;
                        resultItem.shipTo_Name = item.ShipTo_Name;
                        resultItem.branchCode = item.BranchCode;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.order_Qty = item.Order_Qty;
                        resultItem.order_Unit = item.Order_Unit;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("CheckOrderNotPick");

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

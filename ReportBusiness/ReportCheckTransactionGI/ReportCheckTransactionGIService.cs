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

namespace ReportBusiness.ReportCheckTransactionGI
{
    public class ReportCheckTransactionGIService
    {
        #region printReport
        public dynamic printReportCheckTransactionGI(ReportCheckTransactionGIViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            //var BB_DBContext = new BinbalanceDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var GI_DBContext = new PlanGIDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportCheckTransactionGIViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);

                var product_Id = "";
                var planGoodsIssue_No = "";
                var truckLoad_No = "";
                var appointment_Id = "";
                var bill_No = "";
                var goodsIssue_No = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();
                var statusModels = new List<int?>();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                if (!string.IsNullOrEmpty(data.planGoodsIssue_No))
                {
                    planGoodsIssue_No = data.planGoodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.truckLoad_No))
                {
                    truckLoad_No = data.truckLoad_No;
                }
                if (!string.IsNullOrEmpty(data.appointment_Id))
                {
                    appointment_Id = data.appointment_Id;
                }
                if (!string.IsNullOrEmpty(data.bill_No))
                {
                    bill_No = data.bill_No;
                }
                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    goodsIssue_No = data.goodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;

                }


                var pro_id = new SqlParameter("@PRO_ID", product_Id);
                var gi = new SqlParameter("@GI", goodsIssue_No);
                var plan_gi = new SqlParameter("@PLAN_GI", planGoodsIssue_No);
                var tl = new SqlParameter("@TL", truckLoad_No);
                var bill = new SqlParameter("@BIL", bill_No);
                var app = new SqlParameter("@APP_ID", appointment_Id);
                var app_date = new SqlParameter("@APP_DATE", dateStart);
                var app_date_to = new SqlParameter("@APP_DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_ReportCheckTransactionGI>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_ReportCheckTransactionGI.FromSql("sp_ReportCheckTransactionGI @PRO_ID , @GI , @PLAN_GI, @TL, @BIL,@APP_ID, @APP_DATE, @APP_DATE_TO", pro_id, gi, plan_gi, tl, bill, app, app_date, app_date_to).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_ReportCheckTransactionGI.FromSql("sp_ReportCheckTransactionGI @PRO_ID , @GI , @PLAN_GI, @TL, @BIL,@APP_ID, @APP_DATE, @APP_DATE_TO", pro_id, gi, plan_gi, tl, bill, app, app_date, app_date_to).ToList();
                }

                //var resultquery = Master_DBContext.sp_ReportCheckTransactionGI.FromSql("sp_ReportCheckTransactionGI @GI, @PLAN_GI, @TL, @BIL,@APP_ID, @APP_DATE, @APP_DATE_TO", gi, plan_gi, tl, bill, app, app_date, app_date_to).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportCheckTransactionGIViewModel();
                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.report_date = startDate;
                    resultItem.report_date_to = endDate;
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

                        var resultItem = new ReportCheckTransactionGIViewModel();
                        resultItem.rowNum = num + 1;
                        resultItem.truckLoad_No = item.TruckLoad_No;
                        resultItem.appointment_Id = item.Appointment_Id;
                        resultItem.dock_Name = item.Dock_Name;
                        resultItem.appointment_Date = item.Appointment_Date != null ? item.Appointment_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.appointment_Time = item.Appointment_Time;
                        resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No;
                        resultItem.shipTo_Id = item.ShipTo_Id;
                        resultItem.shipTo_Name = item.ShipTo_Name;
                        resultItem.province = item.Province;
                        resultItem.branchCode = item.BranchCode;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.order_Qty = item.Order_Qty;
                        resultItem.wHGI_QTY = item.WHGI_QTY;
                        resultItem.tRGI_QTY = item.TRGI_QTY;
                        resultItem.order_UNIT = item.Order_UNIT;
                        resultItem.goodsIssue_No = item.GoodsIssue_No;
                        resultItem.goodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.bill_No = item.Bill_No;
                        resultItem.matdoc = item.Matdoc;
                        resultItem.bu_Order_Qty = item.BU_Order_Qty;
                        resultItem.bu_WHGIQty = item.BU_WHGIQty;
                        resultItem.bu_TRGI_QTY = item.BU_TRGI_QTY;
                        resultItem.bu_Unit = item.BU_Unit;
                        resultItem.su_Order_QTY = item.SU_Order_QTY;
                        resultItem.su_WHGIQty = item.SU_WHGIQty;
                        resultItem.su_TRGI_QTY = item.SU_TRGI_QTY;
                        resultItem.su_Unit = item.SU_Unit;
                        resultItem.document_Remark = item.Document_Remark;
                        resultItem.documentRef_No3 = item.DocumentRef_No3;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportCheckTransactionGI");
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

        public string ExportExcel(ReportCheckTransactionGIViewModel data, string rootPath = "")
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
            var result = new List<ReportCheckTransactionGIViewModel>();
            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);

                var product_Id = "";
                var planGoodsIssue_No = "";
                var truckLoad_No = "";
                var appointment_Id = "";
                var bill_No = "";
                var goodsIssue_No = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();
                var statusModels = new List<int?>();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                if (!string.IsNullOrEmpty(data.planGoodsIssue_No))
                {
                    planGoodsIssue_No = data.planGoodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.truckLoad_No))
                {
                    truckLoad_No = data.truckLoad_No;
                }
                if (!string.IsNullOrEmpty(data.appointment_Id))
                {
                    appointment_Id = data.appointment_Id;
                }
                if (!string.IsNullOrEmpty(data.bill_No))
                {
                    bill_No = data.bill_No;
                }
                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    goodsIssue_No = data.goodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;

                }


                var pro_id = new SqlParameter("@PRO_ID", product_Id);
                var gi = new SqlParameter("@GI", goodsIssue_No);
                var plan_gi = new SqlParameter("@PLAN_GI", planGoodsIssue_No);
                var tl = new SqlParameter("@TL", truckLoad_No);
                var bill = new SqlParameter("@BIL", bill_No);
                var app = new SqlParameter("@APP_ID", appointment_Id);
                var app_date = new SqlParameter("@APP_DATE", dateStart);
                var app_date_to = new SqlParameter("@APP_DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_ReportCheckTransactionGI>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_ReportCheckTransactionGI.FromSql("sp_ReportCheckTransactionGI @PRO_ID , @GI , @PLAN_GI, @TL, @BIL,@APP_ID, @APP_DATE, @APP_DATE_TO", pro_id, gi, plan_gi, tl, bill, app, app_date, app_date_to).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_ReportCheckTransactionGI.FromSql("sp_ReportCheckTransactionGI @PRO_ID , @GI , @PLAN_GI, @TL, @BIL,@APP_ID, @APP_DATE, @APP_DATE_TO", pro_id, gi, plan_gi, tl, bill, app, app_date, app_date_to).ToList();
                }

                //var resultquery = Master_DBContext.sp_ReportCheckTransactionGI.FromSql("sp_ReportCheckTransactionGI @GI, @PLAN_GI, @TL, @BIL,@APP_ID, @APP_DATE, @APP_DATE_TO", gi, plan_gi, tl, bill, app, app_date, app_date_to).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportCheckTransactionGIViewModel();
                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.report_date = startDate;
                    resultItem.report_date_to = endDate;
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

                        var resultItem = new ReportCheckTransactionGIViewModel();
                        resultItem.rowNum = num + 1;
                        resultItem.truckLoad_No = item.TruckLoad_No;
                        resultItem.appointment_Id = item.Appointment_Id;
                        resultItem.dock_Name = item.Dock_Name;
                        resultItem.appointment_Date = item.Appointment_Date != null ? item.Appointment_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.appointment_Time = item.Appointment_Time;
                        resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No;
                        resultItem.shipTo_Id = item.ShipTo_Id;
                        resultItem.shipTo_Name = item.ShipTo_Name;
                        resultItem.province = item.Province;
                        resultItem.branchCode = item.BranchCode;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.order_Qty = item.Order_Qty;
                        resultItem.wHGI_QTY = item.WHGI_QTY;
                        resultItem.tRGI_QTY = item.TRGI_QTY;
                        resultItem.order_UNIT = item.Order_UNIT;
                        resultItem.goodsIssue_No = item.GoodsIssue_No;
                        resultItem.goodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.bill_No = item.Bill_No;
                        resultItem.matdoc = item.Matdoc;
                        resultItem.bu_Order_Qty = item.BU_Order_Qty;
                        resultItem.bu_WHGIQty = item.BU_WHGIQty;
                        resultItem.bu_TRGI_QTY = item.BU_TRGI_QTY;
                        resultItem.bu_Unit = item.BU_Unit;
                        resultItem.su_Order_QTY = item.SU_Order_QTY;
                        resultItem.su_WHGIQty = item.SU_WHGIQty;
                        resultItem.su_TRGI_QTY = item.SU_TRGI_QTY;
                        resultItem.su_Unit = item.SU_Unit;
                        resultItem.document_Remark = item.Document_Remark;
                        resultItem.documentRef_No3 = item.DocumentRef_No3;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportCheckTransactionGI");

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

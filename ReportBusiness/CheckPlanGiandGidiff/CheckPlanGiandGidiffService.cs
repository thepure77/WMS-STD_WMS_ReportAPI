﻿using AspNetCore.Reporting;
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

namespace ReportBusiness.CheckPlanGiandGidiff
{
    public class CheckPlanGiandGidiffService
    {
        #region printReportLaborPerformance
        public dynamic printReportPan(CheckPlanGiandGidiffViewModel data, string rootPath = "")
        {

            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<CheckPlanGiandGidiffViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var goodsIssue_No = "";
                var order_Seq = "";
                var planGoodsIssue_No = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                //var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();

                var statusModels = new List<int?>();

                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    goodsIssue_No = data.goodsIssue_No;
                }

                if (!string.IsNullOrEmpty(data.order_Seq))
                {
                    order_Seq = data.order_Seq;
                }

                if (!string.IsNullOrEmpty(data.planGoodsIssue_No))
                {
                    planGoodsIssue_No = data.planGoodsIssue_No;
                }
                //if (!string.IsNullOrEmpty(data.product_Name))
                //{
                //    product_Name = data.product_Name;
                //}
                //if (!string.IsNullOrEmpty(data.sale_UNIT))
                //{
                //    sale_Unit = data.sale_UNIT;
                //}
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }

                //@GI_NO
                //@PLAN 
                //@SEQ
                //@APP_DATE
                //@APP_DATE_TO

                var gi_Id = new SqlParameter("@GI_NO", goodsIssue_No);
                var seq = new SqlParameter("@SEQ", order_Seq);
                var plan = new SqlParameter("@PLAN", planGoodsIssue_No);
                var app_date = new SqlParameter("@APP_DATE", dateStart);
                var app_date_to = new SqlParameter("@APP_DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_CheckPlanGiandGidiff>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_CheckPlanGiandGidiff.FromSql("sp_CheckPlanGiandGidiff @GI_NO , @PLAN , @SEQ , @APP_DATE ,@APP_DATE_TO", gi_Id, plan, seq, app_date, app_date_to).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_CheckPlanGiandGidiff.FromSql("sp_CheckPlanGiandGidiff @GI_NO , @PLAN , @SEQ , @APP_DATE ,@APP_DATE_TO", gi_Id, plan, seq, app_date, app_date_to).ToList();
                }


                //var resultquery = Master_DBContext.sp_CheckPlanGiandGidiff.FromSql("sp_CheckPlanGiandGidiff @GI_NO , @PLAN , @SEQ , @APP_DATE ,@APP_DATE_TO", gi_Id, plan, seq, app_date, app_date_to).ToList();
                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new CheckPlanGiandGidiffViewModel();
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

                        var resultItem = new CheckPlanGiandGidiffViewModel();
                        resultItem.rowNo = num + 1; ;
                        resultItem.appointment_Id = item.Appointment_Id;
                        resultItem.appointment_Date = item.Appointment_Date != null ? item.Appointment_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.appointment_Time = item.Appointment_Time;
                        resultItem.truckLoad_No = item.TruckLoad_No;
                        resultItem.order_Seq = item.Order_Seq;
                        resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No;
                        resultItem.lineNum = item.LineNum;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.bu_Order_TotalQty = item.BU_Order_TotalQty;
                        resultItem.bu_GI_TotalQty = item.BU_GI_TotalQty;
                        resultItem.su_Order_TotalQty = item.SU_Order_TotalQty;
                        resultItem.su_GI_TotalQty = item.SU_GI_TotalQty;
                        resultItem.su_Unit = item.SU_Unit;
                        resultItem.erp_Location = item.ERP_Location;
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.diff = item.diff;
                        resultItem.goodsIssue_No = item.GoodsIssue_No;
                        resultItem.documentRef_No3 = item.DocumentRef_No3;
                        resultItem.document_Remark = item.Document_Remark;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("CheckPlanGiandGidiff");
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
        public string ExportExcel(CheckPlanGiandGidiffViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<CheckPlanGiandGidiffViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var goodsIssue_No = "";
                var order_Seq = "";
                var planGoodsIssue_No = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                //var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();

                var statusModels = new List<int?>();

                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    goodsIssue_No = data.goodsIssue_No;
                }

                if (!string.IsNullOrEmpty(data.order_Seq))
                {
                    order_Seq = data.order_Seq;
                }

                if (!string.IsNullOrEmpty(data.planGoodsIssue_No))
                {
                    planGoodsIssue_No = data.planGoodsIssue_No;
                }
                //if (!string.IsNullOrEmpty(data.product_Name))
                //{
                //    product_Name = data.product_Name;
                //}
                //if (!string.IsNullOrEmpty(data.sale_UNIT))
                //{
                //    sale_Unit = data.sale_UNIT;
                //}
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }

                //@GI_NO
                //@PLAN 
                //@SEQ
                //@APP_DATE
                //@APP_DATE_TO

                var gi_Id = new SqlParameter("@GI_NO", goodsIssue_No);
                var seq = new SqlParameter("@SEQ", order_Seq);
                var plan = new SqlParameter("@PLAN", planGoodsIssue_No);
                var app_date = new SqlParameter("@APP_DATE", dateStart);
                var app_date_to = new SqlParameter("@APP_DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_CheckPlanGiandGidiff>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_CheckPlanGiandGidiff.FromSql("sp_CheckPlanGiandGidiff @GI_NO , @PLAN , @SEQ , @APP_DATE ,@APP_DATE_TO", gi_Id, plan, seq, app_date, app_date_to).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_CheckPlanGiandGidiff.FromSql("sp_CheckPlanGiandGidiff @GI_NO , @PLAN , @SEQ , @APP_DATE ,@APP_DATE_TO", gi_Id, plan, seq, app_date, app_date_to).ToList();
                }


                //var resultquery = Master_DBContext.sp_CheckPlanGiandGidiff.FromSql("sp_CheckPlanGiandGidiff @GI_NO , @PLAN , @SEQ , @APP_DATE ,@APP_DATE_TO", gi_Id, plan, seq, app_date, app_date_to).ToList();
                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new CheckPlanGiandGidiffViewModel();
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

                        var resultItem = new CheckPlanGiandGidiffViewModel();
                        resultItem.rowNo = num + 1; ;
                        resultItem.appointment_Id = item.Appointment_Id;
                        resultItem.appointment_Date = item.Appointment_Date != null ? item.Appointment_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.appointment_Time = item.Appointment_Time;
                        resultItem.truckLoad_No = item.TruckLoad_No;
                        resultItem.order_Seq = item.Order_Seq;
                        resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No;
                        resultItem.lineNum = item.LineNum;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.bu_Order_TotalQty = item.BU_Order_TotalQty;
                        resultItem.bu_GI_TotalQty = item.BU_GI_TotalQty;
                        resultItem.su_Order_TotalQty = item.SU_Order_TotalQty;
                        resultItem.su_GI_TotalQty = item.SU_GI_TotalQty;
                        resultItem.su_Unit = item.SU_Unit;
                        resultItem.erp_Location = item.ERP_Location;
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.diff = item.diff;
                        resultItem.goodsIssue_No = item.GoodsIssue_No;
                        resultItem.documentRef_No3 = item.DocumentRef_No3;
                        resultItem.document_Remark = item.Document_Remark;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("CheckPlanGiandGidiff");

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

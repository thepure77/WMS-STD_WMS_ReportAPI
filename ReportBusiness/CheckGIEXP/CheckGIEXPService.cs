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

namespace ReportBusiness.CheckGIEXP
{
    public class CheckGIEXPService
    {
        #region printReportLaborPerformance
        public dynamic printReportPan(CheckGIEXPViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<CheckGIEXPViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var product_Id = "";
                var shipTo_ID = "";
                var planGoodsIssue_No = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                //var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();

                var statusModels = new List<int?>();

                if (!string.IsNullOrEmpty(data.product_ID))
                {
                    product_Id = data.product_ID;
                }

                if (!string.IsNullOrEmpty(data.shipTo_Id))
                {
                    shipTo_ID = data.shipTo_Id;
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
                //if (!string.IsNullOrEmpty(data.product_Name))
                //{
                //    product_Name = data.product_Name;
                //}
                //if (!string.IsNullOrEmpty(data.sale_UNIT))
                //{
                //    sale_Unit = data.sale_UNIT;
                //}
                //@PRO_ID
                //@SHIP
                //@PLAN
                //@GI_DATE
                //@GI_DATE_TO

                var pro_Id = new SqlParameter("@PRO_ID", product_Id);
                var ship = new SqlParameter("@SHIP", shipTo_ID);
                var plan = new SqlParameter("@PLAN", planGoodsIssue_No);
                var gi_date = new SqlParameter("@GI_DATE", dateStart);
                var gi_date_to = new SqlParameter("@GI_DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_CheckGIEXP>();

                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_CheckGIEXP.FromSql("sp_CheckGIEXP @PRO_ID , @SHIP , @PLAN , @GI_DATE ,@GI_DATE_TO", pro_Id, ship, plan, gi_date, gi_date_to).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_CheckGIEXP.FromSql("sp_CheckGIEXP @PRO_ID , @SHIP , @PLAN , @GI_DATE ,@GI_DATE_TO", pro_Id, ship, plan, gi_date, gi_date_to).ToList();
                }

                //var resultquery = Master_DBContext.sp_CheckGIEXP.FromSql("sp_CheckGIEXP @PRO_ID , @SHIP , @PLAN , @GI_DATE ,@GI_DATE_TO", pro_Id, ship, plan, gi_date, gi_date_to).ToList();
                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new CheckGIEXPViewModel();
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

                        var resultItem = new CheckGIEXPViewModel();
                        resultItem.rowNo = num + 1; ;
                        resultItem.goodsIssue_No = item.GoodsIssue_No;
                        resultItem.goodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No;
                        resultItem.shipTo_Id = item.ShipTo_Id;
                        resultItem.shipTo_Name = item.ShipTo_Name;
                        resultItem.product_ID = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.goodsReceive_EXP_Date = item.GoodsReceive_EXP_Date;
                        resultItem.qty = item.Qty;
                        resultItem.totalQty = item.TotalQty;
                        resultItem.erp_Location = item.ERP_Location;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("CheckGIEXP");
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
        public string ExportExcel(CheckGIEXPViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<CheckGIEXPViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var product_Id = "";
                var shipTo_ID = "";
                var planGoodsIssue_No = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                //var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();

                var statusModels = new List<int?>();

                if (!string.IsNullOrEmpty(data.product_ID))
                {
                    product_Id = data.product_ID;
                }

                if (!string.IsNullOrEmpty(data.shipTo_Id))
                {
                    shipTo_ID = data.shipTo_Id;
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
                //if (!string.IsNullOrEmpty(data.product_Name))
                //{
                //    product_Name = data.product_Name;
                //}
                //if (!string.IsNullOrEmpty(data.sale_UNIT))
                //{
                //    sale_Unit = data.sale_UNIT;
                //}
                //@PRO_ID
                //@SHIP
                //@PLAN
                //@GI_DATE
                //@GI_DATE_TO

                var pro_Id = new SqlParameter("@PRO_ID", product_Id);
                var ship = new SqlParameter("@SHIP", shipTo_ID);
                var plan = new SqlParameter("@PLAN", planGoodsIssue_No);
                var gi_date = new SqlParameter("@GI_DATE", dateStart);
                var gi_date_to = new SqlParameter("@GI_DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_CheckGIEXP>();

                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_CheckGIEXP.FromSql("sp_CheckGIEXP @PRO_ID , @SHIP , @PLAN , @GI_DATE ,@GI_DATE_TO", pro_Id, ship, plan, gi_date, gi_date_to).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_CheckGIEXP.FromSql("sp_CheckGIEXP @PRO_ID , @SHIP , @PLAN , @GI_DATE ,@GI_DATE_TO", pro_Id, ship, plan, gi_date, gi_date_to).ToList();
                }

                //var resultquery = Master_DBContext.sp_CheckGIEXP.FromSql("sp_CheckGIEXP @PRO_ID , @SHIP , @PLAN , @GI_DATE ,@GI_DATE_TO", pro_Id, ship, plan, gi_date, gi_date_to).ToList();
                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new CheckGIEXPViewModel();
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

                        var resultItem = new CheckGIEXPViewModel();
                        resultItem.rowNo = num + 1; ;
                        resultItem.goodsIssue_No = item.GoodsIssue_No;
                        resultItem.goodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No;
                        resultItem.shipTo_Id = item.ShipTo_Id;
                        resultItem.shipTo_Name = item.ShipTo_Name;
                        resultItem.product_ID = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.goodsReceive_EXP_Date = item.GoodsReceive_EXP_Date;
                        resultItem.qty = item.Qty;
                        resultItem.totalQty = item.TotalQty;
                        resultItem.erp_Location = item.ERP_Location;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("CheckGIEXP");

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

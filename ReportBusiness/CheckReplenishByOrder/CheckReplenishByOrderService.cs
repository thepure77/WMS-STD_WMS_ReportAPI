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

namespace ReportBusiness.CheckReplenishByOrder
{
    public class CheckReplenishByOrderService
    {
        #region printReportLaborPerformance
        public dynamic printReportPan(CheckReplenishByOrderViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<CheckReplenishByOrderViewModel>();

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
                var resultquery = new List<MasterDataDataAccess.Models.sp_CheckReplenishByOrder>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_CheckReplenishByOrder.FromSql("sp_CheckReplenishByOrder @PRO_ID ", pro_Id).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_CheckReplenishByOrder.FromSql("sp_CheckReplenishByOrder @PRO_ID ", pro_Id).ToList();
                }

                //var resultquery = Master_DBContext.sp_CheckReplenishByOrder.FromSql("sp_CheckReplenishByOrder @PRO_ID ", pro_Id).ToList();
                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new CheckReplenishByOrderViewModel();
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

                        var resultItem = new CheckReplenishByOrderViewModel();
                        resultItem.rowNo = num + 1; ;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.bu_QTY = item.BU_QTY;
                        resultItem.order_QTY = item.Order_QTY;
                        resultItem.order_Unit = item.Order_Unit;
                        resultItem.su_QTY = item.SU_QTY;
                        resultItem.su_UNIT = item.SU_UNIT;
                        resultItem.su_Weight = item.SU_Weight;
                        resultItem.su_W = item.SU_W;
                        resultItem.su_L = item.SU_L;
                        resultItem.su_H = item.SU_H;
                        resultItem.isPeicePick = item.IsPeicePick;
                        resultItem.qtyInPiecePick_1 = item.QtyInPiecePick;
                        resultItem.qtyInPiecePick_2 = item.diff;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("CheckReplenishByOrder");
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
        public string ExportExcel(CheckReplenishByOrderViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<CheckReplenishByOrderViewModel>();

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
                var resultquery = new List<MasterDataDataAccess.Models.sp_CheckReplenishByOrder>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_CheckReplenishByOrder.FromSql("sp_CheckReplenishByOrder @PRO_ID ", pro_Id).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_CheckReplenishByOrder.FromSql("sp_CheckReplenishByOrder @PRO_ID ", pro_Id).ToList();
                }

                //var resultquery = Master_DBContext.sp_CheckReplenishByOrder.FromSql("sp_CheckReplenishByOrder @PRO_ID ", pro_Id).ToList();
                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new CheckReplenishByOrderViewModel();
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

                        var resultItem = new CheckReplenishByOrderViewModel();
                        resultItem.rowNo = num + 1; ;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.bu_QTY = item.BU_QTY;
                        resultItem.order_QTY = item.Order_QTY;
                        resultItem.order_Unit = item.Order_Unit;
                        resultItem.su_QTY = item.SU_QTY;
                        resultItem.su_UNIT = item.SU_UNIT;
                        resultItem.su_Weight = item.SU_Weight;
                        resultItem.su_W = item.SU_W;
                        resultItem.su_L = item.SU_L;
                        resultItem.su_H = item.SU_H;
                        resultItem.isPeicePick = item.IsPeicePick;
                        resultItem.qtyInPiecePick_1 = item.QtyInPiecePick;
                        resultItem.qtyInPiecePick_2 = item.diff;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("CheckReplenishByOrder");

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

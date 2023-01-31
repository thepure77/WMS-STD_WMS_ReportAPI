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

namespace ReportBusiness.ReportCheckStockAVB
{
    public class ReportCheckStockAVBService
    {
        #region printReport
        public dynamic printReportCheckStockAVB(ReportCheckStockAVBViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();
            //var BB_DBContext = new BinbalanceDbContext();


            var GI_DBContext = new PlanGIDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportCheckStockAVBViewModel>();

            try
            {
                var product_Id = "";
                //var product_Name = "";

                var statusModels = new List<int?>();
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                //if (!string.IsNullOrEmpty(data.product_Name))
                //{
                //    product_Name = data.product_Name;
                //}

                var pro_Id = new SqlParameter("@PRO_ID", product_Id);
                //var pro_Name = new SqlParameter("@PRO_NAME", product_Name);
                var resultquery = new List<MasterDataDataAccess.Models.sp_ReportCheckStockAVB>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_ReportCheckStockAVB.FromSql("sp_ReportCheckStockAVB @PRO_ID", pro_Id).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_ReportCheckStockAVB.FromSql("sp_ReportCheckStockAVB @PRO_ID", pro_Id).ToList();
                }

                //var resultquery = Master_DBContext.sp_ReportCheckStockAVB.FromSql("sp_ReportCheckStockAVB @PRO_ID", pro_Id).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportCheckStockAVBViewModel();
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

                        var resultItem = new ReportCheckStockAVBViewModel();
                        resultItem.rowNum = num + 1; ;
                        resultItem.currentDatetime = item.CurrentDatetime;
                        resultItem.last5Days = item.Last5Days != null ? item.Last5Days.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.bu_QtyOnHand = item.BU_QtyOnHand;
                        resultItem.bu_GIQty_5_Day = item.BU_GIQty_5_Day;
                        resultItem.open_BU_Qty = item.Open_BU_Qty;
                        resultItem.bu_Balance = item.BU_Balance;
                        resultItem.bu_UNIT = item.BU_UNIT;
                        resultItem.bu_Converage_Day = item.BU_Converage_Day;
                        resultItem.su_QtyOnHand = item.SU_QtyOnHand;
                        resultItem.su_GIQty_5_Day = item.SU_GIQty_5_Day;
                        resultItem.open_SU_Qty = item.Open_SU_Qty;
                        resultItem.su_Balance = item.SU_Balance;
                        resultItem.su_UNIT = item.SU_UNIT;
                        resultItem.su_Converage_Day = item.SU_Converage_Day;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportCheckStockAVB");
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

        public string ExportExcel(ReportCheckStockAVBViewModel data, string rootPath = "")
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
            var result = new List<ReportCheckStockAVBViewModel>();
            try
            {
                var product_Id = "";
                //var product_Name = "";

                var statusModels = new List<int?>();
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                //if (!string.IsNullOrEmpty(data.product_Name))
                //{
                //    product_Name = data.product_Name;
                //}

                var pro_Id = new SqlParameter("@PRO_ID", product_Id);
                //var pro_Name = new SqlParameter("@PRO_NAME", product_Name);
                var resultquery = new List<MasterDataDataAccess.Models.sp_ReportCheckStockAVB>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_ReportCheckStockAVB.FromSql("sp_ReportCheckStockAVB @PRO_ID", pro_Id).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_ReportCheckStockAVB.FromSql("sp_ReportCheckStockAVB @PRO_ID", pro_Id).ToList();
                }

                //var resultquery = Master_DBContext.sp_ReportCheckStockAVB.FromSql("sp_ReportCheckStockAVB @PRO_ID", pro_Id).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportCheckStockAVBViewModel();
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

                        var resultItem = new ReportCheckStockAVBViewModel();
                        resultItem.rowNum = num + 1; ;
                        resultItem.currentDatetime = item.CurrentDatetime;
                        resultItem.last5Days = item.Last5Days != null ? item.Last5Days.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.bu_QtyOnHand = item.BU_QtyOnHand;
                        resultItem.bu_GIQty_5_Day = item.BU_GIQty_5_Day;
                        resultItem.open_BU_Qty = item.Open_BU_Qty;
                        resultItem.bu_Balance = item.BU_Balance;
                        resultItem.bu_UNIT = item.BU_UNIT;
                        resultItem.bu_Converage_Day = item.BU_Converage_Day;
                        resultItem.su_QtyOnHand = item.SU_QtyOnHand;
                        resultItem.su_GIQty_5_Day = item.SU_GIQty_5_Day;
                        resultItem.open_SU_Qty = item.Open_SU_Qty;
                        resultItem.su_Balance = item.SU_Balance;
                        resultItem.su_UNIT = item.SU_UNIT;
                        resultItem.su_Converage_Day = item.SU_Converage_Day;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportCheckStockAVB");

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

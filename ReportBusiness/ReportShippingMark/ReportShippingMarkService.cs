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

namespace ReportBusiness.ReportShippingMark
{
    public class ReportShippingMarkService
    {
        #region printReport
        public dynamic printReportShippingMark(ReportShippingMarkViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            //var BB_DBContext = new BinbalanceDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var GI_DBContext = new PlanGIDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportShippingMarkViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var tempCondition = "";
                var business_Unit = "";
                var dO_NO = "";
                var shipto_Address = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                var statusModels = new List<int?>();
                //if (!string.IsNullOrEmpty(data.warehouse))
                //{
                //    tempCondition = data.warehouse;
                //}
                if (data.businessUnitList != null)
                {
                    business_Unit = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.dO_NO))
                {
                    dO_NO = data.dO_NO;
                }
                if (!string.IsNullOrEmpty(data.shipto_Address))
                {
                    shipto_Address = data.shipto_Address;
                }
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }

                //var temp = new SqlParameter("@TempCondition_Index", tempCondition);
                var bu = new SqlParameter("@BusinessUnit_Index", business_Unit);
                var plan = new SqlParameter("@Ref_Document_Index", dO_NO);
                var shipto = new SqlParameter("@ShipTo_Index", shipto_Address);
                var date = new SqlParameter("@Date", dateStart);
                var date_to = new SqlParameter("@Date_To", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_03_ShippingMark>();
                if (data.warehouse != "02") 
                {
                    resultquery = Master_DBContext.sp_rpt_03_ShippingMark.FromSql("sp_rpt_03_ShippingMark  @BusinessUnit_Index,@Ref_Document_Index,@ShipTo_Index,@Date,@Date_To", bu, plan, shipto, date, date_to).ToList();

                }else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_03_ShippingMark.FromSql("sp_rpt_03_ShippingMark  @BusinessUnit_Index,@Ref_Document_Index,@ShipTo_Index,@Date,@Date_To", bu, plan, shipto, date, date_to).ToList();
                }


                //var resultquery = Master_DBContext.sp_ReportPick.FromSql("sp_ReportPick @GT,@TAG,@LOC_TO,@PRO_ID,@C_DATE,@C_DATE_TO", gt, tag, loc_name, pro_id, c_date, c_date_to).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportShippingMarkViewModel();
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

                        var resultItem = new ReportShippingMarkViewModel();

                        resultItem.rowIndex = item.RowIndex;
                        resultItem.rowNum = num + 1;
                        resultItem.warehouse = item.Warehouse;
                        resultItem.business_Unit = item.Business_Unit;
                        resultItem.dO_NO = item.DO_NO;
                        resultItem.sO_NO = item.SO_NO;
                        resultItem.pallet = item.Pallet; 
                        resultItem.product = item.Product;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.qty = item.Qty;
                        resultItem.unit = item.Unit;
                        resultItem.eXP_Date = item.EXP_Date != null ? item.EXP_Date.Value.ToString("dd/MM/yy") : "";
                        resultItem.batch_Lot = item.Batch_Lot;
                        resultItem.shipto_Address = item.Shipto_Address;
                        resultItem.doc_date = item.Doc_date != null ? item.Doc_date.Value.ToString("dd/MM/yy") : "";
                        result.Add(resultItem);
                        num++;
                    }
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportShippingMark");
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

        public string ExportExcel(ReportShippingMarkViewModel data, string rootPath = "")
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
            var result = new List<ReportShippingMarkViewModel>();
            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var tempCondition = "";
                var business_Unit = "";
                var dO_NO = "";
                var shipto_Address = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                var statusModels = new List<int?>();
                //if (!string.IsNullOrEmpty(data.warehouse))
                //{
                //    tempCondition = data.warehouse;
                //}
                if (data.businessUnitList != null)
                {
                    business_Unit = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.dO_NO))
                {
                    dO_NO = data.dO_NO;
                }
                if (!string.IsNullOrEmpty(data.shipto_Address))
                {
                    shipto_Address = data.shipto_Address;
                }
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }

                //var temp = new SqlParameter("@TempCondition_Index", tempCondition);
                var bu = new SqlParameter("@BusinessUnit_Index", business_Unit);
                var plan = new SqlParameter("@Ref_Document_Index", dO_NO);
                var shipto = new SqlParameter("@ShipTo_Index", shipto_Address);
                var date = new SqlParameter("@Date", dateStart);
                var date_to = new SqlParameter("@Date_To", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_03_ShippingMark>();
                if (data.warehouse != "02")
                {
                    resultquery = Master_DBContext.sp_rpt_03_ShippingMark.FromSql("sp_rpt_03_ShippingMark  @BusinessUnit_Index,@Ref_Document_Index,@ShipTo_Index,@Date,@Date_To", bu, plan, shipto, date, date_to).ToList();

                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_03_ShippingMark.FromSql("sp_rpt_03_ShippingMark  @BusinessUnit_Index,@Ref_Document_Index,@ShipTo_Index,@Date,@Date_To", bu, plan, shipto, date, date_to).ToList();
                }


                //var resultquery = Master_DBContext.sp_ReportPick.FromSql("sp_ReportPick @GT,@TAG,@LOC_TO,@PRO_ID,@C_DATE,@C_DATE_TO", gt, tag, loc_name, pro_id, c_date, c_date_to).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportShippingMarkViewModel();
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

                        var resultItem = new ReportShippingMarkViewModel();

                        resultItem.rowIndex = item.RowIndex;
                        resultItem.rowNum = num + 1;
                        resultItem.warehouse = item.Warehouse;
                        resultItem.business_Unit = item.Business_Unit;
                        resultItem.dO_NO = item.DO_NO;
                        resultItem.sO_NO = item.SO_NO;
                        resultItem.pallet = item.Pallet;
                        resultItem.product = item.Product;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.qty = item.Qty;
                        resultItem.unit = item.Unit;
                        resultItem.eXP_Date = item.EXP_Date != null ? item.EXP_Date.Value.ToString("dd/MM/yy") : "";
                        resultItem.batch_Lot = item.Batch_Lot;
                        resultItem.shipto_Address = item.Shipto_Address;
                        resultItem.doc_date = item.Doc_date != null ? item.Doc_date.Value.ToString("dd/MM/yy") : "";
                        result.Add(resultItem);
                        num++;
                    }
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportShippingMark");

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

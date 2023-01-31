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

namespace ReportBusiness.ReportPlan
{
    public class ReportPlanService
    {
        #region printReport
        public dynamic printReportPlan(ReportPlanViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            //var BB_DBContext = new BinbalanceDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var GI_DBContext = new PlanGIDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportPlanViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                //var tempCondition = "";
                var business_Unit = "";
                var dO_NO = "";
                var shipto_Address = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                var statusModels = new List<int?>();
                //if (!string.IsNullOrEmpty(data.tempCondition))
                //{
                //    tempCondition = data.tempCondition;
                //}
                if (data.businessUnitList != null)
                {
                    business_Unit = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.dO_NO))
                {
                    dO_NO = data.dO_NO;
                }
                if (!string.IsNullOrEmpty(data.ship_to))
                {
                    shipto_Address = data.ship_to;
                }
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }

                //var temp = new SqlParameter("@TempCondition_Index", tempCondition);
                var bu = new SqlParameter("@BusinessUnit_Index", business_Unit);
                var plan = new SqlParameter("@PlanGoodsissue_Index", dO_NO);
                var shipto = new SqlParameter("@ShipTo_Index", shipto_Address);
                var date = new SqlParameter("@Date", dateStart);
                var date_to = new SqlParameter("@Date_To", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_04_Pickingplan >();
                if (data.tempCondition != "02")
                {
                    resultquery = Master_DBContext.sp_rpt_04_Pickingplan.FromSql("sp_rpt_04_Pickingplan  @BusinessUnit_Index,@PlanGoodsissue_Index,@ShipTo_Index,@Date,@Date_To", bu, plan, shipto, date, date_to).ToList();

                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_04_Pickingplan.FromSql("sp_rpt_04_Pickingplan  @BusinessUnit_Index,@PlanGoodsissue_Index,@ShipTo_Index,@Date,@Date_To", bu, plan, shipto, date, date_to).ToList();

                }



                //var resultquery = Master_DBContext.sp_ReportPick.FromSql("sp_ReportPick @GT,@TAG,@LOC_TO,@PRO_ID,@C_DATE,@C_DATE_TO", gt, tag, loc_name, pro_id, c_date, c_date_to).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportPlanViewModel();
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

                        var resultItem = new ReportPlanViewModel();

                        resultItem.rowIndex = item.RowIndex;
                        resultItem.rowNum = num + 1;
                        resultItem.tempCondition = item.TempCondition;
                        resultItem.business_Unit = item.Business_Unit;
                        resultItem.dO_NO = item.DO_NO;
                        resultItem.sO_NO = item.SO_NO;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.doc_date = item.Doc_date != null ? item.Doc_date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.shipto_Address = item.Shipto_Address;
                        resultItem.tote = item.Tote;
                        resultItem.total_qty = item.Total_qty;
                        resultItem.unit_BU = item.Unit_BU;
                        resultItem.qty = item.Qty;
                        resultItem.unit_SU = item.Unit_SU;
                        resultItem.bu_Qty = item.Bu_Qty;
                        resultItem.bU_SU = item.BU_SU;
                        resultItem.netWeight_KG = item.NetWeight_KG;
                        resultItem.grsWeight_KG = item.GrsWeight_KG;
                        resultItem.wEIGHT_PC_KG = item.WEIGHT_PC_KG;
                        resultItem.cBM_SU = item.CBM_SU;
                        resultItem.cBM = item.CBM;
                        resultItem.productConversion_Height = item.ProductConversion_Height;
                        resultItem.productConversion_Length = item.ProductConversion_Length;
                        resultItem.productConversion_Width = item.ProductConversion_Width;
                        resultItem.ref_No1 = item.Ref_No1;
                        resultItem.ref_No2 = item.Ref_No2;

                        //if (data.ambientRoom != "02")
                        //{
                        //    resultItem.ambientRoom = "Ambient";
                        //}
                        //else
                        //{
                        //    resultItem.ambientRoom = "Freeze";
                        //}
                        result.Add(resultItem);
                        num++;
                    }
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportPlan");
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

        public string ExportExcel(ReportPlanViewModel data, string rootPath = "")
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
            var result = new List<ReportPlanViewModel>();
            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                //var tempCondition = "";
                var business_Unit = "";
                var dO_NO = "";
                var shipto_Address = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                var statusModels = new List<int?>();
                //if (!string.IsNullOrEmpty(data.tempCondition))
                //{
                //    tempCondition = data.tempCondition;
                //}
                if (data.businessUnitList != null)
                {
                    business_Unit = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.dO_NO))
                {
                    dO_NO = data.dO_NO;
                }
                if (!string.IsNullOrEmpty(data.ship_to))
                {
                    shipto_Address = data.ship_to;
                }
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }

                //var temp = new SqlParameter("@TempCondition_Index", tempCondition);
                var bu = new SqlParameter("@BusinessUnit_Index", business_Unit);
                var plan = new SqlParameter("@PlanGoodsissue_Index", dO_NO);
                var shipto = new SqlParameter("@ShipTo_Index", shipto_Address);
                var date = new SqlParameter("@Date", dateStart);
                var date_to = new SqlParameter("@Date_To", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_04_Pickingplan>();
                if (data.tempCondition != "02")
                {
                    resultquery = Master_DBContext.sp_rpt_04_Pickingplan.FromSql("sp_rpt_04_Pickingplan  @BusinessUnit_Index,@PlanGoodsissue_Index,@ShipTo_Index,@Date,@Date_To", bu, plan, shipto, date, date_to).ToList();

                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_04_Pickingplan.FromSql("sp_rpt_04_Pickingplan  @BusinessUnit_Index,@PlanGoodsissue_Index,@ShipTo_Index,@Date,@Date_To", bu, plan, shipto, date, date_to).ToList();

                }



                //var resultquery = Master_DBContext.sp_ReportPick.FromSql("sp_ReportPick @GT,@TAG,@LOC_TO,@PRO_ID,@C_DATE,@C_DATE_TO", gt, tag, loc_name, pro_id, c_date, c_date_to).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportPlanViewModel();
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

                        var resultItem = new ReportPlanViewModel();

                        resultItem.rowIndex = item.RowIndex;
                        resultItem.rowNum = num + 1;
                        resultItem.tempCondition = item.TempCondition;
                        resultItem.business_Unit = item.Business_Unit;
                        resultItem.dO_NO = item.DO_NO;
                        resultItem.sO_NO = item.SO_NO;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.doc_date = item.Doc_date != null ? item.Doc_date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.shipto_Address = item.Shipto_Address;
                        resultItem.tote = item.Tote;
                        resultItem.total_qty = item.Total_qty;
                        resultItem.unit_BU = item.Unit_BU;
                        resultItem.qty = item.Qty;
                        resultItem.unit_SU = item.Unit_SU;
                        resultItem.bu_Qty = item.Bu_Qty;
                        resultItem.bU_SU = item.BU_SU;
                        resultItem.netWeight_KG = item.NetWeight_KG;
                        resultItem.grsWeight_KG = item.GrsWeight_KG;
                        resultItem.wEIGHT_PC_KG = item.WEIGHT_PC_KG;
                        resultItem.cBM_SU = item.CBM_SU;
                        resultItem.cBM = item.CBM;
                        resultItem.productConversion_Height = item.ProductConversion_Height;
                        resultItem.productConversion_Length = item.ProductConversion_Length;
                        resultItem.productConversion_Width = item.ProductConversion_Width;
                        resultItem.ref_No1 = item.Ref_No1;
                        resultItem.ref_No2 = item.Ref_No2;
                        //if (data.ambientRoom != "02")
                        //{
                        //    resultItem.ambientRoom = "Ambient";
                        //}
                        //else
                        //{
                        //    resultItem.ambientRoom = "Freeze";
                        //}
                        result.Add(resultItem);
                        num++;
                    }
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportPlan");

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

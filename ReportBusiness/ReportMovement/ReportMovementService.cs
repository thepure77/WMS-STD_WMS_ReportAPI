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

namespace ReportBusiness.ReportMovement
{
    public class ReportMovementService
    {
        #region printReport
        public dynamic printReportMovement(ReportMovementViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            //var BB_DBContext = new BinbalanceDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var GI_DBContext = new PlanGIDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportMovementViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                //var tempCondition = "";
                var business_Unit = "";
                var product_Id = "";
                var tag_no = "";
                DateTime gr_Date = DateTime.Now.toString().toBetweenDate().start;
                DateTime gr_Date_To = DateTime.Now.toString().toBetweenDate().end;
                var batch_Lot = "";
                DateTime exp_Date = DateTime.Now.toString().toBetweenDate().start;
                DateTime exp_Date_To = DateTime.Now.toString().toBetweenDate().end;
                DateTime mfg_Date = DateTime.Now.toString().toBetweenDate().start;
                DateTime mfg_Date_To = DateTime.Now.toString().toBetweenDate().end;
                var gr_Date_null = "";
                var gr_Date_To_null = "";
                var exp_Date_null = "";
                var exp_Date_To_null = "";
                var mfg_Date_null = "";
                var mfg_Date_To_null = "";

                var statusModels = new List<int?>();

                //if (!string.IsNullOrEmpty(data.tempCondition))
                //{
                //    tempCondition = data.tempCondition;
                //}
                if (data.businessUnitList != null)
                {
                    business_Unit = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    tag_no = data.tag_No;
                }
                var gr_d = new SqlParameter();
                var gr_dt = new SqlParameter();
                if (!string.IsNullOrEmpty(data.goodsReceive_Date))
                {
                    gr_Date = data.goodsReceive_Date.toBetweenDate().start;
                    gr_Date_To = data.goodsReceive_Date_To.toBetweenDate().end;

                    gr_d = new SqlParameter("@Update_Date", gr_Date);
                    gr_dt = new SqlParameter("@Update_Date_To", gr_Date_To);
                }
                else
                {
                    gr_d = new SqlParameter("@Update_Date", gr_Date_null);
                    gr_dt = new SqlParameter("@Update_Date_To", gr_Date_To_null);
                }
                if (!string.IsNullOrEmpty(data.product_Lot))
                {
                    batch_Lot = data.product_Lot;
                }
                var exp_d = new SqlParameter();
                var exp_dt = new SqlParameter();
                if (!string.IsNullOrEmpty(data.goodsReceive_EXP_Date) && !string.IsNullOrEmpty(data.goodsReceive_EXP_Date_To))
                {
                    exp_Date = data.goodsReceive_EXP_Date.toBetweenDate().start;
                    exp_Date_To = data.goodsReceive_EXP_Date_To.toBetweenDate().end;

                    exp_d = new SqlParameter("@Exp_Date", exp_Date);
                    exp_dt = new SqlParameter("@Exp_Date_To", exp_Date_To);
                }
                else
                {
                    exp_d = new SqlParameter("@Exp_Date", exp_Date_null);
                    exp_dt = new SqlParameter("@Exp_Date_To", exp_Date_To_null);
                }

                var mfg_d = new SqlParameter();
                var mfg_dt = new SqlParameter();
                if (!string.IsNullOrEmpty(data.goodsReceive_MFG_Date) && !string.IsNullOrEmpty(data.goodsReceive_MFG_Date_To))
                {
                    mfg_Date = data.goodsReceive_MFG_Date.toBetweenDate().start;
                    mfg_Date_To = data.goodsReceive_MFG_Date_To.toBetweenDate().end;

                    mfg_d = new SqlParameter("@Mfg_Date", mfg_Date);
                    mfg_dt = new SqlParameter("@Mfg_Date_To", mfg_Date_To);
                }
                else
                {
                    mfg_d = new SqlParameter("@Mfg_Date", mfg_Date_null);
                    mfg_dt = new SqlParameter("@Mfg_Date_To", mfg_Date_To_null);
                }

                ///var temp = new SqlParameter("@TempCondition_Index", tempCondition);
                var bu = new SqlParameter("@BusinessUnit_Index", business_Unit);
                var proID = new SqlParameter("@Product_Id", product_Id);
                var tag = new SqlParameter("@Tag_No", tag_no);
                var batch = new SqlParameter("@Batch_Lot", batch_Lot);

                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_18_Movement>();
                if (data.warehouse_Type != "02")
                {
                    resultquery = Master_DBContext.sp_rpt_18_Movement.FromSql("sp_rpt_18_Movement  @BusinessUnit_Index,@Product_Id,@Tag_No,@Update_Date,@Update_Date_To,@Batch_Lot,@Exp_Date,@Exp_Date_To,@Mfg_Date,@Mfg_Date_To", bu, proID, tag, gr_d, gr_dt, batch, exp_d, exp_dt, mfg_d, mfg_dt).ToList();

                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_18_Movement.FromSql("sp_rpt_18_Movement  @BusinessUnit_Index,@Product_Id,@Tag_No,@Update_Date,@Update_Date_To,@Batch_Lot,@Exp_Date,@Exp_Date_To,@Mfg_Date,@Mfg_Date_To", bu, proID, tag, gr_d, gr_dt, batch, exp_d, exp_dt, mfg_d, mfg_dt).ToList();

                }


                //var resultquery = Master_DBContext.sp_ReportPick.FromSql("sp_ReportPick @GT,@TAG,@LOC_TO,@PRO_ID,@C_DATE,@C_DATE_TO", gt, tag, loc_name, pro_id, c_date, c_date_to).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportMovementViewModel();
                    resultItem.showDate = "ข้อมูลวันที่ " + data.goodsReceive_Date_Show + " ถึง " + data.goodsReceive_Date_To_Show;
                    //var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    //var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    //resultItem.report_date = startDate;
                    //resultItem.report_date_to = endDate;
                    result.Add(resultItem);
                    //var resultItem = new ReportMovementViewModel();
                    //var GI_D = DateTime.ParseExact(data.goodsIssue_Date.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    //var delivery_Date_ = DateTime.ParseExact(data.expect_Delivery_Date.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);
                    //var delivery_Date_To_ = DateTime.ParseExact(data.expect_Delivery_Date_To.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    //var _load_Date_ = DateTime.ParseExact(data.expect_Delivery_Date.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);
                    //var _load_Date_To_ = DateTime.ParseExact(data.expect_Delivery_Date_To.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    //resultItem.goodsIssue_Date = GI_D;
                    //resultItem.expect_Delivery_Date = delivery_Date_;
                    //resultItem.expect_Delivery_Date_To = delivery_Date_To_;
                    //resultItem.appointment_Date = _load_Date_;
                    //resultItem.appointment_Date_To = _load_Date_To_;
                    //result.Add(resultItem);
                }
                else
                {
                    int num = 0;
                    foreach (var item in resultquery)
                    {
                        //var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                        //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        //var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                        //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new ReportMovementViewModel();
                        resultItem.showDate = "ข้อมูลวันที่ " + data.goodsReceive_Date_Show +" ถึง "+ data.goodsReceive_Date_To_Show;
                        resultItem.row_Index = item.Row_Index;
                        resultItem.rowNum = num + 1;
                        resultItem.warehouse_Type = item.Warehouse_Type;
                        resultItem.businessUnit_Name = item.BusinessUnit_Name;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.vendor_Id = item.Vendor_Id;
                        resultItem.vendor_Name = item.Vendor_Name;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.binCard_QtyIn = item.BinCard_QtyIn;
                        resultItem.binCard_QtyOut = item.BinCard_QtyIn;
                        resultItem.typeMovement = item.TypeMovement;
                        resultItem.productConversion_Id = item.ProductConversion_Id;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.goodsReceive_Date = item.GoodsReceive_Date != null ? item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.tag_No = item.Tag_No;
                        resultItem.wMS_Sloc = item.WMS_Sloc;
                        resultItem.sAP_Sloc = item.SAP_Sloc;
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.goodsReceive_MFG_Date = item.GoodsReceive_MFG_Date != null ? item.GoodsReceive_MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.goodsReceive_EXP_Date = item.GoodsReceive_EXP_Date != null ? item.GoodsReceive_EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                        result.Add(resultItem);
                        num++;
                    }
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportMovement");
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
                olog.logging("ReportMovement", ex.Message);
                throw ex;
            }
        }
        #endregion

        public string ExportExcel(ReportMovementViewModel data, string rootPath = "")
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
            var result = new List<ReportMovementViewModel>();
            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                //var tempCondition = "";
                var business_Unit = "";
                var product_Id = "";
                var tag_no = "";
                DateTime gr_Date = DateTime.Now.toString().toBetweenDate().start;
                DateTime gr_Date_To = DateTime.Now.toString().toBetweenDate().end;
                var batch_Lot = "";
                DateTime exp_Date = DateTime.Now.toString().toBetweenDate().start;
                DateTime exp_Date_To = DateTime.Now.toString().toBetweenDate().end;
                DateTime mfg_Date = DateTime.Now.toString().toBetweenDate().start;
                DateTime mfg_Date_To = DateTime.Now.toString().toBetweenDate().end;
                var gr_Date_null = "";
                var gr_Date_To_null = "";
                var exp_Date_null = "";
                var exp_Date_To_null = "";
                var mfg_Date_null = "";
                var mfg_Date_To_null = "";

                var statusModels = new List<int?>();

                //if (!string.IsNullOrEmpty(data.tempCondition))
                //{
                //    tempCondition = data.tempCondition;
                //}
                if (data.businessUnitList != null)
                {
                    business_Unit = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    tag_no = data.tag_No;
                }
                var gr_d = new SqlParameter();
                var gr_dt = new SqlParameter();
                if (!string.IsNullOrEmpty(data.goodsReceive_Date))
                {
                    gr_Date = data.goodsReceive_Date.toBetweenDate().start;
                    gr_Date_To = data.goodsReceive_Date_To.toBetweenDate().end;

                    gr_d = new SqlParameter("@Update_Date", gr_Date);
                    gr_dt = new SqlParameter("@Update_Date_To", gr_Date_To);
                }
                else
                {
                    gr_d = new SqlParameter("@Update_Date", gr_Date_null);
                    gr_dt = new SqlParameter("@Update_Date_To", gr_Date_To_null);
                }
                if (!string.IsNullOrEmpty(data.product_Lot))
                {
                    batch_Lot = data.product_Lot;
                }
                var exp_d = new SqlParameter();
                var exp_dt = new SqlParameter();
                if (!string.IsNullOrEmpty(data.goodsReceive_EXP_Date) && !string.IsNullOrEmpty(data.goodsReceive_EXP_Date_To))
                {
                    exp_Date = data.goodsReceive_EXP_Date.toBetweenDate().start;
                    exp_Date_To = data.goodsReceive_EXP_Date_To.toBetweenDate().end;

                    exp_d = new SqlParameter("@Exp_Date", exp_Date);
                    exp_dt = new SqlParameter("@Exp_Date_To", exp_Date_To);
                }
                else
                {
                    exp_d = new SqlParameter("@Exp_Date", exp_Date_null);
                    exp_dt = new SqlParameter("@Exp_Date_To", exp_Date_To_null);
                }

                var mfg_d = new SqlParameter();
                var mfg_dt = new SqlParameter();
                if (!string.IsNullOrEmpty(data.goodsReceive_MFG_Date) && !string.IsNullOrEmpty(data.goodsReceive_MFG_Date_To))
                {
                    mfg_Date = data.goodsReceive_MFG_Date.toBetweenDate().start;
                    mfg_Date_To = data.goodsReceive_MFG_Date_To.toBetweenDate().end;

                    mfg_d = new SqlParameter("@Mfg_Date", mfg_Date);
                    mfg_dt = new SqlParameter("@Mfg_Date_To", mfg_Date_To);
                }
                else
                {
                    mfg_d = new SqlParameter("@Mfg_Date", mfg_Date_null);
                    mfg_dt = new SqlParameter("@Mfg_Date_To", mfg_Date_To_null);
                }

                ///var temp = new SqlParameter("@TempCondition_Index", tempCondition);
                var bu = new SqlParameter("@BusinessUnit_Index", business_Unit);
                var proID = new SqlParameter("@Product_Id", product_Id);
                var tag = new SqlParameter("@Tag_No", tag_no);
                var batch = new SqlParameter("@Batch_Lot", batch_Lot);

                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_18_Movement>();
                if (data.warehouse_Type != "02")
                {
                    resultquery = Master_DBContext.sp_rpt_18_Movement.FromSql("sp_rpt_18_Movement  @BusinessUnit_Index,@Product_Id,@Tag_No,@Update_Date,@Update_Date_To,@Batch_Lot,@Exp_Date,@Exp_Date_To,@Mfg_Date,@Mfg_Date_To", bu, proID, tag, gr_d, gr_dt, batch, exp_d, exp_dt, mfg_d, mfg_dt).ToList();

                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_18_Movement.FromSql("sp_rpt_18_Movement  @BusinessUnit_Index,@Product_Id,@Tag_No,@Update_Date,@Update_Date_To,@Batch_Lot,@Exp_Date,@Exp_Date_To,@Mfg_Date,@Mfg_Date_To", bu, proID, tag, gr_d, gr_dt, batch, exp_d, exp_dt, mfg_d, mfg_dt).ToList();

                }


                //var resultquery = Master_DBContext.sp_ReportPick.FromSql("sp_ReportPick @GT,@TAG,@LOC_TO,@PRO_ID,@C_DATE,@C_DATE_TO", gt, tag, loc_name, pro_id, c_date, c_date_to).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportMovementViewModel();
                    resultItem.showDate = "ข้อมูลวันที่ " + data.goodsReceive_Date_Show + " ถึง " + data.goodsReceive_Date_To_Show;
                    //var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    //var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    //resultItem.report_date = startDate;
                    //resultItem.report_date_to = endDate;
                    result.Add(resultItem);
                    //var resultItem = new ReportMovementViewModel();
                    //var GI_D = DateTime.ParseExact(data.goodsIssue_Date.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    //var delivery_Date_ = DateTime.ParseExact(data.expect_Delivery_Date.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);
                    //var delivery_Date_To_ = DateTime.ParseExact(data.expect_Delivery_Date_To.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    //var _load_Date_ = DateTime.ParseExact(data.expect_Delivery_Date.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);
                    //var _load_Date_To_ = DateTime.ParseExact(data.expect_Delivery_Date_To.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    //resultItem.goodsIssue_Date = GI_D;
                    //resultItem.expect_Delivery_Date = delivery_Date_;
                    //resultItem.expect_Delivery_Date_To = delivery_Date_To_;
                    //resultItem.appointment_Date = _load_Date_;
                    //resultItem.appointment_Date_To = _load_Date_To_;
                    //result.Add(resultItem);
                }
                else
                {
                    int num = 0;
                    foreach (var item in resultquery)
                    {
                        //var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                        //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        //var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                        //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new ReportMovementViewModel();
                        resultItem.showDate = "ข้อมูลวันที่ " + data.goodsReceive_Date_Show + " ถึง " + data.goodsReceive_Date_To_Show;
                        resultItem.row_Index = item.Row_Index;
                        resultItem.rowNum = num + 1;
                        resultItem.warehouse_Type = item.Warehouse_Type;
                        resultItem.businessUnit_Name = item.BusinessUnit_Name;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.vendor_Id = item.Vendor_Id;
                        resultItem.vendor_Name = item.Vendor_Name;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.binCard_QtyIn = item.BinCard_QtyIn;
                        resultItem.binCard_QtyOut = item.BinCard_QtyIn;
                        resultItem.typeMovement = item.TypeMovement;
                        resultItem.productConversion_Id = item.ProductConversion_Id;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.goodsReceive_Date = item.GoodsReceive_Date != null ? item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.tag_No = item.Tag_No;
                        resultItem.wMS_Sloc = item.WMS_Sloc;
                        resultItem.sAP_Sloc = item.SAP_Sloc;
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.goodsReceive_MFG_Date = item.GoodsReceive_MFG_Date != null ? item.GoodsReceive_MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.goodsReceive_EXP_Date = item.GoodsReceive_EXP_Date != null ? item.GoodsReceive_EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                        result.Add(resultItem);
                        num++;
                    }
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportMovement");

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
                //olog.logging("ReportMovement", ex.Message);
                throw ex;
            }

        }

        
    }
}

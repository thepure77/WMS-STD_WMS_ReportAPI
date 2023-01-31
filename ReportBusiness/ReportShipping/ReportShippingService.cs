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

namespace ReportBusiness.ReportShipping
{
    public class ReportShippingService
    {
        #region printReport
        public dynamic printReportShipping(ReportShippingViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            //var BB_DBContext = new BinbalanceDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var GI_DBContext = new PlanGIDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportShippingViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                //var tempCondition = "";
                var business_Unit = "";
                var shipment = "";
                var planGoodsissue = "";
                var Goodsissue = "";
                var tag_No = "";
                var product_Id = "";
                var branch = "";
                DateTime gi_date = DateTime.Now.toString().toBetweenDate().start;
                DateTime gi_date_To = DateTime.Now.toString().toBetweenDate().end;
                DateTime expect_Delivery_Date = DateTime.Now.toString().toBetweenDate().start;
                DateTime expect_Delivery_Date_To = DateTime.Now.toString().toBetweenDate().end;
                DateTime load_Date = DateTime.Now.toString().toBetweenDate().start;
                DateTime load_Date_To = DateTime.Now.toString().toBetweenDate().end;
                var gi_date_null = "";
                var gi_date_to_null = "";
                var expect_Delivery_Date_null = "";
                var expect_Delivery_Date_To_null = "";
                var load_Date_null = "";
                var load_Date_To_null = "";
                var statusModels = new List<int?>();

                //if (!string.IsNullOrEmpty(data.tempCondition))
                //{
                //    tempCondition = data.tempCondition;
                //}
                if (data.businessUnitList != null)
                {
                    business_Unit = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.shipment))
                {
                    shipment = data.shipment;
                }
                if (!string.IsNullOrEmpty(data.planGoodsIssue_No))
                {
                    planGoodsissue = data.planGoodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    Goodsissue = data.goodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.tag_no))
                {
                    tag_No = data.tag_no;
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                if (!string.IsNullOrEmpty(data.branch))
                {
                    branch = data.branch;
                }

                var gi_d = new SqlParameter();
                var gi_dt = new SqlParameter();
                if (!string.IsNullOrEmpty(data.goodsIssue_Date))
                {
                    gi_date = data.goodsIssue_Date.toBetweenDate().start;
                    gi_date_To = data.goodsIssue_Date_To.toBetweenDate().end;

                    gi_d = new SqlParameter("@GI_Date", gi_date);
                    gi_dt = new SqlParameter("@GI_Date_To", gi_date_To);
                }
                else
                {
                    gi_d = new SqlParameter("@GI_Date", gi_date_null);
                    gi_dt = new SqlParameter("@GI_Date_To", gi_date_to_null);
                }

                var delivery_Date = new SqlParameter();
                var delivery_Date_To = new SqlParameter();
                if (!string.IsNullOrEmpty(data.expect_Delivery_Date) && !string.IsNullOrEmpty(data.expect_Delivery_Date_To))
                {
                    expect_Delivery_Date = data.expect_Delivery_Date.toBetweenDate().start;
                    expect_Delivery_Date_To = data.expect_Delivery_Date_To.toBetweenDate().end;

                    delivery_Date = new SqlParameter("@Expect_Delivery_Date", expect_Delivery_Date);
                    delivery_Date_To = new SqlParameter("@Expect_Delivery_Date_To", expect_Delivery_Date_To);
                }
                else
                {
                    delivery_Date = new SqlParameter("@Expect_Delivery_Date", expect_Delivery_Date_null);
                    delivery_Date_To = new SqlParameter("@Expect_Delivery_Date_To", expect_Delivery_Date_To_null);
                }

                var load_Date_ = new SqlParameter();
                var load_Date_To_ = new SqlParameter();
                if (!string.IsNullOrEmpty(data.appointment_Date) && !string.IsNullOrEmpty(data.appointment_Date_To))
                {
                    load_Date = data.appointment_Date.toBetweenDate().start;
                    load_Date_To = data.appointment_Date_To.toBetweenDate().end;

                    load_Date_ = new SqlParameter("@Load_Date", load_Date);
                    load_Date_To_ = new SqlParameter("@Load_Date_To", load_Date_To);
                }
                else
                {
                    load_Date_ = new SqlParameter("@Load_Date", load_Date_null);
                    load_Date_To_ = new SqlParameter("@Load_Date_To", load_Date_To_null);
                }

                //var temp = new SqlParameter("@TempCondition_Index", tempCondition);
                var bu = new SqlParameter("@BusinessUnit_Index", business_Unit);
                var ship = new SqlParameter("@Shipment_Index", shipment);
                var planGI = new SqlParameter("@PlanGoodsissue_Index", planGoodsissue);
                var giNo = new SqlParameter("@Goodsissue_index", Goodsissue);
                var tag = new SqlParameter("@Tag_No", tag_No);
                var proID = new SqlParameter("@Product_Index", product_Id);
                var bar = new SqlParameter("@Branch", branch);

                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_14_Shipping>();
                if(data.tempCondition != "02")
                {
                    resultquery = Master_DBContext.sp_rpt_14_Shipping.FromSql("sp_rpt_14_Shipping  @BusinessUnit_Index,@Shipment_Index,@PlanGoodsissue_Index,@Goodsissue_index,@Tag_No,@Product_Index,@Branch,@GI_Date,@GI_Date_To,@Expect_Delivery_Date,@Expect_Delivery_Date_To,@Load_Date,@Load_Date_To", bu, ship, planGI, giNo, tag, proID, bar, gi_d, gi_dt, delivery_Date, delivery_Date_To, load_Date_, load_Date_To_).ToList();

                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_14_Shipping.FromSql("sp_rpt_14_Shipping  @BusinessUnit_Index,@Shipment_Index,@PlanGoodsissue_Index,@Goodsissue_index,@Tag_No,@Product_Index,@Branch,@GI_Date,@GI_Date_To,@Expect_Delivery_Date,@Expect_Delivery_Date_To,@Load_Date,@Load_Date_To", bu, ship, planGI, giNo, tag, proID, bar, gi_d, gi_dt, delivery_Date, delivery_Date_To, load_Date_, load_Date_To_).ToList();
                }


                //var resultquery = Master_DBContext.sp_ReportPick.FromSql("sp_ReportPick @GT,@TAG,@LOC_TO,@PRO_ID,@C_DATE,@C_DATE_TO", gt, tag, loc_name, pro_id, c_date, c_date_to).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportShippingViewModel();
                    //var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    //var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    //resultItem.report_date = startDate;
                    //resultItem.report_date_to = endDate;
                    result.Add(resultItem);
                    //var resultItem = new ReportShippingViewModel();
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

                        var resultItem = new ReportShippingViewModel();

                        resultItem.row_Index = item.Row_Index;
                        resultItem.rowNum = num + 1;
                        resultItem.tempCondition = item.TempCondition_Name;
                        resultItem.businessUnit_Name = item.BusinessUnit_Name;
                        resultItem.expect_Delivery_Date = item.Expect_Delivery_Date != null ? item.Expect_Delivery_Date.Value.ToString("dd/MM/yy") : "";
                        resultItem.appointment_Time = item.Appointment_Time;
                        resultItem.truckLoad_No = item.TruckLoad_No; 
                        resultItem.appointment_Id = item.Appointment_Id;
                        resultItem.dock_Name = item.Dock_Name;
                        resultItem.goodsIssue_No = item.GoodsIssue_No;
                        resultItem.goodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No; 
                        resultItem.billing = item.Billing;
                        resultItem.matdoc = item.Matdoc;
                        resultItem.shipTo_Id = item.ShipTo_Id;
                        resultItem.shipTo_Name = item.ShipTo_Name;
                        resultItem.province = item.Province;
                        resultItem.branch = item.Branch;
                        //resultItem.product_Id = item.Product_Id;
                        //resultItem.product_Name = item.Product_Name;
                        //resultItem.productConversion_Name = item.ProductConversion_Name;
                        //resultItem.gI_Qty = item.GI_Qty;
                        //resultItem.ratio = item.Ratio;
                        //resultItem.sU_Conversion = item.SU_Conversion;
                        resultItem.cBM = item.CBM;
                        //resultItem.productConversion_Name_P = item.ProductConversion_Name_P;
                        resultItem.vehicleType_Name = item.VehicleType_Name;
                        resultItem.vehicle_No = item.Vehicle_No;
                        resultItem.vehicleCompany_Name = item.VehicleCompany_Name;
                        resultItem.countitem = item.Countitem;
                        resultItem.palletID = item.PalletID;
                        resultItem.appointment_Date = item.Appointment_Date != null ? item.Appointment_Date.Value.ToString("dd/MM/yyyy") : "";
                        result.Add(resultItem);
                        num++;
                    }
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportShipping");
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

        public string ExportExcel(ReportShippingViewModel data, string rootPath = "")
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
            var result = new List<ReportShippingViewModel>();
            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                //var tempCondition = "";
                var business_Unit = "";
                var shipment = "";
                var planGoodsissue = "";
                var Goodsissue = "";
                var tag_No = "";
                var product_Id = "";
                var branch = "";
                DateTime gi_date = DateTime.Now.toString().toBetweenDate().start;
                DateTime gi_date_To = DateTime.Now.toString().toBetweenDate().end;
                DateTime expect_Delivery_Date = DateTime.Now.toString().toBetweenDate().start;
                DateTime expect_Delivery_Date_To = DateTime.Now.toString().toBetweenDate().end;
                DateTime load_Date = DateTime.Now.toString().toBetweenDate().start;
                DateTime load_Date_To = DateTime.Now.toString().toBetweenDate().end;
                var gi_date_null = "";
                var gi_date_to_null = "";
                var expect_Delivery_Date_null = "";
                var expect_Delivery_Date_To_null = "";
                var load_Date_null = "";
                var load_Date_To_null = "";
                var statusModels = new List<int?>();

                //if (!string.IsNullOrEmpty(data.tempCondition))
                //{
                //    tempCondition = data.tempCondition;
                //}
                if (data.businessUnitList != null)
                {
                    business_Unit = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.shipment))
                {
                    shipment = data.shipment;
                }
                if (!string.IsNullOrEmpty(data.planGoodsIssue_No))
                {
                    planGoodsissue = data.planGoodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    Goodsissue = data.goodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.tag_no))
                {
                    tag_No = data.tag_no;
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                if (!string.IsNullOrEmpty(data.branch))
                {
                    branch = data.branch;
                }

                var gi_d = new SqlParameter();
                var gi_dt = new SqlParameter();
                if (!string.IsNullOrEmpty(data.goodsIssue_Date))
                {
                    gi_date = data.goodsIssue_Date.toBetweenDate().start;
                    gi_date_To = data.goodsIssue_Date_To.toBetweenDate().end;

                    gi_d = new SqlParameter("@GI_Date", gi_date);
                    gi_dt = new SqlParameter("@GI_Date_To", gi_date_To);
                }
                else
                {
                    gi_d = new SqlParameter("@GI_Date", gi_date_null);
                    gi_dt = new SqlParameter("@GI_Date_To", gi_date_to_null);
                }

                var delivery_Date = new SqlParameter();
                var delivery_Date_To = new SqlParameter();
                if (!string.IsNullOrEmpty(data.expect_Delivery_Date) && !string.IsNullOrEmpty(data.expect_Delivery_Date_To))
                {
                    expect_Delivery_Date = data.expect_Delivery_Date.toBetweenDate().start;
                    expect_Delivery_Date_To = data.expect_Delivery_Date_To.toBetweenDate().end;

                    delivery_Date = new SqlParameter("@Expect_Delivery_Date", expect_Delivery_Date);
                    delivery_Date_To = new SqlParameter("@Expect_Delivery_Date_To", expect_Delivery_Date_To);
                }
                else
                {
                    delivery_Date = new SqlParameter("@Expect_Delivery_Date", expect_Delivery_Date_null);
                    delivery_Date_To = new SqlParameter("@Expect_Delivery_Date_To", expect_Delivery_Date_To_null);
                }

                var load_Date_ = new SqlParameter();
                var load_Date_To_ = new SqlParameter();
                if (!string.IsNullOrEmpty(data.appointment_Date) && !string.IsNullOrEmpty(data.appointment_Date_To))
                {
                    load_Date = data.appointment_Date.toBetweenDate().start;
                    load_Date_To = data.appointment_Date_To.toBetweenDate().end;

                    load_Date_ = new SqlParameter("@Load_Date", load_Date);
                    load_Date_To_ = new SqlParameter("@Load_Date_To", load_Date_To);
                }
                else
                {
                    load_Date_ = new SqlParameter("@Load_Date", load_Date_null);
                    load_Date_To_ = new SqlParameter("@Load_Date_To", load_Date_To_null);
                }

                //var temp = new SqlParameter("@TempCondition_Index", tempCondition);
                var bu = new SqlParameter("@BusinessUnit_Index", business_Unit);
                var ship = new SqlParameter("@Shipment_Index", shipment);
                var planGI = new SqlParameter("@PlanGoodsissue_Index", planGoodsissue);
                var giNo = new SqlParameter("@Goodsissue_index", Goodsissue);
                var tag = new SqlParameter("@Tag_No", tag_No);
                var proID = new SqlParameter("@Product_Index", product_Id);
                var bar = new SqlParameter("@Branch", branch);

                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_14_Shipping>();
                if (data.tempCondition != "02")
                {
                    resultquery = Master_DBContext.sp_rpt_14_Shipping.FromSql("sp_rpt_14_Shipping  @BusinessUnit_Index,@Shipment_Index,@PlanGoodsissue_Index,@Goodsissue_index,@Tag_No,@Product_Index,@Branch,@GI_Date,@GI_Date_To,@Expect_Delivery_Date,@Expect_Delivery_Date_To,@Load_Date,@Load_Date_To", bu, ship, planGI, giNo, tag, proID, bar, gi_d, gi_dt, delivery_Date, delivery_Date_To, load_Date_, load_Date_To_).ToList();

                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_14_Shipping.FromSql("sp_rpt_14_Shipping  @BusinessUnit_Index,@Shipment_Index,@PlanGoodsissue_Index,@Goodsissue_index,@Tag_No,@Product_Index,@Branch,@GI_Date,@GI_Date_To,@Expect_Delivery_Date,@Expect_Delivery_Date_To,@Load_Date,@Load_Date_To", bu, ship, planGI, giNo, tag, proID, bar, gi_d, gi_dt, delivery_Date, delivery_Date_To, load_Date_, load_Date_To_).ToList();
                    olog.logging("ReportShipping", resultquery.Count.ToString());

                }


                //var resultquery = Master_DBContext.sp_ReportPick.FromSql("sp_ReportPick @GT,@TAG,@LOC_TO,@PRO_ID,@C_DATE,@C_DATE_TO", gt, tag, loc_name, pro_id, c_date, c_date_to).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportShippingViewModel();
                    //var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    //var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    //resultItem.report_date = startDate;
                    //resultItem.report_date_to = endDate;
                    result.Add(resultItem);
                    //var resultItem = new ReportShippingViewModel();
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

                        var resultItem = new ReportShippingViewModel();

                        resultItem.row_Index = item.Row_Index;
                        resultItem.rowNum = num + 1;
                        resultItem.tempCondition = item.TempCondition_Name;
                        resultItem.businessUnit_Name = item.BusinessUnit_Name;
                        resultItem.expect_Delivery_Date = item.Expect_Delivery_Date != null ? item.Expect_Delivery_Date.Value.ToString("dd/MM/yy") : "";
                        resultItem.appointment_Time = item.Appointment_Time;
                        resultItem.truckLoad_No = item.TruckLoad_No;
                        resultItem.appointment_Id = item.Appointment_Id;
                        resultItem.dock_Name = item.Dock_Name;
                        resultItem.goodsIssue_No = item.GoodsIssue_No;
                        resultItem.goodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No;
                        resultItem.billing = item.Billing;
                        resultItem.matdoc = item.Matdoc;
                        resultItem.shipTo_Id = item.ShipTo_Id;
                        resultItem.shipTo_Name = item.ShipTo_Name;
                        resultItem.province = item.Province;
                        resultItem.branch = item.Branch;
                        //resultItem.product_Id = item.Product_Id;
                        //resultItem.product_Name = item.Product_Name;
                        //resultItem.productConversion_Name = item.ProductConversion_Name;
                        //resultItem.gI_Qty = item.GI_Qty;
                        //resultItem.ratio = item.Ratio;
                        //resultItem.sU_Conversion = item.SU_Conversion;
                        resultItem.cBM = item.CBM;
                        //resultItem.productConversion_Name_P = item.ProductConversion_Name_P;
                        resultItem.vehicleType_Name = item.VehicleType_Name;
                        resultItem.vehicle_No = item.Vehicle_No;
                        resultItem.vehicleCompany_Name = item.VehicleCompany_Name;
                        resultItem.countitem = item.Countitem;
                        resultItem.palletID = item.PalletID;
                        resultItem.appointment_Date = item.Appointment_Date != null ? item.Appointment_Date.Value.ToString("dd/MM/yyyy") : "";
                        result.Add(resultItem);
                        num++;
                    }
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportShipping");

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
                olog.logging("ReportShipping", ex.Message);
                throw ex;
            }

        }

    }
}

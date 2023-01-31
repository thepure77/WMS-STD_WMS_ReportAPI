using AspNetCore.Report.ReportExecutionService;
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

namespace ReportBusiness.ReportCoverDay
{
    public class ReportCoverDayService
    {
        #region printReportCoverDay
        public dynamic printReportCoverDay(ReportCoverDayViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportCoverDayViewModel>();
            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                string datereport = "";
                string product_index = "";
                string businessunit_index = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                

                var day_of_month = "";
                var gi_Date_From = "";
                var gi_Date_To = "";
                var check_month = "ReportCoverDay";

                //if (!string.IsNullOrEmpty(data.month) && data.year != null)
                //{
                //    day_of_month = DateTime.DaysInMonth(data.year, Convert.ToInt32(data.month)).ToString();
                //    gi_Date_From = data.year.ToString() + "-" + data.month + "-" + "01";
                //    gi_Date_To = data.year.ToString() + "-" + data.month + "-" + day_of_month;

                //    string fullMonthName = new DateTime(data.year, Convert.ToInt32(data.month), 1).ToString("MMMM");
                //    datereport = "Monthly  : " + fullMonthName + "  " + data.year;

                //    if (day_of_month == "28")
                //    {
                //        check_month = "ReportCoverDay_28";
                //    }
                //    else if (day_of_month == "29")
                //    {
                //        check_month = "ReportCoverDay_29";
                //    }
                //    else if (day_of_month == "30")
                //    {
                //        check_month = "ReportCoverDay_30";
                //    }
                //}

                if (!string.IsNullOrEmpty(data.gi_date_from) && !string.IsNullOrEmpty(data.gi_date_to))
                {
                    gi_Date_From = data.gi_date_from;
                    gi_Date_To = data.gi_date_to;
                    //var sd_string = data.gi_date_from.toBetweenDate().start.Date.ToString().Substring(0,10);
                    //var ed_string = data.gi_date_to.toBetweenDate().end.Date.ToString().Substring(0, 10);
                    //datereport = "Date From : " + sd_string + " To  " + ed_string;
                    var sd_string = data.gi_date_from.Substring(6, 2) + "/" + data.gi_date_from.Substring(4, 2) + "/" + data.gi_date_from.Substring(0, 4);
                    var ed_string = data.gi_date_to.Substring(6, 2) + "/" + data.gi_date_to.Substring(4, 2) + "/" + data.gi_date_to.Substring(0, 4);
                    datereport = "Date From : " + sd_string + " To  " + ed_string;
                }

                if (!string.IsNullOrEmpty(data.product_Index.ToString()))
                {
                    product_index = data.product_Index.ToString();

                }

                if (data.businessUnitList != null)
                {
                    businessunit_index = data.businessUnitList.BusinessUnit_Index.ToString();
                }


                var statusModels = new List<int?>();
                var query = $@"EXEC sp_rpt_02_CoverDay '{businessunit_index}','{product_index}','{data.product_Lot}','{gi_Date_From}','{gi_Date_To}'";


                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_02_CoverDay>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_rpt_02_CoverDay.FromSql(query).ToList();
                    //resultquery = Master_DBContext.sp_rpt_01_Traceability.FromSql("sp_rpt_01_Traceability @BusinessUnit_Name, @Product_Index, @MFG_Date, @EXP_Date,@GoodsReceive_Date,@GoodsReceive_Date_From,@GoodsReceive_Date_To,@Product_Lot,@Vendor_Index", BusinessUnit_Name, Product_Index, MFG_Date, EXP_Date, GoodsReceive_Date, GoodsReceive_Date_From, GoodsReceive_Date_To, Product_Lot, Vendor_Index).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_02_CoverDay.FromSql(query).ToList();
                    //resultquery = temp_Master_DBContext.sp_rpt_01_Traceability.FromSql("sp_rpt_01_Traceability @BusinessUnit_Name, @Product_Index, @MFG_Date, @EXP_Date,@GoodsReceive_Date,@GoodsReceive_Date_From,@GoodsReceive_Date_To,@Product_Lot,@Vendor_Index", BusinessUnit_Name, Product_Index, MFG_Date, EXP_Date, GoodsReceive_Date, GoodsReceive_Date_From, GoodsReceive_Date_To, Product_Lot, Vendor_Index).ToList();
                }

                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportCoverDayViewModel();
                    var startDate = DateTime.ParseExact(data.gi_date_from.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.gi_date_to.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.report_date = datereport;
                    resultItem.report_date_to = endDate;
                    //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    result.Add(resultItem);
                }
                else
                {
                    int num = 0;
                    foreach (var item in resultquery)
                    {
                        var startDate = DateTime.ParseExact(data.gi_date_from.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.gi_date_to.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new ReportCoverDayViewModel();
                        resultItem.rowNum = num + 1;
                        if (data.ambientRoom != "02")
                        {
                            resultItem.ambientRoom = "Ambient";
                        }
                        else
                        {
                            resultItem.ambientRoom = "Freeze";
                        }
                        resultItem.business_unit = item.BusinessUnit_Name;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.ratio = item.Ratio;
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.stockOnHand = item.StockOnHand;
                        resultItem.cover_Qty = item.Cover_Qty;
                        resultItem.gi_AVG_Qty = item.GI_AVG_Qty;
                        resultItem.balance_AVG = item.Balance_AVG;
                        resultItem.sumCalDay = item.SumCalDay;
                        resultItem.whgi = item.WHGI;
                        resultItem.uom = item.UOM;
                        resultItem.d01 = item.D01;
                        resultItem.d02 = item.D02;
                        resultItem.d03 = item.D03;
                        resultItem.d04 = item.D04;
                        resultItem.d05 = item.D05;
                        resultItem.d06 = item.D06;
                        resultItem.d07 = item.D07;
                        resultItem.d08 = item.D08;
                        resultItem.d09 = item.D09;
                        resultItem.d10 = item.D10;
                        resultItem.d11 = item.D11;
                        resultItem.d12 = item.D12;
                        resultItem.d13 = item.D13;
                        resultItem.d14 = item.D14;
                        resultItem.d15 = item.D15;
                        resultItem.d16 = item.D16;
                        resultItem.d17 = item.D17;
                        resultItem.d18 = item.D18;
                        resultItem.d19 = item.D19;
                        resultItem.d20 = item.D20;
                        resultItem.d21 = item.D21;
                        resultItem.d22 = item.D22;
                        resultItem.d23 = item.D23;
                        resultItem.d24 = item.D24;
                        resultItem.d25 = item.D25;
                        resultItem.d26 = item.D26;
                        resultItem.d27 = item.D27;
                        resultItem.d28 = item.D28;
                        resultItem.d29 = item.D29;
                        resultItem.d30 = item.D30;
                        resultItem.d31 = item.D31;
                        resultItem.report_date = datereport;

                        result.Add(resultItem);
                        num++;
                    }
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl(check_month);

                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

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
               // olog.logging("ReportGIByShipmentDateAndBusinessUnit", ex.Message);
                throw ex;
            }
        }
        #endregion

        public string ExportExcel(ReportCoverDayViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportCoverDayViewModel>();
            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                string datereport = "";
                string product_index = "";
                string businessunit_index = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var day_of_month = "";
                var gi_Date_From = "";
                var gi_Date_To = "";
                var check_month = "ReportCoverDay";

                //if (!string.IsNullOrEmpty(data.month) && data.year != null)
                //{
                //    day_of_month = DateTime.DaysInMonth(data.year, Convert.ToInt32(data.month)).ToString();
                //    gi_Date_From = data.year.ToString() + "-" + data.month + "-" + "01";
                //    gi_Date_To = data.year.ToString() + "-" + data.month + "-" + day_of_month;

                //    string fullMonthName = new DateTime(data.year, Convert.ToInt32(data.month), 1).ToString("MMMM");
                //    datereport = "Monthly  : " + fullMonthName + "  " + data.year;

                //    if (day_of_month == "28")
                //    {
                //        check_month = "ReportCoverDay_28";
                //    }
                //    else if (day_of_month == "29")
                //    {
                //        check_month = "ReportCoverDay_29";
                //    }
                //    else if (day_of_month == "30")
                //    {
                //        check_month = "ReportCoverDay_30";
                //    }
                //}

                if (!string.IsNullOrEmpty(data.gi_date_from) && !string.IsNullOrEmpty(data.gi_date_to))
                {
                    gi_Date_From = data.gi_date_from;
                    gi_Date_To = data.gi_date_to;

                    //var sd_string = data.gi_date_from.toBetweenDate().start.Date.ToString().Substring(0, 10);
                    //var ed_string = data.gi_date_to.toBetweenDate().end.Date.ToString().Substring(0, 10);
                    //datereport = "Date From : " + sd_string + " To  " + ed_string;

                    var sd_string = data.gi_date_from.Substring(6, 2) + "/" + data.gi_date_from.Substring(4, 2) + "/" + data.gi_date_from.Substring(0, 4);
                    var ed_string = data.gi_date_to.Substring(6, 2) + "/" + data.gi_date_to.Substring(4, 2) + "/" + data.gi_date_to.Substring(0, 4);
                    datereport = "Date From : " + sd_string + " To  " + ed_string;
                }

                if (!string.IsNullOrEmpty(data.product_Index.ToString()))
                {
                    product_index = data.product_Index.ToString();

                }

                if (data.businessUnitList != null)
                {
                    businessunit_index = data.businessUnitList.BusinessUnit_Index.ToString();
                }


                var statusModels = new List<int?>();
                var query = $@"EXEC sp_rpt_02_CoverDay '{businessunit_index}','{product_index}','{data.product_Lot}','{gi_Date_From}','{gi_Date_To}'";


                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_02_CoverDay>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_rpt_02_CoverDay.FromSql(query).ToList();
                    //resultquery = Master_DBContext.sp_rpt_01_Traceability.FromSql("sp_rpt_01_Traceability @BusinessUnit_Name, @Product_Index, @MFG_Date, @EXP_Date,@GoodsReceive_Date,@GoodsReceive_Date_From,@GoodsReceive_Date_To,@Product_Lot,@Vendor_Index", BusinessUnit_Name, Product_Index, MFG_Date, EXP_Date, GoodsReceive_Date, GoodsReceive_Date_From, GoodsReceive_Date_To, Product_Lot, Vendor_Index).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_02_CoverDay.FromSql(query).ToList();
                    //resultquery = temp_Master_DBContext.sp_rpt_01_Traceability.FromSql("sp_rpt_01_Traceability @BusinessUnit_Name, @Product_Index, @MFG_Date, @EXP_Date,@GoodsReceive_Date,@GoodsReceive_Date_From,@GoodsReceive_Date_To,@Product_Lot,@Vendor_Index", BusinessUnit_Name, Product_Index, MFG_Date, EXP_Date, GoodsReceive_Date, GoodsReceive_Date_From, GoodsReceive_Date_To, Product_Lot, Vendor_Index).ToList();
                }

                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportCoverDayViewModel();
                    var startDate = DateTime.ParseExact(data.gi_date_from.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.gi_date_to.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.report_date = datereport;
                    resultItem.report_date_to = endDate;
                    //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    result.Add(resultItem);
                }
                else
                {
                    int num = 0;
                    foreach (var item in resultquery)
                    {
                        var startDate = DateTime.ParseExact(data.gi_date_from.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.gi_date_to.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new ReportCoverDayViewModel();
                        resultItem.rowNum = num + 1;
                        if (data.ambientRoom != "02")
                        {
                            resultItem.ambientRoom = "Ambient";
                        }
                        else
                        {
                            resultItem.ambientRoom = "Freeze";
                        }
                        resultItem.business_unit = item.BusinessUnit_Name;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.ratio = item.Ratio;
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.stockOnHand = item.StockOnHand;
                        resultItem.cover_Qty = item.Cover_Qty;
                        resultItem.gi_AVG_Qty = item.GI_AVG_Qty;
                        resultItem.balance_AVG = item.Balance_AVG;
                        resultItem.sumCalDay = item.SumCalDay;
                        resultItem.whgi = item.WHGI;
                        resultItem.uom = item.UOM;
                        resultItem.d01 = item.D01;
                        resultItem.d02 = item.D02;
                        resultItem.d03 = item.D03;
                        resultItem.d04 = item.D04;
                        resultItem.d05 = item.D05;
                        resultItem.d06 = item.D06;
                        resultItem.d07 = item.D07;
                        resultItem.d08 = item.D08;
                        resultItem.d09 = item.D09;
                        resultItem.d10 = item.D10;
                        resultItem.d11 = item.D11;
                        resultItem.d12 = item.D12;
                        resultItem.d13 = item.D13;
                        resultItem.d14 = item.D14;
                        resultItem.d15 = item.D15;
                        resultItem.d16 = item.D16;
                        resultItem.d17 = item.D17;
                        resultItem.d18 = item.D18;
                        resultItem.d19 = item.D19;
                        resultItem.d20 = item.D20;
                        resultItem.d21 = item.D21;
                        resultItem.d22 = item.D22;
                        resultItem.d23 = item.D23;
                        resultItem.d24 = item.D24;
                        resultItem.d25 = item.D25;
                        resultItem.d26 = item.D26;
                        resultItem.d27 = item.D27;
                        resultItem.d28 = item.D28;
                        resultItem.d29 = item.D29;
                        resultItem.d30 = item.D30;
                        resultItem.d31 = item.D31;
                        resultItem.report_date = datereport;

                        result.Add(resultItem);
                        num++;
                    }
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl(check_month);

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
                //olog.logging("ReportGIByShipmentDateAndBusinessUnit", ex.Message);
                throw ex;
            }

        }
    }
}

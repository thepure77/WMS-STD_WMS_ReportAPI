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

namespace ReportBusiness.ReportTraceability
{
    public class ReportTraceabilityService
    {
        #region printReportTraceability
        public dynamic printReportTraceability(ReportTraceabilityViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportTraceabilityViewModel>();           
            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);

                string product_index = "";
                string businessunit_index = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                var year = DateTime.Now.Year;
                var day_of_month = "";
                var goodsReceive_Date_From = "";
                var goodsReceive_Date_To = "";
                var vendor_index = "";
                

                if (!string.IsNullOrEmpty(data.month))
                {
                    day_of_month = DateTime.DaysInMonth(year, Convert.ToInt32(data.month)).ToString();
                    goodsReceive_Date_From = year.ToString() + "-" + data.month + "-" + "01";
                    goodsReceive_Date_To = year.ToString() + "-" + data.month + "-" + day_of_month;
                }


                if (!string.IsNullOrEmpty(data.mfg_Date))
                {
                    data.mfg_Date = data.mfg_Date.datetoString();
                }
               

                if (!string.IsNullOrEmpty(data.exp_Date))
                {
                    data.exp_Date = data.exp_Date.datetoString();
                }
               

                if (!string.IsNullOrEmpty(data.goodsReceive_Date))
                {
                    data.goodsReceive_Date = data.goodsReceive_Date.datetoString();
                }
                

                if (!string.IsNullOrEmpty(data.product_Index.ToString()))
                {
                    product_index =   data.product_Index.ToString();

                }

                if (data.businessUnitList != null)
                {
                    businessunit_index = data.businessUnitList.BusinessUnit_Index.ToString();
                }

                if (!string.IsNullOrEmpty(data.vendor_Index.ToString()))
                {
                    vendor_index = data.vendor_Index.ToString();

                }

                //if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                //{
                //    dateStart = data.report_date.toBetweenDate().start;
                //    dateEnd = data.report_date_to.toBetweenDate().end;
                //}

                var statusModels = new List<int?>();
                var query = $@"EXEC sp_rpt_01_Traceability '{businessunit_index}','{product_index}','{data.mfg_Date}','{data.exp_Date}','{data.goodsReceive_Date}',
                                                           '{goodsReceive_Date_From}','{goodsReceive_Date_To}','{data.product_Lot}','{vendor_index}'";
                               

                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_01_Traceability>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_rpt_01_Traceability.FromSql(query).ToList();
                    //resultquery = Master_DBContext.sp_rpt_01_Traceability.FromSql("sp_rpt_01_Traceability @BusinessUnit_Name, @Product_Index, @MFG_Date, @EXP_Date,@GoodsReceive_Date,@GoodsReceive_Date_From,@GoodsReceive_Date_To,@Product_Lot,@Vendor_Index", BusinessUnit_Name, Product_Index, MFG_Date, EXP_Date, GoodsReceive_Date, GoodsReceive_Date_From, GoodsReceive_Date_To, Product_Lot, Vendor_Index).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_01_Traceability.FromSql(query).ToList();
                    //resultquery = temp_Master_DBContext.sp_rpt_01_Traceability.FromSql("sp_rpt_01_Traceability @BusinessUnit_Name, @Product_Index, @MFG_Date, @EXP_Date,@GoodsReceive_Date,@GoodsReceive_Date_From,@GoodsReceive_Date_To,@Product_Lot,@Vendor_Index", BusinessUnit_Name, Product_Index, MFG_Date, EXP_Date, GoodsReceive_Date, GoodsReceive_Date_From, GoodsReceive_Date_To, Product_Lot, Vendor_Index).ToList();
                }

                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportTraceabilityViewModel();
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

                        var resultItem = new ReportTraceabilityViewModel();
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
                        resultItem.vendor_Name = item.Vendor_Name;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.po_Qty = Convert.ToInt32(item.PO_Qty);
                        resultItem.inbound_SumQty = Convert.ToInt32(item.Inbound_SumQty);
                        resultItem.percent_GR = item.Percent_GR;
                        resultItem.percent_GI = item.Percent_GI;
                        resultItem.outbound_SumQty = Convert.ToInt32(item.Outbound_SumQty);
                        resultItem.exp_Date = item.EXP_Date != null ? item.EXP_Date.Value.ToString("yyyy-MM-dd") : "";
                        resultItem.mfg_Date = item.MFG_Date != null ? item.MFG_Date.Value.ToString("yyyy-MM-dd") : "";
                        resultItem.goodsReceive_Date = item.GoodsReceive_Date != null ? item.GoodsReceive_Date.Value.ToString("yyyy-MM-dd") : "";
                        resultItem.goodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("yyyy-MM-dd") : "";
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.shipTo_Id = item.ShipTo_Id;
                        resultItem.branch_Code = item.Branch_Code;
                        resultItem.sale_Unit = item.Sale_Unit;                       
                        result.Add(resultItem);
                        num++;
                    }
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportTraceability");
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
               // olog.logging("ReportGIByShipmentDateAndBusinessUnit", ex.Message);
                throw ex;
            }
        }
        #endregion

        public string ExportExcel(ReportTraceabilityViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportTraceabilityViewModel>();
            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);

                string product_index = "";
                string businessunit_index = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                var year = DateTime.Now.Year;
                var day_of_month = "";
                var goodsReceive_Date_From = "";
                var goodsReceive_Date_To = "";
                var vendor_index = "";


                if (!string.IsNullOrEmpty(data.month))
                {
                    day_of_month = DateTime.DaysInMonth(year, Convert.ToInt32(data.month)).ToString();
                    goodsReceive_Date_From = year.ToString() + "-" + data.month + "-" + "01";
                    goodsReceive_Date_To = year.ToString() + "-" + data.month + "-" + day_of_month;
                }


                if (!string.IsNullOrEmpty(data.mfg_Date))
                {
                    data.mfg_Date = data.mfg_Date.datetoString();
                }


                if (!string.IsNullOrEmpty(data.exp_Date))
                {
                    data.exp_Date = data.exp_Date.datetoString();
                }


                if (!string.IsNullOrEmpty(data.goodsReceive_Date))
                {
                    data.goodsReceive_Date = data.goodsReceive_Date.datetoString();
                }


                if (!string.IsNullOrEmpty(data.product_Index.ToString()))
                {
                    product_index = data.product_Index.ToString();

                }

                if (data.businessUnitList != null)
                {
                    businessunit_index = data.businessUnitList.BusinessUnit_Index.ToString();
                }

                if (!string.IsNullOrEmpty(data.vendor_Index.ToString()))
                {
                    vendor_index = data.vendor_Index.ToString();

                }

                //if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                //{
                //    dateStart = data.report_date.toBetweenDate().start;
                //    dateEnd = data.report_date_to.toBetweenDate().end;
                //}

                var statusModels = new List<int?>();
                var query = $@"EXEC sp_rpt_01_Traceability '{businessunit_index}','{product_index}','{data.mfg_Date}','{data.exp_Date}','{data.goodsReceive_Date}',
                                                           '{goodsReceive_Date_From}','{goodsReceive_Date_To}','{data.product_Lot}','{vendor_index}'";


                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_01_Traceability>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_rpt_01_Traceability.FromSql(query).ToList();
                    //resultquery = Master_DBContext.sp_rpt_01_Traceability.FromSql("sp_rpt_01_Traceability @BusinessUnit_Name, @Product_Index, @MFG_Date, @EXP_Date,@GoodsReceive_Date,@GoodsReceive_Date_From,@GoodsReceive_Date_To,@Product_Lot,@Vendor_Index", BusinessUnit_Name, Product_Index, MFG_Date, EXP_Date, GoodsReceive_Date, GoodsReceive_Date_From, GoodsReceive_Date_To, Product_Lot, Vendor_Index).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_01_Traceability.FromSql(query).ToList();
                    //resultquery = temp_Master_DBContext.sp_rpt_01_Traceability.FromSql("sp_rpt_01_Traceability @BusinessUnit_Name, @Product_Index, @MFG_Date, @EXP_Date,@GoodsReceive_Date,@GoodsReceive_Date_From,@GoodsReceive_Date_To,@Product_Lot,@Vendor_Index", BusinessUnit_Name, Product_Index, MFG_Date, EXP_Date, GoodsReceive_Date, GoodsReceive_Date_From, GoodsReceive_Date_To, Product_Lot, Vendor_Index).ToList();
                }

                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportTraceabilityViewModel();
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

                        var resultItem = new ReportTraceabilityViewModel();
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
                        resultItem.vendor_Name = item.Vendor_Name;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.po_Qty = Convert.ToInt32(item.PO_Qty);
                        resultItem.inbound_SumQty = Convert.ToInt32(item.Inbound_SumQty);
                        resultItem.percent_GR = item.Percent_GR;
                        resultItem.percent_GI = item.Percent_GI;
                        resultItem.outbound_SumQty = Convert.ToInt32(item.Outbound_SumQty);
                        resultItem.exp_Date = item.EXP_Date != null ? item.EXP_Date.Value.ToString("yyyy-MM-dd") : "";
                        resultItem.mfg_Date = item.MFG_Date != null ? item.MFG_Date.Value.ToString("yyyy-MM-dd") : "";
                        resultItem.goodsReceive_Date = item.GoodsReceive_Date != null ? item.GoodsReceive_Date.Value.ToString("yyyy-MM-dd") : "";
                        resultItem.goodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("yyyy-MM-dd") : "";
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.shipTo_Id = item.ShipTo_Id;
                        resultItem.branch_Code = item.Branch_Code;
                        resultItem.sale_Unit = item.Sale_Unit;
                        result.Add(resultItem);
                        num++;
                    }
                }


                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportTraceability");

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

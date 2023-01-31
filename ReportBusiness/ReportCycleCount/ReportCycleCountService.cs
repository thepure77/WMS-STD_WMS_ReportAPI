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

namespace ReportBusiness.ReportCycleCount
{
    public class ReportCycleCountService
    {
        #region printReport
        public dynamic printReportCycleCount(ReportCycleCountViewModel data, string rootPath = "")
        {
            var BinbalanceDbContext = new BinbalanceDbContext();
            var temp_BinbalanceDbContext = new temp_BinbalanceDbContext();
            var culture = new System.Globalization.CultureInfo("th-TH");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportCycleCountViewModel>();

            try
            {
                olog.logging("log_ReportSummaryCycleCount", "Start");
                BinbalanceDbContext.Database.SetCommandTimeout(360);
                olog.logging("log_ReportSummaryCycleCount", "1");
                //temp_BinbalanceDbContext.Database.SetCommandTimeout(360);
                olog.logging("log_ReportSummaryCycleCount", "2");
                var tag_No = "";
                var owner_Name = "";
                var product_Id = "";
                var itemStatus_Name = "";
                var CycleCount_No = "";
                var Status_Diff_Count = "";
                var business_Unit = "";
                var location_Type = "";
                olog.logging("log_ReportSummaryCycleCount", "3");
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                olog.logging("log_ReportSummaryCycleCount", "4");
                var statusModels = new List<int?>();
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    tag_No = data.tag_No;
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                if (!string.IsNullOrEmpty(data.owner_Name))
                {
                    owner_Name = data.owner_Name;
                }
                if (!string.IsNullOrEmpty(data.itemStatus_Name))
                {
                    itemStatus_Name = data.itemStatus_Name;
                }
                if (!string.IsNullOrEmpty(data.cycleCount_No))
                {
                    CycleCount_No = data.cycleCount_No;
                }
                if (!string.IsNullOrEmpty(data.status_Diff_Count))
                {
                    Status_Diff_Count = data.status_Diff_Count;
                }
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }
                if(data.businessUnitList != null)
                {
                    business_Unit = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.locationType_Name))
                {
                    location_Type = data.locationType_Name;
                }
                olog.logging("log_ReportSummaryCycleCount", "5");
                var tag_no = new SqlParameter("@Tag_No", tag_No);
                var pro_id = new SqlParameter("@Product_Id", product_Id);
                var owner = new SqlParameter("@Owner_Name", owner_Name);
                var itemstatus = new SqlParameter("@ItemStatus_Name", itemStatus_Name);
                var cycle_no = new SqlParameter("@CycleCount_No", CycleCount_No);
                var status_diff = new SqlParameter("@Status_Diff_Count", Status_Diff_Count);
                var c_date = new SqlParameter("@CycleCount_Date", dateStart);
                var c_date_to = new SqlParameter("@CycleCount_Date_To", dateEnd);
                var business = new SqlParameter("@BusinessUnit_Name", business_Unit);
                var loc_Type = new SqlParameter("@LocationType_Name", location_Type);
                var resultquery = new List<BinbalanceDataAccess.Models.sp_CycleCountSummary>();
                olog.logging("log_ReportSummaryCycleCount", "resultquery befor");
                if (data.ambientRoom != "02")
                {
                    resultquery = BinbalanceDbContext.sp_CycleCountSummary.FromSql("sp_CycleCountSummary @Tag_No,@Product_Id,@Owner_Name,@ItemStatus_Name,@CycleCount_No,@Status_Diff_Count,@CycleCount_Date,@CycleCount_Date_To,@BusinessUnit_Name,@LocationType_Name", tag_no, pro_id, owner, itemstatus, cycle_no, status_diff, c_date, c_date_to, business, loc_Type).ToList();
                }
                else
                {
                    resultquery = temp_BinbalanceDbContext.sp_CycleCountSummary.FromSql("sp_CycleCountSummary @Tag_No,@Product_Id,@Owner_Name,@ItemStatus_Name,@CycleCount_No,@Status_Diff_Count,@CycleCount_Date,@CycleCount_Date_To,@BusinessUnit_Name,@LocationType_Name", tag_no, pro_id, owner, itemstatus, cycle_no, status_diff, c_date, c_date_to, business, loc_Type).ToList();
                }
                olog.logging("log_ReportSummaryCycleCount", "resultquery after" + resultquery.ToString());

                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportCycleCountViewModel();
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

                        var resultItem = new ReportCycleCountViewModel();
                        resultItem.rowNum = num + 1;
                        resultItem.cycleCount_No = item.CycleCount_No;
                        resultItem.locationType_Name = item.LocationType_Name;
                        resultItem.location_Name = item.Location_Name;
                        resultItem.tag_No = item.Tag_No;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.owner_Name = item.Owner_Name;
                        resultItem.eRP_Location = item.ERP_Location;
                        resultItem.location_Name = item.Location_Name;
                        resultItem.itemStatus_Index = item.ItemStatus_Index;
                        resultItem.itemStatus_Id = item.ItemStatus_Id;
                        resultItem.itemStatus_Name = item.ItemStatus_Name;
                        resultItem.binBalance_QtyBal = item.BinBalance_QtyBal;
                        resultItem.goodsReceive_ProductConversion_Index = item.GoodsReceive_ProductConversion_Index;
                        resultItem.goodsReceive_ProductConversion_Id = item.GoodsReceive_ProductConversion_Id;
                        resultItem.goodsReceive_ProductConversion_Name = item.GoodsReceive_ProductConversion_Name;
                        resultItem.sALE_ProductConversion_Name = item.SALE_ProductConversion_Name;
                        resultItem.sale_Unit = item.Sale_Unit;
                        resultItem.eRP_Location = item.ERP_Location;
                        resultItem.sALE_ProductConversion_Ratio = item.SALE_ProductConversion_Ratio;
                        resultItem.qty_Diff = item.Qty_Diff;
                        resultItem.qty_Count = item.Qty_Count;
                        resultItem.status_Diff_Count = item.Status_Diff_Count;
                        resultItem.count_Date = item.Count_Date != null ? item.Count_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.count_by = item.Count_by;
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.goodsReceive_MFG_Date = item.GoodsReceive_MFG_Date != null ? item.GoodsReceive_MFG_Date.Value.ToString("dd MMM yy", culture) : "";
                        resultItem.goodsReceive_EXP_Date = item.GoodsReceive_EXP_Date != null ? item.GoodsReceive_EXP_Date.Value.ToString("dd MMM yy", culture) : "";
                        resultItem.goodsReceive_No = item.GoodsReceive_No;
                        resultItem.goodsReceive_Date = item.GoodsReceive_Date != null ? item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.businessUnit_Name = item.BusinessUnit_Name;



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
                olog.logging("log_ReportSummaryCycleCount", "befor" + result.ToString());
                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportCycleCount");
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
                olog.logging("log_ReportSummaryCycleCount", ex.Message);
                throw ex;
            }
        }
        #endregion

        public string ExportExcel(ReportCycleCountViewModel data, string rootPath = "")
        {
            var BinbalanceDbContext = new BinbalanceDbContext();
            var temp_BinbalanceDbContext = new temp_BinbalanceDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportCycleCountViewModel>();
            try
            {
                BinbalanceDbContext.Database.SetCommandTimeout(360);
                //temp_BinbalanceDbContext.Database.SetCommandTimeout(360);
                var tag_No = "";
                var owner_Name = "";
                var product_Id = "";
                var itemStatus_Name = "";
                var CycleCount_No = "";
                var Status_Diff_Count = "";
                var business_Unit = "";
                var location_Type = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                var statusModels = new List<int?>();
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    tag_No = data.tag_No;
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                if (!string.IsNullOrEmpty(data.owner_Name))
                {
                    owner_Name = data.owner_Name;
                }
                if (!string.IsNullOrEmpty(data.itemStatus_Name))
                {
                    itemStatus_Name = data.itemStatus_Name;
                }
                if (!string.IsNullOrEmpty(data.cycleCount_No))
                {
                    CycleCount_No = data.cycleCount_No;
                }
                if (!string.IsNullOrEmpty(data.status_Diff_Count))
                {
                    Status_Diff_Count = data.status_Diff_Count;
                }
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }
                if (data.businessUnitList != null)
                {
                    business_Unit = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.locationType_Name))
                {
                    location_Type = data.locationType_Name;
                }

                var tag_no = new SqlParameter("@Tag_No", tag_No);
                var pro_id = new SqlParameter("@Product_Id", product_Id);
                var owner = new SqlParameter("@Owner_Name", owner_Name);
                var itemstatus = new SqlParameter("@ItemStatus_Name", itemStatus_Name);
                var cycle_no = new SqlParameter("@CycleCount_No", CycleCount_No);
                var status_diff = new SqlParameter("@Status_Diff_Count", Status_Diff_Count);
                var c_date = new SqlParameter("@CycleCount_Date", dateStart);
                var c_date_to = new SqlParameter("@CycleCount_Date_To", dateEnd);
                var business = new SqlParameter("@BusinessUnit_Name", business_Unit);
                var loc_Type = new SqlParameter("@LocationType_Name", location_Type);
                var resultquery = new List<BinbalanceDataAccess.Models.sp_CycleCountSummary>();
                if (data.ambientRoom != "02")
                {
                    resultquery = BinbalanceDbContext.sp_CycleCountSummary.FromSql("sp_CycleCountSummary @Tag_No,@Product_Id,@Owner_Name,@ItemStatus_Name,@CycleCount_No,@Status_Diff_Count,@CycleCount_Date,@CycleCount_Date_To,@BusinessUnit_Name,@LocationType_Name", tag_no, pro_id, owner, itemstatus, cycle_no, status_diff, c_date, c_date_to, business, loc_Type).ToList();
                }
                else
                {
                    resultquery = temp_BinbalanceDbContext.sp_CycleCountSummary.FromSql("sp_CycleCountSummary @Tag_No,@Product_Id,@Owner_Name,@ItemStatus_Name,@CycleCount_No,@Status_Diff_Count,@CycleCount_Date,@CycleCount_Date_To,@BusinessUnit_Name,@LocationType_Name", tag_no, pro_id, owner, itemstatus, cycle_no, status_diff, c_date, c_date_to, business, loc_Type).ToList();
                }

                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportCycleCountViewModel();
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

                        var resultItem = new ReportCycleCountViewModel();
                        resultItem.rowNum = num + 1;
                        resultItem.cycleCount_No = item.CycleCount_No;
                        resultItem.locationType_Name = item.LocationType_Name;
                        resultItem.location_Name = item.Location_Name;
                        resultItem.tag_No = item.Tag_No;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.owner_Name = item.Owner_Name;
                        resultItem.eRP_Location = item.ERP_Location;
                        resultItem.location_Name = item.Location_Name;
                        resultItem.itemStatus_Index = item.ItemStatus_Index;
                        resultItem.itemStatus_Id = item.ItemStatus_Id;
                        resultItem.itemStatus_Name = item.ItemStatus_Name;
                        resultItem.binBalance_QtyBal = item.BinBalance_QtyBal;
                        resultItem.goodsReceive_ProductConversion_Index = item.GoodsReceive_ProductConversion_Index;
                        resultItem.goodsReceive_ProductConversion_Id = item.GoodsReceive_ProductConversion_Id;
                        resultItem.goodsReceive_ProductConversion_Name = item.GoodsReceive_ProductConversion_Name;
                        resultItem.sALE_ProductConversion_Name = item.SALE_ProductConversion_Name;
                        resultItem.sale_Unit = item.Sale_Unit;
                        resultItem.eRP_Location = item.ERP_Location;
                        resultItem.sALE_ProductConversion_Ratio = item.SALE_ProductConversion_Ratio;
                        resultItem.qty_Diff = item.Qty_Diff;
                        resultItem.qty_Count = item.Qty_Count;
                        resultItem.status_Diff_Count = item.Status_Diff_Count;
                        resultItem.count_Date = item.Count_Date != null ? item.Count_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.count_by = item.Count_by;
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.goodsReceive_MFG_Date = item.GoodsReceive_MFG_Date != null ? item.GoodsReceive_MFG_Date.Value.ToString("dd MMM yy", culture) : "";
                        resultItem.goodsReceive_EXP_Date = item.GoodsReceive_EXP_Date != null ? item.GoodsReceive_EXP_Date.Value.ToString("dd MMM yy", culture) : "";
                        resultItem.goodsReceive_No = item.GoodsReceive_No;
                        resultItem.goodsReceive_Date = item.GoodsReceive_Date != null ? item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.businessUnit_Name = item.BusinessUnit_Name;

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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportCycleCount");

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
                olog.logging("log_ReportSummaryCycleCountExcel", ex.Message);
                throw ex;
            }

        }

    }
}

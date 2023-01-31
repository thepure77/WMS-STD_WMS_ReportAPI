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

namespace ReportBusiness.ReportPickingPerformance
{
    public class ReportPickingPerformanceService
    {

        #region printReportPickingPerformance
        public dynamic printReportPickingPerformance(ReportPickingPerformanceViewModel data, string rootPath = "")
        {
            var db = new MasterDataDbContext();
            var db_temp = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            olog.logging("ReportSorting", "Start");
            var result = new List<ReportPickingPerformanceViewModel>();
            try
            {
                db.Database.SetCommandTimeout(360);
                db_temp.Database.SetCommandTimeout(360);
                var BusinessUnit_Index = "";
                var GoodsIssue_No = "";
                var TruckLoad_Index = "";
                var WaveRound = "";
                var GI_Date_From = "";
                var GI_Date_To = "";

                if (data.businessUnitList != null)
                {
                    BusinessUnit_Index = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.GoodsIssue_No))
                {
                    GoodsIssue_No = data.GoodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.TruckLoad_No))
                {
                    var dbGI = new GIDbContext();
                    var queryTruckLoad_Index = dbGI.im_TruckLoad.Where(C => C.TruckLoad_No == data.TruckLoad_No).FirstOrDefault();
                    TruckLoad_Index = queryTruckLoad_Index.TruckLoad_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.WaveRound))
                {
                    WaveRound = data.WaveRound;
                }
                if (!string.IsNullOrEmpty(data.GoodsIssue_Date) && !string.IsNullOrEmpty(data.GoodsIssue_Date_To))
                {
                    GI_Date_From = data.GoodsIssue_Date;
                    GI_Date_To = data.GoodsIssue_Date_To;
                }

                var businessUnit_Index = new SqlParameter("@BusinessUnit_Index", BusinessUnit_Index);
                var goodsIssue_No = new SqlParameter("@GoodsIssue_No", GoodsIssue_No);
                var truckLoad_Index = new SqlParameter("@TruckLoad_Index", TruckLoad_Index);
                var waveRound = new SqlParameter("@WaveRound", WaveRound);
                var gI_Date_From = new SqlParameter("@GI_Date_From", GI_Date_From);
                var gI_Date_To = new SqlParameter("@GI_Date_To", GI_Date_To);
                olog.logging("ReportSorting", BusinessUnit_Index);
                olog.logging("ReportSorting", GoodsIssue_No);
                olog.logging("ReportSorting", TruckLoad_Index);
                olog.logging("ReportSorting", WaveRound);
                olog.logging("ReportSorting", GI_Date_From);
                olog.logging("ReportSorting", GI_Date_To);
                olog.logging("ReportSorting", "Befor FromSql");
                var query = new List<MasterDataDataAccess.Models.sp_rpt_12_Picking_Performance>();

                var ambientRoom_name = "";
                if (data.ambientRoom != "02")
                {
                    olog.logging("ReportSorting", "Step 1");
                    query = db.sp_rpt_12_Picking_Performance.FromSql("sp_rpt_12_Picking_Performance @BusinessUnit_Index, @GoodsIssue_No, @TruckLoad_Index, @WaveRound, @GI_Date_From, @GI_Date_To",
                        businessUnit_Index, goodsIssue_No, truckLoad_Index, waveRound, gI_Date_From, gI_Date_To).ToList();
                    olog.logging("ReportSorting", "Step 2");
                    ambientRoom_name = "Ambient";
                }
                else
                {
                    olog.logging("ReportSorting", "Step 1");
                    query = db_temp.sp_rpt_12_Picking_Performance.FromSql("sp_rpt_12_Picking_Performance @BusinessUnit_Index, @GoodsIssue_No, @TruckLoad_Index, @WaveRound, @GI_Date_From, @GI_Date_To",
                        businessUnit_Index, goodsIssue_No, truckLoad_Index, waveRound, gI_Date_From, gI_Date_To).ToList();
                    olog.logging("ReportSorting", "Step 2");
                    ambientRoom_name = "Freeze";
                }
                olog.logging("ReportSorting", "After FromSql"+ query);

                int num = 0;
                foreach (var item in query)
                {
                    var resultItem = new ReportPickingPerformanceViewModel();

                    num = num + 1;
                    resultItem.Row_No = num;
                    resultItem.ambientRoom_name = ambientRoom_name;
                    resultItem.BusinessUnit_Name = item.BusinessUnit_Name;
                    resultItem.Warehouse_Type = item.Warehouse_Type;
                    resultItem.GoodsIssue_Index = item.GoodsIssue_Index;
                    resultItem.GoodsIssue_No = item.GoodsIssue_No;
                    resultItem.GoodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.CountTruckLoad_GI = item.CountTruckLoad_GI;
                    resultItem.TruckLoad_No = item.TruckLoad_No;
                    resultItem.TruckLoad_Date = item.TruckLoad_Date;
                    resultItem.CBM = item.CBM;
                    resultItem.Sum_CBM = item.Sum_CBM;
                    resultItem.Dock_Name = item.Dock_Name;
                    resultItem.CountAllRollcage_GI = item.CountAllRollcage_GI;
                    resultItem.TruckLoadRollcage_Use = item.TruckLoadRollcage_Use;
                    resultItem.Chute_No = item.Chute_No;
                    resultItem.WaveRound = item.WaveRound;
                    resultItem.WaveStart_Date = item.WaveStart_Date;
                    resultItem.WaveComplete_Date = item.WaveComplete_Date;
                    resultItem.Tag_ASRS = item.Tag_ASRS;
                    resultItem.Tag_LBL = item.Tag_LBL;
                    resultItem.Tag_CFR_XL = item.Tag_CFR_XL;
                    resultItem.Tag_CFR_M = item.Tag_CFR_M;
                    resultItem.Total_Tag = item.Total_Tag;
                    resultItem.CountTagOut_GI = item.CountTagOut_GI;
                    resultItem.LastCartonScanIn = item.LastCartonScanIn;
                    resultItem.LastPickingSelective = item.LastPickingSelective;
                    resultItem.LastInspectionTote = item.LastInspectionTote;
                    resultItem.Hrs_ASRS = item.Hrs_ASRS.ToString();
                    resultItem.Hrs_LBL = item.Hrs_LBL.ToString();
                    resultItem.Hrs_CFR = item.Hrs_CFR.ToString();
                    resultItem.Hrs_PickingWave = item.Hrs_PickingWave.ToString();

                    result.Add(resultItem);
                }
                olog.logging("ReportSorting", "Befor FromSql" + result);


                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportPickingPerformance");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);
                olog.logging("ReportSorting", "after GetUrl ReportPickingPerformance");
                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "ReportPickingPerformance" + DateTime.Now.ToString("yyyyMMddHHmmss");

                var renderedBytes = report.Execute(RenderType.Pdf);

                Utils objReport = new Utils();
                fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".pdf", rootPath);
                var saveLocation = objReport.PhysicalPath(fileName + ".pdf", rootPath);
                return saveLocation;


            }
            catch (Exception ex)
            {
                olog.logging("ReportSorting", ex.ToString());
                throw ex;
            }
        }
        #endregion

        public string ExportExcel(ReportPickingPerformanceViewModel data, string rootPath = "")
        {
            var db = new MasterDataDbContext();
            var db_temp = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            olog.logging("ReportSorting", "Start");
            var result = new List<ReportPickingPerformanceViewModel>();
            try
            {
                db.Database.SetCommandTimeout(360);
                db_temp.Database.SetCommandTimeout(360);
                var BusinessUnit_Index = "";
                var GoodsIssue_No = "";
                var TruckLoad_Index = "";
                var WaveRound = "";
                var GI_Date_From = "";
                var GI_Date_To = "";

                if (data.businessUnitList != null)
                {
                    BusinessUnit_Index = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.GoodsIssue_No))
                {
                    GoodsIssue_No = data.GoodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.TruckLoad_No))
                {
                    var dbGI = new GIDbContext();
                    var queryTruckLoad_Index = dbGI.im_TruckLoad.Where(C => C.TruckLoad_No == data.TruckLoad_No).FirstOrDefault();
                    TruckLoad_Index = queryTruckLoad_Index.TruckLoad_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.WaveRound))
                {
                    WaveRound = data.WaveRound;
                }
                if (!string.IsNullOrEmpty(data.GoodsIssue_Date) && !string.IsNullOrEmpty(data.GoodsIssue_Date_To))
                {
                    GI_Date_From = data.GoodsIssue_Date;
                    GI_Date_To = data.GoodsIssue_Date_To;
                }

                var businessUnit_Index = new SqlParameter("@BusinessUnit_Index", BusinessUnit_Index);
                var goodsIssue_No = new SqlParameter("@GoodsIssue_No", GoodsIssue_No);
                var truckLoad_Index = new SqlParameter("@TruckLoad_Index", TruckLoad_Index);
                var waveRound = new SqlParameter("@WaveRound", WaveRound);
                var gI_Date_From = new SqlParameter("@GI_Date_From", GI_Date_From);
                var gI_Date_To = new SqlParameter("@GI_Date_To", GI_Date_To);
                olog.logging("ReportSorting", BusinessUnit_Index);
                olog.logging("ReportSorting", GoodsIssue_No);
                olog.logging("ReportSorting", TruckLoad_Index);
                olog.logging("ReportSorting", WaveRound);
                olog.logging("ReportSorting", GI_Date_From);
                olog.logging("ReportSorting", GI_Date_To);
                olog.logging("ReportSorting", "Befor FromSql");
                var query = new List<MasterDataDataAccess.Models.sp_rpt_12_Picking_Performance>();

                var ambientRoom_name = "";
                if (data.ambientRoom != "02")
                {
                    olog.logging("ReportSorting", "Step 1");
                    query = db.sp_rpt_12_Picking_Performance.FromSql("sp_rpt_12_Picking_Performance @BusinessUnit_Index, @GoodsIssue_No, @TruckLoad_Index, @WaveRound, @GI_Date_From, @GI_Date_To",
                        businessUnit_Index, goodsIssue_No, truckLoad_Index, waveRound, gI_Date_From, gI_Date_To).ToList();
                    olog.logging("ReportSorting", "Step 2");
                    ambientRoom_name = "Ambient";
                }
                else
                {
                    olog.logging("ReportSorting", "Step 1");
                    query = db_temp.sp_rpt_12_Picking_Performance.FromSql("sp_rpt_12_Picking_Performance @BusinessUnit_Index, @GoodsIssue_No, @TruckLoad_Index, @WaveRound, @GI_Date_From, @GI_Date_To",
                        businessUnit_Index, goodsIssue_No, truckLoad_Index, waveRound, gI_Date_From, gI_Date_To).ToList();
                    olog.logging("ReportSorting", "Step 2");
                    ambientRoom_name = "Freeze";
                }
                olog.logging("ReportSorting", "After FromSql" + query);

                int num = 0;
                foreach (var item in query)
                {
                    var resultItem = new ReportPickingPerformanceViewModel();

                    num = num + 1;
                    resultItem.Row_No = num;
                    resultItem.ambientRoom_name = ambientRoom_name;
                    resultItem.BusinessUnit_Name = item.BusinessUnit_Name;
                    resultItem.Warehouse_Type = item.Warehouse_Type;
                    resultItem.GoodsIssue_Index = item.GoodsIssue_Index;
                    resultItem.GoodsIssue_No = item.GoodsIssue_No;
                    resultItem.GoodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.CountTruckLoad_GI = item.CountTruckLoad_GI;
                    resultItem.TruckLoad_No = item.TruckLoad_No;
                    resultItem.TruckLoad_Date = item.TruckLoad_Date;
                    resultItem.CBM = item.CBM;
                    resultItem.Sum_CBM = item.Sum_CBM;
                    resultItem.Dock_Name = item.Dock_Name;
                    resultItem.CountAllRollcage_GI = item.CountAllRollcage_GI;
                    resultItem.TruckLoadRollcage_Use = item.TruckLoadRollcage_Use;
                    resultItem.Chute_No = item.Chute_No;
                    resultItem.WaveRound = item.WaveRound;
                    resultItem.WaveStart_Date = item.WaveStart_Date;
                    resultItem.WaveComplete_Date = item.WaveComplete_Date;
                    resultItem.Tag_ASRS = item.Tag_ASRS;
                    resultItem.Tag_LBL = item.Tag_LBL;
                    resultItem.Tag_CFR_XL = item.Tag_CFR_XL;
                    resultItem.Tag_CFR_M = item.Tag_CFR_M;
                    resultItem.Total_Tag = item.Total_Tag;
                    resultItem.CountTagOut_GI = item.CountTagOut_GI;
                    resultItem.LastCartonScanIn = item.LastCartonScanIn;
                    resultItem.LastPickingSelective = item.LastPickingSelective;
                    resultItem.LastInspectionTote = item.LastInspectionTote;
                    resultItem.Hrs_ASRS = item.Hrs_ASRS.ToString();
                    resultItem.Hrs_LBL = item.Hrs_LBL.ToString();
                    resultItem.Hrs_CFR = item.Hrs_CFR.ToString();
                    resultItem.Hrs_PickingWave = item.Hrs_PickingWave.ToString();

                    result.Add(resultItem);
                }
                olog.logging("ReportSorting", "Befor FromSql" + result);




                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportPickingPerformance");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "ReportPickingPerformance" + DateTime.Now.ToString("yyyyMMddHHmmss");
                
                Utils objReport = new Utils();
                var renderedBytes = report.Execute(RenderType.Excel);
                //fullPath = saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);

                fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);
                var saveLocation = objReport.PhysicalPath(fileName + ".xls", rootPath);
                return saveLocation;


                //var saveLocation = rootPath + fullPath;
                ////File.Delete(saveLocation);
                ////ExcelRefresh(reportPath);
                //return saveLocation;
            }
            catch (Exception ex)
            {
                olog.logging("ReportSorting", ex.ToString());
                throw ex;
            }

        }
    }
}

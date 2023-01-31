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

namespace ReportBusiness.ReportPickingPerformanceRecords
{
    public class ReportPickingPerformanceRecordsService
    {

        #region printReportPickingPerformanceRecords
        public dynamic printReportPickingPerformanceRecords(ReportPickingPerformanceRecordsViewModel data, string rootPath = "")
        {
            var db = new GIDbContext();
            var M_DBContext = new MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            //olog.logging("ReportPickingPerformanceRecords", "Start");
            var result = new List<ReportPickingPerformanceRecordsViewModel>();
            try
            {
                DateTime startdate = DateTime.Now.toString().toBetweenDate().start;
                DateTime enddate = DateTime.Now.toString().toBetweenDate().end;

                //result.Add(new ReportPickingPerformanceRecordsViewModel() { GoodsIssue_Date = "12/12/2021", GoodsIssue_No = "GI001", TruckLoad_No = "TM0001", Total_tag = 10 });
                //result.Add(new ReportPickingPerformanceRecordsViewModel() { GoodsIssue_Date = "12/12/2021", GoodsIssue_No = "GI001", TruckLoad_No = "TM0002", Total_tag = 20 });
                //result.Add(new ReportPickingPerformanceRecordsViewModel() { GoodsIssue_Date = "12/12/2021", GoodsIssue_No = "GI002", TruckLoad_No = "TM0003", Total_tag = 30 });
                //result.Add(new ReportPickingPerformanceRecordsViewModel() { GoodsIssue_Date = "13/12/2021", GoodsIssue_No = "GI003", TruckLoad_No = "TM0004", Total_tag = 40 });

                var queryPP = db.TB_RPT_picking_Performance.AsQueryable();

                if (!string.IsNullOrEmpty(data.GoodsIssue_No))
                {
                    queryPP = queryPP.Where(c => c.GoodsIssue_No.Contains(data.GoodsIssue_No));
                }

                if (!string.IsNullOrEmpty(data.TruckLoad_No))
                {
                    queryPP = queryPP.Where(c => c.TruckLoad_No.Contains(data.TruckLoad_No));
                }

                if (!string.IsNullOrEmpty(data.Round_Name))
                {
                    //var queryRound = M_DBContext.MS_Round.Where(c => c.Round_Id == data.Round).Select(s => new { s.Round_Index, s.Round_Id, s.Round_Name }).FirstOrDefault();

                    //if (queryRound != null)
                    //{
                    //    //data.Round_index = queryRound.Round_Index.ToString();
                    //    queryPP = queryPP.Where(c => c.Round_Name == queryRound.Round_Name);
                    //}
                    queryPP = queryPP.Where(c => c.Round_Name == data.Round_Name);
                }

                if (!string.IsNullOrEmpty(data.GoodsIssue_Date) && !string.IsNullOrEmpty(data.GoodsIssue_Date_To))
                {
                    var dateStart = data.GoodsIssue_Date.toBetweenDate().start;
                    var dateEnd = data.GoodsIssue_Date_To.toBetweenDate().end;

                    startdate = dateStart;
                    enddate = dateEnd;

                    queryPP = queryPP.Where(c => c.GoodsIssue_Date >= dateStart && c.GoodsIssue_Date <= dateEnd);
                }
                else if (!string.IsNullOrEmpty(data.GoodsIssue_Date))
                {
                    var dateStart = data.GoodsIssue_Date.toBetweenDate().start;
                    var dateEnd = data.GoodsIssue_Date.toBetweenDate().end;

                    startdate = dateStart;
                    enddate = dateEnd;

                    queryPP = queryPP.Where(c => c.GoodsIssue_Date >= dateStart && c.GoodsIssue_Date <= dateEnd);
                }
                else if (!string.IsNullOrEmpty(data.GoodsIssue_Date_To))
                {
                    var dateStart = data.GoodsIssue_Date_To.toBetweenDate().start;
                    var dateEnd = data.GoodsIssue_Date_To.toBetweenDate().end;

                    startdate = dateStart;
                    enddate = dateEnd;

                    queryPP = queryPP.Where(c => c.GoodsIssue_Date >= dateStart && c.GoodsIssue_Date <= dateEnd);
                }

                //if (string.IsNullOrEmpty(data.GoodsIssue_No))
                //{
                //    data.GoodsIssue_No = "";
                //}
                //if (string.IsNullOrEmpty(data.TruckLoad_No))
                //{
                //    data.TruckLoad_No = "";
                //}
                //if (string.IsNullOrEmpty(data.Round_index))
                //{
                //    data.Round_index = "";
                //}

                //var gi_No = new SqlParameter("@GI", data.GoodsIssue_No);
                //var load_No = new SqlParameter("@TL", data.TruckLoad_No);
                //var rnd = new SqlParameter("@RND", data.Round_index);
                //var start = new SqlParameter("@GI_DATE", startdate.ToString("yyyy-MM-dd HH:mm:ss"));
                //var end = new SqlParameter("@GI_DATE_TO", enddate.ToString("yyyy-MM-dd HH:mm:ss"));

                //var query = db.sp_Picking_performance.FromSql("sp_Picking_performance @GI, @TL, @RND, @GI_DATE, @GI_DATE_TO", gi_No, load_No, rnd, start, end).ToList();
                var query = queryPP.ToList();
                foreach (var item in query)
                {
                    var resultItem = new ReportPickingPerformanceRecordsViewModel();
                    resultItem.GoodsIssue_No = item.GoodsIssue_No;
                    resultItem.GoodsIssue_Date = item.GoodsIssue_Date.ToString("dd-MM-yyyy");
                    resultItem.TruckLoad_No = item.TruckLoad_No;
                    resultItem.TruckLoad_Date = item.TruckLoad_Date.ToString("dd-MM-yyyy");
                    resultItem.Dock_Name = item.Dock_Name;
                    resultItem.Rollcage_Use = item.Rollcage_Use;
                    resultItem.Chute_No = item.Chute_No;
                    resultItem.Round_Name = item.Round_Name;
                    resultItem.Start_Wave = item.Start_Wave == null ? "" : item.Start_Wave.Value.ToString("HH:mm:ss");
                    resultItem.Closed_Wave = item.Closed_Wave == null ? "" : item.Closed_Wave.Value.ToString("HH:mm:ss");
                    resultItem.CBM = item.CBM;
                    resultItem.Tag_ASRS = item.tag_ASRS;
                    resultItem.Tag_LBL = item.tag_LBL;
                    resultItem.Tag_CFR_XL = item.tag_CFR_XL;
                    resultItem.Tag_CFR_M = item.tag_CFR_M;
                    resultItem.Total_tag = item.Total_tag;
                    resultItem.Last_Scanin = item.Last_Scanin == null ? "" : item.Last_Scanin.Value.ToString("HH:mm:ss");
                    resultItem.Last_Selecting = item.Last_Selecting == null ? "" : item.Last_Selecting.Value.ToString("HH:mm:ss");
                    resultItem.Last_Inpection = item.Last_Inpection == null ? "" : item.Last_Inpection.Value.ToString("HH:mm:ss");
                    //resultItem.Duration_LBL = item.Duration_LBL;
                    //resultItem.Duration_PP = item.Duration_PP;
                    //resultItem.Picking_Wave = item.Picking_Wave;
                    if (item.Duration_ASRS != null)
                    {
                        TimeSpan asrs = TimeSpan.FromMinutes(item.Duration_ASRS ?? 0);
                        resultItem.Duration_ASRS = string.Format("{0}:{1:00}", (int)asrs.TotalHours, asrs.Minutes);
                    }
                    else
                    {
                        resultItem.Duration_ASRS = "";
                    }
                    if (item.Duration_LBL != null)
                    {
                        TimeSpan lbl = TimeSpan.FromMinutes(item.Duration_LBL ?? 0);
                        resultItem.Duration_LBL = string.Format("{0}:{1:00}", (int)lbl.TotalHours, lbl.Minutes);
                    }
                    else
                    {
                        resultItem.Duration_LBL = "";
                    }
                    if (item.Duration_PP != null)
                    {
                        TimeSpan pp = TimeSpan.FromMinutes(item.Duration_PP ?? 0);
                        resultItem.Duration_PP = string.Format("{0}:{1:00}", (int)pp.TotalHours, pp.Minutes);
                    }
                    else
                    {
                        resultItem.Duration_PP = "";
                    }
                    if (item.Picking_Wave != null)
                    {
                        TimeSpan pw = TimeSpan.FromMinutes(item.Picking_Wave ?? 0);
                        resultItem.Picking_Wave = string.Format("{0}:{1:00}", (int)pw.TotalHours, pw.Minutes);
                    }
                    else
                    {
                        resultItem.Picking_Wave = "";
                    }

                    result.Add(resultItem);
                }



                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportPickingPerformanceRecords");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "ReportPickingPerformanceRecords" + DateTime.Now.ToString("yyyyMMddHHmmss");

                var renderedBytes = report.Execute(RenderType.Pdf);

                Utils objReport = new Utils();
                fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".pdf", rootPath);
                var saveLocation = objReport.PhysicalPath(fileName + ".pdf", rootPath);
                return saveLocation;


            }
            catch (Exception ex)
            {
                //olog.logging("ReportPickingPerformanceRecords", ex.Message);
                throw ex;
            }
        }
        #endregion

        public string ExportExcel(ReportPickingPerformanceRecordsViewModel data, string rootPath = "")
        {
            var db = new GIDbContext();
            var M_DBContext = new MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportPickingPerformanceRecordsViewModel>();

            try
            {
                DateTime startdate = DateTime.Now.toString().toBetweenDate().start;
                DateTime enddate = DateTime.Now.toString().toBetweenDate().end;

                //result.Add(new ReportPickingPerformanceRecordsViewModel() { GoodsIssue_Date = "12/12/2021", GoodsIssue_No = "GI001", TruckLoad_No = "TM0001", Total_tag = 10 });
                //result.Add(new ReportPickingPerformanceRecordsViewModel() { GoodsIssue_Date = "12/12/2021", GoodsIssue_No = "GI001", TruckLoad_No = "TM0002", Total_tag = 20 });
                //result.Add(new ReportPickingPerformanceRecordsViewModel() { GoodsIssue_Date = "12/12/2021", GoodsIssue_No = "GI002", TruckLoad_No = "TM0003", Total_tag = 30 });
                //result.Add(new ReportPickingPerformanceRecordsViewModel() { GoodsIssue_Date = "13/12/2021", GoodsIssue_No = "GI003", TruckLoad_No = "TM0004", Total_tag = 40 });

                var queryPP = db.TB_RPT_picking_Performance.AsQueryable();

                if (!string.IsNullOrEmpty(data.GoodsIssue_No))
                {
                    queryPP = queryPP.Where(c => c.GoodsIssue_No.Contains(data.GoodsIssue_No));
                }

                if (!string.IsNullOrEmpty(data.TruckLoad_No))
                {
                    queryPP = queryPP.Where(c => c.TruckLoad_No.Contains(data.TruckLoad_No));
                }

                if (!string.IsNullOrEmpty(data.Round_Name))
                {
                    //var queryRound = M_DBContext.MS_Round.Where(c => c.Round_Id == data.Round).Select(s => new { s.Round_Index, s.Round_Id, s.Round_Name }).FirstOrDefault();

                    //if (queryRound != null)
                    //{
                    //    //data.Round_index = queryRound.Round_Index.ToString();
                    //    queryPP = queryPP.Where(c => c.Round_Name == queryRound.Round_Name);
                    //}
                    queryPP = queryPP.Where(c => c.Round_Name == data.Round_Name);
                }

                if (!string.IsNullOrEmpty(data.GoodsIssue_Date) && !string.IsNullOrEmpty(data.GoodsIssue_Date_To))
                {
                    var dateStart = data.GoodsIssue_Date.toBetweenDate().start;
                    var dateEnd = data.GoodsIssue_Date_To.toBetweenDate().end;

                    startdate = dateStart;
                    enddate = dateEnd;

                    queryPP = queryPP.Where(c => c.GoodsIssue_Date >= dateStart && c.GoodsIssue_Date <= dateEnd);
                }
                else if (!string.IsNullOrEmpty(data.GoodsIssue_Date))
                {
                    var dateStart = data.GoodsIssue_Date.toBetweenDate().start;
                    var dateEnd = data.GoodsIssue_Date.toBetweenDate().end;

                    startdate = dateStart;
                    enddate = dateEnd;

                    queryPP = queryPP.Where(c => c.GoodsIssue_Date >= dateStart && c.GoodsIssue_Date <= dateEnd);
                }
                else if (!string.IsNullOrEmpty(data.GoodsIssue_Date_To))
                {
                    var dateStart = data.GoodsIssue_Date_To.toBetweenDate().start;
                    var dateEnd = data.GoodsIssue_Date_To.toBetweenDate().end;

                    startdate = dateStart;
                    enddate = dateEnd;

                    queryPP = queryPP.Where(c => c.GoodsIssue_Date >= dateStart && c.GoodsIssue_Date <= dateEnd);
                }

                //if (string.IsNullOrEmpty(data.GoodsIssue_No))
                //{
                //    data.GoodsIssue_No = "";
                //}
                //if (string.IsNullOrEmpty(data.TruckLoad_No))
                //{
                //    data.TruckLoad_No = "";
                //}
                //if (string.IsNullOrEmpty(data.Round_index))
                //{
                //    data.Round_index = "";
                //}

                //var gi_No = new SqlParameter("@GI", data.GoodsIssue_No);
                //var load_No = new SqlParameter("@TL", data.TruckLoad_No);
                //var rnd = new SqlParameter("@RND", data.Round_index);
                //var start = new SqlParameter("@GI_DATE", startdate.ToString("yyyy-MM-dd HH:mm:ss"));
                //var end = new SqlParameter("@GI_DATE_TO", enddate.ToString("yyyy-MM-dd HH:mm:ss"));

                //var query = db.sp_Picking_performance.FromSql("sp_Picking_performance @GI, @TL, @RND, @GI_DATE, @GI_DATE_TO", gi_No, load_No, rnd, start, end).ToList();
                var query = queryPP.ToList();
                foreach (var item in query)
                {
                    var resultItem = new ReportPickingPerformanceRecordsViewModel();
                    resultItem.GoodsIssue_No = item.GoodsIssue_No;
                    resultItem.GoodsIssue_Date = item.GoodsIssue_Date.ToString("dd-MM-yyyy");
                    resultItem.TruckLoad_No = item.TruckLoad_No;
                    resultItem.TruckLoad_Date = item.TruckLoad_Date.ToString("dd-MM-yyyy");
                    resultItem.Dock_Name = item.Dock_Name;
                    resultItem.Rollcage_Use = item.Rollcage_Use;
                    resultItem.Chute_No = item.Chute_No;
                    resultItem.Round_Name = item.Round_Name;
                    resultItem.Start_Wave = item.Start_Wave == null ? "" : item.Start_Wave.Value.ToString("HH:mm:ss");
                    resultItem.Closed_Wave = item.Closed_Wave == null ? "" : item.Closed_Wave.Value.ToString("HH:mm:ss");
                    resultItem.CBM = item.CBM;
                    resultItem.Tag_ASRS = item.tag_ASRS;
                    resultItem.Tag_LBL = item.tag_LBL;
                    resultItem.Tag_CFR_XL = item.tag_CFR_XL;
                    resultItem.Tag_CFR_M = item.tag_CFR_M;
                    resultItem.Total_tag = item.Total_tag;
                    resultItem.Last_Scanin = item.Last_Scanin == null ? "" : item.Last_Scanin.Value.ToString("HH:mm:ss");
                    resultItem.Last_Selecting = item.Last_Selecting == null ? "" : item.Last_Selecting.Value.ToString("HH:mm:ss");
                    resultItem.Last_Inpection = item.Last_Inpection == null ? "" : item.Last_Inpection.Value.ToString("HH:mm:ss");
                    //resultItem.Duration_LBL = item.Duration_LBL;
                    //resultItem.Duration_PP = item.Duration_PP;
                    //resultItem.Picking_Wave = item.Picking_Wave;
                    if (item.Duration_ASRS != null)
                    {
                        TimeSpan asrs = TimeSpan.FromMinutes(item.Duration_ASRS ?? 0);
                        resultItem.Duration_ASRS = string.Format("{0}:{1:00}", (int)asrs.TotalHours, asrs.Minutes);
                    }
                    else
                    {
                        resultItem.Duration_ASRS = "";
                    }
                    if (item.Duration_LBL != null)
                    {
                        TimeSpan lbl = TimeSpan.FromMinutes(item.Duration_LBL ?? 0);
                        resultItem.Duration_LBL = string.Format("{0}:{1:00}", (int)lbl.TotalHours, lbl.Minutes);
                    }
                    else
                    {
                        resultItem.Duration_LBL = "";
                    }
                    if (item.Duration_PP != null)
                    {
                        TimeSpan pp = TimeSpan.FromMinutes(item.Duration_PP ?? 0);
                        resultItem.Duration_PP = string.Format("{0}:{1:00}", (int)pp.TotalHours, pp.Minutes);
                    }
                    else
                    {
                        resultItem.Duration_PP = "";
                    }
                    if (item.Picking_Wave != null)
                    {
                        TimeSpan pw = TimeSpan.FromMinutes(item.Picking_Wave ?? 0);
                        resultItem.Picking_Wave = string.Format("{0}:{1:00}", (int)pw.TotalHours, pw.Minutes);
                    }
                    else
                    {
                        resultItem.Picking_Wave = "";
                    }

                    result.Add(resultItem);
                }


                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportPickingPerformanceRecords");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "ReportPickingPerformanceRecords" + DateTime.Now.ToString("yyyyMMddHHmmss");

                var renderedBytes = report.Execute(RenderType.Excel);
                //fullPath = saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);

                Utils objReport = new Utils();
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
                throw ex;
            }

        }

        //public string saveReport(byte[] file, string name, string rootPath)
        //{
        //    var saveLocation = PhysicalPath(name, rootPath);
        //    FileStream fs = new FileStream(saveLocation, FileMode.Create);
        //    BinaryWriter bw = new BinaryWriter(fs);
        //    try
        //    {
        //        try
        //        {
        //            bw.Write(file);
        //        }
        //        finally
        //        {
        //            fs.Close();
        //            bw.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return VirtualPath(name);
        //}

        //public string PhysicalPath(string name, string rootPath)
        //{
        //    var filename = name;
        //    var vPath = ReportPath;
        //    var path = rootPath + vPath;
        //    if (!System.IO.Directory.Exists(path))
        //    {
        //        System.IO.Directory.CreateDirectory(path);
        //    }

        //    var saveLocation = System.IO.Path.Combine(path, filename);
        //    return saveLocation;
        //}
        //public string VirtualPath(string name)
        //{
        //    var filename = name;
        //    var vPath = ReportPath;
        //    vPath = vPath.Replace("~", "");
        //    return vPath + filename;
        //}
        //private string ReportPath
        //{
        //    get
        //    {
        //        var url = "\\ReportGenerator\\";
        //        return url;
        //    }
        //}
    }
}

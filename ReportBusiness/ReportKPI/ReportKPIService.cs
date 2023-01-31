using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using PlanGRBusiness.Libs;
using ReportDataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
namespace ReportBusiness.ReportKPI
{
    public class ReportKPIService
    {
        #region printReportKPI
        public dynamic printReportKPI(ReportKPIViewModel data, string rootPath = "")
        {

            //var BB_DBContext = new BinbalanceDbContext();
            

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<ReportKPIViewModel>();

            try
            {
                var D_DBContext = new DashboardDbContext();

                var dateNow = DateTime.Now;
                var Current_Date = dateNow.ToString("dd/MM/yyyy");
                var Current_Time = dateNow.ToString("HH:mm:ss");

                var query = D_DBContext.View_KPI.ToList();

                //olog.logging("ReportKPI", data.Report_Date + " " + data.Report_Date_To);
                if (!string.IsNullOrEmpty(data.Report_Date) && !string.IsNullOrEmpty(data.Report_Date_To))
                {
                    //DateTime dateStart = DateTime.ParseExact(data.Report_Date, "yyyyMMdd", CultureInfo.InvariantCulture);
                    //DateTime dateEnd = DateTime.ParseExact(data.Report_Date_To, "yyyyMMdd", CultureInfo.InvariantCulture);
                    //query = query.Where(c => c.Activity_Date >= dateStart && c.Activity_Date <= dateEnd).ToList();

                    var dateStart = data.Report_Date.toBetweenDate();
                    var dateEnd = data.Report_Date_To.toBetweenDate();
                    //olog.logging("ReportKPI", dateStart.start + " " + dateEnd.end);
                    query = query.Where(c => c.Activity_Date >= dateStart.start && c.Activity_Date <= dateEnd.end).ToList();
                }
                else if (!string.IsNullOrEmpty(data.Report_Date))
                {
                    var report_Date_From = data.Report_Date.toBetweenDate();
                    query = query.Where(c => c.Activity_Date >= report_Date_From.start).ToList();
                }
                else if (!string.IsNullOrEmpty(data.Report_Date_To))
                {
                    var report_Date_To = data.Report_Date_To.toBetweenDate();
                    query = query.Where(c => c.Activity_Date <= report_Date_To.start).ToList();
                }
                //olog.logging("ReportKPI", "End Where");
                query = query.OrderBy(o => o.Activity_Date).ToList();
                //olog.logging("ReportKPI", query.Count.ToString());

                foreach (var item in query)
                {
                    var resultItem = new ReportKPIViewModel();
                    resultItem.Current_Date = Current_Date;
                    resultItem.Current_Time = Current_Time;
                    resultItem.Kpi_Date = item.Activity_Date.ToString("dd-MM-yyyy");
                    resultItem.Inbound_Receiving_WU = item.InB_RECV_WorkUnit;
                    resultItem.Inbound_Receiving_AVG = item.InB_RECV_AVG;
                    resultItem.Inbound_Receiving_Rating = item.InB_RECV_Rating;
                    resultItem.Inbound_Putaway_WU = item.InB_PUT_WorkUnit;
                    resultItem.Inbound_Putaway_AVG = item.InB_PUT_AVG;
                    resultItem.Inbound_Putaway_Rating = item.InB_PUT_Rating;
                    resultItem.Production_Receiving_WU = item.Prod_RECV_WorkUnit;
                    resultItem.Production_Receiving_AVG = item.Prod_RECV_AVG;
                    resultItem.Production_Receiving_Rating = item.Prod_RECV_Rating;
                    resultItem.Production_Putaway_WU = item.Prod_PUT_WorkUnit;
                    resultItem.Production_Putaway_AVG = item.Prod_PUT_AVG;
                    resultItem.Production_Putaway_Rating = item.Prod_PUT_Rating;
                    resultItem.Outbound_QI_WU = item.TF_QI_WorkUnit;
                    resultItem.Outbound_QI_AVG = item.TF_QI_AVG;
                    resultItem.Outbound_QI_Rating = item.TF_QI_Rating;
                    resultItem.Outbound_Picking_WU = item.OutB_PICK_WorkUnit;
                    resultItem.Outbound_Picking_AVG = item.OutB_PICK_AVG;
                    resultItem.Outbound_Picking_Rating = item.OutB_PICK_Rating;
                    resultItem.Outbound_Block_WU = item.TF_BLOCK_WorkUnit;
                    resultItem.Outbound_Block_AVG = item.TF_BLOCK_AVG;
                    resultItem.Outbound_Block_Rating = item.TF_BLOCK_Rating;

                    result.Add(resultItem);
                }

                //result.Add(new ReportKPIViewModel() { Current_Date = Current_Date, Current_Time = Current_Time, Kpi_Date = "01/12/2021", Inbound_Receiving_WU = 144, Inbound_Receiving_AVG = 80, Inbound_Receiving_Rating = "OK"});
                //result.Add(new ReportKPIViewModel() { Current_Date = Current_Date, Current_Time = Current_Time, Kpi_Date = "02/12/2021", Inbound_Receiving_WU = 123, Inbound_Receiving_AVG = 121, Inbound_Receiving_Rating = "TEST" });
                //result.Add(new ReportKPIViewModel() { Current_Date = Current_Date, Current_Time = Current_Time, Kpi_Date = "03/12/2021", Inbound_Receiving_WU = 110, Inbound_Receiving_AVG = 76, Inbound_Receiving_Rating = "TEST" });
                //result.Add(new ReportKPIViewModel() { Current_Date = Current_Date, Current_Time = Current_Time, Kpi_Date = "04/12/2021", Inbound_Receiving_WU = 34, Inbound_Receiving_AVG = 80, Inbound_Receiving_Rating = "OK" });
                //result.Add(new ReportKPIViewModel() { Current_Date = Current_Date, Current_Time = Current_Time, Kpi_Date = "05/12/2021", Inbound_Receiving_WU = 48, Inbound_Receiving_AVG = 76, Inbound_Receiving_Rating = "M" });
                //result.Add(new ReportKPIViewModel() { Current_Date = Current_Date, Current_Time = Current_Time, Kpi_Date = "06/12/2021", Inbound_Receiving_WU = 54, Inbound_Receiving_AVG = 65, Inbound_Receiving_Rating = "OK" });
                //result.Add(new ReportKPIViewModel() { Current_Date = Current_Date, Current_Time = Current_Time, Kpi_Date = "07/12/2021", Inbound_Receiving_WU = 102, Inbound_Receiving_AVG = 87, Inbound_Receiving_Rating = "TEST" });
                //result.Add(new ReportKPIViewModel() { Current_Date = Current_Date, Current_Time = Current_Time, Kpi_Date = "08/12/2021", Inbound_Receiving_WU = 87, Inbound_Receiving_AVG = 60, Inbound_Receiving_Rating = "OK" });
                //result.Add(new ReportKPIViewModel() { Current_Date = Current_Date, Current_Time = Current_Time, Kpi_Date = "09/12/2021", Inbound_Receiving_WU = 95, Inbound_Receiving_AVG = 95, Inbound_Receiving_Rating = "OK" });
                //result.Add(new ReportKPIViewModel() { Current_Date = Current_Date, Current_Time = Current_Time, Kpi_Date = "10/12/2021", Inbound_Receiving_WU = 121, Inbound_Receiving_AVG = 120, Inbound_Receiving_Rating = "OK" });







                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportKPI");
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

        public string ExportExcel(ReportKPIViewModel data, string rootPath = "")
        {

            var D_DBContext = new DashboardDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportKPIViewModel>();

            try
            {
                var dateNow = DateTime.Now;
                var Current_Date = dateNow.ToString("dd/MM/yyyy");
                var Current_Time = dateNow.ToString("HH:mm:ss");

                var query = D_DBContext.View_KPI.ToList();

                if (!string.IsNullOrEmpty(data.Report_Date) && !string.IsNullOrEmpty(data.Report_Date_To))
                {
                    var dateStart = data.Report_Date.toBetweenDate();
                    var dateEnd = data.Report_Date_To.toBetweenDate();
                    query = query.Where(c => c.Activity_Date >= dateStart.start && c.Activity_Date <= dateEnd.end).ToList();
                }
                else if (!string.IsNullOrEmpty(data.Report_Date))
                {
                    var report_Date_From = data.Report_Date.toBetweenDate();
                    query = query.Where(c => c.Activity_Date >= report_Date_From.start).ToList();
                }
                else if (!string.IsNullOrEmpty(data.Report_Date_To))
                {
                    var report_Date_To = data.Report_Date_To.toBetweenDate();
                    query = query.Where(c => c.Activity_Date <= report_Date_To.start).ToList();
                }

                query = query.OrderBy(o => o.Activity_Date).ToList();

                foreach (var item in query)
                {
                    var resultItem = new ReportKPIViewModel();
                    resultItem.Current_Date = Current_Date;
                    resultItem.Current_Time = Current_Time;
                    resultItem.Kpi_Date = item.Activity_Date.ToString("dd-MM-yyyy");
                    resultItem.Inbound_Receiving_WU = item.InB_RECV_WorkUnit;
                    resultItem.Inbound_Receiving_AVG = item.InB_RECV_AVG;
                    resultItem.Inbound_Receiving_Rating = item.InB_RECV_Rating;
                    resultItem.Inbound_Putaway_WU = item.InB_PUT_WorkUnit;
                    resultItem.Inbound_Putaway_AVG = item.InB_PUT_AVG;
                    resultItem.Inbound_Putaway_Rating = item.InB_PUT_Rating;
                    resultItem.Production_Receiving_WU = item.Prod_RECV_WorkUnit;
                    resultItem.Production_Receiving_AVG = item.Prod_RECV_AVG;
                    resultItem.Production_Receiving_Rating = item.Prod_RECV_Rating;
                    resultItem.Production_Putaway_WU = item.Prod_PUT_WorkUnit;
                    resultItem.Production_Putaway_AVG = item.Prod_PUT_AVG;
                    resultItem.Production_Putaway_Rating = item.Prod_PUT_Rating;
                    resultItem.Outbound_QI_WU = item.TF_QI_WorkUnit;
                    resultItem.Outbound_QI_AVG = item.TF_QI_AVG;
                    resultItem.Outbound_QI_Rating = item.TF_QI_Rating;
                    resultItem.Outbound_Picking_WU = item.OutB_PICK_WorkUnit;
                    resultItem.Outbound_Picking_AVG = item.OutB_PICK_AVG;
                    resultItem.Outbound_Picking_Rating = item.OutB_PICK_Rating;
                    resultItem.Outbound_Block_WU = item.TF_BLOCK_WorkUnit;
                    resultItem.Outbound_Block_AVG = item.TF_BLOCK_AVG;
                    resultItem.Outbound_Block_Rating = item.TF_BLOCK_Rating;

                    result.Add(resultItem);
                }


                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportKPI");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "ReportKPI" + DateTime.Now.ToString("yyyyMMddHHmmss");

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

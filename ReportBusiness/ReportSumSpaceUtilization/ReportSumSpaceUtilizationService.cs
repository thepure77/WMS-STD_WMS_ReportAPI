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

namespace ReportBusiness.ReportSumSpaceUtilization
{
    public class ReportSumSpaceUtilizationService
    {
        #region printReportSumSpaceUtilization
        public dynamic printReportSumSpaceUtilization(ReportSumSpaceUtilizationViewModel data, string rootPath = "")
        {            
            try
            {
                var result = Getdata(data);

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSumSpaceUtilizationNew");
                LocalReport report = new LocalReport(reportPath);
                if (data.ambientRoom == "01" || data.ambientRoom == "02")
                {
                    report.AddDataSource("DataSet1", result.location_type);
                    report.AddDataSource("DataSet2", result.owner);
                }
                else
                {
                    reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSumSpaceUtilizationAllNew");
                    report = new LocalReport(reportPath);
                    report.AddDataSource("DataSet1", result.location_type);
                    report.AddDataSource("DataSet2", result.owner);
                    report.AddDataSource("DataSet3", result.location_type_all);
                    report.AddDataSource("DataSet4", result.owner_all);
                }

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

        public string ExportExcel(ReportSumSpaceUtilizationViewModel data, string rootPath = "")
        {            
            //var result = new List<ReportSumSpaceUtilizationViewModel>();
            try
            {
                var result = Getdata(data);

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSumSpaceUtilizationNew");
                LocalReport report = new LocalReport(reportPath);
                if (data.ambientRoom == "01" || data.ambientRoom == "02")
                {
                    report.AddDataSource("DataSet1", result.location_type);
                    report.AddDataSource("DataSet2", result.owner);
                }
                else
                {
                    reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSumSpaceUtilizationAllNew");
                    report = new LocalReport(reportPath);
                    report.AddDataSource("DataSet1", result.location_type);
                    report.AddDataSource("DataSet2", result.owner);
                    report.AddDataSource("DataSet3", result.location_type_all);
                    report.AddDataSource("DataSet4", result.owner_all);
                }

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

        //public ReportResultSumSpaceUtilizationViewModel Getdata(ReportSumSpaceUtilizationViewModel data)
        //{
        //    var Master_DBContext = new MasterDataDbContext();
        //    var temp_Master_DBContext = new temp_MasterDataDbContext();

        //    var culture = new System.Globalization.CultureInfo("en-US");
        //    //String State = "Start";
        //    //String msglog = "";
        //    //var olog = new logtxt();
        //    var result = new ReportResultSumSpaceUtilizationViewModel();
        //    var location_list = new List<ReportSumSpaceUtilizationViewModel>();
        //    var owner_list = new List<ReportSumSpaceUtilizationViewModel>();
        //    var location_per_list = new List<ReportSumSpaceUtilizationViewModel>();
        //    var owner_per_list = new List<ReportSumSpaceUtilizationViewModel>();
        //    var location_list_all = new List<ReportSumSpaceUtilizationViewModel>();
        //    var owner_list_all = new List<ReportSumSpaceUtilizationViewModel>();
        //    var location_per_list_all = new List<ReportSumSpaceUtilizationViewModel>();
        //    var owner_per_list_all = new List<ReportSumSpaceUtilizationViewModel>();

        //    try
        //    {
        //        Master_DBContext.Database.SetCommandTimeout(360);
        //        temp_Master_DBContext.Database.SetCommandTimeout(360);
        //        string datereport = "";
        //        string locationType_Name = "";
        //        DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
        //        DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

        //        var day_of_month = "";
        //        var date_From = "";
        //        var date_To = "";
        //        var check_month = "ReportSumSpaceUtilization";
        //        var warehouse = "";

        //        //if (!string.IsNullOrEmpty(data.month) && data.year != null)
        //        //{
        //        //    day_of_month = DateTime.DaysInMonth(data.year, Convert.ToInt32(data.month)).ToString();
        //        //    date_From = data.year.ToString() + "-" + data.month + "-" + "01";
        //        //    date_To = data.year.ToString() + "-" + data.month + "-" + day_of_month;

        //        //    string fullMonthName = new DateTime(data.year, Convert.ToInt32(data.month), 1).ToString("MMMM");
        //        //    datereport = "Monthly  : " + fullMonthName + "  " + data.year;

        //        //    if (day_of_month == "28")
        //        //    {
        //        //        check_month = "ReportSumSpaceUtilization_28";
        //        //    }
        //        //    else if (day_of_month == "29")
        //        //    {
        //        //        check_month = "ReportSumSpaceUtilization_29";
        //        //    }
        //        //    else if (day_of_month == "30")
        //        //    {
        //        //        check_month = "ReportSumSpaceUtilization_30";
        //        //    }
        //        //}

        //        if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
        //        {
        //            date_From = data.report_date;
        //            date_To = data.report_date_to;
        //            //datereport = "Date  : ";

        //            var sd_string = data.report_date.Substring(6, 2) + "/" + data.report_date.Substring(4, 2) + "/" + data.report_date.Substring(0, 4);
        //            var ed_string = data.report_date_to.Substring(6, 2) + "/" + data.report_date_to.Substring(4, 2) + "/" + data.report_date_to.Substring(0, 4);
        //            datereport = "Date From : " + sd_string + " To  " + ed_string;
        //        }

        //        //if (!string.IsNullOrEmpty(data.month) && data.year != null)
        //        //{
        //        //    day_of_month = DateTime.DaysInMonth(data.year, Convert.ToInt32(data.month)).ToString();
        //        //    date_From = data.year.ToString() + "-" + data.month + "-" + "01";
        //        //    date_To = data.year.ToString() + "-" + data.month + "-" + day_of_month;

        //        //    string fullMonthName = new DateTime(data.year, Convert.ToInt32(data.month), 1).ToString("MMMM");
        //        //    datereport = "Date  : ";

        //        //    //if (day_of_month == "28")
        //        //    //{
        //        //    //    check_month = "ReportSumSpaceUtilization_28";
        //        //    //}
        //        //    //else if (day_of_month == "29")
        //        //    //{
        //        //    //    check_month = "ReportSumSpaceUtilization_29";
        //        //    //}
        //        //    //else if (day_of_month == "30")
        //        //    //{
        //        //    //    check_month = "ReportSumSpaceUtilization_30";
        //        //    //}
        //        //}


        //        if (data.locationTypeList != null)
        //        {
        //            locationType_Name = data.locationTypeList.locationType_Name.ToString();
        //        }


        //        var statusModels = new List<int?>();
        //        var query_location = $@"EXEC sp_rpt_15_Space_Utilization '{locationType_Name}','{date_From}','{date_To}'";
        //        var query_owner = $@"EXEC sp_rpt_15_Space_Utilization_Owner '{locationType_Name}','{date_From}','{date_To}'";
        //        var query_location_per = $@"EXEC sp_rpt_15_Space_Utilization_Percent '{locationType_Name}','{date_From}','{date_To}'";
        //        var query_owner_per = $@"EXEC sp_rpt_15_Space_Utilization_Percent_Owner '{locationType_Name}','{date_From}','{date_To}'";


        //        var resultquery_location = new List<MasterDataDataAccess.Models.sp_rpt_15_Space_Utilization>();
        //        var resultquery_owner = new List<MasterDataDataAccess.Models.sp_rpt_15_Space_Utilization_Owner>();
        //        var resultquery_location_per = new List<MasterDataDataAccess.Models.sp_rpt_15_Space_Utilization_Percent>();
        //        var resultquery_owner_per = new List<MasterDataDataAccess.Models.sp_rpt_15_Space_Utilization_Percent_Owner>();


        //        if (data.ambientRoom == "01")
        //        {
        //            warehouse = "Ambient Total";
        //            resultquery_location = Master_DBContext.sp_rpt_15_Space_Utilization.FromSql(query_location).ToList();
        //            resultquery_owner = Master_DBContext.sp_rpt_15_Space_Utilization_Owner.FromSql(query_owner).ToList();
        //            resultquery_location_per = Master_DBContext.sp_rpt_15_Space_Utilization_Percent.FromSql(query_location_per).ToList();
        //            resultquery_owner_per = Master_DBContext.sp_rpt_15_Space_Utilization_Percent_Owner.FromSql(query_owner_per).ToList();

        //        }
        //        else if (data.ambientRoom == "02")
        //        {
        //            warehouse = "Freeze Total";
        //            resultquery_location = temp_Master_DBContext.sp_rpt_15_Space_Utilization.FromSql(query_location).ToList();
        //            resultquery_owner = temp_Master_DBContext.sp_rpt_15_Space_Utilization_Owner.FromSql(query_owner).ToList();
        //            resultquery_location_per = temp_Master_DBContext.sp_rpt_15_Space_Utilization_Percent.FromSql(query_location_per).ToList();
        //            resultquery_owner_per = temp_Master_DBContext.sp_rpt_15_Space_Utilization_Percent_Owner.FromSql(query_owner_per).ToList();
        //        }
        //        else
        //        {
        //            resultquery_location = Master_DBContext.sp_rpt_15_Space_Utilization.FromSql(query_location).ToList();
        //            resultquery_owner = Master_DBContext.sp_rpt_15_Space_Utilization_Owner.FromSql(query_owner).ToList();
        //            resultquery_location_per = Master_DBContext.sp_rpt_15_Space_Utilization_Percent.FromSql(query_location_per).ToList();
        //            resultquery_owner_per = Master_DBContext.sp_rpt_15_Space_Utilization_Percent_Owner.FromSql(query_owner_per).ToList();

        //            if (resultquery_location.Count == 0)
        //            {
        //                var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                resultItem.report_date = datereport;
        //                resultItem.report_date_to = endDate;
        //                resultItem.warehouse = warehouse;
        //                //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        //                location_list.Add(resultItem);
        //            }
        //            else
        //            {

        //                int num = 0;
        //                foreach (var item in resultquery_location)
        //                {
        //                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                    resultItem.rowNum = num + 1;
        //                    resultItem.ambientRoom = "Ambient";
        //                    resultItem.locationType_Name = item.LocationType_Name;
        //                    resultItem.count_Location = item.Count_Location;
        //                    resultItem.d01 = item.D01;
        //                    resultItem.d02 = item.D02;
        //                    resultItem.d03 = item.D03;
        //                    resultItem.d04 = item.D04;
        //                    resultItem.d05 = item.D05;
        //                    resultItem.d06 = item.D06;
        //                    resultItem.d07 = item.D07;
        //                    resultItem.d08 = item.D08;
        //                    resultItem.d09 = item.D09;
        //                    resultItem.d10 = item.D10;
        //                    resultItem.d11 = item.D11;
        //                    resultItem.d12 = item.D12;
        //                    resultItem.d13 = item.D13;
        //                    resultItem.d14 = item.D14;
        //                    resultItem.d15 = item.D15;
        //                    resultItem.d16 = item.D16;
        //                    resultItem.d17 = item.D17;
        //                    resultItem.d18 = item.D18;
        //                    resultItem.d19 = item.D19;
        //                    resultItem.d20 = item.D20;
        //                    resultItem.d21 = item.D21;
        //                    resultItem.d22 = item.D22;
        //                    resultItem.d23 = item.D23;
        //                    resultItem.d24 = item.D24;
        //                    resultItem.d25 = item.D25;
        //                    resultItem.d26 = item.D26;
        //                    resultItem.d27 = item.D27;
        //                    resultItem.d28 = item.D28;
        //                    resultItem.d29 = item.D29;
        //                    resultItem.d30 = item.D30;
        //                    resultItem.d31 = item.D31;
        //                    resultItem.report_date = datereport;
        //                    resultItem.warehouse = warehouse;

        //                    if (item.IsCal == 1)
        //                    {
        //                        resultItem.count_Location_sum = item.Count_Location;
        //                        resultItem.d01_sum = item.D01;
        //                        resultItem.d02_sum = item.D02;
        //                        resultItem.d03_sum = item.D03;
        //                        resultItem.d04_sum = item.D04;
        //                        resultItem.d05_sum = item.D05;
        //                        resultItem.d06_sum = item.D06;
        //                        resultItem.d07_sum = item.D07;
        //                        resultItem.d08_sum = item.D08;
        //                        resultItem.d09_sum = item.D09;
        //                        resultItem.d10_sum = item.D10;
        //                        resultItem.d11_sum = item.D11;
        //                        resultItem.d12_sum = item.D12;
        //                        resultItem.d13_sum = item.D13;
        //                        resultItem.d14_sum = item.D14;
        //                        resultItem.d15_sum = item.D15;
        //                        resultItem.d16_sum = item.D16;
        //                        resultItem.d17_sum = item.D17;
        //                        resultItem.d18_sum = item.D18;
        //                        resultItem.d19_sum = item.D19;
        //                        resultItem.d20_sum = item.D20;
        //                        resultItem.d21_sum = item.D21;
        //                        resultItem.d22_sum = item.D22;
        //                        resultItem.d23_sum = item.D23;
        //                        resultItem.d24_sum = item.D24;
        //                        resultItem.d25_sum = item.D25;
        //                        resultItem.d26_sum = item.D26;
        //                        resultItem.d27_sum = item.D27;
        //                        resultItem.d28_sum = item.D28;
        //                        resultItem.d29_sum = item.D29;
        //                        resultItem.d30_sum = item.D30;
        //                        resultItem.d31_sum = item.D31;
        //                    }
        //                    location_list.Add(resultItem);
        //                    num++;
        //                }
        //            }


        //            if (resultquery_owner.Count == 0)
        //            {
        //                var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                resultItem.report_date = datereport;
        //                resultItem.report_date_to = endDate;
        //                resultItem.warehouse = warehouse;
        //                //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        //                owner_list.Add(resultItem);
        //            }
        //            else
        //            {
        //                int num = 0;
        //                foreach (var item in resultquery_owner)
        //                {
        //                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                    resultItem.rowNum = num + 1;
        //                    resultItem.ambientRoom = "Ambient";
        //                    resultItem.owner_Name = item.Owner_Name;
        //                    resultItem.maxValuePeak = item.MaxValuePeak;
        //                    resultItem.d01 = item.D01;
        //                    resultItem.d02 = item.D02;
        //                    resultItem.d03 = item.D03;
        //                    resultItem.d04 = item.D04;
        //                    resultItem.d05 = item.D05;
        //                    resultItem.d06 = item.D06;
        //                    resultItem.d07 = item.D07;
        //                    resultItem.d08 = item.D08;
        //                    resultItem.d09 = item.D09;
        //                    resultItem.d10 = item.D10;
        //                    resultItem.d11 = item.D11;
        //                    resultItem.d12 = item.D12;
        //                    resultItem.d13 = item.D13;
        //                    resultItem.d14 = item.D14;
        //                    resultItem.d15 = item.D15;
        //                    resultItem.d16 = item.D16;
        //                    resultItem.d17 = item.D17;
        //                    resultItem.d18 = item.D18;
        //                    resultItem.d19 = item.D19;
        //                    resultItem.d20 = item.D20;
        //                    resultItem.d21 = item.D21;
        //                    resultItem.d22 = item.D22;
        //                    resultItem.d23 = item.D23;
        //                    resultItem.d24 = item.D24;
        //                    resultItem.d25 = item.D25;
        //                    resultItem.d26 = item.D26;
        //                    resultItem.d27 = item.D27;
        //                    resultItem.d28 = item.D28;
        //                    resultItem.d29 = item.D29;
        //                    resultItem.d30 = item.D30;
        //                    resultItem.d31 = item.D31;
        //                    resultItem.report_date = datereport;
        //                    resultItem.warehouse = warehouse;

        //                    if (item.IsCal == 1)
        //                    {
        //                        resultItem.maxValuePeak_sum = item.MaxValuePeak;
        //                        resultItem.d01_sum = item.D01;
        //                        resultItem.d02_sum = item.D02;
        //                        resultItem.d03_sum = item.D03;
        //                        resultItem.d04_sum = item.D04;
        //                        resultItem.d05_sum = item.D05;
        //                        resultItem.d06_sum = item.D06;
        //                        resultItem.d07_sum = item.D07;
        //                        resultItem.d08_sum = item.D08;
        //                        resultItem.d09_sum = item.D09;
        //                        resultItem.d10_sum = item.D10;
        //                        resultItem.d11_sum = item.D11;
        //                        resultItem.d12_sum = item.D12;
        //                        resultItem.d13_sum = item.D13;
        //                        resultItem.d14_sum = item.D14;
        //                        resultItem.d15_sum = item.D15;
        //                        resultItem.d16_sum = item.D16;
        //                        resultItem.d17_sum = item.D17;
        //                        resultItem.d18_sum = item.D18;
        //                        resultItem.d19_sum = item.D19;
        //                        resultItem.d20_sum = item.D20;
        //                        resultItem.d21_sum = item.D21;
        //                        resultItem.d22_sum = item.D22;
        //                        resultItem.d23_sum = item.D23;
        //                        resultItem.d24_sum = item.D24;
        //                        resultItem.d25_sum = item.D25;
        //                        resultItem.d26_sum = item.D26;
        //                        resultItem.d27_sum = item.D27;
        //                        resultItem.d28_sum = item.D28;
        //                        resultItem.d29_sum = item.D29;
        //                        resultItem.d30_sum = item.D30;
        //                        resultItem.d31_sum = item.D31;
        //                    }

        //                    owner_list.Add(resultItem);
        //                    num++;
        //                }
        //            }

        //            if (resultquery_location_per.Count == 0)
        //            {
        //                var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                resultItem.report_date = datereport;
        //                resultItem.report_date_to = endDate;
        //                resultItem.warehouse = warehouse;
        //                //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        //                location_per_list.Add(resultItem);
        //            }
        //            else
        //            {
        //                int num = 0;
        //                int count = resultquery_location_per.Where( c => c.IsCal == 1).Count();
        //                foreach (var item in resultquery_location_per)
        //                {
        //                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                    resultItem.rowNum = num + 1;
        //                    resultItem.ambientRoom = "Ambient";
        //                    resultItem.locationType_Name = item.LocationType_Name;
        //                    resultItem.maxValuePeak = (item.MaxValuePeak / 100);
        //                    resultItem.d01 = (item.D01 / 100);
        //                    resultItem.d02 = (item.D02 / 100);
        //                    resultItem.d03 = (item.D03 / 100);
        //                    resultItem.d04 = (item.D04 / 100);
        //                    resultItem.d05 = (item.D05 / 100);
        //                    resultItem.d06 = (item.D06 / 100);
        //                    resultItem.d07 = (item.D07 / 100);
        //                    resultItem.d08 = (item.D08 / 100);
        //                    resultItem.d09 = (item.D09 / 100);
        //                    resultItem.d10 = (item.D10 / 100);
        //                    resultItem.d11 = (item.D11 / 100);
        //                    resultItem.d12 = (item.D12 / 100);
        //                    resultItem.d13 = (item.D13 / 100);
        //                    resultItem.d14 = (item.D14 / 100);
        //                    resultItem.d15 = (item.D15 / 100);
        //                    resultItem.d16 = (item.D16 / 100);
        //                    resultItem.d17 = (item.D17 / 100);
        //                    resultItem.d18 = (item.D18 / 100);
        //                    resultItem.d19 = (item.D19 / 100);
        //                    resultItem.d20 = (item.D20 / 100);
        //                    resultItem.d21 = (item.D21 / 100);
        //                    resultItem.d22 = (item.D22 / 100);
        //                    resultItem.d23 = (item.D23 / 100);
        //                    resultItem.d24 = (item.D24 / 100);
        //                    resultItem.d25 = (item.D25 / 100);
        //                    resultItem.d26 = (item.D26 / 100);
        //                    resultItem.d27 = (item.D27 / 100);
        //                    resultItem.d28 = (item.D28 / 100);
        //                    resultItem.d29 = (item.D29 / 100);
        //                    resultItem.d30 = (item.D30 / 100);
        //                    resultItem.d31 = (item.D31 / 100);

        //                    resultItem.d01_per = resultItem.d01 / count;
        //                    resultItem.d02_per = resultItem.d02 / count;
        //                    resultItem.d03_per = resultItem.d03 / count;
        //                    resultItem.d04_per = resultItem.d04 / count;
        //                    resultItem.d05_per = resultItem.d05 / count;
        //                    resultItem.d06_per = resultItem.d06 / count;
        //                    resultItem.d07_per = resultItem.d07 / count;
        //                    resultItem.d08_per = resultItem.d08 / count;
        //                    resultItem.d09_per = resultItem.d09 / count;
        //                    resultItem.d10_per = resultItem.d10 / count;
        //                    resultItem.d11_per = resultItem.d11 / count;
        //                    resultItem.d12_per = resultItem.d12 / count;
        //                    resultItem.d13_per = resultItem.d13 / count;
        //                    resultItem.d14_per = resultItem.d14 / count;
        //                    resultItem.d15_per = resultItem.d15 / count;
        //                    resultItem.d16_per = resultItem.d16 / count;
        //                    resultItem.d17_per = resultItem.d17 / count;
        //                    resultItem.d18_per = resultItem.d18 / count;
        //                    resultItem.d19_per = resultItem.d19 / count;
        //                    resultItem.d20_per = resultItem.d20 / count;
        //                    resultItem.d21_per = resultItem.d21 / count;
        //                    resultItem.d22_per = resultItem.d22 / count;
        //                    resultItem.d23_per = resultItem.d23 / count;
        //                    resultItem.d24_per = resultItem.d24 / count;
        //                    resultItem.d25_per = resultItem.d25 / count;
        //                    resultItem.d26_per = resultItem.d26 / count;
        //                    resultItem.d27_per = resultItem.d27 / count;
        //                    resultItem.d28_per = resultItem.d28 / count;
        //                    resultItem.d29_per = resultItem.d29 / count;
        //                    resultItem.d30_per = resultItem.d30 / count;
        //                    resultItem.d31_per = resultItem.d31 / count;
        //                    resultItem.maxValuePeak_per = resultItem.maxValuePeak / count;

        //                    if (item.IsCal == 1)
        //                    {
        //                        resultItem.maxValuePeak_sum = resultItem.maxValuePeak_per;
        //                        resultItem.d01_sum = resultItem.d01_per;
        //                        resultItem.d02_sum = resultItem.d02_per;
        //                        resultItem.d03_sum = resultItem.d03_per;
        //                        resultItem.d04_sum = resultItem.d04_per;
        //                        resultItem.d05_sum = resultItem.d05_per;
        //                        resultItem.d06_sum = resultItem.d06_per;
        //                        resultItem.d07_sum = resultItem.d07_per;
        //                        resultItem.d08_sum = resultItem.d08_per;
        //                        resultItem.d09_sum = resultItem.d09_per;
        //                        resultItem.d10_sum = resultItem.d10_per;
        //                        resultItem.d11_sum = resultItem.d11_per;
        //                        resultItem.d12_sum = resultItem.d12_per;
        //                        resultItem.d13_sum = resultItem.d13_per;
        //                        resultItem.d14_sum = resultItem.d14_per;
        //                        resultItem.d15_sum = resultItem.d15_per;
        //                        resultItem.d16_sum = resultItem.d16_per;
        //                        resultItem.d17_sum = resultItem.d17_per;
        //                        resultItem.d18_sum = resultItem.d18_per;
        //                        resultItem.d19_sum = resultItem.d19_per;
        //                        resultItem.d20_sum = resultItem.d20_per;
        //                        resultItem.d21_sum = resultItem.d21_per;
        //                        resultItem.d22_sum = resultItem.d22_per;
        //                        resultItem.d23_sum = resultItem.d23_per;
        //                        resultItem.d24_sum = resultItem.d24_per;
        //                        resultItem.d25_sum = resultItem.d25_per;
        //                        resultItem.d26_sum = resultItem.d26_per;
        //                        resultItem.d27_sum = resultItem.d27_per;
        //                        resultItem.d28_sum = resultItem.d28_per;
        //                        resultItem.d29_sum = resultItem.d29_per;
        //                        resultItem.d30_sum = resultItem.d30_per;
        //                        resultItem.d31_sum = resultItem.d31_per;
        //                    }

        //                    resultItem.report_date = datereport;
        //                    resultItem.warehouse = warehouse;
        //                    location_per_list.Add(resultItem);
        //                    num++;
        //                }
        //            }

        //            if (resultquery_owner_per.Count == 0)
        //            {
        //                var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                resultItem.report_date = datereport;
        //                resultItem.report_date_to = endDate;
        //                resultItem.warehouse = warehouse;
        //                //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        //                owner_per_list.Add(resultItem);
        //            }
        //            else
        //            {
        //                int num = 0;
        //                int count = resultquery_owner_per.Where(c => c.IsCal == 1).Count();
        //                foreach (var item in resultquery_owner_per)
        //                {
        //                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                    resultItem.rowNum = num + 1;
        //                    resultItem.ambientRoom = "Ambient";
        //                    resultItem.owner_Name = item.Owner_Name;
        //                    resultItem.maxValuePeak = (item.MaxValuePeak / 100);
        //                    resultItem.d01 = (item.D01 / 100);
        //                    resultItem.d02 = (item.D02 / 100);
        //                    resultItem.d03 = (item.D03 / 100);
        //                    resultItem.d04 = (item.D04 / 100);
        //                    resultItem.d05 = (item.D05 / 100);
        //                    resultItem.d06 = (item.D06 / 100);
        //                    resultItem.d07 = (item.D07 / 100);
        //                    resultItem.d08 = (item.D08 / 100);
        //                    resultItem.d09 = (item.D09 / 100);
        //                    resultItem.d10 = (item.D10 / 100);
        //                    resultItem.d11 = (item.D11 / 100);
        //                    resultItem.d12 = (item.D12 / 100);
        //                    resultItem.d13 = (item.D13 / 100);
        //                    resultItem.d14 = (item.D14 / 100);
        //                    resultItem.d15 = (item.D15 / 100);
        //                    resultItem.d16 = (item.D16 / 100);
        //                    resultItem.d17 = (item.D17 / 100);
        //                    resultItem.d18 = (item.D18 / 100);
        //                    resultItem.d19 = (item.D19 / 100);
        //                    resultItem.d20 = (item.D20 / 100);
        //                    resultItem.d21 = (item.D21 / 100);
        //                    resultItem.d22 = (item.D22 / 100);
        //                    resultItem.d23 = (item.D23 / 100);
        //                    resultItem.d24 = (item.D24 / 100);
        //                    resultItem.d25 = (item.D25 / 100);
        //                    resultItem.d26 = (item.D26 / 100);
        //                    resultItem.d27 = (item.D27 / 100);
        //                    resultItem.d28 = (item.D28 / 100);
        //                    resultItem.d29 = (item.D29 / 100);
        //                    resultItem.d30 = (item.D30 / 100);
        //                    resultItem.d31 = (item.D31 / 100);

        //                    resultItem.d01_per = resultItem.d01 / count;
        //                    resultItem.d02_per = resultItem.d02 / count;
        //                    resultItem.d03_per = resultItem.d03 / count;
        //                    resultItem.d04_per = resultItem.d04 / count;
        //                    resultItem.d05_per = resultItem.d05 / count;
        //                    resultItem.d06_per = resultItem.d06 / count;
        //                    resultItem.d07_per = resultItem.d07 / count;
        //                    resultItem.d08_per = resultItem.d08 / count;
        //                    resultItem.d09_per = resultItem.d09 / count;
        //                    resultItem.d10_per = resultItem.d10 / count;
        //                    resultItem.d11_per = resultItem.d11 / count;
        //                    resultItem.d12_per = resultItem.d12 / count;
        //                    resultItem.d13_per = resultItem.d13 / count;
        //                    resultItem.d14_per = resultItem.d14 / count;
        //                    resultItem.d15_per = resultItem.d15 / count;
        //                    resultItem.d16_per = resultItem.d16 / count;
        //                    resultItem.d17_per = resultItem.d17 / count;
        //                    resultItem.d18_per = resultItem.d18 / count;
        //                    resultItem.d19_per = resultItem.d19 / count;
        //                    resultItem.d20_per = resultItem.d20 / count;
        //                    resultItem.d21_per = resultItem.d21 / count;
        //                    resultItem.d22_per = resultItem.d22 / count;
        //                    resultItem.d23_per = resultItem.d23 / count;
        //                    resultItem.d24_per = resultItem.d24 / count;
        //                    resultItem.d25_per = resultItem.d25 / count;
        //                    resultItem.d26_per = resultItem.d26 / count;
        //                    resultItem.d27_per = resultItem.d27 / count;
        //                    resultItem.d28_per = resultItem.d28 / count;
        //                    resultItem.d29_per = resultItem.d29 / count;
        //                    resultItem.d30_per = resultItem.d30 / count;
        //                    resultItem.d31_per = resultItem.d31 / count;
        //                    resultItem.maxValuePeak_per = resultItem.maxValuePeak / count;
        //                    resultItem.report_date = datereport;
        //                    resultItem.warehouse = warehouse;

        //                    if (item.IsCal == 1)
        //                    {
        //                        resultItem.maxValuePeak_sum = resultItem.maxValuePeak_per;
        //                        resultItem.d01_sum = resultItem.d01_per;
        //                        resultItem.d02_sum = resultItem.d02_per;
        //                        resultItem.d03_sum = resultItem.d03_per;
        //                        resultItem.d04_sum = resultItem.d04_per;
        //                        resultItem.d05_sum = resultItem.d05_per;
        //                        resultItem.d06_sum = resultItem.d06_per;
        //                        resultItem.d07_sum = resultItem.d07_per;
        //                        resultItem.d08_sum = resultItem.d08_per;
        //                        resultItem.d09_sum = resultItem.d09_per;
        //                        resultItem.d10_sum = resultItem.d10_per;
        //                        resultItem.d11_sum = resultItem.d11_per;
        //                        resultItem.d12_sum = resultItem.d12_per;
        //                        resultItem.d13_sum = resultItem.d13_per;
        //                        resultItem.d14_sum = resultItem.d14_per;
        //                        resultItem.d15_sum = resultItem.d15_per;
        //                        resultItem.d16_sum = resultItem.d16_per;
        //                        resultItem.d17_sum = resultItem.d17_per;
        //                        resultItem.d18_sum = resultItem.d18_per;
        //                        resultItem.d19_sum = resultItem.d19_per;
        //                        resultItem.d20_sum = resultItem.d20_per;
        //                        resultItem.d21_sum = resultItem.d21_per;
        //                        resultItem.d22_sum = resultItem.d22_per;
        //                        resultItem.d23_sum = resultItem.d23_per;
        //                        resultItem.d24_sum = resultItem.d24_per;
        //                        resultItem.d25_sum = resultItem.d25_per;
        //                        resultItem.d26_sum = resultItem.d26_per;
        //                        resultItem.d27_sum = resultItem.d27_per;
        //                        resultItem.d28_sum = resultItem.d28_per;
        //                        resultItem.d29_sum = resultItem.d29_per;
        //                        resultItem.d30_sum = resultItem.d30_per;
        //                        resultItem.d31_sum = resultItem.d31_per;
        //                    }

        //                    owner_per_list.Add(resultItem);
        //                    num++;
        //                }
        //            }

        //            result.location_type = location_list;
        //            result.owner = owner_list;
        //            result.location_type_per = location_per_list;
        //            result.owner_per = owner_per_list;

        //            /////// Freeze /////
        //            resultquery_location = temp_Master_DBContext.sp_rpt_15_Space_Utilization.FromSql(query_location).ToList();
        //            resultquery_owner = temp_Master_DBContext.sp_rpt_15_Space_Utilization_Owner.FromSql(query_owner).ToList();
        //            resultquery_location_per = temp_Master_DBContext.sp_rpt_15_Space_Utilization_Percent.FromSql(query_location_per).ToList();
        //            resultquery_owner_per = temp_Master_DBContext.sp_rpt_15_Space_Utilization_Percent_Owner.FromSql(query_owner_per).ToList();

        //            if (resultquery_location.Count == 0)
        //            {
        //                var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                resultItem.report_date = datereport;
        //                resultItem.report_date_to = endDate;
        //                resultItem.warehouse = warehouse;
        //                //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        //                location_list_all.Add(resultItem);
        //            }
        //            else
        //            {

        //                int num = 0;
        //                foreach (var item in resultquery_location)
        //                {
        //                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                    resultItem.rowNum = num + 1;
        //                    resultItem.ambientRoom = "Freeze";
        //                    resultItem.locationType_Name = item.LocationType_Name;
        //                    resultItem.count_Location = item.Count_Location;
        //                    resultItem.d01 = item.D01;
        //                    resultItem.d02 = item.D02;
        //                    resultItem.d03 = item.D03;
        //                    resultItem.d04 = item.D04;
        //                    resultItem.d05 = item.D05;
        //                    resultItem.d06 = item.D06;
        //                    resultItem.d07 = item.D07;
        //                    resultItem.d08 = item.D08;
        //                    resultItem.d09 = item.D09;
        //                    resultItem.d10 = item.D10;
        //                    resultItem.d11 = item.D11;
        //                    resultItem.d12 = item.D12;
        //                    resultItem.d13 = item.D13;
        //                    resultItem.d14 = item.D14;
        //                    resultItem.d15 = item.D15;
        //                    resultItem.d16 = item.D16;
        //                    resultItem.d17 = item.D17;
        //                    resultItem.d18 = item.D18;
        //                    resultItem.d19 = item.D19;
        //                    resultItem.d20 = item.D20;
        //                    resultItem.d21 = item.D21;
        //                    resultItem.d22 = item.D22;
        //                    resultItem.d23 = item.D23;
        //                    resultItem.d24 = item.D24;
        //                    resultItem.d25 = item.D25;
        //                    resultItem.d26 = item.D26;
        //                    resultItem.d27 = item.D27;
        //                    resultItem.d28 = item.D28;
        //                    resultItem.d29 = item.D29;
        //                    resultItem.d30 = item.D30;
        //                    resultItem.d31 = item.D31;
        //                    resultItem.report_date = datereport;
        //                    resultItem.warehouse = warehouse;

        //                    if (item.IsCal == 1)
        //                    {
        //                        resultItem.count_Location_sum = item.Count_Location;
        //                        resultItem.d01_sum = item.D01;
        //                        resultItem.d02_sum = item.D02;
        //                        resultItem.d03_sum = item.D03;
        //                        resultItem.d04_sum = item.D04;
        //                        resultItem.d05_sum = item.D05;
        //                        resultItem.d06_sum = item.D06;
        //                        resultItem.d07_sum = item.D07;
        //                        resultItem.d08_sum = item.D08;
        //                        resultItem.d09_sum = item.D09;
        //                        resultItem.d10_sum = item.D10;
        //                        resultItem.d11_sum = item.D11;
        //                        resultItem.d12_sum = item.D12;
        //                        resultItem.d13_sum = item.D13;
        //                        resultItem.d14_sum = item.D14;
        //                        resultItem.d15_sum = item.D15;
        //                        resultItem.d16_sum = item.D16;
        //                        resultItem.d17_sum = item.D17;
        //                        resultItem.d18_sum = item.D18;
        //                        resultItem.d19_sum = item.D19;
        //                        resultItem.d20_sum = item.D20;
        //                        resultItem.d21_sum = item.D21;
        //                        resultItem.d22_sum = item.D22;
        //                        resultItem.d23_sum = item.D23;
        //                        resultItem.d24_sum = item.D24;
        //                        resultItem.d25_sum = item.D25;
        //                        resultItem.d26_sum = item.D26;
        //                        resultItem.d27_sum = item.D27;
        //                        resultItem.d28_sum = item.D28;
        //                        resultItem.d29_sum = item.D29;
        //                        resultItem.d30_sum = item.D30;
        //                        resultItem.d31_sum = item.D31;
        //                    }

        //                    location_list_all.Add(resultItem);
        //                    num++;
        //                }
        //            }


        //            if (resultquery_owner.Count == 0)
        //            {
        //                var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                resultItem.report_date = datereport;
        //                resultItem.report_date_to = endDate;
        //                resultItem.warehouse = warehouse;
        //                //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        //                owner_list_all.Add(resultItem);
        //            }
        //            else
        //            {
        //                int num = 0;
        //                foreach (var item in resultquery_owner)
        //                {
        //                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                    resultItem.rowNum = num + 1;
        //                    resultItem.ambientRoom = "Freeze";
        //                    resultItem.owner_Name = item.Owner_Name;
        //                    resultItem.maxValuePeak = item.MaxValuePeak;
        //                    resultItem.d01 = item.D01;
        //                    resultItem.d02 = item.D02;
        //                    resultItem.d03 = item.D03;
        //                    resultItem.d04 = item.D04;
        //                    resultItem.d05 = item.D05;
        //                    resultItem.d06 = item.D06;
        //                    resultItem.d07 = item.D07;
        //                    resultItem.d08 = item.D08;
        //                    resultItem.d09 = item.D09;
        //                    resultItem.d10 = item.D10;
        //                    resultItem.d11 = item.D11;
        //                    resultItem.d12 = item.D12;
        //                    resultItem.d13 = item.D13;
        //                    resultItem.d14 = item.D14;
        //                    resultItem.d15 = item.D15;
        //                    resultItem.d16 = item.D16;
        //                    resultItem.d17 = item.D17;
        //                    resultItem.d18 = item.D18;
        //                    resultItem.d19 = item.D19;
        //                    resultItem.d20 = item.D20;
        //                    resultItem.d21 = item.D21;
        //                    resultItem.d22 = item.D22;
        //                    resultItem.d23 = item.D23;
        //                    resultItem.d24 = item.D24;
        //                    resultItem.d25 = item.D25;
        //                    resultItem.d26 = item.D26;
        //                    resultItem.d27 = item.D27;
        //                    resultItem.d28 = item.D28;
        //                    resultItem.d29 = item.D29;
        //                    resultItem.d30 = item.D30;
        //                    resultItem.d31 = item.D31;
        //                    resultItem.report_date = datereport;
        //                    resultItem.warehouse = warehouse;

        //                    if (item.IsCal == 1)
        //                    {
        //                        resultItem.maxValuePeak_sum = item.MaxValuePeak;
        //                        resultItem.d01_sum = item.D01;
        //                        resultItem.d02_sum = item.D02;
        //                        resultItem.d03_sum = item.D03;
        //                        resultItem.d04_sum = item.D04;
        //                        resultItem.d05_sum = item.D05;
        //                        resultItem.d06_sum = item.D06;
        //                        resultItem.d07_sum = item.D07;
        //                        resultItem.d08_sum = item.D08;
        //                        resultItem.d09_sum = item.D09;
        //                        resultItem.d10_sum = item.D10;
        //                        resultItem.d11_sum = item.D11;
        //                        resultItem.d12_sum = item.D12;
        //                        resultItem.d13_sum = item.D13;
        //                        resultItem.d14_sum = item.D14;
        //                        resultItem.d15_sum = item.D15;
        //                        resultItem.d16_sum = item.D16;
        //                        resultItem.d17_sum = item.D17;
        //                        resultItem.d18_sum = item.D18;
        //                        resultItem.d19_sum = item.D19;
        //                        resultItem.d20_sum = item.D20;
        //                        resultItem.d21_sum = item.D21;
        //                        resultItem.d22_sum = item.D22;
        //                        resultItem.d23_sum = item.D23;
        //                        resultItem.d24_sum = item.D24;
        //                        resultItem.d25_sum = item.D25;
        //                        resultItem.d26_sum = item.D26;
        //                        resultItem.d27_sum = item.D27;
        //                        resultItem.d28_sum = item.D28;
        //                        resultItem.d29_sum = item.D29;
        //                        resultItem.d30_sum = item.D30;
        //                        resultItem.d31_sum = item.D31;
        //                    }
        //                    owner_list_all.Add(resultItem);
        //                    num++;
        //                }
        //            }

        //            if (resultquery_location_per.Count == 0)
        //            {
        //                var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                resultItem.report_date = datereport;
        //                resultItem.report_date_to = endDate;
        //                resultItem.warehouse = warehouse;
        //                //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        //                location_per_list_all.Add(resultItem);
        //            }
        //            else
        //            {
        //                int num = 0;
        //                int count = resultquery_location_per.Where(c => c.IsCal == 1).Count();
        //                foreach (var item in resultquery_location_per)
        //                {
        //                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                    resultItem.rowNum = num + 1;
        //                    resultItem.ambientRoom = "Freeze";
        //                    resultItem.locationType_Name = item.LocationType_Name;
        //                    resultItem.maxValuePeak = (item.MaxValuePeak / 100);
        //                    resultItem.d01 = (item.D01 / 100);
        //                    resultItem.d02 = (item.D02 / 100);
        //                    resultItem.d03 = (item.D03 / 100);
        //                    resultItem.d04 = (item.D04 / 100);
        //                    resultItem.d05 = (item.D05 / 100);
        //                    resultItem.d06 = (item.D06 / 100);
        //                    resultItem.d07 = (item.D07 / 100);
        //                    resultItem.d08 = (item.D08 / 100);
        //                    resultItem.d09 = (item.D09 / 100);
        //                    resultItem.d10 = (item.D10 / 100);
        //                    resultItem.d11 = (item.D11 / 100);
        //                    resultItem.d12 = (item.D12 / 100);
        //                    resultItem.d13 = (item.D13 / 100);
        //                    resultItem.d14 = (item.D14 / 100);
        //                    resultItem.d15 = (item.D15 / 100);
        //                    resultItem.d16 = (item.D16 / 100);
        //                    resultItem.d17 = (item.D17 / 100);
        //                    resultItem.d18 = (item.D18 / 100);
        //                    resultItem.d19 = (item.D19 / 100);
        //                    resultItem.d20 = (item.D20 / 100);
        //                    resultItem.d21 = (item.D21 / 100);
        //                    resultItem.d22 = (item.D22 / 100);
        //                    resultItem.d23 = (item.D23 / 100);
        //                    resultItem.d24 = (item.D24 / 100);
        //                    resultItem.d25 = (item.D25 / 100);
        //                    resultItem.d26 = (item.D26 / 100);
        //                    resultItem.d27 = (item.D27 / 100);
        //                    resultItem.d28 = (item.D28 / 100);
        //                    resultItem.d29 = (item.D29 / 100);
        //                    resultItem.d30 = (item.D30 / 100);
        //                    resultItem.d31 = (item.D31 / 100);

        //                    resultItem.d01_per = resultItem.d01 / count;
        //                    resultItem.d02_per = resultItem.d02 / count;
        //                    resultItem.d03_per = resultItem.d03 / count;
        //                    resultItem.d04_per = resultItem.d04 / count;
        //                    resultItem.d05_per = resultItem.d05 / count;
        //                    resultItem.d06_per = resultItem.d06 / count;
        //                    resultItem.d07_per = resultItem.d07 / count;
        //                    resultItem.d08_per = resultItem.d08 / count;
        //                    resultItem.d09_per = resultItem.d09 / count;
        //                    resultItem.d10_per = resultItem.d10 / count;
        //                    resultItem.d11_per = resultItem.d11 / count;
        //                    resultItem.d12_per = resultItem.d12 / count;
        //                    resultItem.d13_per = resultItem.d13 / count;
        //                    resultItem.d14_per = resultItem.d14 / count;
        //                    resultItem.d15_per = resultItem.d15 / count;
        //                    resultItem.d16_per = resultItem.d16 / count;
        //                    resultItem.d17_per = resultItem.d17 / count;
        //                    resultItem.d18_per = resultItem.d18 / count;
        //                    resultItem.d19_per = resultItem.d19 / count;
        //                    resultItem.d20_per = resultItem.d20 / count;
        //                    resultItem.d21_per = resultItem.d21 / count;
        //                    resultItem.d22_per = resultItem.d22 / count;
        //                    resultItem.d23_per = resultItem.d23 / count;
        //                    resultItem.d24_per = resultItem.d24 / count;
        //                    resultItem.d25_per = resultItem.d25 / count;
        //                    resultItem.d26_per = resultItem.d26 / count;
        //                    resultItem.d27_per = resultItem.d27 / count;
        //                    resultItem.d28_per = resultItem.d28 / count;
        //                    resultItem.d29_per = resultItem.d29 / count;
        //                    resultItem.d30_per = resultItem.d30 / count;
        //                    resultItem.d31_per = resultItem.d31 / count;
        //                    resultItem.maxValuePeak_per = resultItem.maxValuePeak / count;

        //                    if (item.IsCal == 1)
        //                    {
        //                        resultItem.maxValuePeak_sum = resultItem.maxValuePeak_per;
        //                        resultItem.d01_sum = resultItem.d01_per;
        //                        resultItem.d02_sum = resultItem.d02_per;
        //                        resultItem.d03_sum = resultItem.d03_per;
        //                        resultItem.d04_sum = resultItem.d04_per;
        //                        resultItem.d05_sum = resultItem.d05_per;
        //                        resultItem.d06_sum = resultItem.d06_per;
        //                        resultItem.d07_sum = resultItem.d07_per;
        //                        resultItem.d08_sum = resultItem.d08_per;
        //                        resultItem.d09_sum = resultItem.d09_per;
        //                        resultItem.d10_sum = resultItem.d10_per;
        //                        resultItem.d11_sum = resultItem.d11_per;
        //                        resultItem.d12_sum = resultItem.d12_per;
        //                        resultItem.d13_sum = resultItem.d13_per;
        //                        resultItem.d14_sum = resultItem.d14_per;
        //                        resultItem.d15_sum = resultItem.d15_per;
        //                        resultItem.d16_sum = resultItem.d16_per;
        //                        resultItem.d17_sum = resultItem.d17_per;
        //                        resultItem.d18_sum = resultItem.d18_per;
        //                        resultItem.d19_sum = resultItem.d19_per;
        //                        resultItem.d20_sum = resultItem.d20_per;
        //                        resultItem.d21_sum = resultItem.d21_per;
        //                        resultItem.d22_sum = resultItem.d22_per;
        //                        resultItem.d23_sum = resultItem.d23_per;
        //                        resultItem.d24_sum = resultItem.d24_per;
        //                        resultItem.d25_sum = resultItem.d25_per;
        //                        resultItem.d26_sum = resultItem.d26_per;
        //                        resultItem.d27_sum = resultItem.d27_per;
        //                        resultItem.d28_sum = resultItem.d28_per;
        //                        resultItem.d29_sum = resultItem.d29_per;
        //                        resultItem.d30_sum = resultItem.d30_per;
        //                        resultItem.d31_sum = resultItem.d31_per;
        //                    }

        //                    resultItem.report_date = datereport;
        //                    resultItem.warehouse = warehouse;
        //                    location_per_list_all.Add(resultItem);
        //                    num++;
        //                }
        //            }

        //            if (resultquery_owner_per.Count == 0)
        //            {
        //                var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                resultItem.report_date = datereport;
        //                resultItem.report_date_to = endDate;
        //                resultItem.warehouse = warehouse;
        //                //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        //                owner_per_list_all.Add(resultItem);
        //            }
        //            else
        //            {
        //                int num = 0;
        //                int count = resultquery_owner_per.Where(c => c.IsCal == 1).Count();
        //                foreach (var item in resultquery_owner_per)
        //                {
        //                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                    resultItem.rowNum = num + 1;
        //                    resultItem.ambientRoom = "Freeze";
        //                    resultItem.owner_Name = item.Owner_Name;
        //                    resultItem.maxValuePeak = (item.MaxValuePeak / 100);
        //                    resultItem.d01 = (item.D01 / 100);
        //                    resultItem.d02 = (item.D02 / 100);
        //                    resultItem.d03 = (item.D03 / 100);
        //                    resultItem.d04 = (item.D04 / 100);
        //                    resultItem.d05 = (item.D05 / 100);
        //                    resultItem.d06 = (item.D06 / 100);
        //                    resultItem.d07 = (item.D07 / 100);
        //                    resultItem.d08 = (item.D08 / 100);
        //                    resultItem.d09 = (item.D09 / 100);
        //                    resultItem.d10 = (item.D10 / 100);
        //                    resultItem.d11 = (item.D11 / 100);
        //                    resultItem.d12 = (item.D12 / 100);
        //                    resultItem.d13 = (item.D13 / 100);
        //                    resultItem.d14 = (item.D14 / 100);
        //                    resultItem.d15 = (item.D15 / 100);
        //                    resultItem.d16 = (item.D16 / 100);
        //                    resultItem.d17 = (item.D17 / 100);
        //                    resultItem.d18 = (item.D18 / 100);
        //                    resultItem.d19 = (item.D19 / 100);
        //                    resultItem.d20 = (item.D20 / 100);
        //                    resultItem.d21 = (item.D21 / 100);
        //                    resultItem.d22 = (item.D22 / 100);
        //                    resultItem.d23 = (item.D23 / 100);
        //                    resultItem.d24 = (item.D24 / 100);
        //                    resultItem.d25 = (item.D25 / 100);
        //                    resultItem.d26 = (item.D26 / 100);
        //                    resultItem.d27 = (item.D27 / 100);
        //                    resultItem.d28 = (item.D28 / 100);
        //                    resultItem.d29 = (item.D29 / 100);
        //                    resultItem.d30 = (item.D30 / 100);
        //                    resultItem.d31 = (item.D31 / 100);

        //                    resultItem.d01_per = resultItem.d01 / count;
        //                    resultItem.d02_per = resultItem.d02 / count;
        //                    resultItem.d03_per = resultItem.d03 / count;
        //                    resultItem.d04_per = resultItem.d04 / count;
        //                    resultItem.d05_per = resultItem.d05 / count;
        //                    resultItem.d06_per = resultItem.d06 / count;
        //                    resultItem.d07_per = resultItem.d07 / count;
        //                    resultItem.d08_per = resultItem.d08 / count;
        //                    resultItem.d09_per = resultItem.d09 / count;
        //                    resultItem.d10_per = resultItem.d10 / count;
        //                    resultItem.d11_per = resultItem.d11 / count;
        //                    resultItem.d12_per = resultItem.d12 / count;
        //                    resultItem.d13_per = resultItem.d13 / count;
        //                    resultItem.d14_per = resultItem.d14 / count;
        //                    resultItem.d15_per = resultItem.d15 / count;
        //                    resultItem.d16_per = resultItem.d16 / count;
        //                    resultItem.d17_per = resultItem.d17 / count;
        //                    resultItem.d18_per = resultItem.d18 / count;
        //                    resultItem.d19_per = resultItem.d19 / count;
        //                    resultItem.d20_per = resultItem.d20 / count;
        //                    resultItem.d21_per = resultItem.d21 / count;
        //                    resultItem.d22_per = resultItem.d22 / count;
        //                    resultItem.d23_per = resultItem.d23 / count;
        //                    resultItem.d24_per = resultItem.d24 / count;
        //                    resultItem.d25_per = resultItem.d25 / count;
        //                    resultItem.d26_per = resultItem.d26 / count;
        //                    resultItem.d27_per = resultItem.d27 / count;
        //                    resultItem.d28_per = resultItem.d28 / count;
        //                    resultItem.d29_per = resultItem.d29 / count;
        //                    resultItem.d30_per = resultItem.d30 / count;
        //                    resultItem.d31_per = resultItem.d31 / count;
        //                    resultItem.maxValuePeak_per = resultItem.maxValuePeak / count;
        //                    resultItem.report_date = datereport;
        //                    resultItem.warehouse = warehouse;

        //                    if (item.IsCal == 1)
        //                    {
        //                        resultItem.maxValuePeak_sum = resultItem.maxValuePeak_per;
        //                        resultItem.d01_sum = resultItem.d01_per;
        //                        resultItem.d02_sum = resultItem.d02_per;
        //                        resultItem.d03_sum = resultItem.d03_per;
        //                        resultItem.d04_sum = resultItem.d04_per;
        //                        resultItem.d05_sum = resultItem.d05_per;
        //                        resultItem.d06_sum = resultItem.d06_per;
        //                        resultItem.d07_sum = resultItem.d07_per;
        //                        resultItem.d08_sum = resultItem.d08_per;
        //                        resultItem.d09_sum = resultItem.d09_per;
        //                        resultItem.d10_sum = resultItem.d10_per;
        //                        resultItem.d11_sum = resultItem.d11_per;
        //                        resultItem.d12_sum = resultItem.d12_per;
        //                        resultItem.d13_sum = resultItem.d13_per;
        //                        resultItem.d14_sum = resultItem.d14_per;
        //                        resultItem.d15_sum = resultItem.d15_per;
        //                        resultItem.d16_sum = resultItem.d16_per;
        //                        resultItem.d17_sum = resultItem.d17_per;
        //                        resultItem.d18_sum = resultItem.d18_per;
        //                        resultItem.d19_sum = resultItem.d19_per;
        //                        resultItem.d20_sum = resultItem.d20_per;
        //                        resultItem.d21_sum = resultItem.d21_per;
        //                        resultItem.d22_sum = resultItem.d22_per;
        //                        resultItem.d23_sum = resultItem.d23_per;
        //                        resultItem.d24_sum = resultItem.d24_per;
        //                        resultItem.d25_sum = resultItem.d25_per;
        //                        resultItem.d26_sum = resultItem.d26_per;
        //                        resultItem.d27_sum = resultItem.d27_per;
        //                        resultItem.d28_sum = resultItem.d28_per;
        //                        resultItem.d29_sum = resultItem.d29_per;
        //                        resultItem.d30_sum = resultItem.d30_per;
        //                        resultItem.d31_sum = resultItem.d31_per;
        //                    }
        //                    owner_per_list_all.Add(resultItem);
        //                    num++;
        //                }
        //            }

        //            result.location_type_all = location_list_all;
        //            result.owner_all = owner_list_all;
        //            result.location_type_per_all = location_per_list_all;
        //            result.owner_per_all = owner_per_list_all;
        //            ///

        //        }

        //        if (data.ambientRoom == "01" || data.ambientRoom == "02")
        //        {

        //            if (resultquery_location.Count == 0)
        //            {
        //                var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                resultItem.report_date = datereport;
        //                resultItem.report_date_to = endDate;
        //                resultItem.warehouse = warehouse;
        //                //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        //                location_list.Add(resultItem);
        //            }
        //            else
        //            {

        //                int num = 0;
        //                foreach (var item in resultquery_location)
        //                {
        //                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                    resultItem.rowNum = num + 1;
        //                    if (data.ambientRoom != "02")
        //                    {
        //                        resultItem.ambientRoom = "Ambient";
        //                    }
        //                    else
        //                    {
        //                        resultItem.ambientRoom = "Freeze";
        //                    }
        //                    resultItem.locationType_Name = item.LocationType_Name;
        //                    resultItem.count_Location = item.Count_Location;
        //                    resultItem.d01 = item.D01;
        //                    resultItem.d02 = item.D02;
        //                    resultItem.d03 = item.D03;
        //                    resultItem.d04 = item.D04;
        //                    resultItem.d05 = item.D05;
        //                    resultItem.d06 = item.D06;
        //                    resultItem.d07 = item.D07;
        //                    resultItem.d08 = item.D08;
        //                    resultItem.d09 = item.D09;
        //                    resultItem.d10 = item.D10;
        //                    resultItem.d11 = item.D11;
        //                    resultItem.d12 = item.D12;
        //                    resultItem.d13 = item.D13;
        //                    resultItem.d14 = item.D14;
        //                    resultItem.d15 = item.D15;
        //                    resultItem.d16 = item.D16;
        //                    resultItem.d17 = item.D17;
        //                    resultItem.d18 = item.D18;
        //                    resultItem.d19 = item.D19;
        //                    resultItem.d20 = item.D20;
        //                    resultItem.d21 = item.D21;
        //                    resultItem.d22 = item.D22;
        //                    resultItem.d23 = item.D23;
        //                    resultItem.d24 = item.D24;
        //                    resultItem.d25 = item.D25;
        //                    resultItem.d26 = item.D26;
        //                    resultItem.d27 = item.D27;
        //                    resultItem.d28 = item.D28;
        //                    resultItem.d29 = item.D29;
        //                    resultItem.d30 = item.D30;
        //                    resultItem.d31 = item.D31;
        //                    resultItem.report_date = datereport;
        //                    resultItem.warehouse = warehouse;

        //                    if (item.IsCal == 1)
        //                    {
        //                        resultItem.count_Location_sum = item.Count_Location;
        //                        resultItem.d01_sum = item.D01;
        //                        resultItem.d02_sum = item.D02;
        //                        resultItem.d03_sum = item.D03;
        //                        resultItem.d04_sum = item.D04;
        //                        resultItem.d05_sum = item.D05;
        //                        resultItem.d06_sum = item.D06;
        //                        resultItem.d07_sum = item.D07;
        //                        resultItem.d08_sum = item.D08;
        //                        resultItem.d09_sum = item.D09;
        //                        resultItem.d10_sum = item.D10;
        //                        resultItem.d11_sum = item.D11;
        //                        resultItem.d12_sum = item.D12;
        //                        resultItem.d13_sum = item.D13;
        //                        resultItem.d14_sum = item.D14;
        //                        resultItem.d15_sum = item.D15;
        //                        resultItem.d16_sum = item.D16;
        //                        resultItem.d17_sum = item.D17;
        //                        resultItem.d18_sum = item.D18;
        //                        resultItem.d19_sum = item.D19;
        //                        resultItem.d20_sum = item.D20;
        //                        resultItem.d21_sum = item.D21;
        //                        resultItem.d22_sum = item.D22;
        //                        resultItem.d23_sum = item.D23;
        //                        resultItem.d24_sum = item.D24;
        //                        resultItem.d25_sum = item.D25;
        //                        resultItem.d26_sum = item.D26;
        //                        resultItem.d27_sum = item.D27;
        //                        resultItem.d28_sum = item.D28;
        //                        resultItem.d29_sum = item.D29;
        //                        resultItem.d30_sum = item.D30;
        //                        resultItem.d31_sum = item.D31;
        //                    }
        //                    location_list.Add(resultItem);
        //                    num++;
        //                }
        //            }


        //            if (resultquery_owner.Count == 0)
        //            {
        //                var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                resultItem.report_date = datereport;
        //                resultItem.report_date_to = endDate;
        //                resultItem.warehouse = warehouse;
        //                //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        //                owner_list.Add(resultItem);
        //            }
        //            else
        //            {
        //                int num = 0;
        //                foreach (var item in resultquery_owner)
        //                {
        //                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                    resultItem.rowNum = num + 1;
        //                    if (data.ambientRoom != "02")
        //                    {
        //                        resultItem.ambientRoom = "Ambient";
        //                    }
        //                    else
        //                    {
        //                        resultItem.ambientRoom = "Freeze";
        //                    }
        //                    resultItem.owner_Name = item.Owner_Name;
        //                    resultItem.maxValuePeak = item.MaxValuePeak;
        //                    resultItem.d01 = item.D01;
        //                    resultItem.d02 = item.D02;
        //                    resultItem.d03 = item.D03;
        //                    resultItem.d04 = item.D04;
        //                    resultItem.d05 = item.D05;
        //                    resultItem.d06 = item.D06;
        //                    resultItem.d07 = item.D07;
        //                    resultItem.d08 = item.D08;
        //                    resultItem.d09 = item.D09;
        //                    resultItem.d10 = item.D10;
        //                    resultItem.d11 = item.D11;
        //                    resultItem.d12 = item.D12;
        //                    resultItem.d13 = item.D13;
        //                    resultItem.d14 = item.D14;
        //                    resultItem.d15 = item.D15;
        //                    resultItem.d16 = item.D16;
        //                    resultItem.d17 = item.D17;
        //                    resultItem.d18 = item.D18;
        //                    resultItem.d19 = item.D19;
        //                    resultItem.d20 = item.D20;
        //                    resultItem.d21 = item.D21;
        //                    resultItem.d22 = item.D22;
        //                    resultItem.d23 = item.D23;
        //                    resultItem.d24 = item.D24;
        //                    resultItem.d25 = item.D25;
        //                    resultItem.d26 = item.D26;
        //                    resultItem.d27 = item.D27;
        //                    resultItem.d28 = item.D28;
        //                    resultItem.d29 = item.D29;
        //                    resultItem.d30 = item.D30;
        //                    resultItem.d31 = item.D31;
        //                    resultItem.report_date = datereport;
        //                    resultItem.warehouse = warehouse;

        //                    if (item.IsCal == 1)
        //                    {
        //                        resultItem.maxValuePeak_sum = item.MaxValuePeak;
        //                        resultItem.d01_sum = item.D01;
        //                        resultItem.d02_sum = item.D02;
        //                        resultItem.d03_sum = item.D03;
        //                        resultItem.d04_sum = item.D04;
        //                        resultItem.d05_sum = item.D05;
        //                        resultItem.d06_sum = item.D06;
        //                        resultItem.d07_sum = item.D07;
        //                        resultItem.d08_sum = item.D08;
        //                        resultItem.d09_sum = item.D09;
        //                        resultItem.d10_sum = item.D10;
        //                        resultItem.d11_sum = item.D11;
        //                        resultItem.d12_sum = item.D12;
        //                        resultItem.d13_sum = item.D13;
        //                        resultItem.d14_sum = item.D14;
        //                        resultItem.d15_sum = item.D15;
        //                        resultItem.d16_sum = item.D16;
        //                        resultItem.d17_sum = item.D17;
        //                        resultItem.d18_sum = item.D18;
        //                        resultItem.d19_sum = item.D19;
        //                        resultItem.d20_sum = item.D20;
        //                        resultItem.d21_sum = item.D21;
        //                        resultItem.d22_sum = item.D22;
        //                        resultItem.d23_sum = item.D23;
        //                        resultItem.d24_sum = item.D24;
        //                        resultItem.d25_sum = item.D25;
        //                        resultItem.d26_sum = item.D26;
        //                        resultItem.d27_sum = item.D27;
        //                        resultItem.d28_sum = item.D28;
        //                        resultItem.d29_sum = item.D29;
        //                        resultItem.d30_sum = item.D30;
        //                        resultItem.d31_sum = item.D31;
        //                    }
        //                    owner_list.Add(resultItem);
        //                    num++;
        //                }
        //            }

        //            if (resultquery_location_per.Count == 0)
        //            {
        //                var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                resultItem.report_date = datereport;
        //                resultItem.report_date_to = endDate;
        //                resultItem.warehouse = warehouse;
        //                //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        //                location_per_list.Add(resultItem);
        //            }
        //            else
        //            {
        //                int num = 0;
        //                int count = resultquery_location_per.Where( c => c.IsCal == 1).Count();
        //                foreach (var item in resultquery_location_per)
        //                {
        //                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                    resultItem.rowNum = num + 1;
        //                    if (data.ambientRoom != "02")
        //                    {
        //                        resultItem.ambientRoom = "Ambient";
        //                    }
        //                    else
        //                    {
        //                        resultItem.ambientRoom = "Freeze";
        //                    }
        //                    resultItem.locationType_Name = item.LocationType_Name;
        //                    resultItem.maxValuePeak = (item.MaxValuePeak / 100);
        //                    resultItem.d01 = (item.D01 / 100);
        //                    resultItem.d02 = (item.D02 / 100);
        //                    resultItem.d03 = (item.D03 / 100);
        //                    resultItem.d04 = (item.D04 / 100);
        //                    resultItem.d05 = (item.D05 / 100);
        //                    resultItem.d06 = (item.D06 / 100);
        //                    resultItem.d07 = (item.D07 / 100);
        //                    resultItem.d08 = (item.D08 / 100);
        //                    resultItem.d09 = (item.D09 / 100);
        //                    resultItem.d10 = (item.D10 / 100);
        //                    resultItem.d11 = (item.D11 / 100);
        //                    resultItem.d12 = (item.D12 / 100);
        //                    resultItem.d13 = (item.D13 / 100);
        //                    resultItem.d14 = (item.D14 / 100);
        //                    resultItem.d15 = (item.D15 / 100);
        //                    resultItem.d16 = (item.D16 / 100);
        //                    resultItem.d17 = (item.D17 / 100);
        //                    resultItem.d18 = (item.D18 / 100);
        //                    resultItem.d19 = (item.D19 / 100);
        //                    resultItem.d20 = (item.D20 / 100);
        //                    resultItem.d21 = (item.D21 / 100);
        //                    resultItem.d22 = (item.D22 / 100);
        //                    resultItem.d23 = (item.D23 / 100);
        //                    resultItem.d24 = (item.D24 / 100);
        //                    resultItem.d25 = (item.D25 / 100);
        //                    resultItem.d26 = (item.D26 / 100);
        //                    resultItem.d27 = (item.D27 / 100);
        //                    resultItem.d28 = (item.D28 / 100);
        //                    resultItem.d29 = (item.D29 / 100);
        //                    resultItem.d30 = (item.D30 / 100);
        //                    resultItem.d31 = (item.D31 / 100);

        //                    resultItem.d01_per = resultItem.d01 / count;
        //                    resultItem.d02_per = resultItem.d02 / count;
        //                    resultItem.d03_per = resultItem.d03 / count;
        //                    resultItem.d04_per = resultItem.d04 / count;
        //                    resultItem.d05_per = resultItem.d05 / count;
        //                    resultItem.d06_per = resultItem.d06 / count;
        //                    resultItem.d07_per = resultItem.d07 / count;
        //                    resultItem.d08_per = resultItem.d08 / count;
        //                    resultItem.d09_per = resultItem.d09 / count;
        //                    resultItem.d10_per = resultItem.d10 / count;
        //                    resultItem.d11_per = resultItem.d11 / count;
        //                    resultItem.d12_per = resultItem.d12 / count;
        //                    resultItem.d13_per = resultItem.d13 / count;
        //                    resultItem.d14_per = resultItem.d14 / count;
        //                    resultItem.d15_per = resultItem.d15 / count;
        //                    resultItem.d16_per = resultItem.d16 / count;
        //                    resultItem.d17_per = resultItem.d17 / count;
        //                    resultItem.d18_per = resultItem.d18 / count;
        //                    resultItem.d19_per = resultItem.d19 / count;
        //                    resultItem.d20_per = resultItem.d20 / count;
        //                    resultItem.d21_per = resultItem.d21 / count;
        //                    resultItem.d22_per = resultItem.d22 / count;
        //                    resultItem.d23_per = resultItem.d23 / count;
        //                    resultItem.d24_per = resultItem.d24 / count;
        //                    resultItem.d25_per = resultItem.d25 / count;
        //                    resultItem.d26_per = resultItem.d26 / count;
        //                    resultItem.d27_per = resultItem.d27 / count;
        //                    resultItem.d28_per = resultItem.d28 / count;
        //                    resultItem.d29_per = resultItem.d29 / count;
        //                    resultItem.d30_per = resultItem.d30 / count;
        //                    resultItem.d31_per = resultItem.d31 / count;
        //                    resultItem.maxValuePeak_per = resultItem.maxValuePeak / count;

        //                    resultItem.report_date = datereport;
        //                    resultItem.warehouse = warehouse;

        //                    if (item.IsCal == 1)
        //                    {
        //                        resultItem.maxValuePeak_sum = resultItem.maxValuePeak_per;
        //                        resultItem.d01_sum = resultItem.d01_per;
        //                        resultItem.d02_sum = resultItem.d02_per;
        //                        resultItem.d03_sum = resultItem.d03_per;
        //                        resultItem.d04_sum = resultItem.d04_per;
        //                        resultItem.d05_sum = resultItem.d05_per;
        //                        resultItem.d06_sum = resultItem.d06_per;
        //                        resultItem.d07_sum = resultItem.d07_per;
        //                        resultItem.d08_sum = resultItem.d08_per;
        //                        resultItem.d09_sum = resultItem.d09_per;
        //                        resultItem.d10_sum = resultItem.d10_per;
        //                        resultItem.d11_sum = resultItem.d11_per;
        //                        resultItem.d12_sum = resultItem.d12_per;
        //                        resultItem.d13_sum = resultItem.d13_per;
        //                        resultItem.d14_sum = resultItem.d14_per;
        //                        resultItem.d15_sum = resultItem.d15_per;
        //                        resultItem.d16_sum = resultItem.d16_per;
        //                        resultItem.d17_sum = resultItem.d17_per;
        //                        resultItem.d18_sum = resultItem.d18_per;
        //                        resultItem.d19_sum = resultItem.d19_per;
        //                        resultItem.d20_sum = resultItem.d20_per;
        //                        resultItem.d21_sum = resultItem.d21_per;
        //                        resultItem.d22_sum = resultItem.d22_per;
        //                        resultItem.d23_sum = resultItem.d23_per;
        //                        resultItem.d24_sum = resultItem.d24_per;
        //                        resultItem.d25_sum = resultItem.d25_per;
        //                        resultItem.d26_sum = resultItem.d26_per;
        //                        resultItem.d27_sum = resultItem.d27_per;
        //                        resultItem.d28_sum = resultItem.d28_per;
        //                        resultItem.d29_sum = resultItem.d29_per;
        //                        resultItem.d30_sum = resultItem.d30_per;
        //                        resultItem.d31_sum = resultItem.d31_per;
        //                    }

        //                    location_per_list.Add(resultItem);
        //                    num++;
        //                }
        //            }

        //            if (resultquery_owner_per.Count == 0)
        //            {
        //                var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                resultItem.report_date = datereport;
        //                resultItem.report_date_to = endDate;
        //                resultItem.warehouse = warehouse;
        //                //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        //                owner_per_list.Add(resultItem);
        //            }
        //            else
        //            {
        //                int num = 0;
        //                int count = resultquery_owner_per.Where(c => c.IsCal == 1).Count();
        //                foreach (var item in resultquery_owner_per)
        //                {
        //                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                    var resultItem = new ReportSumSpaceUtilizationViewModel();
        //                    resultItem.rowNum = num + 1;
        //                    if (data.ambientRoom != "02")
        //                    {
        //                        resultItem.ambientRoom = "Ambient";
        //                    }
        //                    else
        //                    {
        //                        resultItem.ambientRoom = "Freeze";
        //                    }
        //                    resultItem.owner_Name = item.Owner_Name;
        //                    resultItem.maxValuePeak = (item.MaxValuePeak / 100);
        //                    resultItem.d01 = (item.D01 / 100);
        //                    resultItem.d02 = (item.D02 / 100);
        //                    resultItem.d03 = (item.D03 / 100);
        //                    resultItem.d04 = (item.D04 / 100);
        //                    resultItem.d05 = (item.D05 / 100);
        //                    resultItem.d06 = (item.D06 / 100);
        //                    resultItem.d07 = (item.D07 / 100);
        //                    resultItem.d08 = (item.D08 / 100);
        //                    resultItem.d09 = (item.D09 / 100);
        //                    resultItem.d10 = (item.D10 / 100);
        //                    resultItem.d11 = (item.D11 / 100);
        //                    resultItem.d12 = (item.D12 / 100);
        //                    resultItem.d13 = (item.D13 / 100);
        //                    resultItem.d14 = (item.D14 / 100);
        //                    resultItem.d15 = (item.D15 / 100);
        //                    resultItem.d16 = (item.D16 / 100);
        //                    resultItem.d17 = (item.D17 / 100);
        //                    resultItem.d18 = (item.D18 / 100);
        //                    resultItem.d19 = (item.D19 / 100);
        //                    resultItem.d20 = (item.D20 / 100);
        //                    resultItem.d21 = (item.D21 / 100);
        //                    resultItem.d22 = (item.D22 / 100);
        //                    resultItem.d23 = (item.D23 / 100);
        //                    resultItem.d24 = (item.D24 / 100);
        //                    resultItem.d25 = (item.D25 / 100);
        //                    resultItem.d26 = (item.D26 / 100);
        //                    resultItem.d27 = (item.D27 / 100);
        //                    resultItem.d28 = (item.D28 / 100);
        //                    resultItem.d29 = (item.D29 / 100);
        //                    resultItem.d30 = (item.D30 / 100);
        //                    resultItem.d31 = (item.D31 / 100);

        //                    resultItem.d01_per = resultItem.d01 / count;
        //                    resultItem.d02_per = resultItem.d02 / count;
        //                    resultItem.d03_per = resultItem.d03 / count;
        //                    resultItem.d04_per = resultItem.d04 / count;
        //                    resultItem.d05_per = resultItem.d05 / count;
        //                    resultItem.d06_per = resultItem.d06 / count;
        //                    resultItem.d07_per = resultItem.d07 / count;
        //                    resultItem.d08_per = resultItem.d08 / count;
        //                    resultItem.d09_per = resultItem.d09 / count;
        //                    resultItem.d10_per = resultItem.d10 / count;
        //                    resultItem.d11_per = resultItem.d11 / count;
        //                    resultItem.d12_per = resultItem.d12 / count;
        //                    resultItem.d13_per = resultItem.d13 / count;
        //                    resultItem.d14_per = resultItem.d14 / count;
        //                    resultItem.d15_per = resultItem.d15 / count;
        //                    resultItem.d16_per = resultItem.d16 / count;
        //                    resultItem.d17_per = resultItem.d17 / count;
        //                    resultItem.d18_per = resultItem.d18 / count;
        //                    resultItem.d19_per = resultItem.d19 / count;
        //                    resultItem.d20_per = resultItem.d20 / count;
        //                    resultItem.d21_per = resultItem.d21 / count;
        //                    resultItem.d22_per = resultItem.d22 / count;
        //                    resultItem.d23_per = resultItem.d23 / count;
        //                    resultItem.d24_per = resultItem.d24 / count;
        //                    resultItem.d25_per = resultItem.d25 / count;
        //                    resultItem.d26_per = resultItem.d26 / count;
        //                    resultItem.d27_per = resultItem.d27 / count;
        //                    resultItem.d28_per = resultItem.d28 / count;
        //                    resultItem.d29_per = resultItem.d29 / count;
        //                    resultItem.d30_per = resultItem.d30 / count;
        //                    resultItem.d31_per = resultItem.d31 / count;
        //                    resultItem.maxValuePeak_per = resultItem.maxValuePeak / count;
        //                    resultItem.report_date = datereport;
        //                    resultItem.warehouse = warehouse;

        //                    if (item.IsCal == 1)
        //                    {
        //                        resultItem.maxValuePeak_sum = resultItem.maxValuePeak_per;
        //                        resultItem.d01_sum = resultItem.d01_per;
        //                        resultItem.d02_sum = resultItem.d02_per;
        //                        resultItem.d03_sum = resultItem.d03_per;
        //                        resultItem.d04_sum = resultItem.d04_per;
        //                        resultItem.d05_sum = resultItem.d05_per;
        //                        resultItem.d06_sum = resultItem.d06_per;
        //                        resultItem.d07_sum = resultItem.d07_per;
        //                        resultItem.d08_sum = resultItem.d08_per;
        //                        resultItem.d09_sum = resultItem.d09_per;
        //                        resultItem.d10_sum = resultItem.d10_per;
        //                        resultItem.d11_sum = resultItem.d11_per;
        //                        resultItem.d12_sum = resultItem.d12_per;
        //                        resultItem.d13_sum = resultItem.d13_per;
        //                        resultItem.d14_sum = resultItem.d14_per;
        //                        resultItem.d15_sum = resultItem.d15_per;
        //                        resultItem.d16_sum = resultItem.d16_per;
        //                        resultItem.d17_sum = resultItem.d17_per;
        //                        resultItem.d18_sum = resultItem.d18_per;
        //                        resultItem.d19_sum = resultItem.d19_per;
        //                        resultItem.d20_sum = resultItem.d20_per;
        //                        resultItem.d21_sum = resultItem.d21_per;
        //                        resultItem.d22_sum = resultItem.d22_per;
        //                        resultItem.d23_sum = resultItem.d23_per;
        //                        resultItem.d24_sum = resultItem.d24_per;
        //                        resultItem.d25_sum = resultItem.d25_per;
        //                        resultItem.d26_sum = resultItem.d26_per;
        //                        resultItem.d27_sum = resultItem.d27_per;
        //                        resultItem.d28_sum = resultItem.d28_per;
        //                        resultItem.d29_sum = resultItem.d29_per;
        //                        resultItem.d30_sum = resultItem.d30_per;
        //                        resultItem.d31_sum = resultItem.d31_per;
        //                    }

        //                    owner_per_list.Add(resultItem);
        //                    num++;
        //                }
        //            }

        //            result.location_type = location_list;
        //            result.owner = owner_list;
        //            result.location_type_per = location_per_list;
        //            result.owner_per = owner_per_list;                    
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
              
        //        throw ex;
        //    }
        //}

        public ReportResultSumSpaceUtilizationViewModel Getdata(ReportSumSpaceUtilizationViewModel data)
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            //String State = "Start";
            //String msglog = "";
            //var olog = new logtxt();
            var result = new ReportResultSumSpaceUtilizationViewModel();
            var location_list = new List<ReportSumSpaceUtilizationViewModel>();
            var owner_list = new List<ReportSumSpaceUtilizationViewModel>();
            var location_per_list = new List<ReportSumSpaceUtilizationViewModel>();
            var owner_per_list = new List<ReportSumSpaceUtilizationViewModel>();
            var location_list_all = new List<ReportSumSpaceUtilizationViewModel>();
            var owner_list_all = new List<ReportSumSpaceUtilizationViewModel>();
            var location_per_list_all = new List<ReportSumSpaceUtilizationViewModel>();
            var owner_per_list_all = new List<ReportSumSpaceUtilizationViewModel>();
            var locationtotal_list = new List<ReportSumSpaceUtilizationViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                string datereport = "";
                string locationType_Name = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var day_of_month = "";
                var date_From = "";
                var date_To = "";
                var check_month = "ReportSumSpaceUtilization";
                var warehouse = "";

                //if (!string.IsNullOrEmpty(data.month) && data.year != null)
                //{
                //    day_of_month = DateTime.DaysInMonth(data.year, Convert.ToInt32(data.month)).ToString();
                //    date_From = data.year.ToString() + "-" + data.month + "-" + "01";
                //    date_To = data.year.ToString() + "-" + data.month + "-" + day_of_month;

                //    string fullMonthName = new DateTime(data.year, Convert.ToInt32(data.month), 1).ToString("MMMM");
                //    datereport = "Monthly  : " + fullMonthName + "  " + data.year;

                //    if (day_of_month == "28")
                //    {
                //        check_month = "ReportSumSpaceUtilization_28";
                //    }
                //    else if (day_of_month == "29")
                //    {
                //        check_month = "ReportSumSpaceUtilization_29";
                //    }
                //    else if (day_of_month == "30")
                //    {
                //        check_month = "ReportSumSpaceUtilization_30";
                //    }
                //}

                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    date_From = data.report_date;
                    date_To = data.report_date_to;
                    //datereport = "Date  : ";

                    var sd_string = data.report_date.Substring(6, 2) + "/" + data.report_date.Substring(4, 2) + "/" + data.report_date.Substring(0, 4);
                    var ed_string = data.report_date_to.Substring(6, 2) + "/" + data.report_date_to.Substring(4, 2) + "/" + data.report_date_to.Substring(0, 4);
                    datereport = "Date From : " + sd_string + " To  " + ed_string;
                }

                //if (!string.IsNullOrEmpty(data.month) && data.year != null)
                //{
                //    day_of_month = DateTime.DaysInMonth(data.year, Convert.ToInt32(data.month)).ToString();
                //    date_From = data.year.ToString() + "-" + data.month + "-" + "01";
                //    date_To = data.year.ToString() + "-" + data.month + "-" + day_of_month;

                //    string fullMonthName = new DateTime(data.year, Convert.ToInt32(data.month), 1).ToString("MMMM");
                //    datereport = "Date  : ";

                //    //if (day_of_month == "28")
                //    //{
                //    //    check_month = "ReportSumSpaceUtilization_28";
                //    //}
                //    //else if (day_of_month == "29")
                //    //{
                //    //    check_month = "ReportSumSpaceUtilization_29";
                //    //}
                //    //else if (day_of_month == "30")
                //    //{
                //    //    check_month = "ReportSumSpaceUtilization_30";
                //    //}
                //}


                if (data.locationTypeList != null)
                {
                    locationType_Name = data.locationTypeList.locationType_Name.ToString();
                }


                var statusModels = new List<int?>();
                var query_location = $@"EXEC sp_rpt_15_Space_Utilization_All '{locationType_Name}','{date_From}','{date_To}'";
                var query_owner = $@"EXEC sp_rpt_15_Space_Utilization_Owner_All '{locationType_Name}','{date_From}','{date_To}'";
              


                var resultquery_location = new List<MasterDataDataAccess.Models.sp_rpt_15_Space_Utilization_All>();
                var resultquery_owner = new List<MasterDataDataAccess.Models.sp_rpt_15_Space_Utilization_Owner_All>();
                


                if (data.ambientRoom == "01")
                {
                    warehouse = "Ambient Total";
                    resultquery_location = Master_DBContext.sp_rpt_15_Space_Utilization_All.FromSql(query_location).ToList();
                    resultquery_owner = Master_DBContext.sp_rpt_15_Space_Utilization_Owner_All.FromSql(query_owner).ToList();
                   

                }
                else if (data.ambientRoom == "02")
                {
                    warehouse = "Freeze Total";
                    resultquery_location = temp_Master_DBContext.sp_rpt_15_Space_Utilization_All.FromSql(query_location).ToList();
                    resultquery_owner = temp_Master_DBContext.sp_rpt_15_Space_Utilization_Owner_All.FromSql(query_owner).ToList();
                   
                }
                else
                {
                    resultquery_location = Master_DBContext.sp_rpt_15_Space_Utilization_All.FromSql(query_location).ToList();
                    resultquery_owner = Master_DBContext.sp_rpt_15_Space_Utilization_Owner_All.FromSql(query_owner).ToList();


                    if (resultquery_location.Count == 0)
                    {
                        var resultItem = new ReportSumSpaceUtilizationViewModel();
                        var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.report_date = datereport;
                        resultItem.report_date_to = endDate;
                        resultItem.warehouse = warehouse;
                        location_list.Add(resultItem);
                    }
                    else
                    {

                        int num = 1;
                        foreach (var item in resultquery_location)
                        {
                            var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                            var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                            var resultItem = new ReportSumSpaceUtilizationViewModel();
                            resultItem.rowNum = num;
                            if (item.LocationType_Name == "ASRS")
                            {
                                resultItem.rowNum_text = (num++).ToString();
                            }
                            else if (item.LocationType_Name == "ASRS125")
                            {
                                resultItem.rowNum_text = "1.1";
                            }
                            else if (item.LocationType_Name == "ASRS145")
                            {
                                resultItem.rowNum_text = "1.2";
                            }
                            else if (item.LocationType_Name == "ASRS165")
                            {
                                resultItem.rowNum_text = "1.3";
                            }
                            else
                            {
                                resultItem.rowNum_text = (num++).ToString();
                            }
                            resultItem.ambientRoom = "Ambient";
                            resultItem.locationType_Name = item.LocationType_Name;
                            resultItem.count_Location = item.Count_Location;
                            resultItem.maxValue_ = item.Count_Location > 0 ? Convert.ToInt32(item.Count_Location).ToString("#,###") : "-";
                            resultItem.d01_ = item.D01 > 0 ? Convert.ToInt32(item.D01).ToString("#,###") : "-";
                            resultItem.d02_ = item.D02 > 0 ? Convert.ToInt32(item.D02).ToString("#,###") : "-";
                            resultItem.d03_ = item.D03 > 0 ? Convert.ToInt32(item.D03).ToString("#,###") : "-";
                            resultItem.d04_ = item.D04 > 0 ? Convert.ToInt32(item.D04).ToString("#,###") : "-";
                            resultItem.d05_ = item.D05 > 0 ? Convert.ToInt32(item.D05).ToString("#,###") : "-";
                            resultItem.d06_ = item.D06 > 0 ? Convert.ToInt32(item.D06).ToString("#,###") : "-";
                            resultItem.d07_ = item.D07 > 0 ? Convert.ToInt32(item.D07).ToString("#,###") : "-";
                            resultItem.d08_ = item.D08 > 0 ? Convert.ToInt32(item.D08).ToString("#,###") : "-";
                            resultItem.d09_ = item.D09 > 0 ? Convert.ToInt32(item.D09).ToString("#,###") : "-";
                            resultItem.d10_ = item.D10 > 0 ? Convert.ToInt32(item.D10).ToString("#,###") : "-";
                            resultItem.d11_ = item.D11 > 0 ? Convert.ToInt32(item.D11).ToString("#,###") : "-";
                            resultItem.d12_ = item.D12 > 0 ? Convert.ToInt32(item.D12).ToString("#,###") : "-";
                            resultItem.d13_ = item.D13 > 0 ? Convert.ToInt32(item.D13).ToString("#,###") : "-";
                            resultItem.d14_ = item.D14 > 0 ? Convert.ToInt32(item.D14).ToString("#,###") : "-";
                            resultItem.d15_ = item.D15 > 0 ? Convert.ToInt32(item.D15).ToString("#,###") : "-";
                            resultItem.d16_ = item.D16 > 0 ? Convert.ToInt32(item.D16).ToString("#,###") : "-";
                            resultItem.d17_ = item.D17 > 0 ? Convert.ToInt32(item.D17).ToString("#,###") : "-";
                            resultItem.d18_ = item.D18 > 0 ? Convert.ToInt32(item.D18).ToString("#,###") : "-";
                            resultItem.d19_ = item.D19 > 0 ? Convert.ToInt32(item.D19).ToString("#,###") : "-";
                            resultItem.d20_ = item.D20 > 0 ? Convert.ToInt32(item.D20).ToString("#,###") : "-";
                            resultItem.d21_ = item.D21 > 0 ? Convert.ToInt32(item.D21).ToString("#,###") : "-";
                            resultItem.d22_ = item.D22 > 0 ? Convert.ToInt32(item.D22).ToString("#,###") : "-";
                            resultItem.d23_ = item.D23 > 0 ? Convert.ToInt32(item.D23).ToString("#,###") : "-";
                            resultItem.d24_ = item.D24 > 0 ? Convert.ToInt32(item.D24).ToString("#,###") : "-";
                            resultItem.d25_ = item.D25 > 0 ? Convert.ToInt32(item.D25).ToString("#,###") : "-";
                            resultItem.d26_ = item.D26 > 0 ? Convert.ToInt32(item.D26).ToString("#,###") : "-";
                            resultItem.d27_ = item.D27 > 0 ? Convert.ToInt32(item.D27).ToString("#,###") : "-";
                            resultItem.d28_ = item.D28 > 0 ? Convert.ToInt32(item.D28).ToString("#,###") : "-";
                            resultItem.d29_ = item.D29 > 0 ? Convert.ToInt32(item.D29).ToString("#,###") : "-";
                            resultItem.d30_ = item.D30 > 0 ? Convert.ToInt32(item.D30).ToString("#,###") : "-";
                            resultItem.d31_ = item.D31 > 0 ? Convert.ToInt32(item.D31).ToString("#,###") : "-";
                            resultItem.report_date = datereport;
                            resultItem.warehouse = warehouse;
                            location_list.Add(resultItem);

                            /////percent
                            var resultItem_P = new ReportSumSpaceUtilizationViewModel();
                            resultItem_P.maxValue_ = Convert.ToDecimal(item.MaxValuePeak).ToString("#,##0.##") + "%";
                            resultItem_P.d01_ = Convert.ToDecimal(item.D01_P).ToString("#,##0.##") + "%";
                            resultItem_P.d02_ = Convert.ToDecimal(item.D02_P).ToString("#,##0.##") + "%";
                            resultItem_P.d03_ = Convert.ToDecimal(item.D03_P).ToString("#,##0.##") + "%";
                            resultItem_P.d04_ = Convert.ToDecimal(item.D04_P).ToString("#,##0.##") + "%";
                            resultItem_P.d05_ = Convert.ToDecimal(item.D05_P).ToString("#,##0.##") + "%";
                            resultItem_P.d06_ = Convert.ToDecimal(item.D06_P).ToString("#,##0.##") + "%";
                            resultItem_P.d07_ = Convert.ToDecimal(item.D07_P).ToString("#,##0.##") + "%";
                            resultItem_P.d08_ = Convert.ToDecimal(item.D08_P).ToString("#,##0.##") + "%";
                            resultItem_P.d09_ = Convert.ToDecimal(item.D09_P).ToString("#,##0.##") + "%";
                            resultItem_P.d10_ = Convert.ToDecimal(item.D10_P).ToString("#,##0.##") + "%";
                            resultItem_P.d11_ = Convert.ToDecimal(item.D11_P).ToString("#,##0.##") + "%";
                            resultItem_P.d12_ = Convert.ToDecimal(item.D12_P).ToString("#,##0.##") + "%";
                            resultItem_P.d13_ = Convert.ToDecimal(item.D13_P).ToString("#,##0.##") + "%";
                            resultItem_P.d14_ = Convert.ToDecimal(item.D14_P).ToString("#,##0.##") + "%";
                            resultItem_P.d15_ = Convert.ToDecimal(item.D15_P).ToString("#,##0.##") + "%";
                            resultItem_P.d16_ = Convert.ToDecimal(item.D16_P).ToString("#,##0.##") + "%";
                            resultItem_P.d17_ = Convert.ToDecimal(item.D17_P).ToString("#,##0.##") + "%";
                            resultItem_P.d18_ = Convert.ToDecimal(item.D18_P).ToString("#,##0.##") + "%";
                            resultItem_P.d19_ = Convert.ToDecimal(item.D19_P).ToString("#,##0.##") + "%";
                            resultItem_P.d20_ = Convert.ToDecimal(item.D20_P).ToString("#,##0.##") + "%";
                            resultItem_P.d21_ = Convert.ToDecimal(item.D21_P).ToString("#,##0.##") + "%";
                            resultItem_P.d22_ = Convert.ToDecimal(item.D22_P).ToString("#,##0.##") + "%";
                            resultItem_P.d23_ = Convert.ToDecimal(item.D23_P).ToString("#,##0.##") + "%";
                            resultItem_P.d24_ = Convert.ToDecimal(item.D24_P).ToString("#,##0.##") + "%";
                            resultItem_P.d25_ = Convert.ToDecimal(item.D25_P).ToString("#,##0.##") + "%";
                            resultItem_P.d26_ = Convert.ToDecimal(item.D26_P).ToString("#,##0.##") + "%";
                            resultItem_P.d27_ = Convert.ToDecimal(item.D27_P).ToString("#,##0.##") + "%";
                            resultItem_P.d28_ = Convert.ToDecimal(item.D28_P).ToString("#,##0.##") + "%";
                            resultItem_P.d29_ = Convert.ToDecimal(item.D29_P).ToString("#,##0.##") + "%";
                            resultItem_P.d30_ = Convert.ToDecimal(item.D30_P).ToString("#,##0.##") + "%";
                            resultItem_P.d31_ = Convert.ToDecimal(item.D31_P).ToString("#,##0.##") + "%";
                            location_list.Add(resultItem_P);

                        }

                        var count = resultquery_location.Where(c => c.IsCal == 1).Count();
                        if (count > 0)
                        {
                            ////total
                            var resultItem2 = new ReportSumSpaceUtilizationViewModel();
                            resultItem2.locationType_Name = "TOTAL";
                            resultItem2.maxValue_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.Count_Location) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.Count_Location)).ToString("#,###") : "-";
                            resultItem2.d01_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D01) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D01)).ToString("#,###") : "-";
                            resultItem2.d02_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D02) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D02)).ToString("#,###") : "-";
                            resultItem2.d03_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D03) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D03)).ToString("#,###") : "-";
                            resultItem2.d04_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D04) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D04)).ToString("#,###") : "-";
                            resultItem2.d05_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D05) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D05)).ToString("#,###") : "-";
                            resultItem2.d06_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D06) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D06)).ToString("#,###") : "-";
                            resultItem2.d07_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D07) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D07)).ToString("#,###") : "-";
                            resultItem2.d08_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D08) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D08)).ToString("#,###") : "-";
                            resultItem2.d09_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D09) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D09)).ToString("#,###") : "-";
                            resultItem2.d10_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D10) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D10)).ToString("#,###") : "-";
                            resultItem2.d11_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D11) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D11)).ToString("#,###") : "-";
                            resultItem2.d12_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D12) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D12)).ToString("#,###") : "-";
                            resultItem2.d13_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D13) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D13)).ToString("#,###") : "-";
                            resultItem2.d14_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D14) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D14)).ToString("#,###") : "-";
                            resultItem2.d15_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D15) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D15)).ToString("#,###") : "-";
                            resultItem2.d16_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D16) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D16)).ToString("#,###") : "-";
                            resultItem2.d17_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D17) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D17)).ToString("#,###") : "-";
                            resultItem2.d18_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D18) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D18)).ToString("#,###") : "-";
                            resultItem2.d19_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D19) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D19)).ToString("#,###") : "-";
                            resultItem2.d20_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D20) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D20)).ToString("#,###") : "-";
                            resultItem2.d21_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D21) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D21)).ToString("#,###") : "-";
                            resultItem2.d22_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D22) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D22)).ToString("#,###") : "-";
                            resultItem2.d23_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D23) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D23)).ToString("#,###") : "-";
                            resultItem2.d24_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D24) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D24)).ToString("#,###") : "-";
                            resultItem2.d25_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D25) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D25)).ToString("#,###") : "-";
                            resultItem2.d26_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D26) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D26)).ToString("#,###") : "-";
                            resultItem2.d27_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D27) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D27)).ToString("#,###") : "-";
                            resultItem2.d28_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D28) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D28)).ToString("#,###") : "-";
                            resultItem2.d29_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D29) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D29)).ToString("#,###") : "-";
                            resultItem2.d30_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D30) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D30)).ToString("#,###") : "-";
                            resultItem2.d31_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D31) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D31)).ToString("#,###") : "-";
                            location_list.Add(resultItem2);



                            var resultItem3 = new ReportSumSpaceUtilizationViewModel();
                            resultItem3.maxValue_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.MaxValuePeak) / count).ToString("#,##0.##") + "%";
                            resultItem3.d01_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D01_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d02_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D02_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d03_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D03_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d04_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D04_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d05_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D05_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d06_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D06_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d07_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D07_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d08_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D08_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d09_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D09_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d10_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D10_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d11_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D11_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d12_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D12_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d13_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D13_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d14_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D14_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d15_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D15_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d16_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D16_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d17_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D17_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d18_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D18_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d19_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D19_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d20_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D20_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d21_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D21_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d22_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D22_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d23_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D23_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d24_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D24_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d25_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D25_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d26_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D26_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d27_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D27_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d28_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D28_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d29_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D29_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d30_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D30_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d31_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D31_P) / count).ToString("#,##0.##") + "%";
                            location_list.Add(resultItem3);
                        }
                        else
                        {
                            ////total
                            var resultItem2 = new ReportSumSpaceUtilizationViewModel();
                            resultItem2.locationType_Name = "TOTAL";
                            resultItem2.maxValue_ = resultquery_location.Sum(a => a.Count_Location) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.Count_Location)).ToString("#,###") : "-";
                            resultItem2.d01_ = resultquery_location.Sum(a => a.D01) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D01)).ToString("#,###") : "-";
                            resultItem2.d02_ = resultquery_location.Sum(a => a.D02) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D02)).ToString("#,###") : "-";
                            resultItem2.d03_ = resultquery_location.Sum(a => a.D03) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D03)).ToString("#,###") : "-";
                            resultItem2.d04_ = resultquery_location.Sum(a => a.D04) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D04)).ToString("#,###") : "-";
                            resultItem2.d05_ = resultquery_location.Sum(a => a.D05) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D05)).ToString("#,###") : "-";
                            resultItem2.d06_ = resultquery_location.Sum(a => a.D06) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D06)).ToString("#,###") : "-";
                            resultItem2.d07_ = resultquery_location.Sum(a => a.D07) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D07)).ToString("#,###") : "-";
                            resultItem2.d08_ = resultquery_location.Sum(a => a.D08) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D08)).ToString("#,###") : "-";
                            resultItem2.d09_ = resultquery_location.Sum(a => a.D09) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D09)).ToString("#,###") : "-";
                            resultItem2.d10_ = resultquery_location.Sum(a => a.D10) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D10)).ToString("#,###") : "-";
                            resultItem2.d11_ = resultquery_location.Sum(a => a.D11) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D11)).ToString("#,###") : "-";
                            resultItem2.d12_ = resultquery_location.Sum(a => a.D12) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D12)).ToString("#,###") : "-";
                            resultItem2.d13_ = resultquery_location.Sum(a => a.D13) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D13)).ToString("#,###") : "-";
                            resultItem2.d14_ = resultquery_location.Sum(a => a.D14) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D14)).ToString("#,###") : "-";
                            resultItem2.d15_ = resultquery_location.Sum(a => a.D15) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D15)).ToString("#,###") : "-";
                            resultItem2.d16_ = resultquery_location.Sum(a => a.D16) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D16)).ToString("#,###") : "-";
                            resultItem2.d17_ = resultquery_location.Sum(a => a.D17) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D17)).ToString("#,###") : "-";
                            resultItem2.d18_ = resultquery_location.Sum(a => a.D18) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D18)).ToString("#,###") : "-";
                            resultItem2.d19_ = resultquery_location.Sum(a => a.D19) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D19)).ToString("#,###") : "-";
                            resultItem2.d20_ = resultquery_location.Sum(a => a.D20) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D20)).ToString("#,###") : "-";
                            resultItem2.d21_ = resultquery_location.Sum(a => a.D21) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D21)).ToString("#,###") : "-";
                            resultItem2.d22_ = resultquery_location.Sum(a => a.D22) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D22)).ToString("#,###") : "-";
                            resultItem2.d23_ = resultquery_location.Sum(a => a.D23) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D23)).ToString("#,###") : "-";
                            resultItem2.d24_ = resultquery_location.Sum(a => a.D24) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D24)).ToString("#,###") : "-";
                            resultItem2.d25_ = resultquery_location.Sum(a => a.D25) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D25)).ToString("#,###") : "-";
                            resultItem2.d26_ = resultquery_location.Sum(a => a.D26) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D26)).ToString("#,###") : "-";
                            resultItem2.d27_ = resultquery_location.Sum(a => a.D27) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D27)).ToString("#,###") : "-";
                            resultItem2.d28_ = resultquery_location.Sum(a => a.D28) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D28)).ToString("#,###") : "-";
                            resultItem2.d29_ = resultquery_location.Sum(a => a.D29) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D29)).ToString("#,###") : "-";
                            resultItem2.d30_ = resultquery_location.Sum(a => a.D30) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D30)).ToString("#,###") : "-";
                            resultItem2.d31_ = resultquery_location.Sum(a => a.D31) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D31)).ToString("#,###") : "-";
                            location_list.Add(resultItem2);


                            var resultItem3 = new ReportSumSpaceUtilizationViewModel();
                            resultItem3.maxValue_ = Convert.ToDecimal(resultquery_location.Sum(a => a.MaxValuePeak)).ToString("#,##0.##") + "%";
                            resultItem3.d01_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D01_P)).ToString("#,##0.##") + "%";
                            resultItem3.d02_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D02_P)).ToString("#,##0.##") + "%";
                            resultItem3.d03_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D03_P)).ToString("#,##0.##") + "%";
                            resultItem3.d04_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D04_P)).ToString("#,##0.##") + "%";
                            resultItem3.d05_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D05_P)).ToString("#,##0.##") + "%";
                            resultItem3.d06_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D06_P)).ToString("#,##0.##") + "%";
                            resultItem3.d07_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D07_P)).ToString("#,##0.##") + "%";
                            resultItem3.d08_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D08_P)).ToString("#,##0.##") + "%";
                            resultItem3.d09_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D09_P)).ToString("#,##0.##") + "%";
                            resultItem3.d10_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D10_P)).ToString("#,##0.##") + "%";
                            resultItem3.d11_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D11_P)).ToString("#,##0.##") + "%";
                            resultItem3.d12_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D12_P)).ToString("#,##0.##") + "%";
                            resultItem3.d13_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D13_P)).ToString("#,##0.##") + "%";
                            resultItem3.d14_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D14_P)).ToString("#,##0.##") + "%";
                            resultItem3.d15_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D15_P)).ToString("#,##0.##") + "%";
                            resultItem3.d16_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D16_P)).ToString("#,##0.##") + "%";
                            resultItem3.d17_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D17_P)).ToString("#,##0.##") + "%";
                            resultItem3.d18_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D18_P)).ToString("#,##0.##") + "%";
                            resultItem3.d19_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D19_P)).ToString("#,##0.##") + "%";
                            resultItem3.d20_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D20_P)).ToString("#,##0.##") + "%";
                            resultItem3.d21_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D21_P)).ToString("#,##0.##") + "%";
                            resultItem3.d22_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D22_P)).ToString("#,##0.##") + "%";
                            resultItem3.d23_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D23_P)).ToString("#,##0.##") + "%";
                            resultItem3.d24_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D24_P)).ToString("#,##0.##") + "%";
                            resultItem3.d25_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D25_P)).ToString("#,##0.##") + "%";
                            resultItem3.d26_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D26_P)).ToString("#,##0.##") + "%";
                            resultItem3.d27_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D27_P)).ToString("#,##0.##") + "%";
                            resultItem3.d28_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D28_P)).ToString("#,##0.##") + "%";
                            resultItem3.d29_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D29_P)).ToString("#,##0.##") + "%";
                            resultItem3.d30_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D30_P)).ToString("#,##0.##") + "%";
                            resultItem3.d31_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D31_P)).ToString("#,##0.##") + "%";
                            location_list.Add(resultItem3);
                        }
                    }

                    ////Owner////

                    if (resultquery_owner.Count == 0)
                    {
                        var resultItem = new ReportSumSpaceUtilizationViewModel();
                        var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.report_date = datereport;
                        resultItem.report_date_to = endDate;
                        resultItem.warehouse = warehouse;
                        owner_list.Add(resultItem);
                    }
                    else
                    {

                        int num = 1;
                        foreach (var item in resultquery_owner)
                        {
                            var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                            var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                            var resultItem = new ReportSumSpaceUtilizationViewModel();
                            resultItem.rowNum = num;
                            resultItem.rowNum_text = (num++).ToString();
                             resultItem.ambientRoom = "Ambient";
                            resultItem.owner_Name = item.Owner_Name;
                            resultItem.maxValue_ = item.MaxValuePeak > 0 ? Convert.ToInt32(item.MaxValuePeak).ToString("#,###") : "-";
                            resultItem.d01_ = item.D01 > 0 ? Convert.ToInt32(item.D01).ToString("#,###") : "-";
                            resultItem.d02_ = item.D02 > 0 ? Convert.ToInt32(item.D02).ToString("#,###") : "-";
                            resultItem.d03_ = item.D03 > 0 ? Convert.ToInt32(item.D03).ToString("#,###") : "-";
                            resultItem.d04_ = item.D04 > 0 ? Convert.ToInt32(item.D04).ToString("#,###") : "-";
                            resultItem.d05_ = item.D05 > 0 ? Convert.ToInt32(item.D05).ToString("#,###") : "-";
                            resultItem.d06_ = item.D06 > 0 ? Convert.ToInt32(item.D06).ToString("#,###") : "-";
                            resultItem.d07_ = item.D07 > 0 ? Convert.ToInt32(item.D07).ToString("#,###") : "-";
                            resultItem.d08_ = item.D08 > 0 ? Convert.ToInt32(item.D08).ToString("#,###") : "-";
                            resultItem.d09_ = item.D09 > 0 ? Convert.ToInt32(item.D09).ToString("#,###") : "-";
                            resultItem.d10_ = item.D10 > 0 ? Convert.ToInt32(item.D10).ToString("#,###") : "-";
                            resultItem.d11_ = item.D11 > 0 ? Convert.ToInt32(item.D11).ToString("#,###") : "-";
                            resultItem.d12_ = item.D12 > 0 ? Convert.ToInt32(item.D12).ToString("#,###") : "-";
                            resultItem.d13_ = item.D13 > 0 ? Convert.ToInt32(item.D13).ToString("#,###") : "-";
                            resultItem.d14_ = item.D14 > 0 ? Convert.ToInt32(item.D14).ToString("#,###") : "-";
                            resultItem.d15_ = item.D15 > 0 ? Convert.ToInt32(item.D15).ToString("#,###") : "-";
                            resultItem.d16_ = item.D16 > 0 ? Convert.ToInt32(item.D16).ToString("#,###") : "-";
                            resultItem.d17_ = item.D17 > 0 ? Convert.ToInt32(item.D17).ToString("#,###") : "-";
                            resultItem.d18_ = item.D18 > 0 ? Convert.ToInt32(item.D18).ToString("#,###") : "-";
                            resultItem.d19_ = item.D19 > 0 ? Convert.ToInt32(item.D19).ToString("#,###") : "-";
                            resultItem.d20_ = item.D20 > 0 ? Convert.ToInt32(item.D20).ToString("#,###") : "-";
                            resultItem.d21_ = item.D21 > 0 ? Convert.ToInt32(item.D21).ToString("#,###") : "-";
                            resultItem.d22_ = item.D22 > 0 ? Convert.ToInt32(item.D22).ToString("#,###") : "-";
                            resultItem.d23_ = item.D23 > 0 ? Convert.ToInt32(item.D23).ToString("#,###") : "-";
                            resultItem.d24_ = item.D24 > 0 ? Convert.ToInt32(item.D24).ToString("#,###") : "-";
                            resultItem.d25_ = item.D25 > 0 ? Convert.ToInt32(item.D25).ToString("#,###") : "-";
                            resultItem.d26_ = item.D26 > 0 ? Convert.ToInt32(item.D26).ToString("#,###") : "-";
                            resultItem.d27_ = item.D27 > 0 ? Convert.ToInt32(item.D27).ToString("#,###") : "-";
                            resultItem.d28_ = item.D28 > 0 ? Convert.ToInt32(item.D28).ToString("#,###") : "-";
                            resultItem.d29_ = item.D29 > 0 ? Convert.ToInt32(item.D29).ToString("#,###") : "-";
                            resultItem.d30_ = item.D30 > 0 ? Convert.ToInt32(item.D30).ToString("#,###") : "-";
                            resultItem.d31_ = item.D31 > 0 ? Convert.ToInt32(item.D31).ToString("#,###") : "-";
                            resultItem.report_date = datereport;
                            resultItem.warehouse = warehouse;
                            owner_list.Add(resultItem);

                            /////percent
                            var resultItem_P = new ReportSumSpaceUtilizationViewModel();
                            resultItem_P.maxValue_ = Convert.ToDecimal(item.MaxValuePeak_Percent).ToString("#,##0.##") + "%";
                            resultItem_P.d01_ = Convert.ToDecimal(item.D01_P).ToString("#,##0.##") + "%";
                            resultItem_P.d02_ = Convert.ToDecimal(item.D02_P).ToString("#,##0.##") + "%";
                            resultItem_P.d03_ = Convert.ToDecimal(item.D03_P).ToString("#,##0.##") + "%";
                            resultItem_P.d04_ = Convert.ToDecimal(item.D04_P).ToString("#,##0.##") + "%";
                            resultItem_P.d05_ = Convert.ToDecimal(item.D05_P).ToString("#,##0.##") + "%";
                            resultItem_P.d06_ = Convert.ToDecimal(item.D06_P).ToString("#,##0.##") + "%";
                            resultItem_P.d07_ = Convert.ToDecimal(item.D07_P).ToString("#,##0.##") + "%";
                            resultItem_P.d08_ = Convert.ToDecimal(item.D08_P).ToString("#,##0.##") + "%";
                            resultItem_P.d09_ = Convert.ToDecimal(item.D09_P).ToString("#,##0.##") + "%";
                            resultItem_P.d10_ = Convert.ToDecimal(item.D10_P).ToString("#,##0.##") + "%";
                            resultItem_P.d11_ = Convert.ToDecimal(item.D11_P).ToString("#,##0.##") + "%";
                            resultItem_P.d12_ = Convert.ToDecimal(item.D12_P).ToString("#,##0.##") + "%";
                            resultItem_P.d13_ = Convert.ToDecimal(item.D13_P).ToString("#,##0.##") + "%";
                            resultItem_P.d14_ = Convert.ToDecimal(item.D14_P).ToString("#,##0.##") + "%";
                            resultItem_P.d15_ = Convert.ToDecimal(item.D15_P).ToString("#,##0.##") + "%";
                            resultItem_P.d16_ = Convert.ToDecimal(item.D16_P).ToString("#,##0.##") + "%";
                            resultItem_P.d17_ = Convert.ToDecimal(item.D17_P).ToString("#,##0.##") + "%";
                            resultItem_P.d18_ = Convert.ToDecimal(item.D18_P).ToString("#,##0.##") + "%";
                            resultItem_P.d19_ = Convert.ToDecimal(item.D19_P).ToString("#,##0.##") + "%";
                            resultItem_P.d20_ = Convert.ToDecimal(item.D20_P).ToString("#,##0.##") + "%";
                            resultItem_P.d21_ = Convert.ToDecimal(item.D21_P).ToString("#,##0.##") + "%";
                            resultItem_P.d22_ = Convert.ToDecimal(item.D22_P).ToString("#,##0.##") + "%";
                            resultItem_P.d23_ = Convert.ToDecimal(item.D23_P).ToString("#,##0.##") + "%";
                            resultItem_P.d24_ = Convert.ToDecimal(item.D24_P).ToString("#,##0.##") + "%";
                            resultItem_P.d25_ = Convert.ToDecimal(item.D25_P).ToString("#,##0.##") + "%";
                            resultItem_P.d26_ = Convert.ToDecimal(item.D26_P).ToString("#,##0.##") + "%";
                            resultItem_P.d27_ = Convert.ToDecimal(item.D27_P).ToString("#,##0.##") + "%";
                            resultItem_P.d28_ = Convert.ToDecimal(item.D28_P).ToString("#,##0.##") + "%";
                            resultItem_P.d29_ = Convert.ToDecimal(item.D29_P).ToString("#,##0.##") + "%";
                            resultItem_P.d30_ = Convert.ToDecimal(item.D30_P).ToString("#,##0.##") + "%";
                            resultItem_P.d31_ = Convert.ToDecimal(item.D31_P).ToString("#,##0.##") + "%";
                            owner_list.Add(resultItem_P);

                        }

                        var count = resultquery_owner.Where(c => c.IsCal == 1).Count();
                        if (count > 0)
                        {
                            ////total
                            var resultItem2 = new ReportSumSpaceUtilizationViewModel();
                            resultItem2.owner_Name = "TOTAL";
                            resultItem2.maxValue_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.MaxValuePeak) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.MaxValuePeak)).ToString("#,###") : "-";
                            resultItem2.d01_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D01) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D01)).ToString("#,###") : "-";
                            resultItem2.d02_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D02) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D02)).ToString("#,###") : "-";
                            resultItem2.d03_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D03) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D03)).ToString("#,###") : "-";
                            resultItem2.d04_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D04) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D04)).ToString("#,###") : "-";
                            resultItem2.d05_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D05) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D05)).ToString("#,###") : "-";
                            resultItem2.d06_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D06) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D06)).ToString("#,###") : "-";
                            resultItem2.d07_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D07) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D07)).ToString("#,###") : "-";
                            resultItem2.d08_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D08) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D08)).ToString("#,###") : "-";
                            resultItem2.d09_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D09) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D09)).ToString("#,###") : "-";
                            resultItem2.d10_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D10) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D10)).ToString("#,###") : "-";
                            resultItem2.d11_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D11) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D11)).ToString("#,###") : "-";
                            resultItem2.d12_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D12) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D12)).ToString("#,###") : "-";
                            resultItem2.d13_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D13) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D13)).ToString("#,###") : "-";
                            resultItem2.d14_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D14) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D14)).ToString("#,###") : "-";
                            resultItem2.d15_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D15) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D15)).ToString("#,###") : "-";
                            resultItem2.d16_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D16) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D16)).ToString("#,###") : "-";
                            resultItem2.d17_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D17) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D17)).ToString("#,###") : "-";
                            resultItem2.d18_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D18) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D18)).ToString("#,###") : "-";
                            resultItem2.d19_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D19) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D19)).ToString("#,###") : "-";
                            resultItem2.d20_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D20) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D20)).ToString("#,###") : "-";
                            resultItem2.d21_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D21) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D21)).ToString("#,###") : "-";
                            resultItem2.d22_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D22) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D22)).ToString("#,###") : "-";
                            resultItem2.d23_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D23) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D23)).ToString("#,###") : "-";
                            resultItem2.d24_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D24) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D24)).ToString("#,###") : "-";
                            resultItem2.d25_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D25) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D25)).ToString("#,###") : "-";
                            resultItem2.d26_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D26) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D26)).ToString("#,###") : "-";
                            resultItem2.d27_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D27) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D27)).ToString("#,###") : "-";
                            resultItem2.d28_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D28) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D28)).ToString("#,###") : "-";
                            resultItem2.d29_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D29) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D29)).ToString("#,###") : "-";
                            resultItem2.d30_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D30) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D30)).ToString("#,###") : "-";
                            resultItem2.d31_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D31) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D31)).ToString("#,###") : "-";
                            owner_list.Add(resultItem2);



                            var resultItem3 = new ReportSumSpaceUtilizationViewModel();
                            resultItem3.maxValue_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.MaxValuePeak_Percent) / count).ToString("#,##0.##") + "%";
                            resultItem3.d01_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D01_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d02_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D02_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d03_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D03_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d04_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D04_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d05_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D05_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d06_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D06_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d07_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D07_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d08_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D08_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d09_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D09_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d10_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D10_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d11_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D11_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d12_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D12_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d13_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D13_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d14_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D14_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d15_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D15_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d16_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D16_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d17_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D17_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d18_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D18_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d19_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D19_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d20_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D20_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d21_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D21_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d22_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D22_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d23_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D23_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d24_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D24_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d25_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D25_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d26_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D26_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d27_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D27_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d28_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D28_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d29_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D29_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d30_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D30_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d31_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D31_P) / count).ToString("#,##0.##") + "%";
                            owner_list.Add(resultItem3);
                        }
                        else
                        {
                            ////total
                            var resultItem2 = new ReportSumSpaceUtilizationViewModel();
                            resultItem2.owner_Name = "TOTAL";
                            resultItem2.maxValue_ = resultquery_owner.Sum(a => a.MaxValuePeak) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.MaxValuePeak)).ToString("#,###") : "-";
                            resultItem2.d01_ = resultquery_owner.Sum(a => a.D01) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D01)).ToString("#,###") : "-";
                            resultItem2.d02_ = resultquery_owner.Sum(a => a.D02) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D02)).ToString("#,###") : "-";
                            resultItem2.d03_ = resultquery_owner.Sum(a => a.D03) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D03)).ToString("#,###") : "-";
                            resultItem2.d04_ = resultquery_owner.Sum(a => a.D04) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D04)).ToString("#,###") : "-";
                            resultItem2.d05_ = resultquery_owner.Sum(a => a.D05) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D05)).ToString("#,###") : "-";
                            resultItem2.d06_ = resultquery_owner.Sum(a => a.D06) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D06)).ToString("#,###") : "-";
                            resultItem2.d07_ = resultquery_owner.Sum(a => a.D07) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D07)).ToString("#,###") : "-";
                            resultItem2.d08_ = resultquery_owner.Sum(a => a.D08) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D08)).ToString("#,###") : "-";
                            resultItem2.d09_ = resultquery_owner.Sum(a => a.D09) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D09)).ToString("#,###") : "-";
                            resultItem2.d10_ = resultquery_owner.Sum(a => a.D10) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D10)).ToString("#,###") : "-";
                            resultItem2.d11_ = resultquery_owner.Sum(a => a.D11) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D11)).ToString("#,###") : "-";
                            resultItem2.d12_ = resultquery_owner.Sum(a => a.D12) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D12)).ToString("#,###") : "-";
                            resultItem2.d13_ = resultquery_owner.Sum(a => a.D13) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D13)).ToString("#,###") : "-";
                            resultItem2.d14_ = resultquery_owner.Sum(a => a.D14) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D14)).ToString("#,###") : "-";
                            resultItem2.d15_ = resultquery_owner.Sum(a => a.D15) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D15)).ToString("#,###") : "-";
                            resultItem2.d16_ = resultquery_owner.Sum(a => a.D16) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D16)).ToString("#,###") : "-";
                            resultItem2.d17_ = resultquery_owner.Sum(a => a.D17) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D17)).ToString("#,###") : "-";
                            resultItem2.d18_ = resultquery_owner.Sum(a => a.D18) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D18)).ToString("#,###") : "-";
                            resultItem2.d19_ = resultquery_owner.Sum(a => a.D19) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D19)).ToString("#,###") : "-";
                            resultItem2.d20_ = resultquery_owner.Sum(a => a.D20) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D20)).ToString("#,###") : "-";
                            resultItem2.d21_ = resultquery_owner.Sum(a => a.D21) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D21)).ToString("#,###") : "-";
                            resultItem2.d22_ = resultquery_owner.Sum(a => a.D22) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D22)).ToString("#,###") : "-";
                            resultItem2.d23_ = resultquery_owner.Sum(a => a.D23) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D23)).ToString("#,###") : "-";
                            resultItem2.d24_ = resultquery_owner.Sum(a => a.D24) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D24)).ToString("#,###") : "-";
                            resultItem2.d25_ = resultquery_owner.Sum(a => a.D25) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D25)).ToString("#,###") : "-";
                            resultItem2.d26_ = resultquery_owner.Sum(a => a.D26) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D26)).ToString("#,###") : "-";
                            resultItem2.d27_ = resultquery_owner.Sum(a => a.D27) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D27)).ToString("#,###") : "-";
                            resultItem2.d28_ = resultquery_owner.Sum(a => a.D28) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D28)).ToString("#,###") : "-";
                            resultItem2.d29_ = resultquery_owner.Sum(a => a.D29) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D29)).ToString("#,###") : "-";
                            resultItem2.d30_ = resultquery_owner.Sum(a => a.D30) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D30)).ToString("#,###") : "-";
                            resultItem2.d31_ = resultquery_owner.Sum(a => a.D31) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D31)).ToString("#,###") : "-";
                            owner_list.Add(resultItem2);


                            var resultItem3 = new ReportSumSpaceUtilizationViewModel();
                            resultItem3.maxValue_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.MaxValuePeak)).ToString("#,##0.##") + "%";
                            resultItem3.d01_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D01_P)).ToString("#,##0.##") + "%";
                            resultItem3.d02_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D02_P)).ToString("#,##0.##") + "%";
                            resultItem3.d03_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D03_P)).ToString("#,##0.##") + "%";
                            resultItem3.d04_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D04_P)).ToString("#,##0.##") + "%";
                            resultItem3.d05_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D05_P)).ToString("#,##0.##") + "%";
                            resultItem3.d06_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D06_P)).ToString("#,##0.##") + "%";
                            resultItem3.d07_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D07_P)).ToString("#,##0.##") + "%";
                            resultItem3.d08_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D08_P)).ToString("#,##0.##") + "%";
                            resultItem3.d09_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D09_P)).ToString("#,##0.##") + "%";
                            resultItem3.d10_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D10_P)).ToString("#,##0.##") + "%";
                            resultItem3.d11_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D11_P)).ToString("#,##0.##") + "%";
                            resultItem3.d12_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D12_P)).ToString("#,##0.##") + "%";
                            resultItem3.d13_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D13_P)).ToString("#,##0.##") + "%";
                            resultItem3.d14_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D14_P)).ToString("#,##0.##") + "%";
                            resultItem3.d15_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D15_P)).ToString("#,##0.##") + "%";
                            resultItem3.d16_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D16_P)).ToString("#,##0.##") + "%";
                            resultItem3.d17_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D17_P)).ToString("#,##0.##") + "%";
                            resultItem3.d18_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D18_P)).ToString("#,##0.##") + "%";
                            resultItem3.d19_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D19_P)).ToString("#,##0.##") + "%";
                            resultItem3.d20_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D20_P)).ToString("#,##0.##") + "%";
                            resultItem3.d21_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D21_P)).ToString("#,##0.##") + "%";
                            resultItem3.d22_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D22_P)).ToString("#,##0.##") + "%";
                            resultItem3.d23_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D23_P)).ToString("#,##0.##") + "%";
                            resultItem3.d24_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D24_P)).ToString("#,##0.##") + "%";
                            resultItem3.d25_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D25_P)).ToString("#,##0.##") + "%";
                            resultItem3.d26_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D26_P)).ToString("#,##0.##") + "%";
                            resultItem3.d27_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D27_P)).ToString("#,##0.##") + "%";
                            resultItem3.d28_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D28_P)).ToString("#,##0.##") + "%";
                            resultItem3.d29_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D29_P)).ToString("#,##0.##") + "%";
                            resultItem3.d30_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D30_P)).ToString("#,##0.##") + "%";
                            resultItem3.d31_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D31_P)).ToString("#,##0.##") + "%";
                            owner_list.Add(resultItem3);
                        }
                    }

                    ////End Owner////

                    result.location_type = location_list;
                    result.owner = owner_list;

                    /////// Freeze /////
                    resultquery_location = temp_Master_DBContext.sp_rpt_15_Space_Utilization_All.FromSql(query_location).ToList();
                    resultquery_owner = temp_Master_DBContext.sp_rpt_15_Space_Utilization_Owner_All.FromSql(query_owner).ToList();

                    if (resultquery_location.Count == 0)
                    {
                        var resultItem = new ReportSumSpaceUtilizationViewModel();
                        var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.report_date = datereport;
                        resultItem.report_date_to = endDate;
                        resultItem.warehouse = warehouse;
                        location_list_all.Add(resultItem);
                    }
                    else
                    {

                        int num = 1;
                        foreach (var item in resultquery_location)
                        {
                            var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                            var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                            var resultItem = new ReportSumSpaceUtilizationViewModel();
                            resultItem.rowNum = num;
                            if (item.LocationType_Name == "ASRS")
                            {
                                resultItem.rowNum_text = (num++).ToString();
                            }
                            else if (item.LocationType_Name == "ASRS125")
                            {
                                resultItem.rowNum_text = "1.1";
                            }
                            else if (item.LocationType_Name == "ASRS145")
                            {
                                resultItem.rowNum_text = "1.2";
                            }
                            else if (item.LocationType_Name == "ASRS165")
                            {
                                resultItem.rowNum_text = "1.3";
                            }
                            else
                            {
                                resultItem.rowNum_text = (num++).ToString();
                            }

                           
                            resultItem.ambientRoom = "Freeze";
                            resultItem.locationType_Name = item.LocationType_Name;
                            resultItem.count_Location = item.Count_Location;
                            resultItem.maxValue_ = item.Count_Location > 0 ? Convert.ToInt32(item.Count_Location).ToString("#,###") : "-";
                            resultItem.d01_ = item.D01 > 0 ? Convert.ToInt32(item.D01).ToString("#,###") : "-";
                            resultItem.d02_ = item.D02 > 0 ? Convert.ToInt32(item.D02).ToString("#,###") : "-";
                            resultItem.d03_ = item.D03 > 0 ? Convert.ToInt32(item.D03).ToString("#,###") : "-";
                            resultItem.d04_ = item.D04 > 0 ? Convert.ToInt32(item.D04).ToString("#,###") : "-";
                            resultItem.d05_ = item.D05 > 0 ? Convert.ToInt32(item.D05).ToString("#,###") : "-";
                            resultItem.d06_ = item.D06 > 0 ? Convert.ToInt32(item.D06).ToString("#,###") : "-";
                            resultItem.d07_ = item.D07 > 0 ? Convert.ToInt32(item.D07).ToString("#,###") : "-";
                            resultItem.d08_ = item.D08 > 0 ? Convert.ToInt32(item.D08).ToString("#,###") : "-";
                            resultItem.d09_ = item.D09 > 0 ? Convert.ToInt32(item.D09).ToString("#,###") : "-";
                            resultItem.d10_ = item.D10 > 0 ? Convert.ToInt32(item.D10).ToString("#,###") : "-";
                            resultItem.d11_ = item.D11 > 0 ? Convert.ToInt32(item.D11).ToString("#,###") : "-";
                            resultItem.d12_ = item.D12 > 0 ? Convert.ToInt32(item.D12).ToString("#,###") : "-";
                            resultItem.d13_ = item.D13 > 0 ? Convert.ToInt32(item.D13).ToString("#,###") : "-";
                            resultItem.d14_ = item.D14 > 0 ? Convert.ToInt32(item.D14).ToString("#,###") : "-";
                            resultItem.d15_ = item.D15 > 0 ? Convert.ToInt32(item.D15).ToString("#,###") : "-";
                            resultItem.d16_ = item.D16 > 0 ? Convert.ToInt32(item.D16).ToString("#,###") : "-";
                            resultItem.d17_ = item.D17 > 0 ? Convert.ToInt32(item.D17).ToString("#,###") : "-";
                            resultItem.d18_ = item.D18 > 0 ? Convert.ToInt32(item.D18).ToString("#,###") : "-";
                            resultItem.d19_ = item.D19 > 0 ? Convert.ToInt32(item.D19).ToString("#,###") : "-";
                            resultItem.d20_ = item.D20 > 0 ? Convert.ToInt32(item.D20).ToString("#,###") : "-";
                            resultItem.d21_ = item.D21 > 0 ? Convert.ToInt32(item.D21).ToString("#,###") : "-";
                            resultItem.d22_ = item.D22 > 0 ? Convert.ToInt32(item.D22).ToString("#,###") : "-";
                            resultItem.d23_ = item.D23 > 0 ? Convert.ToInt32(item.D23).ToString("#,###") : "-";
                            resultItem.d24_ = item.D24 > 0 ? Convert.ToInt32(item.D24).ToString("#,###") : "-";
                            resultItem.d25_ = item.D25 > 0 ? Convert.ToInt32(item.D25).ToString("#,###") : "-";
                            resultItem.d26_ = item.D26 > 0 ? Convert.ToInt32(item.D26).ToString("#,###") : "-";
                            resultItem.d27_ = item.D27 > 0 ? Convert.ToInt32(item.D27).ToString("#,###") : "-";
                            resultItem.d28_ = item.D28 > 0 ? Convert.ToInt32(item.D28).ToString("#,###") : "-";
                            resultItem.d29_ = item.D29 > 0 ? Convert.ToInt32(item.D29).ToString("#,###") : "-";
                            resultItem.d30_ = item.D30 > 0 ? Convert.ToInt32(item.D30).ToString("#,###") : "-";
                            resultItem.d31_ = item.D31 > 0 ? Convert.ToInt32(item.D31).ToString("#,###") : "-";
                            resultItem.report_date = datereport;
                            resultItem.warehouse = warehouse;
                            location_list_all.Add(resultItem);

                            /////percent
                            var resultItem_P = new ReportSumSpaceUtilizationViewModel();
                            resultItem_P.maxValue_ = Convert.ToDecimal(item.MaxValuePeak).ToString("#,##0.##") + "%";
                            resultItem_P.d01_ = Convert.ToDecimal(item.D01_P).ToString("#,##0.##") + "%";
                            resultItem_P.d02_ = Convert.ToDecimal(item.D02_P).ToString("#,##0.##") + "%";
                            resultItem_P.d03_ = Convert.ToDecimal(item.D03_P).ToString("#,##0.##") + "%";
                            resultItem_P.d04_ = Convert.ToDecimal(item.D04_P).ToString("#,##0.##") + "%";
                            resultItem_P.d05_ = Convert.ToDecimal(item.D05_P).ToString("#,##0.##") + "%";
                            resultItem_P.d06_ = Convert.ToDecimal(item.D06_P).ToString("#,##0.##") + "%";
                            resultItem_P.d07_ = Convert.ToDecimal(item.D07_P).ToString("#,##0.##") + "%";
                            resultItem_P.d08_ = Convert.ToDecimal(item.D08_P).ToString("#,##0.##") + "%";
                            resultItem_P.d09_ = Convert.ToDecimal(item.D09_P).ToString("#,##0.##") + "%";
                            resultItem_P.d10_ = Convert.ToDecimal(item.D10_P).ToString("#,##0.##") + "%";
                            resultItem_P.d11_ = Convert.ToDecimal(item.D11_P).ToString("#,##0.##") + "%";
                            resultItem_P.d12_ = Convert.ToDecimal(item.D12_P).ToString("#,##0.##") + "%";
                            resultItem_P.d13_ = Convert.ToDecimal(item.D13_P).ToString("#,##0.##") + "%";
                            resultItem_P.d14_ = Convert.ToDecimal(item.D14_P).ToString("#,##0.##") + "%";
                            resultItem_P.d15_ = Convert.ToDecimal(item.D15_P).ToString("#,##0.##") + "%";
                            resultItem_P.d16_ = Convert.ToDecimal(item.D16_P).ToString("#,##0.##") + "%";
                            resultItem_P.d17_ = Convert.ToDecimal(item.D17_P).ToString("#,##0.##") + "%";
                            resultItem_P.d18_ = Convert.ToDecimal(item.D18_P).ToString("#,##0.##") + "%";
                            resultItem_P.d19_ = Convert.ToDecimal(item.D19_P).ToString("#,##0.##") + "%";
                            resultItem_P.d20_ = Convert.ToDecimal(item.D20_P).ToString("#,##0.##") + "%";
                            resultItem_P.d21_ = Convert.ToDecimal(item.D21_P).ToString("#,##0.##") + "%";
                            resultItem_P.d22_ = Convert.ToDecimal(item.D22_P).ToString("#,##0.##") + "%";
                            resultItem_P.d23_ = Convert.ToDecimal(item.D23_P).ToString("#,##0.##") + "%";
                            resultItem_P.d24_ = Convert.ToDecimal(item.D24_P).ToString("#,##0.##") + "%";
                            resultItem_P.d25_ = Convert.ToDecimal(item.D25_P).ToString("#,##0.##") + "%";
                            resultItem_P.d26_ = Convert.ToDecimal(item.D26_P).ToString("#,##0.##") + "%";
                            resultItem_P.d27_ = Convert.ToDecimal(item.D27_P).ToString("#,##0.##") + "%";
                            resultItem_P.d28_ = Convert.ToDecimal(item.D28_P).ToString("#,##0.##") + "%";
                            resultItem_P.d29_ = Convert.ToDecimal(item.D29_P).ToString("#,##0.##") + "%";
                            resultItem_P.d30_ = Convert.ToDecimal(item.D30_P).ToString("#,##0.##") + "%";
                            resultItem_P.d31_ = Convert.ToDecimal(item.D31_P).ToString("#,##0.##") + "%";
                            location_list_all.Add(resultItem_P);

                        }

                        var count = resultquery_location.Where(c => c.IsCal == 1).Count();
                        if (count > 0)
                        {
                            ////total
                            var resultItem2 = new ReportSumSpaceUtilizationViewModel();
                            resultItem2.locationType_Name = "TOTAL";
                            resultItem2.maxValue_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.Count_Location) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.Count_Location)).ToString("#,###") : "-";
                            resultItem2.d01_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D01) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D01)).ToString("#,###") : "-";
                            resultItem2.d02_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D02) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D02)).ToString("#,###") : "-";
                            resultItem2.d03_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D03) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D03)).ToString("#,###") : "-";
                            resultItem2.d04_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D04) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D04)).ToString("#,###") : "-";
                            resultItem2.d05_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D05) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D05)).ToString("#,###") : "-";
                            resultItem2.d06_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D06) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D06)).ToString("#,###") : "-";
                            resultItem2.d07_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D07) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D07)).ToString("#,###") : "-";
                            resultItem2.d08_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D08) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D08)).ToString("#,###") : "-";
                            resultItem2.d09_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D09) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D09)).ToString("#,###") : "-";
                            resultItem2.d10_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D10) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D10)).ToString("#,###") : "-";
                            resultItem2.d11_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D11) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D11)).ToString("#,###") : "-";
                            resultItem2.d12_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D12) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D12)).ToString("#,###") : "-";
                            resultItem2.d13_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D13) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D13)).ToString("#,###") : "-";
                            resultItem2.d14_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D14) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D14)).ToString("#,###") : "-";
                            resultItem2.d15_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D15) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D15)).ToString("#,###") : "-";
                            resultItem2.d16_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D16) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D16)).ToString("#,###") : "-";
                            resultItem2.d17_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D17) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D17)).ToString("#,###") : "-";
                            resultItem2.d18_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D18) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D18)).ToString("#,###") : "-";
                            resultItem2.d19_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D19) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D19)).ToString("#,###") : "-";
                            resultItem2.d20_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D20) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D20)).ToString("#,###") : "-";
                            resultItem2.d21_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D21) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D21)).ToString("#,###") : "-";
                            resultItem2.d22_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D22) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D22)).ToString("#,###") : "-";
                            resultItem2.d23_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D23) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D23)).ToString("#,###") : "-";
                            resultItem2.d24_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D24) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D24)).ToString("#,###") : "-";
                            resultItem2.d25_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D25) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D25)).ToString("#,###") : "-";
                            resultItem2.d26_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D26) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D26)).ToString("#,###") : "-";
                            resultItem2.d27_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D27) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D27)).ToString("#,###") : "-";
                            resultItem2.d28_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D28) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D28)).ToString("#,###") : "-";
                            resultItem2.d29_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D29) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D29)).ToString("#,###") : "-";
                            resultItem2.d30_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D30) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D30)).ToString("#,###") : "-";
                            resultItem2.d31_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D31) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D31)).ToString("#,###") : "-";
                            location_list_all.Add(resultItem2);



                            var resultItem3 = new ReportSumSpaceUtilizationViewModel();
                            resultItem3.maxValue_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.MaxValuePeak) / count).ToString("#,##0.##") + "%";
                            resultItem3.d01_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D01_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d02_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D02_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d03_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D03_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d04_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D04_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d05_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D05_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d06_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D06_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d07_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D07_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d08_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D08_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d09_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D09_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d10_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D10_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d11_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D11_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d12_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D12_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d13_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D13_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d14_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D14_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d15_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D15_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d16_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D16_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d17_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D17_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d18_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D18_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d19_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D19_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d20_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D20_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d21_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D21_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d22_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D22_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d23_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D23_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d24_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D24_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d25_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D25_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d26_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D26_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d27_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D27_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d28_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D28_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d29_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D29_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d30_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D30_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d31_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D31_P) / count).ToString("#,##0.##") + "%";
                            location_list_all.Add(resultItem3);
                        }
                        else
                        {
                            ////total
                            var resultItem2 = new ReportSumSpaceUtilizationViewModel();
                            resultItem2.locationType_Name = "TOTAL";
                            resultItem2.maxValue_ = resultquery_location.Sum(a => a.Count_Location) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.Count_Location)).ToString("#,###") : "-";
                            resultItem2.d01_ = resultquery_location.Sum(a => a.D01) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D01)).ToString("#,###") : "-";
                            resultItem2.d02_ = resultquery_location.Sum(a => a.D02) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D02)).ToString("#,###") : "-";
                            resultItem2.d03_ = resultquery_location.Sum(a => a.D03) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D03)).ToString("#,###") : "-";
                            resultItem2.d04_ = resultquery_location.Sum(a => a.D04) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D04)).ToString("#,###") : "-";
                            resultItem2.d05_ = resultquery_location.Sum(a => a.D05) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D05)).ToString("#,###") : "-";
                            resultItem2.d06_ = resultquery_location.Sum(a => a.D06) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D06)).ToString("#,###") : "-";
                            resultItem2.d07_ = resultquery_location.Sum(a => a.D07) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D07)).ToString("#,###") : "-";
                            resultItem2.d08_ = resultquery_location.Sum(a => a.D08) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D08)).ToString("#,###") : "-";
                            resultItem2.d09_ = resultquery_location.Sum(a => a.D09) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D09)).ToString("#,###") : "-";
                            resultItem2.d10_ = resultquery_location.Sum(a => a.D10) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D10)).ToString("#,###") : "-";
                            resultItem2.d11_ = resultquery_location.Sum(a => a.D11) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D11)).ToString("#,###") : "-";
                            resultItem2.d12_ = resultquery_location.Sum(a => a.D12) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D12)).ToString("#,###") : "-";
                            resultItem2.d13_ = resultquery_location.Sum(a => a.D13) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D13)).ToString("#,###") : "-";
                            resultItem2.d14_ = resultquery_location.Sum(a => a.D14) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D14)).ToString("#,###") : "-";
                            resultItem2.d15_ = resultquery_location.Sum(a => a.D15) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D15)).ToString("#,###") : "-";
                            resultItem2.d16_ = resultquery_location.Sum(a => a.D16) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D16)).ToString("#,###") : "-";
                            resultItem2.d17_ = resultquery_location.Sum(a => a.D17) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D17)).ToString("#,###") : "-";
                            resultItem2.d18_ = resultquery_location.Sum(a => a.D18) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D18)).ToString("#,###") : "-";
                            resultItem2.d19_ = resultquery_location.Sum(a => a.D19) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D19)).ToString("#,###") : "-";
                            resultItem2.d20_ = resultquery_location.Sum(a => a.D20) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D20)).ToString("#,###") : "-";
                            resultItem2.d21_ = resultquery_location.Sum(a => a.D21) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D21)).ToString("#,###") : "-";
                            resultItem2.d22_ = resultquery_location.Sum(a => a.D22) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D22)).ToString("#,###") : "-";
                            resultItem2.d23_ = resultquery_location.Sum(a => a.D23) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D23)).ToString("#,###") : "-";
                            resultItem2.d24_ = resultquery_location.Sum(a => a.D24) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D24)).ToString("#,###") : "-";
                            resultItem2.d25_ = resultquery_location.Sum(a => a.D25) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D25)).ToString("#,###") : "-";
                            resultItem2.d26_ = resultquery_location.Sum(a => a.D26) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D26)).ToString("#,###") : "-";
                            resultItem2.d27_ = resultquery_location.Sum(a => a.D27) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D27)).ToString("#,###") : "-";
                            resultItem2.d28_ = resultquery_location.Sum(a => a.D28) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D28)).ToString("#,###") : "-";
                            resultItem2.d29_ = resultquery_location.Sum(a => a.D29) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D29)).ToString("#,###") : "-";
                            resultItem2.d30_ = resultquery_location.Sum(a => a.D30) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D30)).ToString("#,###") : "-";
                            resultItem2.d31_ = resultquery_location.Sum(a => a.D31) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.D31)).ToString("#,###") : "-";
                            location_list_all.Add(resultItem2);


                            var resultItem3 = new ReportSumSpaceUtilizationViewModel();
                            resultItem3.maxValue_ = Convert.ToDecimal(resultquery_location.Sum(a => a.MaxValuePeak)).ToString("#,##0.##") + "%";
                            resultItem3.d01_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D01_P)).ToString("#,##0.##") + "%";
                            resultItem3.d02_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D02_P)).ToString("#,##0.##") + "%";
                            resultItem3.d03_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D03_P)).ToString("#,##0.##") + "%";
                            resultItem3.d04_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D04_P)).ToString("#,##0.##") + "%";
                            resultItem3.d05_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D05_P)).ToString("#,##0.##") + "%";
                            resultItem3.d06_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D06_P)).ToString("#,##0.##") + "%";
                            resultItem3.d07_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D07_P)).ToString("#,##0.##") + "%";
                            resultItem3.d08_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D08_P)).ToString("#,##0.##") + "%";
                            resultItem3.d09_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D09_P)).ToString("#,##0.##") + "%";
                            resultItem3.d10_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D10_P)).ToString("#,##0.##") + "%";
                            resultItem3.d11_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D11_P)).ToString("#,##0.##") + "%";
                            resultItem3.d12_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D12_P)).ToString("#,##0.##") + "%";
                            resultItem3.d13_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D13_P)).ToString("#,##0.##") + "%";
                            resultItem3.d14_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D14_P)).ToString("#,##0.##") + "%";
                            resultItem3.d15_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D15_P)).ToString("#,##0.##") + "%";
                            resultItem3.d16_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D16_P)).ToString("#,##0.##") + "%";
                            resultItem3.d17_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D17_P)).ToString("#,##0.##") + "%";
                            resultItem3.d18_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D18_P)).ToString("#,##0.##") + "%";
                            resultItem3.d19_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D19_P)).ToString("#,##0.##") + "%";
                            resultItem3.d20_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D20_P)).ToString("#,##0.##") + "%";
                            resultItem3.d21_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D21_P)).ToString("#,##0.##") + "%";
                            resultItem3.d22_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D22_P)).ToString("#,##0.##") + "%";
                            resultItem3.d23_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D23_P)).ToString("#,##0.##") + "%";
                            resultItem3.d24_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D24_P)).ToString("#,##0.##") + "%";
                            resultItem3.d25_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D25_P)).ToString("#,##0.##") + "%";
                            resultItem3.d26_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D26_P)).ToString("#,##0.##") + "%";
                            resultItem3.d27_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D27_P)).ToString("#,##0.##") + "%";
                            resultItem3.d28_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D28_P)).ToString("#,##0.##") + "%";
                            resultItem3.d29_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D29_P)).ToString("#,##0.##") + "%";
                            resultItem3.d30_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D30_P)).ToString("#,##0.##") + "%";
                            resultItem3.d31_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D31_P)).ToString("#,##0.##") + "%";
                            location_list_all.Add(resultItem3);
                        }
                    }

                    ////Owner////

                    if (resultquery_owner.Count == 0)
                    {
                        var resultItem = new ReportSumSpaceUtilizationViewModel();
                        var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.report_date = datereport;
                        resultItem.report_date_to = endDate;
                        resultItem.warehouse = warehouse;
                        owner_list_all.Add(resultItem);
                    }
                    else
                    {

                        int num = 1;
                        foreach (var item in resultquery_owner)
                        {
                            var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                            var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                            var resultItem = new ReportSumSpaceUtilizationViewModel();
                            resultItem.rowNum = num;
                            resultItem.rowNum_text = (num++).ToString();
                            resultItem.ambientRoom = "Freeze";
                            resultItem.owner_Name = item.Owner_Name;
                            resultItem.maxValue_ = item.MaxValuePeak > 0 ? Convert.ToInt32(item.MaxValuePeak).ToString("#,###") : "-";
                            resultItem.d01_ = item.D01 > 0 ? Convert.ToInt32(item.D01).ToString("#,###") : "-";
                            resultItem.d02_ = item.D02 > 0 ? Convert.ToInt32(item.D02).ToString("#,###") : "-";
                            resultItem.d03_ = item.D03 > 0 ? Convert.ToInt32(item.D03).ToString("#,###") : "-";
                            resultItem.d04_ = item.D04 > 0 ? Convert.ToInt32(item.D04).ToString("#,###") : "-";
                            resultItem.d05_ = item.D05 > 0 ? Convert.ToInt32(item.D05).ToString("#,###") : "-";
                            resultItem.d06_ = item.D06 > 0 ? Convert.ToInt32(item.D06).ToString("#,###") : "-";
                            resultItem.d07_ = item.D07 > 0 ? Convert.ToInt32(item.D07).ToString("#,###") : "-";
                            resultItem.d08_ = item.D08 > 0 ? Convert.ToInt32(item.D08).ToString("#,###") : "-";
                            resultItem.d09_ = item.D09 > 0 ? Convert.ToInt32(item.D09).ToString("#,###") : "-";
                            resultItem.d10_ = item.D10 > 0 ? Convert.ToInt32(item.D10).ToString("#,###") : "-";
                            resultItem.d11_ = item.D11 > 0 ? Convert.ToInt32(item.D11).ToString("#,###") : "-";
                            resultItem.d12_ = item.D12 > 0 ? Convert.ToInt32(item.D12).ToString("#,###") : "-";
                            resultItem.d13_ = item.D13 > 0 ? Convert.ToInt32(item.D13).ToString("#,###") : "-";
                            resultItem.d14_ = item.D14 > 0 ? Convert.ToInt32(item.D14).ToString("#,###") : "-";
                            resultItem.d15_ = item.D15 > 0 ? Convert.ToInt32(item.D15).ToString("#,###") : "-";
                            resultItem.d16_ = item.D16 > 0 ? Convert.ToInt32(item.D16).ToString("#,###") : "-";
                            resultItem.d17_ = item.D17 > 0 ? Convert.ToInt32(item.D17).ToString("#,###") : "-";
                            resultItem.d18_ = item.D18 > 0 ? Convert.ToInt32(item.D18).ToString("#,###") : "-";
                            resultItem.d19_ = item.D19 > 0 ? Convert.ToInt32(item.D19).ToString("#,###") : "-";
                            resultItem.d20_ = item.D20 > 0 ? Convert.ToInt32(item.D20).ToString("#,###") : "-";
                            resultItem.d21_ = item.D21 > 0 ? Convert.ToInt32(item.D21).ToString("#,###") : "-";
                            resultItem.d22_ = item.D22 > 0 ? Convert.ToInt32(item.D22).ToString("#,###") : "-";
                            resultItem.d23_ = item.D23 > 0 ? Convert.ToInt32(item.D23).ToString("#,###") : "-";
                            resultItem.d24_ = item.D24 > 0 ? Convert.ToInt32(item.D24).ToString("#,###") : "-";
                            resultItem.d25_ = item.D25 > 0 ? Convert.ToInt32(item.D25).ToString("#,###") : "-";
                            resultItem.d26_ = item.D26 > 0 ? Convert.ToInt32(item.D26).ToString("#,###") : "-";
                            resultItem.d27_ = item.D27 > 0 ? Convert.ToInt32(item.D27).ToString("#,###") : "-";
                            resultItem.d28_ = item.D28 > 0 ? Convert.ToInt32(item.D28).ToString("#,###") : "-";
                            resultItem.d29_ = item.D29 > 0 ? Convert.ToInt32(item.D29).ToString("#,###") : "-";
                            resultItem.d30_ = item.D30 > 0 ? Convert.ToInt32(item.D30).ToString("#,###") : "-";
                            resultItem.d31_ = item.D31 > 0 ? Convert.ToInt32(item.D31).ToString("#,###") : "-";
                            resultItem.report_date = datereport;
                            resultItem.warehouse = warehouse;
                            owner_list_all.Add(resultItem);

                            /////percent
                            var resultItem_P = new ReportSumSpaceUtilizationViewModel();
                            resultItem_P.maxValue_ = Convert.ToDecimal(item.MaxValuePeak_Percent).ToString("#,##0.##") + "%";
                            resultItem_P.d01_ = Convert.ToDecimal(item.D01_P).ToString("#,##0.##") + "%";
                            resultItem_P.d02_ = Convert.ToDecimal(item.D02_P).ToString("#,##0.##") + "%";
                            resultItem_P.d03_ = Convert.ToDecimal(item.D03_P).ToString("#,##0.##") + "%";
                            resultItem_P.d04_ = Convert.ToDecimal(item.D04_P).ToString("#,##0.##") + "%";
                            resultItem_P.d05_ = Convert.ToDecimal(item.D05_P).ToString("#,##0.##") + "%";
                            resultItem_P.d06_ = Convert.ToDecimal(item.D06_P).ToString("#,##0.##") + "%";
                            resultItem_P.d07_ = Convert.ToDecimal(item.D07_P).ToString("#,##0.##") + "%";
                            resultItem_P.d08_ = Convert.ToDecimal(item.D08_P).ToString("#,##0.##") + "%";
                            resultItem_P.d09_ = Convert.ToDecimal(item.D09_P).ToString("#,##0.##") + "%";
                            resultItem_P.d10_ = Convert.ToDecimal(item.D10_P).ToString("#,##0.##") + "%";
                            resultItem_P.d11_ = Convert.ToDecimal(item.D11_P).ToString("#,##0.##") + "%";
                            resultItem_P.d12_ = Convert.ToDecimal(item.D12_P).ToString("#,##0.##") + "%";
                            resultItem_P.d13_ = Convert.ToDecimal(item.D13_P).ToString("#,##0.##") + "%";
                            resultItem_P.d14_ = Convert.ToDecimal(item.D14_P).ToString("#,##0.##") + "%";
                            resultItem_P.d15_ = Convert.ToDecimal(item.D15_P).ToString("#,##0.##") + "%";
                            resultItem_P.d16_ = Convert.ToDecimal(item.D16_P).ToString("#,##0.##") + "%";
                            resultItem_P.d17_ = Convert.ToDecimal(item.D17_P).ToString("#,##0.##") + "%";
                            resultItem_P.d18_ = Convert.ToDecimal(item.D18_P).ToString("#,##0.##") + "%";
                            resultItem_P.d19_ = Convert.ToDecimal(item.D19_P).ToString("#,##0.##") + "%";
                            resultItem_P.d20_ = Convert.ToDecimal(item.D20_P).ToString("#,##0.##") + "%";
                            resultItem_P.d21_ = Convert.ToDecimal(item.D21_P).ToString("#,##0.##") + "%";
                            resultItem_P.d22_ = Convert.ToDecimal(item.D22_P).ToString("#,##0.##") + "%";
                            resultItem_P.d23_ = Convert.ToDecimal(item.D23_P).ToString("#,##0.##") + "%";
                            resultItem_P.d24_ = Convert.ToDecimal(item.D24_P).ToString("#,##0.##") + "%";
                            resultItem_P.d25_ = Convert.ToDecimal(item.D25_P).ToString("#,##0.##") + "%";
                            resultItem_P.d26_ = Convert.ToDecimal(item.D26_P).ToString("#,##0.##") + "%";
                            resultItem_P.d27_ = Convert.ToDecimal(item.D27_P).ToString("#,##0.##") + "%";
                            resultItem_P.d28_ = Convert.ToDecimal(item.D28_P).ToString("#,##0.##") + "%";
                            resultItem_P.d29_ = Convert.ToDecimal(item.D29_P).ToString("#,##0.##") + "%";
                            resultItem_P.d30_ = Convert.ToDecimal(item.D30_P).ToString("#,##0.##") + "%";
                            resultItem_P.d31_ = Convert.ToDecimal(item.D31_P).ToString("#,##0.##") + "%";
                            owner_list_all.Add(resultItem_P);

                        }

                        var count = resultquery_owner.Where(c => c.IsCal == 1).Count();
                        if (count > 0)
                        {
                            ////total
                            var resultItem2 = new ReportSumSpaceUtilizationViewModel();
                            resultItem2.owner_Name = "TOTAL";
                            resultItem2.maxValue_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.MaxValuePeak) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.MaxValuePeak)).ToString("#,###") : "-";
                            resultItem2.d01_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D01) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D01)).ToString("#,###") : "-";
                            resultItem2.d02_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D02) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D02)).ToString("#,###") : "-";
                            resultItem2.d03_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D03) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D03)).ToString("#,###") : "-";
                            resultItem2.d04_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D04) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D04)).ToString("#,###") : "-";
                            resultItem2.d05_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D05) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D05)).ToString("#,###") : "-";
                            resultItem2.d06_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D06) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D06)).ToString("#,###") : "-";
                            resultItem2.d07_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D07) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D07)).ToString("#,###") : "-";
                            resultItem2.d08_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D08) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D08)).ToString("#,###") : "-";
                            resultItem2.d09_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D09) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D09)).ToString("#,###") : "-";
                            resultItem2.d10_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D10) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D10)).ToString("#,###") : "-";
                            resultItem2.d11_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D11) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D11)).ToString("#,###") : "-";
                            resultItem2.d12_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D12) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D12)).ToString("#,###") : "-";
                            resultItem2.d13_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D13) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D13)).ToString("#,###") : "-";
                            resultItem2.d14_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D14) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D14)).ToString("#,###") : "-";
                            resultItem2.d15_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D15) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D15)).ToString("#,###") : "-";
                            resultItem2.d16_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D16) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D16)).ToString("#,###") : "-";
                            resultItem2.d17_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D17) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D17)).ToString("#,###") : "-";
                            resultItem2.d18_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D18) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D18)).ToString("#,###") : "-";
                            resultItem2.d19_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D19) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D19)).ToString("#,###") : "-";
                            resultItem2.d20_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D20) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D20)).ToString("#,###") : "-";
                            resultItem2.d21_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D21) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D21)).ToString("#,###") : "-";
                            resultItem2.d22_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D22) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D22)).ToString("#,###") : "-";
                            resultItem2.d23_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D23) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D23)).ToString("#,###") : "-";
                            resultItem2.d24_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D24) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D24)).ToString("#,###") : "-";
                            resultItem2.d25_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D25) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D25)).ToString("#,###") : "-";
                            resultItem2.d26_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D26) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D26)).ToString("#,###") : "-";
                            resultItem2.d27_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D27) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D27)).ToString("#,###") : "-";
                            resultItem2.d28_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D28) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D28)).ToString("#,###") : "-";
                            resultItem2.d29_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D29) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D29)).ToString("#,###") : "-";
                            resultItem2.d30_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D30) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D30)).ToString("#,###") : "-";
                            resultItem2.d31_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D31) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D31)).ToString("#,###") : "-";
                            owner_list_all.Add(resultItem2);



                            var resultItem3 = new ReportSumSpaceUtilizationViewModel();
                            resultItem3.maxValue_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.MaxValuePeak_Percent) / count).ToString("#,##0.##") + "%";
                            resultItem3.d01_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D01_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d02_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D02_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d03_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D03_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d04_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D04_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d05_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D05_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d06_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D06_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d07_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D07_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d08_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D08_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d09_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D09_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d10_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D10_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d11_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D11_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d12_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D12_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d13_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D13_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d14_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D14_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d15_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D15_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d16_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D16_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d17_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D17_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d18_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D18_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d19_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D19_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d20_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D20_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d21_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D21_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d22_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D22_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d23_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D23_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d24_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D24_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d25_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D25_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d26_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D26_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d27_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D27_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d28_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D28_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d29_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D29_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d30_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D30_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d31_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D31_P) / count).ToString("#,##0.##") + "%";
                            owner_list_all.Add(resultItem3);
                        }
                        else
                        {
                            ////total
                            var resultItem2 = new ReportSumSpaceUtilizationViewModel();
                            resultItem2.owner_Name = "TOTAL";
                            resultItem2.maxValue_ = resultquery_owner.Sum(a => a.MaxValuePeak) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.MaxValuePeak)).ToString("#,###") : "-";
                            resultItem2.d01_ = resultquery_owner.Sum(a => a.D01) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D01)).ToString("#,###") : "-";
                            resultItem2.d02_ = resultquery_owner.Sum(a => a.D02) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D02)).ToString("#,###") : "-";
                            resultItem2.d03_ = resultquery_owner.Sum(a => a.D03) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D03)).ToString("#,###") : "-";
                            resultItem2.d04_ = resultquery_owner.Sum(a => a.D04) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D04)).ToString("#,###") : "-";
                            resultItem2.d05_ = resultquery_owner.Sum(a => a.D05) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D05)).ToString("#,###") : "-";
                            resultItem2.d06_ = resultquery_owner.Sum(a => a.D06) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D06)).ToString("#,###") : "-";
                            resultItem2.d07_ = resultquery_owner.Sum(a => a.D07) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D07)).ToString("#,###") : "-";
                            resultItem2.d08_ = resultquery_owner.Sum(a => a.D08) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D08)).ToString("#,###") : "-";
                            resultItem2.d09_ = resultquery_owner.Sum(a => a.D09) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D09)).ToString("#,###") : "-";
                            resultItem2.d10_ = resultquery_owner.Sum(a => a.D10) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D10)).ToString("#,###") : "-";
                            resultItem2.d11_ = resultquery_owner.Sum(a => a.D11) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D11)).ToString("#,###") : "-";
                            resultItem2.d12_ = resultquery_owner.Sum(a => a.D12) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D12)).ToString("#,###") : "-";
                            resultItem2.d13_ = resultquery_owner.Sum(a => a.D13) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D13)).ToString("#,###") : "-";
                            resultItem2.d14_ = resultquery_owner.Sum(a => a.D14) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D14)).ToString("#,###") : "-";
                            resultItem2.d15_ = resultquery_owner.Sum(a => a.D15) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D15)).ToString("#,###") : "-";
                            resultItem2.d16_ = resultquery_owner.Sum(a => a.D16) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D16)).ToString("#,###") : "-";
                            resultItem2.d17_ = resultquery_owner.Sum(a => a.D17) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D17)).ToString("#,###") : "-";
                            resultItem2.d18_ = resultquery_owner.Sum(a => a.D18) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D18)).ToString("#,###") : "-";
                            resultItem2.d19_ = resultquery_owner.Sum(a => a.D19) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D19)).ToString("#,###") : "-";
                            resultItem2.d20_ = resultquery_owner.Sum(a => a.D20) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D20)).ToString("#,###") : "-";
                            resultItem2.d21_ = resultquery_owner.Sum(a => a.D21) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D21)).ToString("#,###") : "-";
                            resultItem2.d22_ = resultquery_owner.Sum(a => a.D22) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D22)).ToString("#,###") : "-";
                            resultItem2.d23_ = resultquery_owner.Sum(a => a.D23) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D23)).ToString("#,###") : "-";
                            resultItem2.d24_ = resultquery_owner.Sum(a => a.D24) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D24)).ToString("#,###") : "-";
                            resultItem2.d25_ = resultquery_owner.Sum(a => a.D25) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D25)).ToString("#,###") : "-";
                            resultItem2.d26_ = resultquery_owner.Sum(a => a.D26) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D26)).ToString("#,###") : "-";
                            resultItem2.d27_ = resultquery_owner.Sum(a => a.D27) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D27)).ToString("#,###") : "-";
                            resultItem2.d28_ = resultquery_owner.Sum(a => a.D28) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D28)).ToString("#,###") : "-";
                            resultItem2.d29_ = resultquery_owner.Sum(a => a.D29) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D29)).ToString("#,###") : "-";
                            resultItem2.d30_ = resultquery_owner.Sum(a => a.D30) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D30)).ToString("#,###") : "-";
                            resultItem2.d31_ = resultquery_owner.Sum(a => a.D31) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D31)).ToString("#,###") : "-";
                            owner_list_all.Add(resultItem2);


                            var resultItem3 = new ReportSumSpaceUtilizationViewModel();
                            resultItem3.maxValue_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.MaxValuePeak)).ToString("#,##0.##") + "%";
                            resultItem3.d01_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D01_P)).ToString("#,##0.##") + "%";
                            resultItem3.d02_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D02_P)).ToString("#,##0.##") + "%";
                            resultItem3.d03_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D03_P)).ToString("#,##0.##") + "%";
                            resultItem3.d04_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D04_P)).ToString("#,##0.##") + "%";
                            resultItem3.d05_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D05_P)).ToString("#,##0.##") + "%";
                            resultItem3.d06_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D06_P)).ToString("#,##0.##") + "%";
                            resultItem3.d07_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D07_P)).ToString("#,##0.##") + "%";
                            resultItem3.d08_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D08_P)).ToString("#,##0.##") + "%";
                            resultItem3.d09_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D09_P)).ToString("#,##0.##") + "%";
                            resultItem3.d10_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D10_P)).ToString("#,##0.##") + "%";
                            resultItem3.d11_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D11_P)).ToString("#,##0.##") + "%";
                            resultItem3.d12_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D12_P)).ToString("#,##0.##") + "%";
                            resultItem3.d13_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D13_P)).ToString("#,##0.##") + "%";
                            resultItem3.d14_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D14_P)).ToString("#,##0.##") + "%";
                            resultItem3.d15_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D15_P)).ToString("#,##0.##") + "%";
                            resultItem3.d16_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D16_P)).ToString("#,##0.##") + "%";
                            resultItem3.d17_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D17_P)).ToString("#,##0.##") + "%";
                            resultItem3.d18_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D18_P)).ToString("#,##0.##") + "%";
                            resultItem3.d19_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D19_P)).ToString("#,##0.##") + "%";
                            resultItem3.d20_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D20_P)).ToString("#,##0.##") + "%";
                            resultItem3.d21_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D21_P)).ToString("#,##0.##") + "%";
                            resultItem3.d22_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D22_P)).ToString("#,##0.##") + "%";
                            resultItem3.d23_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D23_P)).ToString("#,##0.##") + "%";
                            resultItem3.d24_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D24_P)).ToString("#,##0.##") + "%";
                            resultItem3.d25_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D25_P)).ToString("#,##0.##") + "%";
                            resultItem3.d26_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D26_P)).ToString("#,##0.##") + "%";
                            resultItem3.d27_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D27_P)).ToString("#,##0.##") + "%";
                            resultItem3.d28_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D28_P)).ToString("#,##0.##") + "%";
                            resultItem3.d29_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D29_P)).ToString("#,##0.##") + "%";
                            resultItem3.d30_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D30_P)).ToString("#,##0.##") + "%";
                            resultItem3.d31_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D31_P)).ToString("#,##0.##") + "%";
                            owner_list_all.Add(resultItem3);
                        }
                    }

                    result.location_type_all = location_list_all;
                    result.owner_all = owner_list_all;

                    /////////

                }

                if (data.ambientRoom == "01" || data.ambientRoom == "02")
                {

                    if (resultquery_location.Count == 0)
                    {
                        var resultItem = new ReportSumSpaceUtilizationViewModel();
                        var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.report_date = datereport;
                        resultItem.report_date_to = endDate;
                        resultItem.warehouse = warehouse;                      
                        location_list.Add(resultItem);
                    }
                    else
                    {

                        int num = 1;
                        foreach (var item in resultquery_location)
                        {
                            var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                            var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                            var resultItem = new ReportSumSpaceUtilizationViewModel();
                            resultItem.rowNum = num;
                            if (item.LocationType_Name == "ASRS")
                            {
                                resultItem.rowNum_text = (num++).ToString();
                            }
                            else if (item.LocationType_Name == "ASRS125")
                            {
                                resultItem.rowNum_text = "1.1";
                            }
                            else if (item.LocationType_Name == "ASRS145")
                            {
                                resultItem.rowNum_text = "1.2";
                            }
                            else if (item.LocationType_Name == "ASRS165")
                            {
                                resultItem.rowNum_text = "1.3";
                            }
                            else
                            {
                                resultItem.rowNum_text = (num++).ToString();
                            }
                           
                            if (data.ambientRoom != "02")
                            {
                                resultItem.ambientRoom = "Ambient";
                            }
                            else
                            {
                                resultItem.ambientRoom = "Freeze";
                            }
                            resultItem.locationType_Name = item.LocationType_Name;
                            resultItem.count_Location = item.Count_Location;
                            resultItem.maxValue_ = item.Count_Location > 0 ? Convert.ToInt32(item.Count_Location).ToString("#,###") : "-" ;
                            resultItem.d01_ = item.D01 > 0 ? Convert.ToInt32(item.D01).ToString("#,###"): "-" ;
                            resultItem.d02_ = item.D02 > 0 ? Convert.ToInt32(item.D02).ToString("#,###"): "-" ;
                            resultItem.d03_ = item.D03 > 0 ? Convert.ToInt32(item.D03).ToString("#,###"): "-" ;
                            resultItem.d04_ = item.D04 > 0 ? Convert.ToInt32(item.D04).ToString("#,###"): "-" ;
                            resultItem.d05_ = item.D05 > 0 ? Convert.ToInt32(item.D05).ToString("#,###"): "-" ;
                            resultItem.d06_ = item.D06 > 0 ? Convert.ToInt32(item.D06).ToString("#,###"): "-" ;
                            resultItem.d07_ = item.D07 > 0 ? Convert.ToInt32(item.D07).ToString("#,###"): "-" ;
                            resultItem.d08_ = item.D08 > 0 ? Convert.ToInt32(item.D08).ToString("#,###"): "-" ;
                            resultItem.d09_ = item.D09 > 0 ? Convert.ToInt32(item.D09).ToString("#,###"): "-" ;
                            resultItem.d10_ = item.D10 > 0 ? Convert.ToInt32(item.D10).ToString("#,###"): "-" ;
                            resultItem.d11_ = item.D11 > 0 ? Convert.ToInt32(item.D11).ToString("#,###"): "-" ;
                            resultItem.d12_ = item.D12 > 0 ? Convert.ToInt32(item.D12).ToString("#,###"): "-" ;
                            resultItem.d13_ = item.D13 > 0 ? Convert.ToInt32(item.D13).ToString("#,###"): "-" ;
                            resultItem.d14_ = item.D14 > 0 ? Convert.ToInt32(item.D14).ToString("#,###"): "-" ;
                            resultItem.d15_ = item.D15 > 0 ? Convert.ToInt32(item.D15).ToString("#,###"): "-" ;
                            resultItem.d16_ = item.D16 > 0 ? Convert.ToInt32(item.D16).ToString("#,###"): "-" ;
                            resultItem.d17_ = item.D17 > 0 ? Convert.ToInt32(item.D17).ToString("#,###"): "-" ;
                            resultItem.d18_ = item.D18 > 0 ? Convert.ToInt32(item.D18).ToString("#,###"): "-" ;
                            resultItem.d19_ = item.D19 > 0 ? Convert.ToInt32(item.D19).ToString("#,###"): "-" ;
                            resultItem.d20_ = item.D20 > 0 ? Convert.ToInt32(item.D20).ToString("#,###"): "-" ;
                            resultItem.d21_ = item.D21 > 0 ? Convert.ToInt32(item.D21).ToString("#,###"): "-" ;
                            resultItem.d22_ = item.D22 > 0 ? Convert.ToInt32(item.D22).ToString("#,###"): "-" ;
                            resultItem.d23_ = item.D23 > 0 ? Convert.ToInt32(item.D23).ToString("#,###"): "-" ;
                            resultItem.d24_ = item.D24 > 0 ? Convert.ToInt32(item.D24).ToString("#,###"): "-" ;
                            resultItem.d25_ = item.D25 > 0 ? Convert.ToInt32(item.D25).ToString("#,###"): "-" ;
                            resultItem.d26_ = item.D26 > 0 ? Convert.ToInt32(item.D26).ToString("#,###"): "-" ;
                            resultItem.d27_ = item.D27 > 0 ? Convert.ToInt32(item.D27).ToString("#,###"): "-" ;
                            resultItem.d28_ = item.D28 > 0 ? Convert.ToInt32(item.D28).ToString("#,###"): "-" ;
                            resultItem.d29_ = item.D29 > 0 ? Convert.ToInt32(item.D29).ToString("#,###"): "-" ;
                            resultItem.d30_ = item.D30 > 0 ? Convert.ToInt32(item.D30).ToString("#,###"): "-" ;
                            resultItem.d31_ = item.D31 > 0 ? Convert.ToInt32(item.D31).ToString("#,###") : "-";
                            resultItem.report_date = datereport;
                            resultItem.warehouse = warehouse;
                            location_list.Add(resultItem);

                            /////percent
                            var resultItem_P = new ReportSumSpaceUtilizationViewModel();
                            resultItem_P.maxValue_ = Convert.ToDecimal(item.MaxValuePeak).ToString("#,##0.##") + "%";
                            resultItem_P.d01_ = Convert.ToDecimal(item.D01_P).ToString("#,##0.##") + "%";
                            resultItem_P.d02_ = Convert.ToDecimal(item.D02_P).ToString("#,##0.##") + "%";
                            resultItem_P.d03_ = Convert.ToDecimal(item.D03_P).ToString("#,##0.##") + "%";
                            resultItem_P.d04_ = Convert.ToDecimal(item.D04_P).ToString("#,##0.##") + "%";
                            resultItem_P.d05_ = Convert.ToDecimal(item.D05_P).ToString("#,##0.##") + "%";
                            resultItem_P.d06_ = Convert.ToDecimal(item.D06_P).ToString("#,##0.##") + "%";
                            resultItem_P.d07_ = Convert.ToDecimal(item.D07_P).ToString("#,##0.##") + "%";
                            resultItem_P.d08_ = Convert.ToDecimal(item.D08_P).ToString("#,##0.##") + "%";
                            resultItem_P.d09_ = Convert.ToDecimal(item.D09_P).ToString("#,##0.##") + "%";
                            resultItem_P.d10_ = Convert.ToDecimal(item.D10_P).ToString("#,##0.##") + "%";
                            resultItem_P.d11_ = Convert.ToDecimal(item.D11_P).ToString("#,##0.##") + "%";
                            resultItem_P.d12_ = Convert.ToDecimal(item.D12_P).ToString("#,##0.##") + "%";
                            resultItem_P.d13_ = Convert.ToDecimal(item.D13_P).ToString("#,##0.##") + "%";
                            resultItem_P.d14_ = Convert.ToDecimal(item.D14_P).ToString("#,##0.##") + "%";
                            resultItem_P.d15_ = Convert.ToDecimal(item.D15_P).ToString("#,##0.##") + "%";
                            resultItem_P.d16_ = Convert.ToDecimal(item.D16_P).ToString("#,##0.##") + "%";
                            resultItem_P.d17_ = Convert.ToDecimal(item.D17_P).ToString("#,##0.##") + "%";
                            resultItem_P.d18_ = Convert.ToDecimal(item.D18_P).ToString("#,##0.##") + "%";
                            resultItem_P.d19_ = Convert.ToDecimal(item.D19_P).ToString("#,##0.##") + "%";
                            resultItem_P.d20_ = Convert.ToDecimal(item.D20_P).ToString("#,##0.##") + "%";
                            resultItem_P.d21_ = Convert.ToDecimal(item.D21_P).ToString("#,##0.##") + "%";
                            resultItem_P.d22_ = Convert.ToDecimal(item.D22_P).ToString("#,##0.##") + "%";
                            resultItem_P.d23_ = Convert.ToDecimal(item.D23_P).ToString("#,##0.##") + "%";
                            resultItem_P.d24_ = Convert.ToDecimal(item.D24_P).ToString("#,##0.##") + "%";
                            resultItem_P.d25_ = Convert.ToDecimal(item.D25_P).ToString("#,##0.##") + "%";
                            resultItem_P.d26_ = Convert.ToDecimal(item.D26_P).ToString("#,##0.##") + "%";
                            resultItem_P.d27_ = Convert.ToDecimal(item.D27_P).ToString("#,##0.##") + "%";
                            resultItem_P.d28_ = Convert.ToDecimal(item.D28_P).ToString("#,##0.##") + "%";
                            resultItem_P.d29_ = Convert.ToDecimal(item.D29_P).ToString("#,##0.##") + "%";
                            resultItem_P.d30_ = Convert.ToDecimal(item.D30_P).ToString("#,##0.##") + "%";
                            resultItem_P.d31_ = Convert.ToDecimal(item.D31_P).ToString("#,##0.##") + "%";
                            location_list.Add(resultItem_P);

                        }

                        var count = resultquery_location.Where(c => c.IsCal == 1).Count();
                        if (count > 0)
                        {
                            ////total
                            var resultItem2 = new ReportSumSpaceUtilizationViewModel();
                            resultItem2.locationType_Name = "TOTAL";
                            resultItem2.maxValue_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.Count_Location) > 0 ? Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.Count_Location)).ToString("#,###") : "-";
                            resultItem2.d01_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D01)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D01)).ToString("#,###"): "-";
                            resultItem2.d02_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D02)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D02)).ToString("#,###"): "-";
                            resultItem2.d03_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D03)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D03)).ToString("#,###"): "-";
                            resultItem2.d04_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D04)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D04)).ToString("#,###"): "-";
                            resultItem2.d05_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D05)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D05)).ToString("#,###"): "-";
                            resultItem2.d06_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D06)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D06)).ToString("#,###"): "-";
                            resultItem2.d07_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D07)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D07)).ToString("#,###"): "-";
                            resultItem2.d08_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D08)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D08)).ToString("#,###"): "-";
                            resultItem2.d09_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D09)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D09)).ToString("#,###"): "-";
                            resultItem2.d10_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D10)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D10)).ToString("#,###"): "-";
                            resultItem2.d11_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D11)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D11)).ToString("#,###"): "-";
                            resultItem2.d12_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D12)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D12)).ToString("#,###"): "-";
                            resultItem2.d13_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D13)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D13)).ToString("#,###"): "-";
                            resultItem2.d14_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D14)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D14)).ToString("#,###"): "-";
                            resultItem2.d15_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D15)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D15)).ToString("#,###"): "-";
                            resultItem2.d16_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D16)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D16)).ToString("#,###"): "-";
                            resultItem2.d17_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D17)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D17)).ToString("#,###"): "-";
                            resultItem2.d18_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D18)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D18)).ToString("#,###"): "-";
                            resultItem2.d19_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D19)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D19)).ToString("#,###"): "-";
                            resultItem2.d20_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D20)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D20)).ToString("#,###"): "-";
                            resultItem2.d21_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D21)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D21)).ToString("#,###"): "-";
                            resultItem2.d22_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D22)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D22)).ToString("#,###"): "-";
                            resultItem2.d23_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D23)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D23)).ToString("#,###"): "-";
                            resultItem2.d24_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D24)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D24)).ToString("#,###"): "-";
                            resultItem2.d25_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D25)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D25)).ToString("#,###"): "-";
                            resultItem2.d26_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D26)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D26)).ToString("#,###"): "-";
                            resultItem2.d27_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D27)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D27)).ToString("#,###"): "-";
                            resultItem2.d28_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D28)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D28)).ToString("#,###"): "-";
                            resultItem2.d29_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D29)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D29)).ToString("#,###"): "-";
                            resultItem2.d30_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D30)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D30)).ToString("#,###"): "-";
                            resultItem2.d31_ = resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D31)> 0 ?Convert.ToInt32(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D31)).ToString("#,###"): "-";
                            location_list.Add(resultItem2);                                                                                   



                            var resultItem3 = new ReportSumSpaceUtilizationViewModel();
                            resultItem3.maxValue_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.MaxValuePeak) / count).ToString("#,##0.##") + "%";
                            resultItem3.d01_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D01_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d02_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D02_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d03_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D03_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d04_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D04_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d05_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D05_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d06_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D06_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d07_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D07_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d08_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D08_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d09_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D09_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d10_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D10_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d11_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D11_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d12_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D12_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d13_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D13_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d14_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D14_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d15_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D15_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d16_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D16_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d17_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D17_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d18_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D18_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d19_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D19_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d20_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D20_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d21_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D21_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d22_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D22_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d23_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D23_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d24_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D24_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d25_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D25_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d26_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D26_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d27_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D27_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d28_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D28_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d29_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D29_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d30_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D30_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d31_ = Convert.ToDecimal(resultquery_location.Where(c => c.IsCal == 1).Sum(a => a.D31_P) / count).ToString("#,##0.##") + "%";
                            location_list.Add(resultItem3);
                        }
                        else
                        {
                            ////total
                            var resultItem2 = new ReportSumSpaceUtilizationViewModel();
                            resultItem2.locationType_Name = "TOTAL";
                            resultItem2.maxValue_ = resultquery_location.Sum(a => a.Count_Location) > 0 ? Convert.ToInt32(resultquery_location.Sum(a => a.Count_Location)).ToString("#,###") : "-";
                            resultItem2.d01_ = resultquery_location.Sum(a => a.D01) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D01)).ToString("#,###") : "-";
                            resultItem2.d02_ = resultquery_location.Sum(a => a.D02) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D02)).ToString("#,###") : "-";
                            resultItem2.d03_ = resultquery_location.Sum(a => a.D03) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D03)).ToString("#,###") : "-";
                            resultItem2.d04_ = resultquery_location.Sum(a => a.D04) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D04)).ToString("#,###") : "-";
                            resultItem2.d05_ = resultquery_location.Sum(a => a.D05) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D05)).ToString("#,###") : "-";
                            resultItem2.d06_ = resultquery_location.Sum(a => a.D06) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D06)).ToString("#,###") : "-";
                            resultItem2.d07_ = resultquery_location.Sum(a => a.D07) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D07)).ToString("#,###") : "-";
                            resultItem2.d08_ = resultquery_location.Sum(a => a.D08) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D08)).ToString("#,###") : "-";
                            resultItem2.d09_ = resultquery_location.Sum(a => a.D09) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D09)).ToString("#,###") : "-";
                            resultItem2.d10_ = resultquery_location.Sum(a => a.D10) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D10)).ToString("#,###") : "-";
                            resultItem2.d11_ = resultquery_location.Sum(a => a.D11) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D11)).ToString("#,###") : "-";
                            resultItem2.d12_ = resultquery_location.Sum(a => a.D12) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D12)).ToString("#,###") : "-";
                            resultItem2.d13_ = resultquery_location.Sum(a => a.D13) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D13)).ToString("#,###") : "-";
                            resultItem2.d14_ = resultquery_location.Sum(a => a.D14) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D14)).ToString("#,###") : "-";
                            resultItem2.d15_ = resultquery_location.Sum(a => a.D15) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D15)).ToString("#,###") : "-";
                            resultItem2.d16_ = resultquery_location.Sum(a => a.D16) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D16)).ToString("#,###") : "-";
                            resultItem2.d17_ = resultquery_location.Sum(a => a.D17) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D17)).ToString("#,###") : "-";
                            resultItem2.d18_ = resultquery_location.Sum(a => a.D18) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D18)).ToString("#,###") : "-";
                            resultItem2.d19_ = resultquery_location.Sum(a => a.D19) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D19)).ToString("#,###") : "-";
                            resultItem2.d20_ = resultquery_location.Sum(a => a.D20) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D20)).ToString("#,###") : "-";
                            resultItem2.d21_ = resultquery_location.Sum(a => a.D21) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D21)).ToString("#,###") : "-";
                            resultItem2.d22_ = resultquery_location.Sum(a => a.D22) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D22)).ToString("#,###") : "-";
                            resultItem2.d23_ = resultquery_location.Sum(a => a.D23) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D23)).ToString("#,###") : "-";
                            resultItem2.d24_ = resultquery_location.Sum(a => a.D24) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D24)).ToString("#,###") : "-";
                            resultItem2.d25_ = resultquery_location.Sum(a => a.D25) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D25)).ToString("#,###") : "-";
                            resultItem2.d26_ = resultquery_location.Sum(a => a.D26) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D26)).ToString("#,###") : "-";
                            resultItem2.d27_ = resultquery_location.Sum(a => a.D27) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D27)).ToString("#,###") : "-";
                            resultItem2.d28_ = resultquery_location.Sum(a => a.D28) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D28)).ToString("#,###") : "-";
                            resultItem2.d29_ = resultquery_location.Sum(a => a.D29) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D29)).ToString("#,###") : "-";
                            resultItem2.d30_ = resultquery_location.Sum(a => a.D30) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D30)).ToString("#,###") : "-";
                            resultItem2.d31_ = resultquery_location.Sum(a => a.D31) > 0 ?Convert.ToInt32(resultquery_location.Sum(a => a.D31)).ToString("#,###") : "-";
                            location_list.Add(resultItem2);


                            var resultItem3 = new ReportSumSpaceUtilizationViewModel();
                            resultItem3.maxValue_ = Convert.ToDecimal(resultquery_location.Sum(a => a.MaxValuePeak)).ToString("#,##0.##") + "%";
                            resultItem3.d01_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D01_P)).ToString("#,##0.##") + "%";
                            resultItem3.d02_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D02_P)).ToString("#,##0.##") + "%";
                            resultItem3.d03_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D03_P)).ToString("#,##0.##") + "%";
                            resultItem3.d04_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D04_P)).ToString("#,##0.##") + "%";
                            resultItem3.d05_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D05_P)).ToString("#,##0.##") + "%";
                            resultItem3.d06_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D06_P)).ToString("#,##0.##") + "%";
                            resultItem3.d07_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D07_P)).ToString("#,##0.##") + "%";
                            resultItem3.d08_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D08_P)).ToString("#,##0.##") + "%";
                            resultItem3.d09_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D09_P)).ToString("#,##0.##") + "%";
                            resultItem3.d10_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D10_P)).ToString("#,##0.##") + "%";
                            resultItem3.d11_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D11_P)).ToString("#,##0.##") + "%";
                            resultItem3.d12_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D12_P)).ToString("#,##0.##") + "%";
                            resultItem3.d13_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D13_P)).ToString("#,##0.##") + "%";
                            resultItem3.d14_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D14_P)).ToString("#,##0.##") + "%";
                            resultItem3.d15_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D15_P)).ToString("#,##0.##") + "%";
                            resultItem3.d16_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D16_P)).ToString("#,##0.##") + "%";
                            resultItem3.d17_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D17_P)).ToString("#,##0.##") + "%";
                            resultItem3.d18_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D18_P)).ToString("#,##0.##") + "%";
                            resultItem3.d19_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D19_P)).ToString("#,##0.##") + "%";
                            resultItem3.d20_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D20_P)).ToString("#,##0.##") + "%";
                            resultItem3.d21_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D21_P)).ToString("#,##0.##") + "%";
                            resultItem3.d22_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D22_P)).ToString("#,##0.##") + "%";
                            resultItem3.d23_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D23_P)).ToString("#,##0.##") + "%";
                            resultItem3.d24_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D24_P)).ToString("#,##0.##") + "%";
                            resultItem3.d25_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D25_P)).ToString("#,##0.##") + "%";
                            resultItem3.d26_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D26_P)).ToString("#,##0.##") + "%";
                            resultItem3.d27_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D27_P)).ToString("#,##0.##") + "%";
                            resultItem3.d28_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D28_P)).ToString("#,##0.##") + "%";
                            resultItem3.d29_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D29_P)).ToString("#,##0.##") + "%";
                            resultItem3.d30_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D30_P)).ToString("#,##0.##") + "%";
                            resultItem3.d31_ = Convert.ToDecimal(resultquery_location.Sum(a => a.D31_P)).ToString("#,##0.##") + "%";
                            location_list.Add(resultItem3);
                        }
                    }

                    ////Owner////

                    if (resultquery_owner.Count == 0)
                    {
                        var resultItem = new ReportSumSpaceUtilizationViewModel();
                        var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.report_date = datereport;
                        resultItem.report_date_to = endDate;
                        resultItem.warehouse = warehouse;
                        owner_list.Add(resultItem);
                    }
                    else
                    {

                        int num = 1;
                        foreach (var item in resultquery_owner)
                        {
                            var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                            var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                            var resultItem = new ReportSumSpaceUtilizationViewModel();
                            resultItem.rowNum = num;
                            resultItem.rowNum_text = (num++).ToString();
                            if (data.ambientRoom != "02")
                            {
                                resultItem.ambientRoom = "Ambient";
                            }
                            else
                            {
                                resultItem.ambientRoom = "Freeze";
                            }
                            resultItem.owner_Name = item.Owner_Name;
                            resultItem.maxValue_ = item.MaxValuePeak > 0 ? Convert.ToInt32(item.MaxValuePeak).ToString("#,###") : "-";
                            resultItem.d01_ = item.D01 > 0 ? Convert.ToInt32(item.D01).ToString("#,###") : "-";
                            resultItem.d02_ = item.D02 > 0 ? Convert.ToInt32(item.D02).ToString("#,###") : "-";
                            resultItem.d03_ = item.D03 > 0 ? Convert.ToInt32(item.D03).ToString("#,###") : "-";
                            resultItem.d04_ = item.D04 > 0 ? Convert.ToInt32(item.D04).ToString("#,###") : "-";
                            resultItem.d05_ = item.D05 > 0 ? Convert.ToInt32(item.D05).ToString("#,###") : "-";
                            resultItem.d06_ = item.D06 > 0 ? Convert.ToInt32(item.D06).ToString("#,###") : "-";
                            resultItem.d07_ = item.D07 > 0 ? Convert.ToInt32(item.D07).ToString("#,###") : "-";
                            resultItem.d08_ = item.D08 > 0 ? Convert.ToInt32(item.D08).ToString("#,###") : "-";
                            resultItem.d09_ = item.D09 > 0 ? Convert.ToInt32(item.D09).ToString("#,###") : "-";
                            resultItem.d10_ = item.D10 > 0 ? Convert.ToInt32(item.D10).ToString("#,###") : "-";
                            resultItem.d11_ = item.D11 > 0 ? Convert.ToInt32(item.D11).ToString("#,###") : "-";
                            resultItem.d12_ = item.D12 > 0 ? Convert.ToInt32(item.D12).ToString("#,###") : "-";
                            resultItem.d13_ = item.D13 > 0 ? Convert.ToInt32(item.D13).ToString("#,###") : "-";
                            resultItem.d14_ = item.D14 > 0 ? Convert.ToInt32(item.D14).ToString("#,###") : "-";
                            resultItem.d15_ = item.D15 > 0 ? Convert.ToInt32(item.D15).ToString("#,###") : "-";
                            resultItem.d16_ = item.D16 > 0 ? Convert.ToInt32(item.D16).ToString("#,###") : "-";
                            resultItem.d17_ = item.D17 > 0 ? Convert.ToInt32(item.D17).ToString("#,###") : "-";
                            resultItem.d18_ = item.D18 > 0 ? Convert.ToInt32(item.D18).ToString("#,###") : "-";
                            resultItem.d19_ = item.D19 > 0 ? Convert.ToInt32(item.D19).ToString("#,###") : "-";
                            resultItem.d20_ = item.D20 > 0 ? Convert.ToInt32(item.D20).ToString("#,###") : "-";
                            resultItem.d21_ = item.D21 > 0 ? Convert.ToInt32(item.D21).ToString("#,###") : "-";
                            resultItem.d22_ = item.D22 > 0 ? Convert.ToInt32(item.D22).ToString("#,###") : "-";
                            resultItem.d23_ = item.D23 > 0 ? Convert.ToInt32(item.D23).ToString("#,###") : "-";
                            resultItem.d24_ = item.D24 > 0 ? Convert.ToInt32(item.D24).ToString("#,###") : "-";
                            resultItem.d25_ = item.D25 > 0 ? Convert.ToInt32(item.D25).ToString("#,###") : "-";
                            resultItem.d26_ = item.D26 > 0 ? Convert.ToInt32(item.D26).ToString("#,###") : "-";
                            resultItem.d27_ = item.D27 > 0 ? Convert.ToInt32(item.D27).ToString("#,###") : "-";
                            resultItem.d28_ = item.D28 > 0 ? Convert.ToInt32(item.D28).ToString("#,###") : "-";
                            resultItem.d29_ = item.D29 > 0 ? Convert.ToInt32(item.D29).ToString("#,###") : "-";
                            resultItem.d30_ = item.D30 > 0 ? Convert.ToInt32(item.D30).ToString("#,###") : "-";
                            resultItem.d31_ = item.D31 > 0 ? Convert.ToInt32(item.D31).ToString("#,###") : "-";
                            resultItem.report_date = datereport;
                            resultItem.warehouse = warehouse;
                            owner_list.Add(resultItem);

                            /////percent
                            var resultItem_P = new ReportSumSpaceUtilizationViewModel();
                            resultItem_P.maxValue_ = Convert.ToDecimal(item.MaxValuePeak_Percent).ToString("#,##0.##") + "%";
                            resultItem_P.d01_ = Convert.ToDecimal(item.D01_P).ToString("#,##0.##") + "%";
                            resultItem_P.d02_ = Convert.ToDecimal(item.D02_P).ToString("#,##0.##") + "%";
                            resultItem_P.d03_ = Convert.ToDecimal(item.D03_P).ToString("#,##0.##") + "%";
                            resultItem_P.d04_ = Convert.ToDecimal(item.D04_P).ToString("#,##0.##") + "%";
                            resultItem_P.d05_ = Convert.ToDecimal(item.D05_P).ToString("#,##0.##") + "%";
                            resultItem_P.d06_ = Convert.ToDecimal(item.D06_P).ToString("#,##0.##") + "%";
                            resultItem_P.d07_ = Convert.ToDecimal(item.D07_P).ToString("#,##0.##") + "%";
                            resultItem_P.d08_ = Convert.ToDecimal(item.D08_P).ToString("#,##0.##") + "%";
                            resultItem_P.d09_ = Convert.ToDecimal(item.D09_P).ToString("#,##0.##") + "%";
                            resultItem_P.d10_ = Convert.ToDecimal(item.D10_P).ToString("#,##0.##") + "%";
                            resultItem_P.d11_ = Convert.ToDecimal(item.D11_P).ToString("#,##0.##") + "%";
                            resultItem_P.d12_ = Convert.ToDecimal(item.D12_P).ToString("#,##0.##") + "%";
                            resultItem_P.d13_ = Convert.ToDecimal(item.D13_P).ToString("#,##0.##") + "%";
                            resultItem_P.d14_ = Convert.ToDecimal(item.D14_P).ToString("#,##0.##") + "%";
                            resultItem_P.d15_ = Convert.ToDecimal(item.D15_P).ToString("#,##0.##") + "%";
                            resultItem_P.d16_ = Convert.ToDecimal(item.D16_P).ToString("#,##0.##") + "%";
                            resultItem_P.d17_ = Convert.ToDecimal(item.D17_P).ToString("#,##0.##") + "%";
                            resultItem_P.d18_ = Convert.ToDecimal(item.D18_P).ToString("#,##0.##") + "%";
                            resultItem_P.d19_ = Convert.ToDecimal(item.D19_P).ToString("#,##0.##") + "%";
                            resultItem_P.d20_ = Convert.ToDecimal(item.D20_P).ToString("#,##0.##") + "%";
                            resultItem_P.d21_ = Convert.ToDecimal(item.D21_P).ToString("#,##0.##") + "%";
                            resultItem_P.d22_ = Convert.ToDecimal(item.D22_P).ToString("#,##0.##") + "%";
                            resultItem_P.d23_ = Convert.ToDecimal(item.D23_P).ToString("#,##0.##") + "%";
                            resultItem_P.d24_ = Convert.ToDecimal(item.D24_P).ToString("#,##0.##") + "%";
                            resultItem_P.d25_ = Convert.ToDecimal(item.D25_P).ToString("#,##0.##") + "%";
                            resultItem_P.d26_ = Convert.ToDecimal(item.D26_P).ToString("#,##0.##") + "%";
                            resultItem_P.d27_ = Convert.ToDecimal(item.D27_P).ToString("#,##0.##") + "%";
                            resultItem_P.d28_ = Convert.ToDecimal(item.D28_P).ToString("#,##0.##") + "%";
                            resultItem_P.d29_ = Convert.ToDecimal(item.D29_P).ToString("#,##0.##") + "%";
                            resultItem_P.d30_ = Convert.ToDecimal(item.D30_P).ToString("#,##0.##") + "%";
                            resultItem_P.d31_ = Convert.ToDecimal(item.D31_P).ToString("#,##0.##") + "%";
                            owner_list.Add(resultItem_P);

                        }

                        var count = resultquery_owner.Where(c => c.IsCal == 1).Count();
                        if (count > 0)
                        {
                            ////total
                            var resultItem2 = new ReportSumSpaceUtilizationViewModel();
                            resultItem2.owner_Name = "TOTAL";
                            resultItem2.maxValue_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.MaxValuePeak) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.MaxValuePeak)).ToString("#,###") : "-";
                            resultItem2.d01_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D01) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D01)).ToString("#,###") : "-";
                            resultItem2.d02_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D02) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D02)).ToString("#,###") : "-";
                            resultItem2.d03_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D03) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D03)).ToString("#,###") : "-";
                            resultItem2.d04_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D04) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D04)).ToString("#,###") : "-";
                            resultItem2.d05_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D05) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D05)).ToString("#,###") : "-";
                            resultItem2.d06_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D06) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D06)).ToString("#,###") : "-";
                            resultItem2.d07_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D07) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D07)).ToString("#,###") : "-";
                            resultItem2.d08_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D08) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D08)).ToString("#,###") : "-";
                            resultItem2.d09_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D09) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D09)).ToString("#,###") : "-";
                            resultItem2.d10_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D10) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D10)).ToString("#,###") : "-";
                            resultItem2.d11_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D11) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D11)).ToString("#,###") : "-";
                            resultItem2.d12_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D12) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D12)).ToString("#,###") : "-";
                            resultItem2.d13_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D13) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D13)).ToString("#,###") : "-";
                            resultItem2.d14_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D14) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D14)).ToString("#,###") : "-";
                            resultItem2.d15_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D15) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D15)).ToString("#,###") : "-";
                            resultItem2.d16_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D16) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D16)).ToString("#,###") : "-";
                            resultItem2.d17_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D17) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D17)).ToString("#,###") : "-";
                            resultItem2.d18_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D18) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D18)).ToString("#,###") : "-";
                            resultItem2.d19_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D19) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D19)).ToString("#,###") : "-";
                            resultItem2.d20_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D20) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D20)).ToString("#,###") : "-";
                            resultItem2.d21_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D21) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D21)).ToString("#,###") : "-";
                            resultItem2.d22_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D22) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D22)).ToString("#,###") : "-";
                            resultItem2.d23_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D23) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D23)).ToString("#,###") : "-";
                            resultItem2.d24_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D24) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D24)).ToString("#,###") : "-";
                            resultItem2.d25_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D25) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D25)).ToString("#,###") : "-";
                            resultItem2.d26_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D26) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D26)).ToString("#,###") : "-";
                            resultItem2.d27_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D27) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D27)).ToString("#,###") : "-";
                            resultItem2.d28_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D28) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D28)).ToString("#,###") : "-";
                            resultItem2.d29_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D29) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D29)).ToString("#,###") : "-";
                            resultItem2.d30_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D30) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D30)).ToString("#,###") : "-";
                            resultItem2.d31_ = resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D31) > 0 ? Convert.ToInt32(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D31)).ToString("#,###") : "-";
                            owner_list.Add(resultItem2);



                            var resultItem3 = new ReportSumSpaceUtilizationViewModel();
                            resultItem3.maxValue_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.MaxValuePeak_Percent) / count).ToString("#,##0.##") + "%";
                            resultItem3.d01_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D01_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d02_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D02_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d03_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D03_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d04_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D04_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d05_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D05_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d06_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D06_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d07_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D07_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d08_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D08_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d09_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D09_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d10_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D10_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d11_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D11_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d12_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D12_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d13_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D13_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d14_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D14_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d15_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D15_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d16_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D16_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d17_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D17_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d18_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D18_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d19_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D19_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d20_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D20_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d21_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D21_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d22_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D22_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d23_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D23_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d24_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D24_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d25_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D25_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d26_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D26_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d27_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D27_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d28_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D28_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d29_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D29_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d30_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D30_P) / count).ToString("#,##0.##") + "%";
                            resultItem3.d31_ = Convert.ToDecimal(resultquery_owner.Where(c => c.IsCal == 1).Sum(a => a.D31_P) / count).ToString("#,##0.##") + "%";
                            owner_list.Add(resultItem3);
                        }
                        else
                        {
                            ////total
                            var resultItem2 = new ReportSumSpaceUtilizationViewModel();
                            resultItem2.owner_Name = "TOTAL";
                            resultItem2.maxValue_ = resultquery_owner.Sum(a => a.MaxValuePeak) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.MaxValuePeak)).ToString("#,###") : "-";
                            resultItem2.d01_ = resultquery_owner.Sum(a => a.D01) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D01)).ToString("#,###") : "-";
                            resultItem2.d02_ = resultquery_owner.Sum(a => a.D02) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D02)).ToString("#,###") : "-";
                            resultItem2.d03_ = resultquery_owner.Sum(a => a.D03) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D03)).ToString("#,###") : "-";
                            resultItem2.d04_ = resultquery_owner.Sum(a => a.D04) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D04)).ToString("#,###") : "-";
                            resultItem2.d05_ = resultquery_owner.Sum(a => a.D05) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D05)).ToString("#,###") : "-";
                            resultItem2.d06_ = resultquery_owner.Sum(a => a.D06) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D06)).ToString("#,###") : "-";
                            resultItem2.d07_ = resultquery_owner.Sum(a => a.D07) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D07)).ToString("#,###") : "-";
                            resultItem2.d08_ = resultquery_owner.Sum(a => a.D08) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D08)).ToString("#,###") : "-";
                            resultItem2.d09_ = resultquery_owner.Sum(a => a.D09) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D09)).ToString("#,###") : "-";
                            resultItem2.d10_ = resultquery_owner.Sum(a => a.D10) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D10)).ToString("#,###") : "-";
                            resultItem2.d11_ = resultquery_owner.Sum(a => a.D11) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D11)).ToString("#,###") : "-";
                            resultItem2.d12_ = resultquery_owner.Sum(a => a.D12) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D12)).ToString("#,###") : "-";
                            resultItem2.d13_ = resultquery_owner.Sum(a => a.D13) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D13)).ToString("#,###") : "-";
                            resultItem2.d14_ = resultquery_owner.Sum(a => a.D14) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D14)).ToString("#,###") : "-";
                            resultItem2.d15_ = resultquery_owner.Sum(a => a.D15) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D15)).ToString("#,###") : "-";
                            resultItem2.d16_ = resultquery_owner.Sum(a => a.D16) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D16)).ToString("#,###") : "-";
                            resultItem2.d17_ = resultquery_owner.Sum(a => a.D17) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D17)).ToString("#,###") : "-";
                            resultItem2.d18_ = resultquery_owner.Sum(a => a.D18) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D18)).ToString("#,###") : "-";
                            resultItem2.d19_ = resultquery_owner.Sum(a => a.D19) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D19)).ToString("#,###") : "-";
                            resultItem2.d20_ = resultquery_owner.Sum(a => a.D20) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D20)).ToString("#,###") : "-";
                            resultItem2.d21_ = resultquery_owner.Sum(a => a.D21) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D21)).ToString("#,###") : "-";
                            resultItem2.d22_ = resultquery_owner.Sum(a => a.D22) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D22)).ToString("#,###") : "-";
                            resultItem2.d23_ = resultquery_owner.Sum(a => a.D23) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D23)).ToString("#,###") : "-";
                            resultItem2.d24_ = resultquery_owner.Sum(a => a.D24) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D24)).ToString("#,###") : "-";
                            resultItem2.d25_ = resultquery_owner.Sum(a => a.D25) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D25)).ToString("#,###") : "-";
                            resultItem2.d26_ = resultquery_owner.Sum(a => a.D26) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D26)).ToString("#,###") : "-";
                            resultItem2.d27_ = resultquery_owner.Sum(a => a.D27) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D27)).ToString("#,###") : "-";
                            resultItem2.d28_ = resultquery_owner.Sum(a => a.D28) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D28)).ToString("#,###") : "-";
                            resultItem2.d29_ = resultquery_owner.Sum(a => a.D29) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D29)).ToString("#,###") : "-";
                            resultItem2.d30_ = resultquery_owner.Sum(a => a.D30) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D30)).ToString("#,###") : "-";
                            resultItem2.d31_ = resultquery_owner.Sum(a => a.D31) > 0 ? Convert.ToInt32(resultquery_owner.Sum(a => a.D31)).ToString("#,###") : "-";
                            owner_list.Add(resultItem2);


                            var resultItem3 = new ReportSumSpaceUtilizationViewModel();
                            resultItem3.maxValue_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.MaxValuePeak)).ToString("#,##0.##") + "%";
                            resultItem3.d01_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D01_P)).ToString("#,##0.##") + "%";
                            resultItem3.d02_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D02_P)).ToString("#,##0.##") + "%";
                            resultItem3.d03_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D03_P)).ToString("#,##0.##") + "%";
                            resultItem3.d04_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D04_P)).ToString("#,##0.##") + "%";
                            resultItem3.d05_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D05_P)).ToString("#,##0.##") + "%";
                            resultItem3.d06_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D06_P)).ToString("#,##0.##") + "%";
                            resultItem3.d07_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D07_P)).ToString("#,##0.##") + "%";
                            resultItem3.d08_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D08_P)).ToString("#,##0.##") + "%";
                            resultItem3.d09_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D09_P)).ToString("#,##0.##") + "%";
                            resultItem3.d10_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D10_P)).ToString("#,##0.##") + "%";
                            resultItem3.d11_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D11_P)).ToString("#,##0.##") + "%";
                            resultItem3.d12_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D12_P)).ToString("#,##0.##") + "%";
                            resultItem3.d13_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D13_P)).ToString("#,##0.##") + "%";
                            resultItem3.d14_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D14_P)).ToString("#,##0.##") + "%";
                            resultItem3.d15_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D15_P)).ToString("#,##0.##") + "%";
                            resultItem3.d16_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D16_P)).ToString("#,##0.##") + "%";
                            resultItem3.d17_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D17_P)).ToString("#,##0.##") + "%";
                            resultItem3.d18_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D18_P)).ToString("#,##0.##") + "%";
                            resultItem3.d19_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D19_P)).ToString("#,##0.##") + "%";
                            resultItem3.d20_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D20_P)).ToString("#,##0.##") + "%";
                            resultItem3.d21_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D21_P)).ToString("#,##0.##") + "%";
                            resultItem3.d22_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D22_P)).ToString("#,##0.##") + "%";
                            resultItem3.d23_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D23_P)).ToString("#,##0.##") + "%";
                            resultItem3.d24_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D24_P)).ToString("#,##0.##") + "%";
                            resultItem3.d25_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D25_P)).ToString("#,##0.##") + "%";
                            resultItem3.d26_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D26_P)).ToString("#,##0.##") + "%";
                            resultItem3.d27_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D27_P)).ToString("#,##0.##") + "%";
                            resultItem3.d28_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D28_P)).ToString("#,##0.##") + "%";
                            resultItem3.d29_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D29_P)).ToString("#,##0.##") + "%";
                            resultItem3.d30_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D30_P)).ToString("#,##0.##") + "%";
                            resultItem3.d31_ = Convert.ToDecimal(resultquery_owner.Sum(a => a.D31_P)).ToString("#,##0.##") + "%";
                            owner_list.Add(resultItem3);
                        }
                    }

                    result.location_type = location_list;
                    result.owner = owner_list;
                  
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}


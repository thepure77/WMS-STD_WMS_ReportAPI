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

namespace ReportBusiness.ReportVolumeByShipToPickZoneViewModel
{
    public class ReportVolumeByShipToPickZoneService
    {
        #region printReportLaborPerformance
        public dynamic printReportPan(ReportVolumeByShipToPickZoneViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();
            

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportVolumeByShipToPickZoneViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var statusModels = new List<int?>();
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;

                }

                var sm_date = new SqlParameter("@SM_DATE", dateStart);
                var sm_date_to = new SqlParameter("@SM_DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_ReportVolumeByShipToPickZone>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_ReportVolumeByShipToPickZone.FromSql("sp_ReportVolumeByShipToPickZone @SM_DATE,@SM_DATE_TO", sm_date, sm_date_to).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_ReportVolumeByShipToPickZone.FromSql("sp_ReportVolumeByShipToPickZone @SM_DATE,@SM_DATE_TO", sm_date, sm_date_to).ToList();
                }

                //var resultquery = Master_DBContext.sp_CheckBalanceAllLocation.FromSql("sp_CheckBalanceAllLocation @PRO_ID, @TAG, @LOC", pro_Id, tag, loc).ToList();
                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportVolumeByShipToPickZoneViewModel();
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

                        var resultItem = new ReportVolumeByShipToPickZoneViewModel();
                        resultItem.rowNo = num + 1; ;
                        resultItem.shipment_Date = item.Appointment_Date != null ? item.Appointment_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.shipment_Time = item.Appointment_Time;
                        resultItem.shipment_No = item.TruckLoad_No;
                        resultItem.branch_Code = item.Branch;
                        resultItem.ship_To = item.Shipto_id;
                        resultItem.ship_To_Name = item.Shipto_name;
                        resultItem.province = item.Province;
                        resultItem.aSRS = item.ASRS;
                        resultItem.totebox = item.Totebox;
                        resultItem.labeling = item.Labeling;
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
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportVolumeByShipToPickZone");
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
                //olog.logging("ReportVolumeByShipToPickZone", ex.Message);
                throw ex;
            }
        }
        #endregion


        public string ExportExcel(ReportVolumeByShipToPickZoneViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportVolumeByShipToPickZoneViewModel>();

            try
            {
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var statusModels = new List<int?>();
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;

                }

                var sm_date = new SqlParameter("@SM_DATE", dateStart);
                var sm_date_to = new SqlParameter("@SM_DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_ReportVolumeByShipToPickZone>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_ReportVolumeByShipToPickZone.FromSql("sp_ReportVolumeByShipToPickZone @SM_DATE,@SM_DATE_TO", sm_date, sm_date_to).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_ReportVolumeByShipToPickZone.FromSql("sp_ReportVolumeByShipToPickZone @SM_DATE,@SM_DATE_TO", sm_date, sm_date_to).ToList();
                }

                //var resultquery = Master_DBContext.sp_CheckBalanceAllLocation.FromSql("sp_CheckBalanceAllLocation @PRO_ID, @TAG, @LOC", pro_Id, tag, loc).ToList();
                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportVolumeByShipToPickZoneViewModel();
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

                        var resultItem = new ReportVolumeByShipToPickZoneViewModel();
                        resultItem.rowNo = num + 1; ;
                        resultItem.shipment_Date = item.Appointment_Date != null ? item.Appointment_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.shipment_Time = item.Appointment_Time;
                        resultItem.shipment_No = item.TruckLoad_No;
                        resultItem.branch_Code = item.Branch;
                        resultItem.ship_To = item.Shipto_id;
                        resultItem.ship_To_Name = item.Shipto_name;
                        resultItem.province = item.Province;
                        resultItem.aSRS = item.ASRS;
                        resultItem.totebox = item.Totebox;
                        resultItem.labeling = item.Labeling;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportVolumeByShipToPickZone");

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

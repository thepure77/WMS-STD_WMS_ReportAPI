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

namespace ReportBusiness.VolumeByShipmentPickZone
{
    public class VolumeByShipmentPickZoneService
    {
        public dynamic printReportPan(VolumeByShipmentPickZoneViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();
            //var BB_DBContext = new BinbalanceDbContext();

            //var GI_DBContext = new PlanGIDbContext();
            //@PRO_ID
            //    @PRO_NAME
            //    @TAG
            //    @LOC
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<VolumeByShipmentPickZoneViewModel>();

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

                //var product_Id = "";
                //var product_Name = "";
                //var tag_No = "";
                //var location_Name = "";

                var ship_date = new SqlParameter("@SHIP_DATE", dateStart);
                var ship_date_to = new SqlParameter("@SHIP_DATE_TO", dateEnd);


                var resultquery = new List<MasterDataDataAccess.Models.sp_VolumeByShipment>();

                var room = "";
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_VolumeByShipment.FromSql("sp_VolumeByShipment @SHIP_DATE, @SHIP_DATE_TO", ship_date, ship_date_to).ToList();
                    room = "Ambient";
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_VolumeByShipment.FromSql("sp_VolumeByShipment @SHIP_DATE, @SHIP_DATE_TO", ship_date, ship_date_to).ToList();
                    room = "ห้องเย็น";
                }

                //var resultquery = Master_DBContext.sp_CheckBalanceAllLocation.FromSql("sp_CheckBalanceAllLocation @PRO_ID, @TAG, @LOC", pro_Id, tag, loc).ToList();
                //var resultquery = query.ToList();
                //olog.logging("VolumeByShipmentPickZone", resultquery.Count.ToString());
                if (resultquery.Count == 0)
                {
                    var resultItem = new VolumeByShipmentPickZoneViewModel();
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

                        var resultItem = new VolumeByShipmentPickZoneViewModel();
                        resultItem.rowNo = num + 1;
                        resultItem.shipment_Date = item.Appointment_Date != null ? item.Appointment_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.shipment_Time = item.Appointment_Time;
                        resultItem.shipment_No = item.TruckLoad_No;
                        resultItem.dock = item.Dock_Name;
                        resultItem.good_Issue_No = item.GoodsIssue_No;
                        resultItem.wave_Date = item.GoodsIssue_Date;
                        resultItem.asrs = item.ASRS;
                        resultItem.toteBox = item.Totebox;
                        resultItem.labelling = item.Labeling;
                        resultItem.ambientRoom = room;
                        result.Add(resultItem);
                        num++;
                    }
                }





                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("VolumeByShipmentPickZone");
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
                //olog.logging("VolumeByShipmentPickZone", ex.Message);
                throw ex;
            }
        }

        public string ExportExcel(VolumeByShipmentPickZoneViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();
            //var BB_DBContext = new BinbalanceDbContext();

            //var GI_DBContext = new PlanGIDbContext();
            //@PRO_ID
            //    @PRO_NAME
            //    @TAG
            //    @LOC
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<VolumeByShipmentPickZoneViewModel>();

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

                //var product_Id = "";
                //var product_Name = "";
                //var tag_No = "";
                //var location_Name = "";

                var ship_date = new SqlParameter("@SHIP_DATE", dateStart);
                var ship_date_to = new SqlParameter("@SHIP_DATE_TO", dateEnd);


                var resultquery = new List<MasterDataDataAccess.Models.sp_VolumeByShipment>();

                var room = "";
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_VolumeByShipment.FromSql("sp_VolumeByShipment @SHIP_DATE, @SHIP_DATE_TO", ship_date, ship_date_to).ToList();
                    room = "Ambient";
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_VolumeByShipment.FromSql("sp_VolumeByShipment @SHIP_DATE, @SHIP_DATE_TO", ship_date, ship_date_to).ToList();
                    room = "ห้องเย็น";
                }

                //var resultquery = Master_DBContext.sp_CheckBalanceAllLocation.FromSql("sp_CheckBalanceAllLocation @PRO_ID, @TAG, @LOC", pro_Id, tag, loc).ToList();
                //var resultquery = query.ToList();
                //olog.logging("VolumeByAppointmentPickZone", resultquery.Count.ToString());
                if (resultquery.Count == 0)
                {

                    var resultItem = new VolumeByShipmentPickZoneViewModel();
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

                        var resultItem = new VolumeByShipmentPickZoneViewModel();
                        resultItem.rowNo = num + 1;
                        resultItem.shipment_Date = item.Appointment_Date != null ? item.Appointment_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.shipment_Time = item.Appointment_Time;
                        resultItem.shipment_No = item.TruckLoad_No;
                        resultItem.dock = item.Dock_Name;
                        resultItem.good_Issue_No = item.GoodsIssue_No;
                        resultItem.wave_Date = item.GoodsIssue_Date;
                        resultItem.asrs = item.ASRS;
                        resultItem.toteBox = item.Totebox;
                        resultItem.labelling = item.Labeling;
                        resultItem.ambientRoom = room;
                        result.Add(resultItem);
                        num++;
                    }
                }



                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("VolumeByShipmentPickZone");

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

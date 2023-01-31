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

namespace ReportBusiness.ReportGIByShipmentNoAndProductId
{
    public class ReportGIByShipmentNoAndProductIdService
    {
        #region printReportGIByShipmentNoAndProductId
        public dynamic printReportGIByShipmentNoAndProductId(ReportGIByShipmentNoAndProductIdViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportGIByShipmentNoAndProductIdViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                string product_index = "";
                string businessunit_index = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var statusModels = new List<int?>();
                var sql_ = "";

                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }

                if (!string.IsNullOrEmpty(data.product_Index.ToString()))
                {
                    product_index = data.product_Index.ToString();
                    
                }

                if (data.businessUnitList != null)
                {
                    businessunit_index =  data.businessUnitList.BusinessUnit_Index.ToString();
                }
               
                var pd_index = new SqlParameter("@PD_INDEX", product_index);
                var bu_index = new SqlParameter("@BU_INDEX", businessunit_index);
                var sm_date_st = new SqlParameter("@SM_DT_START", dateStart);
                var sm_date_ed = new SqlParameter("@SM_DT_END", dateEnd);

                var resultquery = new List<MasterDataDataAccess.Models.sp_ReportGIByShipmentNoAndProductId>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_ReportGIByShipmentNoAndProductId.FromSql("sp_ReportGIByShipmentNoAndProductId @PD_INDEX, @BU_INDEX, @SM_DT_START, @SM_DT_END", pd_index, bu_index, sm_date_st, sm_date_ed).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_ReportGIByShipmentNoAndProductId.FromSql("sp_ReportGIByShipmentNoAndProductId @PD_INDEX, @BU_INDEX, @SM_DT_START, @SM_DT_END", pd_index, bu_index, sm_date_st, sm_date_ed).ToList();
                }
               

                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportGIByShipmentNoAndProductIdViewModel();
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

                        var resultItem = new ReportGIByShipmentNoAndProductIdViewModel();
                        resultItem.rowNum = num + 1; ;
                        resultItem.shipment_date = item.TruckLoad_Date != null ? item.TruckLoad_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.shipment_time = item.Appointment_Time;
                        resultItem.shipment_no = item.TruckLoad_No;
                        resultItem.business_unit = item.BusinessUnit_Name;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.su_Order = Convert.ToInt32(item.SU_order);
                        resultItem.su_WhGi_Qty = Convert.ToInt32(item.SU_WH_GI_Qty);
                        resultItem.su_TrGi_Qty = Convert.ToInt32(item.SU_TR_GI_Qty);
                        resultItem.su_UNIT = item.SU_unit;
                        resultItem.su_CBM = item.SU_CBM;
                        resultItem.su_Volume = item.SU_Volume;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportGIByShipmentNoAndProductId");
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
                //olog.logging("ReportGIByShipmentNoAndProductId", ex.Message);
                throw ex;
            }
        }
        #endregion

        public string ExportExcel(ReportGIByShipmentNoAndProductIdViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportGIByShipmentNoAndProductIdViewModel>();
            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                string product_index = "";
                string businessunit_index = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var statusModels = new List<int?>();
                var sql_ = "";

                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }

                if (!string.IsNullOrEmpty(data.product_Index.ToString()))
                {
                    product_index = data.product_Index.ToString();

                }

                if (data.businessUnitList != null)
                {
                    businessunit_index = data.businessUnitList.BusinessUnit_Index.ToString();
                }

                var pd_index = new SqlParameter("@PD_INDEX", product_index);
                var bu_index = new SqlParameter("@BU_INDEX", businessunit_index);
                var sm_date_st = new SqlParameter("@SM_DT_START", dateStart);
                var sm_date_ed = new SqlParameter("@SM_DT_END", dateEnd);

                var resultquery = new List<MasterDataDataAccess.Models.sp_ReportGIByShipmentNoAndProductId>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_ReportGIByShipmentNoAndProductId.FromSql("sp_ReportGIByShipmentNoAndProductId @PD_INDEX, @BU_INDEX, @SM_DT_START, @SM_DT_END", pd_index, bu_index, sm_date_st, sm_date_ed).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_ReportGIByShipmentNoAndProductId.FromSql("sp_ReportGIByShipmentNoAndProductId @PD_INDEX, @BU_INDEX, @SM_DT_START, @SM_DT_END", pd_index, bu_index, sm_date_st, sm_date_ed).ToList();
                }


                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportGIByShipmentNoAndProductIdViewModel();
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

                        var resultItem = new ReportGIByShipmentNoAndProductIdViewModel();
                        resultItem.rowNum = num + 1; ;
                        resultItem.shipment_date = item.TruckLoad_Date != null ? item.TruckLoad_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.shipment_time = item.Appointment_Time;
                        resultItem.shipment_no = item.TruckLoad_No;
                        resultItem.business_unit = item.BusinessUnit_Name;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.su_Order = Convert.ToInt32(item.SU_order);
                        resultItem.su_WhGi_Qty = Convert.ToInt32(item.SU_WH_GI_Qty);
                        resultItem.su_TrGi_Qty = Convert.ToInt32(item.SU_TR_GI_Qty);
                        resultItem.su_UNIT = item.SU_unit;
                        resultItem.su_CBM = item.SU_CBM;
                        resultItem.su_Volume = item.SU_Volume;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportGIByShipmentNoAndProductId");

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
                //olog.logging("ReportGIByShipmentNoAndProductId", ex.Message);
                throw ex;
            }

        }
    }
}

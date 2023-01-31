using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using PlanGRBusiness.Libs;
using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportBusiness.CheckGrPutaway
{
    public class CheckGrPutawayService
    {
        #region printReportLaborPerformance
        public dynamic printReportPan(CheckGrPutawayViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<CheckGrPutawayViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var product_Id = "";
                var goodsReceive_No = "";
                var asp_No = "";

                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                //var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();

                var statusModels = new List<int?>();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                if (!string.IsNullOrEmpty(data.goodsReceive_No))
                {
                    goodsReceive_No = data.goodsReceive_No;
                }
                if (!string.IsNullOrEmpty(data.asp_No))
                {
                    asp_No = data.asp_No;
                }

                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }
                //@PRO_ID
                //@GR
                //@ASN
                //@GR_DATE
                //

                var pro_Id = new SqlParameter("@PRO_ID", product_Id);
                var gr = new SqlParameter("@GR", goodsReceive_No);
                var asn = new SqlParameter("@ASN", asp_No);
                var gr_date = new SqlParameter("@GR_DATE", dateStart);
                var gr_date_to = new SqlParameter("@GR_DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_CheckGrPutaway>();

                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_CheckGrPutaway.FromSql("sp_CheckGrPutaway @PRO_ID , @GR , @ASN , @GR_DATE , @GR_DATE_TO", pro_Id, gr, asn, gr_date, gr_date_to).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_CheckGrPutaway.FromSql("sp_CheckGrPutaway @PRO_ID , @GR , @ASN , @GR_DATE , @GR_DATE_TO", pro_Id, gr, asn, gr_date, gr_date_to).ToList();
                }


                //var resultquery = Master_DBContext.sp_CheckGrPutaway.FromSql("sp_CheckGrPutaway @PRO_ID , @GR , @ASN , @GR_DATE , @GR_DATE_TO", pro_Id, gr, asn, gr_date, gr_date_to).ToList();
                //var resultquery = Master_DBContext.sp_CheckGrPutaway.FromSql("sp_CheckGrPutaway @PRO_ID , @GR , @ASN , @GR_DATE , @GR_DATE_TO", pro_Id , gr , asn , gr_date , gr_date_to).ToList();
                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new CheckGrPutawayViewModel();
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

                        var resultItem = new CheckGrPutawayViewModel();
                        resultItem.rowNo = num + 1; ;
                        resultItem.goodsReceive_No = item.GoodsReceive_No;
                        resultItem.documentType_Name = item.LocationType_Name;
                        resultItem.po_No = item.PO_No;
                        resultItem.asp_No = item.ASN_No;
                        resultItem.appointment_id = item.Appointment_id;
                        resultItem.goodsReceive_Date = item.GoodsReceive_Date != null ? item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.tag_No = item.Tag_No;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.mfg_Date = item.MFG_Date != null ? item.MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.exp_Date = item.EXP_Date != null ? item.EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.bu_Qty = item.BU_Qty;
                        resultItem.bu_Unit = item.BU_Unit;
                        resultItem.su_Qty = item.SU_Qty;
                        resultItem.su_Unit = item.SU_Unit;
                        resultItem.suggest_Location_Name = item.Suggest_Location_Name;
                        resultItem.locationType_Name = item.LocationType_Name;
                        resultItem.putaway_Date = item.Putaway_Date;
                        resultItem.tag_Status = item.Tag_Status;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("CheckGrPutaway");
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
        public string ExportExcel(CheckGrPutawayViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<CheckGrPutawayViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var product_Id = "";
                var goodsReceive_No = "";
                var asp_No = "";

                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                //var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();

                var statusModels = new List<int?>();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                if (!string.IsNullOrEmpty(data.goodsReceive_No))
                {
                    goodsReceive_No = data.goodsReceive_No;
                }
                if (!string.IsNullOrEmpty(data.asp_No))
                {
                    asp_No = data.asp_No;
                }

                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }
                //@PRO_ID
                //@GR
                //@ASN
                //@GR_DATE
                //

                var pro_Id = new SqlParameter("@PRO_ID", product_Id);
                var gr = new SqlParameter("@GR", goodsReceive_No);
                var asn = new SqlParameter("@ASN", asp_No);
                var gr_date = new SqlParameter("@GR_DATE", dateStart);
                var gr_date_to = new SqlParameter("@GR_DATE_TO", dateEnd);

                var resultquery = new List<MasterDataDataAccess.Models.sp_CheckGrPutaway>();

                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_CheckGrPutaway.FromSql("sp_CheckGrPutaway @PRO_ID , @GR , @ASN , @GR_DATE , @GR_DATE_TO", pro_Id, gr, asn, gr_date, gr_date_to).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_CheckGrPutaway.FromSql("sp_CheckGrPutaway @PRO_ID , @GR , @ASN , @GR_DATE , @GR_DATE_TO", pro_Id, gr, asn, gr_date, gr_date_to).ToList();
                }


                //var resultquery = Master_DBContext.sp_CheckGrPutaway.FromSql("sp_CheckGrPutaway @PRO_ID , @GR , @ASN , @GR_DATE , @GR_DATE_TO", pro_Id, gr, asn, gr_date, gr_date_to).ToList();
                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new CheckGrPutawayViewModel();
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

                        var resultItem = new CheckGrPutawayViewModel();
                        resultItem.rowNo = num + 1; ;
                        resultItem.goodsReceive_No = item.GoodsReceive_No;
                        resultItem.documentType_Name = item.LocationType_Name;
                        resultItem.po_No = item.PO_No;
                        resultItem.asp_No = item.ASN_No;
                        resultItem.appointment_id = item.Appointment_id;
                        resultItem.goodsReceive_Date = item.GoodsReceive_Date != null ? item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.tag_No = item.Tag_No;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.mfg_Date = item.MFG_Date != null ? item.MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.exp_Date = item.EXP_Date != null ? item.EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.bu_Qty = item.BU_Qty;
                        resultItem.bu_Unit = item.BU_Unit;
                        resultItem.su_Qty = item.SU_Qty;
                        resultItem.su_Unit = item.SU_Unit;
                        resultItem.suggest_Location_Name = item.Suggest_Location_Name;
                        resultItem.locationType_Name = item.LocationType_Name;
                        resultItem.putaway_Date = item.Putaway_Date;
                        resultItem.tag_Status = item.Tag_Status;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("CheckGrPutaway");

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

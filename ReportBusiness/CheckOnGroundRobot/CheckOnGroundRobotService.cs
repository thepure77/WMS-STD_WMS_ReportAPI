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

namespace ReportBusiness.CheckOnGroundRobot
{
    public class CheckOnGroundRobotService
    {
        #region printReportLaborPerformance
        public dynamic printReportPan(CheckOnGroundRobotViewModel data, string rootPath = "")
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
            var result = new List<CheckOnGroundRobotViewModel>();

            try
            {

                var product_Id = "";
                //var product_Name = "";
                var tagOut_No = "";
                var location = "";

                //var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();

                var statusModels = new List<int?>();

                if (!string.IsNullOrEmpty(data.product_ID))
                {
                    product_Id = data.product_ID;
                }

                //if (!string.IsNullOrEmpty(data.product_Name))
                //{
                //    product_Name = data.product_Name;
                //}
               
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    tagOut_No = data.tag_No;
                }
                if (!string.IsNullOrEmpty(data.location_Name))
                {
                    location = data.location_Name;
                }

                //@PRO_ID
                //@PRO_NAME
                //@TAG
                //@LOC

                var pro_Id = new SqlParameter("@PRO_ID", product_Id);
                //var pro_Name = new SqlParameter("@PRO_NAME", product_Name);
                var loc = new SqlParameter("@LOC", location);
                var tag = new SqlParameter("@TAG", tagOut_No);
                var resultquery = new List<MasterDataDataAccess.Models.sp_CheckOnGroundRobot>();

                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_CheckOnGroundRobot.FromSql("sp_CheckOnGroundRobot @PRO_ID ,@TAG ,@LOC ", pro_Id, tag, loc).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_CheckOnGroundRobot.FromSql("sp_CheckOnGroundRobot @PRO_ID ,@TAG ,@LOC ", pro_Id, tag, loc).ToList();
                }



                //var resultquery = Master_DBContext.sp_CheckOnGroundRobot.FromSql("sp_CheckOnGroundRobot @PRO_ID ,@TAG ,@LOC ", pro_Id, tag, loc).ToList();
                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new CheckOnGroundRobotViewModel();
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

                        var resultItem = new CheckOnGroundRobotViewModel();
                        resultItem.rowNo = num + 1; ;
                        resultItem.product_ID = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.location_Name = item.Location_Name;
                        resultItem.tag_No = item.Tag_No;
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.GoodsIssue_MFG_Date = item.GoodsReceive_MFG_Date != null ? item.GoodsReceive_MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.GoodsIssue_EXP_Date = item.GoodsReceive_EXP_Date != null ? item.GoodsReceive_EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.bu_QtyOnHand = item.BU_QtyOnHand;
                        resultItem.bu_UNIT = item.BU_UNIT;
                        resultItem.su_QtyOnHand = item.SU_QtyOnHand;
                        resultItem.su_UNIT = item.SU_UNIT;
                        resultItem.documentRef_No = item.DocumentRef_No;
                        resultItem.robotGroup = item.RobotGroup;
                        resultItem.erp_Location = item.ERP_Location;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("CheckOnGroundRobot");
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
        public string ExportExcel(CheckOnGroundRobotViewModel data, string rootPath = "")
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
            var result = new List<CheckOnGroundRobotViewModel>();

            try
            {

                var product_Id = "";
                //var product_Name = "";
                var tagOut_No = "";
                var location = "";

                //var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();

                var statusModels = new List<int?>();

                if (!string.IsNullOrEmpty(data.product_ID))
                {
                    product_Id = data.product_ID;
                }

                //if (!string.IsNullOrEmpty(data.product_Name))
                //{
                //    product_Name = data.product_Name;
                //}

                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    tagOut_No = data.tag_No;
                }
                if (!string.IsNullOrEmpty(data.location_Name))
                {
                    location = data.location_Name;
                }

                //@PRO_ID
                //@PRO_NAME
                //@TAG
                //@LOC

                var pro_Id = new SqlParameter("@PRO_ID", product_Id);
                //var pro_Name = new SqlParameter("@PRO_NAME", product_Name);
                var loc = new SqlParameter("@LOC", location);
                var tag = new SqlParameter("@TAG", tagOut_No);
                var resultquery = new List<MasterDataDataAccess.Models.sp_CheckOnGroundRobot>();

                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_CheckOnGroundRobot.FromSql("sp_CheckOnGroundRobot @PRO_ID ,@TAG ,@LOC ", pro_Id, tag, loc).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_CheckOnGroundRobot.FromSql("sp_CheckOnGroundRobot @PRO_ID ,@TAG ,@LOC ", pro_Id, tag, loc).ToList();
                }



                //var resultquery = Master_DBContext.sp_CheckOnGroundRobot.FromSql("sp_CheckOnGroundRobot @PRO_ID ,@TAG ,@LOC ", pro_Id, tag, loc).ToList();
                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new CheckOnGroundRobotViewModel();
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

                        var resultItem = new CheckOnGroundRobotViewModel();
                        resultItem.rowNo = num + 1; ;
                        resultItem.product_ID = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.location_Name = item.Location_Name;
                        resultItem.tag_No = item.Tag_No;
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.GoodsIssue_MFG_Date = item.GoodsReceive_MFG_Date != null ? item.GoodsReceive_MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.GoodsIssue_EXP_Date = item.GoodsReceive_EXP_Date != null ? item.GoodsReceive_EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.bu_QtyOnHand = item.BU_QtyOnHand;
                        resultItem.bu_UNIT = item.BU_UNIT;
                        resultItem.su_QtyOnHand = item.SU_QtyOnHand;
                        resultItem.su_UNIT = item.SU_UNIT;
                        resultItem.documentRef_No = item.DocumentRef_No;
                        resultItem.robotGroup = item.RobotGroup;
                        resultItem.erp_Location = item.ERP_Location;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("CheckOnGroundRobot");

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

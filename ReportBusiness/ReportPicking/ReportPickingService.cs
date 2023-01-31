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

namespace ReportBusiness.ReportPicking
{
    public class ReportPickingService
    {
        #region printReportPicking
        public dynamic printReportPicking(ReportPickingViewModel data, string rootPath = "")
        {

            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportPickingPrintViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);

                var goodsIssue_No = "";
                var truckLoad_No = "";
                var planGoodsIssue_No = "";
                var businessunit_index = "";
                var tag_No = "";
                var product_Id = "";
                var location_Id = "";
                var chut_Id = "";
                var locationType_index = "";
                var itemStatus_Index = "";
                var dateStart_null = "";
                var dateEnd_null = "";
                var tagOut_No = "";

                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var statusModels = new List<int?>();

               


                if (data.businessUnitList != null)
                {
                    businessunit_index = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (data.locationTypeList != null)
                {
                    locationType_index = data.locationTypeList.locationType_Index.ToString();
                }
                if (data.itemStatuList != null)
                {
                    itemStatus_Index = data.itemStatuList.itemStatus_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    goodsIssue_No = data.goodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.truckLoad_No))
                {
                    truckLoad_No = data.truckLoad_No;
                }
                if (!string.IsNullOrEmpty(data.PlanGoodsIssue_No))
                {
                    planGoodsIssue_No = data.PlanGoodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    tag_No = data.tag_No;
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                if (!string.IsNullOrEmpty(data.location_Id))
                {
                    location_Id = data.location_Id;
                }
                if (!string.IsNullOrEmpty(data.chut_Id))
                {
                    chut_Id = data.chut_Id;
                }
                if (!string.IsNullOrEmpty(data.tagOut_No))
                {
                    tagOut_No = data.tagOut_No;
                }

                var gI_Date_From = new SqlParameter();
                var gI_Date_To = new SqlParameter();

                if (!string.IsNullOrEmpty(data.date_Main_Start) && !string.IsNullOrEmpty(data.date_Main_to))
                {
                    dateStart = data.date_Main_Start.toBetweenDate().start;
                    dateEnd = data.date_Main_to.toBetweenDate().end;

                    gI_Date_From = new SqlParameter("@GI_Date_From", dateStart);
                    gI_Date_To = new SqlParameter("@GI_Date_To", dateEnd);
                }
                else
                {
                    gI_Date_From = new SqlParameter("@GI_Date_From", dateStart_null);
                    gI_Date_To = new SqlParameter("@GI_Date_To", dateEnd_null);
                }

                if (data.date_Main_Start == null && data.date_Main_to == null || data.date_Main_Start == "" && data.date_Main_to == "")
                {
                    if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                    {
                        dateStart = data.report_date.toBetweenDate().start;
                        dateEnd = data.report_date_to.toBetweenDate().end;

                        gI_Date_From = new SqlParameter("@GI_Date_From", dateStart);
                        gI_Date_To = new SqlParameter("@GI_Date_To", dateEnd);
                    }
                    else
                    {
                        gI_Date_From = new SqlParameter("@GI_Date_From", dateStart_null);
                        gI_Date_To = new SqlParameter("@GI_Date_To", dateEnd_null);
                    }
                }


                var Bu_Index = new SqlParameter("@BusinessUnit_Index", businessunit_index);
                var GI_No = new SqlParameter("@GoodsIssue_No", goodsIssue_No);
                var TL_No = new SqlParameter("@TruckLoad_No", truckLoad_No);
                var Plan_GI_no = new SqlParameter("@PlanGoodsIssue_No", planGoodsIssue_No);
                var TAG_NO = new SqlParameter("@Tag_No", tag_No);
                var Locat_Type_Index = new SqlParameter("LocationType_Index", locationType_index);
                var Locat_Id = new SqlParameter("@Location_Id", location_Id);
                var Product_Id = new SqlParameter("@Product_Id", product_Id);
                var CHUT_ID = new SqlParameter("@Chute_No", chut_Id);
                var Status_Index = new SqlParameter("@Status", itemStatus_Index);
                var TagOut_No = new SqlParameter("@TagOut_No", tagOut_No);

                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_13_Picking>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_rpt_13_Picking.FromSql("sp_rpt_13_Picking @BusinessUnit_Index , @GoodsIssue_No , @TruckLoad_No , @PlanGoodsIssue_No , @Tag_No , @LocationType_Index , @Location_Id , @Product_Id , @Chute_No , @GI_Date_From , @GI_Date_To , @Status , @TagOut_No"
                        , Bu_Index , GI_No , TL_No , Plan_GI_no , TAG_NO, Locat_Type_Index , Locat_Id , Product_Id , CHUT_ID , gI_Date_From , gI_Date_To , Status_Index , TagOut_No).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_13_Picking.FromSql("sp_rpt_13_Picking @BusinessUnit_Index , @GoodsIssue_No , @TruckLoad_No , @PlanGoodsIssue_No , @Tag_No , @LocationType_Index , @Location_Id , @Product_Id , @Chute_No , @GI_Date_From , @GI_Date_To , @Status , @TagOut_No"
                        , Bu_Index , GI_No , TL_No , Plan_GI_no , TAG_NO , Locat_Type_Index , Locat_Id , Product_Id , CHUT_ID , gI_Date_From , gI_Date_To , Status_Index , TagOut_No).ToList();
                }


                if (resultquery.Count == 0)
                {
                }
                else
                {
                    int num = 0;
                    foreach (var item in resultquery)
                    {

                        var resultItem = new ReportPickingPrintViewModel();
                        resultItem.rowNum = num + 1; ;
                        resultItem.ambientRoom = item.Warehouse_Type;
                        resultItem.businessUnit_Name = item.BusinessUnit_Name;
                        resultItem.goodsIssue_No = item.GoodsIssue_No;
                        resultItem.truckLoad_No = item.TruckLoad_No;
                        resultItem.truckLoad_Date = item.TruckLoad_Date != null ? item.TruckLoad_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No;
                        resultItem.tag_No = item.Tag_No;
                        resultItem.tagOut_No = item.TagOut_No;
                        resultItem.CBM = item.CBM.ToString();
                        resultItem.tote = item.Tote.ToString();
                        resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No;
                        resultItem.locationType_Name = item.LocationType_Name;
                        resultItem.location_Name = item.Location_Name;
                        resultItem.chute_No = item.Chute_No;
                        resultItem.status_Item = item.ItemStatus_Name;
                        result.Add(resultItem);
                        num++;
                    }
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");            
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportPicking");
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
                throw ex;
            }
        }
        #endregion

        public string ExportExcel(ReportPickingViewModel data, string rootPath = "")
        {

            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportPickingPrintViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);

                var goodsIssue_No = "";
                var truckLoad_No = "";
                var planGoodsIssue_No = "";
                var businessunit_index = "";
                var tag_No = "";
                var product_Id = "";
                var location_Id = "";
                var chut_Id = "";
                var locationType_index = "";
                var itemStatus_Index = "";
                var dateStart_null = "";
                var dateEnd_null = "";
                var tagOut_No = "";

                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var statusModels = new List<int?>();




                if (data.businessUnitList != null)
                {
                    businessunit_index = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (data.locationTypeList != null)
                {
                    locationType_index = data.locationTypeList.locationType_Index.ToString();
                }
                if (data.itemStatuList != null)
                {
                    itemStatus_Index = data.itemStatuList.itemStatus_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    goodsIssue_No = data.goodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.truckLoad_No))
                {
                    truckLoad_No = data.truckLoad_No;
                }
                if (!string.IsNullOrEmpty(data.PlanGoodsIssue_No))
                {
                    planGoodsIssue_No = data.PlanGoodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    tag_No = data.tag_No;
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                if (!string.IsNullOrEmpty(data.location_Id))
                {
                    location_Id = data.location_Id;
                }
                if (!string.IsNullOrEmpty(data.chut_Id))
                {
                    chut_Id = data.chut_Id;
                }
                if (!string.IsNullOrEmpty(data.tagOut_No))
                {
                    tagOut_No = data.tagOut_No;
                }

                var gI_Date_From = new SqlParameter();
                var gI_Date_To = new SqlParameter();

                if (!string.IsNullOrEmpty(data.date_Main_Start) && !string.IsNullOrEmpty(data.date_Main_to))
                {
                    dateStart = data.date_Main_Start.toBetweenDate().start;
                    dateEnd = data.date_Main_to.toBetweenDate().end;

                    gI_Date_From = new SqlParameter("@GI_Date_From", dateStart);
                    gI_Date_To = new SqlParameter("@GI_Date_To", dateEnd);
                }
                else
                {
                    gI_Date_From = new SqlParameter("@GI_Date_From", dateStart_null);
                    gI_Date_To = new SqlParameter("@GI_Date_To", dateEnd_null);
                }

                if (data.date_Main_Start == null && data.date_Main_to == null || data.date_Main_Start == "" && data.date_Main_to == "")
                {
                    if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                    {
                        dateStart = data.report_date.toBetweenDate().start;
                        dateEnd = data.report_date_to.toBetweenDate().end;

                        gI_Date_From = new SqlParameter("@GI_Date_From", dateStart);
                        gI_Date_To = new SqlParameter("@GI_Date_To", dateEnd);
                    }
                    else
                    {
                        gI_Date_From = new SqlParameter("@GI_Date_From", dateStart_null);
                        gI_Date_To = new SqlParameter("@GI_Date_To", dateEnd_null);
                    }
                }


                var Bu_Index = new SqlParameter("@BusinessUnit_Index", businessunit_index);
                var GI_No = new SqlParameter("@GoodsIssue_No", goodsIssue_No);
                var TL_No = new SqlParameter("@TruckLoad_No", truckLoad_No);
                var Plan_GI_no = new SqlParameter("@PlanGoodsIssue_No", planGoodsIssue_No);
                var TAG_NO = new SqlParameter("@Tag_No", tag_No);
                var Locat_Type_Index = new SqlParameter("LocationType_Index", locationType_index);
                var Locat_Id = new SqlParameter("@Location_Id", location_Id);
                var Product_Id = new SqlParameter("@Product_Id", product_Id);
                var CHUT_ID = new SqlParameter("@Chute_No", chut_Id);
                var Status_Index = new SqlParameter("@Status", itemStatus_Index);
                var TagOut_No = new SqlParameter("@TagOut_No", tagOut_No);

                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_13_Picking>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_rpt_13_Picking.FromSql("sp_rpt_13_Picking @BusinessUnit_Index , @GoodsIssue_No , @TruckLoad_No , @PlanGoodsIssue_No , @Tag_No , @LocationType_Index , @Location_Id , @Product_Id , @Chute_No , @GI_Date_From , @GI_Date_To , @Status , @TagOut_No"
                        , Bu_Index, GI_No, TL_No, Plan_GI_no, TAG_NO, Locat_Type_Index, Locat_Id, Product_Id, CHUT_ID, gI_Date_From, gI_Date_To, Status_Index, TagOut_No).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_13_Picking.FromSql("sp_rpt_13_Picking @BusinessUnit_Index , @GoodsIssue_No , @TruckLoad_No , @PlanGoodsIssue_No , @Tag_No , @LocationType_Index , @Location_Id , @Product_Id , @Chute_No , @GI_Date_From , @GI_Date_To , @Status , @TagOut_No"
                        , Bu_Index, GI_No, TL_No, Plan_GI_no, TAG_NO, Locat_Type_Index, Locat_Id, Product_Id, CHUT_ID, gI_Date_From, gI_Date_To, Status_Index, TagOut_No).ToList();
                }


                if (resultquery.Count == 0)
                {
                }
                else
                {
                    int num = 0;
                    foreach (var item in resultquery)
                    {

                        var resultItem = new ReportPickingPrintViewModel();
                        resultItem.rowNum = num + 1; ;
                        resultItem.ambientRoom = item.Warehouse_Type;
                        resultItem.businessUnit_Name = item.BusinessUnit_Name;
                        resultItem.goodsIssue_No = item.GoodsIssue_No;
                        resultItem.truckLoad_No = item.TruckLoad_No;
                        resultItem.truckLoad_Date = item.TruckLoad_Date != null ? item.TruckLoad_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No;
                        resultItem.tag_No = item.Tag_No;
                        resultItem.tagOut_No = item.TagOut_No;
                        resultItem.CBM = item.CBM.ToString();
                        resultItem.tote = item.Tote.ToString();
                        resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No;
                        resultItem.locationType_Name = item.LocationType_Name;
                        resultItem.location_Name = item.Location_Name;
                        resultItem.chute_No = item.Chute_No;
                        resultItem.status_Item = item.ItemStatus_Name;
                        result.Add(resultItem);
                        num++;
                    }
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportPicking");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "tmpReport" + DateTime.Now.ToString("yyyyMMddHHmmss");

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

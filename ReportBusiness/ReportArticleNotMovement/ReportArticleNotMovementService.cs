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

namespace ReportBusiness.ReportArticleNotMovement
{
    public class ReportArticleNotMovementService
    {
        #region printReportPicking
        public dynamic printArticleNotMovement(ReportArticleNotMovementViewModel data, string rootPath = "")
        {

            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportArticleNotMovementPrintViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);

                var product_Id = "";
                var tag_No = "";
                var quantity = "";
                var locationType_index = "";
                var businessunit_index = "";
                var product_Lot = "";
                
                
                var dateStart_null = "";
                var dateEnd_null = "";
                var dateNowStart_null = "";
                var dateNowEnd_null = "";
                var expStart_null = "";
                var expEnd_null = "";
                var mfgStart_null = "";
                var mfgEnd_null = "";

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
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    tag_No = data.tag_No;
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                if (!string.IsNullOrEmpty(data.batch))
                {
                    product_Lot = data.batch;
                }
                if (!string.IsNullOrEmpty(data.quantity))
                {
                    quantity = data.quantity;
                }


                var create_Date_From = new SqlParameter();
                var create_Date_To = new SqlParameter();
                var date_From = "";
                var date_To = "";
                var date_Null = "";

                var summary_Date = "";
                var total_Date = "";

                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;

                    create_Date_From = new SqlParameter("@Update_Date", dateStart);
                    create_Date_To = new SqlParameter("@Update_Date_To", dateEnd);

                    date_From = dateStart.ToString("dd/MM/yyyy");
                    date_To = dateEnd.ToString("dd/MM/yyyy");

                    summary_Date = "ข้อมูลวันที่" + " " + date_From + " " + "ถึงวันที่" + " " + date_To;
                    total_Date = summary_Date;
                }
                else
                {
                    create_Date_From = new SqlParameter("@Update_Date", dateStart_null);
                    create_Date_To = new SqlParameter("@Update_Date_To", dateEnd_null);

                    date_Null = "ไม่ระบุวันที่";
                    total_Date = date_Null;
                }

                if (data.report_date == null && data.report_date_to == null || data.report_date == "" && data.report_date_to == "")
                {
                    if (!string.IsNullOrEmpty(data.date_Now_From) && !string.IsNullOrEmpty(data.date_Now_To))
                    {
                        dateStart = data.date_Now_From.toBetweenDate().start;
                        dateEnd = data.date_Now_To.toBetweenDate().end;

                        create_Date_From = new SqlParameter("@Update_Date", dateStart);
                        create_Date_To = new SqlParameter("@Update_Date_To", dateEnd);

                        date_From = dateStart.ToString("dd/MM/yyyy");
                        date_To = dateEnd.ToString("dd/MM/yyyy");

                        summary_Date = "ข้อมูลวันที่" + " " + date_From + " " + "ถึงวันที่" + " " + date_To;
                        total_Date = summary_Date;
                    }
                    else
                    {
                        create_Date_From = new SqlParameter("@Update_Date", dateNowStart_null);
                        create_Date_To = new SqlParameter("@Update_Date_To", dateNowEnd_null);

                        date_Null = "ไม่ระบุวันที่";
                        total_Date = date_Null;
                    }
                }

                var exp_Date_From = new SqlParameter();
                var exp_Date_To = new SqlParameter();

                if (!string.IsNullOrEmpty(data.exp_Date_From) && !string.IsNullOrEmpty(data.exp_Date_To))
                {
                    dateStart = data.exp_Date_From.toBetweenDate().start;
                    dateEnd = data.exp_Date_To.toBetweenDate().end;

                    exp_Date_From = new SqlParameter("Exp_Date", dateStart);
                    exp_Date_To = new SqlParameter("@Exp_Date_To", dateEnd);
                }
                else
                {
                    exp_Date_From = new SqlParameter("Exp_Date", expStart_null);
                    exp_Date_To = new SqlParameter("@Exp_Date_To", expEnd_null);
                }

                var mfg_Date_From = new SqlParameter();
                var mfg_Date_To = new SqlParameter();

                if (!string.IsNullOrEmpty(data.mfg_Date_From) && !string.IsNullOrEmpty(data.mfg_Date_To))
                {
                    dateStart = data.mfg_Date_From.toBetweenDate().start;
                    dateEnd = data.mfg_Date_To.toBetweenDate().end;

                    mfg_Date_From = new SqlParameter("@Mfg_Date", dateStart);
                    mfg_Date_To = new SqlParameter("@Mfg_Date_To", dateEnd);
                }
                else
                {
                    mfg_Date_From = new SqlParameter("@Mfg_Date", mfgStart_null);
                    mfg_Date_To = new SqlParameter("@Mfg_Date_To", mfgEnd_null);
                }

             
                var Product_Id = new SqlParameter("@Product_Id", product_Id);
                var TAG_NO = new SqlParameter("@Tag_No", tag_No);
                var Day = new SqlParameter("@Day", quantity);
                var Bu_Index = new SqlParameter("@BusinessUnit_Index", businessunit_index);
                var Locat_Type_Index = new SqlParameter("@LocationType_Index", locationType_index);
                var Product_Lot = new SqlParameter("@Batch_Lot", product_Lot);
                
                

               

                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_17_Not_Movement>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_rpt_17_Not_Movement.FromSql("sp_rpt_17_Not_Movement @Product_Id , @Tag_No , @Day , @Update_Date , @Update_Date_To , @LocationType_Index , @BusinessUnit_Index , @Batch_Lot , @Exp_Date , @Exp_Date_To , @Mfg_Date ,@Mfg_Date_To "
                     , Product_Id , TAG_NO , Day , create_Date_From , create_Date_To , Locat_Type_Index , Bu_Index , Product_Lot , exp_Date_From , exp_Date_To , mfg_Date_From , mfg_Date_To).ToList();

                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_17_Not_Movement.FromSql("sp_rpt_17_Not_Movement @Product_Id , @Tag_No , @Day , @Update_Date , @Update_Date_To , @LocationType_Index , @BusinessUnit_Index , @Batch_Lot , @Exp_Date , @Exp_Date_To , @Mfg_Date ,@Mfg_Date_To "
                     , Product_Id, TAG_NO, Day, create_Date_From, create_Date_To, Locat_Type_Index, Bu_Index, Product_Lot, exp_Date_From, exp_Date_To, mfg_Date_From, mfg_Date_To).ToList();
                }


                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportArticleNotMovementPrintViewModel();
                    result.Add(resultItem);
                }
                else
                {
                
                    foreach (var item in resultquery)
                    {

                        var resultItem = new ReportArticleNotMovementPrintViewModel();
                        resultItem.date = total_Date;
                        resultItem.row_num = item.Row_No;
                        resultItem.ambientRoom = item.Warehouse_Type;
                        resultItem.tag_No = item.Tag_No;
                        resultItem.location = item.Location_Name;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.business_Name = item.BusinessUnit_Name;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.su_Unit = item.Su_Unit;
                        resultItem.su_Qty = item.Su_Qty.ToString();
                        resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.diff_Movement = item.Diff_Movement.ToString();
                        resultItem.wms_Sloc = item.WMS_Sloc;
                        resultItem.sap_Sloc = item.SAP_Sloc;
                        resultItem.status_Item = item.ItemStatus_Name;
                        resultItem.mfg_date = item.GoodsReceive_MFG_Date != null ? item.GoodsReceive_MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.exp_date = item.GoodsReceive_EXP_Date != null ? item.GoodsReceive_EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.product_Lot = item.Product_Lot;
                        result.Add(resultItem);
                       
                    }
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportArticleNotMovement");
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


        public string ExportExcel(ReportArticleNotMovementViewModel data, string rootPath = "")
        {

            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportArticleNotMovementPrintViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);

                var product_Id = "";
                var tag_No = "";
                var quantity = "";
                var locationType_index = "";
                var businessunit_index = "";
                var product_Lot = "";


                var dateStart_null = "";
                var dateEnd_null = "";
                var dateNowStart_null = "";
                var dateNowEnd_null = "";
                var expStart_null = "";
                var expEnd_null = "";
                var mfgStart_null = "";
                var mfgEnd_null = "";

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
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    tag_No = data.tag_No;
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                if (!string.IsNullOrEmpty(data.batch))
                {
                    product_Lot = data.batch;
                }
                if (!string.IsNullOrEmpty(data.quantity))
                {
                    quantity = data.quantity;
                }


                var create_Date_From = new SqlParameter();
                var create_Date_To = new SqlParameter();
                var date_From = "";
                var date_To = "";
                var date_Null = "";

                var summary_Date = "";
                var total_Date = "";

                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;

                    create_Date_From = new SqlParameter("@Update_Date", dateStart);
                    create_Date_To = new SqlParameter("@Update_Date_To", dateEnd);

                    date_From = dateStart.ToString("dd/MM/yyyy");
                    date_To = dateEnd.ToString("dd/MM/yyyy");

                    summary_Date = "ข้อมูลวันที่" + " " + date_From + " " + "ถึงวันที่" + " " + date_To;
                    total_Date = summary_Date;
                }
                else
                {
                    create_Date_From = new SqlParameter("@Update_Date", dateStart_null);
                    create_Date_To = new SqlParameter("@Update_Date_To", dateEnd_null);

                    date_Null = "ไม่ระบุวันที่";
                    total_Date = date_Null;
                }

                if (data.report_date == null && data.report_date_to == null || data.report_date == "" && data.report_date_to == "")
                {
                    if (!string.IsNullOrEmpty(data.date_Now_From) && !string.IsNullOrEmpty(data.date_Now_To))
                    {
                        dateStart = data.date_Now_From.toBetweenDate().start;
                        dateEnd = data.date_Now_To.toBetweenDate().end;

                        create_Date_From = new SqlParameter("@Update_Date", dateStart);
                        create_Date_To = new SqlParameter("@Update_Date_To", dateEnd);

                        date_From = dateStart.ToString("dd/MM/yyyy");
                        date_To = dateEnd.ToString("dd/MM/yyyy");

                        summary_Date = "ข้อมูลวันที่" + " " + date_From + " " + "ถึงวันที่" + " " + date_To;
                        total_Date = summary_Date;
                    }
                    else
                    {
                        create_Date_From = new SqlParameter("@Update_Date", dateNowStart_null);
                        create_Date_To = new SqlParameter("@Update_Date_To", dateNowEnd_null);

                        date_Null = "ไม่ระบุวันที่";
                        total_Date = date_Null;
                    }
                }

                var exp_Date_From = new SqlParameter();
                var exp_Date_To = new SqlParameter();

                if (!string.IsNullOrEmpty(data.exp_Date_From) && !string.IsNullOrEmpty(data.exp_Date_To))
                {
                    dateStart = data.exp_Date_From.toBetweenDate().start;
                    dateEnd = data.exp_Date_To.toBetweenDate().end;

                    exp_Date_From = new SqlParameter("Exp_Date", dateStart);
                    exp_Date_To = new SqlParameter("@Exp_Date_To", dateEnd);
                }
                else
                {
                    exp_Date_From = new SqlParameter("Exp_Date", expStart_null);
                    exp_Date_To = new SqlParameter("@Exp_Date_To", expEnd_null);
                }

                var mfg_Date_From = new SqlParameter();
                var mfg_Date_To = new SqlParameter();

                if (!string.IsNullOrEmpty(data.mfg_Date_From) && !string.IsNullOrEmpty(data.mfg_Date_To))
                {
                    dateStart = data.mfg_Date_From.toBetweenDate().start;
                    dateEnd = data.mfg_Date_To.toBetweenDate().end;

                    mfg_Date_From = new SqlParameter("@Mfg_Date", dateStart);
                    mfg_Date_To = new SqlParameter("@Mfg_Date_To", dateEnd);
                }
                else
                {
                    mfg_Date_From = new SqlParameter("@Mfg_Date", mfgStart_null);
                    mfg_Date_To = new SqlParameter("@Mfg_Date_To", mfgEnd_null);
                }


                var Product_Id = new SqlParameter("@Product_Id", product_Id);
                var TAG_NO = new SqlParameter("@Tag_No", tag_No);
                var Day = new SqlParameter("@Day", quantity);
                var Bu_Index = new SqlParameter("@BusinessUnit_Index", businessunit_index);
                var Locat_Type_Index = new SqlParameter("@LocationType_Index", locationType_index);
                var Product_Lot = new SqlParameter("@Batch_Lot", product_Lot);





                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_17_Not_Movement>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_rpt_17_Not_Movement.FromSql("sp_rpt_17_Not_Movement @Product_Id , @Tag_No , @Day , @Update_Date , @Update_Date_To , @LocationType_Index , @BusinessUnit_Index , @Batch_Lot , @Exp_Date , @Exp_Date_To , @Mfg_Date ,@Mfg_Date_To "
                     , Product_Id, TAG_NO, Day, create_Date_From, create_Date_To, Locat_Type_Index, Bu_Index, Product_Lot, exp_Date_From, exp_Date_To, mfg_Date_From, mfg_Date_To).ToList();

                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_17_Not_Movement.FromSql("sp_rpt_17_Not_Movement @Product_Id , @Tag_No , @Day , @Update_Date , @Update_Date_To , @LocationType_Index , @BusinessUnit_Index , @Batch_Lot , @Exp_Date , @Exp_Date_To , @Mfg_Date ,@Mfg_Date_To "
                     , Product_Id, TAG_NO, Day, create_Date_From, create_Date_To, Locat_Type_Index, Bu_Index, Product_Lot, exp_Date_From, exp_Date_To, mfg_Date_From, mfg_Date_To).ToList();
                }


                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportArticleNotMovementPrintViewModel();
                    result.Add(resultItem);
                }
                else
                {

                    foreach (var item in resultquery)
                    {

                        var resultItem = new ReportArticleNotMovementPrintViewModel();
                        resultItem.date = total_Date;
                        resultItem.row_num = item.Row_No;
                        resultItem.ambientRoom = item.Warehouse_Type;
                        resultItem.tag_No = item.Tag_No;
                        resultItem.location = item.Location_Name;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.business_Name = item.BusinessUnit_Name;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.su_Unit = item.Su_Unit;
                        resultItem.su_Qty = item.Su_Qty.ToString();
                        resultItem.update_Date = item.Update_Date != null ? item.Update_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.diff_Movement = item.Diff_Movement.ToString();
                        resultItem.wms_Sloc = item.WMS_Sloc;
                        resultItem.sap_Sloc = item.SAP_Sloc;
                        resultItem.status_Item = item.ItemStatus_Name;
                        resultItem.mfg_date = item.GoodsReceive_MFG_Date != null ? item.GoodsReceive_MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.exp_date = item.GoodsReceive_EXP_Date != null ? item.GoodsReceive_EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.product_Lot = item.Product_Lot;
                        result.Add(resultItem);

                    }
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportArticleNotMovement");
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

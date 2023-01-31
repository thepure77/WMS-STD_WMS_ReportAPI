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
using System.IO;
using System.Linq;
using System.Text;

namespace ReportBusiness.ReportRecallOutbound
{
    public class ReportRecallOutboundService
    {

        #region printReport
        public dynamic printReportRecall(ReportRecallOutboundRequestModel data, string rootPath = "" )
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportRecallOutboundExModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);

                var convert_Type_query = new List<MasterDataDataAccess.Models.View_ReportRecall_Outbound_Excel>();
                var query = convert_Type_query.AsQueryable();
                var room_Name = "";
                if (data.ambientRoom != "02")
                {
                    query = Master_DBContext.View_ReportRecall_Outbound_Excel.AsQueryable();
                    room_Name = "Ambient";
                }
                else
                {
                    query = temp_Master_DBContext.View_ReportRecall_Outbound_Excel.AsQueryable();
                    room_Name = "Freeze";
                }
                //var query = Master_DBContext.View_ReportRecall_Outbound_Excel.AsQueryable();
                if (data.advanceSearch == true)
                {
                    //เช็ตแถวแรก
                    if (!string.IsNullOrEmpty(data.materialNo))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] materNo_list = data.materialNo.Split(spearator, count, StringSplitOptions.None);
                        //var multi_mater = string.arr_material.Where(c => arr_material.Contains(c.Product_Id));

                        query = query.Where(c => materNo_list.Contains(c.Product_Id));
                        //query = query.Where(c => c.Product_Id.Contains(data.materialNo));
                    }
                    if (!string.IsNullOrEmpty(data.goodsIssue_date) && !string.IsNullOrEmpty(data.goodsIssue_date_to))
                    {
                        var dateStart = data.goodsIssue_date.toBetweenDate();
                        var dateEnd = data.goodsIssue_date_to.toBetweenDate();
                        query = query.Where(c => c.GoodsIssue_Date >= dateStart.start && c.GoodsIssue_Date <= dateEnd.end);

                    }
                    if (!string.IsNullOrEmpty(data.date_load) && !string.IsNullOrEmpty(data.date_load_to))
                    {
                        var dateStart = data.date_load.toBetweenDate();
                        var dateEnd = data.date_load_to.toBetweenDate();
                        query = query.Where(c => c.truckLoad_Date >= dateStart.start && c.truckLoad_Date <= dateEnd.end);
                    }
                    if (!string.IsNullOrEmpty(data.shipTo_ID))
                    {
                        query = query.Where(c => c.ShipTo_Id.Contains(data.shipTo_ID)
                                            || c.ShipTo_Name.Contains(data.shipTo_ID));
                    }

                    //เช็ตแถวสอง
                    if (!string.IsNullOrEmpty(data.Branch))
                    {
                        query = query.Where(c => c.Branch_Code.Contains(data.Branch));
                    }
                    if (!string.IsNullOrEmpty(data.Province))
                    {
                        query = query.Where(c => c.Province_Name.Contains(data.Province)
                                            || c.Province_Id.Contains(data.Province));
                    }
                    if (!string.IsNullOrEmpty(data.planGoodsIssue_No))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] doNo_list = data.planGoodsIssue_No.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => doNo_list.Contains(c.PlanGoodsIssue_No));
                        //query = query.Where(c => c.PlanGoodsIssue_No.Contains(data.planGoodsIssue_No));
                    }
                    if (!string.IsNullOrEmpty(data.billing_macdoc))
                    {
                        query = query.Where(c => c.Billing_Matdoc.Contains(data.billing_macdoc));
                    }

                    //เช็ตแถวสาม
                    if (!string.IsNullOrEmpty(data.NoTag))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] tag_list = data.NoTag.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => tag_list.Contains(c.Tag_No));
                        //query = query.Where(c => c.Tag_No.Contains(data.NoTag));
                    }
                    if (!string.IsNullOrEmpty(data.date_mfg) && !string.IsNullOrEmpty(data.date_mfg_to))
                    {
                        var dateStart = data.date_mfg.toBetweenDate();
                        var dateEnd = data.date_mfg_to.toBetweenDate();
                        query = query.Where(c => c.MFG_Date >= dateStart.start && c.MFG_Date <= dateEnd.end);
                    }
                    if (!string.IsNullOrEmpty(data.date_exp) && !string.IsNullOrEmpty(data.date_exp_to))
                    {
                        var dateStart = data.date_exp.toBetweenDate();
                        var dateEnd = data.date_exp_to.toBetweenDate();
                        query = query.Where(c => c.EXP_Date >= dateStart.start && c.EXP_Date <= dateEnd.end);
                    }
                    if (!string.IsNullOrEmpty(data.batch_lot))
                    {
                        query = query.Where(c => c.Product_Lot_GI.Contains(data.batch_lot) || c.Product_Lot_GI.Contains(data.batch_lot));
                    }

                    //เช็ตแถวสี่

                    if (!string.IsNullOrEmpty(data.sloc))
                    {
                        query = query.Where(c => c.ERP_Location.Contains(data.sloc));
                    }
                    if (!string.IsNullOrEmpty(data.Match))
                    {
                        query = query.Where(c => c.Match_Name.Contains(data.Match));
                    }
                }
                else
                {
                    //เช็ตแถวแรก
                    if (!string.IsNullOrEmpty(data.materialNo))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] materNo_list = data.materialNo.Split(spearator, count, StringSplitOptions.None);
                        //var multi_mater = string.arr_material.Where(c => arr_material.Contains(c.Product_Id));

                        query = query.Where(c => materNo_list.Contains(c.Product_Id));
                        //query = query.Where(c => c.Product_Id.Contains(data.materialNo));
                    }
                    if (!string.IsNullOrEmpty(data.goodsIssue_date) && !string.IsNullOrEmpty(data.goodsIssue_date_to))
                    {
                        var dateStart = data.goodsIssue_date.toBetweenDate();
                        var dateEnd = data.goodsIssue_date_to.toBetweenDate();
                        query = query.Where(c => c.GoodsIssue_Date >= dateStart.start && c.GoodsIssue_Date <= dateEnd.end);

                    }
                    if (!string.IsNullOrEmpty(data.date_load) && !string.IsNullOrEmpty(data.date_load_to))
                    {
                        var dateStart = data.date_load.toBetweenDate();
                        var dateEnd = data.date_load_to.toBetweenDate();
                        query = query.Where(c => c.truckLoad_Date >= dateStart.start && c.truckLoad_Date <= dateEnd.end);
                    }
                    if (!string.IsNullOrEmpty(data.shipTo_ID))
                    {
                        query = query.Where(c => c.ShipTo_Id.Contains(data.shipTo_ID)
                                            || c.ShipTo_Name.Contains(data.shipTo_ID));
                    }

                    //เช็ตแถวสอง
                    if (!string.IsNullOrEmpty(data.Branch))
                    {
                        query = query.Where(c => c.Branch_Code.Contains(data.Branch));
                    }
                    if (!string.IsNullOrEmpty(data.Province))
                    {
                        query = query.Where(c => c.Province_Name.Contains(data.Province)
                                            || c.Province_Id.Contains(data.Province));
                    }
                    if (!string.IsNullOrEmpty(data.planGoodsIssue_No))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] doNo_list = data.planGoodsIssue_No.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => doNo_list.Contains(c.PlanGoodsIssue_No));
                        //query = query.Where(c => c.PlanGoodsIssue_No.Contains(data.planGoodsIssue_No));
                    }
                    if (!string.IsNullOrEmpty(data.billing_macdoc))
                    {
                        query = query.Where(c => c.Billing_Matdoc.Contains(data.billing_macdoc));
                    }
                }

                var resultquery = query.OrderBy(o=>o.Tag_No).ToList();
                //Orderby 
                //Tag_No
                //GoodsIssue_No
                //TruckLoad_No

                if (resultquery.Count == 0)
                {
                            
                }
                else
                {
                    int num = 0;
                    foreach (var item in query)
                    {
                        var resultItem = new ReportRecallOutboundExModel();

                        resultItem.rowNo = num + 1;
                        resultItem.Date_now_form = DateTime.Now.ToString("dd/MM/yyyy  hh:mm:ss ") + " น.";
                        resultItem.Tag_No = item.Tag_No;
                        resultItem.TruckLoad_Date = item.truckLoad_Date != null ? item.truckLoad_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.MFG_Date = item.MFG_Date != null ? item.MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.EXP_Date = item.EXP_Date != null ? item.EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.GoodsIssue_No = item.GoodsIssue_No;
                        resultItem.GoodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.PlanGoodsIssue_No = item.PlanGoodsIssue_No;
                        resultItem.Branch_code = item.Branch_Code;
                        resultItem.ProvincE_Id = item.Province_Id;
                        resultItem.ProvincE = item.Province_Name;
                        resultItem.Match_name = item.Match_Name;
                        resultItem.ShipTo_Id = item.ShipTo_Id;
                        resultItem.ShipTo_Name = item.ShipTo_Name;
                        resultItem.Tag_Pick = item.Tag_Pick;
                        resultItem.Product_Id = item.Product_Id;
                        resultItem.Product_Name = item.Product_Name;
                        resultItem.Product_Lot_GI = item.Product_Lot_GI;
                        resultItem.Order_BUQty = item.Order_BUQty;
                        resultItem.Order_BUConversion = item.Order_BUConversion;
                        resultItem.Order_SUQty = item.Order_SUQty;
                        resultItem.Order_SUConversion = item.Order_SUConversion;
                        resultItem.Sale_BUQty = item.Sale_BUQty;
                        resultItem.Sale_BUConversion = item.Sale_BUConversion;
                        resultItem.Sale_SUQty = item.Sale_SUQty;
                        resultItem.Sale_SUConversion = item.Sale_SUConversion;
                        resultItem.ERP_Location = item.ERP_Location;
                        resultItem.Billing_Date = item.Billing_Date != null ? item.Billing_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.Billing_Matdoc = item.Billing_Matdoc;
                        resultItem.TruckLoad_No = item.TruckLoad_No;
                        resultItem.Appointment_Id = item.Appointment_Id;
                        resultItem.Appointment_Date = item.Appointment_Date != null ? item.Appointment_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.Appointment_Time = item.Appointment_Time;
                        resultItem.Dock_Name = item.Dock_Name;
                        resultItem.Vehicle_registration = item.Vehicle_Registration;
                        resultItem.ambientRoom = room_Name;
                        result.Add(resultItem);
                        num++;

                    }
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportRecallOutbound_Export");
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
                //olog.logging("Report", ex.Message);
                throw ex;
            }
        }
        #endregion


        #region ExportExcel
        public string ExportExcel(ReportRecallOutboundRequestModel data, string rootPath = "")
        {
            //var DBContext = new PlanGRDbContext();
            //var GR_DBContext = new GRDbContext();
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportRecallOutboundExModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);

                var convert_Type_query = new List<MasterDataDataAccess.Models.View_ReportRecall_Outbound_Excel>();
                var query = convert_Type_query.AsQueryable();
                var room_Name = "";
                if (data.ambientRoom != "02")
                {
                    query = Master_DBContext.View_ReportRecall_Outbound_Excel.AsQueryable();
                    room_Name = "Ambient";
                }
                else
                {
                    query = temp_Master_DBContext.View_ReportRecall_Outbound_Excel.AsQueryable();
                    room_Name = "Freeze";
                }
                //var query = Master_DBContext.View_ReportRecall_Outbound_Excel.AsQueryable();
                if (data.advanceSearch == true)
                {
                    //เช็ตแถวแรก
                    if (!string.IsNullOrEmpty(data.materialNo))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] materNo_list = data.materialNo.Split(spearator, count, StringSplitOptions.None);
                        //var multi_mater = string.arr_material.Where(c => arr_material.Contains(c.Product_Id));

                        query = query.Where(c => materNo_list.Contains(c.Product_Id));
                        //query = query.Where(c => c.Product_Id.Contains(data.materialNo));
                    }
                    if (!string.IsNullOrEmpty(data.goodsIssue_date) && !string.IsNullOrEmpty(data.goodsIssue_date_to))
                    {
                        var dateStart = data.goodsIssue_date.toBetweenDate();
                        var dateEnd = data.goodsIssue_date_to.toBetweenDate();
                        query = query.Where(c => c.GoodsIssue_Date >= dateStart.start && c.GoodsIssue_Date <= dateEnd.end);

                    }
                    if (!string.IsNullOrEmpty(data.date_load) && !string.IsNullOrEmpty(data.date_load_to))
                    {
                        var dateStart = data.date_load.toBetweenDate();
                        var dateEnd = data.date_load_to.toBetweenDate();
                        query = query.Where(c => c.truckLoad_Date >= dateStart.start && c.truckLoad_Date <= dateEnd.end);
                    }
                    if (!string.IsNullOrEmpty(data.shipTo_ID))
                    {
                        query = query.Where(c => c.ShipTo_Id.Contains(data.shipTo_ID)
                                            || c.ShipTo_Name.Contains(data.shipTo_ID));
                    }

                    //เช็ตแถวสอง
                    if (!string.IsNullOrEmpty(data.Branch))
                    {
                        query = query.Where(c => c.Branch_Code.Contains(data.Branch));
                    }
                    if (!string.IsNullOrEmpty(data.Province))
                    {
                        query = query.Where(c => c.Province_Name.Contains(data.Province)
                                            || c.Province_Id.Contains(data.Province));
                    }
                    if (!string.IsNullOrEmpty(data.planGoodsIssue_No))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] doNo_list = data.planGoodsIssue_No.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => doNo_list.Contains(c.PlanGoodsIssue_No));
                        //query = query.Where(c => c.PlanGoodsIssue_No.Contains(data.planGoodsIssue_No));
                    }
                    if (!string.IsNullOrEmpty(data.billing_macdoc))
                    {
                        query = query.Where(c => c.Billing_Matdoc.Contains(data.billing_macdoc));
                    }

                    //เช็ตแถวสาม
                    if (!string.IsNullOrEmpty(data.NoTag))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] tag_list = data.NoTag.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => tag_list.Contains(c.Tag_No));
                        //query = query.Where(c => c.Tag_No.Contains(data.NoTag));
                    }
                    if (!string.IsNullOrEmpty(data.date_mfg) && !string.IsNullOrEmpty(data.date_mfg_to))
                    {
                        var dateStart = data.date_mfg.toBetweenDate();
                        var dateEnd = data.date_mfg_to.toBetweenDate();
                        query = query.Where(c => c.MFG_Date >= dateStart.start && c.MFG_Date <= dateEnd.end);
                    }
                    if (!string.IsNullOrEmpty(data.date_exp) && !string.IsNullOrEmpty(data.date_exp_to))
                    {
                        var dateStart = data.date_exp.toBetweenDate();
                        var dateEnd = data.date_exp_to.toBetweenDate();
                        query = query.Where(c => c.EXP_Date >= dateStart.start && c.EXP_Date <= dateEnd.end);
                    }
                    if (!string.IsNullOrEmpty(data.batch_lot))
                    {
                        query = query.Where(c => c.Product_Lot_GI.Contains(data.batch_lot) || c.Product_Lot_GI.Contains(data.batch_lot));
                    }

                    //เช็ตแถวสี่

                    if (!string.IsNullOrEmpty(data.sloc))
                    {
                        query = query.Where(c => c.ERP_Location.Contains(data.sloc));
                    }
                    if (!string.IsNullOrEmpty(data.Match))
                    {
                        query = query.Where(c => c.Match_Name.Contains(data.Match));
                    }
                }
                else
                {
                    //เช็ตแถวแรก
                    if (!string.IsNullOrEmpty(data.materialNo))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] materNo_list = data.materialNo.Split(spearator, count, StringSplitOptions.None);
                        //var multi_mater = string.arr_material.Where(c => arr_material.Contains(c.Product_Id));

                        query = query.Where(c => materNo_list.Contains(c.Product_Id));
                        //query = query.Where(c => c.Product_Id.Contains(data.materialNo));
                    }
                    if (!string.IsNullOrEmpty(data.goodsIssue_date) && !string.IsNullOrEmpty(data.goodsIssue_date_to))
                    {
                        var dateStart = data.goodsIssue_date.toBetweenDate();
                        var dateEnd = data.goodsIssue_date_to.toBetweenDate();
                        query = query.Where(c => c.GoodsIssue_Date >= dateStart.start && c.GoodsIssue_Date <= dateEnd.end);

                    }
                    if (!string.IsNullOrEmpty(data.date_load) && !string.IsNullOrEmpty(data.date_load_to))
                    {
                        var dateStart = data.date_load.toBetweenDate();
                        var dateEnd = data.date_load_to.toBetweenDate();
                        query = query.Where(c => c.truckLoad_Date >= dateStart.start && c.truckLoad_Date <= dateEnd.end);
                    }
                    if (!string.IsNullOrEmpty(data.shipTo_ID))
                    {
                        query = query.Where(c => c.ShipTo_Id.Contains(data.shipTo_ID)
                                            || c.ShipTo_Name.Contains(data.shipTo_ID));
                    }

                    //เช็ตแถวสอง
                    if (!string.IsNullOrEmpty(data.Branch))
                    {
                        query = query.Where(c => c.Branch_Code.Contains(data.Branch));
                    }
                    if (!string.IsNullOrEmpty(data.Province))
                    {
                        query = query.Where(c => c.Province_Name.Contains(data.Province)
                                            || c.Province_Id.Contains(data.Province));
                    }
                    if (!string.IsNullOrEmpty(data.planGoodsIssue_No))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] doNo_list = data.planGoodsIssue_No.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => doNo_list.Contains(c.PlanGoodsIssue_No));
                        //query = query.Where(c => c.PlanGoodsIssue_No.Contains(data.planGoodsIssue_No));
                    }
                    if (!string.IsNullOrEmpty(data.billing_macdoc))
                    {
                        query = query.Where(c => c.Billing_Matdoc.Contains(data.billing_macdoc));
                    }
                }

                var resultquery = query.OrderBy(o => o.Tag_No).ToList();
                //Orderby 
                //Tag_No
                //GoodsIssue_No
                //TruckLoad_No

                if (resultquery.Count == 0)
                {

                }
                else
                {
                    int num = 0;
                    foreach (var item in query)
                    {
                        var resultItem = new ReportRecallOutboundExModel();

                        resultItem.rowNo = num + 1;
                        resultItem.Date_now_form = DateTime.Now.ToString("dd/MM/yyyy  hh:mm:ss ") + " น.";
                        resultItem.Tag_No = item.Tag_No;
                        resultItem.TruckLoad_Date = item.truckLoad_Date != null ? item.truckLoad_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.MFG_Date = item.MFG_Date != null ? item.MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.EXP_Date = item.EXP_Date != null ? item.EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.GoodsIssue_No = item.GoodsIssue_No;
                        resultItem.GoodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.PlanGoodsIssue_No = item.PlanGoodsIssue_No;
                        resultItem.Branch_code = item.Branch_Code;
                        resultItem.ProvincE_Id = item.Province_Id;
                        resultItem.ProvincE = item.Province_Name;
                        resultItem.Match_name = item.Match_Name;
                        resultItem.ShipTo_Id = item.ShipTo_Id;
                        resultItem.ShipTo_Name = item.ShipTo_Name;
                        resultItem.Tag_Pick = item.Tag_Pick;
                        resultItem.Product_Id = item.Product_Id;
                        resultItem.Product_Name = item.Product_Name;
                        resultItem.Product_Lot_GI = item.Product_Lot_GI;
                        resultItem.Order_BUQty = item.Order_BUQty;
                        resultItem.Order_BUConversion = item.Order_BUConversion;
                        resultItem.Order_SUQty = item.Order_SUQty;
                        resultItem.Order_SUConversion = item.Order_SUConversion;
                        resultItem.Sale_BUQty = item.Sale_BUQty;
                        resultItem.Sale_BUConversion = item.Sale_BUConversion;
                        resultItem.Sale_SUQty = item.Sale_SUQty;
                        resultItem.Sale_SUConversion = item.Sale_SUConversion;
                        resultItem.ERP_Location = item.ERP_Location;
                        resultItem.Billing_Date = item.Billing_Date != null ? item.Billing_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.Billing_Matdoc = item.Billing_Matdoc;
                        resultItem.TruckLoad_No = item.TruckLoad_No;
                        resultItem.Appointment_Id = item.Appointment_Id;
                        resultItem.Appointment_Date = item.Appointment_Date != null ? item.Appointment_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.Appointment_Time = item.Appointment_Time;
                        resultItem.Dock_Name = item.Dock_Name;
                        resultItem.Vehicle_registration = item.Vehicle_Registration;
                        resultItem.ambientRoom = room_Name;
                        result.Add(resultItem);
                        num++;

                    }
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportRecallOutbound_Export");

                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "tmpReport";

                var renderedBytes = report.Execute(RenderType.Excel);
                fullPath = saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);


                var saveLocation = rootPath + fullPath;
                //File.Delete(saveLocation);
                //ExcelRefresh(reportPath);
                return saveLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string saveReport(byte[] file, string name, string rootPath)
        {
            var saveLocation = PhysicalPath(name, rootPath);
            FileStream fs = new FileStream(saveLocation, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            try
            {
                try
                {
                    bw.Write(file);
                }
                finally
                {
                    fs.Close();
                    bw.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return VirtualPath(name);
        }
        public string PhysicalPath(string name, string rootPath)
        {
            var filename = name;
            var vPath = ReportPath;
            var path = rootPath + vPath;
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            var saveLocation = System.IO.Path.Combine(path, filename);
            return saveLocation;
        }
        public string VirtualPath(string name)
        {
            var filename = name;
            var vPath = ReportPath;
            vPath = vPath.Replace("~", "");
            return vPath + filename;
        }
        private string ReportPath
        {
            get
            {
                var url = "\\ReportGenerator\\";
                return url;
            }
        }
        #endregion
    }
}
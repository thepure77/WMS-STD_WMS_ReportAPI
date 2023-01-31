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

namespace ReportBusiness.ReportRecall
{
    public class ReportRecallService
    {

        #region printReport
        public dynamic printReportRecall(ReportRecallRequestModel data, string rootPath = "" )
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportRecallExModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
               
                var convert_Type_query = new List<MasterDataDataAccess.Models.View_ReportRecall_Excel>();
                var query = convert_Type_query.AsQueryable();
                var room_Name = "";
                //var query = new List<MasterDataDataAccess.Models.View_ReportRecall_Excel>();
                if (data.ambientRoom != "02")
                {
                    query = Master_DBContext.View_ReportRecall_Excel.AsQueryable();
                    room_Name = "Ambient";
                }
                else
                {
                    query = temp_Master_DBContext.View_ReportRecall_Excel.AsQueryable();
                    room_Name = "Freeze";
                }
                //var query = Master_DBContext.View_ReportRecall_Excel.AsQueryable();
                if (data.advanceSearch == true)
                {
                    //เช็ตแถวแรก
                    if (!string.IsNullOrEmpty(data.materialNo))
                    {

                        char[] spearator = { ',' , ' '};
                        Int32 count = 1000;
                        String[] materNo_list = data.materialNo.Split(spearator , count , StringSplitOptions.None);
                        //var multi_mater = string.arr_material.Where(c => arr_material.Contains(c.Product_Id));
                   
                        query = query.Where(c => materNo_list.Contains(c.Product_Id));
                    }

                    if (!string.IsNullOrEmpty(data.vendorId))   //
                    {
                        //query = query.Where(c => c.Vendor_Id.Contains(data.vendorId));
                        query = query.Where(c => c.Vendor_Id.Contains(data.vendorId)
                                                 || c.Vendor_Name.Contains(data.vendorId));
                    }

                    if (!string.IsNullOrEmpty(data.po_No))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] poNo_list = data.po_No.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => poNo_list.Contains(c.PO_No));
                        //query = query.Where(c => c.PO_No.Contains(data.po_No));
                    }

                    if (!string.IsNullOrEmpty(data.NoTag))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] tag_list = data.NoTag.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => tag_list.Contains(c.Tag_No));
                        //query = query.Where(c => c.Tag_No.Contains(data.NoTag));
                    }


                    //เช็ตแถวสอง
                    if (!string.IsNullOrEmpty(data.goodsReceive_No))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] grNo_list = data.goodsReceive_No.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => grNo_list.Contains(c.GoodsReceive_No));
                        //query = query.Where(c => c.GoodsReceive_No.Contains(data.goodsReceive_No));
                    }
                    if (!string.IsNullOrEmpty(data.date_GR) && !string.IsNullOrEmpty(data.date_GR_to))
                    {
                        var dateStart = data.date_GR.toBetweenDate();
                        var dateEnd = data.date_GR_to.toBetweenDate();
                        query = query.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end); //
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

                    //เช็ตแถวสาม
                    if (!string.IsNullOrEmpty(data.batch_lot))
                    {
                        query = query.Where(c => c.Product_Lot_GI.Contains(data.batch_lot) || c.Product_Lot_GI.Contains(data.batch_lot));
                    }
                    if (!string.IsNullOrEmpty(data.sloc))
                    {
                        query = query.Where(c => c.ERP_Location.Contains(data.sloc));
                    }
                    if (!string.IsNullOrEmpty(data.billing_macdoc))
                    {
                        query = query.Where(c => c.Billing_Matdoc.Contains(data.billing_macdoc));
                    }
                    if (!string.IsNullOrEmpty(data.shipTo_ID))
                    {
                        query = query.Where(c => c.ShipTo_Id.Contains(data.shipTo_ID));
                    }

                    //เช็ตแถวสี่
                    if (!string.IsNullOrEmpty(data.planGoodsIssue_No))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] doNo_list = data.planGoodsIssue_No.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => doNo_list.Contains(c.PlanGoodsIssue_No));
                        //query = query.Where(c => c.PlanGoodsIssue_No.Contains(data.planGoodsIssue_No));
                    }
                    if (!string.IsNullOrEmpty(data.date_do) && !string.IsNullOrEmpty(data.date_do_to)) //DO date
                    {
                        var dateStart = data.date_do.toBetweenDate();
                        var dateEnd = data.date_do_to.toBetweenDate();
                        query = query.Where(c => c.Billing_Date >= dateStart.start && c.Billing_Date <= dateEnd.end);
                    }
                    if (!string.IsNullOrEmpty(data.GoodsIssue_No))
                    {
                        query = query.Where(c => c.GoodsIssue_No.Contains(data.GoodsIssue_No));
                    }
                    if (!string.IsNullOrEmpty(data.goodsIssue_date) && !string.IsNullOrEmpty(data.goodsIssue_date_to))
                    {
                        var dateStart = data.goodsIssue_date.toBetweenDate();
                        var dateEnd = data.goodsIssue_date_to.toBetweenDate();
                        query = query.Where(c => c.GoodsIssue_Date >= dateStart.start && c.GoodsIssue_Date <= dateEnd.end);

                    }
                    
                    //เช็ตแถวห้า
                    if (!string.IsNullOrEmpty(data.truckLoad_No)) //shipment
                    {
                        query = query.Where(c => c.TruckLoad_No.Contains(data.truckLoad_No));
                    }
                    if (!string.IsNullOrEmpty(data.date_load) && !string.IsNullOrEmpty(data.date_load_to))
                    {
                        var dateStart = data.date_load.toBetweenDate();
                        var dateEnd = data.date_load_to.toBetweenDate();
                        query = query.Where(c => c.truckLoad_Date >= dateStart.start && c.truckLoad_Date <= dateEnd.end); //
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
                    if (!string.IsNullOrEmpty(data.vendorId))   //
                    {
                        query = query.Where(c => c.Vendor_Id.Contains(data.vendorId)
                                                 || c.Vendor_Name.Contains(data.vendorId));
                        //query = query.Where(c => c.Vendor_Id.Contains(data.vendorId));
                    }
                    if (!string.IsNullOrEmpty(data.po_No))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] poNo_list = data.po_No.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => poNo_list.Contains(c.PO_No));
                        //query = query.Where(c => c.PO_No.Contains(data.po_No));
                    }
                    if (!string.IsNullOrEmpty(data.NoTag))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] tag_list = data.NoTag.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => tag_list.Contains(c.Tag_No));
                        //query = query.Where(c => c.Tag_No.Contains(data.NoTag));
                    }

                    //เช็ตแถวสอง
                    if (!string.IsNullOrEmpty(data.goodsReceive_No))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] grNo_list = data.goodsReceive_No.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => grNo_list.Contains(c.GoodsReceive_No));
                        //query = query.Where(c => c.GoodsReceive_No.Contains(data.goodsReceive_No));
                    }
                    if (!string.IsNullOrEmpty(data.date_GR) && !string.IsNullOrEmpty(data.date_GR_to))
                    {
                        var dateStart = data.date_GR.toBetweenDate();
                        var dateEnd = data.date_GR_to.toBetweenDate();
                        query = query.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end); //
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

                    //เช็ตแถวสาม
                    if (!string.IsNullOrEmpty(data.batch_lot))
                    {
                        query = query.Where(c => c.Product_Lot_GI.Contains(data.batch_lot) || c.Product_Lot_GI.Contains(data.batch_lot));
                    }
                    if (!string.IsNullOrEmpty(data.sloc))
                    {
                        query = query.Where(c => c.ERP_Location.Contains(data.sloc));
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
                        var resultItem = new ReportRecallExModel();

                        resultItem.rowNo = num + 1;
                        resultItem.Date_now_form = DateTime.Now.ToString("dd/MM/yyyy  hh:mm:ss ") + " น.";
                        resultItem.Tag_No = item.Tag_No;
                        resultItem.GoodsReceive_No = item.GoodsReceive_No;
                        resultItem.PO_No = item.PO_No;
                        resultItem.Vendor_Id = item.Vendor_Id;
                        resultItem.Vendor_Name = item.Vendor_Name;
                        resultItem.Product_Lot_GR = item.Product_Lot_GR;
                        resultItem.MFG_Date = item.MFG_Date != null ? item.MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.EXP_Date = item.EXP_Date != null ? item.EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.GoodsIssue_No = item.GoodsIssue_No;
                        resultItem.GoodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.PlanGoodsIssue_No = item.PlanGoodsIssue_No;
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
                        resultItem.Billing_Date_GR = item.Billing_Matdoc_GR;
                        resultItem.Billing_Matdoc = item.Billing_Matdoc;
                        resultItem.TruckLoad_No = item.TruckLoad_No;
                        resultItem.Appointment_Id = item.Appointment_Id;
                        resultItem.Appointment_Date = item.Appointment_Date != null ? item.Appointment_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.Appointment_Time = item.Appointment_Time;
                        resultItem.Dock_Name = item.Dock_Name;
                        resultItem.ambientRoom = room_Name;
                        result.Add(resultItem);
                        num++;

                    }
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportRecall_Export");
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
        public string ExportExcel(ReportRecallRequestModel data, string rootPath = "")
        {
            //var DBContext = new PlanGRDbContext();
            //var GR_DBContext = new GRDbContext();
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportRecallExModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);

                var convert_Type_query = new List<MasterDataDataAccess.Models.View_ReportRecall_Excel>();
                var query = convert_Type_query.AsQueryable();
                var room_Name = "";
                //var query = new List<MasterDataDataAccess.Models.View_ReportRecall_Excel>();
                if (data.ambientRoom != "02")
                {
                    query = Master_DBContext.View_ReportRecall_Excel.AsQueryable();
                    room_Name = "Ambient";
                }
                else
                {
                    query = temp_Master_DBContext.View_ReportRecall_Excel.AsQueryable();
                    room_Name = "Freeze";
                }
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
                    }

                    if (!string.IsNullOrEmpty(data.vendorId))   //
                    {
                        query = query.Where(c => c.Vendor_Id.Contains(data.vendorId)
                                                 || c.Vendor_Name.Contains(data.vendorId));
                        //query = query.Where(c => c.Vendor_Id.Contains(data.vendorId));
                    }

                    if (!string.IsNullOrEmpty(data.po_No))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] poNo_list = data.po_No.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => poNo_list.Contains(c.PO_No));
                        //query = query.Where(c => c.PO_No.Contains(data.po_No));
                    }

                    if (!string.IsNullOrEmpty(data.NoTag))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] tag_list = data.NoTag.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => tag_list.Contains(c.Tag_No));
                        //query = query.Where(c => c.Tag_No.Contains(data.NoTag));
                    }


                    //เช็ตแถวสอง
                    if (!string.IsNullOrEmpty(data.goodsReceive_No))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] grNo_list = data.goodsReceive_No.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => grNo_list.Contains(c.GoodsReceive_No));
                        //query = query.Where(c => c.GoodsReceive_No.Contains(data.goodsReceive_No));
                    }
                    if (!string.IsNullOrEmpty(data.date_GR) && !string.IsNullOrEmpty(data.date_GR_to))
                    {
                        var dateStart = data.date_GR.toBetweenDate();
                        var dateEnd = data.date_GR_to.toBetweenDate();
                        query = query.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end); //
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

                    //เช็ตแถวสาม
                    if (!string.IsNullOrEmpty(data.batch_lot))
                    {
                        query = query.Where(c => c.Product_Lot_GI.Contains(data.batch_lot) || c.Product_Lot_GI.Contains(data.batch_lot));
                    }
                    if (!string.IsNullOrEmpty(data.sloc))
                    {
                        query = query.Where(c => c.ERP_Location.Contains(data.sloc));
                    }
                    if (!string.IsNullOrEmpty(data.billing_macdoc))
                    {
                        query = query.Where(c => c.Billing_Matdoc.Contains(data.billing_macdoc));
                    }
                    if (!string.IsNullOrEmpty(data.shipTo_ID))
                    {
                        query = query.Where(c => c.ShipTo_Id.Contains(data.shipTo_ID));
                    }

                    //เช็ตแถวสี่
                    if (!string.IsNullOrEmpty(data.planGoodsIssue_No))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] doNo_list = data.planGoodsIssue_No.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => doNo_list.Contains(c.PlanGoodsIssue_No));
                        //query = query.Where(c => c.PlanGoodsIssue_No.Contains(data.planGoodsIssue_No));
                    }
                    if (!string.IsNullOrEmpty(data.date_do) && !string.IsNullOrEmpty(data.date_do_to)) //DO date
                    {
                        var dateStart = data.date_do.toBetweenDate();
                        var dateEnd = data.date_do_to.toBetweenDate();
                        query = query.Where(c => c.Billing_Date >= dateStart.start && c.Billing_Date <= dateEnd.end);
                    }
                    if (!string.IsNullOrEmpty(data.GoodsIssue_No))
                    {
                        query = query.Where(c => c.GoodsIssue_No.Contains(data.GoodsIssue_No));
                    }
                    if (!string.IsNullOrEmpty(data.goodsIssue_date) && !string.IsNullOrEmpty(data.goodsIssue_date_to))
                    {
                        var dateStart = data.goodsIssue_date.toBetweenDate();
                        var dateEnd = data.goodsIssue_date_to.toBetweenDate();
                        query = query.Where(c => c.GoodsIssue_Date >= dateStart.start && c.GoodsIssue_Date <= dateEnd.end);

                    }

                    //เช็ตแถวห้า
                    if (!string.IsNullOrEmpty(data.truckLoad_No)) //shipment
                    {
                        query = query.Where(c => c.TruckLoad_No.Contains(data.truckLoad_No));
                    }
                    if (!string.IsNullOrEmpty(data.date_load) && !string.IsNullOrEmpty(data.date_load_to))
                    {
                        var dateStart = data.date_load.toBetweenDate();
                        var dateEnd = data.date_load_to.toBetweenDate();
                        query = query.Where(c => c.truckLoad_Date >= dateStart.start && c.truckLoad_Date <= dateEnd.end); //
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
                    if (!string.IsNullOrEmpty(data.vendorId))   //
                    {
                        query = query.Where(c => c.Vendor_Id.Contains(data.vendorId)
                                                 || c.Vendor_Name.Contains(data.vendorId));
                        //query = query.Where(c => c.Vendor_Id.Contains(data.vendorId));
                    }
                    if (!string.IsNullOrEmpty(data.po_No))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] poNo_list = data.po_No.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => poNo_list.Contains(c.PO_No));
                        //query = query.Where(c => c.PO_No.Contains(data.po_No));
                    }
                    if (!string.IsNullOrEmpty(data.NoTag))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] tag_list = data.NoTag.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => tag_list.Contains(c.Tag_No));
                        //query = query.Where(c => c.Tag_No.Contains(data.NoTag));
                    }

                    //เช็ตแถวสอง
                    if (!string.IsNullOrEmpty(data.goodsReceive_No))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] grNo_list = data.goodsReceive_No.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => grNo_list.Contains(c.GoodsReceive_No));
                        //query = query.Where(c => c.GoodsReceive_No.Contains(data.goodsReceive_No));
                    }
                    if (!string.IsNullOrEmpty(data.date_GR) && !string.IsNullOrEmpty(data.date_GR_to))
                    {
                        var dateStart = data.date_GR.toBetweenDate();
                        var dateEnd = data.date_GR_to.toBetweenDate();
                        query = query.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end); //
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

                    //เช็ตแถวสาม
                    if (!string.IsNullOrEmpty(data.batch_lot))
                    {
                        query = query.Where(c => c.Product_Lot_GI.Contains(data.batch_lot) || c.Product_Lot_GI.Contains(data.batch_lot));
                    }
                    if (!string.IsNullOrEmpty(data.sloc))
                    {
                        query = query.Where(c => c.ERP_Location.Contains(data.sloc));
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
                        var resultItem = new ReportRecallExModel();

                        resultItem.rowNo = num + 1;
                        resultItem.Date_now_form = DateTime.Now.ToString("dd/MM/yyyy  hh:mm:ss ") + " น.";
                        resultItem.Tag_No = item.Tag_No;
                        resultItem.GoodsReceive_No = item.GoodsReceive_No;
                        resultItem.PO_No = item.PO_No;
                        resultItem.Vendor_Id = item.Vendor_Id;
                        resultItem.Vendor_Name = item.Vendor_Name;
                        resultItem.Product_Lot_GR = item.Product_Lot_GR;
                        resultItem.MFG_Date = item.MFG_Date != null ? item.MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.EXP_Date = item.EXP_Date != null ? item.EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.GoodsIssue_No = item.GoodsIssue_No;
                        resultItem.GoodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.PlanGoodsIssue_No = item.PlanGoodsIssue_No;
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
                        resultItem.Billing_Date_GR = item.Billing_Matdoc_GR;
                        resultItem.Billing_Matdoc = item.Billing_Matdoc;
                        resultItem.TruckLoad_No = item.TruckLoad_No;
                        resultItem.Appointment_Id = item.Appointment_Id;
                        resultItem.Appointment_Date = item.Appointment_Date != null ? item.Appointment_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.Appointment_Time = item.Appointment_Time;
                        resultItem.Dock_Name = item.Dock_Name;
                        resultItem.ambientRoom = room_Name;
                        result.Add(resultItem);
                        num++;

                    }
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportRecall_Export");

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
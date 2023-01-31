using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using PlanGRBusiness.Libs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportBusiness.ReportReceiving
{
    public class ReportReceivingService
    {
        #region printReportReceiving
        public dynamic printReportReceiving(ReportReceivingViewModel data, string rootPath = "")
        {

            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var culture = new System.Globalization.CultureInfo("en-US");
            var culture_TH = new System.Globalization.CultureInfo("th-TH");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportReceivingPrintViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);

                
                var goodsReceive_No = "";
                var businessunit_index = "";
                var purchaseOrder_No = "";
                var documentType_Index = "";
                var vendor_Index = "";
                var planGoodsReceive_No = "";
                var matdoc = "";
                var dateStart_null = "";
                var dateEnd_null = "";
                //DateTime gR_Date_From = DateTime.Now.toString().toBetweenDate().start;
                //DateTime gR_Date_To = DateTime.Now.toString().toBetweenDate().end;
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var statusModels = new List<int?>();
                

                if (data.businessUnitList != null)
                {
                    businessunit_index = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.goodsReceive_No))
                {
                    goodsReceive_No = data.goodsReceive_No;
                }
                if (!string.IsNullOrEmpty(data.purchaseOrder_No))
                {
                    purchaseOrder_No = data.purchaseOrder_No;
                }
                if (!string.IsNullOrEmpty(data.documentType_Index))
                {
                    documentType_Index = data.documentType_Index;
                }
                if (!string.IsNullOrEmpty(data.vendor_Index))
                {
                    vendor_Index = data.vendor_Index;
                }
                if (!string.IsNullOrEmpty(data.planGoodsReceive_No))
                {
                    planGoodsReceive_No = data.planGoodsReceive_No;
                }
                if (!string.IsNullOrEmpty(data.matdoc))
                {
                    matdoc = data.matdoc;
                }
                //if (!string.IsNullOrEmpty(data.gR_Date_From) && !string.IsNullOrEmpty(data.gR_Date_To))
                //{
                //    dateStart = data.gR_Date_From.toBetweenDate().start;
                //    dateEnd = data.gR_Date_To.toBetweenDate().end;
                //    //dateStart = data.gR_Date_From.toBetweenDate().start;
                //    //dateEnd = data.gR_Date_To.toBetweenDate().end;
                //}

                var date = new SqlParameter();
                var date_to = new SqlParameter();
                if (!string.IsNullOrEmpty(data.gR_Date_From) && !string.IsNullOrEmpty(data.gR_Date_To))
                {
                    dateStart = data.gR_Date_From.toBetweenDate().start;
                    dateEnd = data.gR_Date_To.toBetweenDate().end;
                    date = new SqlParameter("@GR_Date_From", dateStart);
                    date_to = new SqlParameter("@GR_Date_To", dateEnd);
                }   
                else
                {
                    date = new SqlParameter("@GR_Date_From", dateStart_null);
                    date_to = new SqlParameter("@GR_Date_To", dateEnd_null);
                }
                



                var PO_In = new SqlParameter("@PurchaseOrder_No", purchaseOrder_No);
                var Bu_Index = new SqlParameter("@BusinessUnit_Index", businessunit_index);
                var GR_In = new SqlParameter("@GoodsReceive_No", goodsReceive_No);
                var DocType_In = new SqlParameter("@DocumentType_Index", documentType_Index);
                var Ven_In = new SqlParameter("@Vendor_Index", vendor_Index);
                var PlanGR_In = new SqlParameter("@PlanGoodsReceive_No", planGoodsReceive_No);
                var Mat = new SqlParameter("@Matdoc", matdoc);
                
                //var Date_From = new SqlParameter("@GR_Date_From", gR_Date_From);
                //var Date_To = new SqlParameter("@GR_Date_To", gR_Date_To);

                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_10_Receiving>();

                //resultquery = Master_DBContext.sp_rpt_10_Receiving.FromSql("sp_rpt_10_Receiving @BusinessUnit_Index , @GoodsReceive_No , @GR_Date_From , @GR_Date_To , @PurchaseOrder_No , @DocumentType_Index , @Vendor_Index , @PlanGoodsReceive_No , @Matdoc ", Bu_Index, GR_In, date, date_to, PO_In, DocType_In, Ven_In, PlanGR_In, Mat).ToList();


                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_rpt_10_Receiving.FromSql("sp_rpt_10_Receiving @BusinessUnit_Index , @GoodsReceive_No , @GR_Date_From , @GR_Date_To , @PurchaseOrder_No , @DocumentType_Index , @Vendor_Index , @PlanGoodsReceive_No , @Matdoc ", Bu_Index, GR_In, date, date_to, PO_In, DocType_In, Ven_In, PlanGR_In, Mat).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_10_Receiving.FromSql("sp_rpt_10_Receiving @BusinessUnit_Index , @GoodsReceive_No , @GR_Date_From , @GR_Date_To , @PurchaseOrder_No , @DocumentType_Index , @Vendor_Index , @PlanGoodsReceive_No , @Matdoc ", Bu_Index, GR_In, date, date_to, PO_In, DocType_In, Ven_In, PlanGR_In, Mat).ToList();

                }


                //if (resultquery.Count == 0)
                //{
                //    var resultItem = new ReportReceivingPrintViewModel();
                //    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                //    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                //    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //    resultItem.report_date = startDate;
                //    resultItem.report_date_to = endDate;
                //    result.Add(resultItem);
                //}
                /////////////////////////////////////////////////////////////////////////    07-11-2022 aut//////////////////////////////////////////////////////////
                //var queryGroup = (from res in resultquery
                //                      //join loc in dbMaster.View_Location.ToList() on res.Location_Index equals loc.Location_Index
                //                  select new
                //                  {
                //                      GoodsReceive_No = res.GoodsReceive_No,
                //                      ASN_SumQty = res.ASN_SumQty

                //                  }).GroupBy(c => new
                //                  {
                //                      c.GoodsReceive_No,
                //                      c.ASN_SumQty
                //                  })
                //               .Select(c => new
                //               {
                //                   c.Key.GoodsReceive_No,
                //                   c.Key.ASN_SumQty,


                //                   //SumQtyBal = c.Sum(s => s.BinBalance_QtyBal),
                //                   //SumQtyRe = c.Sum(s => s.BinBalance_QtyReserve),
                //                   //SumWeight = c.Sum(s => s.BinBalance_WeightBal)
                //               });
                //var aa = queryGroup.ToList();

                ////////////////////////////////////////////////////////////////////////

                //else
                //{
                //int num = 0;
                /////////////////////////////////////////////////////////////////////////    07-11-2022 aut//////////////////////////////////////////////////////////
                decimal num = 0;
                int row = 0;
                foreach (var item in resultquery)
                {
                    if (item.Row_No == 1)
                    {
                        num = item.ASN_SumQty;
                    }
                    else
                    {

                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    //var startDate = DateTime.ParseExact(data.gR_Date_From.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    //var endDate = DateTime.ParseExact(data.gR_Date_To.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var resultItem = new ReportReceivingPrintViewModel();
                    resultItem.row_No = row + 1; ;
                    //var resultItem = new ReportReceivingPrintViewModel();
                    //    resultItem.row_No = num + 1; ;
                    //var resultItem = new ReportReceivingPrintViewModel();
                    //    resultItem.row_No = row + 1; 
                    resultItem.ambientRoom = item.Warehouse_Type;
                        resultItem.businessUnit_Name = item.BusinessUnit_Name;
                        resultItem.vendor_Name = item.Vendor_Name;
                        resultItem.purchaseOrder_Due_Date = (item.PurchaseOrder_Due_Date != null) ? item.PurchaseOrder_Due_Date.Value.ToString("dd MMM yy", culture_TH) : "";
                        //resultItem.purchaseOrder_Due_Date = item.PurchaseOrder_Due_Date != null ? item.PurchaseOrder_Due_Date.Value.ToString("dd/MM/yyyy") :  "";
                        resultItem.purchaseOrder_Due_Time = item.PurchaseOrder_Due_Date != null ? item.PurchaseOrder_Due_Date.Value.ToString("HH:mm:ss") : "";
                        resultItem.goodsReceive_No = item.GoodsReceive_No;
                        resultItem.goodsReceive_Date = (item.GoodsReceive_Date != null) ? item.GoodsReceive_Date.Value.ToString("dd MMM yy", culture_TH) : "";
                        //resultItem.goodsReceive_Date = item.GoodsReceive_Date != null ? item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.purchaseOrder_No = item.PurchaseOrder_No;
                        //resultItem.purchaseOrder_Date = item.PurchaseOrder_Date.ToString();
                        resultItem.purchaseOrder_Date = item.PurchaseOrder_Date != null ? item.PurchaseOrder_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.planGoodsReceive_No = item.PlanGoodsReceive_No;
                        resultItem.ref_Document_No = item.Ref_Document_No;
                        resultItem.planGoodsReceive_Date = (item.PlanGoodsReceive_Date != null) ? item.PlanGoodsReceive_Date.Value.ToString("dd MMM yy", culture_TH) : "";
                        //resultItem.planGoodsReceive_Date = item.PlanGoodsReceive_Date != null ? item.PlanGoodsReceive_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.putAway_SumQty = item.PutAway_SumQty.ToString();
                        resultItem.putAway_SU = item.PutAway_SU;
                        resultItem.pO_SumQty = item.PO_SumQty.ToString();
                        resultItem.pO_SALE_SU = item.PO_SALE_SU;
                        resultItem.aSN_SumQty = item.ASN_SumQty.ToString();
                        //resultItem.aSN_SumQty = num.ToString();
                        resultItem.aSN_SALE_SU = item.ASN_SALE_SU;
                        resultItem.remaining_ReceiveQty = item.Remaining_ReceiveQty.ToString();
                        //resultItem.remaining_ReceiveQty = (num - item.PutAway_SumQty).ToString() ;
                        resultItem.remaining_Receive_SALE_SU = item.Remaining_Receive_SALE_SU;
                        resultItem.itemStatus_Name = item.ItemStatus_Name;
                        resultItem.mFG_Date = (item.MFG_Date != null) ? item.MFG_Date.Value.ToString("dd MMM yy", culture_TH) : "";
                        //resultItem.mFG_Date = item.MFG_Date != null ? item.MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.eXP_Date = (item.EXP_Date != null) ? item.EXP_Date.Value.ToString("dd MMM yy", culture_TH) : "";
                        //resultItem.eXP_Date = item.EXP_Date != null ? item.EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.sAP_Sloc = item.SAP_Sloc;
                        resultItem.wMS_Sloc = item.WMS_Sloc;
                        resultItem.plant = item.Plant;
                        resultItem.putaway_Date = (item.Putaway_Date != null) ? item.Putaway_Date.Value.ToString("dd MMM yy", culture_TH) : "";
                        resultItem.putaway_Time = item.Putaway_Date != null ? item.Putaway_Date.Value.ToString("HH:mm:ss") : "";
                        resultItem.documentType_Name = item.DocumentType_Name;
                        resultItem.matdoc = item.Matdoc;
                        resultItem.matdoc_Date = (item.Matdoc_Date != null) ? item.Matdoc_Date.Value.ToString("dd MMM yy", culture_TH) : "";
                        resultItem.matdoc_Time = item.Matdoc_Date != null ? item.Matdoc_Date.Value.ToString("HH:mm:ss") : "";
                        resultItem.documentRef_No2 = item.DocumentRef_No2;
                        resultItem.processStatus_Name = item.ProcessStatus_Name;
                        resultItem.document_Remark = item.Document_Remark;
                    
                        result.Add(resultItem);
                        row++;
                        //num = num - item.PutAway_SumQty ;
                    }
                //}
                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportReceiving");
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


        #region printReport
        //public dynamic printReportReceiving(ReportReceivingViewModel data, string rootPath = "")
        //{
        //    var MasterDataDbContext = new MasterDataDbContext();
        //    var temp_MasterDataDbContext = new temp_MasterDataDbContext();
        //    var culture = new System.Globalization.CultureInfo("en-US");
        //    String State = "Start";
        //    String msglog = "";
        //    var olog = new logtxt();
        //    var result = new List<ReportReceivingViewModel>();

        //    try
        //    {
        //        MasterDataDbContext.Database.SetCommandTimeout(360);
        //        temp_MasterDataDbContext.Database.SetCommandTimeout(360);
        //        var tag_No = "";
        //        var owner_Name = "";
        //        var product_Id = "";
        //        var itemStatus_Name = "";
        //        var CycleCount_No = "";
        //        var Status_Diff_Count = "";
        //        var business_Unit = "";
        //        var location_Type = "";
        //        DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
        //        DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
        //        var statusModels = new List<int?>();
        //        //if (!string.IsNullOrEmpty(data.tag_No))
        //        //{
        //        //    tag_No = data.tag_No;
        //        //}
        //        //if (!string.IsNullOrEmpty(data.product_Id))
        //        //{
        //        //    product_Id = data.product_Id;
        //        //}
        //        //if (!string.IsNullOrEmpty(data.owner_Name))
        //        //{
        //        //    owner_Name = data.owner_Name;
        //        //}
        //        //if (!string.IsNullOrEmpty(data.itemStatus_Name))
        //        //{
        //        //    itemStatus_Name = data.itemStatus_Name;
        //        //}
        //        //if (!string.IsNullOrEmpty(data.cycleCount_No))
        //        //{
        //        //    CycleCount_No = data.cycleCount_No;
        //        //}
        //        //if (!string.IsNullOrEmpty(data.status_Diff_Count))
        //        //{
        //        //    Status_Diff_Count = data.status_Diff_Count;
        //        //}
        //        //if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
        //        //{
        //        //    dateStart = data.report_date.toBetweenDate().start;
        //        //    dateEnd = data.report_date_to.toBetweenDate().end;
        //        //}
        //        //if (!string.IsNullOrEmpty(data.businessUnit_Name))
        //        //{
        //        //    business_Unit = data.businessUnit_Name;
        //        //}
        //        //if (!string.IsNullOrEmpty(data.locationType_Name))
        //        //{
        //        //    location_Type = data.locationType_Name;
        //        //}

        //        var tag_no = new SqlParameter("@Tag_No", tag_No);
        //        var pro_id = new SqlParameter("@Product_Id", product_Id);
        //        var owner = new SqlParameter("@Owner_Name", owner_Name);
        //        var itemstatus = new SqlParameter("@ItemStatus_Name", itemStatus_Name);
        //        var cycle_no = new SqlParameter("@CycleCount_No", CycleCount_No);
        //        var status_diff = new SqlParameter("@Status_Diff_Count", Status_Diff_Count);
        //        var c_date = new SqlParameter("@CycleCount_Date", dateStart);
        //        var c_date_to = new SqlParameter("@CycleCount_Date_To", dateEnd);
        //        var business = new SqlParameter("@BusinessUnit_Name", business_Unit);
        //        var loc_Type = new SqlParameter("@LocationType_Name", location_Type);
        //        var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_10_Receiving>();

        //        //if (data.ambientRoom != "02")
        //        //{
        //        //    resultquery = MasterDataDbContext.sp_rpt_10_Receiving.FromSql("sp_rpt_10_Receiving @Tag_No,@Product_Id,@Owner_Name,@ItemStatus_Name,@CycleCount_No,@Status_Diff_Count,@CycleCount_Date,@CycleCount_Date_To,@BusinessUnit_Name,@LocationType_Name", tag_no, pro_id, owner, itemstatus, cycle_no, status_diff, c_date, c_date_to, business, loc_Type).ToList();
        //        //}
        //        //else
        //        //{
        //        //    resultquery = temp_MasterDataDbContext.sp_rpt_10_Receiving.FromSql("sp_rpt_10_Receiving @Tag_No,@Product_Id,@Owner_Name,@ItemStatus_Name,@CycleCount_No,@Status_Diff_Count,@CycleCount_Date,@CycleCount_Date_To,@BusinessUnit_Name,@LocationType_Name", tag_no, pro_id, owner, itemstatus, cycle_no, status_diff, c_date, c_date_to, business, loc_Type).ToList();
        //        //}

        //        if (resultquery.Count == 0)
        //        {
        //            var resultItem = new ReportReceivingViewModel();
        //            var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //            var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //            resultItem.report_date = startDate;
        //            resultItem.report_date_to = endDate;
        //            result.Add(resultItem);
        //        }
        //        else
        //        {
        //            int num = 0;
        //            foreach (var item in resultquery)
        //            {
        //                var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                var resultItem = new ReportReceivingViewModel();
        //                resultItem.rowNum = num + 1;
        //                resultItem.cycleCount_No = item.CycleCount_No;
        //                resultItem.locationType_Name = item.LocationType_Name;
        //                resultItem.location_Name = item.Location_Name;
        //                resultItem.tag_No = item.Tag_No;
        //                resultItem.product_Id = item.Product_Id;
        //                resultItem.product_Name = item.Product_Name;
        //                resultItem.owner_Name = item.Owner_Name;
        //                resultItem.eRP_Location = item.ERP_Location;
        //                resultItem.location_Name = item.Location_Name;
        //                resultItem.itemStatus_Id = item.ItemStatus_Id;
        //                resultItem.sALE_ProductConversion_Name = item.SALE_ProductConversion_Name;
        //                resultItem.sale_Unit = item.Sale_Unit;
        //                resultItem.eRP_Location = item.ERP_Location;
        //                resultItem.sALE_ProductConversion_Ratio = item.SALE_ProductConversion_Ratio;
        //                resultItem.qty_Diff = item.Qty_Diff;
        //                resultItem.qty_Count = item.Qty_Count;
        //                resultItem.status_Diff_Count = item.Status_Diff_Count;
        //                resultItem.count_Date = item.Count_Date;
        //                resultItem.count_by = item.Count_by;
        //                resultItem.product_Lot = item.Product_Lot;
        //                resultItem.goodsReceive_MFG_Date = item.GoodsReceive_MFG_Date;
        //                resultItem.goodsReceive_EXP_Date = item.GoodsReceive_EXP_Date;
        //                resultItem.goodsReceive_Date = item.GoodsReceive_Date;
        //                resultItem.goodsReceive_ProductConversion_Id = item.GoodsReceive_ProductConversion_Id;
        //                resultItem.businessUnit_Name = item.BusinessUnit_Name;



        //                if (data.ambientRoom != "02")
        //                {
        //                    resultItem.ambientRoom = "Ambient";
        //                }
        //                else
        //                {
        //                    resultItem.ambientRoom = "Freeze";
        //                }
        //                result.Add(resultItem);
        //                num++;
        //            }
        //        }
        //        rootPath = rootPath.Replace("\\ReportAPI", "");
        //        //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
        //        var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportReceiving");
        //        //var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSpaceUtilization");
        //        LocalReport report = new LocalReport(reportPath);
        //        report.AddDataSource("DataSet1", result);

        //        System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        //        string fileName = "";
        //        string fullPath = "";
        //        fileName = "tmpReport" + DateTime.Now.ToString("yyyyMMddHHmmss");

        //        var renderedBytes = report.Execute(RenderType.Pdf);

        //        Utils objReport = new Utils();
        //        fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".pdf", rootPath);
        //        var saveLocation = objReport.PhysicalPath(fileName + ".pdf", rootPath);
        //        return saveLocation;


        //    }
        //    catch (Exception ex)
        //    {
        //        //olog.logging("ReportKPI", ex.Message);
        //        throw ex;
        //    }
        //}
        #endregion

        public string ExportExcel(ReportReceivingViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var culture = new System.Globalization.CultureInfo("en-US");
            var culture_TH = new System.Globalization.CultureInfo("th-TH");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportReceivingPrintViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);


                var goodsReceive_No = "";
                var businessunit_index = "";
                var purchaseOrder_No = "";
                var documentType_Index = "";
                var vendor_Index = "";
                var planGoodsReceive_No = "";
                var matdoc = "";
                var dateStart_null = "";
                var dateEnd_null = "";
                //DateTime gR_Date_From = DateTime.Now.toString().toBetweenDate().start;
                //DateTime gR_Date_To = DateTime.Now.toString().toBetweenDate().end;
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var statusModels = new List<int?>();


                if (data.businessUnitList != null)
                {
                    businessunit_index = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.goodsReceive_No))
                {
                    goodsReceive_No = data.goodsReceive_No;
                }
                if (!string.IsNullOrEmpty(data.purchaseOrder_No))
                {
                    purchaseOrder_No = data.purchaseOrder_No;
                }
                if (!string.IsNullOrEmpty(data.documentType_Index))
                {
                    documentType_Index = data.documentType_Index;
                }
                if (!string.IsNullOrEmpty(data.vendor_Index))
                {
                    vendor_Index = data.vendor_Index;
                }
                if (!string.IsNullOrEmpty(data.planGoodsReceive_No))
                {
                    planGoodsReceive_No = data.planGoodsReceive_No;
                }
                if (!string.IsNullOrEmpty(data.matdoc))
                {
                    matdoc = data.matdoc;
                }
                //if (!string.IsNullOrEmpty(data.gR_Date_From) && !string.IsNullOrEmpty(data.gR_Date_To))
                //{
                //    dateStart = data.gR_Date_From.toBetweenDate().start;
                //    dateEnd = data.gR_Date_To.toBetweenDate().end;
                //    //dateStart = data.gR_Date_From.toBetweenDate().start;
                //    //dateEnd = data.gR_Date_To.toBetweenDate().end;
                //}

                var date = new SqlParameter();
                var date_to = new SqlParameter();
                if (!string.IsNullOrEmpty(data.gR_Date_From) && !string.IsNullOrEmpty(data.gR_Date_To))
                {
                    dateStart = data.gR_Date_From.toBetweenDate().start;
                    dateEnd = data.gR_Date_To.toBetweenDate().end;
                    date = new SqlParameter("@GR_Date_From", dateStart);
                    date_to = new SqlParameter("@GR_Date_To", dateEnd);
                }
                else
                {
                    date = new SqlParameter("@GR_Date_From", dateStart_null);
                    date_to = new SqlParameter("@GR_Date_To", dateEnd_null);
                }




                var PO_In = new SqlParameter("@PurchaseOrder_No", purchaseOrder_No);
                var Bu_Index = new SqlParameter("@BusinessUnit_Index", businessunit_index);
                var GR_In = new SqlParameter("@GoodsReceive_No", goodsReceive_No);
                var DocType_In = new SqlParameter("@DocumentType_Index", documentType_Index);
                var Ven_In = new SqlParameter("@Vendor_Index", vendor_Index);
                var PlanGR_In = new SqlParameter("@PlanGoodsReceive_No", planGoodsReceive_No);
                var Mat = new SqlParameter("@Matdoc", matdoc);

                //var Date_From = new SqlParameter("@GR_Date_From", gR_Date_From);
                //var Date_To = new SqlParameter("@GR_Date_To", gR_Date_To);

                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_10_Receiving>();

                //resultquery = Master_DBContext.sp_rpt_10_Receiving.FromSql("sp_rpt_10_Receiving @BusinessUnit_Index , @GoodsReceive_No , @GR_Date_From , @GR_Date_To , @PurchaseOrder_No , @DocumentType_Index , @Vendor_Index , @PlanGoodsReceive_No , @Matdoc ", Bu_Index, GR_In, date, date_to, PO_In, DocType_In, Ven_In, PlanGR_In, Mat).ToList();


                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_rpt_10_Receiving.FromSql("sp_rpt_10_Receiving @BusinessUnit_Index , @GoodsReceive_No , @GR_Date_From , @GR_Date_To , @PurchaseOrder_No , @DocumentType_Index , @Vendor_Index , @PlanGoodsReceive_No , @Matdoc ", Bu_Index, GR_In, date, date_to, PO_In, DocType_In, Ven_In, PlanGR_In, Mat).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_10_Receiving.FromSql("sp_rpt_10_Receiving @BusinessUnit_Index , @GoodsReceive_No , @GR_Date_From , @GR_Date_To , @PurchaseOrder_No , @DocumentType_Index , @Vendor_Index , @PlanGoodsReceive_No , @Matdoc ", Bu_Index, GR_In, date, date_to, PO_In, DocType_In, Ven_In, PlanGR_In, Mat).ToList();

                }


                //if (resultquery.Count == 0)
                //{
                //    var resultItem = new ReportReceivingPrintViewModel();
                //    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                //    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                //    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //    resultItem.report_date = startDate;
                //    resultItem.report_date_to = endDate;
                //    result.Add(resultItem);
                //}
                /////////////////////////////////////////////////////////////////////////    07-11-2022 aut//////////////////////////////////////////////////////////
                //var queryGroup = (from res in resultquery
                //                      //join loc in dbMaster.View_Location.ToList() on res.Location_Index equals loc.Location_Index
                //                  select new
                //                  {
                //                      GoodsReceive_No = res.GoodsReceive_No,
                //                      ASN_SumQty = res.ASN_SumQty

                //                  }).GroupBy(c => new
                //                  {
                //                      c.GoodsReceive_No,
                //                      c.ASN_SumQty
                //                  })
                //               .Select(c => new
                //               {
                //                   c.Key.GoodsReceive_No,
                //                   c.Key.ASN_SumQty,


                //                   //SumQtyBal = c.Sum(s => s.BinBalance_QtyBal),
                //                   //SumQtyRe = c.Sum(s => s.BinBalance_QtyReserve),
                //                   //SumWeight = c.Sum(s => s.BinBalance_WeightBal)
                //               });
                //var aa = queryGroup.ToList();

                ////////////////////////////////////////////////////////////////////////

                //else
                //{
                //int num = 0;
                /////////////////////////////////////////////////////////////////////////    07-11-2022 aut//////////////////////////////////////////////////////////
                decimal num = 0;
                int row = 0;
                foreach (var item in resultquery)
                {
                    if (item.Row_No == 1)
                    {
                        num = item.ASN_SumQty;
                    }
                    else
                    {

                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    //var startDate = DateTime.ParseExact(data.gR_Date_From.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    //var endDate = DateTime.ParseExact(data.gR_Date_To.Substring(0, 8), "yyyyMMdd",
                    //System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var resultItem = new ReportReceivingPrintViewModel();
                    resultItem.row_No = row + 1; ;
                    //var resultItem = new ReportReceivingPrintViewModel();
                    //    resultItem.row_No = num + 1; ;
                    //var resultItem = new ReportReceivingPrintViewModel();
                    //    resultItem.row_No = row + 1; 
                    resultItem.ambientRoom = item.Warehouse_Type;
                    resultItem.businessUnit_Name = item.BusinessUnit_Name;
                    resultItem.vendor_Name = item.Vendor_Name;
                    resultItem.purchaseOrder_Due_Date = (item.PurchaseOrder_Due_Date != null) ? item.PurchaseOrder_Due_Date.Value.ToString("dd MMM yy", culture_TH) : "";
                    //resultItem.purchaseOrder_Due_Date = item.PurchaseOrder_Due_Date != null ? item.PurchaseOrder_Due_Date.Value.ToString("dd/MM/yyyy") :  "";
                    resultItem.purchaseOrder_Due_Time = item.PurchaseOrder_Due_Date != null ? item.PurchaseOrder_Due_Date.Value.ToString("HH:mm:ss") : "";
                    resultItem.goodsReceive_No = item.GoodsReceive_No;
                    resultItem.goodsReceive_Date = (item.GoodsReceive_Date != null) ? item.GoodsReceive_Date.Value.ToString("dd MMM yy", culture_TH) : "";
                    //resultItem.goodsReceive_Date = item.GoodsReceive_Date != null ? item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.purchaseOrder_No = item.PurchaseOrder_No;
                    //resultItem.purchaseOrder_Date = item.PurchaseOrder_Date.ToString();
                    resultItem.purchaseOrder_Date = item.PurchaseOrder_Date != null ? item.PurchaseOrder_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.planGoodsReceive_No = item.PlanGoodsReceive_No;
                    resultItem.ref_Document_No = item.Ref_Document_No;
                    resultItem.planGoodsReceive_Date = (item.PlanGoodsReceive_Date != null) ? item.PlanGoodsReceive_Date.Value.ToString("dd MMM yy", culture_TH) : "";
                    //resultItem.planGoodsReceive_Date = item.PlanGoodsReceive_Date != null ? item.PlanGoodsReceive_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.product_Lot = item.Product_Lot;
                    resultItem.putAway_SumQty = item.PutAway_SumQty.ToString();
                    resultItem.putAway_SU = item.PutAway_SU;
                    resultItem.pO_SumQty = item.PO_SumQty.ToString();
                    resultItem.pO_SALE_SU = item.PO_SALE_SU;
                    resultItem.aSN_SumQty = item.ASN_SumQty.ToString();
                    //resultItem.aSN_SumQty = num.ToString();
                    resultItem.aSN_SALE_SU = item.ASN_SALE_SU;
                    resultItem.remaining_ReceiveQty = item.Remaining_ReceiveQty.ToString();
                    //resultItem.remaining_ReceiveQty = (num - item.PutAway_SumQty).ToString() ;
                    resultItem.remaining_Receive_SALE_SU = item.Remaining_Receive_SALE_SU;
                    resultItem.itemStatus_Name = item.ItemStatus_Name;
                    resultItem.mFG_Date = (item.MFG_Date != null) ? item.MFG_Date.Value.ToString("dd MMM yy", culture_TH) : "";
                    //resultItem.mFG_Date = item.MFG_Date != null ? item.MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.eXP_Date = (item.EXP_Date != null) ? item.EXP_Date.Value.ToString("dd MMM yy", culture_TH) : "";
                    //resultItem.eXP_Date = item.EXP_Date != null ? item.EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.sAP_Sloc = item.SAP_Sloc;
                    resultItem.wMS_Sloc = item.WMS_Sloc;
                    resultItem.plant = item.Plant;
                    resultItem.putaway_Date = (item.Putaway_Date != null) ? item.Putaway_Date.Value.ToString("dd MMM yy", culture_TH) : "";
                    resultItem.putaway_Time = item.Putaway_Date != null ? item.Putaway_Date.Value.ToString("HH:mm:ss") : "";
                    resultItem.documentType_Name = item.DocumentType_Name;
                    resultItem.matdoc = item.Matdoc;
                    resultItem.matdoc_Date = (item.Matdoc_Date != null) ? item.Matdoc_Date.Value.ToString("dd MMM yy", culture_TH) : "";
                    resultItem.matdoc_Time = item.Matdoc_Date != null ? item.Matdoc_Date.Value.ToString("HH:mm:ss") : "";
                    resultItem.documentRef_No2 = item.DocumentRef_No2;
                    resultItem.processStatus_Name = item.ProcessStatus_Name;
                    resultItem.document_Remark = item.Document_Remark;

                    result.Add(resultItem);
                    row++;
                    //num = num - item.PutAway_SumQty ;
                }
                //}

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportReceiving");

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
        public string ConvertToThai(string inAsciiString)
        {
            //Encoding inAsciiEncoding = Encoding.ASCII;

            //Encoding outUTF8Encoding = Encoding.UTF8;
            //byte[] inAsciiBytes = inAsciiEncoding.GetBytes(inAsciiString);
            //byte[] outUTF8Bytes = Encoding.Convert(inAsciiEncoding, outUTF8Encoding, inAsciiBytes);
            //char[] inUTF8Chars = new char[outUTF8Encoding.GetCharCount(outUTF8Bytes, 0, outUTF8Bytes.Length)];
            //outUTF8Encoding.GetChars(outUTF8Bytes, 0, outUTF8Bytes.Length, inUTF8Chars, 0);
            //string outUTF8String = new string(inUTF8Chars);
            //return outUTF8String;
            var text = "";
            var ascii = Encoding.UTF8.GetBytes(inAsciiString);
            text = Encoding.UTF8.GetString(ascii);
            return text;
        }


    }

}

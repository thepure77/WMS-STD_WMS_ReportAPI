using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using PlanGRBusiness.Libs;
using ReportBusiness.Report5;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportBusiness.ReportSummaryInventoryPayment
{
    public class ReportSummaryInventoryPaymentService
    {

        #region printReportSummaryInventoryPayment
        //public dynamic printReportSummaryInventoryPayment(ReportSummaryInventoryPaymentViewModel data, string rootPath = "")
        //{
        //    var GI_DBContext = new GIDbContext();
        //    var PlanGI_DBContext = new PlanGIDbContext();
        //    var BB_DBContext = new BinbalanceDbContext();

        //    var culture = new System.Globalization.CultureInfo("en-US");
        //    String State = "Start";
        //    String msglog = "";
        //    var olog = new logtxt();

        //    var result = new List<ReportSummaryInventoryPaymentViewModel>();

        //    try
        //    {
        //        var queryGI = GI_DBContext.View_RPT_GI_SummaryInventoryPayment.AsQueryable();
        //        var queryPlanGI = PlanGI_DBContext.View_RPT_PlanGI_SummaryInventoryPayment.AsQueryable();

        //        if (!string.IsNullOrEmpty(data.product_Id))
        //        {
        //            queryGI = queryGI.Where(c => c.Product_Id == data.product_Id);
        //        }

        //        if (!string.IsNullOrEmpty(data.owner_Id))
        //        {
        //            queryGI = queryGI.Where(c => c.Owner_Id == data.owner_Id);
        //        }

        //        if (!string.IsNullOrEmpty(data.goodsIssue_date) && !string.IsNullOrEmpty(data.goodsIssue_date_To))
        //        {
        //            var dateStart = data.goodsIssue_date.toBetweenDate();
        //            var dateEnd = data.goodsIssue_date_To.toBetweenDate();
        //            queryGI = queryGI.Where(c => c.GoodsIssue_Date >= dateStart.start && c.GoodsIssue_Date <= dateEnd.end);
        //        }
        //        else if (!string.IsNullOrEmpty(data.goodsIssue_date))
        //        {
        //            var goodsIssue_date_From = data.goodsIssue_date.toBetweenDate();
        //            queryGI = queryGI.Where(c => c.GoodsIssue_Date >= goodsIssue_date_From.start);
        //        }
        //        else if (!string.IsNullOrEmpty(data.goodsIssue_date_To))
        //        {
        //            var goodsIssue_date_To = data.goodsIssue_date_To.toBetweenDate();
        //            queryGI = queryGI.Where(c => c.GoodsIssue_Date <= goodsIssue_date_To.start);
        //        }

        //        var queryRPT_GI = queryGI.ToList();
        //        var queryRPT_PlanGI = queryPlanGI.ToList();

        //        var query_TaskIten = GI_DBContext.IM_TaskItem.Where(c => queryRPT_PlanGI.Select(s => s.PlanGoodsIssue_Index).Contains(c.PlanGoodsIssueItem_Index)).ToList();

        //        var sumTask = query_TaskIten.GroupBy(c => new
        //        {
        //            c.PlanGoodsIssue_Index,
        //            c.PlanGoodsIssue_No,
        //            c.Product_Id,
        //            c.Location_Index
        //        })
        //       .Select(c => new
        //       {
        //           c.Key.PlanGoodsIssue_Index,
        //           c.Key.PlanGoodsIssue_No,
        //           c.Key.Product_Id,
        //           c.Key.Location_Index,
        //           SumQty = c.Sum(s => s.TotalQty),
        //       }).ToList();


        //        var query = (from GI in queryRPT_GI
        //                     join PlanGI in queryRPT_PlanGI on GI.Ref_Document_Index equals PlanGI.PlanGoodsIssue_Index into ps
        //                     from r in ps
        //                     select new
        //                     {
        //                         gi = GI,
        //                         plangi = r
        //                     }).ToList();



        //        if (query.Count == 0)
        //        {
        //            var resultItem = new ReportSummaryInventoryPaymentViewModel();

        //            var startDate = DateTime.ParseExact(data.goodsIssue_date.Substring(0, 8), "yyyyMMdd",
        //            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //            var endDate = DateTime.ParseExact(data.goodsIssue_date_To.Substring(0, 8), "yyyyMMdd",
        //            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //            resultItem.checkQuery = true;
        //            resultItem.product_Id = data.product_Id;
        //            resultItem.product_Name = data.product_Name;
        //            resultItem.qty = 0;
        //            resultItem.totalQty = 0;
        //            resultItem.goodsIssue_date = startDate;
        //            resultItem.goodsIssue_date_To = endDate;
        //            result.Add(resultItem);
        //        }
        //        else
        //        {
        //            foreach (var item in query.OrderByDescending(o => o.plangi.PlanGoodsIssue_No))
        //            {

        //                string date = item.gi.GoodsIssue_Date.toString();
        //                string GIDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                string PickDate = "";
        //                if (item.gi.Picking_Date != null)
        //                {
        //                    string datePick = item.gi.Picking_Date.toString();
        //                    PickDate = DateTime.ParseExact(datePick.Substring(0, 8), "yyyyMMdd",
        //                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);
        //                }

        //                var resultItem = new ReportSummaryInventoryPaymentViewModel();

        //                resultItem.planGoodsIssue_No = item.plangi.PlanGoodsIssue_No;
        //                //resultItem.goodsIssue_Date = GIDate;
        //                resultItem.goodsIssue_Date = PickDate;
        //                resultItem.product_Id = item.gi.Product_Id;
        //                resultItem.product_Name = item.gi.Product_Name;
        //                resultItem.goodsIssue_Time = item.gi.GoodsIssue_Time;
        //                resultItem.planGoodsIssue_No = item.plangi.PlanGoodsIssue_No;
        //                resultItem.goodsIssue_No = item.gi.GoodsIssue_No;
        //                resultItem.qty = item.gi.totalQty;
        //                resultItem.productConversion_Name = item.plangi.ProductConversion_Name;
        //                resultItem.location_Name = item.gi.Location_Name;
        //                resultItem.shipTo_Address = item.plangi.SoldTo_Name + " / " + item.plangi.ShipTo_Name;  //item.plangi.ShipTo_Address;
        //                resultItem.documentItem_Remark = item.plangi.DocumentItem_Remark;
        //                resultItem.update_By = item.gi.Update_By;
        //                resultItem.owner_Name = item.gi.Owner_Name;
        //                //resultItem.totalQty = sumTask.Where(c => c.Product_Id == item.gi.Product_Id && c.PlanGoodsIssue_No == item.plangi.PlanGoodsIssue_No && c.Location_Index == item.gi.Location_Index).Select(s => s.SumQty).FirstOrDefault();
        //                resultItem.totalQty = item.plangi.Qty;
        //                //try { resultItem.totalQty = BB_DBContext.wm_BinBalance.Where(c => c.Product_Id == item.gi.Product_Id && c.Location_Index == item.gi.Location_Index && c.BinBalance_QtyBal > 0).Sum(s => s.BinBalance_QtyBal); } catch { };
        //                resultItem.picking_By = item.gi.Picking_By;
        //                resultItem.picking_Date = item.gi.Picking_Date == null ? "" :  item.gi.Picking_Date.Value.ToString("HH:mm");


        //                var startDate = DateTime.ParseExact(data.goodsIssue_date.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                var endDate = DateTime.ParseExact(data.goodsIssue_date_To.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                resultItem.goodsIssue_date = startDate;
        //                resultItem.goodsIssue_date_To = endDate;

        //                result.Add(resultItem);
        //            }
        //        }

        //        rootPath = rootPath.Replace("\\ReportAPI", "");
        //        //var reportPath = rootPath + "\\ReportBusiness\\ReportSummaryInventoryPayment\\ReportSummaryInventoryPayment.rdlc";
        //        var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSummaryInventoryPayment");
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

        //        throw ex;
        //    }
        //}
        #endregion

        #region printReportSummaryInventoryPayment
        public dynamic printReportSummaryInventoryPayment(ReportSummaryInventoryPaymentViewModel data, string rootPath = "")
        {
            var GI_DBContext = new GIDbContext();
            //var olog = new logtxt();
            //olog.logging("log_Report", "Start");
            var culture = new System.Globalization.CultureInfo("en-US");

            String State = "Start";
            String msglog = "";
            var result = new List<ReportSummaryInventoryPaymentViewModel>();

            try
            {
                //var queryGI = GI_DBContext.View_RPT_SummaryInventoryPayment.AsQueryable().ToList();

                //if (!string.IsNullOrEmpty(data.TruckLoad_No))
                //{
                //    queryGI = queryGI.Where(c => c.TruckLoad_Index == Guid.Parse(data.TruckLoad_No)).ToList();
                //}
                // if (!string.IsNullOrEmpty(data.PlanGoodsIssue_No))
                //{
                //    queryGI = queryGI.Where(c => c.PlanGoodsIssue_No == data.PlanGoodsIssue_No).ToList();
                //}
                // if (!string.IsNullOrEmpty(data.Billing_No))
                //{
                //    queryGI = queryGI.Where(c => c.Billing_No == data.Billing_No).ToList();
                //}
                //if (!string.IsNullOrEmpty(data.ShipTo_Id))
                //{
                //    queryGI = queryGI.Where(c => c.ShipTo_Id == data.ShipTo_Id).ToList();
                //}
                //if (!string.IsNullOrEmpty(data.PlanGoodsIssue_Date) &&  !string.IsNullOrEmpty(data.PlanGoodsIssue_Date_To))
                //{
                //    var dateStart = data.PlanGoodsIssue_Date.toBetweenDate();
                //    var dateEnd = data.PlanGoodsIssue_Date_To.toBetweenDate();
                //    queryGI = queryGI.Where(c => c.PlanGoodsIssue_Date >= dateStart.start && c.PlanGoodsIssue_Date <= dateEnd.end).ToList();
                //}
                GI_DBContext.Database.SetCommandTimeout(360);

                var tl_no ="";
                var plan_no = "";
                var bill_no = "";
                var shipto = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                if (!string.IsNullOrEmpty(data.TruckLoad_No))
                {
                    tl_no = data.TruckLoad_No;
                }
                if (!string.IsNullOrEmpty(data.PlanGoodsIssue_No))
                {
                    plan_no = data.PlanGoodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.Billing_No))
                {
                    bill_no = data.Billing_No;
                }
                if (!string.IsNullOrEmpty(data.ShipTo_Id))
                {
                    shipto = data.ShipTo_Id;
                }
                if (!string.IsNullOrEmpty(data.PlanGoodsIssue_Date) && !string.IsNullOrEmpty(data.PlanGoodsIssue_Date_To))
                {
                    dateStart = data.PlanGoodsIssue_Date.toBetweenDate().start;
                    dateEnd = data.PlanGoodsIssue_Date_To.toBetweenDate().end;
                }

                //@GI,@PLAN,@TL,@BIL,@MAT,@SHI,@GI_DATE,@GI_DATE_TO
                
                var TL = new SqlParameter("@TL", tl_no);
                var PLAN = new SqlParameter("@PLAN", plan_no);
                var BIL = new SqlParameter("@BIL", bill_no);
                var SHI = new SqlParameter("@SHI", shipto);
                var GI_DATE = new SqlParameter("@GI_DATE", dateStart);
                var GI_DATE_TO = new SqlParameter("@GI_DATE_TO", dateEnd);
                var GI = new SqlParameter("@GI", "");
                var MAT = new SqlParameter("@MAT", "");
                //olog.logging("log_Report", tl_no + " | " + plan_no + " | " + bill_no + " | " + shipto + " | " + dateStart.ToString() + " | " + dateEnd.ToString());
                var queryGI = GI_DBContext.sp_payment.FromSql("sp_payment  @GI,@PLAN,@TL,@BIL,@MAT,@SHI,@GI_DATE,@GI_DATE_TO", GI, PLAN, TL, BIL, MAT, SHI, GI_DATE, GI_DATE_TO).ToList();
                //olog.logging("log_Report", queryGI.Count.ToString());

                if (queryGI.Count == 0)
                {
                    var resultItem = new ReportSummaryInventoryPaymentViewModel();

                    var startDate = DateTime.ParseExact(data.PlanGoodsIssue_Date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.PlanGoodsIssue_Date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    //resultItem.checkQuery = true;
                    //resultItem.product_Id = data.product_Id;
                    //resultItem.product_Name = data.product_Name;
                    //resultItem.qty = 0;
                    //resultItem.totalQty = 0;
                    //resultItem.goodsIssue_date = startDate;
                    //resultItem.goodsIssue_date_To = endDate;
                    //result.Add(resultItem);
                }
                else
                {
                    foreach (var item in queryGI.OrderBy(c=> c.TruckLoad_No).ThenBy(c=> c.PlanGoodsIssue_No))
                    {

                        if (item.PlanGoodsIssue_Date != null)
                        {
                            string date = item.PlanGoodsIssue_Date.toString();
                            string GIDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);
                        }

                        var resultItem = new ReportSummaryInventoryPaymentViewModel();

                        //resultItem.Create_Date_OMS = item.Create_Date_OMS;
                        //resultItem.Create_Time_OMS = item.Create_Time_OMS != null ? (item.Create_Time_OMS.ToString().Split('.'))[0] : null ;
                        //resultItem.SO_No = item.SO_No;
                        //resultItem.PlanGoodsIssue_Date = item.PlanGoodsIssue_Date.Value.ToString("dd/MM/yyyy");
                        //resultItem.PlanGoodsIssue_No = item.PlanGoodsIssue_No;
                        //resultItem.TruckLoad_No = item.TruckLoad_No;
                        //resultItem.Load_Date = item.Load_Date;
                        //resultItem.Load_Time = item.Load_Time;
                        //resultItem.GoodsIssue_No = item.GoodsIssue_No;
                        //resultItem.Wave_Date = item.Wave_Date;
                        //resultItem.Wave_Time = item.Wave_Time != null ? (item.Wave_Time.ToString().Split('.'))[0] : null;
                        //resultItem.ItemStatus_Name = item.ItemStatus_Name;
                        //resultItem.Product_Id = item.Product_Id;
                        //resultItem.Product_Name = item.Product_Name;
                        //resultItem.Product_Lot = item.Product_Lot;
                        //resultItem.Order_Qty= item.Order_Qty;
                        //resultItem.Order_Unit= item.Order_Unit;
                        //resultItem.Pick_Qty= item.Pick_Qty;
                        //resultItem.Pick_Unit= item.Pick_Unit;
                        //resultItem.Short_ship= item.Short_ship;
                        //resultItem.Short_Unit= item.Short_Unit;
                        //resultItem.Tote= item.Tote;
                        //resultItem.Pick_location= item.Pick_location;
                        //resultItem.GI_Date= item.GI_Date;
                        //resultItem.GI_Time= item.GI_Time;
                        //resultItem.Dock= item.Dock;
                        //resultItem.Billing_No= item.Billing_No;
                        //resultItem.Matdoc= item.Matdoc;
                        //resultItem.Zone= item.Zone;
                        //resultItem.Sub_Zone= item.Sub_Zone;
                        //resultItem.Province= item.Province;
                        //resultItem.Branch= item.Branch;
                        //resultItem.ShipTo_Id= item.ShipTo_Id;
                        //resultItem.ShipTo_Address= item.ShipTo_Address;
                        //resultItem.Vehicle_Registration = item.Vehicle_Registration;

                        resultItem.TruckLoad_No = item.TruckLoad_No;
                        resultItem.Appointment_Id = item.Appointment_Id;
                        resultItem.Dock_Name = item.Dock_Name;
                        resultItem.Appointment_Date = item.Appointment_Date;
                        resultItem.Appointment_Time = item.Appointment_Time;
                        resultItem.PlanGoodsIssue_No = item.PlanGoodsIssue_No;
                        resultItem.ShipTo_Id = item.ShipTo_Id;
                        resultItem.ShipTo_Name = item.ShipTo_Name;
                        resultItem.BranchCode = item.BranchCode;
                        resultItem.Product_Id = item.Product_Id;
                        resultItem.Product_Name = item.Product_Name;
                        resultItem.Order_Qty = item.Order_Qty;
                        resultItem.WHGI_QTY = item.WHGI_QTY;
                        resultItem.TRGI_QTY = item.TRGI_QTY;
                        resultItem.Order_UNIT = item.Order_UNIT;
                        resultItem.GoodsIssue_No = item.GoodsIssue_No;
                        resultItem.GoodsIssue_Date = item.GoodsIssue_Date;
                        resultItem.Bill_No = item.Bill_No;
                        resultItem.Matdoc = item.Matdoc;
                        resultItem.BU_Order_Qty = item.BU_Order_Qty;
                        resultItem.BU_WHGIQty = item.BU_WHGIQty;
                        resultItem.BU_TRGI_QTY = item.BU_TRGI_QTY;
                        resultItem.BU_Unit = item.BU_Unit;
                        resultItem.SU_Order_QTY = item.SU_Order_QTY;
                        resultItem.SU_WHGIQty = item.SU_WHGIQty;
                        resultItem.SU_TRGI_QTY = item.SU_TRGI_QTY;
                        resultItem.SU_Unit = item.SU_Unit;
                        resultItem.MFG_date = item.MFG_date;
                        resultItem.EXP_date = item.EXP_date;
                        resultItem.Document_Remark = item.Document_Remark;
                        resultItem.DocumentRef_No3 = item.DocumentRef_No3;

                        var startDate = DateTime.ParseExact(data.PlanGoodsIssue_Date.Substring(0, 8), "yyyyMMdd",System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.PlanGoodsIssue_Date_To.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.start_date = startDate;
                        resultItem.end_date = endDate;

                        result.Add(resultItem);
                    }
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSummaryInventoryPayment");
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
                //olog.logging("log_Report", ex.Message);
                throw ex;
            }
        }
        #endregion

        #region autoSearchOwner
        public List<ItemListViewModel> autoSearchOwner(ItemListViewModel data)
        {
            try
            {

                using (var context = new GIDbContext())
                {


                    var query = context.View_RPT_GI_SummaryInventoryPayment.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key) && data.key != "-")
                    {
                        query = query.Where(c => c.Owner_Id.Contains(data.key)
                        || c.Owner_Name.Contains(data.key));

                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Owner_Index, c.Owner_Id, c.Owner_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Owner_Index,
                            id = item.Owner_Id,
                            name = item.Owner_Name,

                        };

                        items.Add(resultItem);
                    }

                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public string ExportExcel(ReportSummaryInventoryPaymentViewModel data, string rootPath = "")
        {
            var GI_DBContext = new GIDbContext();
            var PlanGI_DBContext = new PlanGIDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            //var olog = new logtxt();

            var result = new List<ReportSummaryInventoryPaymentViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSummaryInventoryPayment");

            try
            {
                //var queryGI = GI_DBContext.View_RPT_SummaryInventoryPayment.AsQueryable().ToList();

                //if (!string.IsNullOrEmpty(data.TruckLoad_No))
                //{
                //    queryGI = queryGI.Where(c => c.TruckLoad_Index == Guid.Parse(data.TruckLoad_No)).ToList();
                //}
                //if (!string.IsNullOrEmpty(data.PlanGoodsIssue_No))
                //{
                //    queryGI = queryGI.Where(c => c.PlanGoodsIssue_No == data.PlanGoodsIssue_No).ToList();
                //}
                //if (!string.IsNullOrEmpty(data.Billing_No))
                //{
                //    queryGI = queryGI.Where(c => c.Billing_No == data.Billing_No).ToList();
                //}
                //if (!string.IsNullOrEmpty(data.ShipTo_Id))
                //{
                //    queryGI = queryGI.Where(c => c.ShipTo_Id == data.ShipTo_Id).ToList();
                //}
                //if (!string.IsNullOrEmpty(data.PlanGoodsIssue_Date) && !string.IsNullOrEmpty(data.PlanGoodsIssue_Date_To))
                //{
                //    var dateStart = data.PlanGoodsIssue_Date.toBetweenDate();
                //    var dateEnd = data.PlanGoodsIssue_Date_To.toBetweenDate();
                //    queryGI = queryGI.Where(c => c.PlanGoodsIssue_Date >= dateStart.start && c.PlanGoodsIssue_Date <= dateEnd.end).ToList();
                //}
                GI_DBContext.Database.SetCommandTimeout(360);
                var tl_no = "";
                var plan_no = "";
                var bill_no = "";
                var shipto = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                if (!string.IsNullOrEmpty(data.TruckLoad_No))
                {
                    tl_no = data.TruckLoad_No;
                }
                if (!string.IsNullOrEmpty(data.PlanGoodsIssue_No))
                {
                    plan_no = data.PlanGoodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.Billing_No))
                {
                    bill_no = data.Billing_No;
                }
                if (!string.IsNullOrEmpty(data.ShipTo_Id))
                {
                    shipto = data.ShipTo_Id;
                }
                if (!string.IsNullOrEmpty(data.PlanGoodsIssue_Date) && !string.IsNullOrEmpty(data.PlanGoodsIssue_Date_To))
                {
                    dateStart = data.PlanGoodsIssue_Date.toBetweenDate().start;
                    dateEnd = data.PlanGoodsIssue_Date_To.toBetweenDate().end;
                }

                var TL = new SqlParameter("@TL", tl_no);
                var PLAN = new SqlParameter("@PLAN", plan_no);
                var BIL = new SqlParameter("@BIL", bill_no);
                var SHI = new SqlParameter("@SHI", shipto);
                var GI_DATE = new SqlParameter("@GI_DATE", dateStart);
                var GI_DATE_TO = new SqlParameter("@GI_DATE_TO", dateEnd);
                var GI = new SqlParameter("@GI", "");
                var MAT = new SqlParameter("@MAT", "");
                var queryGI = GI_DBContext.sp_payment.FromSql("sp_payment  @GI,@PLAN,@TL,@BIL,@MAT,@SHI,@GI_DATE,@GI_DATE_TO", GI, PLAN, TL, BIL, MAT, SHI, GI_DATE, GI_DATE_TO).ToList();

                if (queryGI.Count == 0)
                {
                    var resultItem = new ReportSummaryInventoryPaymentViewModel();

                    var startDate = DateTime.ParseExact(data.PlanGoodsIssue_Date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.PlanGoodsIssue_Date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    //resultItem.checkQuery = true;
                    //resultItem.product_Id = data.product_Id;
                    //resultItem.product_Name = data.product_Name;
                    //resultItem.qty = 0;
                    //resultItem.totalQty = 0;
                    //resultItem.goodsIssue_date = startDate;
                    //resultItem.goodsIssue_date_To = endDate;
                    //result.Add(resultItem);
                }
                else
                {
                    foreach (var item in queryGI.OrderBy(c => c.TruckLoad_No).ThenBy(c => c.PlanGoodsIssue_No))
                    {

                        if (item.PlanGoodsIssue_Date != null)
                        {
                            string date = item.PlanGoodsIssue_Date.toString();
                            string GIDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                            System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);
                        }

                        var resultItem = new ReportSummaryInventoryPaymentViewModel();

                        //resultItem.Create_Date_OMS = item.Create_Date_OMS;
                        //resultItem.Create_Time_OMS = item.Create_Time_OMS != null ? (item.Create_Time_OMS.ToString().Split('.'))[0] : null;
                        //resultItem.SO_No = item.SO_No;
                        //resultItem.PlanGoodsIssue_Date = item.PlanGoodsIssue_Date.ToString();
                        //resultItem.PlanGoodsIssue_No = item.PlanGoodsIssue_No;
                        //resultItem.TruckLoad_No = item.TruckLoad_No;
                        //resultItem.Load_Date = item.Load_Date;
                        //resultItem.Load_Time = item.Load_Time;
                        //resultItem.GoodsIssue_No = item.GoodsIssue_No;
                        //resultItem.Wave_Date = item.Wave_Date;
                        //resultItem.Wave_Time = item.Wave_Time != null ? (item.Wave_Time.ToString().Split('.'))[0] : null;
                        //resultItem.ItemStatus_Name = item.ItemStatus_Name;
                        //resultItem.Product_Id = item.Product_Id;
                        //resultItem.Product_Name = item.Product_Name;
                        //resultItem.Product_Lot = item.Product_Lot;
                        //resultItem.Order_Qty = item.Order_Qty;
                        //resultItem.Order_Unit = item.Order_Unit;
                        //resultItem.Pick_Qty = item.Pick_Qty;
                        //resultItem.Pick_Unit = item.Pick_Unit;
                        //resultItem.Short_ship = item.Short_ship;
                        //resultItem.Short_Unit = item.Short_Unit;
                        //resultItem.Tote = item.Tote;
                        //resultItem.Pick_location = item.Pick_location;
                        //resultItem.GI_Date = item.GI_Date;
                        //resultItem.GI_Time = item.GI_Time;
                        //resultItem.Dock = item.Dock;
                        //resultItem.Billing_No = item.Billing_No;
                        //resultItem.Matdoc = item.Matdoc;
                        //resultItem.Zone = item.Zone;
                        //resultItem.Sub_Zone = item.Sub_Zone;
                        //resultItem.Province = item.Province;
                        //resultItem.Branch = item.Branch;
                        //resultItem.ShipTo_Id = item.ShipTo_Id;
                        //resultItem.ShipTo_Address = item.ShipTo_Address;
                        //resultItem.Vehicle_Registration = item.Vehicle_Registration;

                        resultItem.TruckLoad_No = item.TruckLoad_No;
                        resultItem.Appointment_Id = item.Appointment_Id;
                        resultItem.Dock_Name = item.Dock_Name;
                        resultItem.Appointment_Date = item.Appointment_Date;
                        resultItem.Appointment_Time = item.Appointment_Time != null ? (item.Appointment_Time.ToString().Split('.'))[0] : null;
                        resultItem.PlanGoodsIssue_No = item.PlanGoodsIssue_No;
                        resultItem.ShipTo_Id = item.ShipTo_Id;
                        resultItem.ShipTo_Name = item.ShipTo_Name;
                        resultItem.BranchCode = item.BranchCode;
                        resultItem.Product_Id = item.Product_Id;
                        resultItem.Product_Name = item.Product_Name;
                        resultItem.Order_Qty = item.Order_Qty;
                        resultItem.WHGI_QTY = item.WHGI_QTY;
                        resultItem.TRGI_QTY = item.TRGI_QTY;
                        resultItem.Order_UNIT = item.Order_UNIT;
                        resultItem.GoodsIssue_No = item.GoodsIssue_No;
                        resultItem.GoodsIssue_Date = item.GoodsIssue_Date;
                        resultItem.Bill_No = item.Bill_No;
                        resultItem.Matdoc = item.Matdoc;
                        resultItem.BU_Order_Qty = item.BU_Order_Qty;
                        resultItem.BU_WHGIQty = item.BU_WHGIQty;
                        resultItem.BU_TRGI_QTY = item.BU_TRGI_QTY;
                        resultItem.BU_Unit = item.BU_Unit;
                        resultItem.SU_Order_QTY = item.SU_Order_QTY;
                        resultItem.SU_WHGIQty = item.SU_WHGIQty;
                        resultItem.SU_TRGI_QTY = item.SU_TRGI_QTY;
                        resultItem.SU_Unit = item.SU_Unit;
                        resultItem.MFG_date = item.MFG_date;
                        resultItem.EXP_date = item.EXP_date;
                        resultItem.Document_Remark = item.Document_Remark;
                        resultItem.DocumentRef_No3 = item.DocumentRef_No3;

                        var startDate = DateTime.ParseExact(data.PlanGoodsIssue_Date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.PlanGoodsIssue_Date_To.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.start_date = startDate;
                        resultItem.end_date = endDate;

                        result.Add(resultItem);
                    }
                }

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
                //olog.logging("Report", ex.Message);
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

    }
}

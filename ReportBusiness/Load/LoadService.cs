using AspNetCore.Reporting;
using BinbalanceBusiness;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using GIDataAccess.Models;

using Microsoft.EntityFrameworkCore;




using System;
using System.Collections.Generic;

using System.Data.SqlClient;

using System.Linq;



namespace ReportBusiness.Load
{
    public class LoadService
    {
       
        #region printOutTracePicking
        public actionResultTrace_pickingViewModel printOutTracePicking(Trace_picking data)
        {
            var Outbound_DBContext = new GIDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            var olog = new logtxt();
            try
            {
                Outbound_DBContext.Database.SetCommandTimeout(360);


                var GI = new SqlParameter("@GI", data.Goodsissue_Index == null ? "" : data.Goodsissue_Index);
                var TL = new SqlParameter("@TL", data.TruckLoad_Index == null ? "" : data.TruckLoad_Index);
                var PGL = new SqlParameter("@PGL", data.PlanGoodsIssue_Index == null ? "" : data.PlanGoodsIssue_Index);
                var PLI = new SqlParameter("@PLI", data.Pallet_No == null ? "" : data.Pallet_No);
                var TAO = new SqlParameter("@TAO", data.TagOut_No == null ? "" : data.TagOut_No);
                var LCTY = new SqlParameter("@LCTY", data.LocationType == null ? "" : data.LocationType);
                var LCTN = new SqlParameter("@LCTN", data.Current_location == null ? "" : data.Current_location);
                var ST = new SqlParameter("@ST", data.status == null ? "" : data.status);
                var CH = new SqlParameter("@CH", data.Chute_Id == null ? "" : data.Chute_Id);


                var rpt_data = Outbound_DBContext.View_Trace_picking.FromSql("sp_Trace_pick @GI ,@TL ,@PGL ,@PLI ,@TAO ,@LCTY ,@LCTN ,@CH ,@ST ", GI, TL, PGL, PLI, TAO, LCTY, LCTN, CH, ST).ToList();

                if (!string.IsNullOrEmpty(data.load_Date) && !string.IsNullOrEmpty(data.load_Date_To))
                {
                    var dateStart = data.load_Date.toBetweenDate();
                    var dateEnd = data.load_Date_To.toBetweenDate();
                    rpt_data = rpt_data.Where(c => c.TruckLoad_Date >= dateStart.start && c.TruckLoad_Date <= dateEnd.end).ToList();
                }

                var Item = new List<View_Trace_picking>();
                var TotalRow = new List<View_Trace_picking>();


                TotalRow = rpt_data.ToList();
                var Row = 1;

                if (!data.export)
                {
                    if (data.CurrentPage != 0 && data.PerPage != 0)
                    {
                        rpt_data = rpt_data.Skip(((data.CurrentPage - 1) * data.PerPage)).OrderBy(c => c.RowIndex).ToList();
                        Row = (data.CurrentPage == 1 ? 1 : ((data.CurrentPage - 1) * data.PerPage) + 1);
                    }

                    if (data.PerPage != 0)
                    {
                        rpt_data = rpt_data.Take(data.PerPage).OrderBy(c => c.RowIndex).ToList();

                    }
                    else
                    {
                        rpt_data = rpt_data.Take(50).OrderBy(c => c.RowIndex).ToList();
                    }
                }


                var result = new List<Trace_picking>();
                foreach (var item in rpt_data)
                {
                    Trace_picking trace = new Trace_picking();
                    trace.RowIndex = Row;
                    trace.Goodsissue_No = item.Goodsissue_No;
                    trace.TruckLoad_No = item.TruckLoad_No;
                    trace.TruckLoad_Date = item.TruckLoad_Date;
                    trace.PlanGoodsIssue_No = item.PlanGoodsIssue_No;
                    trace.Pallet_No = item.Pallet_No;
                    trace.TagOut_No = item.TagOut_No;
                    trace.Product_Id = item.Product_Id;
                    trace.Product_Name = item.Product_Name;
                    trace.Qty = item.Qty;
                    trace.ProductConversion_Name = item.ProductConversion_Name;
                    trace.Product_Lot = item.Product_Lot;
                    trace.LocationType = item.LocationType;
                    trace.Pick_location = item.Pick_location;
                    trace.Old_location = item.Old_location;
                    trace.Current_location = item.Current_location;
                    trace.status = item.status;
                    trace.Chute_Id = item.Chute_Id;
                    trace.RollCage_Id = item.RollCage_Id;
                    trace.DocumentRef_No5 = item.DocumentRef_No5;
                    trace.DocumentRef_No2 = item.DocumentRef_No2;
                    trace.TagOutRef_No1 = item.TagOutRef_No1;
                    trace.PickingPickQty_By = item.PickingPickQty_By;
                    trace.PickingPickQty_Date = item.PickingPickQty_Date;
                    Row++;
                    result.Add(trace);
                }
                var count = TotalRow.Count;
                var actionResult = new actionResultTrace_pickingViewModel();
                actionResult.itemsTrace = result.ToList();
                actionResult.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };
                return actionResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Excel Trace Pick
        public ResultExportModelPick ExcelOutPick(Trace_picking data)
        {
            var Outbound_DBContext = new GIDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            var olog = new logtxt();
            try
            {
                Outbound_DBContext.Database.SetCommandTimeout(360);


                var GI = new SqlParameter("@GI", data.Goodsissue_Index == null ? "" : data.Goodsissue_Index);
                var TL = new SqlParameter("@TL", data.TruckLoad_Index == null ? "" : data.TruckLoad_Index);
                var PGL = new SqlParameter("@PGL", data.PlanGoodsIssue_Index == null ? "" : data.PlanGoodsIssue_Index);
                var PLI = new SqlParameter("@PLI", data.Pallet_No == null ? "" : data.Pallet_No);
                var TAO = new SqlParameter("@TAO", data.TagOut_No == null ? "" : data.TagOut_No);
                var LCTY = new SqlParameter("@LCTY", data.LocationType == null ? "" : data.LocationType);
                var LCTN = new SqlParameter("@LCTN", data.Current_location == null ? "" : data.Current_location);
                var ST = new SqlParameter("@ST", data.status == null ? "" : data.status);
                var CH = new SqlParameter("@CH", data.Chute_Id == null ? "" : data.Chute_Id);


                var rpt_data = Outbound_DBContext.View_Trace_picking.FromSql("sp_Trace_pick @GI ,@TL ,@PGL ,@PLI ,@TAO ,@LCTY ,@LCTN ,@CH ,@ST ", GI, TL, PGL, PLI, TAO, LCTY, LCTN, CH, ST).ToList();

                if (!string.IsNullOrEmpty(data.load_Date) && !string.IsNullOrEmpty(data.load_Date_To))
                {
                    var dateStart = data.load_Date.toBetweenDate();
                    var dateEnd = data.load_Date_To.toBetweenDate();
                    rpt_data = rpt_data.Where(c => c.TruckLoad_Date >= dateStart.start && c.TruckLoad_Date <= dateEnd.end).ToList();
                }

                var Item = new List<View_Trace_picking>();
                var TotalRow = new List<View_Trace_picking>();


                TotalRow = rpt_data.ToList();
                var Row = 1;

                if (!data.export)
                {
                    if (data.CurrentPage != 0 && data.PerPage != 0)
                    {
                        rpt_data = rpt_data.Skip(((data.CurrentPage - 1) * data.PerPage)).OrderBy(c => c.RowIndex).ToList();
                        Row = (data.CurrentPage == 1 ? 1 : ((data.CurrentPage - 1) * data.PerPage) + 1);
                    }

                    if (data.PerPage != 0)
                    {
                        rpt_data = rpt_data.Take(data.PerPage).OrderBy(c => c.RowIndex).ToList();

                    }
                    else
                    {
                        rpt_data = rpt_data.Take(50).OrderBy(c => c.RowIndex).ToList();
                    }
                }


                var result = new List<ExportExcelModel_Pick>();
                foreach (var item in rpt_data)
                {
                    ExportExcelModel_Pick trace = new ExportExcelModel_Pick();
                    trace.RowIndex = Row;
                    trace.Goodsissue_No = item.Goodsissue_No;
                    trace.TruckLoad_No = item.TruckLoad_No;
                    trace.TruckLoad_Date = item.TruckLoad_Date;
                    trace.PlanGoodsIssue_No = item.PlanGoodsIssue_No;
                    trace.Pallet_No = item.Pallet_No;
                    trace.TagOut_No = item.TagOut_No;
                    trace.Product_Id = item.Product_Id;
                    trace.Product_Name = item.Product_Name;
                    trace.Qty = item.Qty;
                    trace.ProductConversion_Name = item.ProductConversion_Name;
                    trace.Product_Lot = item.Product_Lot;
                    trace.LocationType = item.LocationType;
                    trace.Pick_location = item.Pick_location;
                    trace.Old_location = item.Old_location;
                    trace.Current_location = item.Current_location;
                    trace.status = item.status;
                    trace.Chute_Id = item.Chute_Id;
                    trace.RollCage_Id = item.RollCage_Id;
                    trace.DocumentRef_No5 = item.DocumentRef_No5;
                    trace.DocumentRef_No2 = item.DocumentRef_No2;
                    trace.TagOutRef_No1 = item.TagOutRef_No1;
                    trace.PickingPickQty_By = item.PickingPickQty_By;
                    trace.PickingPickQty_Date = item.PickingPickQty_Date;
                    Row++;
                    result.Add(trace);
                }
                var count = TotalRow.Count;
                var actionResult = new ResultExportModelPick();
                actionResult.itemsTrace = result.ToList();
                return actionResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region printOutTraceLoading
        public actionResultTrace_loadingViewModel printOutTraceLoading(Trace_loading data)
        {
            var Outbound_DBContext = new GIDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            var olog = new logtxt();
            try
            {
                Outbound_DBContext.Database.SetCommandTimeout(360);


                var TL = new SqlParameter("@TL", data.TruckLoad_Index == null ? "" : data.TruckLoad_Index);
                var PGL = new SqlParameter("@PGL", data.PlanGoodsIssue_Index == null ? "" : data.PlanGoodsIssue_Index);
                var TAO = new SqlParameter("@TAO", data.TagOut_No == null ? "" : data.TagOut_No);
                var DK = new SqlParameter("@DK", data.Dock_Index == null ? "" : data.Dock_Index);
                var CH = new SqlParameter("@CH", data.Chute_Id == null ? "" : data.Chute_Id);
                var RC = new SqlParameter("@RC", data.RollCage_Index == null ? "" : data.RollCage_Index);
                var ST = new SqlParameter("@ST", data.status == null ? "" : data.status);
                var APT = new SqlParameter("@APT", data.Appointment_Time == null ? "" : data.Appointment_Time);


                var rpt_data = Outbound_DBContext.View_Trace_Loading.FromSql("sp_Trace_Loading @TL ,@PGL ,@TAO ,@DK ,@CH ,@RC ,@ST ,@APT ", TL, PGL, TAO, DK, CH, RC, ST, APT).ToList();

                if (!string.IsNullOrEmpty(data.load_Date) && !string.IsNullOrEmpty(data.load_Date_To))
                {
                    //var dateStart = data.load_Date.toBetweenDate();
                    //var dateEnd = data.load_Date_To.toBetweenDate();
                    //rpt_data = rpt_data.Where(c => c.TruckLoad_Date >= dateStart.start && c.TruckLoad_Date <= dateEnd.end).ToList();
                }

                var Item = new List<View_Trace_Loading>();
                var TotalRow = new List<View_Trace_Loading>();


                TotalRow = rpt_data.ToList();
                var Row = 1;

                if (!data.export)
                {
                    if (data.CurrentPage != 0 && data.PerPage != 0)
                    {
                        rpt_data = rpt_data.Skip(((data.CurrentPage - 1) * data.PerPage)).OrderBy(c => c.RowIndex).ToList();
                        Row = (data.CurrentPage == 1 ? 1 : ((data.CurrentPage - 1) * data.PerPage) + 1);
                    }

                    if (data.PerPage != 0)
                    {
                        rpt_data = rpt_data.Take(data.PerPage).OrderBy(c => c.RowIndex).ToList();

                    }
                    else
                    {
                        rpt_data = rpt_data.Take(50).OrderBy(c => c.RowIndex).ToList();
                    }
                }


                var result = new List<Trace_loading>();
                foreach (var item in rpt_data)
                {
                    Trace_loading trace = new Trace_loading();
                    trace.RowIndex = Row;
                    trace.TruckLoad_No = item.TruckLoad_No;
                    trace.Dock_Name = item.Dock_Name;
                    trace.TruckLoad_Date = item.TruckLoad_Date;
                    trace.PlanGoodsIssue_No = item.PlanGoodsIssue_No;
                    trace.Chute_Id = item.Chute_Id;
                    trace.RollCage_Name = item.RollCage_Name;
                    trace.IsTote = item.IsTote;
                    trace.TagOut_No = item.TagOut_No;
                    trace.Product_Id = item.Product_Id;
                    trace.Product_Name = item.Product_Name;
                    trace.Qty = item.Qty;
                    trace.ProductConversion_Name = item.ProductConversion_Name;
                    trace.Product_Lot = item.Product_Lot;
                    trace.status = item.status;
                    trace.LocationType = item.LocationType;
                    trace.TagOutRef_No1 = item.TagOutRef_No1;
                    trace.Address = item.Address;

                    Row++;
                    result.Add(trace);
                }
                var count = TotalRow.Count;
                var actionResult = new actionResultTrace_loadingViewModel();
                actionResult.itemsTrace = result.ToList();
                actionResult.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };
                return actionResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Excel Trace Loading 
        public ResultExportModelTraceLoading ExcelOutTraceLoading(Trace_loading data)
        {
            var Outbound_DBContext = new GIDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            var olog = new logtxt();
            try
            {
                Outbound_DBContext.Database.SetCommandTimeout(360);


                var TL = new SqlParameter("@TL", data.TruckLoad_Index == null ? "" : data.TruckLoad_Index);
                var PGL = new SqlParameter("@PGL", data.PlanGoodsIssue_Index == null ? "" : data.PlanGoodsIssue_Index);
                var TAO = new SqlParameter("@TAO", data.TagOut_No == null ? "" : data.TagOut_No);
                var DK = new SqlParameter("@DK", data.Dock_Index == null ? "" : data.Dock_Index);
                var CH = new SqlParameter("@CH", data.Chute_Id == null ? "" : data.Chute_Id);
                var RC = new SqlParameter("@RC", data.RollCage_Index == null ? "" : data.RollCage_Index);
                var ST = new SqlParameter("@ST", data.status == null ? "" : data.status);
                var APT = new SqlParameter("@APT", data.Appointment_Time == null ? "" : data.Appointment_Time);


                var rpt_data = Outbound_DBContext.View_Trace_Loading.FromSql("sp_Trace_Loading @TL ,@PGL ,@TAO ,@DK ,@CH ,@RC ,@ST ,@APT ", TL, PGL, TAO, DK, CH, RC, ST, APT).ToList();

                if (!string.IsNullOrEmpty(data.load_Date) && !string.IsNullOrEmpty(data.load_Date_To))
                {
                    var dateStart = data.load_Date.toBetweenDate();
                    var dateEnd = data.load_Date_To.toBetweenDate();
                    rpt_data = rpt_data.Where(c => c.TruckLoad_Date >= dateStart.start && c.TruckLoad_Date <= dateEnd.end).ToList();
                }

                var Item = new List<View_Trace_Loading>();
                var TotalRow = new List<View_Trace_Loading>();


                TotalRow = rpt_data.ToList();
                var Row = 1;

                if (!data.export)
                {
                    if (data.CurrentPage != 0 && data.PerPage != 0)
                    {
                        rpt_data = rpt_data.Skip(((data.CurrentPage - 1) * data.PerPage)).OrderBy(c => c.RowIndex).ToList();
                        Row = (data.CurrentPage == 1 ? 1 : ((data.CurrentPage - 1) * data.PerPage) + 1);
                    }

                    if (data.PerPage != 0)
                    {
                        rpt_data = rpt_data.Take(data.PerPage).OrderBy(c => c.RowIndex).ToList();

                    }
                    else
                    {
                        rpt_data = rpt_data.Take(50).OrderBy(c => c.RowIndex).ToList();
                    }
                }


                var result = new List<ExportExcelModel>();
                foreach (var item in rpt_data)
                {
                    ExportExcelModel trace = new ExportExcelModel();
                    trace.RowIndex = Row;
                    trace.TruckLoad_No = item.TruckLoad_No;
                    trace.Dock_Name = item.Dock_Name;
                    trace.TruckLoad_Date = item.TruckLoad_Date;
                    trace.PlanGoodsIssue_No = item.PlanGoodsIssue_No;
                    trace.Chute_Id = item.Chute_Id;
                    trace.RollCage_Name = item.RollCage_Name;
                    trace.IsTote = item.IsTote;
                    trace.TagOut_No = item.TagOut_No;
                    trace.Product_Id = item.Product_Id;
                    trace.Product_Name = item.Product_Name;
                    trace.Qty = item.Qty;
                    trace.ProductConversion_Name = item.ProductConversion_Name;
                    trace.Product_Lot = item.Product_Lot;
                    trace.status = item.status;
                    trace.LocationType = item.LocationType;
                    trace.TagOutRef_No1 = item.TagOutRef_No1;
                    trace.Address = item.Address;

                    Row++;
                    result.Add(trace);
                }
                var count = TotalRow.Count;
                var actionResult = new ResultExportModelTraceLoading();
                actionResult.itemsTrace = result.ToList();
                return actionResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region + Trace Transfer +

        public actionResultTrace_TransferViewModel printOutTraceTransferReplenish(TraceTransferModel data)
        {
            var culture = new System.Globalization.CultureInfo("en-US");
            var olog = new logtxt();
            var TransferDbContext = new TransferDbContext();
            try
            {
                TransferDbContext.Database.SetCommandTimeout(360);

                var PID = new SqlParameter("@ProductID", data.product_Id == null ? "" : data.product_Id.ToString());
                var PLOT = new SqlParameter("@ProductLot", data.product_Lot == null ? "" : data.product_Lot.ToString());
                var TAG = new SqlParameter("@TagNo", data.tag_No == null ? "" : data.tag_No.ToString());
                var LO = new SqlParameter("@LocationID", data.location_Id == null ? "" : data.location_Id.ToString());
                var GT = new SqlParameter("@GoodsTransferNo", data.goodsTransfer_No == null ? "" : data.goodsTransfer_No.ToString());
                var ST = new SqlParameter("@Status", data.processStatus_Id == null ? "" : data.processStatus_Id.ToString());
                var TFD = new SqlParameter("@Transfer_Date", data.transfer_Date == null ? "" : data.transfer_Date.ToString());
                var TFDT = new SqlParameter("@Transfer_Date_To", data.transfer_Date_To == null ? "" : data.transfer_Date_To.ToString());

                var responseData = TransferDbContext.sp_Trace_replenishment.FromSql("sp_Trace_replenishment @ProductID ,@ProductLot ,@TagNo ,@LocationID ,@GoodsTransferNo ,@Status ,@Transfer_Date , @Transfer_Date_To", PID, PLOT, TAG, LO, GT, ST, TFD, TFDT).ToList();

                if (!string.IsNullOrEmpty(data.transfer_Date) && !string.IsNullOrEmpty(data.transfer_Date_To))
                {
                    var dateStart = Convert.ToDateTime(data.transfer_Date);
                    var dateEnd = Convert.ToDateTime(data.transfer_Date_To);
                    responseData = responseData.Where(c => c.GoodsTransfer_Date >= dateStart && c.GoodsTransfer_Date <= dateEnd).ToList();
                }

                var TotalRow = new List<TransferDataAccess.Models.sp_Trace_replenishment>();

                TotalRow = responseData.ToList();
                var Row = 1;

                if (!data.export)
                {
                    if (data.CurrentPage != 0 && data.PerPage != 0)
                    {
                        responseData = responseData.Skip(((data.CurrentPage - 1) * data.PerPage)).OrderBy(c => c.RowIndex).ToList();
                        Row = (data.CurrentPage == 1 ? 1 : ((data.CurrentPage - 1) * data.PerPage) + 1);
                    }

                    if (data.PerPage != 0)
                    {
                        responseData = responseData.Take(data.PerPage).OrderBy(c => c.RowIndex).ToList();

                    }
                    else
                    {
                        responseData = responseData.Take(50).OrderBy(c => c.RowIndex).ToList();
                    }
                }

                var result = new List<TraceTransferModel>();
                foreach (var item in responseData)
                {
                    TraceTransferModel traceTransfer = new TraceTransferModel();
                    traceTransfer.rowIndex = Row;
                    traceTransfer.goodsTransfer_No = item.GoodsTransfer_No;
                    traceTransfer.tag_No = item.Tag_No;
                    traceTransfer.product_Id = item.Product_Id;
                    traceTransfer.product_Name = item.Product_Name;
                    traceTransfer.product_Lot = item.Product_Lot;
                    traceTransfer.qty = item.Qty;
                    traceTransfer.ratio = item.Ratio;
                    traceTransfer.totalQty = item.TotalQty;
                    traceTransfer.productConversion_Name = item.ProductConversion_Name;
                    traceTransfer.location_Id = item.Location_Id;
                    traceTransfer.location_Id_To = item.Location_Id_To;
                    traceTransfer.goodsTransfer_Date = Convert.ToDateTime(item.GoodsTransfer_Date).ToString("dd/MM/yyyy");
                    //traceTransfer.gt_Status = item.GT_Status;
                    //traceTransfer.gti_Status = item.GTI_Status;
                    //traceTransfer.t_Status = item.T_Status;
                    //traceTransfer.ti_Status = item.TI_Status; 

                    traceTransfer.remaining = item.Remaining;
                    traceTransfer.unit_Remaining = item.Unit_Remaining;
                    traceTransfer.total = item.Total;
                    traceTransfer.unit_Total = item.Unit_Total;

                    traceTransfer.create_By = item.Create_By;
                    traceTransfer.create_Date = Convert.ToDateTime(item.Create_Date).ToString("dd/MM/yyyy hh:mm:ss");
                    traceTransfer.update_By = item.Update_By;
                    traceTransfer.update_Date = Convert.ToDateTime(item.Update_Date).ToString("dd/MM/yyyy hh:mm:ss");

                    traceTransfer.documentType_Id = item.DocumentType_Id;
                    traceTransfer.processStatus_Index = item.ProcessStatus_Index;
                    traceTransfer.processStatus_Id = item.ProcessStatus_Id;
                    traceTransfer.processStatus_Name = item.ProcessStatus_Name;

                    Row++;
                    result.Add(traceTransfer);
                }
                var count = TotalRow.Count;
                var actionResult = new actionResultTrace_TransferViewModel();
                actionResult.itemsTrace = result.ToList();
                actionResult.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, };
                return actionResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using PlanGRBusiness.Libs;
using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportBusiness.Report1
{
    public class Report1Service
    {

        #region printReport1
        public dynamic printReport1(Report1ViewModel_V2 data, string rootPath = "")
        {
            var DBContext = new PlanGRDbContext();
            var GR_DBContext = new GRDbContext();
            var M_DBContext = new MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report1ViewModel_V2>();

            try
            {
                var query = GR_DBContext.View_Report1_GR.AsQueryable();
                query = query.Where(c => c.Document_status != -1);
                var statusModels = new List<int?>();
                if (!string.IsNullOrEmpty(data.goodsReceive_No))
                {
                    query = query.Where(c => c.GoodsReceive_No.Contains(data.goodsReceive_No));
                }
                if (!string.IsNullOrEmpty(data.planGoodsReceive_no))
                {
                    query = query.Where(c => c.PlanGoodsReceive_no.Contains(data.planGoodsReceive_no));
                }
                if (!string.IsNullOrEmpty(data.goodsReceive_date) && !string.IsNullOrEmpty(data.goodsReceive_date_to))
                {
                    var dateStart = data.goodsReceive_date.toBetweenDate();
                    var dateEnd = data.goodsReceive_date_to.toBetweenDate();
                    query = query.Where(c => c.GoodsReceive_date >= dateStart.start && c.GoodsReceive_date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date))
                {
                    var goodsReceive_date_From = data.goodsReceive_date.toBetweenDate();
                    query = query.Where(c => c.GoodsReceive_date >= goodsReceive_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date_to))
                {
                    var goodsReceive_date_To = data.goodsReceive_date_to.toBetweenDate();
                    query = query.Where(c => c.GoodsReceive_date <= goodsReceive_date_To.start);
                }

                if (!string.IsNullOrEmpty(data.documentType_Index.ToString().Replace("00000000-0000-0000-0000-000000000000", "")))
                {
                    query = query.Where(c => c.DocumentType_Index == data.documentType_Index);
                }

                if (data.status.Count > 0)
                {
                    foreach (var item in data.status)
                    {
                        statusModels.Add(item.value);
                        //if (item.value == 0)
                        //{
                        //    statusModels.Add(0);
                        //}
                        //if (item.value == 1)
                        //{
                        //    statusModels.Add(1);
                        //}
                        //if (item.value == 2)
                        //{
                        //    statusModels.Add(2);
                        //}
                        //if (item.value == 3)
                        //{
                        //    statusModels.Add(3);
                        //}
                        //if (item.value == -1)
                        //{
                        //    statusModels.Add(-1);
                        //}
                        //if (item.value == -2)
                        //{
                        //    statusModels.Add(-2);
                        //}
                    }
                    query = query.Where(c => statusModels.Contains(c.Document_status));
                }
                 
                var resultquery = query.ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new Report1ViewModel_V2();
                    var startDate = DateTime.ParseExact(data.goodsReceive_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.goodsReceive_date_to.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.goodsReceive_date = startDate;
                    resultItem.goodsReceive_date_to = endDate;
                    resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in resultquery)
                    {
                        var startDate = DateTime.ParseExact(data.goodsReceive_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.goodsReceive_date_to.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new Report1ViewModel_V2();
                        resultItem.rowNum = item.RowNum;
                        resultItem.goodsReceiveItem_Index = item.GoodsReceiveItem_Index;
                        resultItem.goodsReceive_No = item.GoodsReceive_No;
                        resultItem.goodsReceive_Index = item.GoodsReceive_Index;
                        resultItem.goodsReceive_Date = item.GoodsReceive_date.Value.ToString("dd/MM/yyyy hh:mm:ss");
                        resultItem.po_no = item.Po_no;
                        resultItem.po_date = (!string.IsNullOrEmpty(item.Po_date.toString())) ? item.Po_date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.planGoodsReceive_no = item.PlanGoodsReceive_no;
                        resultItem.planGoodsReceive_date = (!string.IsNullOrEmpty(item.PlanGoodsReceive_date.toString())) ? item.PlanGoodsReceive_date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.product_id = item.product_id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.qty = item.Qty;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.product_lot = item.Product_lot;
                        resultItem.qty_po = item.Qty_po;
                        resultItem.po_unit = item.Po_unit;
                        resultItem.qty_asn = item.Qty_asn;
                        resultItem.asn_unit = item.Asn_unit;
                        resultItem.qty_base_unit_gr = item.Qty_base_unit_gr;
                        resultItem.base_unit_gr = item.Base_unit_gr;
                        resultItem.itemStatus_Name = item.ItemStatus_Name;
                        resultItem.mfg_date = (!string.IsNullOrEmpty(item.Mfg_date.toString())) ? item.Mfg_date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.exp_date = (!string.IsNullOrEmpty(item.Exp_date.toString())) ? item.Exp_date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.erp_location = item.Erp_location;
                        resultItem.documentRef_No1 = item.DocumentRef_No1;
                        resultItem.documentType_name = item.DocumentType_name;
                        resultItem.documentRef_no2 = item.DocumentRef_no2;
                        resultItem.document_status = item.Document_status;
                        resultItem.processstatus_name = item.Processstatus_name;
                        resultItem.document_remark = item.Document_remark;
                        resultItem.create_By = item.Create_By;
                        resultItem.remainQty = item.Qty_asn - item.Qty;
                        resultItem.goodsReceive_date = startDate;
                        resultItem.goodsReceive_date_to = endDate;
                        resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");


                        //resultItem.planGoodsReceive_Index = item.PlanGoodsReceive_Index;
                        //resultItem.planGoodsReceiveItem_Index = item.PlanGoodsReceiveItem_Index;
                        //resultItem.product_Index = item.Product_Index;
                        //resultItem.productConversion_Index = item.ProductConversion_Index;


                        result.Add(resultItem);
                    }
                }

                //var queryGR = GR_DBContext.View_RPT_GRV2.AsQueryable();
                //var queryPlanGR = DBContext.View_RPT_PlanGRV2.AsQueryable();

                //if (!string.IsNullOrEmpty(data.planGoodsReceive_No))
                //{
                //    queryPlanGR = queryPlanGR.Where(c => c.PlanGoodsReceive_No.Contains(data.planGoodsReceive_No));
                //}

                //if (!string.IsNullOrEmpty(data.planGoodsReceive_date) && !string.IsNullOrEmpty(data.planGoodsReceive_date_To))
                //{
                //    var dateStart = data.planGoodsReceive_date.toBetweenDate();
                //    var dateEnd = data.planGoodsReceive_date_To.toBetweenDate();
                //    queryPlanGR = queryPlanGR.Where(c => c.PlanGoodsReceive_Date >= dateStart.start && c.PlanGoodsReceive_Date <= dateEnd.end);
                //}

                //else if (!string.IsNullOrEmpty(data.planGoodsReceive_date))
                //{
                //    var planGoodsReceive_date_From = data.planGoodsReceive_date.toBetweenDate();
                //    queryPlanGR = queryPlanGR.Where(c => c.PlanGoodsReceive_Date >= planGoodsReceive_date_From.start);
                //}
                //else if (!string.IsNullOrEmpty(data.planGoodsReceive_date_To))
                //{
                //    var planGoodsReceive_date_To = data.planGoodsReceive_date_To.toBetweenDate();
                //    queryPlanGR = queryPlanGR.Where(c => c.PlanGoodsReceive_Date <= planGoodsReceive_date_To.start);
                //}


                //var query = (from PGR in queryPlanGR.ToList()
                //             join PRDCAT in M_DBContext.MS_Product.Select(s => new
                //             {
                //                 s.Product_Index,
                //                 s.Product_Id,
                //                 s.ProductCategory_Index,
                //                 s.ProductCategory_Id,
                //                 s.ProductCategory_Name,
                //                 s.Ref_No3,
                //                 s.ProductConversion_Index,
                //                 s.ProductConversion_Id,
                //                 s.ProductConversion_Name
                //             }).ToList() on PGR.Product_Index equals PRDCAT.Product_Index into PDC
                //             from PRDCAT in PDC.DefaultIfEmpty()
                //             join GR in queryGR.ToList() on PGR.PlanGoodsReceiveItem_Index equals GR.Ref_DocumentItem_Index into GRJOIN
                //             from GR in GRJOIN.DefaultIfEmpty()
                //             select new
                //             {
                //                 PGR?.PlanGoodsReceive_Index,
                //                 PGR?.PlanGoodsReceiveItem_Index,
                //                 PGR?.Product_Index,
                //                 PRDCAT?.ProductConversion_Index,
                //                 PGR?.PlanGoodsReceive_Date,
                //                 PGR?.PlanGoodsReceive_No,
                //                 PGR?.Owner_Id,
                //                 PGR?.Owner_Name,
                //                 PGR?.DocumentRef_No2,
                //                 PGR?.Product_Id,
                //                 PGR?.Product_Name,
                //                 PGR?.PlanGoodsReceive_Due_Date,
                //                 PRDCAT?.ProductCategory_Id,
                //                 PRDCAT?.ProductCategory_Name,
                //                 PRDCAT?.Ref_No3,
                //                 PGR?.TotalQty,
                //                 PRDCAT?.ProductConversion_Name,
                //                 GR?.MIN_GoodsReceive_Date,
                //                 GR?.GR_TotalQty,
                //                 Remain_TotalQty = PGR.TotalQty - (GR?.GR_TotalQty ?? 0),
                //                 GR?.MAX_GoodsReceive_Date,

                //             }).ToList();


                //if (query.Count == 0)
                //{
                //    var resultItem = new Report1ViewModel();
                //    var startDate = DateTime.ParseExact(data.planGoodsReceive_date.Substring(0, 8), "yyyyMMdd",
                //    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //    var endDate = DateTime.ParseExact(data.planGoodsReceive_date_To.Substring(0, 8), "yyyyMMdd",
                //    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //    resultItem.planGoodsReceive_date = startDate;
                //    resultItem.planGoodsReceive_date_To = endDate;
                //    result.Add(resultItem);
                //}
                //else
                //{
                //    foreach (var item in query)
                //    {

                //        string date = item.PlanGoodsReceive_Date.toString();
                //        string planGRDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                //        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //        string dueDate = item.PlanGoodsReceive_Due_Date.toString();
                //        string planGR_DueDate = DateTime.ParseExact(dueDate.Substring(0, 8), "yyyyMMdd",
                //        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //        var resultItem = new Report1ViewModel();
                //        //resultItem.planGoodsReceive_Index = item.PlanGoodsReceive_Index;
                //        //resultItem.planGoodsReceiveItem_Index = item.PlanGoodsReceiveItem_Index;
                //        //resultItem.product_Index = item.Product_Index;
                //        //resultItem.productConversion_Index = item.ProductConversion_Index;
                //        resultItem.planGoodsReceive_Date = item.PlanGoodsReceive_Date == null ? "" : item.PlanGoodsReceive_Date.Value.ToString("dd/MM/yyyy");
                //        resultItem.planGoodsReceive_No = item.PlanGoodsReceive_No;
                //        resultItem.owner_Id = item.Owner_Id;
                //        resultItem.owner_Name = item.Owner_Name;
                //        resultItem.documentRef_No2 = item.DocumentRef_No2;
                //        resultItem.product_Id = item.Product_Id;
                //        resultItem.product_Name = item.Product_Name;
                //        resultItem.planGoodsReceive_Due_Date = item.PlanGoodsReceive_Due_Date == null ? "" : item.PlanGoodsReceive_Due_Date.Value.ToString("dd/MM/yyyy");
                //        resultItem.productCategory_Name = item.ProductCategory_Id;
                //        resultItem.ref_No3 = item.Ref_No3;
                //        resultItem.totalQty = item.TotalQty;
                //        resultItem.productConversion_Name = item.ProductConversion_Name;
                //        resultItem.min_GoodsReceive_Date = item.MIN_GoodsReceive_Date == null ? "" : item.MIN_GoodsReceive_Date.Value.ToString("dd/MM/yyyy");
                //        resultItem.gr_TotalQty = item.GR_TotalQty;
                //        resultItem.remain_TotalQty = item.Remain_TotalQty;
                //        resultItem.max_GoodsReceive_Date = item.MAX_GoodsReceive_Date == null ? "" : item.MAX_GoodsReceive_Date.Value.ToString("dd/MM/yyyy");



                //        var startDate = DateTime.ParseExact(data.planGoodsReceive_date.Substring(0, 8), "yyyyMMdd",
                //        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //        var endDate = DateTime.ParseExact(data.planGoodsReceive_date_To.Substring(0, 8), "yyyyMMdd",
                //        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //        resultItem.planGoodsReceive_date = startDate;
                //        resultItem.planGoodsReceive_date_To = endDate;

                //        result.Add(resultItem);
                //    }
                //}


                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report1\\Report1.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report1");
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




        public string ExportExcel(Report1ViewModel_V2 data, string rootPath = "")
        {
            var DBContext = new PlanGRDbContext();
            var GR_DBContext = new GRDbContext();
            var M_DBContext = new MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<Report1ViewModel_V2>();
            try
            {

                var query = GR_DBContext.View_Report1_GR.AsQueryable();
                query = query.Where(c => c.Document_status != -1);
                var statusModels = new List<int?>();
                if (!string.IsNullOrEmpty(data.goodsReceive_No))
                {
                    query = query.Where(c => c.GoodsReceive_No.Contains(data.goodsReceive_No));
                }
                if (!string.IsNullOrEmpty(data.planGoodsReceive_no))
                {
                    query = query.Where(c => c.PlanGoodsReceive_no.Contains(data.planGoodsReceive_no));
                }
                if (!string.IsNullOrEmpty(data.goodsReceive_date) && !string.IsNullOrEmpty(data.goodsReceive_date_to))
                {
                    var dateStart = data.goodsReceive_date.toBetweenDate();
                    var dateEnd = data.goodsReceive_date_to.toBetweenDate();
                    query = query.Where(c => c.GoodsReceive_date >= dateStart.start && c.GoodsReceive_date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date))
                {
                    var goodsReceive_date_From = data.goodsReceive_date.toBetweenDate();
                    query = query.Where(c => c.GoodsReceive_date >= goodsReceive_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date_to))
                {
                    var goodsReceive_date_To = data.goodsReceive_date_to.toBetweenDate();
                    query = query.Where(c => c.GoodsReceive_date <= goodsReceive_date_To.start);
                }
                if (!string.IsNullOrEmpty(data.documentType_Index.ToString().Replace("00000000-0000-0000-0000-000000000000", "")))
                {
                    query = query.Where(c => c.DocumentType_Index == data.documentType_Index);
                }

                if (data.status.Count > 0)
                {
                    foreach (var item in data.status)
                    {
                        statusModels.Add(item.value);
                        //if (item.value == 0)
                        //{
                        //    statusModels.Add(0);
                        //}
                        //if (item.value == 1)
                        //{
                        //    statusModels.Add(1);
                        //}
                        //if (item.value == 2)
                        //{
                        //    statusModels.Add(2);
                        //}
                        //if (item.value == 3)
                        //{
                        //    statusModels.Add(3);
                        //}
                        //if (item.value == -1)
                        //{
                        //    statusModels.Add(-1);
                        //}
                        //if (item.value == -2)
                        //{
                        //    statusModels.Add(-2);
                        //}
                    }
                    query = query.Where(c => statusModels.Contains(c.Document_status));
                }

                var resultquery = query.ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new Report1ViewModel_V2();
                    var startDate = DateTime.ParseExact(data.goodsReceive_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.goodsReceive_date_to.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.goodsReceive_date = startDate;
                    resultItem.goodsReceive_date_to = endDate;
                    resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in resultquery)
                    {
                        var startDate = DateTime.ParseExact(data.goodsReceive_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.goodsReceive_date_to.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new Report1ViewModel_V2();
                        resultItem.rowNum = item.RowNum;
                        resultItem.goodsReceiveItem_Index = item.GoodsReceiveItem_Index;
                        resultItem.goodsReceive_No = item.GoodsReceive_No;
                        resultItem.goodsReceive_Index = item.GoodsReceive_Index;
                        resultItem.goodsReceive_Date = item.GoodsReceive_date.Value.ToString("dd/MM/yyyy hh:mm:ss");
                        resultItem.po_no = item.Po_no;
                        resultItem.po_date = (!string.IsNullOrEmpty(item.Po_date.toString())) ? item.Po_date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.planGoodsReceive_no = item.PlanGoodsReceive_no;
                        resultItem.planGoodsReceive_date = (!string.IsNullOrEmpty(item.PlanGoodsReceive_date.toString())) ? item.PlanGoodsReceive_date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.product_id = item.product_id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.qty = item.Qty;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.product_lot = item.Product_lot;
                        resultItem.qty_po = item.Qty_po;
                        resultItem.po_unit = item.Po_unit;
                        resultItem.qty_asn = item.Qty_asn;
                        resultItem.asn_unit = item.Asn_unit;
                        resultItem.qty_base_unit_gr = item.Qty_base_unit_gr;
                        resultItem.base_unit_gr = item.Base_unit_gr;
                        resultItem.itemStatus_Name = item.ItemStatus_Name;
                        resultItem.mfg_date = (!string.IsNullOrEmpty(item.Mfg_date.toString())) ? item.Mfg_date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.exp_date = (!string.IsNullOrEmpty(item.Exp_date.toString())) ? item.Exp_date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.erp_location = item.Erp_location;
                        resultItem.documentRef_No1 = item.DocumentRef_No1;
                        resultItem.documentType_name = item.DocumentType_name;
                        resultItem.documentRef_no2 = item.DocumentRef_no2;
                        resultItem.document_status = item.Document_status;
                        resultItem.processstatus_name = item.Processstatus_name;
                        resultItem.document_remark = item.Document_remark;
                        resultItem.create_By = item.Create_By;
                        resultItem.remainQty = item.Qty_asn - item.Qty;
                        resultItem.goodsReceive_date = startDate;
                        resultItem.goodsReceive_date_to = endDate;
                        resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");

                        //resultItem.planGoodsReceive_Index = item.PlanGoodsReceive_Index;
                        //resultItem.planGoodsReceiveItem_Index = item.PlanGoodsReceiveItem_Index;
                        //resultItem.product_Index = item.Product_Index;
                        //resultItem.productConversion_Index = item.ProductConversion_Index;


                        result.Add(resultItem);
                    }
                }

                //var result = new List<Report1ViewModel>();
                //rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + new AppSettingConfig().GetUrl("Report1");
                ////var reportPath = rootPath + "\\Reports\\Invoice\\Invoice.rdlc";

                //var queryGR = GR_DBContext.View_RPT_GRV2.AsQueryable();
                //var queryPlanGR = DBContext.View_RPT_PlanGRV2.AsQueryable();

                //if (!string.IsNullOrEmpty(data.planGoodsReceive_No))
                //{
                //    queryPlanGR = queryPlanGR.Where(c => c.PlanGoodsReceive_No.Contains(data.planGoodsReceive_No));
                //}

                //if (!string.IsNullOrEmpty(data.planGoodsReceive_date) && !string.IsNullOrEmpty(data.planGoodsReceive_date_To))
                //{
                //    var dateStart = data.planGoodsReceive_date.toBetweenDate();
                //    var dateEnd = data.planGoodsReceive_date_To.toBetweenDate();
                //    queryPlanGR = queryPlanGR.Where(c => c.PlanGoodsReceive_Date >= dateStart.start && c.PlanGoodsReceive_Date <= dateEnd.end);
                //}

                //else if (!string.IsNullOrEmpty(data.planGoodsReceive_date))
                //{
                //    var planGoodsReceive_date_From = data.planGoodsReceive_date.toBetweenDate();
                //    queryPlanGR = queryPlanGR.Where(c => c.PlanGoodsReceive_Date >= planGoodsReceive_date_From.start);
                //}
                //else if (!string.IsNullOrEmpty(data.planGoodsReceive_date_To))
                //{
                //    var planGoodsReceive_date_To = data.planGoodsReceive_date_To.toBetweenDate();
                //    queryPlanGR = queryPlanGR.Where(c => c.PlanGoodsReceive_Date <= planGoodsReceive_date_To.start);
                //}
                //var query = (from PGR in queryPlanGR.ToList()
                //             join PRDCAT in M_DBContext.MS_Product.Select(s => new
                //             {
                //                 s.Product_Index,
                //                 s.Product_Id,
                //                 s.ProductCategory_Index,
                //                 s.ProductCategory_Id,
                //                 s.ProductCategory_Name,
                //                 s.Ref_No3,
                //                 s.ProductConversion_Index,
                //                 s.ProductConversion_Id,
                //                 s.ProductConversion_Name
                //             }).ToList() on PGR.Product_Index equals PRDCAT.Product_Index into PDC
                //             from PRDCAT in PDC.DefaultIfEmpty()
                //             join GR in queryGR.ToList() on PGR.PlanGoodsReceiveItem_Index equals GR.Ref_DocumentItem_Index into GRJOIN
                //             from GR in GRJOIN.DefaultIfEmpty()
                //             select new
                //             {
                //                 PGR?.PlanGoodsReceive_Index,
                //                 PGR?.PlanGoodsReceiveItem_Index,
                //                 PGR?.Product_Index,
                //                 PRDCAT?.ProductConversion_Index,
                //                 PGR?.PlanGoodsReceive_Date,
                //                 PGR?.PlanGoodsReceive_No,
                //                 PGR?.Owner_Id,
                //                 PGR?.Owner_Name,
                //                 PGR?.DocumentRef_No2,
                //                 PGR?.Product_Id,
                //                 PGR?.Product_Name,
                //                 PGR?.PlanGoodsReceive_Due_Date,
                //                 PRDCAT?.ProductCategory_Id,
                //                 PRDCAT?.ProductCategory_Name,
                //                 PRDCAT?.Ref_No3,
                //                 PGR?.TotalQty,
                //                 PRDCAT?.ProductConversion_Name,
                //                 GR?.MIN_GoodsReceive_Date,
                //                 GR?.GR_TotalQty,
                //                 Remain_TotalQty = PGR.TotalQty - (GR?.GR_TotalQty ?? 0),
                //                 GR?.MAX_GoodsReceive_Date,

                //             }).ToList();


                //if (query.Count == 0)
                //{
                //    var resultItem = new Report1ViewModel();
                //    var startDate = DateTime.ParseExact(data.planGoodsReceive_date.Substring(0, 8), "yyyyMMdd",
                //    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //    var endDate = DateTime.ParseExact(data.planGoodsReceive_date_To.Substring(0, 8), "yyyyMMdd",
                //    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //    resultItem.planGoodsReceive_date = startDate;
                //    resultItem.planGoodsReceive_date_To = endDate;
                //    result.Add(resultItem);
                //}
                //else
                //{
                //    foreach (var item in query)
                //    {

                //        string date = item.PlanGoodsReceive_Date.toString();
                //        string planGRDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                //        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //        string dueDate = item.PlanGoodsReceive_Due_Date.toString();
                //        string planGR_DueDate = DateTime.ParseExact(dueDate.Substring(0, 8), "yyyyMMdd",
                //        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //        var resultItem = new Report1ViewModel();
                //        //resultItem.planGoodsReceive_Index = item.PlanGoodsReceive_Index;
                //        //resultItem.planGoodsReceiveItem_Index = item.PlanGoodsReceiveItem_Index;
                //        //resultItem.product_Index = item.Product_Index;
                //        //resultItem.productConversion_Index = item.ProductConversion_Index;
                //        resultItem.planGoodsReceive_Date = item.PlanGoodsReceive_Date == null ? "" : item.PlanGoodsReceive_Date.Value.ToString("dd/MM/yyyy");
                //        resultItem.planGoodsReceive_No = item.PlanGoodsReceive_No;
                //        resultItem.owner_Id = item.Owner_Id;
                //        resultItem.owner_Name = item.Owner_Name;
                //        resultItem.documentRef_No2 = item.DocumentRef_No2;
                //        resultItem.product_Id = item.Product_Id;
                //        resultItem.product_Name = item.Product_Name;
                //        resultItem.planGoodsReceive_Due_Date = item.PlanGoodsReceive_Due_Date == null ? "" : item.PlanGoodsReceive_Due_Date.Value.ToString("dd/MM/yyyy");
                //        resultItem.productCategory_Name = item.ProductCategory_Id;
                //        resultItem.ref_No3 = item.Ref_No3;
                //        resultItem.totalQty = item.TotalQty;
                //        resultItem.productConversion_Name = item.ProductConversion_Name;
                //        resultItem.min_GoodsReceive_Date = item.MIN_GoodsReceive_Date == null ? "" : item.MIN_GoodsReceive_Date.Value.ToString("dd/MM/yyyy");
                //        resultItem.gr_TotalQty = item.GR_TotalQty;
                //        resultItem.remain_TotalQty = item.Remain_TotalQty;
                //        resultItem.max_GoodsReceive_Date = item.MAX_GoodsReceive_Date == null ? "" : item.MAX_GoodsReceive_Date.Value.ToString("dd/MM/yyyy");



                //        var startDate = DateTime.ParseExact(data.planGoodsReceive_date.Substring(0, 8), "yyyyMMdd",
                //        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //        var endDate = DateTime.ParseExact(data.planGoodsReceive_date_To.Substring(0, 8), "yyyyMMdd",
                //        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //        resultItem.planGoodsReceive_date = startDate;
                //        resultItem.planGoodsReceive_date_To = endDate;

                //        result.Add(resultItem);
                //    }
                //}

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report1");

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

        

    }
}

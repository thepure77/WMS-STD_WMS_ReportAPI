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

namespace ReportBusiness.ReportCheckTransactionGR
{
    public class ReportCheckTransactionGRService
    {
        #region printReport
        public dynamic printReportCheckTransactionGR(ReportCheckTransactionGRViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            //var BB_DBContext = new BinbalanceDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var GI_DBContext = new PlanGIDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportCheckTransactionGRViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var aSN_No = "";
                var pO_No = "";
                var product_Id = "";
                //var product_Name = "";
                var goodsReceive_No = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                var statusModels = new List<int?>();
                if (!string.IsNullOrEmpty(data.planGoodsReceive_No))
                {
                    aSN_No = data.planGoodsReceive_No;
                }
                if (!string.IsNullOrEmpty(data.pO_No))
                {
                    pO_No = data.pO_No;
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                //if (!string.IsNullOrEmpty(data.product_Name))
                //{
                //    product_Name = data.product_Name;
                //}
                if (!string.IsNullOrEmpty(data.goodsReceive_No))
                {
                    goodsReceive_No = data.goodsReceive_No;
                }
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }

                var asn = new SqlParameter("@ASN", aSN_No);
                var po = new SqlParameter("@PO", pO_No);
                var pro_id = new SqlParameter("@PRO_ID", product_Id);
                //var pro_name = new SqlParameter("@PRO_Name", product_Name);
                var gr = new SqlParameter("@GR", goodsReceive_No);
                var asn_date = new SqlParameter("@ASN_DATE", dateStart);
                var asn_date_to = new SqlParameter("@ASN_DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_ReportCheckTransactionGR>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_ReportCheckTransactionGR.FromSql("sp_ReportCheckTransactionGR @ASN,@PO,@PRO_ID,@GR,@ASN_DATE,@ASN_DATE_TO", asn, po, pro_id, gr, asn_date, asn_date_to).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_ReportCheckTransactionGR.FromSql("sp_ReportCheckTransactionGR @ASN,@PO,@PRO_ID,@GR,@ASN_DATE,@ASN_DATE_TO", asn, po, pro_id, gr, asn_date, asn_date_to).ToList();
                }

                //var resultquery = Master_DBContext.sp_ReportCheckTransactionGR.FromSql("sp_ReportCheckTransactionGR @ASN,@PO,@PRO_ID,@GR,@ASN_DATE,@ASN_DATE_TO", asn, po, pro_id, gr, asn_date, asn_date_to).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportCheckTransactionGRViewModel();
                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.report_date = startDate;
                    resultItem.report_date_to = endDate;
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

                        var resultItem = new ReportCheckTransactionGRViewModel();
                        resultItem.rowNum = num + 1;
                        resultItem.pO_No = item.PO_No;
                        resultItem.aSN_No = item.ASN_No;
                        resultItem.aSN_Date = item.ASN_Date != null ? item.ASN_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.aSN_Linenum = item.ASN_Linenum;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.plan_Qty = item.Plan_Qty;
                        resultItem.gR_Qty = item.GR_Qty;
                        resultItem.pending_Qty = item.Pending_Qty;
                        resultItem.aSN_UNIT = item.ASN_UNIT;
                        resultItem.bU_ASN_QTY = item.BU_ASN_QTY;
                        resultItem.bU_GRQty = item.BU_GRQty;
                        resultItem.bU_Unit = item.BU_Unit;
                        resultItem.sU_ASN_QTY = item.SU_ASN_QTY;
                        resultItem.sU_GRQty = item.SU_GRQty;
                        resultItem.sU_Unit = item.SU_Unit;
                        resultItem.goodsReceive_No = item.GoodsReceive_No;
                        resultItem.goodsReceive_Date = item.GoodsReceive_Date != null ? item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.matdoc = item.Matdoc;
                        resultItem.remark = item.Remark;

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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportCheckTransactionGR");
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
                olog.logging("ReportCheckTransactionGR", ex.Message);
                throw ex;
            }
        }
        #endregion

        public string ExportExcel(ReportCheckTransactionGRViewModel data, string rootPath = "")
        {
            var DBContext = new PlanGRDbContext();
            var GR_DBContext = new GRDbContext();
            var M_DBContext = new MasterDataDbContext();
            var GI_DBContext = new PlanGIDbContext();
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportCheckTransactionGRViewModel>();
            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var aSN_No = "";
                var pO_No = "";
                var product_Id = "";
                //var product_Name = "";
                var goodsReceive_No = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                var statusModels = new List<int?>();
                if (!string.IsNullOrEmpty(data.planGoodsReceive_No))
                {
                    aSN_No = data.planGoodsReceive_No;
                }
                if (!string.IsNullOrEmpty(data.pO_No))
                {
                    pO_No = data.pO_No;
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                //if (!string.IsNullOrEmpty(data.product_Name))
                //{
                //    product_Name = data.product_Name;
                //}
                if (!string.IsNullOrEmpty(data.goodsReceive_No))
                {
                    goodsReceive_No = data.goodsReceive_No;
                }
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }

                var asn = new SqlParameter("@ASN", aSN_No);
                var po = new SqlParameter("@PO", pO_No);
                var pro_id = new SqlParameter("@PRO_ID", product_Id);
                //var pro_name = new SqlParameter("@PRO_Name", product_Name);
                var gr = new SqlParameter("@GR", goodsReceive_No);
                var asn_date = new SqlParameter("@ASN_DATE", dateStart);
                var asn_date_to = new SqlParameter("@ASN_DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_ReportCheckTransactionGR>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_ReportCheckTransactionGR.FromSql("sp_ReportCheckTransactionGR @ASN,@PO,@PRO_ID,@GR,@ASN_DATE,@ASN_DATE_TO", asn, po, pro_id, gr, asn_date, asn_date_to).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_ReportCheckTransactionGR.FromSql("sp_ReportCheckTransactionGR @ASN,@PO,@PRO_ID,@GR,@ASN_DATE,@ASN_DATE_TO", asn, po, pro_id, gr, asn_date, asn_date_to).ToList();
                }

                //var resultquery = Master_DBContext.sp_ReportCheckTransactionGR.FromSql("sp_ReportCheckTransactionGR @ASN,@PO,@PRO_ID,@GR,@ASN_DATE,@ASN_DATE_TO", asn, po, pro_id, gr, asn_date, asn_date_to).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportCheckTransactionGRViewModel();
                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.report_date = startDate;
                    resultItem.report_date_to = endDate;
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

                        var resultItem = new ReportCheckTransactionGRViewModel();
                        resultItem.rowNum = num + 1;
                        resultItem.pO_No = item.PO_No;
                        resultItem.aSN_No = item.ASN_No;
                        resultItem.aSN_Date = item.ASN_Date != null ? item.ASN_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.aSN_Linenum = item.ASN_Linenum;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.plan_Qty = item.Plan_Qty;
                        resultItem.gR_Qty = item.GR_Qty;
                        resultItem.pending_Qty = item.Pending_Qty;
                        resultItem.aSN_UNIT = item.ASN_UNIT;
                        resultItem.bU_ASN_QTY = item.BU_ASN_QTY;
                        resultItem.bU_GRQty = item.BU_GRQty;
                        resultItem.bU_Unit = item.BU_Unit;
                        resultItem.sU_ASN_QTY = item.SU_ASN_QTY;
                        resultItem.sU_GRQty = item.SU_GRQty;
                        resultItem.sU_Unit = item.SU_Unit;
                        resultItem.goodsReceive_No = item.GoodsReceive_No;
                        resultItem.goodsReceive_Date = item.GoodsReceive_Date != null ? item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.matdoc = item.Matdoc;
                        resultItem.remark = item.Remark;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportCheckTransactionGR");

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

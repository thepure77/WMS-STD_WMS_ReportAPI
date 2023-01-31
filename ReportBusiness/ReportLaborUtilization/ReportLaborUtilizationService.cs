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

namespace ReportBusiness.ReportLaborUtilization
{
    public class ReportLaborUtilizationService
    {
        #region printReport
        public dynamic printReportLaborUtilization(ReportLaborUtilizationViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            //var BB_DBContext = new BinbalanceDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var GI_DBContext = new PlanGIDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportLaborUtilizationViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var goodsTransfer_No = "";
                var tag_No = "";
                var product_Id = "";
                //var product_Name = "";
                var location_Name_To = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                var statusModels = new List<int?>();
                if (!string.IsNullOrEmpty(data.goodsTransfer_No))
                {
                    goodsTransfer_No = data.goodsTransfer_No;
                }
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    tag_No = data.tag_No;
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                //if (!string.IsNullOrEmpty(data.product_Name))
                //{
                //    product_Name = data.product_Name;
                //}
                if (!string.IsNullOrEmpty(data.location_Name_To))
                {
                    location_Name_To = data.location_Name_To;
                }
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }

                var gt = new SqlParameter("@GT", goodsTransfer_No);
                var tag = new SqlParameter("@TAG", tag_No);
                var loc_name = new SqlParameter("@LOC_TO", location_Name_To);
                var pro_id = new SqlParameter("@PRO_ID", product_Id);
                //var pro_name = new SqlParameter("@PRO_ID", product_Name);
                var c_date = new SqlParameter("@C_DATE", dateStart);
                var c_date_to = new SqlParameter("@C_DATE_TO", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_ReportCheckTransfer>();
                //if (data.ambientRoom != "02")
                //{
                //    resultquery = Master_DBContext.sp_ReportCheckTransfer.FromSql("sp_ReportCheckTransfer @GT,@TAG,@LOC_TO,@PRO_ID,@C_DATE,@C_DATE_TO", gt, tag, loc_name, pro_id, c_date, c_date_to).ToList();
                //}
                //else
                //{
                //    resultquery = temp_Master_DBContext.sp_ReportCheckTransfer.FromSql("sp_ReportCheckTransfer @GT,@TAG,@LOC_TO,@PRO_ID,@C_DATE,@C_DATE_TO", gt, tag, loc_name, pro_id, c_date, c_date_to).ToList();
                //}

                ////var resultquery = Master_DBContext.sp_ReportCheckTransfer.FromSql("sp_ReportCheckTransfer @GT,@TAG,@LOC_TO,@PRO_ID,@C_DATE,@C_DATE_TO", gt, tag, loc_name, pro_id, c_date, c_date_to).ToList();
                //if (resultquery.Count == 0)
                //{
                //    var resultItem = new ReportLaborUtilizationViewModel();
                //    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                //    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                //    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //    resultItem.report_date = startDate;
                //    resultItem.report_date_to = endDate;
                //    result.Add(resultItem);
                //}
                //else
                //{
                //    int num = 0;
                //    foreach (var item in resultquery)
                //    {
                //        var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                //        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //        var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                //        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //        var resultItem = new ReportLaborUtilizationViewModel();
                //        resultItem.rowNum = num + 1;
                //        resultItem.goodsTransfer_No = item.GoodsTransfer_No;
                //        resultItem.documentType_Name = item.DocumentType_Name;
                //        resultItem.create_Date = item.Create_Date;
                //        resultItem.create_By = item.Create_By;
                //        resultItem.tag_No = item.Tag_No;
                //        resultItem.product_Id = item.Product_Id;
                //        resultItem.product_Name = item.Product_Name;
                //        resultItem.qty = item.Qty;
                //        resultItem.totalQty = item.TotalQty;
                //        resultItem.location_Name = item.Location_Name;
                //        resultItem.location_Name_To = item.Location_Name_To;
                //        resultItem.item_Status_Name = item.ItemStatus_Name;
                //        resultItem.item_Status_Name_To = item.ItemStatus_Name_To;
                //        resultItem.eRP_Location = item.ERP_Location;
                //        resultItem.eRP_Location_To = item.ERP_Location_To;
                //        resultItem.document_Status = item.Document_Status;
                //        resultItem.item_Document_Status = item.Item_Document_Status;
                //        resultItem.goodsReceiveItemLocation_Index = item.GoodsReceiveItemLocation_Index;
                //        resultItem.goodsTransferItem_Index = item.GoodsTransferItem_Index;
                //        if (data.ambientRoom != "02")
                //        {
                //            resultItem.ambientRoom = "Ambient";
                //        }
                //        else
                //        {
                //            resultItem.ambientRoom = "Freeze";
                //        }
                //        result.Add(resultItem);
                //        num++;
                //    }
                //}
                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportLaborUtilization");
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

        public string printReportLaborIncentiveScheme(ReportLaborUtilizationViewModel models, string contentRootPath)
        {
            throw new NotImplementedException();
        }
        #endregion

        public string ExportExcel(ReportLaborUtilizationViewModel data, string rootPath = "")
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
            var result = new List<ReportLaborUtilizationViewModel>();
            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var goodsTransfer_No = "";
                var tag_No = "";
                var product_Id = "";
                //var product_Name = "";
                var location_Name_To = "";
                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;
                var statusModels = new List<int?>();
                if (!string.IsNullOrEmpty(data.goodsTransfer_No))
                {
                    goodsTransfer_No = data.goodsTransfer_No;
                }
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    tag_No = data.tag_No;
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                //if (!string.IsNullOrEmpty(data.product_Name))
                //{
                //    product_Name = data.product_Name;
                //}
                if (!string.IsNullOrEmpty(data.location_Name_To))
                {
                    location_Name_To = data.location_Name_To;
                }
                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;
                }

                //var gt = new SqlParameter("@GT", goodsTransfer_No);
                //var tag = new SqlParameter("@TAG", tag_No);
                //var loc_name = new SqlParameter("@LOC_TO", location_Name_To);
                //var pro_id = new SqlParameter("@PRO_ID", product_Id);
                ////var pro_name = new SqlParameter("@PRO_ID", product_Name);
                //var c_date = new SqlParameter("@C_DATE", dateStart);
                //var c_date_to = new SqlParameter("@C_DATE_TO", dateEnd);
                //var resultquery = new List<MasterDataDataAccess.Models.sp_ReportCheckTransfer>();
                //if (data.ambientRoom != "02")
                //{
                //    resultquery = Master_DBContext.sp_ReportCheckTransfer.FromSql("sp_ReportCheckTransfer @GT,@TAG,@LOC_TO,@PRO_ID,@C_DATE,@C_DATE_TO", gt, tag, loc_name, pro_id, c_date, c_date_to).ToList();
                //}
                //else
                //{
                //    resultquery = temp_Master_DBContext.sp_ReportCheckTransfer.FromSql("sp_ReportCheckTransfer @GT,@TAG,@LOC_TO,@PRO_ID,@C_DATE,@C_DATE_TO", gt, tag, loc_name, pro_id, c_date, c_date_to).ToList();
                //}

                ////var resultquery = Master_DBContext.sp_ReportCheckTransfer.FromSql("sp_ReportCheckTransfer @GT,@TAG,@LOC_TO,@PRO_ID,@C_DATE,@C_DATE_TO", gt, tag, loc_name, pro_id, c_date, c_date_to).ToList();
                //if (resultquery.Count == 0)
                //{
                //    var resultItem = new ReportLaborUtilizationViewModel();
                //    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                //    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                //    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //    resultItem.report_date = startDate;
                //    resultItem.report_date_to = endDate;
                //    result.Add(resultItem);
                //}
                //else
                //{
                //    int num = 0;
                //    foreach (var item in resultquery)
                //    {
                //        var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                //        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //        var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                //        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //        var resultItem = new ReportLaborUtilizationViewModel();
                //        resultItem.rowNum = num + 1;
                //        resultItem.goodsTransfer_No = item.GoodsTransfer_No;
                //        resultItem.documentType_Name = item.DocumentType_Name;
                //        resultItem.create_Date = item.Create_Date;
                //        resultItem.create_By = item.Create_By;
                //        resultItem.tag_No = item.Tag_No;
                //        resultItem.product_Id = item.Product_Id;
                //        resultItem.product_Name = item.Product_Name;
                //        resultItem.qty = item.Qty;
                //        resultItem.totalQty = item.TotalQty;
                //        resultItem.location_Name = item.Location_Name;
                //        resultItem.location_Name_To = item.Location_Name_To;
                //        resultItem.item_Status_Name = item.ItemStatus_Name;
                //        resultItem.item_Status_Name_To = item.ItemStatus_Name_To;
                //        resultItem.eRP_Location = item.ERP_Location;
                //        resultItem.eRP_Location_To = item.ERP_Location_To;
                //        resultItem.document_Status = item.Document_Status;
                //        resultItem.item_Document_Status = item.Item_Document_Status;
                //        resultItem.goodsReceiveItemLocation_Index = item.GoodsReceiveItemLocation_Index;
                //        resultItem.goodsTransferItem_Index = item.GoodsTransferItem_Index;
                //        if (data.ambientRoom != "02")
                //        {
                //            resultItem.ambientRoom = "Ambient";
                //        }
                //        else
                //        {
                //            resultItem.ambientRoom = "Freeze";
                //        }
                //        result.Add(resultItem);
                //        num++;
                //    }
                //}
                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportLaborUtilization");

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

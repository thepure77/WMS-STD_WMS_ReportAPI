﻿using AspNetCore.Reporting;
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

namespace ReportBusiness.ReportCheckStockOnHandPartial
{
    public class ReportCheckStockOnHandPartialService
    {
        #region printReport
        public dynamic printReportCheckStockOnHandPartial(ReportCheckStockOnHandPartialViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            //var BB_DBContext = new BinbalanceDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var GI_DBContext = new PlanGIDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportCheckStockOnHandPartialViewModel>();

            try
            {
                var product_ID = "";
                //var product_Name = "";
                var location_Name = "";
                var tag_No = "";
                var statusModels = new List<int?>();
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_ID = data.product_Id;
                }
                //if (!string.IsNullOrEmpty(data.product_Name))
                //{
                //    product_Name = data.product_Name;
                //}
                if (!string.IsNullOrEmpty(data.location_Name))
                {
                    location_Name = data.location_Name;
                }
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    tag_No = data.tag_No;
                }

                var pro_id = new SqlParameter("@PRO_ID", product_ID);
                //var pro_name = new SqlParameter("@PRO_NAME", product_Name);
                var loc_Name = new SqlParameter("@LO_NAME", location_Name);
                var tag = new SqlParameter("@TAG", tag_No);
                var resultquery = new List<MasterDataDataAccess.Models.sp_ReportCheckStockOnHandPartial>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_ReportCheckStockOnHandPartial.FromSql("sp_ReportCheckStockOnHandPartial @PRO_ID,@LO_NAME,@TAG", pro_id, loc_Name, tag).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_ReportCheckStockOnHandPartial.FromSql("sp_ReportCheckStockOnHandPartial @PRO_ID,@LO_NAME,@TAG", pro_id, loc_Name, tag).ToList();
                }

                //var resultquery = Master_DBContext.sp_ReportCheckStockOnHandPartial.FromSql("sp_ReportCheckStockOnHandPartial @PRO_ID,@LO_NAME,@TAG", pro_id, loc_Name, tag).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportCheckStockOnHandPartialViewModel();
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

                        var resultItem = new ReportCheckStockOnHandPartialViewModel();
                        resultItem.rowNum = num + 1;
                        resultItem.location_Name = item.Location_Name;
                        resultItem.tag_No = item.Tag_No;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.itemStatus_Name = item.ItemStatus_Name;
                        resultItem.goodsReceive_EXP_Date = item.GoodsReceive_EXP_Date != null ? item.GoodsReceive_EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.su_QtyBal = item.SU_QtyBal;
                        resultItem.su_QtyReserve = item.SU_QtyReserve;
                        resultItem.su_QtyOnHand = item.SU_QtyOnHand;
                        resultItem.su_UNIT = item.SU_UNIT;
                        resultItem.erp_Location = item.ERP_Location;
                        resultItem.ageRemain = item.AgeRemain;
                        resultItem.productShelfLife_D = item.ProductShelfLife_D;
                        resultItem.shelfLife_Remian = item.ShelfLife_Remian;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportCheckStockOnHandPartial");
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
        #endregion

        public string ExportExcel(ReportCheckStockOnHandPartialViewModel data, string rootPath = "")
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
            var result = new List<ReportCheckStockOnHandPartialViewModel>();
            try
            {
                var product_ID = "";
                //var product_Name = "";
                var location_Name = "";
                var tag_No = "";
                var statusModels = new List<int?>();
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_ID = data.product_Id;
                }
                //if (!string.IsNullOrEmpty(data.product_Name))
                //{
                //    product_Name = data.product_Name;
                //}
                if (!string.IsNullOrEmpty(data.location_Name))
                {
                    location_Name = data.location_Name;
                }
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    tag_No = data.tag_No;
                }

                var pro_id = new SqlParameter("@PRO_ID", product_ID);
                //var pro_name = new SqlParameter("@PRO_NAME", product_Name);
                var loc_Name = new SqlParameter("@LO_NAME", location_Name);
                var tag = new SqlParameter("@TAG", tag_No);
                var resultquery = new List<MasterDataDataAccess.Models.sp_ReportCheckStockOnHandPartial>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_ReportCheckStockOnHandPartial.FromSql("sp_ReportCheckStockOnHandPartial @PRO_ID,@LO_NAME,@TAG", pro_id, loc_Name, tag).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_ReportCheckStockOnHandPartial.FromSql("sp_ReportCheckStockOnHandPartial @PRO_ID,@LO_NAME,@TAG", pro_id, loc_Name, tag).ToList();
                }

                //var resultquery = Master_DBContext.sp_ReportCheckStockOnHandPartial.FromSql("sp_ReportCheckStockOnHandPartial @PRO_ID,@LO_NAME,@TAG", pro_id, loc_Name, tag).ToList();
                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportCheckStockOnHandPartialViewModel();
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

                        var resultItem = new ReportCheckStockOnHandPartialViewModel();
                        resultItem.rowNum = num + 1;
                        resultItem.location_Name = item.Location_Name;
                        resultItem.tag_No = item.Tag_No;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.itemStatus_Name = item.ItemStatus_Name;
                        resultItem.goodsReceive_EXP_Date = item.GoodsReceive_EXP_Date != null ? item.GoodsReceive_EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.su_QtyBal = item.SU_QtyBal;
                        resultItem.su_QtyReserve = item.SU_QtyReserve;
                        resultItem.su_QtyOnHand = item.SU_QtyOnHand;
                        resultItem.su_UNIT = item.SU_UNIT;
                        resultItem.erp_Location = item.ERP_Location;
                        resultItem.ageRemain = item.AgeRemain;
                        resultItem.productShelfLife_D = item.ProductShelfLife_D;
                        resultItem.shelfLife_Remian = item.ShelfLife_Remian;
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
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportCheckStockOnHandPartial");

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

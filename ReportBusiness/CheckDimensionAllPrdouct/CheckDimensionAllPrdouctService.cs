using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using PlanGRBusiness.Libs;
using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportBusiness.CheckDimensionAllPrdouct
{
    public class CheckDimensionAllPrdouctService
    {
        #region printReportLaborPerformance
        public dynamic printReportPan(CheckDimensionAllPrdouctViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();
            //var BB_DBContext = new BinbalanceDbContext();


            //var GI_DBContext = new PlanGIDbContext();
            //@PRO_ID
            //    @PRO_NAME
            //    @TAG
            //    @LOC
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<CheckDimensionAllPrdouctViewModel>();

            try
            {

                var product_Id = "";
                //var product_Name = "";
                var sale_Unit = "";
                
                //var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();

                var statusModels = new List<int?>();

                if (!string.IsNullOrEmpty(data.product_ID))
                {
                    product_Id = data.product_ID;
                }
                //if (!string.IsNullOrEmpty(data.product_Name))
                //{
                //    product_Name = data.product_Name;
                //}
                if (!string.IsNullOrEmpty(data.sale_UNIT))
                {
                    sale_Unit = data.sale_UNIT;
                }
                //@PRO_ID 
                //@PRO_NAME 
                //@SU

                var pro_Id = new SqlParameter("@PRO_ID", product_Id);
                //var pro_Name = new SqlParameter("@PRO_NAME", product_Name);
                var su = new SqlParameter("@SU", sale_Unit);
                var resultquery = new List<MasterDataDataAccess.Models.sp_CheckDimensionAllPrdouct>();
                var room_name = "";
                if (data.ambientRoom != "02")
                {
                    var ambient = "1";
                    var room = new SqlParameter("@ROOM", ambient);
                    resultquery = Master_DBContext.sp_CheckDimensionAllPrdouct.FromSql("sp_CheckDimensionAllPrdouct @PRO_ID , @SU , @ROOM", pro_Id,su,room).ToList();
                    room_name = "Ambient";
                    
                }
                else
                {
                    var freeze = "4";
                    var room = new SqlParameter("@ROOM", freeze);
                    resultquery = temp_Master_DBContext.sp_CheckDimensionAllPrdouct.FromSql("sp_CheckDimensionAllPrdouct @PRO_ID , @SU ,@ROOM ", pro_Id,su,room).ToList();
                    room_name = "Freeze";
                }


                //var resultquery = Master_DBContext.sp_CheckDimensionAllPrdouct.FromSql("sp_CheckDimensionAllPrdouct @PRO_ID  , @SU", pro_Id, su).ToList();
                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new CheckDimensionAllPrdouctViewModel();
                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.report_date = startDate;
                    resultItem.report_date_to = endDate;
                    //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
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

                        var resultItem = new CheckDimensionAllPrdouctViewModel();
                        resultItem.rowNo = num + 1; ;
                        resultItem.product_ID = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.bu_UNIT = item.BU_UNIT;
                        resultItem.sale_UNIT = item.SALE_UNIT;
                        resultItem.in_UNIT = item.IN_UNIT;
                        resultItem.productConversion_Name = item.UNIT;
                        resultItem.productConversion_Ratio = item.Ratio;
                        resultItem.productConversion_Weight = item.Weight;
                        resultItem.productConversion_GrsWeight = item.GrsWeight;
                        resultItem.productConversion_Width = item.W;
                        resultItem.productConversion_Length = item.L;
                        resultItem.productConversion_Height = item.H;
                        resultItem.ti = item.TI;
                        resultItem.hi = item.HI;
                        resultItem.isPiecePcik = item.IsPiecePcik;
                        resultItem.productItemLife_D = item.ProductItemLife_D;
                        resultItem.productShelfLifeGI_D = item.ProductShelfLifeGI_D;
                        resultItem.productShelfLifeGR_D = item.ProductShelfLifeGR_D;
                        resultItem.productConversionBarcode = item.ProductConversionBarcode;
                        resultItem.sup = item.SUP;
                        resultItem.isMfgDate = item.IsMfgDate;
                        resultItem.isExpDate = item.IsExpDate;
                        resultItem.isLot = item.IsLot;
                        resultItem.isSerial = item.IsSerial;
                        resultItem.bu_qty_Per_Tag = item.BU_Qty_Per_Pallet;
                        resultItem.qty_Per_Tag = item.Qty_Per_Pallet;
                        resultItem.ambientRoom = room_name;
                        result.Add(resultItem);                    
                        num++;
                    }
                }





                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("CheckDimensionAllPrdouct");
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

        public string ExportExcel(CheckDimensionAllPrdouctViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();
            //var BB_DBContext = new BinbalanceDbContext();


            //var GI_DBContext = new PlanGIDbContext();
            //@PRO_ID
            //    @PRO_NAME
            //    @TAG
            //    @LOC
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<CheckDimensionAllPrdouctViewModel>();

            try
            {

                var product_Id = "";
                //var product_Name = "";
                var sale_Unit = "";

                //var query = GI_DBContext.View_RPT_PlanGI.AsQueryable();

                var statusModels = new List<int?>();

                if (!string.IsNullOrEmpty(data.product_ID))
                {
                    product_Id = data.product_ID;
                }
                //if (!string.IsNullOrEmpty(data.product_Name))
                //{
                //    product_Name = data.product_Name;
                //}
                if (!string.IsNullOrEmpty(data.sale_UNIT))
                {
                    sale_Unit = data.sale_UNIT;
                }
                //@PRO_ID 
                //@PRO_NAME 
                //@SU

                var pro_Id = new SqlParameter("@PRO_ID", product_Id);
                //var pro_Name = new SqlParameter("@PRO_NAME", product_Name);
                var su = new SqlParameter("@SU", sale_Unit);
                var resultquery = new List<MasterDataDataAccess.Models.sp_CheckDimensionAllPrdouct>();
                var room_name = "";
                if (data.ambientRoom != "02")
                {
                    var ambient = "1";
                    var room = new SqlParameter("@ROOM", ambient);
                    resultquery = Master_DBContext.sp_CheckDimensionAllPrdouct.FromSql("sp_CheckDimensionAllPrdouct @PRO_ID , @SU , @ROOM", pro_Id, su, room).ToList();
                    room_name = "Ambient";

                }
                else
                {
                    var freeze = "4";
                    var room = new SqlParameter("@ROOM", freeze);
                    resultquery = temp_Master_DBContext.sp_CheckDimensionAllPrdouct.FromSql("sp_CheckDimensionAllPrdouct @PRO_ID , @SU ,@ROOM ", pro_Id, su, room).ToList();
                    room_name = "Freeze";
                }


                //var resultquery = Master_DBContext.sp_CheckDimensionAllPrdouct.FromSql("sp_CheckDimensionAllPrdouct @PRO_ID  , @SU", pro_Id, su).ToList();
                //var resultquery = query.ToList();

                if (resultquery.Count == 0)
                {
                    var resultItem = new CheckDimensionAllPrdouctViewModel();
                    var startDate = DateTime.ParseExact(data.report_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.report_date_to.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.report_date = startDate;
                    resultItem.report_date_to = endDate;
                    //resultItem.printDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
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

                        var resultItem = new CheckDimensionAllPrdouctViewModel();
                        resultItem.rowNo = num + 1; ;
                        resultItem.product_ID = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.bu_UNIT = item.BU_UNIT;
                        resultItem.sale_UNIT = item.SALE_UNIT;
                        resultItem.in_UNIT = item.IN_UNIT;
                        resultItem.productConversion_Name = item.UNIT;
                        resultItem.productConversion_Ratio = item.Ratio;
                        resultItem.productConversion_Weight = item.Weight;
                        resultItem.productConversion_GrsWeight = item.GrsWeight;
                        resultItem.productConversion_Width = item.W;
                        resultItem.productConversion_Length = item.L;
                        resultItem.productConversion_Height = item.H;
                        resultItem.ti = item.TI;
                        resultItem.hi = item.HI;
                        resultItem.isPiecePcik = item.IsPiecePcik;
                        resultItem.productItemLife_D = item.ProductItemLife_D;
                        resultItem.productShelfLifeGI_D = item.ProductShelfLifeGI_D;
                        resultItem.productShelfLifeGR_D = item.ProductShelfLifeGR_D;
                        resultItem.productConversionBarcode = item.ProductConversionBarcode;
                        resultItem.sup = item.SUP;
                        resultItem.isMfgDate = item.IsMfgDate;
                        resultItem.isExpDate = item.IsExpDate;
                        resultItem.isLot = item.IsLot;
                        resultItem.isSerial = item.IsSerial;
                        resultItem.bu_qty_Per_Tag = item.BU_Qty_Per_Pallet;
                        resultItem.qty_Per_Tag = item.Qty_Per_Pallet;
                        resultItem.ambientRoom = room_name;
                        result.Add(resultItem);
                        num++;
                    }
                }




                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("CheckDimensionAllPrdouct");

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

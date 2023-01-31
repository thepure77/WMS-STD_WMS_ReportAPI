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

namespace ReportBusiness.ReportSerialNumber
{
    public class ReportSerialNumberService
    {

        #region ReportSerialNumber
        public dynamic ReportSerialNumber(ReportSerialNumberRequestModel data, string rootPath = "" )
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportSerialNumberExModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var room_Name = "";
                var Product_Id = "";
                var Tag_No = "";
                var GoodsIssue_No = "";
                var PlanGoodsIssue_No = "";
                var dateStart = "";
                var dateEnd = "";

                if (!string.IsNullOrEmpty(data.Product_Id))
                {
                    Product_Id = data.Product_Id;
                }
                if (!string.IsNullOrEmpty(data.Tag_No))
                {
                    Tag_No = data.Tag_No;
                }
                if (!string.IsNullOrEmpty(data.GoodsIssue_No))
                {
                    GoodsIssue_No = data.GoodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.PlanGoodsIssue_No))
                {
                    PlanGoodsIssue_No = data.PlanGoodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.goodsIssue_date) && !string.IsNullOrEmpty(data.goodsIssue_date_to))
                {
                    dateStart = data.goodsIssue_date;
                    dateEnd = data.goodsIssue_date_to;
                }

                var goodsIssue_No = new SqlParameter("@GoodsIssue_No", GoodsIssue_No);
                var pro_Id = new SqlParameter("@Product_Id", Product_Id);
                var tag_No = new SqlParameter("@Tag_No", Tag_No);
                var planGoodsIssue_No = new SqlParameter("@DO_No", PlanGoodsIssue_No);
                var gi_date = new SqlParameter("@GoodsIssue_Date_From", dateStart);
                var gi_date_to = new SqlParameter("@GoodsIssue_Date_To", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_ReportGI_SN>();

                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_ReportGI_SN.FromSql("sp_ReportGI_SN @GoodsIssue_No , @Product_Id , @Tag_No , @DO_No , @GoodsIssue_Date_From ,@GoodsIssue_Date_To", goodsIssue_No, pro_Id, tag_No, planGoodsIssue_No, gi_date, gi_date_to).ToList();
                    room_Name = "Ambient";
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_ReportGI_SN.FromSql("sp_ReportGI_SN @GoodsIssue_No , @Product_Id , @Tag_No , @DO_No , @GoodsIssue_Date_From ,@GoodsIssue_Date_To", goodsIssue_No, pro_Id, tag_No, planGoodsIssue_No, gi_date, gi_date_to).ToList();
                    room_Name = "Freeze";
                }

                if (resultquery.Count == 0){}
                else {
                    int num = 0;
                    foreach (var item in resultquery)
                    {
                        var resultItem = new ReportSerialNumberExModel();

                        resultItem.rowNo = num + 1;
                        resultItem.Date_now_form = DateTime.Now.ToString("dd/MM/yyyy  hh:mm:ss ") + " น.";
                        resultItem.Tag_No = item.Tag_No;
                        resultItem.Wave_Round = item.Wave_Round;
                        resultItem.GoodsIssue_No = item.GoodsIssue_No;
                        resultItem.GoodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.PlanGoodsIssue_No = item.DO_No;
                        resultItem.ShipTo_Id = item.ShipTo_Id;
                        resultItem.ShipTo_Name = item.ShipTo_Name;
                        resultItem.Product_Id = item.Product_Id;
                        resultItem.Product_Name = item.Product_Name;
                        resultItem.Serial = item.Serial;
                        resultItem.ambientRoom = room_Name;
                        result.Add(resultItem);
                        num++;

                    }
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\ReportSerialNumber\\ReportSerialNumber.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSerialNumber");
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
        public string ExportExcel(ReportSerialNumberRequestModel data, string rootPath = "")
        {
            //var DBContext = new PlanGRDbContext();
            //var GR_DBContext = new GRDbContext();
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportSerialNumberExModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);
                var room_Name = "";
                var Product_Id = "";
                var Tag_No = "";
                var GoodsIssue_No = "";
                var PlanGoodsIssue_No = "";
                var dateStart = "";
                var dateEnd = "";

                if (!string.IsNullOrEmpty(data.Product_Id))
                {
                    Product_Id = data.Product_Id;
                }
                if (!string.IsNullOrEmpty(data.Tag_No))
                {
                    Tag_No = data.Tag_No;
                }
                if (!string.IsNullOrEmpty(data.GoodsIssue_No))
                {
                    GoodsIssue_No = data.GoodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.PlanGoodsIssue_No))
                {
                    PlanGoodsIssue_No = data.PlanGoodsIssue_No;
                }
                if (!string.IsNullOrEmpty(data.goodsIssue_date) && !string.IsNullOrEmpty(data.goodsIssue_date_to))
                {
                    dateStart = data.goodsIssue_date;
                    dateEnd = data.goodsIssue_date_to;
                }

                var goodsIssue_No = new SqlParameter("@GoodsIssue_No", GoodsIssue_No);
                var pro_Id = new SqlParameter("@Product_Id", Product_Id);
                var tag_No = new SqlParameter("@Tag_No", Tag_No);
                var planGoodsIssue_No = new SqlParameter("@DO_No", PlanGoodsIssue_No);
                var gi_date = new SqlParameter("@GoodsIssue_Date_From", dateStart);
                var gi_date_to = new SqlParameter("@GoodsIssue_Date_To", dateEnd);
                var resultquery = new List<MasterDataDataAccess.Models.sp_ReportGI_SN>();

                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_ReportGI_SN.FromSql("sp_ReportGI_SN @GoodsIssue_No , @Product_Id , @Tag_No , @DO_No , @GoodsIssue_Date_From ,@GoodsIssue_Date_To", goodsIssue_No, pro_Id, tag_No, planGoodsIssue_No, gi_date, gi_date_to).ToList();
                    room_Name = "Ambient";
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_ReportGI_SN.FromSql("sp_ReportGI_SN @GoodsIssue_No , @Product_Id , @Tag_No , @DO_No , @GoodsIssue_Date_From ,@GoodsIssue_Date_To", goodsIssue_No, pro_Id, tag_No, planGoodsIssue_No, gi_date, gi_date_to).ToList();
                    room_Name = "Freeze";
                }

                if (resultquery.Count == 0) { }
                else
                {
                    int num = 0;
                    foreach (var item in resultquery)
                    {
                        var resultItem = new ReportSerialNumberExModel();

                        resultItem.rowNo = num + 1;
                        resultItem.Date_now_form = DateTime.Now.ToString("dd/MM/yyyy  hh:mm:ss ") + " น.";
                        resultItem.Tag_No = item.Tag_No;
                        resultItem.Wave_Round = item.Wave_Round;
                        resultItem.GoodsIssue_No = item.GoodsIssue_No;
                        resultItem.GoodsIssue_Date = item.GoodsIssue_Date != null ? item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.PlanGoodsIssue_No = item.DO_No;
                        resultItem.ShipTo_Id = item.ShipTo_Id;
                        resultItem.ShipTo_Name = item.ShipTo_Name;
                        resultItem.Product_Id = item.Product_Id;
                        resultItem.Product_Name = item.Product_Name;
                        resultItem.Serial = item.Serial;
                        resultItem.ambientRoom = room_Name;
                        result.Add(resultItem);
                        num++;

                    }
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSerialNumber");

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
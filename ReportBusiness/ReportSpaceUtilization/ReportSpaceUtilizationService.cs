using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using PlanGRBusiness.Libs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportBusiness.ReportSpaceUtilization
{
    public class ReportSpaceUtilizationService
    {
        #region printReportSpaceUtilization
        public dynamic printReportSpaceUtilization(ReportSpaceUtilizationViewModel data, string rootPath = "")
        {

            //var BB_DBContext = new BinbalanceDbContext();
            var M_DBContext = new MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<ReportSpaceUtilizationViewModel>();

            try
            {
                var dateNow = DateTime.Now;
                var Current_Date = dateNow.ToString("dd/MM/yyyy");
                var Current_Time = dateNow.ToString("HH:mm:ss");

                var query = M_DBContext.sp_Count_location.FromSql("sp_Count_location").ToList();

                if (!string.IsNullOrEmpty(data.LocationType_Name))
                {
                    query = query.Where(c => c.LocationType_Name == data.LocationType_Name).ToList();
                }

                foreach (var item in query)
                {
                    var resultItem = new ReportSpaceUtilizationViewModel();
                    resultItem.Current_Date = Current_Date;
                    resultItem.Current_Time = Current_Time;

                    resultItem.LocationType_Name = item.LocationType_Name;
                    resultItem.Count_location = item.Count_location;
                    resultItem.Count_IsUse = item.Count_IsUse;
                    resultItem.Count_Empty = item.Count_Empty;
                    resultItem.Count_Block = item.Count_Block;
                    resultItem.Per_IsUser = item.Per_IsUser;
                    resultItem.Per_Empty = item.Per_Empty;
                    resultItem.Per_Block = item.Per_Block;

                    result.Add(resultItem);
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSpaceUtilization");
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
                olog.logging("ReportSpaceUtilization", ex.Message);
                throw ex;
            }
        }
        #endregion

        public string ExportExcel(ReportSpaceUtilizationViewModel data, string rootPath = "")
        {

            var M_DBContext = new MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportSpaceUtilizationViewModel>();

            try
            {
                var dateNow = DateTime.Now;
                var Current_Date = dateNow.ToString("dd/MM/yyyy");
                var Current_Time = dateNow.ToString("HH:mm:ss");

                var query = M_DBContext.sp_Count_location.FromSql("sp_Count_location").ToList();

                if (!string.IsNullOrEmpty(data.LocationType_Name))
                {
                    query = query.Where(c => c.LocationType_Name == data.LocationType_Name).ToList();
                }

                foreach (var item in query)
                {
                    var resultItem = new ReportSpaceUtilizationViewModel();
                    resultItem.Current_Date = Current_Date;
                    resultItem.Current_Time = Current_Time;
                    resultItem.LocationType_Name = item.LocationType_Name;
                    resultItem.Count_location = item.Count_location;
                    resultItem.Count_IsUse = item.Count_IsUse;
                    resultItem.Count_Empty = item.Count_Empty;
                    resultItem.Count_Block = item.Count_Block;
                    resultItem.Per_IsUser = item.Per_IsUser;
                    resultItem.Per_Empty = item.Per_Empty;
                    resultItem.Per_Block = item.Per_Block;

                    result.Add(resultItem);
                }


                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSpaceUtilization");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "tmpReport" + DateTime.Now.ToString("yyyyMMddHHmmss");

                var renderedBytes = report.Execute(RenderType.Excel);
                fullPath = saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);

                //Utils objReport = new Utils();
                //fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);
                //var saveLocation = objReport.PhysicalPath(fileName + ".xls", rootPath);
                //return saveLocation;


                var saveLocation = rootPath + fullPath;
                ////File.Delete(saveLocation);
                ////ExcelRefresh(reportPath);
                return saveLocation;
            }
            catch (Exception ex)
            {
                olog.logging("ReportSpaceUtilization", ex.Message);
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
